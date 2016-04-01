using System;
using System.Collections.Specialized;
using System.Deployment.Application;
using System.Windows.Forms;
using System.Web;
using BiometricService;

namespace FingerprintEnrollment
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (string.IsNullOrEmpty(Service.GetRemoteAddressWs()))
            {
                MessageBox.Show(null, @"No se ha configurado el servicio, ir a la aplicación TimeAttendance para realizarlo.", @"Enrolamiento de Imputados", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmMain(GetQueryStringParameters()));
        }

        private static NameValueCollection GetQueryStringParameters()
        {
            var col = new NameValueCollection();
            if (!ApplicationDeployment.IsNetworkDeployed) return col;
            var queryString = ApplicationDeployment.CurrentDeployment.ActivationUri?.Query;
            if (queryString != null) col = HttpUtility.ParseQueryString(queryString);
            return col;
        }
    }
}
