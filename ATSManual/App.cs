using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATSManual.Storing;
using ATSManual.Components.Subscriber;
using System.Windows.Forms;
using ATSManual.Database;
using System.IO;

namespace ATSManual
{
    public class AppStore : Store
    {
        public Model.SubscriberModel SelectedSubscriber
        {
            get
            {
                return this.GetCache<Model.SubscriberModel>();
            }
        }

        public Model.TreeFilterModel TreeFilter
        {
            get
            {
                return this.GetCache<Model.TreeFilterModel>();
            }
        }

        public Model.AppModel App
        {
            get
            {
                return this.GetCache<Model.AppModel>();
            }
        }

        public Model.TreeModel Tree
        {
            get
            {
                return this.GetCache<Model.TreeModel>();
            }
        }

    }

    public static class App
    {
        public static bool IsProd
        {
            get
            {
#if DEBUG
                return false;
#else
                return true;
#endif
            }
        }

        public static Dictionary<string, string> config { get; set; } = new Dictionary<string, string>();
        public static void SaveConfig()
        {
            using (var stream = new FileStream("./config.ini", FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite))
            {
                var writer = new StreamWriter(stream);
                foreach (var pair in config)
                {
                    writer.WriteLine($"{pair.Key}={pair.Value}");
                }
                writer.Close();
            }
        }

        public class StoreAction
        {
            private Store store;
            public StoreAction(Store store)
            {
                this.store = store;
            }

            public void RemoveDataFromSelected(string data)
            {
                App.Mutate(store =>
                {
                    var sub = store.SelectedSubscriber.Mutate<Model.SubscriberModel>();

                    if (!sub.removedData.Contains(data))
                        sub.removedData.Add(data);

                });
            }

            public async Task RemoveSubscribers(params int[] ids)
            {
                await Model.SubscriberModel.RemoveSubscribers(ids);
                FetchTreeData();
            }

            public async Task AddSubscriber(int phone, string name, string description)
            {
                await Model.SubscriberModel.AddSubscriber(phone, name, description);
                FetchTreeData();
            }

            public void AddDataToSelected(string data)
            {
                App.Mutate((store) =>
                {
                    var sub = store.SelectedSubscriber.Mutate<Model.SubscriberModel>();

                    if (!sub.addedData.Contains(data))
                        sub.addedData.Add(data);


                });
            }

            public void SaveChanges()
            {
                App.Mutate(store =>
                {
                    var sub = store.SelectedSubscriber;

                    // Если выбран один абонент
                    if (!sub.IsMultiple)
                    {
                        if (sub.removedData.Count > 0)
                        {
                            var command = ATSDataSet.Command($"DELETE FROM [SubscriberData] WHERE [dataId] IN (SELECT [id] FROM [Data] WHERE [key] IN ({Database.ATSDataSet.QuoteValues(sub.removedData)}))");

                            command.ExecuteNonQuery();
                            Logging.Logger.Log($"Данные {string.Join(", ", sub.removedData)} удалены из абонента {sub.ViewItem.subscriber.phone}");
                            sub.removedData.Clear();
                        }

                        if (sub.addedData.Count > 0)
                        {
                            foreach (var data in sub.addedData)
                            {
                                var command = ATSDataSet.Command($@"
IF NOT EXISTS (SELECT [id] FROM [Data] WHERE [key]='{data}')
BEGIN
    INSERT INTO [Data] ([key]) VALUES ('{data}');
END;
INSERT INTO [SubscriberData] ([subscriberId], [dataId]) VALUES ('{sub.ViewItem.subscriber.subscriberId}', (SELECT [id] FROM [Data] WHERE [key]='{data}'));
");
                                var result = command.ExecuteNonQuery();
                            }
                            Logging.Logger.Log($"Данные {string.Join(", ", sub.addedData)} добавлены абоненту {sub.ViewItem.subscriber.phone}");
                            sub.addedData.Clear();
                            //FetchTreeData();
                        }
                    }

                    if (sub.changes.Count > 0)
                    {
                        var ids = sub.ViewItems.Select(i => i.subscriber.subscriberId);
                        //foreach (var item in sub.ViewItems)
                        //{
                        //int id = item.subscriber.subscriberId;


                        //int statusType = item.subscriber.statusType;
                        //string name = item.subscriber.subscriberName;
                        //int phone = item.subscriber.phone;
                        //string description = item.subscriber.description;
                        //bool isEmpty = string.IsNullOrWhiteSpace(item.subscriber.subscriberName);

                        //List<string> data = item.data.Select(d => d.key).ToList();

                        var updates = new List<string>();

                        foreach (var change in sub.changes)
                        {
                            switch (change.Key)
                            {
                                case "subscriberPhone":
                                    {
                                        int phone;
                                        if (!int.TryParse(change.Value, out phone))
                                        {
                                            MessageBox.Show("Некорректно задан номер телефона.");
                                            return;
                                        }
                                        updates.Add($"[phone]='{phone}'");
                                        break;
                                    }
                                case "subscriberDescription":
                                    updates.Add($"[description]='{change.Value}'");
                                    break;
                                case "subscriberName":
                                    updates.Add($"[name]='{change.Value}'");
                                    break;
                                case "statusType":
                                    updates.Add($"[statusType]='{change.Value}'");
                                    break;
                            }
                        }

                        if (sub.changes.ContainsKey("isEmpty"))
                        {
                            string isEmpty;
                            sub.changes.TryGetValue("isEmpty", out isEmpty);
                            if (isEmpty == "True")
                            {
                                updates.RemoveAll(s => s.StartsWith("[name]"));
                                updates.Add($"[name]=''");
                            }
                        }

                        if (updates.Count > 0)
                        {

                            //[name] = '{name}', [statusType] = '{statusType}', [phone] = '{phone}', [description] = '{description}'
                            var command = $"UPDATE [Subscriber] SET {string.Join(",", updates)} WHERE [id] IN ('{string.Join("','", ids)}')";
                            string subscriberPhones = string.Join(", ", sub.ViewItems.Select(i => i.subscriber.phone));

                            try
                            {
                                var updateSubscriberCommand = ATSDataSet.Command(command);
                                var result = updateSubscriberCommand.ExecuteNonQuery();
                                Logging.Logger.Log($"Изменения абонента {subscriberPhones} сохранены. Изменения: {string.Join(", ", sub.changes.Select(change => $"{change.Key} -> {change.Value}"))}");
                            }
                            catch (System.Data.SqlClient.SqlException ex)
                            {
                                if (ex.Number == 2627)
                                {
                                    MessageBox.Show("Невозможно изменить номер телефона: Он уже присутствует в базе данных.");
                                    return;
                                }

                                Logging.Logger.Log($"Ошибка сохранения изменений абонента {subscriberPhones}: {ex.Message}", Logging.Logger.MessageType.Error);
                                MessageBox.Show("Произошла ошибка при сохранении в базу данных.");
                                return;
                            }
                        }

                        sub.changes.Clear();
                        sub.removedData.Clear();
                        sub.addedData.Clear();
                        sub.Mutate();
                    }

                    FetchTreeData();
                    return;
                });
            }

            public async Task ClearDb()
            {
                var cmd = ATSDataSet.Command(@"
DELETE [SubscriberData];
DELETE[Profile];
DELETE[Subscriber];
DELETE[Data];
DELETE[Department];
                ");

                await cmd.ExecuteNonQueryAsync();
                Logging.Logger.Log("База данных была очищена", Logging.Logger.MessageType.Warning);
            }

            public void ChangeSelectedSubscriberMode(bool? mode = null)
            {
                App.ControlMutate((cache) =>
                {
                    var selectedSub = App.store.SelectedSubscriber.Mutate<Model.SubscriberModel>();

                    var nextMode = mode == null ? !selectedSub.IsEditMode : mode.Value;

                    if (!nextMode)
                    {
                        selectedSub.changes.Clear();
                        selectedSub.removedData.Clear();
                        selectedSub.addedData.Clear();
                    }

                    selectedSub.IsEditMode = nextMode;
                    return true;
                });
            }

            // TODO: Обновление данных не происходит при изменении абонентов

            public void FetchTreeData()
            {
                App.ControlMutate((store) =>
                {

                    //var departmentTableAdapter = new ATSManual.Database.ATSDataSetTableAdapters.DepartmentTableAdapter();
                    var subscriberTableAdapter = new ATSManual.Database.ATSDataSetTableAdapters.SubscriberDataTableAdapter();
                    var dataTableAdapter = new ATSManual.Database.ATSDataSetTableAdapters.DataTableAdapter();


                    //var departments = departmentTableAdapter.GetDepartments().ToList();
                    var subscribers = subscriberTableAdapter.GetSubscribers().ToList();

                    var data = dataTableAdapter
                        .GetAllData().ToList();

                    var dataDict = data.GroupBy(val => val.subscriberId).ToDictionary(val => val.Key);

                    var viewItems = new List<SubscribersManager.ViewItem>();
                    //foreach (var dep in departments)
                    //{
                    //    treeItems.Add(new Forms.ManualForm.TreeDepartment(dep));
                    //}
                    foreach (var sub in subscribers)
                    {
                        IGrouping<int, Database.ATSDataSet.DataRow> list;
                        var subData = dataDict.TryGetValue(sub.subscriberId, out list) ? list.ToList() : new List<Database.ATSDataSet.DataRow>();
                        viewItems.Add(new SubscribersManager.ViewItem(sub, subData));
                    }

                    //viewItems.Sort((Forms.TreeManager.TreeItem item1, Forms.TreeManager.TreeItem item2) =>
                    //{
                    //    var srt = new List<string>() { item1.name, item2.name };
                    //    srt.Sort();

                    //    return srt[0] == item2.name ? 1 : -1;
                    //});






                    viewItems.Sort((i1, i2) => i1.subscriber.phone > i2.subscriber.phone ? 1 : -1);

                    store.Tree.Mutate<Model.TreeModel>().items = viewItems;

                    var selectedItem = App.store.SelectedSubscriber.ViewItem;
                    if (selectedItem != null)
                    {
                        var itemIds = App.store.SelectedSubscriber.ViewItems.Select(i => i.subscriber.subscriberId);
                        var sameItems = App.store.Tree.items.Where(item => itemIds.Contains(item.subscriber.subscriberId));

                        App.store.SelectedSubscriber.Mutate<Model.SubscriberModel>().ViewItems = sameItems.ToArray();
                    }

                    return true;

                });
            }
        }

        public static bool isInit { get; private set; } = false;

        private static event EventHandler OnInvalidate = delegate { };

        public static AppStore store { get; private set; } = new AppStore();
        public static StoreAction action { get; private set; }

        public static void Init()
        {
            if (isInit) return;
            action = new StoreAction(store);

            isInit = true;
        }

        public delegate bool? ControllableCacheMutator(AppStore store);
        public delegate void CacheMutator(AppStore store);

        public static void ControlMutate(ControllableCacheMutator mutator, bool force = false)
        {
            if (!isInit) App.Init();
            var update = mutator(store);

            if (update.Value)
                store.Invalidate(force);
        }
        public static void Mutate(CacheMutator mutator, bool force = false)
        {
            if (!isInit) App.Init();
            mutator(store);

            store.Invalidate(force);
        }

        public static void Connect(ICacheConnect target, params CacheResource[] triggers)
        {
            if (!isInit) App.Init();

            store.OnChange += (o, e) =>
            {
                if (triggers.Length == 0 || e.Any(pair => triggers.Contains(pair)))
                {
                    target.CacheReset();
                    target.CacheUpdate(e.ToList());
                }
            };
        }
    }
}
