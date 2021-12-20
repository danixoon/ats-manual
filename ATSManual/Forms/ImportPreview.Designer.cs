using ATSManual.Database;

namespace ATSManual.Forms
{
    partial class ImportPreview
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.previewTable = new System.Windows.Forms.DataGridView();
            this.importButton = new System.Windows.Forms.Button();
            this.subscriberTableAdapter = new ATSManual.Database.ATSDataSetTableAdapters.SubscriberTableAdapter();
            this.queriesTableAdapter = new ATSManual.Database.ATSDataSetTableAdapters.QueriesTableAdapter();
            this.subscriberPhone = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.subscriberName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataKey = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.previewTable)).BeginInit();
            this.SuspendLayout();
            // 
            // previewTable
            // 
            this.previewTable.AllowUserToAddRows = false;
            this.previewTable.AllowUserToDeleteRows = false;
            this.previewTable.AllowUserToResizeRows = false;
            this.previewTable.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.previewTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.previewTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.subscriberPhone,
            this.subscriberName,
            this.dataKey,
            this.dataDescription});
            this.previewTable.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.previewTable.Location = new System.Drawing.Point(12, 12);
            this.previewTable.MultiSelect = false;
            this.previewTable.Name = "previewTable";
            this.previewTable.ReadOnly = true;
            this.previewTable.ShowEditingIcon = false;
            this.previewTable.Size = new System.Drawing.Size(473, 242);
            this.previewTable.TabIndex = 0;
            // 
            // importButton
            // 
            this.importButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.importButton.Location = new System.Drawing.Point(410, 260);
            this.importButton.Name = "importButton";
            this.importButton.Size = new System.Drawing.Size(75, 23);
            this.importButton.TabIndex = 1;
            this.importButton.Text = "Загрузить";
            this.importButton.UseVisualStyleBackColor = true;
            this.importButton.Click += new System.EventHandler(this.importButton_Click);
            // 
            // subscriberTableAdapter
            // 
            this.subscriberTableAdapter.ClearBeforeFill = true;
            // 
            // subscriberPhone
            // 
            this.subscriberPhone.HeaderText = "Номер абонента";
            this.subscriberPhone.Name = "subscriberPhone";
            this.subscriberPhone.ReadOnly = true;
            // 
            // subscriberName
            // 
            this.subscriberName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.subscriberName.HeaderText = "Описание абонента";
            this.subscriberName.Name = "subscriberName";
            this.subscriberName.ReadOnly = true;
            this.subscriberName.Width = 121;
            // 
            // dataKey
            // 
            this.dataKey.HeaderText = "Данные";
            this.dataKey.Name = "dataKey";
            this.dataKey.ReadOnly = true;
            // 
            // dataDescription
            // 
            this.dataDescription.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataDescription.HeaderText = "Примечание";
            this.dataDescription.Name = "dataDescription";
            this.dataDescription.ReadOnly = true;
            // 
            // ImportPreview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(497, 295);
            this.Controls.Add(this.importButton);
            this.Controls.Add(this.previewTable);
            this.Name = "ImportPreview";
            this.Text = "Импорт данных";
            ((System.ComponentModel.ISupportInitialize)(this.previewTable)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView previewTable;
        private System.Windows.Forms.Button importButton;
        private ATSManual.Database.ATSDataSetTableAdapters.SubscriberTableAdapter subscriberTableAdapter;
        private ATSManual.Database.ATSDataSetTableAdapters.QueriesTableAdapter queriesTableAdapter;
        private System.Windows.Forms.DataGridViewTextBoxColumn subscriberPhone;
        private System.Windows.Forms.DataGridViewTextBoxColumn subscriberName;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataKey;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataDescription;
    }
}