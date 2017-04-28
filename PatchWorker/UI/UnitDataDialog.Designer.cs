namespace PatchWorker.UI
{
    partial class UnitDataDialog
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
            this.lblName = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.rbInput = new System.Windows.Forms.RadioButton();
            this.rbOutput = new System.Windows.Forms.RadioButton();
            this.rbModifier = new System.Windows.Forms.RadioButton();
            this.rbgUnitType = new System.Windows.Forms.GroupBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.lblDevice = new System.Windows.Forms.Label();
            this.lblChannel = new System.Windows.Forms.Label();
            this.lblProgCount = new System.Windows.Forms.Label();
            this.txtProgCount = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cbxChannel = new System.Windows.Forms.ComboBox();
            this.cbxDevice = new System.Windows.Forms.ComboBox();
            this.rbgUnitType.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(12, 31);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(35, 13);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "Name";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(68, 31);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(267, 20);
            this.txtName.TabIndex = 1;
            this.txtName.TextAlignChanged += new System.EventHandler(this.validateControlData);
            // 
            // rbInput
            // 
            this.rbInput.AutoSize = true;
            this.rbInput.Checked = true;
            this.rbInput.Location = new System.Drawing.Point(22, 22);
            this.rbInput.Name = "rbInput";
            this.rbInput.Size = new System.Drawing.Size(69, 17);
            this.rbInput.TabIndex = 2;
            this.rbInput.TabStop = true;
            this.rbInput.Text = "Controller";
            this.rbInput.UseVisualStyleBackColor = true;
            this.rbInput.CheckedChanged += new System.EventHandler(this.rbInput_CheckedChanged);
            // 
            // rbOutput
            // 
            this.rbOutput.AutoSize = true;
            this.rbOutput.Location = new System.Drawing.Point(241, 22);
            this.rbOutput.Name = "rbOutput";
            this.rbOutput.Size = new System.Drawing.Size(60, 17);
            this.rbOutput.TabIndex = 4;
            this.rbOutput.Text = "Module";
            this.rbOutput.UseVisualStyleBackColor = true;
            this.rbOutput.CheckedChanged += new System.EventHandler(this.rbOutput_CheckedChanged);
            // 
            // rbModifier
            // 
            this.rbModifier.AutoSize = true;
            this.rbModifier.Location = new System.Drawing.Point(135, 22);
            this.rbModifier.Name = "rbModifier";
            this.rbModifier.Size = new System.Drawing.Size(62, 17);
            this.rbModifier.TabIndex = 3;
            this.rbModifier.Text = "Modifier";
            this.rbModifier.UseVisualStyleBackColor = true;
            this.rbModifier.CheckedChanged += new System.EventHandler(this.rbModifier_CheckedChanged);
            // 
            // rbgUnitType
            // 
            this.rbgUnitType.Controls.Add(this.rbOutput);
            this.rbgUnitType.Controls.Add(this.rbModifier);
            this.rbgUnitType.Controls.Add(this.rbInput);
            this.rbgUnitType.Location = new System.Drawing.Point(12, 68);
            this.rbgUnitType.Name = "rbgUnitType";
            this.rbgUnitType.Size = new System.Drawing.Size(323, 60);
            this.rbgUnitType.TabIndex = 5;
            this.rbgUnitType.TabStop = false;
            this.rbgUnitType.Text = "Unit Type";
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(234, 281);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 100;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Enabled = false;
            this.btnOK.Location = new System.Drawing.Point(339, 281);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 101;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // lblDevice
            // 
            this.lblDevice.AutoSize = true;
            this.lblDevice.Location = new System.Drawing.Point(15, 29);
            this.lblDevice.Name = "lblDevice";
            this.lblDevice.Size = new System.Drawing.Size(68, 13);
            this.lblDevice.TabIndex = 0;
            this.lblDevice.Text = "Input Device";
            // 
            // lblChannel
            // 
            this.lblChannel.AutoSize = true;
            this.lblChannel.Location = new System.Drawing.Point(15, 73);
            this.lblChannel.Name = "lblChannel";
            this.lblChannel.Size = new System.Drawing.Size(73, 13);
            this.lblChannel.TabIndex = 2;
            this.lblChannel.Text = "Input Channel";
            // 
            // lblProgCount
            // 
            this.lblProgCount.AutoSize = true;
            this.lblProgCount.Enabled = false;
            this.lblProgCount.Location = new System.Drawing.Point(182, 73);
            this.lblProgCount.Name = "lblProgCount";
            this.lblProgCount.Size = new System.Drawing.Size(103, 13);
            this.lblProgCount.TabIndex = 4;
            this.lblProgCount.Text = "Number of Programs";
            // 
            // txtProgCount
            // 
            this.txtProgCount.Enabled = false;
            this.txtProgCount.Location = new System.Drawing.Point(287, 69);
            this.txtProgCount.Name = "txtProgCount";
            this.txtProgCount.Size = new System.Drawing.Size(100, 20);
            this.txtProgCount.TabIndex = 8;
            this.txtProgCount.Text = "0";
            // 
            // panel1
            // 
            this.panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.cbxChannel);
            this.panel1.Controls.Add(this.cbxDevice);
            this.panel1.Controls.Add(this.txtProgCount);
            this.panel1.Controls.Add(this.lblProgCount);
            this.panel1.Controls.Add(this.lblChannel);
            this.panel1.Controls.Add(this.lblDevice);
            this.panel1.Location = new System.Drawing.Point(12, 147);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(402, 116);
            this.panel1.TabIndex = 6;
            // 
            // cbxChannel
            // 
            this.cbxChannel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxChannel.FormattingEnabled = true;
            this.cbxChannel.Location = new System.Drawing.Point(96, 69);
            this.cbxChannel.Name = "cbxChannel";
            this.cbxChannel.Size = new System.Drawing.Size(80, 21);
            this.cbxChannel.TabIndex = 7;
            this.cbxChannel.SelectedIndexChanged += new System.EventHandler(this.validateControlData);
            // 
            // cbxDevice
            // 
            this.cbxDevice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxDevice.FormattingEnabled = true;
            this.cbxDevice.Location = new System.Drawing.Point(96, 26);
            this.cbxDevice.Name = "cbxDevice";
            this.cbxDevice.Size = new System.Drawing.Size(291, 21);
            this.cbxDevice.TabIndex = 6;
            this.cbxDevice.SelectedIndexChanged += new System.EventHandler(this.validateControlData);
            // 
            // UnitDataDialog
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(426, 321);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.rbgUnitType);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.lblName);
            this.Name = "UnitDataDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "New Unit Settings";
            this.rbgUnitType.ResumeLayout(false);
            this.rbgUnitType.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.RadioButton rbInput;
        private System.Windows.Forms.RadioButton rbOutput;
        private System.Windows.Forms.RadioButton rbModifier;
        private System.Windows.Forms.GroupBox rbgUnitType;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label lblDevice;
        private System.Windows.Forms.Label lblChannel;
        private System.Windows.Forms.Label lblProgCount;
        private System.Windows.Forms.TextBox txtProgCount;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox cbxChannel;
        private System.Windows.Forms.ComboBox cbxDevice;
    }
}