using ATSManual.Components;
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
    public partial class SubscriberHistory : Form
    {
        public SubscriberHistory()
        {
            InitializeComponent();
            var item = new HistoryItem(4012, "АТС - коммутатор");
            var item2 = new HistoryItem(4013, "Номер - чей-то");

            historyContainer.RowStyles.Clear();
            historyContainer.RowStyles.Add(new RowStyle(SizeType.AutoSize, 26f));
            historyContainer.RowStyles.Add(new RowStyle(SizeType.AutoSize, 26f));
            historyContainer.RowStyles.Add(new RowStyle(SizeType.AutoSize, 26f));
            historyContainer.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));

            historyContainer.Controls.AddRange(new Control[] { item, item2 });
        }
    }
}
