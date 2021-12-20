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
    public partial class LoadingDb : Form
    {
        public LoadingDb()
        {
            //Program.DbChanged += Program_DbChanged;

            InitializeComponent();
        }

        private void Program_DbChanged(object sender, string e)
        {
            this.Close();
        }

        private void LoadingDb_Load(object sender, EventArgs e)
        {
            //await Program.InitDb();
        }
    }
}
