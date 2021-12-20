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
    public partial class Lol : Form
    {
        private string[] messages = new string[] {
            "лох", "пидр", "псина", "лох,пидр!", "мудак", "печел", "пидорас", "гандон",
            "убогая шлюха", "ебанушка", "котик","член","блядина","ОлЕг,","сосун пенисов,","токманцев","кащолка,","красаучик","сосок чибы","чернуха","ержан","осёл ебаный","ара"
        };

        bool isStopped = false;

        public Lol()
        {
            InitializeComponent();
            button1.Click += (o, e) =>
            {
                if (isStopped) { isStopped = false; Prekol(); }
                else
                    isStopped = true;
            };
            Prekol();
        }

        public async void Shuffle()
        {
            var rnd = new System.Random();
            while (!isStopped)
            {
                var dx = (rnd.Next() % 10);
                var dy = (rnd.Next() % 10);
                if (rnd.Next() % 2 > 0) dx *= -1;
                if (rnd.Next() % 2 > 0) dy *= -1;

                this.Location = new Point(this.Location.X + dx, this.Location.X + dy);
                await Task.Delay(50);
            }
        }

        public async void Prekol()
        {
            Shuffle();
            var rnd = new System.Random();
            while (!isStopped)
            {
                button1.Text = messages[rnd.Next() % messages.Length];
                button1.Font = new Font(DefaultFont.FontFamily, 16f + (float)rnd.NextDouble() * 40f, FontStyle.Regular);
                await Task.Delay(100);
            }
        }
    }
}
