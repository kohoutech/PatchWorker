namespace PatchWorker.Dialogs
{
    partial class OutputUnitDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OutputUnitDialog));
            this.lblName = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.cbxChannel = new System.Windows.Forms.ComboBox();
            this.cbxDevice = new System.Windows.Forms.ComboBox();
            this.txtProgCount = new System.Windows.Forms.TextBox();
            this.lblProgCount = new System.Windows.Forms.Label();
            this.lblChannel = new System.Windows.Forms.Label();
            this.lblDevice = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(15, 25);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(35, 13);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "Name";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(120, 22);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(250, 20);
            this.txtName.TabIndex = 1;
            this.txtName.TextAlignChanged += new System.EventHandler(this.validateControlData);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(190, 178);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Enabled = false;
            this.btnOK.Location = new System.Drawing.Point(295, 178);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 6;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // cbxChannel
            // 
            this.cbxChannel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxChannel.FormattingEnabled = true;
            this.cbxChannel.Location = new System.Drawing.Point(120, 96);
            this.cbxChannel.Name = "cbxChannel";
            this.cbxChannel.Size = new System.Drawing.Size(80, 21);
            this.cbxChannel.TabIndex = 3;
            this.cbxChannel.SelectedIndexChanged += new System.EventHandler(this.validateControlData);
            // 
            // cbxDevice
            // 
            this.cbxDevice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxDevice.FormattingEnabled = true;
            this.cbxDevice.Location = new System.Drawing.Point(120, 59);
            this.cbxDevice.Name = "cbxDevice";
            this.cbxDevice.Size = new System.Drawing.Size(250, 21);
            this.cbxDevice.TabIndex = 2;
            this.cbxDevice.SelectedIndexChanged += new System.EventHandler(this.validateControlData);
            // 
            // txtProgCount
            // 
            this.txtProgCount.Location = new System.Drawing.Point(120, 135);
            this.txtProgCount.Name = "txtProgCount";
            this.txtProgCount.Size = new System.Drawing.Size(80, 20);
            this.txtProgCount.TabIndex = 4;
            this.txtProgCount.Text = "0";
            // 
            // lblProgCount
            // 
            this.lblProgCount.AutoSize = true;
            this.lblProgCount.Enabled = false;
            this.lblProgCount.Location = new System.Drawing.Point(15, 138);
            this.lblProgCount.Name = "lblProgCount";
            this.lblProgCount.Size = new System.Drawing.Size(103, 13);
            this.lblProgCount.TabIndex = 104;
            this.lblProgCount.Text = "Number of Programs";
            // 
            // lblChannel
            // 
            this.lblChannel.AutoSize = true;
            this.lblChannel.Location = new System.Drawing.Point(15, 99);
            this.lblChannel.Name = "lblChannel";
            this.lblChannel.Size = new System.Drawing.Size(81, 13);
            this.lblChannel.TabIndex = 103;
            this.lblChannel.Text = "Output Channel";
            // 
            // lblDevice
            // 
            this.lblDevice.AutoSize = true;
            this.lblDevice.Location = new System.Drawing.Point(15, 62);
            this.lblDevice.Name = "lblDevice";
            this.lblDevice.Size = new System.Drawing.Size(76, 13);
            this.lblDevice.TabIndex = 102;
            this.lblDevice.Text = "Output Device";
            // 
            // OutputUnitDialog
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(394, 220);
            this.Controls.Add(this.cbxChannel);
            this.Controls.Add(this.cbxDevice);
            this.Controls.Add(this.txtProgCount);
            this.Controls.Add(this.lblProgCount);
            this.Controls.Add(this.lblChannel);
            this.Controls.Add(this.lblDevice);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.lblName);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "OutputUnitDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Output Unit Settings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.ComboBox cbxChannel;
        private System.Windows.Forms.ComboBox cbxDevice;
        private System.Windows.Forms.TextBox txtProgCount;
        private System.Windows.Forms.Label lblProgCount;
        private System.Windows.Forms.Label lblChannel;
        private System.Windows.Forms.Label lblDevice;
    }
}