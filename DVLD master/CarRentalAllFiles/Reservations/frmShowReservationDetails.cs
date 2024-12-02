using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD2.CarRentalAllFiles.Reservations
{
    public partial class frmShowReservationDetails : Form
    {
        public frmShowReservationDetails(int reservationId)
        {
            InitializeComponent();
            ctrlReservationVehicleCard1.LoadInfo(reservationId);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();   
        }
    }
}
