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
    public partial class AddSubscriberDialog : Form
    {
        public AddSubscriberDialog()
        {
            InitializeComponent();
        }

        private async void addSubscriberButton_Click(object sender, EventArgs e)
        {
            int phone;
            try
            {
                phone = int.Parse(phoneTextBox.contentTextBox.Text);


            }
            catch (Exception ex)
            {
                MessageBox.Show("Некорректно задан номер телефона: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var name = nameTextBox.contentTextBox.Text;
            var desc = descriptionTextBox.contentTextBox.Text;
            var task = App.action.AddSubscriber(phone, name, desc);

            this.Enabled = false;

            try
            {
                await task;
            }
            catch (Exception ex)
            {
                if (ex is System.Data.SqlClient.SqlException)
                {
                    var sqlEx = (System.Data.SqlClient.SqlException)ex;
                    if (sqlEx.Number == 2627)
                        MessageBox.Show($"Абонент с номером {phone} уже присутствует в базе данных.", "Дубликат абонента", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                else
                {
                    MessageBox.Show("Произошла ошибка: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logging.Logger.Log("Ошибка создания нового абонента: " + ex.Message, Logging.Logger.MessageType.Error);
                }
            }

            this.Enabled = true;
            addSubscriberButton.Text = "Добавить";
        }
    }
}
