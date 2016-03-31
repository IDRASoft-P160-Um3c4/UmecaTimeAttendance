using System;
using System.Deployment.Application;
using System.Diagnostics;
using System.Windows.Forms;

namespace TimeAttendanceApp
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            if (ApplicationDeployment.IsNetworkDeployed && ApplicationDeployment.CurrentDeployment.IsFirstRun)
            {
                var startupPath = ApplicationDeployment.IsNetworkDeployed ? ApplicationDeployment.CurrentDeployment.DataDirectory : Application.StartupPath;

                var processStartInfo = new ProcessStartInfo
                {
                    FileName = Application.StartupPath + @"\sdk\Register_SDK.bat",
                    Verb = "runas",
                    WindowStyle = ProcessWindowStyle.Normal,
                    UseShellExecute = true
                };
                Process.Start(processStartInfo);



            }


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FrmMain());
        }
    }
}
