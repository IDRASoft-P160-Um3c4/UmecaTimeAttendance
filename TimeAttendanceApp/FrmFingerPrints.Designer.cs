namespace TimeAttendanceApp
{
    partial class FrmFingerPrints
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
            this.dataGridViewFP = new System.Windows.Forms.DataGridView();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Employee = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Fp0 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Fp1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Fp2 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Fp3 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Fp4 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Fp5 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Fp6 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Fp7 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Fp8 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Fp9 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.cboDevices = new System.Windows.Forms.ComboBox();
            this.lblDevice = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewFP)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewFP
            // 
            this.dataGridViewFP.AllowUserToAddRows = false;
            this.dataGridViewFP.AllowUserToDeleteRows = false;
            this.dataGridViewFP.AllowUserToResizeColumns = false;
            this.dataGridViewFP.AllowUserToResizeRows = false;
            this.dataGridViewFP.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewFP.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Id,
            this.Employee,
            this.Fp0,
            this.Fp1,
            this.Fp2,
            this.Fp3,
            this.Fp4,
            this.Fp5,
            this.Fp6,
            this.Fp7,
            this.Fp8,
            this.Fp9});
            this.dataGridViewFP.Location = new System.Drawing.Point(13, 59);
            this.dataGridViewFP.MultiSelect = false;
            this.dataGridViewFP.Name = "dataGridViewFP";
            this.dataGridViewFP.Size = new System.Drawing.Size(645, 390);
            this.dataGridViewFP.TabIndex = 0;
            this.dataGridViewFP.CurrentCellDirtyStateChanged += new System.EventHandler(this.dataGridViewFP_CurrentCellDirtyStateChanged);
            // 
            // Id
            // 
            this.Id.DataPropertyName = "Id";
            this.Id.HeaderText = "# Empleado";
            this.Id.Name = "Id";
            this.Id.ReadOnly = true;
            this.Id.Width = 75;
            // 
            // Employee
            // 
            this.Employee.DataPropertyName = "Name";
            this.Employee.HeaderText = "Nombre";
            this.Employee.Name = "Employee";
            this.Employee.ReadOnly = true;
            this.Employee.Width = 200;
            // 
            // Fp0
            // 
            this.Fp0.DataPropertyName = "Fp0";
            this.Fp0.HeaderText = "A";
            this.Fp0.Name = "Fp0";
            this.Fp0.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Fp0.ToolTipText = "Meñique Izquierdo";
            this.Fp0.Width = 30;
            // 
            // Fp1
            // 
            this.Fp1.DataPropertyName = "Fp1";
            this.Fp1.HeaderText = "B";
            this.Fp1.Name = "Fp1";
            this.Fp1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Fp1.ToolTipText = "Anular Izquierdo";
            this.Fp1.Width = 30;
            // 
            // Fp2
            // 
            this.Fp2.DataPropertyName = "Fp2";
            this.Fp2.HeaderText = "C";
            this.Fp2.Name = "Fp2";
            this.Fp2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Fp2.ToolTipText = "Medio Izquierdo";
            this.Fp2.Width = 30;
            // 
            // Fp3
            // 
            this.Fp3.DataPropertyName = "Fp3";
            this.Fp3.HeaderText = "D";
            this.Fp3.Name = "Fp3";
            this.Fp3.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Fp3.ToolTipText = "Índice Izquierdo";
            this.Fp3.Width = 30;
            // 
            // Fp4
            // 
            this.Fp4.DataPropertyName = "Fp4";
            this.Fp4.HeaderText = "E";
            this.Fp4.Name = "Fp4";
            this.Fp4.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Fp4.ToolTipText = "Pulgar Izquierdo";
            this.Fp4.Width = 30;
            // 
            // Fp5
            // 
            this.Fp5.DataPropertyName = "Fp5";
            this.Fp5.HeaderText = "F";
            this.Fp5.Name = "Fp5";
            this.Fp5.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Fp5.ToolTipText = "Pulgar Derecho";
            this.Fp5.Width = 30;
            // 
            // Fp6
            // 
            this.Fp6.DataPropertyName = "Fp6";
            this.Fp6.HeaderText = "G";
            this.Fp6.Name = "Fp6";
            this.Fp6.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Fp6.ToolTipText = "Índice Derecho";
            this.Fp6.Width = 30;
            // 
            // Fp7
            // 
            this.Fp7.DataPropertyName = "Fp7";
            this.Fp7.HeaderText = "H";
            this.Fp7.Name = "Fp7";
            this.Fp7.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Fp7.ToolTipText = "Medio Derecho";
            this.Fp7.Width = 30;
            // 
            // Fp8
            // 
            this.Fp8.DataPropertyName = "Fp8";
            this.Fp8.HeaderText = "I";
            this.Fp8.Name = "Fp8";
            this.Fp8.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Fp8.ToolTipText = "Anular Derecho";
            this.Fp8.Width = 30;
            // 
            // Fp9
            // 
            this.Fp9.DataPropertyName = "Fp9";
            this.Fp9.HeaderText = "J";
            this.Fp9.Name = "Fp9";
            this.Fp9.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Fp9.ToolTipText = "Meñique Derecho";
            this.Fp9.Width = 30;
            // 
            // cboDevices
            // 
            this.cboDevices.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDevices.FormattingEnabled = true;
            this.cboDevices.Location = new System.Drawing.Point(97, 16);
            this.cboDevices.Name = "cboDevices";
            this.cboDevices.Size = new System.Drawing.Size(238, 21);
            this.cboDevices.TabIndex = 1;
            // 
            // lblDevice
            // 
            this.lblDevice.AutoSize = true;
            this.lblDevice.Location = new System.Drawing.Point(30, 19);
            this.lblDevice.Name = "lblDevice";
            this.lblDevice.Size = new System.Drawing.Size(61, 13);
            this.lblDevice.TabIndex = 2;
            this.lblDevice.Text = "Dispositivo:";
            // 
            // FrmFingerPrints
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(671, 460);
            this.Controls.Add(this.lblDevice);
            this.Controls.Add(this.cboDevices);
            this.Controls.Add(this.dataGridViewFP);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmFingerPrints";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FrmFingerPrints";
            this.Load += new System.EventHandler(this.FrmFingerPrints_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewFP)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewFP;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn Employee;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Fp0;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Fp1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Fp2;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Fp3;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Fp4;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Fp5;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Fp6;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Fp7;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Fp8;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Fp9;
        private System.Windows.Forms.ComboBox cboDevices;
        private System.Windows.Forms.Label lblDevice;
    }
}