using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ATSManual.Database;

namespace ATSManual.Components.Subscriber
{
    public partial class SubscriberData : UserControl
    {

        public event EventHandler OnDelete = delegate { };
        string data;
        public SubscriberData(string data)
        {
            InitializeComponent();
            this.data = data;
            //this.Anchor = AnchorStyles.Left & AnchorStyles.Right;

            dataTextBox.Text = data;
            
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            this.OnDelete(sender, e);
        }
    }
}
