using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace Process_note
{
    public partial class Form3 : MetroFramework.Forms.MetroForm
    {
        //private Process currentProcess;
        PerformanceCounter pref = new PerformanceCounter("Processor", "% Privileged Time", "_Total");

        public Form3(Process currentProcess)
        {
            InitializeComponent();
            loadProgramData(currentProcess);
        }
        private void loadProgramData(Process currentProcess)
        {
            DateTime startingTime = currentProcess.StartTime;
            metroTextBox1.Text = startingTime.ToString("dd/mm/yyyy hh:mm:ss");
            TimeSpan runningTime = DateTime.Now.Subtract(startingTime);
            metroTextBox2.Text = runningTime.ToString("c").Split('.')[0];
            metroTextBox3.Text = currentProcess.Threads.Count.ToString();

        }
        private void getAllProcess()
        {
            float fcpu = pCPU.NextValue();
            float fram = pRAM.NextValue();
            label3.Text = string.Format("{0:0.00}%", fcpu);
            label4.Text = string.Format("{0:0.00}%", fram);
            metroProgressBar1.Value = (int)fcpu;
            metroProgressBar2.Value = (int)fcpu;
        }
        
        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            getAllProcess();
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "XML-File | *.xml";
            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                File.WriteAllText(saveFileDialog1.FileName, textBox1.Text);
            }
            string fileName = saveFileDialog1.FileName;

            XmlTextWriter xmlText = new XmlTextWriter(fileName, System.Text.Encoding.UTF8);
            xmlText.Formatting = Formatting.Indented;
            xmlText.WriteStartDocument();
            xmlText.WriteStartElement("xmlFile");

            xmlText.WriteStartElement("Title");
            xmlText.WriteString(Form3.ActiveForm.Text);
            xmlText.WriteEndElement();

            xmlText.WriteStartElement("Items");
            xmlText.WriteStartElement("Start time");
            xmlText.WriteString(metroTextBox1.Text);
            xmlText.WriteEndElement();

            xmlText.WriteStartElement("Running time");
            xmlText.WriteString(metroTextBox2.Text);
            xmlText.WriteEndElement();

            xmlText.WriteStartElement("Threads");
            xmlText.WriteString(metroTextBox3.Text);
            xmlText.WriteEndElement();


            xmlText.WriteStartElement("Comments");
            xmlText.WriteString(textBox1.Text);
            xmlText.WriteEndElement();

            xmlText.WriteStartElement("CPU usage");
            xmlText.WriteString(metroProgressBar1.Text);
            xmlText.WriteEndElement();
            xmlText.WriteStartElement("Memory usage");
            xmlText.WriteString(metroProgressBar2.Text.ToString());
            xmlText.WriteEndElement();
            xmlText.WriteEndDocument();
            xmlText.Close();
            
            this.textBox1.Clear();
       
            this.Refresh();



        }

        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(!string.IsNullOrEmpty(textBox1.Text))
            {
                DialogResult dialog = MessageBox.Show("Do you want quit whithout saving?", "Exit", MessageBoxButtons.YesNo );
                if(dialog == DialogResult.Yes)
                {
                    Application.Exit();
                }
                else if(dialog == DialogResult.No)
                {
                    e.Cancel = true;
                }
               
            }
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            this.Refresh();
            Application.DoEvents();
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if(textBox1.Text == "")
            {
                textBox1.Text = "Comments";
                textBox1.ForeColor = Color.Gray;
            }
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            if(textBox1.Text == "Comments")
            {
                textBox1.Text = "";
                textBox1.ForeColor = Color.Black;
            }
        }
    }
}
