namespace DVLD2.CarRentalAllFiles.Maintenance
{
    partial class frmShowMaintenanceDetals
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmShowMaintenanceDetals));
            this.ctrlMaintenanceInfoCard1 = new DVLD2.CarRentalAllFiles.Maintenance.Controls.ctrlMaintenanceInfoCard();
            this.btnClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ctrlMaintenanceInfoCard1
            // 
            this.ctrlMaintenanceInfoCard1.BackColor = System.Drawing.Color.White;
            this.ctrlMaintenanceInfoCard1.Location = new System.Drawing.Point(5, 3);
            this.ctrlMaintenanceInfoCard1.Name = "ctrlMaintenanceInfoCard1";
            this.ctrlMaintenanceInfoCard1.Size = new System.Drawing.Size(737, 581);
            this.ctrlMaintenanceInfoCard1.TabIndex = 0;
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
            this.btnClose.Location = new System.Drawing.Point(611, 592);
            this.btnClose.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(126, 37);
            this.btnClose.TabIndex = 156;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // frmShowMaintenanceDetals
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(744, 641);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.ctrlMaintenanceInfoCard1);
            this.Name = "frmShowMaintenanceDetals";
            this.Text = "Maintenance Detals";
            this.Load += new System.EventHandler(this.frmShowMaintenanceDetals_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.ctrlMaintenanceInfoCard ctrlMaintenanceInfoCard1;
        private System.Windows.Forms.Button btnClose;
    }
}