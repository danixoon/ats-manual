using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ATSManual.Forms
{
    public partial class DataSelectDialog : Form
    {
        private class DataCategory
        {
            public string name;
            public List<DataGroup> childs = new List<DataGroup>();

            public DataCategory(string name, List<DataGroup> childs)
            {
                this.name = name;
                this.childs = childs;
            }
            public DataCategory(string name)
            {
                this.name = name;
            }

            public override string ToString()
            {
                return name;
            }
        }

        private class DataGroup
        {
            public int size;
            public string name;

            public DataGroup(int size, string name = "Без названия")
            {
                this.size = size;
                this.name = name;
            }

            public override string ToString()
            {
                return name;
            }
        }


        private DataCategory selectedCategory;
        private DataCategory SelectedCategory
        {
            get { return selectedCategory; }
            set
            {
                if (selectedCategory != value)
                {
                    selectedCategory = value;
                    categoryComboBox.SelectedItem = value;



                    UpdateListView(value);
                }

            }
        }

        private string selectedData;
        public string SelectedData
        {
            get { return selectedData; }
            private set
            {
                if (selectedData == value) return;
                if (value.Length == 4)
                {

                    DataGroup group;
                    var category = FindCategoryWithData(int.Parse(value), out group);

                    //if (SelectedCategory != category)
                    //{
                    SelectedCategory = category;
                    //categoryComboBox.SelectedItem = category;
                    //UpdateListView(category);
                    //}



                    var item = dataListView.Items.Cast<ListViewItem>().FirstOrDefault(it => it.Tag.ToString() == value);
                    if (item != null)
                    {
                        if (!item.Selected)
                        {
                            item.Selected = true;
                            dataListView.EnsureVisible(item.Index);
                        }
                    }

                    if (dataTextBox.Text != value)
                        dataTextBox.Text = value;



                }

                this.selectedData = value;

            }
        }

        private DataCategory[] categories = new DataCategory[]
        {
            new DataCategory("(00-05) Хирургический корпус", new List<DataGroup>() {
                new DataGroup(5, "6 этаж, 1сс"),
                new DataGroup(5, "7 этаж, 1сс"),

                new DataGroup(5, "4 этаж, 1сс"),
                new DataGroup(5, "5 этаж, 1сс"),

                new DataGroup(3, "1 этаж, 1сс"),
                new DataGroup(3, "2 этаж, 1сс"),
                new DataGroup(3, "3 этаж, ЗНГ по РЛС"),
                new DataGroup(1),

                new DataGroup(1, "ЗНГ по ТЫЛУ"),
                new DataGroup(1, "ЗНГ по КАДРАМ"),
                new DataGroup(1, "ЗНГ по ТЕХ части"),
                new DataGroup(1, "ЗНГ по ОБЩИМ"),
                new DataGroup(1, "КОМАНДИР"),
                new DataGroup(1, "НАЧМЕД"),
                new DataGroup(1, "СЕКРЕТАРЬ"),
                new DataGroup(1, "1 этаж 4сс"),
                new DataGroup(1),
                new DataGroup(1),

                new DataGroup(2, "2 этаж"),
                new DataGroup(2, "1 этаж, 4сс, (Функциональная диагностика)"),
                new DataGroup(1, "2 этаж"),
                new DataGroup(2, "3 этаж, 3сс"),
                new DataGroup(1),
                new DataGroup(2, "3 этаж, серверная"),

                new DataGroup(3, "1 этаж, 2сс"),
                new DataGroup(2, "2 этаж, 4сс"),
                new DataGroup(2, "3 этаж, 2сс"),
                new DataGroup(1),
                new DataGroup(1),
                new DataGroup(1),
            }),
             new DataCategory("(06) Столовая", new List<DataGroup>() {
                new DataGroup(10),
            }),
             new DataCategory("(07) 1 КПП", new List<DataGroup>() {
                new DataGroup(10),
            }),
             new DataCategory("(08) Подвал диагностики", new List<DataGroup>() {
                new DataGroup(10),
            }),
             new DataCategory("(09-11) Терапевтический корпус", new List<DataGroup>() {
                new DataGroup(3, "1 этаж, начальник физио."),
                new DataGroup(2, "1 этаж, лабаратории"),
                new DataGroup(3, "2 этаж, 2 терапия"),
                new DataGroup(2, "2 этаж, диспансер"),

                new DataGroup(5, "3 этаж, 6 отделение"),
                new DataGroup(3, "2 этаж, холл"),
                new DataGroup(2),

                new DataGroup(3, "3 этаж, 6 отделение (гнойная 1сс)"),
                new DataGroup(2, "3 этаж, диспансер"),
                new DataGroup(3, "4 этаж, общая терапия"),
                new DataGroup(2, "5 этаж, пульмонология"),
            }),
             new DataCategory("(12) Батальон обеспечения", new List<DataGroup>() {
                new DataGroup(5),
                new DataGroup(3, "Котельная"),
                new DataGroup(1),
                new DataGroup(1),
            }),
             new DataCategory("13 сотня", new List<DataGroup>() {
                new DataGroup(10),
            }),
             new DataCategory("14 сотня", new List<DataGroup>() {
                new DataGroup(5, "Стоматология"),
                new DataGroup(5, "3 этаж, 2сс, (кардиология)"),
            }),
             new DataCategory("(15-18) Терапевтический корпус", new List<DataGroup>() {
                new DataGroup(5, "1 этаж, 3сс (ГБО, эндоскопия, рентгенхирургия)"),
                new DataGroup(5, "2 этаж, 3сс, (колопроктология)"),

                new DataGroup(5, "3 этаж, 3сс, (кардиология)"),
                new DataGroup(5, "4 этаж, 3сс, (служба связи)"),

                new DataGroup(5, "1 этаж, 2сс, (физиотерапия)"),
                new DataGroup(5, "2 этаж, 2сс, (гастроэнетрология)"),

                new DataGroup(5, "4 этаж, 2сс, (офтальмология)"),
                new DataGroup(5, "5 этаж, 2сс, (1 терапия)"),
            }),
             new DataCategory("19 сотня", new List<DataGroup>() {
                new DataGroup(10, "Транзит на АТС-2"),
            }),
             new DataCategory("(20) Автопарк", new List<DataGroup>() {
                new DataGroup(10),
            }),
             new DataCategory("(21) Склады", new List<DataGroup>() {
                new DataGroup(10),
            }),
             new DataCategory("(22) Консультативный корпус", new List<DataGroup>() {
                new DataGroup(2, "1 этаж"),
                new DataGroup(2, "2 этаж"),
                new DataGroup(1, "4 этаж"),
                new DataGroup(5),
            }),
             new DataCategory("(23) Аптека, архив", new List<DataGroup>() {
                new DataGroup(3, "1 этаж, аптека"),
                new DataGroup(2, "2 этаж, аптека"),
                new DataGroup(2, "1 этаж, архив"),
                new DataGroup(2, "2 этаж, архив"),
                new DataGroup(1, "3 этаж"),
            }),
             new DataCategory("24 сотня", new List<DataGroup>() {
                new DataGroup(5, "ВИЧ лаболатория МОСН"),
                new DataGroup(5, "Паталого-анатомическое отделение"),
            }),
             new DataCategory("(25) Лечебный корпус", new List<DataGroup>() {
                new DataGroup(2, "4 этаж"),
                new DataGroup(3, "3 этаж"),
                new DataGroup(2, "2 этаж"),
                new DataGroup(1, "1 этаж"),
                new DataGroup(2, "1 этаж"),
            }),
             new DataCategory("26 сотня", new List<DataGroup>() {
                new DataGroup(5, "Инфекционное отделение"),
                new DataGroup(5),
            }),
             new DataCategory("27 сотня", new List<DataGroup>() {
                new DataGroup(10),
            }),
             new DataCategory("28 сотня", new List<DataGroup>() {
                new DataGroup(10),
            }),
             new DataCategory("29 сотня", new List<DataGroup>() {
                new DataGroup(10),
            }),
        };

        int maxData;

        public DataSelectDialog()
        {
            InitializeComponent();
            maxData = categories.Aggregate(0,
                    (acc, cat) => acc + cat.childs
                    .Aggregate(0,
                        (childAcc, child) => childAcc + child.size * 10));

        }


        private DataCategory FindCategoryWithData(int data, out DataGroup group)
        {
            int offset = 0;
            foreach (var cat in categories)
            {
                foreach (var gr in cat.childs)
                {
                    group = gr;
                    offset += gr.size * 10;
                    if (data < offset) return cat;
                }
            }

            group = null;
            return null;
        }

        private void dataList_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();

            Graphics g = e.Graphics;
            ListBox box = (ListBox)sender;

            g.DrawString(box.Items[e.Index].ToString(), e.Font, (e.State & DrawItemState.Selected) != 0 ? new SolidBrush(SystemColors.HighlightText) : new SolidBrush(SystemColors.WindowText), new PointF(e.Bounds.X, e.Bounds.Y));

            e.DrawFocusRectangle();
        }

        private void buildingComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedCategory = (DataCategory)categoryComboBox.SelectedItem;
            //dataList.Items.Clear();
            //dataList.Items.AddRange(((DataCategory)categoryComboBox.SelectedItem).childs.ToArray());
        }


        public void SelectData(string id)
        {
            //selectedData = id;
            //dataTextBox.Text = id;


            //foreach (DataGroup item in dataList.Items.Cast<DataGroup>())
            //{
            //    if (item.id == id)
            //    {
            //        dataList.SelectedItem = item;
            //        break;
            //    }
            //}
        }

        private void dataTextBox_TextChanged(object sender, EventArgs e)
        {
            SelectedData = dataTextBox.Text;
            //if (dataTextBox.Text != selectedData)
            //{
            //    //SelectData(dataTextBox.Text);
            //}
        }

        private void submitButton_Click(object sender, EventArgs e)
        {
            Submit();
        }

        private string[][] ownedData;

        private void UpdateListView(DataCategory category)
        {



            dataListView.Groups.Clear();
            dataListView.Items.Clear();

            if (category == null) return;

            int offset = categories
                .TakeWhile(cat => cat != category)
                .Aggregate(0,
                    (acc, cat) => acc + cat.childs
                    .Aggregate(0,
                        (childAcc, child) => childAcc + child.size * 10));


            for (int i = 0; i < category.childs.Count; i++)
            {
                var group = category.childs[i];
                var listGroup = new ListViewGroup(i.ToString(), group.name);
                dataListView.Groups.Add(listGroup);

                var pairAmount = group.size * 10;

                for (int j = 0; j < pairAmount; j++)
                {
                    var id = (j + offset);
                    var _id = id.ToString();
                    _id = (4 - _id.Length > 0) ? new string('0', 4 - _id.Length) + _id : _id;

                    var item = new ListViewItem(_id, listGroup) { Tag = _id, Font = new Font(SystemFonts.StatusFont.FontFamily, 12f), BackColor = ownedData.Select(data => data[0]).Contains(_id) ? Color.Aqua : SystemColors.Window };

                    dataListView.Items.Add(item);
                }

                offset += pairAmount;
            }



        }

        private async void DataSelectDialog_Load(object sender, EventArgs e)
        {
            categoryComboBox.Items.AddRange(categories);
            ownedData = await Model.TreeModel.GetOwnedData();

            SelectedCategory = categories[0];
        }

        private void dataListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            var item = dataListView.SelectedItems.Count > 0 ? dataListView.SelectedItems[0] : null;
            if (item != null)
                SelectedData = item.Tag.ToString();
        }

        private int FindDataIndexWithName(string name)
        {

            int offset = 0;
            foreach (var cat in categories)
            {
                foreach (var gr in cat.childs)
                {
                    if (gr.name.ToLower().Contains(name.ToLower())) return offset + 1;
                    offset += gr.size * 10;
                }
            }

            return -1;
        }

        private void filterTextBox_TextChanged(object sender, EventArgs e)
        {
            var id = FindDataIndexWithName(filterTextBox.contentTextBox.Text);
            if (dataListView.Items.Count > 0 && id != -1 && id < dataListView.Items.Count) dataListView.EnsureVisible(id);
        }

        private void dataListView_DoubleClick(object sender, EventArgs e)
        {
            Submit();
        }

        private void Submit()
        {
            if (SelectedData != null && SelectedData.Length == 4)
            {
                var selected = int.Parse(SelectedData);

                if (selected > maxData)
                {
                    MessageBox.Show($"Выбраные данные вне диапазона возможных значений", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var owned = ownedData.FirstOrDefault(data => data[0] == SelectedData);

                if (owned != null)
                {
                    MessageBox.Show($"Невозможно добавить данные: данные уже привязаны к номеру {owned[1]}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}