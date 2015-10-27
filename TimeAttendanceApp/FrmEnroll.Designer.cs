namespace TimeAttendanceApp
{
    partial class FrmEnroll
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
            this.lblEnroll = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblEnroll
            // 
            this.lblEnroll.AutoSize = true;
            this.lblEnroll.Location = new System.Drawing.Point(136, 54);
            this.lblEnroll.Name = "lblEnroll";
            this.lblEnroll.Size = new System.Drawing.Size(210, 13);
            this.lblEnroll.TabIndex = 0;
            this.lblEnroll.Text = "Se ha iniciado el proceso de enrolamiento, ";
            // 
            // FrmEnroll
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(463, 130);
            this.ControlBox = false;
            this.Controls.Add(this.lblEnroll);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FrmEnroll";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Enrolamiento de Huella Digital.";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblEnroll;
    }
}