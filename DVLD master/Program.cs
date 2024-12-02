using DVLD2.Applications.Renew_Local_License;
using DVLD2.Applications.ReplaceLostOrDamagedLicense;
using DVLD2.Applications.Rlease_Detained_License;
using DVLD2.CarRentalAllFiles;
using DVLD2.CarRentalAllFiles.Customers.Controls;
using DVLD2.CarRentalAllFiles.GlobalForm;
using DVLD2.CarRentalAllFiles.Maintenance;
using DVLD2.CarRentalAllFiles.Reservations;
using DVLD2.CarRentalAllFiles.VehicleReturns;
using DVLD2.CarRentalAllFiles.Vehicles;
using DVLD2.Drivers;
using DVLD2.Licenses.LocalLicenses;
using DVLD2.Main_Form;
using DVLD2.People;
using DVLD2.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD2
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmLogin());
        }
    }
}
