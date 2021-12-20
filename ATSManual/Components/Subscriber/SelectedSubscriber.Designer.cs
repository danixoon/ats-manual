namespace ATSManual.Components.Subscriber
{
    partial class SelectedSubscriber
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.defaultLabel = new System.Windows.Forms.Label();
            this.subscriberPanel = new System.Windows.Forms.Panel();
            this.isEmptySubscriberCheckBox = new System.Windows.Forms.CheckBox();
            this.dataAddButton = new System.Windows.Forms.Button();
            this.statusComboBoxLabel = new System.Windows.Forms.Label();
            this.statusComboBox = new System.Windows.Forms.ComboBox();
            this.saveChangesButton = new System.Windows.Forms.Button();
            this.editModeButton = new System.Windows.Forms.Button();
            this.subscriberDataGroupBox = new System.Windows.Forms.GroupBox();
            this.dataLayout = new System.Windows.Forms.TableLayoutPanel();
            this.subscriberDescription = new ATSManual.LabelTextBox();
            this.phoneTextBox = new ATSManual.LabelTextBox();
            this.subscriberNameTextBox = new ATSManual.LabelTextBox();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.subscriberPanel.SuspendLayout();
            this.subscriberDataGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // defaultLabel
            // 
            this.defaultLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.defaultLabel.Location = new System.Drawing.Point(0, 0);
            this.defaultLabel.Name = "defaultLabel";
            this.defaultLabel.Size = new System.Drawing.Size(398, 450);
            this.defaultLabel.TabIndex = 1;
            this.defaultLabel.Text = "Выберите абонента";
            this.defaultLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.defaultLabel.Visible = false;
            // 
            // subscriberPanel
            // 
            this.subscriberPanel.Controls.Add(this.linkLabel1);
            this.subscriberPanel.Controls.Add(this.isEmptySubscriberCheckBox);
            this.subscriberPanel.Controls.Add(this.dataAddButton);
            this.subscriberPanel.Controls.Add(this.statusComboBoxLabel);
            this.subscriberPanel.Controls.Add(this.statusComboBox);
            this.subscriberPanel.Controls.Add(this.subscriberDescription);
            this.subscriberPanel.Controls.Add(this.saveChangesButton);
            this.subscriberPanel.Controls.Add(this.editModeButton);
            this.subscriberPanel.Controls.Add(this.phoneTextBox);
            this.subscriberPanel.Controls.Add(this.subscriberNameTextBox);
            this.subscriberPanel.Controls.Add(this.subscriberDataGroupBox);
            this.subscriberPanel.Controls.Add(this.defaultLabel);
            this.subscriberPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.subscriberPanel.Location = new System.Drawing.Point(0, 0);
            this.subscriberPanel.Name = "subscriberPanel";
            this.subscriberPanel.Size = new System.Drawing.Size(398, 450);
            this.subscriberPanel.TabIndex = 2;
            // 
            // isEmptySubscriberCheckBox
            // 
            this.isEmptySubscriberCheckBox.AutoSize = true;
            this.isEmptySubscriberCheckBox.Location = new System.Drawing.Point(9, 134);
            this.isEmptySubscriberCheckBox.Name = "isEmptySubscriberCheckBox";
            this.isEmptySubscriberCheckBox.Size = new System.Drawing.Size(97, 17);
            this.isEmptySubscriberCheckBox.TabIndex = 9;
            this.isEmptySubscriberCheckBox.Tag = "isEmpty";
            this.isEmptySubscriberCheckBox.Text = "Пустой номер";
            this.isEmptySubscriberCheckBox.UseVisualStyleBackColor = true;
            this.isEmptySubscriberCheckBox.CheckedChanged += new System.EventHandler(this.isEmptySubscriberCheckBox_CheckedChanged);
            // 
            // dataAddButton
            // 
            this.dataAddButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataAddButton.Enabled = false;
            this.dataAddButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.dataAddButton.Location = new System.Drawing.Point(73, 164);
            this.dataAddButton.MinimumSize = new System.Drawing.Size(75, 0);
            this.dataAddButton.Name = "dataAddButton";
            this.dataAddButton.Size = new System.Drawing.Size(305, 23);
            this.dataAddButton.TabIndex = 1;
            this.dataAddButton.Text = "Добавить";
            this.dataAddButton.UseVisualStyleBackColor = true;
            this.dataAddButton.Click += new System.EventHandler(this.dataAddButton_Click);
            // 
            // statusComboBoxLabel
            // 
            this.statusComboBoxLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.statusComboBoxLabel.AutoSize = true;
            this.statusComboBoxLabel.Location = new System.Drawing.Point(10, 85);
            this.statusComboBoxLabel.Name = "statusComboBoxLabel";
            this.statusComboBoxLabel.Size = new System.Drawing.Size(61, 13);
            this.statusComboBoxLabel.TabIndex = 8;
            this.statusComboBoxLabel.Text = "Состояние";
            // 
            // statusComboBox
            // 
            this.statusComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.statusComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.statusComboBox.FormattingEnabled = true;
            this.statusComboBox.Location = new System.Drawing.Point(9, 104);
            this.statusComboBox.Name = "statusComboBox";
            this.statusComboBox.Size = new System.Drawing.Size(377, 21);
            this.statusComboBox.TabIndex = 7;
            // 
            // saveChangesButton
            // 
            this.saveChangesButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.saveChangesButton.Enabled = false;
            this.saveChangesButton.Location = new System.Drawing.Point(323, 424);
            this.saveChangesButton.Name = "saveChangesButton";
            this.saveChangesButton.Size = new System.Drawing.Size(69, 23);
            this.saveChangesButton.TabIndex = 5;
            this.saveChangesButton.Text = "Сохранить";
            this.saveChangesButton.UseVisualStyleBackColor = true;
            this.saveChangesButton.Click += new System.EventHandler(this.saveChangesButton_Click);
            // 
            // editModeButton
            // 
            this.editModeButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.editModeButton.Location = new System.Drawing.Point(6, 424);
            this.editModeButton.Name = "editModeButton";
            this.editModeButton.Size = new System.Drawing.Size(314, 23);
            this.editModeButton.TabIndex = 5;
            this.editModeButton.Text = "Редактировать";
            this.editModeButton.UseVisualStyleBackColor = true;
            this.editModeButton.Click += new System.EventHandler(this.editModeButton_Click);
            // 
            // subscriberDataGroupBox
            // 
            this.subscriberDataGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.subscriberDataGroupBox.Controls.Add(this.dataLayout);
            this.subscriberDataGroupBox.Location = new System.Drawing.Point(6, 170);
            this.subscriberDataGroupBox.Name = "subscriberDataGroupBox";
            this.subscriberDataGroupBox.Size = new System.Drawing.Size(386, 248);
            this.subscriberDataGroupBox.TabIndex = 2;
            this.subscriberDataGroupBox.TabStop = false;
            this.subscriberDataGroupBox.Text = "Данные";
            // 
            // dataLayout
            // 
            this.dataLayout.AutoScroll = true;
            this.dataLayout.ColumnCount = 1;
            this.dataLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.dataLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataLayout.Location = new System.Drawing.Point(3, 16);
            this.dataLayout.Name = "dataLayout";
            this.dataLayout.RowCount = 1;
            this.dataLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.dataLayout.Size = new System.Drawing.Size(380, 229);
            this.dataLayout.TabIndex = 0;
            // 
            // subscriberDescription
            // 
            this.subscriberDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.subscriberDescription.LabelText = "Описание";
            this.subscriberDescription.Location = new System.Drawing.Point(3, 44);
            this.subscriberDescription.Mask = "";
            this.subscriberDescription.Name = "subscriberDescription";
            this.subscriberDescription.ReadOnly = true;
            this.subscriberDescription.Size = new System.Drawing.Size(389, 40);
            this.subscriberDescription.TabIndex = 6;
            this.subscriberDescription.Tag = "subscriberDescription";
            // 
            // phoneTextBox
            // 
            this.phoneTextBox.LabelText = "Номер";
            this.phoneTextBox.Location = new System.Drawing.Point(3, 3);
            this.phoneTextBox.Mask = "";
            this.phoneTextBox.Name = "phoneTextBox";
            this.phoneTextBox.ReadOnly = true;
            this.phoneTextBox.Size = new System.Drawing.Size(43, 35);
            this.phoneTextBox.TabIndex = 3;
            this.phoneTextBox.Tag = "subscriberPhone";
            // 
            // subscriberNameTextBox
            // 
            this.subscriberNameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.subscriberNameTextBox.LabelText = "Владелец";
            this.subscriberNameTextBox.Location = new System.Drawing.Point(52, 3);
            this.subscriberNameTextBox.Mask = "";
            this.subscriberNameTextBox.Name = "subscriberNameTextBox";
            this.subscriberNameTextBox.ReadOnly = true;
            this.subscriberNameTextBox.Size = new System.Drawing.Size(337, 35);
            this.subscriberNameTextBox.TabIndex = 3;
            this.subscriberNameTextBox.Tag = "subscriberName";
            // 
            // linkLabel1
            // 
            this.linkLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Enabled = false;
            this.linkLabel1.Location = new System.Drawing.Point(325, 3);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(61, 13);
            this.linkLabel1.TabIndex = 10;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Назначить";
            // 
            // SelectedSubscriber
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.subscriberPanel);
            this.Name = "SelectedSubscriber";
            this.Size = new System.Drawing.Size(398, 450);
            this.Load += new System.EventHandler(this.SelectedSubscriber_Load);
            this.subscriberPanel.ResumeLayout(false);
            this.subscriberPanel.PerformLayout();
            this.subscriberDataGroupBox.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label defaultLabel;
        private System.Windows.Forms.Panel subscriberPanel;
        private System.Windows.Forms.GroupBox subscriberDataGroupBox;
        private System.Windows.Forms.TableLayoutPanel dataLayout;
        private LabelTextBox subscriberNameTextBox;
        private System.Windows.Forms.Button editModeButton;
        private System.Windows.Forms.Button dataAddButton;
        private LabelTextBox subscriberDescription;
        private System.Windows.Forms.Button saveChangesButton;
        private LabelTextBox phoneTextBox;
        private System.Windows.Forms.Label statusComboBoxLabel;
        private System.Windows.Forms.ComboBox statusComboBox;
        private System.Windows.Forms.CheckBox isEmptySubscriberCheckBox;
        private System.Windows.Forms.LinkLabel linkLabel1;
    }
}
