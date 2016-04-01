using System;
using System.Windows.Forms;
using BiometricService;

namespace TimeAttendanceApp
{
    public partial class FrmWsAddress : Form
    {
        private readonly Service _service;

        public FrmWsAddress(Service service)
        {

            _service = service;
            var address = Service.GetRemoteAddressWs();
            InitializeComponent();
            txtBoxAddress.Text = address;
        }


        private void btnSaveRegistry_Click(object sender, EventArgs e)
        {

            var addresss = txtBoxAddress.Text;
            if (addresss == null || addresss.Equals(""))
            {
                MessageBox.Show(Constants.WS_MSG_ERROR, "Error");
                return;
            }
            _service.SetRemoteAddressWs(addresss);
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
