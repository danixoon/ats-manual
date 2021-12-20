using ATSManual.Database;

namespace ATSManual.Forms
{
    partial class ManualForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ManualForm));
            this.manualMenuStrip = new System.Windows.Forms.MenuStrip();
            this.файлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importFileToolStrip = new System.Windows.Forms.ToolStripMenuItem();
            this.exportMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.controlStripMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.connectToDatabaseToolStripMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.clearDatabaseMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.configToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.subscriberMapGroupBox = new System.Windows.Forms.GroupBox();
            this.openHistoryButton = new System.Windows.Forms.Button();
            this.subView = new System.Windows.Forms.DataGridView();
            this.phoneColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.subscriber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.removeSubscriberButton = new System.Windows.Forms.Button();
            this.addSubscriberButton = new System.Windows.Forms.Button();
            this.phoneHistory = new System.Windows.Forms.TableLayoutPanel();
            this.selectedGroupBox = new System.Windows.Forms.GroupBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.filterFlowLayout = new System.Windows.Forms.FlowLayoutPanel();
            this.withoutDataFilterCheckbox = new System.Windows.Forms.CheckBox();
            this.isEmptySubscriberFilterCheckBox = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.statusCheckedListBox = new System.Windows.Forms.CheckedListBox();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.notifyMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.openNotifyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitNotifyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.progressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.progressBarLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.subscriberTableAdapter = new ATSManual.Database.ATSDataSetTableAdapters.SubscriberDataTableAdapter();
            this.departmentTableAdapter = new ATSManual.Database.ATSDataSetTableAdapters.DepartmentTableAdapter();
            this.dataTableAdapter = new ATSManual.Database.ATSDataSetTableAdapters.DataTableAdapter();
            this.selectedSubscriber = new ATSManual.Components.Subscriber.SelectedSubscriber();
            this.filterSubscriber = new ATSManual.LabelTextBox();
            this.filterData = new ATSManual.LabelTextBox();
            this.manualMenuStrip.SuspendLayout();
            this.subscriberMapGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.subView)).BeginInit();
            this.selectedGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.filterFlowLayout.SuspendLayout();
            this.notifyMenuStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // manualMenuStrip
            // 
            this.manualMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.файлToolStripMenuItem,
            this.controlStripMenu,
            this.configToolStripMenuItem});
            this.manualMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.manualMenuStrip.Name = "manualMenuStrip";
            this.manualMenuStrip.Size = new System.Drawing.Size(916, 24);
            this.manualMenuStrip.TabIndex = 1;
            this.manualMenuStrip.Text = "menuStrip1";
            // 
            // файлToolStripMenuItem
            // 
            this.файлToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.importFileToolStrip,
            this.exportMenuItem});
            this.файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            this.файлToolStripMenuItem.Size = new System.Drawing.Size(45, 20);
            this.файлToolStripMenuItem.Text = "Файл";
            // 
            // importFileToolStrip
            // 
            this.importFileToolStrip.Name = "importFileToolStrip";
            this.importFileToolStrip.Size = new System.Drawing.Size(116, 22);
            this.importFileToolStrip.Text = "Импорт";
            this.importFileToolStrip.Click += new System.EventHandler(this.importFileToolStrip_Click);
            // 
            // exportMenuItem
            // 
            this.exportMenuItem.Name = "exportMenuItem";
            this.exportMenuItem.Size = new System.Drawing.Size(116, 22);
            this.exportMenuItem.Text = "Экспорт";
            this.exportMenuItem.Click += new System.EventHandler(this.exportMenuItem_Click);
            // 
            // controlStripMenu
            // 
            this.controlStripMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.connectToDatabaseToolStripMenu,
            this.clearDatabaseMenuItem});
            this.controlStripMenu.Name = "controlStripMenu";
            this.controlStripMenu.Size = new System.Drawing.Size(84, 20);
            this.controlStripMenu.Text = "База данных";
            // 
            // connectToDatabaseToolStripMenu
            // 
            this.connectToDatabaseToolStripMenu.Name = "connectToDatabaseToolStripMenu";
            this.connectToDatabaseToolStripMenu.Size = new System.Drawing.Size(150, 22);
            this.connectToDatabaseToolStripMenu.Text = "Подключиться";
            this.connectToDatabaseToolStripMenu.Click += new System.EventHandler(this.connectToDatabaseToolStripMenu_Click);
            // 
            // clearDatabaseMenuItem
            // 
            this.clearDatabaseMenuItem.Enabled = false;
            this.clearDatabaseMenuItem.Name = "clearDatabaseMenuItem";
            this.clearDatabaseMenuItem.Size = new System.Drawing.Size(150, 22);
            this.clearDatabaseMenuItem.Text = "Очистить";
            this.clearDatabaseMenuItem.Click += new System.EventHandler(this.clearDatabaseMenuItem_Click);
            // 
            // configToolStripMenuItem
            // 
            this.configToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.configToolStripMenuItem.Name = "configToolStripMenuItem";
            this.configToolStripMenuItem.Size = new System.Drawing.Size(80, 20);
            this.configToolStripMenuItem.Text = "Управление";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.exitToolStripMenuItem.Text = "Выход";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // subscriberMapGroupBox
            // 
            this.subscriberMapGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.subscriberMapGroupBox.Controls.Add(this.openHistoryButton);
            this.subscriberMapGroupBox.Controls.Add(this.subView);
            this.subscriberMapGroupBox.Location = new System.Drawing.Point(0, 3);
            this.subscriberMapGroupBox.Name = "subscriberMapGroupBox";
            this.subscriberMapGroupBox.Size = new System.Drawing.Size(512, 418);
            this.subscriberMapGroupBox.TabIndex = 2;
            this.subscriberMapGroupBox.TabStop = false;
            this.subscriberMapGroupBox.Text = "Абоненты";
            // 
            // openHistoryButton
            // 
            this.openHistoryButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.openHistoryButton.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.openHistoryButton.Enabled = false;
            this.openHistoryButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.openHistoryButton.Location = new System.Drawing.Point(491, 0);
            this.openHistoryButton.Name = "openHistoryButton";
            this.openHistoryButton.Size = new System.Drawing.Size(16, 12);
            this.openHistoryButton.TabIndex = 4;
            this.openHistoryButton.UseVisualStyleBackColor = false;
            this.openHistoryButton.Click += new System.EventHandler(this.openHistoryButton_Click);
            // 
            // subView
            // 
            this.subView.AllowUserToAddRows = false;
            this.subView.AllowUserToDeleteRows = false;
            this.subView.AllowUserToResizeRows = false;
            this.subView.BackgroundColor = System.Drawing.SystemColors.Window;
            this.subView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.subView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.phoneColumn,
            this.subscriber});
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.subView.DefaultCellStyle = dataGridViewCellStyle1;
            this.subView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.subView.GridColor = System.Drawing.SystemColors.ActiveBorder;
            this.subView.Location = new System.Drawing.Point(3, 16);
            this.subView.Name = "subView";
            this.subView.ReadOnly = true;
            this.subView.RowHeadersVisible = false;
            this.subView.RowTemplate.Height = 16;
            this.subView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.subView.Size = new System.Drawing.Size(506, 399);
            this.subView.TabIndex = 3;
            this.subView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.subView_CellClick);
            this.subView.SelectionChanged += new System.EventHandler(this.subView_SelectionChanged);
            // 
            // phoneColumn
            // 
            this.phoneColumn.HeaderText = "Номер";
            this.phoneColumn.Name = "phoneColumn";
            this.phoneColumn.ReadOnly = true;
            // 
            // subscriber
            // 
            this.subscriber.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.subscriber.HeaderText = "Абонент";
            this.subscriber.Name = "subscriber";
            this.subscriber.ReadOnly = true;
            // 
            // removeSubscriberButton
            // 
            this.removeSubscriberButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.removeSubscriberButton.Location = new System.Drawing.Point(437, 427);
            this.removeSubscriberButton.Name = "removeSubscriberButton";
            this.removeSubscriberButton.Size = new System.Drawing.Size(75, 20);
            this.removeSubscriberButton.TabIndex = 2;
            this.removeSubscriberButton.Text = "Удалить";
            this.removeSubscriberButton.UseVisualStyleBackColor = true;
            this.removeSubscriberButton.Click += new System.EventHandler(this.removeSubscriberButton_Click);
            // 
            // addSubscriberButton
            // 
            this.addSubscriberButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.addSubscriberButton.Location = new System.Drawing.Point(3, 427);
            this.addSubscriberButton.Name = "addSubscriberButton";
            this.addSubscriberButton.Size = new System.Drawing.Size(428, 20);
            this.addSubscriberButton.TabIndex = 1;
            this.addSubscriberButton.Text = "Добавить абонента";
            this.addSubscriberButton.UseVisualStyleBackColor = true;
            this.addSubscriberButton.Click += new System.EventHandler(this.addSubscriberButton_Click);
            // 
            // phoneHistory
            // 
            this.phoneHistory.ColumnCount = 9;
            this.phoneHistory.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.phoneHistory.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.phoneHistory.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.phoneHistory.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.phoneHistory.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.phoneHistory.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.phoneHistory.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.phoneHistory.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.phoneHistory.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.phoneHistory.Location = new System.Drawing.Point(130, 0);
            this.phoneHistory.Name = "phoneHistory";
            this.phoneHistory.RowCount = 1;
            this.phoneHistory.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.phoneHistory.Size = new System.Drawing.Size(335, 15);
            this.phoneHistory.TabIndex = 4;
            // 
            // selectedGroupBox
            // 
            this.selectedGroupBox.Controls.Add(this.selectedSubscriber);
            this.selectedGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.selectedGroupBox.Location = new System.Drawing.Point(0, 0);
            this.selectedGroupBox.Name = "selectedGroupBox";
            this.selectedGroupBox.Size = new System.Drawing.Size(188, 450);
            this.selectedGroupBox.TabIndex = 3;
            this.selectedGroupBox.TabStop = false;
            this.selectedGroupBox.Text = "Выбранное";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(198, 3);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.phoneHistory);
            this.splitContainer1.Panel1.Controls.Add(this.removeSubscriberButton);
            this.splitContainer1.Panel1.Controls.Add(this.subscriberMapGroupBox);
            this.splitContainer1.Panel1.Controls.Add(this.addSubscriberButton);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.selectedGroupBox);
            this.splitContainer1.Size = new System.Drawing.Size(707, 450);
            this.splitContainer1.SplitterDistance = 515;
            this.splitContainer1.TabIndex = 4;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox3.Controls.Add(this.filterFlowLayout);
            this.groupBox3.Location = new System.Drawing.Point(6, 6);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(186, 447);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Фильтрация";
            // 
            // filterFlowLayout
            // 
            this.filterFlowLayout.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.filterFlowLayout.Controls.Add(this.filterSubscriber);
            this.filterFlowLayout.Controls.Add(this.filterData);
            this.filterFlowLayout.Controls.Add(this.withoutDataFilterCheckbox);
            this.filterFlowLayout.Controls.Add(this.isEmptySubscriberFilterCheckBox);
            this.filterFlowLayout.Controls.Add(this.label1);
            this.filterFlowLayout.Controls.Add(this.statusCheckedListBox);
            this.filterFlowLayout.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.filterFlowLayout.Location = new System.Drawing.Point(6, 16);
            this.filterFlowLayout.Name = "filterFlowLayout";
            this.filterFlowLayout.Size = new System.Drawing.Size(174, 428);
            this.filterFlowLayout.TabIndex = 2;
            this.filterFlowLayout.WrapContents = false;
            // 
            // withoutDataFilterCheckbox
            // 
            this.withoutDataFilterCheckbox.AutoSize = true;
            this.withoutDataFilterCheckbox.Location = new System.Drawing.Point(10, 95);
            this.withoutDataFilterCheckbox.Margin = new System.Windows.Forms.Padding(10, 3, 3, 3);
            this.withoutDataFilterCheckbox.Name = "withoutDataFilterCheckbox";
            this.withoutDataFilterCheckbox.Size = new System.Drawing.Size(85, 17);
            this.withoutDataFilterCheckbox.TabIndex = 6;
            this.withoutDataFilterCheckbox.Text = "Без данных";
            this.withoutDataFilterCheckbox.UseVisualStyleBackColor = true;
            this.withoutDataFilterCheckbox.CheckedChanged += new System.EventHandler(this.withoutDataFilterCheckbox_CheckedChanged);
            // 
            // isEmptySubscriberFilterCheckBox
            // 
            this.isEmptySubscriberFilterCheckBox.AutoSize = true;
            this.isEmptySubscriberFilterCheckBox.Location = new System.Drawing.Point(10, 118);
            this.isEmptySubscriberFilterCheckBox.Margin = new System.Windows.Forms.Padding(10, 3, 3, 3);
            this.isEmptySubscriberFilterCheckBox.Name = "isEmptySubscriberFilterCheckBox";
            this.isEmptySubscriberFilterCheckBox.Size = new System.Drawing.Size(97, 17);
            this.isEmptySubscriberFilterCheckBox.TabIndex = 10;
            this.isEmptySubscriberFilterCheckBox.Text = "Пустой номер";
            this.isEmptySubscriberFilterCheckBox.UseVisualStyleBackColor = true;
            this.isEmptySubscriberFilterCheckBox.CheckedChanged += new System.EventHandler(this.isEmptySubscriberFilterCheckBox_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 143);
            this.label1.Margin = new System.Windows.Forms.Padding(10, 5, 3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Состояние";
            // 
            // statusCheckedListBox
            // 
            this.statusCheckedListBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.statusCheckedListBox.CheckOnClick = true;
            this.statusCheckedListBox.FormattingEnabled = true;
            this.statusCheckedListBox.Location = new System.Drawing.Point(10, 159);
            this.statusCheckedListBox.Margin = new System.Windows.Forms.Padding(10, 3, 3, 3);
            this.statusCheckedListBox.Name = "statusCheckedListBox";
            this.statusCheckedListBox.Size = new System.Drawing.Size(160, 109);
            this.statusCheckedListBox.TabIndex = 9;
            this.statusCheckedListBox.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.statusCheckedListBox_ItemCheck);
            this.statusCheckedListBox.Click += new System.EventHandler(this.statusCheckedListBox_Click);
            this.statusCheckedListBox.SelectedIndexChanged += new System.EventHandler(this.statusCheckedListBox_SelectedIndexChanged);
            // 
            // notifyIcon
            // 
            this.notifyIcon.ContextMenuStrip = this.notifyMenuStrip;
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "База данных кросса";
            this.notifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notify_MouseDoubleClick);
            // 
            // notifyMenuStrip
            // 
            this.notifyMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openNotifyToolStripMenuItem,
            this.exitNotifyToolStripMenuItem});
            this.notifyMenuStrip.Name = "notifyMenuStrip";
            this.notifyMenuStrip.Size = new System.Drawing.Size(121, 48);
            // 
            // openNotifyToolStripMenuItem
            // 
            this.openNotifyToolStripMenuItem.Name = "openNotifyToolStripMenuItem";
            this.openNotifyToolStripMenuItem.Size = new System.Drawing.Size(120, 22);
            this.openNotifyToolStripMenuItem.Text = "Открыть";
            this.openNotifyToolStripMenuItem.Click += new System.EventHandler(this.openNotifyToolStripMenuItem_Click);
            // 
            // exitNotifyToolStripMenuItem
            // 
            this.exitNotifyToolStripMenuItem.Name = "exitNotifyToolStripMenuItem";
            this.exitNotifyToolStripMenuItem.Size = new System.Drawing.Size(120, 22);
            this.exitNotifyToolStripMenuItem.Text = "Выход";
            this.exitNotifyToolStripMenuItem.Click += new System.EventHandler(this.exitNotifyToolStripMenuItem_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.progressBar,
            this.toolStripStatusLabel1,
            this.progressBarLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 512);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.statusStrip.Size = new System.Drawing.Size(916, 22);
            this.statusStrip.TabIndex = 6;
            this.statusStrip.Text = "statusStrip1";
            // 
            // progressBar
            // 
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(100, 16);
            this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBar.Value = 100;
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.None;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
            this.toolStripStatusLabel1.Text = "ывавы";
            // 
            // progressBarLabel
            // 
            this.progressBarLabel.Name = "progressBarLabel";
            this.progressBarLabel.Size = new System.Drawing.Size(90, 17);
            this.progressBarLabel.Text = "progressBarLabel";
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(0, 27);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(916, 482);
            this.tabControl1.TabIndex = 7;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.splitContainer1);
            this.tabPage1.Controls.Add(this.groupBox3);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(908, 456);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Абоненты";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(908, 456);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Владельцы";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // subscriberTableAdapter
            // 
            this.subscriberTableAdapter.ClearBeforeFill = true;
            // 
            // departmentTableAdapter
            // 
            this.departmentTableAdapter.ClearBeforeFill = true;
            // 
            // dataTableAdapter
            // 
            this.dataTableAdapter.ClearBeforeFill = true;
            // 
            // selectedSubscriber
            // 
            this.selectedSubscriber.Dock = System.Windows.Forms.DockStyle.Fill;
            this.selectedSubscriber.Location = new System.Drawing.Point(3, 16);
            this.selectedSubscriber.Name = "selectedSubscriber";
            this.selectedSubscriber.Size = new System.Drawing.Size(182, 431);
            this.selectedSubscriber.TabIndex = 0;
            // 
            // filterSubscriber
            // 
            this.filterSubscriber.LabelText = "Запрос";
            this.filterSubscriber.Location = new System.Drawing.Point(3, 3);
            this.filterSubscriber.Mask = "";
            this.filterSubscriber.Name = "filterSubscriber";
            this.filterSubscriber.ReadOnly = false;
            this.filterSubscriber.Size = new System.Drawing.Size(171, 40);
            this.filterSubscriber.TabIndex = 3;
            this.filterSubscriber.TextChanged += new System.EventHandler(this.filterSubscriber_TextChanged);
            // 
            // filterData
            // 
            this.filterData.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.filterData.LabelText = "Данные";
            this.filterData.Location = new System.Drawing.Point(3, 49);
            this.filterData.Mask = "";
            this.filterData.Name = "filterData";
            this.filterData.ReadOnly = false;
            this.filterData.Size = new System.Drawing.Size(171, 40);
            this.filterData.TabIndex = 4;
            this.filterData.TextChanged += new System.EventHandler(this.filterData_TextChanged);
            // 
            // ManualForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(916, 534);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.manualMenuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.manualMenuStrip;
            this.MinimumSize = new System.Drawing.Size(924, 561);
            this.Name = "ManualForm";
            this.Text = "База данных кросса";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ManualForm_FormClosing);
            this.Load += new System.EventHandler(this.ManualForm_Load);
            this.Resize += new System.EventHandler(this.ManualForm_Resize);
            this.manualMenuStrip.ResumeLayout(false);
            this.manualMenuStrip.PerformLayout();
            this.subscriberMapGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.subView)).EndInit();
            this.selectedGroupBox.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.filterFlowLayout.ResumeLayout(false);
            this.filterFlowLayout.PerformLayout();
            this.notifyMenuStrip.ResumeLayout(false);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip manualMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem файлToolStripMenuItem;
        private System.Windows.Forms.GroupBox subscriberMapGroupBox;
        private System.Windows.Forms.GroupBox selectedGroupBox;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.FlowLayoutPanel filterFlowLayout;
        private System.Windows.Forms.ToolStripMenuItem controlStripMenu;

        private Components.Subscriber.SelectedSubscriber selectedSubscriber;
        private System.Windows.Forms.ToolStripMenuItem importFileToolStrip;
        private ATSManual.Database.ATSDataSetTableAdapters.SubscriberDataTableAdapter subscriberTableAdapter;
        private ATSManual.Database.ATSDataSetTableAdapters.DepartmentTableAdapter departmentTableAdapter;
        private LabelTextBox filterSubscriber;
        private LabelTextBox filterData;
        private ATSManual.Database.ATSDataSetTableAdapters.DataTableAdapter dataTableAdapter;
        private System.Windows.Forms.Button addSubscriberButton;
        private System.Windows.Forms.Button removeSubscriberButton;
        private System.Windows.Forms.ToolStripMenuItem clearDatabaseMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportMenuItem;
        private System.Windows.Forms.CheckBox withoutDataFilterCheckbox;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ContextMenuStrip notifyMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem openNotifyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitNotifyToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckedListBox statusCheckedListBox;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripProgressBar progressBar;
        private System.Windows.Forms.ToolStripStatusLabel progressBarLabel;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ToolStripMenuItem connectToDatabaseToolStripMenu;
        private System.Windows.Forms.CheckBox isEmptySubscriberFilterCheckBox;
        private System.Windows.Forms.DataGridView subView;
        private System.Windows.Forms.DataGridViewTextBoxColumn phoneColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn subscriber;
        private System.Windows.Forms.TableLayoutPanel phoneHistory;
        private System.Windows.Forms.ToolStripMenuItem configToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.Button openHistoryButton;
    }
}

