namespace DVLD2.CarRentalAllFiles.VehicleReturns
{
    partial class frmAddVehicleReturn
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAddVehicleReturn));
            this.tbVehicleReturnsInfo = new System.Windows.Forms.TabPage();
            this.btnClose = new System.Windows.Forms.Button();
            this.tcReservationInfo = new System.Windows.Forms.TabControl();
            this.tpCompletePayment = new System.Windows.Forms.TabPage();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.txtFinalCheckNotes = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.pictureBox6 = new System.Windows.Forms.PictureBox();
            this.label5 = new System.Windows.Forms.Label();
            this.pictureBox9 = new System.Windows.Forms.PictureBox();
            this.pictureBox10 = new System.Windows.Forms.PictureBox();
            this.label9 = new System.Windows.Forms.Label();
            this.nudMileage = new System.Windows.Forms.NumericUpDown();
            this.nudAdditionalCharges = new System.Windows.Forms.NumericUpDown();
            this.pictureBox12 = new System.Windows.Forms.PictureBox();
            this.lblVehicleID = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.pictureBox13 = new System.Windows.Forms.PictureBox();
            this.lblReservationID = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnDebtPayment = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.lblMoneyForCreditorOrDebtor = new System.Windows.Forms.Label();
            this.lblToSystemOrToCustomer = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.ctrlVehilesCard1 = new DVLD2.CarRentalAllFiles.Vehicles.Controls.ctrlVehilesCard();
            this.tbVehicleReturnsInfo.SuspendLayout();
            this.tcReservationInfo.SuspendLayout();
            this.tpCompletePayment.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMileage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudAdditionalCharges)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox12)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox13)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // tbVehicleReturnsInfo
            // 
            this.tbVehicleReturnsInfo.Controls.Add(this.btnSave);
            this.tbVehicleReturnsInfo.Controls.Add(this.pictureBox12);
            this.tbVehicleReturnsInfo.Controls.Add(this.lblVehicleID);
            this.tbVehicleReturnsInfo.Controls.Add(this.label12);
            this.tbVehicleReturnsInfo.Controls.Add(this.pictureBox13);
            this.tbVehicleReturnsInfo.Controls.Add(this.lblReservationID);
            this.tbVehicleReturnsInfo.Controls.Add(this.label14);
            this.tbVehicleReturnsInfo.Controls.Add(this.nudAdditionalCharges);
            this.tbVehicleReturnsInfo.Controls.Add(this.nudMileage);
            this.tbVehicleReturnsInfo.Controls.Add(this.label9);
            this.tbVehicleReturnsInfo.Controls.Add(this.pictureBox10);
            this.tbVehicleReturnsInfo.Controls.Add(this.label5);
            this.tbVehicleReturnsInfo.Controls.Add(this.pictureBox9);
            this.tbVehicleReturnsInfo.Controls.Add(this.txtFinalCheckNotes);
            this.tbVehicleReturnsInfo.Controls.Add(this.label4);
            this.tbVehicleReturnsInfo.Controls.Add(this.pictureBox6);
            this.tbVehicleReturnsInfo.Controls.Add(this.ctrlVehilesCard1);
            this.tbVehicleReturnsInfo.Controls.Add(this.btnClose);
            this.tbVehicleReturnsInfo.Location = new System.Drawing.Point(4, 28);
            this.tbVehicleReturnsInfo.Name = "tbVehicleReturnsInfo";
            this.tbVehicleReturnsInfo.Padding = new System.Windows.Forms.Padding(3);
            this.tbVehicleReturnsInfo.Size = new System.Drawing.Size(815, 521);
            this.tbVehicleReturnsInfo.TabIndex = 0;
            this.tbVehicleReturnsInfo.Text = "Vehicle Returns Info";
            this.tbVehicleReturnsInfo.UseVisualStyleBackColor = true;
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.DarkRed;
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnClose.Font = new System.Drawing.Font("Tahoma", 12F);
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Image = global::DVLD2.Properties.Resources.Close_321;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(291, 472);
            this.btnClose.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(126, 37);
            this.btnClose.TabIndex = 137;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // tcReservationInfo
            // 
            this.tcReservationInfo.Controls.Add(this.tbVehicleReturnsInfo);
            this.tcReservationInfo.Controls.Add(this.tpCompletePayment);
            this.tcReservationInfo.Font = new System.Drawing.Font("Tahoma", 12F);
            this.tcReservationInfo.Location = new System.Drawing.Point(3, 4);
            this.tcReservationInfo.Name = "tcReservationInfo";
            this.tcReservationInfo.SelectedIndex = 0;
            this.tcReservationInfo.Size = new System.Drawing.Size(823, 553);
            this.tcReservationInfo.TabIndex = 127;
            // 
            // tpCompletePayment
            // 
            this.tpCompletePayment.BackColor = System.Drawing.Color.White;
            this.tpCompletePayment.Controls.Add(this.pictureBox2);
            this.tpCompletePayment.Controls.Add(this.lblMoneyForCreditorOrDebtor);
            this.tpCompletePayment.Controls.Add(this.label1);
            this.tpCompletePayment.Controls.Add(this.lblToSystemOrToCustomer);
            this.tpCompletePayment.Controls.Add(this.btnDebtPayment);
            this.tpCompletePayment.Controls.Add(this.button3);
            this.tpCompletePayment.Font = new System.Drawing.Font("Tahoma", 12F);
            this.tpCompletePayment.ForeColor = System.Drawing.SystemColors.ControlText;
            this.errorProvider1.SetIconAlignment(this.tpCompletePayment, System.Windows.Forms.ErrorIconAlignment.MiddleLeft);
            this.tpCompletePayment.Location = new System.Drawing.Point(4, 28);
            this.tpCompletePayment.Name = "tpCompletePayment";
            this.tpCompletePayment.Padding = new System.Windows.Forms.Padding(3);
            this.tpCompletePayment.Size = new System.Drawing.Size(815, 521);
            this.tpCompletePayment.TabIndex = 1;
            this.tpCompletePayment.Text = "Complete Payment";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // txtFinalCheckNotes
            // 
            this.txtFinalCheckNotes.Location = new System.Drawing.Point(203, 315);
            this.txtFinalCheckNotes.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtFinalCheckNotes.MaxLength = 50;
            this.txtFinalCheckNotes.Multiline = true;
            this.txtFinalCheckNotes.Name = "txtFinalCheckNotes";
            this.txtFinalCheckNotes.Size = new System.Drawing.Size(594, 147);
            this.txtFinalCheckNotes.TabIndex = 139;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(2, 317);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(160, 20);
            this.label4.TabIndex = 140;
            this.label4.Text = "Final Check Notes:";
            // 
            // pictureBox6
            // 
            this.pictureBox6.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox6.Image")));
            this.pictureBox6.Location = new System.Drawing.Point(166, 317);
            this.pictureBox6.Name = "pictureBox6";
            this.pictureBox6.Size = new System.Drawing.Size(31, 26);
            this.pictureBox6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox6.TabIndex = 141;
            this.pictureBox6.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(-1, 219);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(166, 20);
            this.label5.TabIndex = 143;
            this.label5.Text = "Additional Charges:";
            // 
            // pictureBox9
            // 
            this.pictureBox9.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox9.Image")));
            this.pictureBox9.Location = new System.Drawing.Point(165, 217);
            this.pictureBox9.Name = "pictureBox9";
            this.pictureBox9.Size = new System.Drawing.Size(31, 26);
            this.pictureBox9.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox9.TabIndex = 144;
            this.pictureBox9.TabStop = false;
            // 
            // pictureBox10
            // 
            this.pictureBox10.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox10.Image")));
            this.pictureBox10.Location = new System.Drawing.Point(166, 145);
            this.pictureBox10.Name = "pictureBox10";
            this.pictureBox10.Size = new System.Drawing.Size(31, 26);
            this.pictureBox10.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox10.TabIndex = 144;
            this.pictureBox10.TabStop = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(86, 149);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(76, 20);
            this.label9.TabIndex = 143;
            this.label9.Text = "Mileage:";
            // 
            // nudMileage
            // 
            this.nudMileage.Location = new System.Drawing.Point(203, 147);
            this.nudMileage.Maximum = new decimal(new int[] {
            1874919424,
            2328306,
            0,
            0});
            this.nudMileage.Name = "nudMileage";
            this.nudMileage.Size = new System.Drawing.Size(217, 27);
            this.nudMileage.TabIndex = 145;
            // 
            // nudAdditionalCharges
            // 
            this.nudAdditionalCharges.Location = new System.Drawing.Point(201, 219);
            this.nudAdditionalCharges.Maximum = new decimal(new int[] {
            1874919424,
            2328306,
            0,
            0});
            this.nudAdditionalCharges.Name = "nudAdditionalCharges";
            this.nudAdditionalCharges.Size = new System.Drawing.Size(217, 27);
            this.nudAdditionalCharges.TabIndex = 145;
            // 
            // pictureBox12
            // 
            this.pictureBox12.Image = global::DVLD2.Properties.Resources.Number_32;
            this.pictureBox12.Location = new System.Drawing.Point(166, 83);
            this.pictureBox12.Name = "pictureBox12";
            this.pictureBox12.Size = new System.Drawing.Size(31, 26);
            this.pictureBox12.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox12.TabIndex = 151;
            this.pictureBox12.TabStop = false;
            // 
            // lblVehicleID
            // 
            this.lblVehicleID.AutoSize = true;
            this.lblVehicleID.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVehicleID.Location = new System.Drawing.Point(199, 89);
            this.lblVehicleID.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblVehicleID.Name = "lblVehicleID";
            this.lblVehicleID.Size = new System.Drawing.Size(39, 20);
            this.lblVehicleID.TabIndex = 150;
            this.lblVehicleID.Text = "???";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(70, 89);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(92, 20);
            this.label12.TabIndex = 149;
            this.label12.Text = "VehicleID:";
            // 
            // pictureBox13
            // 
            this.pictureBox13.Image = global::DVLD2.Properties.Resources.Number_32;
            this.pictureBox13.Location = new System.Drawing.Point(165, 27);
            this.pictureBox13.Name = "pictureBox13";
            this.pictureBox13.Size = new System.Drawing.Size(31, 26);
            this.pictureBox13.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox13.TabIndex = 148;
            this.pictureBox13.TabStop = false;
            // 
            // lblReservationID
            // 
            this.lblReservationID.AutoSize = true;
            this.lblReservationID.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReservationID.Location = new System.Drawing.Point(199, 27);
            this.lblReservationID.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblReservationID.Name = "lblReservationID";
            this.lblReservationID.Size = new System.Drawing.Size(39, 20);
            this.lblReservationID.TabIndex = 147;
            this.lblReservationID.Text = "???";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(33, 27);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(129, 20);
            this.label14.TabIndex = 146;
            this.label14.Text = "ReservationID:";
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.LightBlue;
            this.btnSave.Enabled = false;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSave.Font = new System.Drawing.Font("Tahoma", 12F);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Image = global::DVLD2.Properties.Resources.check_mark;
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave.Location = new System.Drawing.Point(425, 472);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(126, 37);
            this.btnSave.TabIndex = 152;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnDebtPayment
            // 
            this.btnDebtPayment.BackColor = System.Drawing.Color.LightBlue;
            this.btnDebtPayment.Enabled = false;
            this.btnDebtPayment.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDebtPayment.Font = new System.Drawing.Font("Tahoma", 12F);
            this.btnDebtPayment.ForeColor = System.Drawing.Color.White;
            this.btnDebtPayment.Image = global::DVLD2.Properties.Resources.check_mark;
            this.btnDebtPayment.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDebtPayment.Location = new System.Drawing.Point(374, 472);
            this.btnDebtPayment.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnDebtPayment.Name = "btnDebtPayment";
            this.btnDebtPayment.Size = new System.Drawing.Size(213, 37);
            this.btnDebtPayment.TabIndex = 154;
            this.btnDebtPayment.Text = "Debt Payment";
            this.btnDebtPayment.UseVisualStyleBackColor = false;
            this.btnDebtPayment.Click += new System.EventHandler(this.btnDebtPayment_Click);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.DarkRed;
            this.button3.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button3.Font = new System.Drawing.Font("Tahoma", 12F);
            this.button3.ForeColor = System.Drawing.Color.White;
            this.button3.Image = global::DVLD2.Properties.Resources.Close_321;
            this.button3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button3.Location = new System.Drawing.Point(233, 472);
            this.button3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(126, 37);
            this.button3.TabIndex = 153;
            this.button3.Text = "Close";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::DVLD2.Properties.Resources.Number_32;
            this.pictureBox2.Location = new System.Drawing.Point(423, 184);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(31, 26);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 157;
            this.pictureBox2.TabStop = false;
            // 
            // lblMoneyForCreditorOrDebtor
            // 
            this.lblMoneyForCreditorOrDebtor.AutoSize = true;
            this.lblMoneyForCreditorOrDebtor.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMoneyForCreditorOrDebtor.Location = new System.Drawing.Point(484, 179);
            this.lblMoneyForCreditorOrDebtor.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMoneyForCreditorOrDebtor.Name = "lblMoneyForCreditorOrDebtor";
            this.lblMoneyForCreditorOrDebtor.Size = new System.Drawing.Size(62, 31);
            this.lblMoneyForCreditorOrDebtor.TabIndex = 156;
            this.lblMoneyForCreditorOrDebtor.Text = "???";
            // 
            // lblToSystemOrToCustomer
            // 
            this.lblToSystemOrToCustomer.AutoSize = true;
            this.lblToSystemOrToCustomer.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold);
            this.lblToSystemOrToCustomer.Location = new System.Drawing.Point(220, 179);
            this.lblToSystemOrToCustomer.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblToSystemOrToCustomer.Name = "lblToSystemOrToCustomer";
            this.lblToSystemOrToCustomer.Size = new System.Drawing.Size(185, 31);
            this.lblToSystemOrToCustomer.TabIndex = 155;
            this.lblToSystemOrToCustomer.Text = "To customer:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(220, 91);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(292, 31);
            this.label1.TabIndex = 155;
            this.label1.Text = "Paid Total Remaining";
            // 
            // ctrlVehilesCard1
            // 
            this.ctrlVehilesCard1.BackColor = System.Drawing.Color.LightBlue;
            this.ctrlVehilesCard1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ctrlVehilesCard1.Font = new System.Drawing.Font("Tahoma", 8F);
            this.ctrlVehilesCard1.Location = new System.Drawing.Point(425, 4);
            this.ctrlVehilesCard1.Margin = new System.Windows.Forms.Padding(4);
            this.ctrlVehilesCard1.Name = "ctrlVehilesCard1";
            this.ctrlVehilesCard1.Size = new System.Drawing.Size(389, 296);
            this.ctrlVehilesCard1.TabIndex = 138;
            // 
            // frmAddVehicleReturn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(827, 555);
            this.Controls.Add(this.tcReservationInfo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "frmAddVehicleReturn";
            this.Text = "Return Vehicle ";
            this.tbVehicleReturnsInfo.ResumeLayout(false);
            this.tbVehicleReturnsInfo.PerformLayout();
            this.tcReservationInfo.ResumeLayout(false);
            this.tpCompletePayment.ResumeLayout(false);
            this.tpCompletePayment.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMileage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudAdditionalCharges)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox12)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox13)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabPage tbVehicleReturnsInfo;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.TabControl tcReservationInfo;
        private System.Windows.Forms.TabPage tpCompletePayment;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private Vehicles.Controls.ctrlVehilesCard ctrlVehilesCard1;
        private System.Windows.Forms.TextBox txtFinalCheckNotes;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox pictureBox6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.PictureBox pictureBox9;
        private System.Windows.Forms.NumericUpDown nudAdditionalCharges;
        private System.Windows.Forms.NumericUpDown nudMileage;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.PictureBox pictureBox10;
        private System.Windows.Forms.PictureBox pictureBox12;
        private System.Windows.Forms.Label lblVehicleID;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.PictureBox pictureBox13;
        private System.Windows.Forms.Label lblReservationID;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnDebtPayment;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label lblMoneyForCreditorOrDebtor;
        private System.Windows.Forms.Label lblToSystemOrToCustomer;
        private System.Windows.Forms.Label label1;
    }
}