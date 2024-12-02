namespace DVLD2.CarRentalAllFiles.Maintenance
{
    partial class frmAddUpdateMaintenance
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAddUpdateMaintenance));
            this.tpMaintenanceInfo = new System.Windows.Forms.TabPage();
            this.ctrlMaintenanceInfoCard1 = new DVLD2.CarRentalAllFiles.Maintenance.Controls.ctrlMaintenanceInfoCard();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.tpVehicleInfo = new System.Windows.Forms.TabPage();
            this.btnPersonInfoNext = new System.Windows.Forms.Button();
            this.ctrlVehicleCardWithFiltter1 = new DVLD2.CarRentalAllFiles.Vehicles.Controls.ctrlVehicleCardWithFiltter();
            this.tcMaintenanceInfo = new System.Windows.Forms.TabControl();
            this.tpMaintenanceInfo.SuspendLayout();
            this.tpVehicleInfo.SuspendLayout();
            this.tcMaintenanceInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // tpMaintenanceInfo
            // 
            this.tpMaintenanceInfo.Controls.Add(this.ctrlMaintenanceInfoCard1);
            this.tpMaintenanceInfo.Controls.Add(this.btnSave);
            this.tpMaintenanceInfo.Controls.Add(this.btnClose);
            this.tpMaintenanceInfo.Location = new System.Drawing.Point(4, 28);
            this.tpMaintenanceInfo.Name = "tpMaintenanceInfo";
            this.tpMaintenanceInfo.Size = new System.Drawing.Size(747, 665);
            this.tpMaintenanceInfo.TabIndex = 2;
            this.tpMaintenanceInfo.Text = "Maintenance Info";
            this.tpMaintenanceInfo.UseVisualStyleBackColor = true;
            // 
            // ctrlMaintenanceInfoCard1
            // 
            this.ctrlMaintenanceInfoCard1.BackColor = System.Drawing.Color.White;
            this.ctrlMaintenanceInfoCard1.Font = new System.Drawing.Font("Tahoma", 8F);
            this.ctrlMaintenanceInfoCard1.Location = new System.Drawing.Point(3, 3);
            this.ctrlMaintenanceInfoCard1.Name = "ctrlMaintenanceInfoCard1";
            this.ctrlMaintenanceInfoCard1.Size = new System.Drawing.Size(737, 581);
            this.ctrlMaintenanceInfoCard1.TabIndex = 121;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.LightBlue;
            this.btnSave.Enabled = false;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSave.Font = new System.Drawing.Font("Tahoma", 12F);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave.Location = new System.Drawing.Point(612, 600);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(126, 37);
            this.btnSave.TabIndex = 156;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.DarkRed;
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnClose.Font = new System.Drawing.Font("Tahoma", 12F);
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(478, 600);
            this.btnClose.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(126, 37);
            this.btnClose.TabIndex = 155;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // tpVehicleInfo
            // 
            this.tpVehicleInfo.Controls.Add(this.btnPersonInfoNext);
            this.tpVehicleInfo.Controls.Add(this.ctrlVehicleCardWithFiltter1);
            this.tpVehicleInfo.Location = new System.Drawing.Point(4, 28);
            this.tpVehicleInfo.Name = "tpVehicleInfo";
            this.tpVehicleInfo.Padding = new System.Windows.Forms.Padding(3);
            this.tpVehicleInfo.Size = new System.Drawing.Size(747, 665);
            this.tpVehicleInfo.TabIndex = 0;
            this.tpVehicleInfo.Text = "Vehicle Info";
            this.tpVehicleInfo.UseVisualStyleBackColor = true;
            // 
            // btnPersonInfoNext
            // 
            this.btnPersonInfoNext.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnPersonInfoNext.Image = global::DVLD2.Properties.Resources.Next_32;
            this.btnPersonInfoNext.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnPersonInfoNext.Location = new System.Drawing.Point(613, 612);
            this.btnPersonInfoNext.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnPersonInfoNext.Name = "btnPersonInfoNext";
            this.btnPersonInfoNext.Size = new System.Drawing.Size(126, 37);
            this.btnPersonInfoNext.TabIndex = 121;
            this.btnPersonInfoNext.Text = "Next";
            this.btnPersonInfoNext.UseVisualStyleBackColor = true;
            this.btnPersonInfoNext.Click += new System.EventHandler(this.btnPersonInfoNext_Click);
            // 
            // ctrlVehicleCardWithFiltter1
            // 
            this.ctrlVehicleCardWithFiltter1.Font = new System.Drawing.Font("Tahoma", 8F);
            this.ctrlVehicleCardWithFiltter1.Location = new System.Drawing.Point(4, 7);
            this.ctrlVehicleCardWithFiltter1.Margin = new System.Windows.Forms.Padding(4);
            this.ctrlVehicleCardWithFiltter1.Name = "ctrlVehicleCardWithFiltter1";
            this.ctrlVehicleCardWithFiltter1.Size = new System.Drawing.Size(738, 655);
            this.ctrlVehicleCardWithFiltter1.TabIndex = 120;
            // 
            // tcMaintenanceInfo
            // 
            this.tcMaintenanceInfo.Controls.Add(this.tpVehicleInfo);
            this.tcMaintenanceInfo.Controls.Add(this.tpMaintenanceInfo);
            this.tcMaintenanceInfo.Font = new System.Drawing.Font("Tahoma", 12F);
            this.tcMaintenanceInfo.Location = new System.Drawing.Point(5, 0);
            this.tcMaintenanceInfo.Name = "tcMaintenanceInfo";
            this.tcMaintenanceInfo.SelectedIndex = 0;
            this.tcMaintenanceInfo.Size = new System.Drawing.Size(755, 697);
            this.tcMaintenanceInfo.TabIndex = 157;
            // 
            // frmAddUpdateMaintenance
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.ClientSize = new System.Drawing.Size(761, 684);
            this.Controls.Add(this.tcMaintenanceInfo);
            this.Name = "frmAddUpdateMaintenance";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Add Maintenance";
            this.Load += new System.EventHandler(this.frmAddUpdateMaintenance_Load);
            this.tpMaintenanceInfo.ResumeLayout(false);
            this.tpVehicleInfo.ResumeLayout(false);
            this.tcMaintenanceInfo.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.TabPage tpMaintenanceInfo;
        private Controls.ctrlMaintenanceInfoCard ctrlMaintenanceInfoCard1;
        private System.Windows.Forms.TabPage tpVehicleInfo;
        private System.Windows.Forms.TabControl tcMaintenanceInfo;
        private Vehicles.Controls.ctrlVehicleCardWithFiltter ctrlVehicleCardWithFiltter1;
        private System.Windows.Forms.Button btnPersonInfoNext;
    }
}