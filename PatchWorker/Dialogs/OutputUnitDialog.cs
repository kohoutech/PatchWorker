﻿/* ----------------------------------------------------------------------------
Patchworker : a midi patchbay
Copyright (C) 1995-2019  George E Greaney

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
    public partial class OutputUnitDialog : Form
    {
        PatchWindow patchWnd;

        List<String> channelNums;
        public String name;
        public String devName;
        public int chanNum;
        public int progCount;

        //for creating new output units
        public OutputUnitDialog(PatchWindow _patchWnd)
        {
            patchWnd = _patchWnd;

            InitializeComponent();

            channelNums = new List<string>() { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16" };

            cbxDevice.DataSource = patchWnd.midiSystem.getOutDevNameList();
            cbxDevice.SelectedIndex = -1;
            cbxChannel.DataSource = channelNums;
            cbxChannel.SelectedIndex = -1;

            name = null;
            devName = null;
            chanNum = -1;
            progCount = 0;
        }

        //for updating existing output units
        public OutputUnitDialog(PatchWindow _patchWnd, String _name, String _devName, int _chanNum, int _progNum)
            : this(_patchWnd)
        {
            txtName.Text = _name;
            cbxDevice.SelectedIndex = -1;

            //set current device in drop list list
            for (int i = 0; i < cbxDevice.Items.Count; i++)
            {
                String item = (String)cbxDevice.Items[i];
                if (item.Equals(_devName))
                {
                    cbxDevice.SelectedIndex = i;
                    break;
                }
            }
            cbxChannel.SelectedIndex = _chanNum - 1;
            txtProgCount.Text = _progNum.ToString();
        }

        //- button methods ------------------------------------------------------------

        //attached to dialog's control to validate when they loose focus
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
            devName = patchWnd.midiSystem.outputDevices[cbxDevice.SelectedIndex].devName;
            chanNum = cbxChannel.SelectedIndex + 1;

            try
            {
                progCount = Int32.Parse(txtProgCount.Text);
            }
            catch (Exception ex)
            {
                progCount = 0;
            }

            DialogResult = DialogResult.OK;
        }
    }
}
