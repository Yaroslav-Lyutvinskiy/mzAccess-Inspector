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
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Inspector {
    public partial class mzAccessForm : Form {
        public mzAccessForm() {
            InitializeComponent();
        }

        public List<string> FileList = new List<string>();

        private void button1_Click(object sender, EventArgs e) {
            textBox2.Text = "";
            /*try{ 
                //check on http level somehow leads to degradation of connection 
                //it works for a first time, does not work at second time at WebService level, and does not work at third time at http level 
                HttpWebRequest Request = (HttpWebRequest)WebRequest.Create(textBox1.Text);
                HttpWebResponse response = (HttpWebResponse)Request.GetResponse();
                if (response.StatusCode != HttpStatusCode.OK){
                    textBox2.Text = String.Format("{0} Returned, but with status: {1}", textBox1.Text, response.StatusDescription);
                    button1.ForeColor = Color.DarkRed;
                    this.DialogResult = DialogResult.None;
                    return;
                }
            }
            catch (Exception ex){
                //  not available at all, for some reason
                textBox2.Text = String.Format("{0} unavailable: {1}", textBox1.Text, ex.Message);
                button1.ForeColor = Color.DarkRed;
                this.DialogResult = DialogResult.None;
                return;
            }*/

            mzAccess_Service.MSDataService TestService = new mzAccess_Service.MSDataService();
            TestService.Url = textBox1.Text;
            TestService.Timeout = 5000;
            string ErrorMessage;
            try {
                string[] FileList = TestService.FileList("*", out ErrorMessage);
                if(ErrorMessage != null) {
                    textBox2.Text = String.Format("{0} function call returned error message: {1}", textBox1.Text, ErrorMessage);
                    this.DialogResult = DialogResult.None;
                    button1.ForeColor = Color.DarkRed;
                    return;
                }
                this.FileList = FileList.ToList();
            }catch(Exception ex) {
                textBox2.Text = String.Format("{0} server exception: {1}", textBox1.Text, ex.Message);
                this.DialogResult = DialogResult.None;
                button1.ForeColor = Color.DarkRed;
                return;
            }
            button1.ForeColor = Color.DarkGreen;
            TestService.Dispose();
        }

        private void textBox1_TextChanged(object sender, EventArgs e) {
            button1.ForeColor = Color.Black;
        }
    }
}
