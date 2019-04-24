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
using System.Linq;
using System.Windows.Forms;
using System.IO;

namespace Inspector {
    public partial class RestoreForm : Form {
        public RestoreForm() {
            InitializeComponent();
        }

        string FileChoosen = "";
        List<string> TFiles = null;

        public static string ChooseSession() {
            string TempPath = Path.GetTempPath();
            List<string> FList = Directory.EnumerateFiles(TempPath,"*.mzAccInspector").ToList();
            if(!FList.Any())
                return null;
            RestoreForm RF = new RestoreForm();
            RF.TFiles = FList;
            foreach(string F in RF.TFiles) {
                StreamReader sr = new StreamReader(F);
                string Str = ""; 
                while(!sr.EndOfStream) {
                    Str = sr.ReadLine();
                }
                RF.listBox1.Items.Add(Str);
                sr.Close();
                sr.Dispose();
            }
            RF.listBox1.SelectedIndex = 0;
            if (RF.TFiles.Count > 1) {
                RF.label1.Text = "Unsaved session has been found:";
            }else {
                RF.label1.Text = "A number of unsaved sessions has been found, please choose one:";
            }
            RF.ShowDialog();
            return RF.FileChoosen;
        }

        private void button1_Click(object sender, EventArgs e) {
            FileChoosen = TFiles[listBox1.SelectedIndex];
            Close();
        }

        private void button2_Click(object sender, EventArgs e) {
            FileChoosen = null;
            Close();
        }

        private void button3_Click(object sender, EventArgs e) {
            foreach(string F in TFiles) {
                File.Delete(F);
            }
            FileChoosen = null;
            Close();
        }
    }
}
