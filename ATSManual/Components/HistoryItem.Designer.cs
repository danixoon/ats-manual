namespace ATSManual.Components
{
    partial class HistoryItem
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
            this.subscriberName = new System.Windows.Forms.Label();
            this.time = new System.Windows.Forms.Label();
            this.subscriberPhone = new System.Windows.Forms.LinkLabel();
            this.groupBox = new System.Windows.Forms.GroupBox();
            this.groupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // subscriberName
            // 
            this.subscriberName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.subscriberName.Location = new System.Drawing.Point(43, 9);
            this.subscriberName.Name = "subscriberName";
            this.subscriberName.Size = new System.Drawing.Size(116, 13);
            this.subscriberName.TabIndex = 0;
            this.subscriberName.Text = "АТС -- ";
            this.subscriberName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // time
            // 
            this.time.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.time.AutoSize = true;
            this.time.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.time.Location = new System.Drawing.Point(165, 9);
            this.time.Name = "time";
            this.time.Size = new System.Drawing.Size(34, 13);
            this.time.TabIndex = 1;
            this.time.Text = "18:45";
            this.time.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // subscriberPhone
            // 
            this.subscriberPhone.AutoSize = true;
            this.subscriberPhone.Location = new System.Drawing.Point(6, 9);
            this.subscriberPhone.Name = "subscriberPhone";
            this.subscriberPhone.Size = new System.Drawing.Size(31, 13);
            this.subscriberPhone.TabIndex = 2;
            this.subscriberPhone.TabStop = true;
            this.subscriberPhone.Text = "4012";
            this.subscriberPhone.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.subscriberPhone.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.subscriberPhone_LinkClicked);
            // 
            // groupBox
            // 
            this.groupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox.Controls.Add(this.subscriberPhone);
            this.groupBox.Controls.Add(this.time);
            this.groupBox.Controls.Add(this.subscriberName);
            this.groupBox.Location = new System.Drawing.Point(3, -3);
            this.groupBox.Name = "groupBox";
            this.groupBox.Size = new System.Drawing.Size(205, 26);
            this.groupBox.TabIndex = 3;
            this.groupBox.TabStop = false;
            // 
            // HistoryItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox);
            this.Name = "HistoryItem";
            this.Size = new System.Drawing.Size(211, 24);
            this.Load += new System.EventHandler(this.HistoryItem_Load);
            this.groupBox.ResumeLayout(false);
            this.groupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label subscriberName;
        private System.Windows.Forms.Label time;
        private System.Windows.Forms.LinkLabel subscriberPhone;
        private System.Windows.Forms.GroupBox groupBox;
    }
}
