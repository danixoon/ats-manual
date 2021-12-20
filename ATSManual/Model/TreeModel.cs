using ATSManual.Forms;
using ATSManual.Storing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATSManual.Database;

namespace ATSManual.Model
{

    public class TreeModel : CacheResource
    {
        public List<SubscribersManager.ViewItem> items = new List<SubscribersManager.ViewItem>();


        public static async Task<string[][]> GetOwnedData()
        {
            List<string[]> keys = new List<string[]>();
            var cmd = ATSDataSet.Command($@"SELECT dat.[key], s.[phone] FROM [Data] dat INNER JOIN [SubscriberData] sub ON dat.[id]=sub.[dataId] INNER JOIN [Subscriber] s ON s.[id]=sub.[subscriberId]");
            foreach (var row in ATSDataSet.GetRows(await cmd.ExecuteReaderAsync()))
            {
                var key = row.GetString(0);
                var phone = row.GetInt32(1);
                keys.Add(new string[] { key, phone.ToString() });
            }

            return keys.ToArray();
        }

    }


}
