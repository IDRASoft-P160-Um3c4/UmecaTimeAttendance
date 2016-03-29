namespace TimeAttendanceApp
{
    partial class FrmWsAddress
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
            this.btnSaveRegistry = new System.Windows.Forms.Button();
            this.txtBoxAddress = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnSaveRegistry
            // 
            this.btnSaveRegistry.Location = new System.Drawing.Point(197, 101);
            this.btnSaveRegistry.Name = "btnSaveRegistry";
            this.btnSaveRegistry.Size = new System.Drawing.Size(75, 23);
            this.btnSaveRegistry.TabIndex = 0;
            this.btnSaveRegistry.Text = "Guardar";
            this.btnSaveRegistry.UseVisualStyleBackColor = true;
            this.btnSaveRegistry.Click += new System.EventHandler(this.btnSaveRegistry_Click);
            // 
            // txtBoxAddress
            // 
            this.txtBoxAddress.Location = new System.Drawing.Point(74, 59);
            this.txtBoxAddress.Name = "txtBoxAddress";
            this.txtBoxAddress.Size = new System.Drawing.Size(337, 20);
            this.txtBoxAddress.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(71, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(340, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Ingrese la dirección del servicio web a la que se conectará la apliación";
            // 
            // FrmWsAddress
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(464, 161);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtBoxAddress);
            this.Controls.Add(this.btnSaveRegistry);
            this.Name = "FrmWsAddress";
            this.Text = "Dirección";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSaveRegistry;
        private System.Windows.Forms.TextBox txtBoxAddress;
        private System.Windows.Forms.Label label1;
    }
}