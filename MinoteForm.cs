/*
Minote - A meeting minutes notes application
Copyright (C) 2011  Mirozell

Website: https://github.com/Mirozell/Minote

Minote is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

Minote is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with Minote.  If not, see <http://www.gnu.org/licenses/>.
*/

using System;
using System.IO;
using System.Windows.Forms;

namespace Minote
{
    public partial class MinoteForm : Form
    {
        private int lastminute = -1;

        public MinoteForm()
        {
            InitializeComponent();
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            if (now.Minute != lastminute)
            {
                string timestamp = string.Format(
                    "[{0}]\n",
                    now.ToShortTimeString());
                Log.AppendText(timestamp);
                File.AppendAllText(OpenFile.Text, timestamp);
            }

            Log.AppendText(Entry.Text + "\n");
            File.AppendAllText(OpenFile.Text, Entry.Text + "\n");
            Entry.Text = string.Empty;

            Log.ScrollToCaret();

            lastminute = now.Minute;
        }

        private void NewFileButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();
            fd.CheckFileExists = false;

            if (fd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string datestamp = string.Format("[{0}]\n", DateTime.Now.ToShortDateString());
                    File.AppendAllText(fd.FileName, datestamp);
                    OpenFile.Text = fd.FileName;
                    Log.Text = string.Empty;
                    lastminute = -1;

                    Log.AppendText(File.ReadAllText(OpenFile.Text));
                    Log.ScrollToCaret();

                    AddButton.Enabled = true;
                    Entry.Focus();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "Error");
                }
            }
        }
    }
}
