namespace PatchWorker.Dialogs
{
    partial class PatchCordDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PatchCordDialog));
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.cbxKeySize = new System.Windows.Forms.ComboBox();
            this.lblKbdSize = new System.Windows.Forms.Label();
            this.cbxOctave = new System.Windows.Forms.ComboBox();
            this.cbxStep = new System.Windows.Forms.ComboBox();
            this.lblOctave = new System.Windows.Forms.Label();
            this.lblStep = new System.Windows.Forms.Label();
            this.btnApply = new System.Windows.Forms.Button();
            this.keysRange = new Transonic.Widget.KeyboardBar();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(394, 152);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(586, 152);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 3;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // cbxKeySize
            // 
            this.cbxKeySize.FormattingEnabled = true;
            this.cbxKeySize.Location = new System.Drawing.Point(601, 14);
            this.cbxKeySize.Name = "cbxKeySize";
            this.cbxKeySize.Size = new System.Drawing.Size(60, 21);
            this.cbxKeySize.TabIndex = 6;
            // 
            // lblKbdSize
            // 
            this.lblKbdSize.AutoSize = true;
            this.lblKbdSize.Location = new System.Drawing.Point(524, 17);
            this.lblKbdSize.Name = "lblKbdSize";
            this.lblKbdSize.Size = new System.Drawing.Size(75, 13);
            this.lblKbdSize.TabIndex = 7;
            this.lblKbdSize.Text = "Keyboard Size";
            // 
            // cbxOctave
            // 
            this.cbxOctave.FormattingEnabled = true;
            this.cbxOctave.Location = new System.Drawing.Point(110, 14);
            this.cbxOctave.Name = "cbxOctave";
            this.cbxOctave.Size = new System.Drawing.Size(60, 21);
            this.cbxOctave.TabIndex = 9;
            // 
            // cbxStep
            // 
            this.cbxStep.FormattingEnabled = true;
            this.cbxStep.Location = new System.Drawing.Point(282, 14);
            this.cbxStep.Name = "cbxStep";
            this.cbxStep.Size = new System.Drawing.Size(60, 21);
            this.cbxStep.TabIndex = 10;
            // 
            // lblOctave
            // 
            this.lblOctave.AutoSize = true;
            this.lblOctave.Location = new System.Drawing.Point(13, 17);
            this.lblOctave.Name = "lblOctave";
            this.lblOctave.Size = new System.Drawing.Size(95, 13);
            this.lblOctave.TabIndex = 4;
            this.lblOctave.Text = "Octave Transpose";
            // 
            // lblStep
            // 
            this.lblStep.AutoSize = true;
            this.lblStep.Location = new System.Drawing.Point(198, 17);
            this.lblStep.Name = "lblStep";
            this.lblStep.Size = new System.Drawing.Size(82, 13);
            this.lblStep.TabIndex = 5;
            this.lblStep.Text = "Step Transpose";
            // 
            // btnApply
            // 
            this.btnApply.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnApply.Location = new System.Drawing.Point(490, 152);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(75, 23);
            this.btnApply.TabIndex = 11;
            this.btnApply.Text = "Apply";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // keysRange
            // 
            this.keysRange.BackColor = System.Drawing.Color.White;
            this.keysRange.Location = new System.Drawing.Point(16, 49);
            this.keysRange.Name = "keysRange";
            this.keysRange.Size = new System.Drawing.Size(646, 89);
            this.keysRange.TabIndex = 8;
            // 
            // PatchCordDialog
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(674, 189);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.cbxStep);
            this.Controls.Add(this.cbxOctave);
            this.Controls.Add(this.keysRange);
            this.Controls.Add(this.lblKbdSize);
            this.Controls.Add(this.cbxKeySize);
            this.Controls.Add(this.lblStep);
            this.Controls.Add(this.lblOctave);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PatchCordDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Key Range / Transpose";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.ComboBox cbxKeySize;
        private System.Windows.Forms.Label lblKbdSize;
        private Transonic.Widget.KeyboardBar keysRange;
        private System.Windows.Forms.ComboBox cbxOctave;
        private System.Windows.Forms.ComboBox cbxStep;
        private System.Windows.Forms.Label lblOctave;
        private System.Windows.Forms.Label lblStep;
        private System.Windows.Forms.Button btnApply;
    }
}