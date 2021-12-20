using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ATSManual.Components
{
    public partial class HistoryItem : UserControl
    {
        public event EventHandler<int> OnSelected = delegate { };
        public int Phone { get; private set; }
        public HistoryItem(int phone, string label)
        {
            InitializeComponent();
            Phone = phone;

            subscriberName.Text = label;
            subscriberPhone.Text = Phone.ToString();

        }

        private void subscriberPhone_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OnSelected(this, this.Phone);
        }

        private void HistoryItem_Load(object sender, EventArgs e)
        {
            Width = Parent.Width;
            Anchor = AnchorStyles.Left & AnchorStyles.Right;
            MaximumSize = new Size(0, 0);
        }
    }
}
