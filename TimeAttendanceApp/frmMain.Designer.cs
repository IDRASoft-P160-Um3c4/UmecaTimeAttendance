namespace TimeAttendanceApp
{
    partial class FrmMain
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
            this.btnSyncUsers = new System.Windows.Forms.Button();
            this.btnEnroll = new System.Windows.Forms.Button();
            this.btnAssistence = new System.Windows.Forms.Button();
            this.btnWsAddress = new System.Windows.Forms.Button();
            this.btnImputed = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnSyncUsers
            // 
            this.btnSyncUsers.Location = new System.Drawing.Point(13, 25);
            this.btnSyncUsers.Name = "btnSyncUsers";
            this.btnSyncUsers.Size = new System.Drawing.Size(377, 71);
            this.btnSyncUsers.TabIndex = 0;
            this.btnSyncUsers.Text = "Sincronizar Usuarios";
            this.btnSyncUsers.UseVisualStyleBackColor = true;
            this.btnSyncUsers.Click += new System.EventHandler(this.btnSyncUsers_Click);
            // 
            // btnEnroll
            // 
            this.btnEnroll.Location = new System.Drawing.Point(13, 102);
            this.btnEnroll.Name = "btnEnroll";
            this.btnEnroll.Size = new System.Drawing.Size(377, 71);
            this.btnEnroll.TabIndex = 1;
            this.btnEnroll.Text = "Enrolamiento de Huellas Digitales";
            this.btnEnroll.UseVisualStyleBackColor = true;
            this.btnEnroll.Click += new System.EventHandler(this.btnEnroll_Click);
            // 
            // btnAssistence
            // 
            this.btnAssistence.Location = new System.Drawing.Point(13, 179);
            this.btnAssistence.Name = "btnAssistence";
            this.btnAssistence.Size = new System.Drawing.Size(377, 71);
            this.btnAssistence.TabIndex = 2;
            this.btnAssistence.Text = "Obtener Control de Asistencia";
            this.btnAssistence.UseVisualStyleBackColor = true;
            this.btnAssistence.Click += new System.EventHandler(this.btnAssistence_Click);
            // 
            // btnWsAddress
            // 
            this.btnWsAddress.Location = new System.Drawing.Point(13, 333);
            this.btnWsAddress.Name = "btnWsAddress";
            this.btnWsAddress.Size = new System.Drawing.Size(377, 71);
            this.btnWsAddress.TabIndex = 4;
            this.btnWsAddress.Text = "Dirección del servicio web";
            this.btnWsAddress.UseVisualStyleBackColor = true;
            this.btnWsAddress.Click += new System.EventHandler(this.btnWsAddress_Click);
            // 
            // btnImputed
            // 
            this.btnImputed.Location = new System.Drawing.Point(12, 256);
            this.btnImputed.Name = "btnImputed";
            this.btnImputed.Size = new System.Drawing.Size(377, 71);
            this.btnImputed.TabIndex = 3;
            this.btnImputed.Text = "Obtener Asistencia de Imputados";
            this.btnImputed.UseVisualStyleBackColor = true;
            this.btnImputed.Click += new System.EventHandler(this.btnImputed_Click);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(402, 416);
            this.Controls.Add(this.btnImputed);
            this.Controls.Add(this.btnWsAddress);
            this.Controls.Add(this.btnAssistence);
            this.Controls.Add(this.btnEnroll);
            this.Controls.Add(this.btnSyncUsers);
            this.MaximizeBox = false;
            this.Name = "FrmMain";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Umeca - CheckClock System";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSyncUsers;
        private System.Windows.Forms.Button btnEnroll;
        private System.Windows.Forms.Button btnAssistence;
        private System.Windows.Forms.Button btnWsAddress;
        private System.Windows.Forms.Button btnImputed;
    }
}

