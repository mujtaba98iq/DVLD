namespace DVLD2.CarRentalAllFiles.Reservations.Controls
{
    partial class ctrlReservationVehicleCard
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
            this.lblYear = new System.Windows.Forms.Label();
            this.lblPricePerDay = new System.Windows.Forms.Label();
            this.lblIsAvalibel = new System.Windows.Forms.Label();
            this.lblVehicleName = new System.Windows.Forms.Label();
            this.lblVehicleID = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.bunifuElipse1 = new Bunifu.Framework.UI.BunifuElipse(this.components);
            this.cmsVehicle = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmsbtnVehicleDetails = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsbtnReturnVehicleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label2 = new System.Windows.Forms.Label();
            this.lblReservationID = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblPickupLocation = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblDropoffLocation = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblInitialRentalDays = new System.Windows.Forms.Label();
            this.pbVehicle = new System.Windows.Forms.PictureBox();
            this.cmsVehicle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbVehicle)).BeginInit();
            this.SuspendLayout();
            // 
            // lblYear
            // 
            this.lblYear.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lblYear.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.lblYear.Location = new System.Drawing.Point(3, 188);
            this.lblYear.Name = "lblYear";
            this.lblYear.Size = new System.Drawing.Size(54, 28);
            this.lblYear.TabIndex = 10;
            this.lblYear.Text = "2020";
            this.lblYear.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPricePerDay
            // 
            this.lblPricePerDay.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lblPricePerDay.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.lblPricePerDay.Location = new System.Drawing.Point(433, 186);
            this.lblPricePerDay.Name = "lblPricePerDay";
            this.lblPricePerDay.Size = new System.Drawing.Size(99, 30);
            this.lblPricePerDay.TabIndex = 4;
            this.lblPricePerDay.Text = "50$";
            this.lblPricePerDay.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblIsAvalibel
            // 
            this.lblIsAvalibel.BackColor = System.Drawing.Color.LightBlue;
            this.lblIsAvalibel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.lblIsAvalibel.Location = new System.Drawing.Point(416, 24);
            this.lblIsAvalibel.Name = "lblIsAvalibel";
            this.lblIsAvalibel.Size = new System.Drawing.Size(119, 30);
            this.lblIsAvalibel.TabIndex = 5;
            this.lblIsAvalibel.Text = "Is Not Avlibel";
            this.lblIsAvalibel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblVehicleName
            // 
            this.lblVehicleName.AutoSize = true;
            this.lblVehicleName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.lblVehicleName.Location = new System.Drawing.Point(168, 305);
            this.lblVehicleName.Name = "lblVehicleName";
            this.lblVehicleName.Size = new System.Drawing.Size(36, 20);
            this.lblVehicleName.TabIndex = 6;
            this.lblVehicleName.Text = "???";
            // 
            // lblVehicleID
            // 
            this.lblVehicleID.AutoSize = true;
            this.lblVehicleID.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.lblVehicleID.Location = new System.Drawing.Point(168, 269);
            this.lblVehicleID.Name = "lblVehicleID";
            this.lblVehicleID.Size = new System.Drawing.Size(36, 20);
            this.lblVehicleID.TabIndex = 7;
            this.lblVehicleID.Text = "???";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label8.Location = new System.Drawing.Point(33, 305);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(111, 20);
            this.label8.TabIndex = 8;
            this.label8.Text = "Vehicle Name:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label1.Location = new System.Drawing.Point(58, 269);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 20);
            this.label1.TabIndex = 9;
            this.label1.Text = "Vehicle ID:";
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
            this.cmsbtnReturnVehicleToolStripMenuItem});
            this.cmsVehicle.Name = "cmsVehicle";
            this.cmsVehicle.Size = new System.Drawing.Size(174, 48);
            // 
            // cmsbtnVehicleDetails
            // 
            this.cmsbtnVehicleDetails.Name = "cmsbtnVehicleDetails";
            this.cmsbtnVehicleDetails.Size = new System.Drawing.Size(180, 22);
            this.cmsbtnVehicleDetails.Text = "Reservatoin Details";
            this.cmsbtnVehicleDetails.Click += new System.EventHandler(this.cmsbtnVehicleDetails_Click);
            // 
            // cmsbtnReturnVehicleToolStripMenuItem
            // 
            this.cmsbtnReturnVehicleToolStripMenuItem.Name = "cmsbtnReturnVehicleToolStripMenuItem";
            this.cmsbtnReturnVehicleToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.cmsbtnReturnVehicleToolStripMenuItem.Text = "Return Vehicle";
            this.cmsbtnReturnVehicleToolStripMenuItem.Click += new System.EventHandler(this.cmsbtnReturnVehicleToolStripMenuItem_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label2.Location = new System.Drawing.Point(29, 233);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(115, 20);
            this.label2.TabIndex = 9;
            this.label2.Text = "ReservationID:";
            // 
            // lblReservationID
            // 
            this.lblReservationID.AutoSize = true;
            this.lblReservationID.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.lblReservationID.Location = new System.Drawing.Point(168, 233);
            this.lblReservationID.Name = "lblReservationID";
            this.lblReservationID.Size = new System.Drawing.Size(36, 20);
            this.lblReservationID.TabIndex = 7;
            this.lblReservationID.Text = "???";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label3.Location = new System.Drawing.Point(19, 377);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(125, 20);
            this.label3.TabIndex = 8;
            this.label3.Text = "Pickup Location:";
            // 
            // lblPickupLocation
            // 
            this.lblPickupLocation.AutoSize = true;
            this.lblPickupLocation.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.lblPickupLocation.Location = new System.Drawing.Point(168, 377);
            this.lblPickupLocation.Name = "lblPickupLocation";
            this.lblPickupLocation.Size = new System.Drawing.Size(36, 20);
            this.lblPickupLocation.TabIndex = 6;
            this.lblPickupLocation.Text = "???";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label5.Location = new System.Drawing.Point(12, 413);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(132, 20);
            this.label5.TabIndex = 8;
            this.label5.Text = "Dropoff Location:";
            // 
            // lblDropoffLocation
            // 
            this.lblDropoffLocation.AutoSize = true;
            this.lblDropoffLocation.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.lblDropoffLocation.Location = new System.Drawing.Point(168, 413);
            this.lblDropoffLocation.Name = "lblDropoffLocation";
            this.lblDropoffLocation.Size = new System.Drawing.Size(36, 20);
            this.lblDropoffLocation.TabIndex = 6;
            this.lblDropoffLocation.Text = "???";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label4.Location = new System.Drawing.Point(3, 341);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(141, 20);
            this.label4.TabIndex = 8;
            this.label4.Text = "Initial Rental Days:";
            // 
            // lblInitialRentalDays
            // 
            this.lblInitialRentalDays.AutoSize = true;
            this.lblInitialRentalDays.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.lblInitialRentalDays.Location = new System.Drawing.Point(168, 341);
            this.lblInitialRentalDays.Name = "lblInitialRentalDays";
            this.lblInitialRentalDays.Size = new System.Drawing.Size(36, 20);
            this.lblInitialRentalDays.TabIndex = 6;
            this.lblInitialRentalDays.Text = "???";
            // 
            // pbVehicle
            // 
            this.pbVehicle.BackColor = System.Drawing.Color.White;
            this.pbVehicle.Dock = System.Windows.Forms.DockStyle.Top;
            this.pbVehicle.Image = global::DVLD2.Properties.Resources.car;
            this.pbVehicle.Location = new System.Drawing.Point(0, 0);
            this.pbVehicle.Name = "pbVehicle";
            this.pbVehicle.Size = new System.Drawing.Size(536, 220);
            this.pbVehicle.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbVehicle.TabIndex = 3;
            this.pbVehicle.TabStop = false;
            // 
            // ctrlReservationVehicleCard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightBlue;
            this.ContextMenuStrip = this.cmsVehicle;
            this.Controls.Add(this.lblYear);
            this.Controls.Add(this.lblPricePerDay);
            this.Controls.Add(this.lblIsAvalibel);
            this.Controls.Add(this.lblDropoffLocation);
            this.Controls.Add(this.lblInitialRentalDays);
            this.Controls.Add(this.lblPickupLocation);
            this.Controls.Add(this.lblVehicleName);
            this.Controls.Add(this.lblReservationID);
            this.Controls.Add(this.lblVehicleID);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pbVehicle);
            this.Margin = new System.Windows.Forms.Padding(3, 10, 3, 10);
            this.Name = "ctrlReservationVehicleCard";
            this.Size = new System.Drawing.Size(536, 451);
            this.Load += new System.EventHandler(this.ctrlReservationVehicleCard_Load);
            this.cmsVehicle.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbVehicle)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblYear;
        private System.Windows.Forms.Label lblPricePerDay;
        private System.Windows.Forms.Label lblIsAvalibel;
        private System.Windows.Forms.Label lblVehicleName;
        private System.Windows.Forms.Label lblVehicleID;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pbVehicle;
        private Bunifu.Framework.UI.BunifuElipse bunifuElipse1;
        private System.Windows.Forms.ContextMenuStrip cmsVehicle;
        private System.Windows.Forms.ToolStripMenuItem cmsbtnVehicleDetails;
        private System.Windows.Forms.ToolStripMenuItem cmsbtnReturnVehicleToolStripMenuItem;
        private System.Windows.Forms.Label lblReservationID;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblDropoffLocation;
        private System.Windows.Forms.Label lblPickupLocation;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblInitialRentalDays;
    }
}
