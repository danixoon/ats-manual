using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ATSManual
{
    public partial class LabelTextBox : UserControl
    {

        public bool isChanged = false;

        [Description("Label text"), Category("Data")]
        public string LabelText
        {
            get { return label.Text; }
            set { label.Text = value; }
        }

        [Description("Mask"), Category("Data")]
        public string Mask
        {
            get { return textBox.Mask; }
            set { textBox.Mask = value; }
        }

        [Description("Is Read Only"), Category("Data")]
        public bool ReadOnly
        {
            get { return contentTextBox.ReadOnly; }
            set { contentTextBox.ReadOnly = value; }
        }

        [Browsable(true)]
        [Description("TextChanged")]
        [Category("Property Changed")]
        public new event EventHandler TextChanged = delegate { };

        public MaskedTextBox contentTextBox;


        public LabelTextBox()
        {
            InitializeComponent();
            this.contentTextBox = textBox;
        }

        private void textBox_TextChanged(object sender, EventArgs e)
        {
            this.TextChanged(sender, e);
        }
    }
}
