namespace ATSManual.Forms
{
    partial class AddSubscriberDialog
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
            this.phoneTextBox = new ATSManual.LabelTextBox();
            this.nameTextBox = new ATSManual.LabelTextBox();
            this.descriptionTextBox = new ATSManual.LabelTextBox();
            this.addSubscriberButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // phoneTextBox
            // 
            this.phoneTextBox.LabelText = "Телефон";
            this.phoneTextBox.Location = new System.Drawing.Point(12, 2);
            this.phoneTextBox.Mask = "0000";
            this.phoneTextBox.Name = "phoneTextBox";
            this.phoneTextBox.ReadOnly = false;
            this.phoneTextBox.Size = new System.Drawing.Size(68, 40);
            this.phoneTextBox.TabIndex = 1;
            // 
            // nameTextBox
            // 
            this.nameTextBox.LabelText = "Имя абонента";
            this.nameTextBox.Location = new System.Drawing.Point(86, 2);
            this.nameTextBox.Mask = "";
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.ReadOnly = false;
            this.nameTextBox.Size = new System.Drawing.Size(194, 40);
            this.nameTextBox.TabIndex = 2;
            // 
            // descriptionTextBox
            // 
            this.descriptionTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.descriptionTextBox.LabelText = "Описание";
            this.descriptionTextBox.Location = new System.Drawing.Point(12, 48);
            this.descriptionTextBox.Mask = "";
            this.descriptionTextBox.Name = "descriptionTextBox";
            this.descriptionTextBox.ReadOnly = false;
            this.descriptionTextBox.Size = new System.Drawing.Size(268, 40);
            this.descriptionTextBox.TabIndex = 3;
            // 
            // addSubscriberButton
            // 
            this.addSubscriberButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.addSubscriberButton.Location = new System.Drawing.Point(12, 94);
            this.addSubscriberButton.Name = "addSubscriberButton";
            this.addSubscriberButton.Size = new System.Drawing.Size(268, 23);
            this.addSubscriberButton.TabIndex = 4;
            this.addSubscriberButton.Text = "Добавить";
            this.addSubscriberButton.UseVisualStyleBackColor = true;
            this.addSubscriberButton.Click += new System.EventHandler(this.addSubscriberButton_Click);
            // 
            // AddSubscriberDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 127);
            this.Controls.Add(this.addSubscriberButton);
            this.Controls.Add(this.descriptionTextBox);
            this.Controls.Add(this.nameTextBox);
            this.Controls.Add(this.phoneTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddSubscriberDialog";
            this.Text = "Добавление абонента";
            this.ResumeLayout(false);

        }

        #endregion

        private LabelTextBox phoneTextBox;
        private LabelTextBox nameTextBox;
        private LabelTextBox descriptionTextBox;
        private System.Windows.Forms.Button addSubscriberButton;
    }
}