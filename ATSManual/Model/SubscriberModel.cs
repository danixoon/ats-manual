using ATSManual.Storing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATSManual.Database;

namespace ATSManual.Model
{

    public enum SubscriberStatus
    {
        Ok = 0,
        Invalid = 1,
        Checking = 2,
        Disabled = 3,
        Unknown = 4
    }

    // TODO: Сделать систему бит-флагов на множественные состояния абонента
    public enum SubscriberFlag
    {
        Disabled = 1,
        Empty = 2
    }

    public class SubscriberModel : CacheResource
    {

        public static string ParseStatus(SubscriberStatus status)
        {
            switch (status)
            {
                case SubscriberStatus.Ok:
                    return "В работе";
                case SubscriberStatus.Checking:
                    return "Необходима проверка";
                case SubscriberStatus.Invalid:
                    return "Ошибочный";
                case SubscriberStatus.Disabled:
                    return "На фибре";
                case SubscriberStatus.Unknown:
                    return "Нет информации";
                default:
                    throw new Exception("Необрабатываемое состояние.");
            }
        }

        public static IEnumerable<string> GetStatuses()
        {
            var arr = Enum.GetNames(typeof(Model.SubscriberStatus));

            return arr.Select((item, i) => ParseStatus((SubscriberStatus)i));

        }

        public bool IsEditMode { get; set; } = false;

        public bool IsMultiple { get { return ViewItems.Length > 1; } }
        public SubscribersManager.ViewItem[] ViewItems { get; set; } = new SubscribersManager.ViewItem[] { };
        public SubscribersManager.ViewItem ViewItem { get { return ViewItems.Length > 0 ? ViewItems[0] : null; } }

        public Dictionary<string, string> changes = new Dictionary<string, string>();
        public bool HasChanges { get { return changes.Count > 0 || removedData.Count > 0 || addedData.Count > 0; } }
        public List<string> removedData = new List<string>();
        public List<string> addedData = new List<string>();

        public static async Task AddSubscriber(int phone, string name, string description)
        {
            var cmd = ATSDataSet.Command($"INSERT INTO [Subscriber] ([phone], [name], [description]) VALUES ('{phone}', '{name}', '{description}')");
            var rowsAffected = await cmd.ExecuteNonQueryAsync();

            Logging.Logger.Log($"Создан абонент с номером {phone}");
        }

        public static async Task RemoveSubscribers(params int[] ids)
        {
            var cmd = ATSDataSet.Command($"DELETE FROM [Subscriber] OUTPUT DELETED.[phone] WHERE [id] IN ('{string.Join("','", ids)}')");
            List<int> deletedPhones = new List<int>();
            ATSDataSet.ReadRows(await cmd.ExecuteReaderAsync(), (reader) =>
            {
                var phone = reader.GetInt32(0);
                deletedPhones.Add(phone);
            });
            Logging.Logger.Log($"Абонент {string.Join(", ", deletedPhones)} удалён из базы данных");
        }
    }
}
