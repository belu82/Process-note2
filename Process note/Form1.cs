using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Process_note
{
    public partial class Form1 : Form
    {
        Process[] process;
        public Form1()
        {
            process = Process.GetProcesses();
            InitializeComponent();
            loadProcesses();
            this.Load += new EventHandler(Form1_Load);

        }

        private void loadProcesses()
        {
            timer1.Start();
            IEnumerable<Process> allProcesses = Process.GetProcesses().OrderBy(Process => Process.ProcessName);

            listView1.Items.Clear();

            foreach (Process process in allProcesses)
            {
                if (process != null && process.Id > 0)
                {
                    try
                    {

                        DateTime dateTime = process.StartTime;
                        ListViewItem item = new ListViewItem(process.Id.ToString());
                        item.SubItems.Add(process.ProcessName);
                        listView1.Items.Add(item);
                    }
                    catch (UnauthorizedAccessException)
                    {
                        continue;
                    }
                    catch (Win32Exception)
                    {
                        continue;
                    }
                    catch (InvalidOperationException)
                    {
                        continue;
                    }
                }
            
        }
        }

      
       
        private void listView1_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
          
        }

        private void button2_Click(object sender, EventArgs e)
        {
            listView1.Select();

            // Create a new instance of the Form2 class
            Form2 settingsForm = new Form2();
            settingsForm.Text = listView1.SelectedItems[0].Text.ToString();

            // Show the settings form
            settingsForm.Show();

            //string processName = listView1.SelectedItems[0].Text.ToString();



        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listView1.SelectedItems.Count == 1)
            {
                Process currentProcess = process[listView1.SelectedIndices[0]];
                Form3 settingsForm = new Form3(currentProcess);
                settingsForm.Text = listView1.SelectedItems[0].Text.ToString();

                // Show the settings form
                settingsForm.Show();
            }

        }
    }
}
