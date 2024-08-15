using MenuGenerator.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MenuGenerator.Appearance
{
    public partial class AlertForm : Form
    {
        public AlertForm()
        {
            InitializeComponent();
        }

        public enum eAction 
        { 
            wait,
            start,
            close
        }

        public enum eType
        {
            Success,
            Warning,
            Error,
            Info
        }

        //Variables.
        private eAction action;
        private int _waitInterval;

        //Methods.
        public void ShowAlert(string msg, eType type) 
        {
            this.Opacity = 0.0;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.label1.Text = msg;
            this.Show();
            this.Top += 50;

            this.action = eAction.start;
            this.timer1.Interval = 1;
            this.timer1.Start();

            switch (type)
            {
                case eType.Success:
                    this.pctLogo.Image = Resources.successLogo;
                    this.BackColor = Color.ForestGreen;
                    btnExit.FlatAppearance.MouseOverBackColor = Color.DarkGreen;
                    this._waitInterval = 1500;
                    break;
                case eType.Warning:
                    this.pctLogo.Image = Resources.errorLogo;
                    this.BackColor = Color.Sienna;
                    btnExit.FlatAppearance.MouseOverBackColor = Color.SaddleBrown;
                    this._waitInterval = 1500;
                    break;
                case eType.Error:
                    this.pctLogo.Image = Resources.errorLogo;
                    this.BackColor = Color.Maroon;
                    btnExit.FlatAppearance.MouseOverBackColor = Color.DarkRed;
                    this._waitInterval = 4000;
                    break;
                case eType.Info:
                    this.pctLogo.Image = Resources.errorLogo;
                    this.BackColor = Color.RoyalBlue;
                    btnExit.FlatAppearance.MouseOverBackColor = Color.DarkBlue;
                    this._waitInterval = 5000;
                    break;
            }

        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            timer1.Interval = 1;
            action = eAction.close;
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            switch (this.action)
            {
                case eAction.wait:
                    timer1.Interval = _waitInterval;
                    action = eAction.close;
                    break;

                case eAction.start:
                    timer1.Interval = 1;
                    this.Opacity += 0.1;
                    this.Top -= 3;

                    if (this.Opacity == 1.0)
                    {
                        action = eAction.wait;
                    }
                    break;

                case eAction.close:
                    timer1.Interval = 1;
                    this.Opacity -= 0.1;
                    this.Top -= 3;
                    if (base.Opacity == 0.0)
                    {
                        base.Close();

                    }
                    break;
            }
        }

    }
}
