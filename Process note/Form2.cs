using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
namespace Process_note
{
    public partial class Form2 : MetroFramework.Forms.MetroForm
    {
        /*
        PerformanceCounter perfCPUcounter = new PerformanceCounter("Processor Information", "% Processor Time", "_Total");
        PerformanceCounter perfMEMcounter = new PerformanceCounter("Memory", "Available MBytes");
        Process ckurrentProcess = Process.GetCurrentProcess();
        */
        public Form2()
        {
            InitializeComponent();

        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void getAllProcess()
        {
            float fcpu = pCPU.NextValue();
            float fram = pRAM.NextValue();
            label1.Text = string.Format("{0:0.00}%", fcpu);
            label2.Text = string.Format("{0:0.00}%", fram);
            metroProgressBar1.Value = (int)fcpu;
            metroProgressBar2.Value = (int)fcpu;
        }

     
        private void timer1_Tick(object sender, EventArgs e)
        {
            getAllProcess();
  
        }

       


        private void Form2_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }


        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void metroProgressBar1_Click(object sender, EventArgs e)
        {

        }

        private void metroProgressBar2_Click(object sender, EventArgs e)
        {

        }
    }
}
