using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using BunifuAnimatorNS;
using DocumentFormat.OpenXml.EMMA;
using MenuGenerator.Appearance;
using MenuGenerator.Classes;
using MenuGenerator.Forms.Others;
using MenuGenerator.Properties;
using static MenuGenerator.Appearance.AlertForm;

namespace MenuGenerator.Forms.Mains
{
    public partial class MainForm : Form
    {
        //Globals.
        BackgroundWorker worker;
        private DataTable dtInfo;


        //Transition.
        internal System.Windows.Forms.Timer animationTimer = new System.Windows.Forms.Timer();
        internal Transition transition1;

        //Constructor.
        public MainForm()
        {
            InitializeComponent();
            InicialTheme();
            InicialTransitionTheme();
        }

        //Methods.
        //--Form.
        internal void LoginForm_Load(object sender, EventArgs e)
        {
            btnCloseApp.Focus();
            prgLoading.Visible = false;
            InicialTransitionLoad();
            LoadCentersData();
        }

        //Timer.
        private void animationTimer_Tick(object sender, EventArgs e)
        {
            switch (transition1)
            {
                case Transition.show:
                    base.Show();
                    this.CenterToScreen();

                    timer1.Interval = 1;
                    this.Opacity += 0.4;
                    this.Top -= 3;

                    if (this.Opacity == 1.0)
                    {
                        timer1.Interval = 5000;
                        animationTimer.Stop();
                    }

                    break;
                case Transition.hide:
                    timer1.Interval = 1;
                    this.Opacity -= 0.1;
                    this.Top -= 3;
                    if (base.Opacity == 0.0)
                    {
                        base.Hide();
                    }
                    break;
            }
        }

        //--MouseDown.
        private void pnlPrincipal_MouseDown(object sender, MouseEventArgs e)
        {
            Helpers.ReleaseCapture();
            this.Handle.SendMessage(0x112, 0xf012, 0);
        }

        //--Buttons.
        private void btnConn_Click(object sender, EventArgs e)
        {
            var connForm = (ConnForm)Application.OpenForms["HomeForm"];

            if (connForm == null)
            {
                connForm = new ConnForm();
                connForm.Show();
            }
            else
            {
                connForm.Visible = true;
            }
        }
        private void btnCloseApp_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        private void btnReport_Click(object sender, EventArgs e)
        {
            btnReport.Text = "Procesando...";
            this.Enabled = false;
            StartLoadingBar();
        }

        //Barra de Carga.
        private void StartLoadingBar()
        {
            BunifuTransition transition = new BunifuTransition();
            transition.ShowSync(prgLoading, false, BunifuAnimatorNS.Animation.Transparent);
            prgLoading.animated = true;
            LoadWorked();
        }
        private void LoadWorked()
        {
            worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.ProgressChanged += new ProgressChangedEventHandler(worker_ProgressChanged);
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);
            worker.DoWork += new DoWorkEventHandler(worker_DoWork);
            worker.RunWorkerAsync();
        }
        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            Thread thread = new Thread(() => LoadDataTablesInfo());
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            thread.Join();
        }
        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            prgLoading.Visible = false;
            prgLoading.animated = false;

            dtInfo.GenerateMenuReport();

            var message = "El Menú Fue Genererado Exitosamente.";
            Alert(message, eType.Success);
            this.Enabled = true;
            btnReport.Text = "Generar Menú";

        }
        private void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            prgLoading.Value = e.ProgressPercentage;
        }

        //Privates.
        private void LoadCentersData()
        {
            // Cargando el Combobox de Centros.
            cmbPriceList.DataSource = Helpers.GetcmbCenters();
            cmbPriceList.DisplayMember = "descripcion";
            cmbPriceList.ValueMember = "id";
            cmbPriceList.SelectedIndex = -1;
            cmbPriceList.Select();
        }
        private void InicialTheme()
        {
            this.Text = string.Empty;
            this.ControlBox = false;

            this.Opacity = 0.0;
            animationTimer.Tick += new EventHandler(animationTimer_Tick);
        }
        private void InicialTransitionTheme()
        {
            this.Opacity = 0.0;
            animationTimer.Tick += new EventHandler(animationTimer_Tick);
        }
        private void InicialTransitionLoad()
        {
            this.Top += 9;
            transition1 = Transition.show;
            animationTimer.Start();
        }
        private void Alert(string msg, AlertForm.eType type)
        {
            var frm = new AlertForm();
            frm.ShowAlert(msg, type);
            frm.TopMost = true;

        }
        private void LoadDataTablesInfo()
        {
            var cPList = cmbPriceList.Value;
            dtInfo = Helpers.GetMenuInfo(cPList);
        }
    }
}

