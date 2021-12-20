using ATSManual.Storing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ATSManual.Model
{
    public class TreeFilterModel : CacheResource
    {
        private string subscriber = "";
        public string Subscriber { get { return subscriber; } set { subscriber = value.Trim().ToLower(); } }

        private string data = "";
        public string Data { get { return data; } set { data = value.Trim().ToLower(); } }

        public bool WithoutData { get; set; } = false;
        public bool WithoutSubscriber { get; set; } = false;

        public IEnumerable<int> StatusFilters { get; set; } = new int[] { };

        private void Filter()
        {

        }
    }
}
