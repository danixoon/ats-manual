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

namespace ATSManual.Forms
{
    public partial class DatabasePromt : Form
    {

        public string connectionString { get; private set; }
        public string DatabaseName { get { return databaseNameTextBox.Text; } }
        private bool newConnection = false;

        public DatabasePromt(bool newConnection = false)
        {
            InitializeComponent();
            DialogResult = DialogResult.Abort;
            this.newConnection = newConnection;
        }

        private async Task<bool> TestConnection(string message)
        {
            var connection = new SqlConnection(connectionString);
            var oldText = connectButton.Text;
            try
            {
                connectButton.Enabled = false;
                connectButton.Text = message;
                await connection.OpenAsync();


                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка подключения: " + ex.Message);
                return false;
            }
            finally
            {
                connectButton.Enabled = true;
                connectButton.Text = oldText;
            }

        }

        private async void connectButton_Click(object sender, EventArgs e)
        {
            connectionString = $"Data Source={ipAddressTextBox.Text},1433;Network Library=DBMSSOCN;Initial Catalog={databaseNameTextBox.Text};Integrated Security=False;User ID={usernameTextBox.Text};Password={passwordTextBox.Text};Connect Timeout=15;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            var result = await TestConnection("Подключение");
            if (result)
            {
                DialogResult = DialogResult.OK;

                if (saveConnectionCheckBox.Checked)
                {
                    App.config["DATABASE_CONNECTION_STRING"] = connectionString;
                    App.SaveConfig();
                }

                Close();
            }
        }

        private async void DatabasePromt_Load(object sender, EventArgs e)
        {
            if (!newConnection && App.config.ContainsKey("DATABASE_CONNECTION_STRING"))
            {
                connectionString = App.config["DATABASE_CONNECTION_STRING"];
                var result = await TestConnection("Подключение из config.ini");
                if (result)
                {
                    DialogResult = DialogResult.OK;
                    Close();
                }
            }

        }
    }
}
