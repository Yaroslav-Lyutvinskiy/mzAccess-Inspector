/*******************************************************************************
  Copyright 2015-2019 Yaroslav Lyutvinskiy <Yaroslav.Lyutvinskiy@ki.se> and 
  Roland Nilsson <Roland.Nilsson@ki.se>
 
  Licensed under the Apache License, Version 2.0 (the "License");
  you may not use this file except in compliance with the License.
  You may obtain a copy of the License at

  http://www.apache.org/licenses/LICENSE-2.0

  Unless required by applicable law or agreed to in writing, software
  distributed under the License is distributed on an "AS IS" BASIS,
  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
  See the License for the specific language governing permissions and
  limitations under the License.
 
 *******************************************************************************/

 using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Inspector {
    public partial class ProgressForm : Form {
        public ProgressForm() {
            InitializeComponent();
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e) {
            progressBar1.Value = e.ProgressPercentage;
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e) {
            (this.Owner as MetaRepForm).CancelSignal = true;
        }

        private void ProgressForm_FormClosed(object sender, FormClosedEventArgs e) {
            this.Owner.Enabled = true; 
        }

        private void ProgressForm_Load(object sender, EventArgs e) {
            this.Owner.Enabled = false; 
        }
    }
}
