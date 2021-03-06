﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;
using BiometricService;

namespace TimeAttendanceApp
{
    public partial class FrmMain : Form
    {

        private readonly Service _service = new Service();
        private void BeginWork()
        {
            btnWsAddress.Enabled = btnSyncImputedUsers.Enabled = btnImputed.Enabled = btnSyncUsers.Enabled = btnEnroll.Enabled = btnAssistence.Enabled = false;
            Cursor = Cursors.WaitCursor;
        }

        private void EndWork()
        {
            btnWsAddress.Enabled = btnSyncImputedUsers.Enabled = btnImputed.Enabled = btnSyncUsers.Enabled = btnEnroll.Enabled = btnAssistence.Enabled = true;
            Cursor = Cursors.Default;
        }

        public FrmMain()
        {
            InitializeComponent();
        }

        private void btnSyncUsers_Click(object sender, EventArgs e)
        {
            try
            {

                var address = Service.GetRemoteAddressWs();
                if (address == null || address.Equals(""))
                {
                    MessageBox.Show(Constants.WS_MSG_ERROR, @"Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }

                BeginWork();
                _service.UpdateUsers();
            }
            finally
            {
                EndWork();
            }
        }

        private void btnAssistence_Click(object sender, EventArgs e)
        {
            try
            {
                var address = Service.GetRemoteAddressWs();
                if (address == null || address.Equals(""))
                {
                    MessageBox.Show(Constants.WS_MSG_ERROR, @"Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }
                BeginWork();
                _service.GetAttendanceLog();
            }
            finally
            {
                EndWork();
            }
        }

        // ReSharper disable once UnusedMember.Local
        private readonly IDictionary<int, string> _messageError = new Dictionary<int, string>
        {
            {-100, "Operation failed or data not exist"},
            {-10, "Transmitted data length is incorrect"},
            {-5, "Data already exists"},
            {-4, "Space is not enough"},
            {-3, "Error size"},
            {-2, "Error in file read/write"},
            {-1, "SDK is not initialized and needs to be reconnected"},
            {0, "Data not found or data repeated"},
            {1, "Operation is correct"},
            {4, "Parameter is incorrect"},
            {101, " Error in allocating buffer"},
        };

        private void btnEnroll_Click(object sender, EventArgs e)
        {
            try
            {
                var address = Service.GetRemoteAddressWs();
                if (address == null || address.Equals(""))
                {
                    MessageBox.Show(Constants.WS_MSG_ERROR, @"Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }
                BeginWork();

                var frmFingerPrints = new FrmFingerPrints(_service);
                frmFingerPrints.ShowDialog(this);
            }
            finally
            {
                EndWork();
            }
        }


        private void btnWsAddress_Click(object sender, EventArgs e)
        {
            try
            {
                BeginWork();

                var frmWsAddress = new FrmWsAddress(_service);
                frmWsAddress.ShowDialog(this);
            }
            finally
            {
                EndWork();
            }
        }

        private void btnImputed_Click(object sender, EventArgs e)
        {
            try
            {
                var address = Service.GetRemoteAddressWs();
                if (address == null || address.Equals(""))
                {
                    MessageBox.Show(Constants.WS_MSG_ERROR, @"Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }
                BeginWork();
                _service.GetImputedLog();
            }
            finally
            {
                EndWork();
            }
        }

        private void btnSyncImputedUsers_Click(object sender, EventArgs e)
        {
            try
            {

                var address = Service.GetRemoteAddressWs();
                if (address == null || address.Equals(""))
                {
                    MessageBox.Show(Constants.WS_MSG_ERROR, @"Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }

                BeginWork();
                _service.UpdateImputedUsers();
            }
            finally
            {
                EndWork();
            }
        }
    }
}
