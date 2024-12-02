namespace DVLD2.CarRentalAllFiles.Vehicles.Controls
{
    partial class ctrlVehilesCard
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.lblVehicleID = new System.Windows.Forms.Label();
            this.lblYear = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblVehicleName = new System.Windows.Forms.Label();
            this.lblIsAvalibel = new System.Windows.Forms.Label();
            this.lblPricePerDay = new System.Windows.Forms.Label();
            this.bunifuElipse1 = new Bunifu.Framework.UI.BunifuElipse(this.components);
            this.cmsVehicle = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmsbtnVehicleDetails = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsbtnVehicleReservation = new System.Windows.Forms.ToolStripMenuItem();
            this.makeNotAvailabelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pbVehicle = new System.Windows.Forms.PictureBox();
            this.vehicleManToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsVehicle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbVehicle)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label1.Location = new System.Drawing.Point(2, 229);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Vehicle ID:";
            // 
            // lblVehicleID
            // 
            this.lblVehicleID.AutoSize = true;
            this.lblVehicleID.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.lblVehicleID.Location = new System.Drawing.Point(83, 229);
            this.lblVehicleID.Name = "lblVehicleID";
            this.lblVehicleID.Size = new System.Drawing.Size(36, 20);
            this.lblVehicleID.TabIndex = 1;
            this.lblVehicleID.Text = "???";
            // 
            // lblYear
            // 
            this.lblYear.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lblYear.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.lblYear.Location = new System.Drawing.Point(6, 186);
            this.lblYear.Name = "lblYear";
            this.lblYear.Size = new System.Drawing.Size(54, 28);
            this.lblYear.TabIndex = 2;
            this.lblYear.Text = "2020";
            this.lblYear.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label8.Location = new System.Drawing.Point(2, 266);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(111, 20);
            this.label8.TabIndex = 1;
            this.label8.Text = "Vehicle Name:";
            // 
            // lblVehicleName
            // 
            this.lblVehicleName.AutoSize = true;
            this.lblVehicleName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.lblVehicleName.Location = new System.Drawing.Point(108, 266);
            this.lblVehicleName.Name = "lblVehicleName";
            this.lblVehicleName.Size = new System.Drawing.Size(36, 20);
            this.lblVehicleName.TabIndex = 1;
            this.lblVehicleName.Text = "???";
            // 
            // lblIsAvalibel
            // 
            this.lblIsAvalibel.BackColor = System.Drawing.Color.LightBlue;
            this.lblIsAvalibel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.lblIsAvalibel.Location = new System.Drawing.Point(254, 3);
            this.lblIsAvalibel.Name = "lblIsAvalibel";
            this.lblIsAvalibel.Size = new System.Drawing.Size(119, 30);
            this.lblIsAvalibel.TabIndex = 1;
            this.lblIsAvalibel.Text = "Is Not Avlibel";
            this.lblIsAvalibel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPricePerDay
            // 
            this.lblPricePerDay.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lblPricePerDay.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.lblPricePerDay.Location = new System.Drawing.Point(287, 184);
            this.lblPricePerDay.Name = "lblPricePerDay";
            this.lblPricePerDay.Size = new System.Drawing.Size(99, 30);
            this.lblPricePerDay.TabIndex = 1;
            this.lblPricePerDay.Text = "50$";
            this.lblPricePerDay.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // bunifuElipse1
            // 
            this.bunifuElipse1.ElipseRadius = 10;
            this.bunifuElipse1.TargetControl = this;
            // 
            // cmsVehicle
            // 
            this.cmsVehicle.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmsbtnVehicleDetails,
            this.editToolStripMenuItem,
            this.cmsbtnVehicleReservation,
            this.vehicleManToolStripMenuItem,
            this.makeNotAvailabelToolStripMenuItem});
            this.cmsVehicle.Name = "cmsVehicle";
            this.cmsVehicle.Size = new System.Drawing.Size(184, 136);
            // 
            // cmsbtnVehicleDetails
            // 
            this.cmsbtnVehicleDetails.Name = "cmsbtnVehicleDetails";
            this.cmsbtnVehicleDetails.Size = new System.Drawing.Size(183, 22);
            this.cmsbtnVehicleDetails.Text = "Vehicle Details";
            this.cmsbtnVehicleDetails.Click += new System.EventHandler(this.cmsbtnVehicleDetails_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.editToolStripMenuItem.Text = "Edit Vehicle Info";
            this.editToolStripMenuItem.Click += new System.EventHandler(this.editToolStripMenuItem_Click);
            // 
            // cmsbtnVehicleReservation
            // 
            this.cmsbtnVehicleReservation.Name = "cmsbtnVehicleReservation";
            this.cmsbtnVehicleReservation.Size = new System.Drawing.Size(183, 22);
            this.cmsbtnVehicleReservation.Text = "Vehicle Reservation";
            this.cmsbtnVehicleReservation.Click += new System.EventHandler(this.cmsbtnVehicleReservation_Click);
            // 
            // makeNotAvailabelToolStripMenuItem
            // 
            this.makeNotAvailabelToolStripMenuItem.Name = "makeNotAvailabelToolStripMenuItem";
            this.makeNotAvailabelToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.makeNotAvailabelToolStripMenuItem.Text = "Make Not Available";
            this.makeNotAvailabelToolStripMenuItem.Click += new System.EventHandler(this.makeNotAvailabelToolStripMenuItem_Click);
            // 
            // pbVehicle
            // 
            this.pbVehicle.BackColor = System.Drawing.Color.White;
            this.pbVehicle.Dock = System.Windows.Forms.DockStyle.Top;
            this.pbVehicle.Image = global::DVLD2.Properties.Resources.car;
            this.pbVehicle.Location = new System.Drawing.Point(0, 0);
            this.pbVehicle.Name = "pbVehicle";
            this.pbVehicle.Size = new System.Drawing.Size(375, 220);
            this.pbVehicle.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbVehicle.TabIndex = 0;
            this.pbVehicle.TabStop = false;
            // 
            // vehicleManToolStripMenuItem
            // 
            this.vehicleManToolStripMenuItem.Name = "vehicleManToolStripMenuItem";
            this.vehicleManToolStripMenuItem.Size = new System.Drawing.Size(183, 22);
            this.vehicleManToolStripMenuItem.Text = "Vehicle Maintenance";
            this.vehicleManToolStripMenuItem.Click += new System.EventHandler(this.vehicleManToolStripMenuItem_Click);
            // 
            // ctrlVehilesCard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightBlue;
            this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ContextMenuStrip = this.cmsVehicle;
            this.Controls.Add(this.lblYear);
            this.Controls.Add(this.lblPricePerDay);
            this.Controls.Add(this.lblIsAvalibel);
            this.Controls.Add(this.lblVehicleName);
            this.Controls.Add(this.lblVehicleID);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pbVehicle);
            this.Name = "ctrlVehilesCard";
            this.Size = new System.Drawing.Size(375, 296);
            this.cmsVehicle.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbVehicle)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbVehicle;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblVehicleID;
        private System.Windows.Forms.Label lblYear;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblVehicleName;
        private System.Windows.Forms.Label lblIsAvalibel;
        private System.Windows.Forms.Label lblPricePerDay;
        private Bunifu.Framework.UI.BunifuElipse bunifuElipse1;
        private System.Windows.Forms.ContextMenuStrip cmsVehicle;
        private System.Windows.Forms.ToolStripMenuItem cmsbtnVehicleDetails;
        private System.Windows.Forms.ToolStripMenuItem cmsbtnVehicleReservation;
        private System.Windows.Forms.ToolStripMenuItem makeNotAvailabelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem vehicleManToolStripMenuItem;
    }
}
