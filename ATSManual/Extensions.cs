using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ATSManual
{
    public static class Extensions
    {
        public static void ForeachControl<T>(this Control parent, Action<T> action) where T : Control
        {
            var controls = new List<Control>() { parent };

            while (controls.Count != 0)
            {
                var control = controls.First();
                foreach (Control child in control.Controls)
                    controls.Add(child);

                if (control is T)
                    action((T)control);

                controls.Remove(control);
            }
        }
    }
}
