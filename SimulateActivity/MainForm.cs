using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace SimulateActivity
{
    public partial class MainForm : Form
    {
        private readonly PreventLockScreen preventLockScreen;
        private readonly System.Windows.Forms.Timer timer;
        private readonly int timerInterval = 400;

        public MainForm()
        {
            InitializeComponent();

            this.preventLockScreen = new PreventLockScreen();

            this.FormClosing += MainForm_FormClosing;

            this.timer = new System.Windows.Forms.Timer();
            this.timer.Tick += Timer_Tick;
            this.timer.Interval = this.timerInterval;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if(this.labelStatus.ForeColor == System.Drawing.Color.Red)
                this.labelStatus.ForeColor = System.Drawing.Color.Yellow;
            else
                this.labelStatus.ForeColor = System.Drawing.Color.Red;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.preventLockScreen.Dispose();
        }

        private void ButtonStartStop_Click(object sender, EventArgs e)
        {
            if (this.preventLockScreen.Enabled)
                this.EnableLockScreen();   
            else
                this.DisableLockScreen();
        }

        private void DisableLockScreen()
        {   
            this.preventLockScreen.DisableLockScreen();

            this.buttonStartStop.Text = "Stop";
            this.labelStatus.Text = "Enabled";
            this.labelStatus.ForeColor = System.Drawing.Color.Red;
            this.labelInfo.Text = "Click the 'Stop' button to enable the system to enter sleep mode or to lock screen";

            this.timer.Start();
        }

        private void EnableLockScreen()
        {            
            this.preventLockScreen.EnableLockScreen();

            this.buttonStartStop.Text = "Start";
            this.labelStatus.Text = "Disabled";
            this.labelStatus.ForeColor = System.Drawing.Color.Black;
            this.labelInfo.Text = "Click the 'Start' button to prevent the system from entering sleep mode or to lock screen";

            this.timer.Stop();
        }
    }
}
