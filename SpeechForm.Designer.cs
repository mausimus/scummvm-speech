namespace ScummVMSpeechBridge
{
    partial class SpeechForm
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
            this.speechInputBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // speechInputBox
            // 
            this.speechInputBox.AcceptsReturn = true;
            this.speechInputBox.Location = new System.Drawing.Point(12, 12);
            this.speechInputBox.Name = "speechInputBox";
            this.speechInputBox.Size = new System.Drawing.Size(408, 20);
            this.speechInputBox.TabIndex = 0;
            this.speechInputBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.speechInputBox_KeyPress);
            // 
            // SpeechForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(432, 42);
            this.Controls.Add(this.speechInputBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "SpeechForm";
            this.Text = "ScummVM Speech Recognition Bridge";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox speechInputBox;
    }
}

