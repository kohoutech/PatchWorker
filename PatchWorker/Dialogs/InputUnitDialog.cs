/* ----------------------------------------------------------------------------
Patchworker : a midi patchbay
Copyright (C) 2005-2017  George E Greaney

This program is free software; you can redistribute it and/or
modify it under the terms of the GNU General Public License
as published by the Free Software Foundation; either version 2
of the License, or (at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program; if not, write to the Free Software
Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.
----------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PatchWorker.Graph;

namespace PatchWorker.Dialogs
{
    public partial class InputUnitDialog : Form
    {
        public PatchWorker patchworker;
        List<String> channelNums;

        public String name;
        public String devName;
        public int chanNum;

        public InputUnitDialog(PatchWorker _patchworker)
        {
            patchworker = _patchworker;

            InitializeComponent();

            channelNums = new List<string>() { "1", "2", "3", "4", "5", "6", "7", "8", "9", 
                "10", "11", "12", "13", "14", "15", "16" };

            cbxDevice.DataSource = patchworker.midiSystem.getInDevNameList();
            cbxDevice.SelectedIndex = -1;
            cbxChannel.DataSource = channelNums;
            cbxChannel.SelectedIndex = -1;

            name = null;
            devName = null;
            chanNum = -1;
        }

//- button methods ------------------------------------------------------------

        private void validateControlData(object sender, EventArgs e)
        {
            bool passed = !(txtName.Text.Equals(""));
            passed = passed && !(cbxDevice.SelectedIndex < 0);
            passed = passed && !(cbxChannel.SelectedIndex < 0);
            btnOK.Enabled = passed;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            name = txtName.Text;
            devName = patchworker.midiSystem.inputDevices[cbxDevice.SelectedIndex].devName;
            chanNum = cbxChannel.SelectedIndex + 1;
            DialogResult = DialogResult.OK;
        }
    }
}
