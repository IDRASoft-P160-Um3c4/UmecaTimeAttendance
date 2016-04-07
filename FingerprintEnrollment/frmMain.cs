using System.Windows.Forms;
using System.Collections.Specialized;
using System.Linq;
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

            cboDevices.DataSource = _service.Devices(DeviceType.Imputed);
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

            var device = _service.Devices(DeviceType.Imputed).First(d => d.id == deviceId);

            if (!device.IsAlive())
            {
                CheckBoxState(chk, value);
                MessageBox.Show(this,
                    string.Format(@"El dispositivo biométrico ""{0}"" no se encuentra disponible o no es accesible",
                        device.name), @"Enrolamiento de Imputados", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            if (value)
            {
                if (MessageBox.Show(@"¿Desea eliminar la huella digital capturada?", @"Enrolamiento de Imputados", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) ==
                    DialogResult.No)
                {
                    CheckBoxState(chk, true);
                    return;
                }
                _service.UpdateImputedFingerPrint(_user, enrollName, finger, null, Service.FingerPrintOperation.Delete);
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

                    _service.UpdateImputedFingerPrint(_user, enrollName, args.Finger, args.FingerPrint, Service.FingerPrintOperation.Update);
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