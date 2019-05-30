/* ----------------------------------------------------------------------------
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
    public partial class PatchCordDialog : Form
    {
        public PatchCord patchCord;
        public int transpose;
        public int loRange;
        public int hiRange;

        int prevTranspose;
        int prevLoRange;
        int prevHiRange;

        public PatchCordDialog(PatchCord _patchCord)
        {
            patchCord = _patchCord;
            InitializeComponent();
            keysRange.selectedColor = Color.FromArgb(0xFF, 0x80, 0x00);

            List<String> octaveNums = new List<string>() { "+5", "+4", "+3", "+2", "+1", "0", "-1", "-2", "-3", "-4", "-5"};
            List<String> stepNums = new List<string>() { "B", "A#", "A", "G#", "G", "F#", "F", "E", "D#", "D", "C#", "C" };                
            List<String> keyNums = new List<string>() { "25", "37", "49", "61", "76", "88", "128" };

            transpose = 0;
            loRange = 0;
            hiRange = 127;

            cbxOctave.DataSource = octaveNums;
            cbxOctave.SelectedIndex = 5;
            cbxStep.DataSource = stepNums;
            cbxStep.SelectedIndex = 11;
            cbxKeySize.DataSource = keyNums;
            cbxKeySize.SelectedIndex = 5;            
        }

        public void setTitle(String srcName, String destName) 
        {
            this.Text = "Key Range / Transpose [" + srcName + " --> " + destName + "]";
        }

        public void initDialogValues(int _transpose, int _loRange, int _hiRange) 
        {
            prevTranspose = transpose;
            prevLoRange = loRange;
            prevHiRange = hiRange;

            transpose = _transpose;
            loRange = _loRange;
            hiRange = _hiRange;

            int octave = transpose / 12;
            if (transpose < 0) octave = ((transpose + 1) / 12) - 1;
            cbxOctave.SelectedIndex = 5 - octave;
            int keynum = transpose - (octave * 12);
            cbxStep.SelectedIndex = 11 - keynum;

            if ((loRange > 0) && (hiRange < 127))
            {
                keysRange.setKeyRange(loRange, hiRange);
            }
        }

        //retsore settings back to initial values
        private void btnCancel_Click(object sender, EventArgs e)
        {
            transpose = prevTranspose;
            loRange = prevLoRange;
            hiRange = prevHiRange;
            patchCord.setPatchCordValues(this);
            this.Close();
        }

        public void applySettings()
        {
            transpose = ((5 - cbxOctave.SelectedIndex) * 12) + (11 - cbxStep.SelectedIndex);
            List<int> range = keysRange.getKeyRange();
            if (range.Count > 0)
            {
                loRange = range[0];
                hiRange = range[range.Count - 1];
            }
            else
            {
                loRange = 0;            //if no keys selected, turn filter off
                hiRange = 127;
            }
            patchCord.setPatchCordValues(this);
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            applySettings();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            applySettings();
            this.Close();
        }

    }
}
