using System.Windows.Forms;
using System.Collections.Specialized;
using System.Text;
using BiometricService;

namespace FingerprintEnrollment
{
    public partial class frmMain : Form
    {
        private readonly Service _service = new Service();
        private readonly NameValueCollection _args;
        private long _imputed, _user;

        public frmMain(NameValueCollection args)
        {
            _args = args;
            InitializeComponent();

            cboDevices.DataSource = _service.Devices;
            cboDevices.ValueMember = "Id";
            cboDevices.DisplayMember = "Name";

            var i = _args.Get("a");
            var u = _args.Get("b");

            long.TryParse(i, out _imputed);
            long.TryParse(u, out _user);


            _imputed = 1;
            _user = 4;
            
            _service.GetImputedFromDb(_imputed);

            lblName.Text = _service.ImputedInfo.Name;


            foreach (var fingerPrint in _service.ImputedInfo.FingerPrints)
            {
                var chk = ((CheckBox)Controls["chk" + Encoding.ASCII.GetString(new[] { (byte)(65 + fingerPrint.Finger) })]);
                CheckBoxState(chk, true);
            }
        }

        private void CheckBoxState(CheckBox chk, bool state)
        {
            chk.CheckedChanged -= chk_CheckedChanged;
            chk.Checked = state;
            chk.CheckedChanged += chk_CheckedChanged;
        }

        private void chk_CheckedChanged(object sender, System.EventArgs e)
        {
            var chk = ((CheckBox)sender);
            var enrollName = _imputed.ToString();
            var finger = chk.Name[3] - "A"[0];
            var value = !chk.Checked;
            var deviceId = long.Parse(cboDevices.SelectedValue.ToString());

            if (value)
            {
                if (MessageBox.Show(@"¿Desea eliminar la huella digital capturada?", "", MessageBoxButtons.YesNo) ==
                    DialogResult.No)
                {
                    CheckBoxState(chk, false);
                }
                _service.UpdateImputedFingerPrint(_user, enrollName, finger, null, Service.FingerPrintOperation.Delete, deviceId);
            }
            else
            {
                

                var frm = new FrmEnroll();
                frm.Show(this);
                Work();

                _service.ClearEvents();
                _service.EnrollCompleted += (o, args) =>
                {
                    frm.Close();
                    End();
                    if (!args.Result)
                    {
                        CheckBoxState(chk, false);
                        return;
                    }

                    _service.UpdateImputedFingerPrint(_user, enrollName, args.Finger, args.FingerPrint, Service.FingerPrintOperation.Update, deviceId);
                };

                _service.Enroll(deviceId, enrollName, finger);
            }
        }

        public void Work()
        {
            cboDevices.Enabled = false;
        }

        public void End()
        {
            cboDevices.Enabled = true;
        }
    }
}
