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

namespace PatchWorker.UI
{
    public partial class UnitDataDialog : Form
    {
        public PatchWorker patchworker;
        public UnitData udata;
        List<String> channelNums;
        UNITTYPE umode;

        public UnitDataDialog()
        {
            InitializeComponent();
            channelNums = new List<string>() { "1", "2", "3", "4", "5", "6", "7", "8", "9", 
                "10", "11", "12", "13", "14", "15", "16" };            
        }

        public void setInitialData(UnitData _udata)
        {
            if (_udata != null)
            {
            }
            else
            {
                setInputControls();
            }
        }

        private void setInputControls()
        {
            umode = UNITTYPE.INPUT;
            lblDevice.Enabled = true;
            lblDevice.Text = "Input Device";
            cbxDevice.Enabled = true;
            cbxDevice.DataSource = patchworker.midiSystem.getInDevNameList();

            lblChannel.Enabled = true;
            lblChannel.Text = "Input Channel";
            cbxChannel.Enabled = true;
            cbxChannel.DataSource = channelNums;
            cbxChannel.SelectedIndex = -1;

            lblProgCount.Enabled = false;
            txtProgCount.Enabled = false;
            txtProgCount.Clear();
        }

        private void setModifierControls()
        {
            umode = UNITTYPE.MODIFIER;
            lblDevice.Enabled = false;
            lblDevice.Text = "none";
            cbxDevice.Enabled = false;
            cbxDevice.DataSource = null;

            lblChannel.Enabled = false;
            lblChannel.Text = "none";
            cbxChannel.Enabled = true;
            cbxChannel.DataSource = null;

            lblProgCount.Enabled = false;
            txtProgCount.Enabled = false;
            txtProgCount.Clear();
        }

        private void setOutputControls()
        {
            umode = UNITTYPE.OUTPUT;
            lblDevice.Enabled = true;
            lblDevice.Text = "Output Device";
            cbxDevice.Enabled = true;
            cbxDevice.DataSource = patchworker.midiSystem.getOutDevNameList();

            lblChannel.Enabled = true;
            lblChannel.Text = "Output Channel";
            cbxChannel.Enabled = true;
            cbxChannel.DataSource = channelNums;
            cbxChannel.SelectedIndex = -1;

            lblProgCount.Enabled = true;
            txtProgCount.Enabled = true;
            txtProgCount.Clear();
        }

        private void rbInput_CheckedChanged(object sender, EventArgs e)
        {
            if (rbInput.Checked)
                setInputControls();
        }

        private void rbModifier_CheckedChanged(object sender, EventArgs e)
        {
            if (rbModifier.Checked)
                setModifierControls();
        }

        private void rbOutput_CheckedChanged(object sender, EventArgs e)
        {
            if (rbOutput.Checked)
                setOutputControls();
        }

//- button methods ------------------------------------------------------------
        private void btnCancel_Click(object sender, EventArgs e)
        {

        }

        private void validateControlData(object sender, EventArgs e)
        {
            bool passed = !(txtName.Text.Equals(""));
            if (umode != UNITTYPE.MODIFIER) {
                passed = passed && (cbxDevice.SelectedIndex >= 0);
            }
            if (umode != UNITTYPE.MODIFIER) {
                passed = passed && (cbxChannel.SelectedIndex >= 0);
            }
            btnOK.Enabled = passed;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            String uname = txtName.Text;
            UNITTYPE utype = (rbInput.Checked ? UNITTYPE.INPUT :
                (rbModifier.Checked ? UNITTYPE.MODIFIER : UNITTYPE.OUTPUT));

            udata = new UnitData(uname, utype);
            if (umode != UNITTYPE.MODIFIER)
            {
                udata.devName = (String) cbxDevice.SelectedItem;
                udata.channelNum = cbxChannel.SelectedIndex;
            }
            if (umode == UNITTYPE.OUTPUT)
            {
                try {
                    udata.progCount = Int32.Parse(txtProgCount.Text);
                } catch (Exception ex) {
                    udata.progCount = 0;
                }
            }
        }

    }
}
