using System;
using System.Linq;
using System.Windows.Forms;
using BiometricService;

namespace TimeAttendanceApp
{
    public partial class FrmFingerPrints : Form
    {
        private readonly BindingSource _bindingSource = new BindingSource();
        private readonly Service _service;
        public FrmFingerPrints(Service service)
        {
            InitializeComponent();

            _service = service;

            cboDevices.DataSource = service.Devices;
            cboDevices.ValueMember = "Id";
            cboDevices.DisplayMember = "Name";


            service.GetUsersFromDb();
            var usersInfo = service.UsersInfo;

            foreach (var userInfo in usersInfo)
            {
                _bindingSource.Add(new User(userInfo));
            }

            dataGridViewFP.AutoGenerateColumns = false;
            dataGridViewFP.AutoSize = false;
            dataGridViewFP.DataSource = _bindingSource;
        }

        private void FrmFingerPrints_Load(object sender, EventArgs e)
        {

        }

        public void Work()
        {
            cboDevices.Enabled = dataGridViewFP.Enabled = false;
        }

        public void End()
        {
            cboDevices.Enabled = dataGridViewFP.Enabled = true;
        }

        private void dataGridViewFP_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dataGridViewFP.IsCurrentCellDirty &&
                dataGridViewFP.CurrentCell is DataGridViewCheckBoxCell)
            {
                var current = dataGridViewFP.CurrentCell;
                var enrollName = dataGridViewFP.Rows[current.RowIndex].Cells[0].Value.ToString();
                var finger = current.ColumnIndex - 2;
                var value = (bool)dataGridViewFP.Rows[current.RowIndex].Cells[current.ColumnIndex].Value;

                if (value)
                {
                    if (MessageBox.Show(@"¿Desea eliminar la huella digital capturada?", "", MessageBoxButtons.YesNo) ==
                        DialogResult.No)
                    {
                        dataGridViewFP.CancelEdit();
                        
                    }
                    dataGridViewFP.CommitEdit(DataGridViewDataErrorContexts.Commit);
                    _service.UpdateUserFingerPrint(enrollName, finger, null, Service.FingerPrintOperation.Delete);
                }
                else
                {
                    var deviceId = long.Parse(cboDevices.SelectedValue.ToString());

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
                            dataGridViewFP.CancelEdit();
                            return;
                        }
                        dataGridViewFP.CommitEdit(DataGridViewDataErrorContexts.Commit);
                        _service.UpdateUserFingerPrint(enrollName, args.Finger, args.FingerPrint, Service.FingerPrintOperation.Update);
                    };

                    _service.Enroll(deviceId, enrollName, finger);
                }
            }
        }
    }

    public class User
    {
        public User() { }
        public User(UserInfo userInfo)
        {
            Id = long.Parse(userInfo.EnrollNumber);
            Name = userInfo.Name;
            if (userInfo.FingerPrints == null) return;
            Fp0 = userInfo.FingerPrints.Any(fp => fp.Finger == 0);
            Fp1 = userInfo.FingerPrints.Any(fp => fp.Finger == 1);
            Fp2 = userInfo.FingerPrints.Any(fp => fp.Finger == 2);
            Fp3 = userInfo.FingerPrints.Any(fp => fp.Finger == 3);
            Fp4 = userInfo.FingerPrints.Any(fp => fp.Finger == 4);
            Fp5 = userInfo.FingerPrints.Any(fp => fp.Finger == 5);
            Fp6 = userInfo.FingerPrints.Any(fp => fp.Finger == 6);
            Fp7 = userInfo.FingerPrints.Any(fp => fp.Finger == 7);
            Fp8 = userInfo.FingerPrints.Any(fp => fp.Finger == 8);
            Fp9 = userInfo.FingerPrints.Any(fp => fp.Finger == 9);
        }
        public long Id { get; set; }
        public string Name { get; set; }
        public bool Fp0 { get; set; }
        public bool Fp1 { get; set; }
        public bool Fp2 { get; set; }
        public bool Fp3 { get; set; }
        public bool Fp4 { get; set; }
        public bool Fp5 { get; set; }
        public bool Fp6 { get; set; }
        public bool Fp7 { get; set; }
        public bool Fp8 { get; set; }
        public bool Fp9 { get; set; }
    }
}
