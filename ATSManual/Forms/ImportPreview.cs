using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using ATSManual.Database;
using System.Text.RegularExpressions;

namespace ATSManual.Forms
{
    public partial class ImportPreview : Form
    {
        private List<ImportData> data;
        public ImportPreview(List<ImportData> data)
        {

            this.data = data;
            InitializeComponent();

            foreach (var row in this.data)
            {
                var dataRow = new DataGridViewRow();
                var rowData = row.GetArray();
                for (int i = 0; i < previewTable.Columns.Count; i++)
                {
                    var cell = new DataGridViewTextBoxCell();
                    cell.Value = rowData[i];

                    var color = ManualForm.ConvertStatus(row.statusType);
                    if (color != null)
                        cell.Style.BackColor = color.Value;

                    dataRow.Cells.Add(cell);
                }

                previewTable.Rows.Add(dataRow);
            }
        }



        private async void importButton_Click(object sender, EventArgs e)
        {



            var dataItems = this.data.SelectMany(d => d.subscriberData.Select(_d => _d.Trim())).ToList();

            var dups = dataItems.GroupBy(item => item).Where(dup => !string.IsNullOrWhiteSpace(dup.Key) && dup.Count() > 1).ToList();
            if (dups.Count > 0)
            {

                MessageBox.Show($"Ошибка импорта: Данные {string.Join(", ", dups.Select(d => d.Key))} прописаны для нескольких разных номеров", "Дубликат данных", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var subDups = this.data.GroupBy(item => item.subscriberPhone).Where(dup => dup.Count() > 1);

            if (subDups.Count() > 0)
            {
                MessageBox.Show($"Ошибка импорта: Абонент {string.Join(", ", subDups.Select(d => d.Key))} имеет дубликаты", "Дубликат абонента", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var dataValues = dataItems.Select(dd => $"'{dd}'");
            var subValues = this.data.Select(d => $"'{d.subscriberPhone}', '{d.subscriberName}', '{d.description}', '{(int)d.statusType}'");


            await App.action.ClearDb();

            var insertDataCommand = ATSDataSet.Command($"INSERT INTO [Data] ([key]) OUTPUT INSERTED.[id] VALUES ({string.Join("),(", dataValues)})");
            var insertDataReader = insertDataCommand.ExecuteReader();
            var insertedDataIds = ATSDataSet.ReadRowsIds(insertDataReader).ToList();

            var insertSubCommand = ATSDataSet.Command($"INSERT INTO [Subscriber] ([phone], [name], [description], [statusType]) OUTPUT INSERTED.[id] VALUES ({string.Join("),(", subValues)})");
            var insertSubReader = insertSubCommand.ExecuteReader();
            var insertedSubIds = ATSDataSet.ReadRowsIds(insertSubReader).ToList();

            var subDataValues =
                this.data.SelectMany(
                    (d, i) =>
                    {

                        var result = new List<string>();
                        foreach (var data in d.subscriberData)
                        {
                            var dataId = dataItems.FindIndex(dv => dv == data);

                            var dataDbId = dataId != -1 ? $"'{insertedDataIds[dataId]}'" : "NULL";

                            result.Add($"'{insertedSubIds[i]}', {dataDbId}");
                        }

                        return result;
                    })
                    .ToList();

            var insertSubDataCommand = ATSDataSet.Command($"INSERT INTO [SubscriberData] (subscriberId, dataId) VALUES ({string.Join("),(", subDataValues)})");

            //try
            //{

            var insertedSubData = insertSubDataCommand.ExecuteNonQuery();
            //}
            //catch (SqlException ex)
            //{
            //    if (ex.Number == 2627)
            //    {
            //        var match = Regex.Match(ex.Message, "is \\((.+)\\)");
            //        var id = int.Parse(Regex.Match(match.ToString(), "\\d+").ToString());

            //        var index = insertedDataIds.FindIndex(d => d == id);
            //        var dataItem = dataItems[index];

            //        MessageBox.Show($"Ошибка импорта: Данные {dataItem} прописаны для двух разных номеров.", "Дубликат данных", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        return;
            //    }
            //}


            //MessageBox.Show("Успешный импорт.");

            Logging.Logger.Log("База данных успешно импортирована", Logging.Logger.MessageType.Warning);
            App.action.FetchTreeData();
            Close();
        }
    }

    public class ImportData
    {
        public string subscriberName;
        public int subscriberPhone;
        public string[] subscriberData;
        public string description;
        public Model.SubscriberStatus statusType = Model.SubscriberStatus.Ok;

        public ImportData(string[] array, Model.SubscriberStatus statusType)
        {
            this.subscriberPhone = int.Parse(array[0]);
            this.subscriberName = array[1];
            this.subscriberData = array[2]?.Split(',').Select(d => d.Trim()).ToArray() ?? new string[0];
            this.description = array[3];
            this.statusType = statusType;
        }

        public string[] GetArray()
        {
            return new string[] {
                subscriberPhone.ToString(),
                subscriberName, subscriberData != null ? string.Join(", ", subscriberData) : null,
                description
            };
        }
    }
}
