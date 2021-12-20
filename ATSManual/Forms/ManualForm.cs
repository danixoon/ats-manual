using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Data;
using System.IO;
using System.Xml.Linq;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ATSManual.Database;
using ATSManual.Storing;

namespace ATSManual.Forms
{
    public partial class ManualForm : Form, ICacheConnect
    {


        private SubscribersManager subManager;


        public ManualForm()
        {
            InitializeComponent();


        }

        public void EnsureEgg()
        {
            if (manualMenuStrip.Items.Count > 3) return;

            var item = new ToolStripMenuItem() { Text = "Спасибо" };


            item.Click += (object o, EventArgs e) =>
            {
                MessageBox.Show("Здарова! Это Бомба, когда-то был тут и написал эту чудную утилиту. Сегодня у меня м");
                if (!App.config.ContainsKey("EGG")) manualMenuStrip.Items.Remove(item);

            };

            manualMenuStrip.Items.Add(item);
        }

        private async void exportMenuItem_Click(object sender, EventArgs e)
        {
            //var dialog = new SaveFileDialog() { Filter = "XML-документ|*.xml" };
            //var dialogResult = dialog.ShowDialog();
            //if (dialogResult == DialogResult.OK)
            //{
            //    Import.Importer.Export(dialog.FileName);
            //}



            var dialog = new SaveFileDialog() { Filter = "Книга Excel|*.xlsx", FileName = $"Кросс - {DateTime.Now.ToShortDateString()}" };
            var dialogResult = dialog.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                var fileName = dialog.FileName.Split('\\').Last();

                await App.store.App.RunTask("export", $"Экспортирование {fileName}", Task.Run(() =>
                {
                    //var file = dialog.FileName.Remove(dialog.FileName.Length - fileName.Length, fileName.Length);
                    Import.Importer.ExportExcelFile(dialog.FileName);
                }));
            }
        }

        public static Model.SubscriberStatus ConvertColor(Color color)
        {
            if (color == Color.FromArgb(192, 80, 77))
                return Model.SubscriberStatus.Invalid;
            if (color == Color.FromArgb(83, 141, 213))
                return Model.SubscriberStatus.Ok;
            if (color == Color.FromArgb(146, 208, 80))
                return Model.SubscriberStatus.Disabled;
            if (color == Color.FromArgb(112, 48, 160))
                return Model.SubscriberStatus.Unknown;

            return Model.SubscriberStatus.Checking;
        }

        public static Color? ConvertStatus(Model.SubscriberStatus status)
        {
            switch (status)
            {
                case Model.SubscriberStatus.Ok:
                    return Color.FromArgb(83, 141, 213);
                case Model.SubscriberStatus.Invalid:
                    return Color.FromArgb(192, 80, 77);
                case Model.SubscriberStatus.Disabled:
                    return Color.FromArgb(146, 208, 80);
                case Model.SubscriberStatus.Unknown:
                    return Color.FromArgb(112, 48, 160);
            }

            return null;
        }

        private async void importFileToolStrip_Click(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog() { Filter = "Книга Excel|*.xlsx" };
            var dialogResult = dialog.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                var rows = await App.store.App.RunTask("export", $"Импортирование {dialog.FileName.Split('\\').Last()}", Task.Run(() =>
                {
                    return Import.Importer.ImportExcelFile(dialog.FileName);
                }));


                //var importedData = Import.Importer.ImportRows(dialog.FileName, 4, (rows) =>
                //{
                //    var matchers = new string[] { "\\d{4}", null, "(\\d{4})|(\\d{6})|(^$)", null };
                //    for (int i = 0; i < 4; i++)
                //    {
                //        if (i == 2 && rows[i] == null) continue;
                //        if (matchers[i] != null && !Regex.IsMatch(rows[i], matchers[i])) return false;
                //    }
                //    return true;
                //});

                var form = new ImportPreview(rows.Select(data => new ImportData(data.row, data.status)).ToList());
                form.ShowDialog();
            }
        }


        private List<SubscribersManager.ViewItem> itemHistory = new List<SubscribersManager.ViewItem>();

        public void HandleSelectSubscriber(bool addToHistory = true, params SubscribersManager.ViewItem[] selectedItems)
        {
            //if (item == null) return;
            if (selectedItems.Length > 1)
                addToHistory = false;

            App.ControlMutate(store =>
            {
                var sub = store.SelectedSubscriber;
                if (App.store.Invalidations > 1) return false;
                if (sub.HasChanges)
                {
                    sub.Mutate();
                    var result = MessageBox.Show("Изменения не сохранены, продолжить?", "Внимание", MessageBoxButtons.OKCancel);
                    if (result != DialogResult.OK) return true;
                }

                sub.Mutate<Model.SubscriberModel>().ViewItems = selectedItems;
                App.action.ChangeSelectedSubscriberMode(false);


                if (addToHistory && itemHistory.FirstOrDefault()?.subscriber.phone != selectedItems[0].subscriber.phone)
                {
                    if (itemHistory.Count == (phoneHistory.ColumnCount + 1) / 2) itemHistory.RemoveAt(itemHistory.Count - 1);
                    itemHistory.Insert(0, selectedItems[0]);
                }

                DrawPhoneHistory();
                return true;
            });
        }

        private void DrawPhoneHistory()
        {
            var sub = App.store.SelectedSubscriber;
            phoneHistory.Controls.Clear();
            for (int i = itemHistory.Count - 1; i >= 0; i--)
            {
                if (phoneHistory.Controls.Count % 2 != 0)
                    phoneHistory.Controls.Add(new Label() { Text = " / ", Dock = DockStyle.Fill, AutoSize = false, TextAlign = ContentAlignment.MiddleCenter });


                var label = new LinkLabel() { Tag = itemHistory[i].subscriber.phone, Text = itemHistory[i].subscriber.phone.ToString(), Dock = DockStyle.Fill, AutoSize = false, TextAlign = ContentAlignment.MiddleCenter };
                phoneHistory.Controls.Add(label);

                label.Click += (o, e) =>
                {
                    var target = App.store.Tree.items.Find(item => item.subscriber.phone == (int)label.Tag);
                    if (target == null)
                        MessageBox.Show("Абонент удалён");
                    else
                        HandleSelectSubscriber(false, target);
                };

                if (!sub.IsMultiple && sub.ViewItem?.subscriber.phone == itemHistory[i].subscriber.phone)
                {
                    label.BackColor = SystemColors.Highlight;
                    label.LinkColor = SystemColors.HighlightText;
                }
            }
        }


        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Lol().Show();
        }

        public void CacheUpdate(List<CacheResource> resources)
        {
            var app = App.store.App;
            var tree = App.store.Tree;
            var filter = App.store.TreeFilter;
            var subscriber = App.store.SelectedSubscriber;


            if (app.isDirty)
            {
                exportMenuItem.Enabled = !app.IsTaskExecuting("export");
                importFileToolStrip.Enabled = !app.IsTaskExecuting("import");

                if (app.tasks.Count > 0)
                {
                    var task = app.tasks.FirstOrDefault();
                    progressBar.Visible = true;
                    progressBarLabel.Text = task.Value.label;
                }
                else
                {
                    progressBar.Visible = false;
                    progressBarLabel.Text = "";
                }
            }


            if (filter.isDirty || tree.isDirty)
            {

                var nodes = tree.items.Where((item) =>
                {
                    List<bool> conds = new List<bool>();
                    var name = item.ToString();

                    if (filter.Subscriber.Length > 0)
                    {
                        if (filter.Subscriber.StartsWith("#"))
                            conds.Add(item.subscriber.subscriberId.ToString().StartsWith(filter.Subscriber.Replace("#", "")));
                        else
                        {
                            var cond = string.Join("", filter.Subscriber.Select(c => $"[{c}]")).Replace("[*]", ".").Replace("\\", "\\\\");
                            conds.Add(Regex.IsMatch(item.ToString(), cond));
                        }

                    }
                    if (filter.WithoutData)
                    {
                        conds.Add(item.data.Count() == 0);
                    }
                    else if (filter.Data.Length > 0)
                    {
                        var isData = item.data.Any(el =>
                        {
                            //if(filter.Data.)
                            var cond = string.Join("", filter.Data.Select(c => $"[{c}]")).Replace("[*]", ".").Replace("\\", "\\\\");
                            return Regex.IsMatch(el.key.ToString(), cond);

                            //return .StartsWith(filter.Data);
                        });



                        conds.Add(isData);
                    }
                    else if (filter.Data.Length > 0) conds.Add(false);
                    if (filter.StatusFilters.Count() > 0)
                    {
                        conds.Add(filter.StatusFilters.Contains(item.subscriber.statusType));
                    }

                    if (isEmptySubscriberFilterCheckBox.Checked)
                    {
                        conds.Add(string.IsNullOrWhiteSpace(item.subscriber.subscriberName));
                    }


                    return conds.Count > 0 && conds.All(el => el) || conds.Count == 0;
                });


                var filteredNodes = nodes.ToList();

                subManager.FillView(filteredNodes);

                if (filteredNodes.Count == 1)
                {
                    var sub = nodes.First();
                    HandleSelectSubscriber(true, sub);
                }

                bool isFiltering = filter.StatusFilters.Count() != 0 || filter.Data.Length != 0 || filter.Subscriber.Length != 0 || filter.WithoutData;

                subscriberMapGroupBox.Text = $"Карта абонентов: {filteredNodes.Count}";
            }

            if (subscriber.isDirty)
            {
                if (subscriber.IsMultiple)
                    selectedGroupBox.Text = $"Несколько абонентов ({subscriber.ViewItems.Length})";
                else if (subscriber.ViewItem != null)
                    selectedGroupBox.Text = $"Абонент #{subscriber.ViewItem.subscriber.subscriberId}";
                else
                    selectedGroupBox.Text = $"Абонент";


                removeSubscriberButton.Enabled = subscriber.ViewItem != null;
            }

            if (subscriber.ViewItem != null) subManager.SelectItems(subscriber.ViewItems);
            //if (App.store.Invalidations > 0)
            subManager.ScrollToSelected();
        }

        public void CacheReset()
        {
            var filter = App.store.TreeFilter;


            filterData.Enabled = !filter.WithoutData;
            withoutDataFilterCheckbox.Checked = filter.WithoutData;
        }

        private void ConnectToDatabase(bool newConnection)
        {
            var database = new DatabasePromt(newConnection);
            var result = database.ShowDialog();
            if (result != DialogResult.OK)
            {
                if (newConnection)
                    return;

                Close();
                return;
            }


            Properties.Settings.Default["ATSManualDBConnectionString"] = database.connectionString;

            Text = $"База данных АТС - {database.DatabaseName}";
            App.action.FetchTreeData();
        }

        private struct FilterStatusItem
        {
            public Model.SubscriberStatus Status { get; private set; }
            public string Name { get; private set; }
            public FilterStatusItem(string name, Model.SubscriberStatus status)
            {
                this.Status = status;
                this.Name = name;
            }
        }

        private void ManualForm_Load(object sender, EventArgs e)
        {

            if (App.IsProd)
            {
                ConnectToDatabase(false);

            }
            else
            {
                var matches = Regex.Match(Properties.Settings.Default["ATSManualDBConnectionString"].ToString(), "Initial Catalog=(.+?);");
                Text = $"База данных АТС - {matches.Groups[1].ToString()}";
            }



            this.subManager = new SubscribersManager(subView, (item) =>
            {

                if (item == null) return;
                var color = ConvertStatus((Model.SubscriberStatus)item.subscriber.statusType);
                if (color != null)
                {
                    var cell = item.Item.Cells[0];
                    cell.Style.BackColor = color.Value;
                    float contrast = Import.Importer.Contrast(color.Value, Color.IndianRed);

                    if (contrast < 3f)
                        cell.Style.ForeColor = Color.White;
                }
            });

            foreach (var status in Model.SubscriberModel.GetStatuses())
            {
                statusCheckedListBox.Items.Add(status);
            }


            App.Init();

            App.Connect(this, App.store.TreeFilter, App.store.Tree, App.store.SelectedSubscriber, App.store.App);



            App.action.FetchTreeData();

            if (App.IsProd)
                Program.StartBackup();
            Program.StartEgg();
        }

        private void addSubscriberButton_Click(object sender, EventArgs e)
        {
            new AddSubscriberDialog().ShowDialog();
        }

        private async void removeSubscriberButton_Click(object sender, EventArgs e)
        {
            var sub = App.store.SelectedSubscriber.ViewItems;
            var subName = string.Join(", ", sub.Select(s => s.subscriber.phone));

            var result = MessageBox.Show($"Вы уверены, что хотите удалить абонента {subName}?", "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                try
                {
                    this.Enabled = false;
                    await App.action.RemoveSubscribers(sub.Select(s => s.subscriber.subscriberId).ToArray());
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при удалении: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                this.Enabled = true;
            }
        }

        private async void clearDatabaseMenuItem_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show($"Вы уверены, что хотите очистить базу данных?", "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes) return;

            result = MessageBox.Show($"ТОЧНО? БЕКАП НА МЕСТЕ?", "!!!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes) return;

            await App.action.ClearDb();
            App.action.FetchTreeData();
        }



        private void withoutDataFilterCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            App.Mutate((store) => store.TreeFilter.Mutate<Model.TreeFilterModel>().WithoutData = withoutDataFilterCheckbox.Checked);
        }

        private void filterSubscriber_TextChanged(object sender, EventArgs e)
        {
            App.Mutate((store) => store.TreeFilter.Mutate<Model.TreeFilterModel>().Subscriber = filterSubscriber.contentTextBox.Text);
        }

        private void filterData_TextChanged(object sender, EventArgs e)
        {
            App.Mutate((store) => store.TreeFilter.Mutate<Model.TreeFilterModel>().Data = filterData.contentTextBox.Text);
        }

        private void statusCheckedListBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        private void ManualForm_Resize(object sender, EventArgs e)
        {

        }

        private void notify_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ShowFromTray();
        }

        public void ShowFromTray()
        {
            Show();
            this.WindowState = FormWindowState.Normal;
            this.TopMost = true;
            this.Focus();
            this.TopMost = false;
            notifyIcon.Visible = false;

        }

        public void HideInTray()
        {
            Hide();
            notifyIcon.Visible = true;
        }

        private void testToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            using (var str = new FileStream("./icon.ico", FileMode.Create))
            {
                Icon.ExtractAssociatedIcon("C:\\Users\\Администратор\\Desktop\\dev\\eventvwr.exe").Save(str);
            }

        }

        private void openNotifyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowFromTray();
        }

        private void exitNotifyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void statusCheckedListBox_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            App.Mutate((store) =>
            {
                var filters = store.TreeFilter.Mutate<Model.TreeFilterModel>().StatusFilters.AsEnumerable();
                if (e.NewValue == CheckState.Unchecked)
                    filters = filters.Where(f => f != e.Index);
                else
                {
                    var list = filters.ToList();
                    list.Add(e.Index);
                    filters = list;
                }

                store.TreeFilter.StatusFilters = filters;
            });
        }

        private void statusCheckedListBox_Click(object sender, EventArgs e)
        {

        }

        private void connectToDatabaseToolStripMenu_Click(object sender, EventArgs e)
        {
            ConnectToDatabase(true);
        }

        private void isEmptySubscriberFilterCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            App.Mutate((store) => store.TreeFilter.Mutate<Model.TreeFilterModel>().WithoutSubscriber = isEmptySubscriberFilterCheckBox.Checked);
        }

        private void subscriberListView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            HandleSelectSubscriber(true, e.Item.Tag as SubscribersManager.ViewItem);
        }

        private void subView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void subView_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void ManualForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (App.IsProd && e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                HideInTray();
            }

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void openHistoryButton_Click(object sender, EventArgs e)
        {
            new SubscriberHistory().Show();
        }

        private void subView_SelectionChanged(object sender, EventArgs e)
        {
            if (App.store.Invalidations > 0)
            {

                return;
            }
            if (subView.SelectedRows.Count == 0) return;

            var tags = new List<SubscribersManager.ViewItem>();
            foreach (DataGridViewRow row in subView.SelectedRows)
                tags.Add(row.Tag as SubscribersManager.ViewItem);

            HandleSelectSubscriber(true, tags.ToArray());
        }
    }
}

