namespace DVLD2.CarRentalAllFiles.Vehicles
{
    partial class frmShowVehicleDetails
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
            this.ctrlVehilesCardDetails1 = new DVLD2.CarRentalAllFiles.Vehicles.Controls.ctrlVehilesCardDetails();
            this.SuspendLayout();
            // 
            // ctrlVehilesCardDetails1
            // 
            this.ctrlVehilesCardDetails1.BackColor = System.Drawing.Color.White;
            this.ctrlVehilesCardDetails1.Location = new System.Drawing.Point(0, 4);
            this.ctrlVehilesCardDetails1.Name = "ctrlVehilesCardDetails1";
            this.ctrlVehilesCardDetails1.Size = new System.Drawing.Size(728, 580);
            this.ctrlVehilesCardDetails1.TabIndex = 0;
            // 
            // frmShowVehicleDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(726, 583);
            this.Controls.Add(this.ctrlVehilesCardDetails1);
            this.Name = "frmShowVehicleDetails";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Vehicle Details";
            this.Load += new System.EventHandler(this.frmShowVehicleDetails_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.ctrlVehilesCardDetails ctrlVehilesCardDetails1;
    }
}