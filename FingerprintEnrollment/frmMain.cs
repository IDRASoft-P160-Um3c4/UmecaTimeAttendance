using System;
using System.Windows.Forms;
using System.Collections.Specialized;
using BiometricService;

namespace FingerprintEnrollment
{
    public partial class frmMain : Form
    {
        private readonly Service _service = new Service();
        private readonly NameValueCollection _args;
        public frmMain(NameValueCollection args)
        {
            _args = args;
            InitializeComponent();

            cboDevices.DataSource = _service.Devices;
            cboDevices.ValueMember = "Id";
            cboDevices.DisplayMember = "Name";

        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            var keys = string.Join(",", _args.AllKeys);
            
            MessageBox.Show(this, keys);
        }
    }
}
