using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ATSManual.Database;
using ATSManual.Storing;


namespace ATSManual.Components.Subscriber
{
    public partial class SelectedSubscriber : UserControl, ICacheConnect
    {



        private void HandleChanges(string tag, string value)
        {
            App.ControlMutate((cache) =>
            {
                var sub = App.store.SelectedSubscriber.Mutate<Model.SubscriberModel>();

                if (cache.Invalidations > 0) return false;

                var changes = sub.changes;

                changes[tag] = value;

                return true;
            });
        }

        public SelectedSubscriber()
        {
            InitializeComponent();
            this.ForeachControl<LabelTextBox>(control => control.TextChanged += (o, e) => HandleChanges((string)control.Tag, control.contentTextBox.Text));

            statusComboBox.Items.AddRange(Model.SubscriberModel.GetStatuses().ToArray());
            statusComboBox.SelectedIndexChanged += (o, e) =>
            {
                if (statusComboBox.SelectedIndex != -1)
                    HandleChanges("statusType", statusComboBox.SelectedIndex.ToString());
            };


            App.Connect(this, App.store.SelectedSubscriber);
        }

        public string DrawChangedLabel(bool changed, string label)
        {
            if (changed)
            {
                if (label.Last() != '*')
                    label += "*";
            }
            else if (label.Last() == '*')
                label = label.Remove(label.Length - 1, 1);

            return label;
        }

        //private Forms.ManualForm.TreeSubscriber prevItem;


        private void UpdateInput(ref string input, ref string label, string tag, Model.SubscriberModel sub)
        {
            bool changed = sub.changes.ContainsKey(tag);
            label = DrawChangedLabel(changed, label);

            if (changed)
            {
                input = sub.changes[tag];
                return;
            }

            //if(sub.IsMultiple)

            switch (tag)
            {
                case "subscriberPhone":
                    if (sub.IsMultiple)
                        input = sub.ViewItems.All(item => item.subscriber.phone == sub.ViewItem.subscriber.phone) ? sub.ViewItem.subscriber.phone.ToString() : null;
                    else
                        input = sub.ViewItem.subscriber.phone.ToString();
                    break;
                case "subscriberDescription":
                    if (sub.IsMultiple)
                        input = sub.ViewItems.All(item => item.subscriber.description == sub.ViewItem.subscriber.description) ? sub.ViewItem.subscriber.description : null;
                    else
                        input = sub.ViewItem.subscriber.description;
                    break;
                case "subscriberName":
                    if (sub.IsMultiple)
                        input = sub.ViewItems.All(item => item.subscriber.subscriberName == sub.ViewItem.subscriber.subscriberName) ? sub.ViewItem.subscriber.subscriberName : null;
                    else
                        input = sub.ViewItem.subscriber.subscriberName;
                    break;
                case "statusType":
                    if (sub.IsMultiple)
                        input = sub.ViewItems.All(item => item.subscriber.statusType == sub.ViewItem.subscriber.statusType) ? sub.ViewItem.subscriber.statusType.ToString() : null;
                    else
                        input = sub.ViewItem.subscriber.statusType.ToString();
                    break;
                case "isEmpty":
                    {
                        var result = string.IsNullOrWhiteSpace(sub.ViewItem.subscriber.subscriberName) ? "True" : "False";
                        if (sub.IsMultiple)
                            input = sub.ViewItems.All(item => string.IsNullOrWhiteSpace(item.subscriber.subscriberName) == string.IsNullOrWhiteSpace(sub.ViewItem.subscriber.subscriberName)) ? result : null;
                        else
                            input = result;
                        break;
                    }
            }
        }

        public void CacheUpdate(List<CacheResource> resources)
        {
            var sub = App.store.SelectedSubscriber;

            if (sub.ViewItem != null)
            {

                this.ForeachControl<LabelTextBox>(control =>
                {
                    string text = control.contentTextBox.Text;
                    string label = control.LabelText;
                    UpdateInput(ref text, ref label, (string)control.Tag, sub);

                    control.contentTextBox.Text = text?.ToString() ?? "...";
                    control.LabelText = label;
                });

                dataLayout.Controls.Clear();
                defaultLabel.SendToBack();
                defaultLabel.Hide();


                //statusComboBox.SelectedIndex = sub.treeItem.subscriber.statusType;

                // Дропбокс статуса
                string comboIndex = statusComboBox.SelectedIndex.ToString();
                string comboLabel = statusComboBoxLabel.Text;

                UpdateInput(ref comboIndex, ref comboLabel, "statusType", sub);

                if (comboIndex != null)
                    statusComboBox.SelectedIndex = int.Parse(comboIndex);
                statusComboBoxLabel.Text = comboLabel;

                // Чекбокс пустого номера
                string isEmptyChecked = isEmptySubscriberCheckBox.Checked ? "True" : "False";
                string isEmptyLabel = isEmptySubscriberCheckBox.Text;

                UpdateInput(ref isEmptyChecked, ref isEmptyLabel, "isEmpty", sub);

                if (isEmptyChecked != null)
                    isEmptySubscriberCheckBox.Checked = isEmptyChecked == "True";
                else
                    isEmptySubscriberCheckBox.Checked = false;

                isEmptySubscriberCheckBox.Text = isEmptyLabel;

                if (isEmptySubscriberCheckBox.Checked)
                {
                    subscriberNameTextBox.contentTextBox.Text = "отсутствует";
                    subscriberNameTextBox.Enabled = false;
                }
                else subscriberNameTextBox.Enabled = true;



                if (!sub.IsMultiple)
                {
                    //dataLayout.Size = new Size(1000, 1000);
                    //dataLayout.Size = new Size(0, 0);

                    var dataList = sub.ViewItem.data.Select(d => d.key).Concat(sub.addedData);

                    foreach (var data in dataList)
                    {
                        if (sub.removedData.Contains(data)) continue;

                        var dataControl = new SubscriberData(data);
                        dataControl.Dock = DockStyle.Fill;
                        dataLayout.Controls.Add(dataControl);

                        dataControl.OnDelete += (o, e) =>
                        {
                            if (sub.addedData.Contains(data)) App.Mutate((store) => sub.Mutate<Model.SubscriberModel>().addedData.Remove(data));
                            else App.action.RemoveDataFromSelected(data);
                        };
                    }

                    dataLayout.ForeachControl<Button>((btn) => btn.Enabled = sub.IsEditMode);

                    // Бля надо отрефакторить ужасно дублируется
                    if (sub.addedData.Count > 0 || sub.removedData.Count > 0)
                    {
                        if (subscriberDataGroupBox.Text.Last() != '*')
                            subscriberDataGroupBox.Text += "*";
                    }
                    else if (subscriberDataGroupBox.Text.Last() == '*')
                        subscriberDataGroupBox.Text = subscriberDataGroupBox.Text.Remove(subscriberDataGroupBox.Text.Length - 1, 1);

                }
                //else
                //{
                //subscriberDataGroupBox.Enabled = false;
                //dataAddButton.Enabled = false;
                //}

                subscriberPanel.ForeachControl<TextBoxBase>((control) => control.ReadOnly = !sub.IsEditMode);
                statusComboBox.Enabled = sub.IsEditMode;
                // Костыли костыли вот и радость моя
                dataLayout.ForeachControl<TextBoxBase>((control) => control.ReadOnly = true);
                //subscriberPhoneTextBox.ReadOnly = true;

                subscriberDataGroupBox.Enabled = !sub.IsMultiple;
                isEmptySubscriberCheckBox.Enabled = sub.IsEditMode;
                dataAddButton.Enabled = !sub.IsMultiple && sub.IsEditMode;
                saveChangesButton.Enabled = sub.IsEditMode && sub.HasChanges;
                editModeButton.Text = sub.IsEditMode ? "Отмена" : "Редактировать";


            }
            else
            {
                defaultLabel.BringToFront();
                defaultLabel.Show();
            }



            //dataLayout.Invalidate();

            //dataLayout.PerformLayout();

            //prevItem = sub.treeItem;
            phoneTextBox.ReadOnly = true;
        }

        public void CacheReset()
        {

            // subscriberPanel.ForeachControl<TextBoxBase>((control) => control.ReadOnly = true);
        }

        private void SelectedSubscriber_Load(object sender, EventArgs e)
        {


        }

        private void editModeButton_Click(object sender, EventArgs e)
        {
            App.action.ChangeSelectedSubscriberMode();
        }

        private void saveChangesButton_Click(object sender, EventArgs e)
        {
            var sub = App.store.SelectedSubscriber;
            if (sub.IsMultiple)
            {
                var result = MessageBox.Show($"Вы уверены что хотите применить изменения к {sub.ViewItems.Length} абонентам?", "Внимание", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result != DialogResult.Yes) return;
            }
            App.action.SaveChanges();
        }

        private void dataAddButton_Click(object sender, EventArgs e)
        {
            var dialog = new Forms.DataSelectDialog();
            var result = dialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                App.action.AddDataToSelected(dialog.SelectedData);
            }
        }

        private void isEmptySubscriberCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            HandleChanges("isEmpty", isEmptySubscriberCheckBox.Checked.ToString());
        }
    }
}
