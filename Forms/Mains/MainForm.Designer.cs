
namespace MenuGenerator.Forms.Mains
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.pnlMain = new System.Windows.Forms.Panel();
            this.pnlPrincipal = new System.Windows.Forms.Panel();
            this.prgLoading = new Bunifu.Framework.UI.BunifuCircleProgressbar();
            this.label1 = new System.Windows.Forms.Label();
            this.lbVersion = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btnMinimize = new CustomComponent.CButtonCBorder();
            this.btnCloseApp = new CustomComponent.CButtonCBorder();
            this.pctLogo = new System.Windows.Forms.PictureBox();
            this.cmbPriceList = new ExportGasPerformance.Appearance.CDropDown();
            this.bunifuElipse1 = new Bunifu.Framework.UI.BunifuElipse(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.btnReport = new MenuGenerator.Appearance.CButton();
            this.btnConn = new MenuGenerator.Appearance.CButton();
            this.pnlMain.SuspendLayout();
            this.pnlPrincipal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pctLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.BackColor = System.Drawing.Color.White;
            this.pnlMain.Controls.Add(this.pnlPrincipal);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(426, 248);
            this.pnlMain.TabIndex = 0;
            // 
            // pnlPrincipal
            // 
            this.pnlPrincipal.Controls.Add(this.prgLoading);
            this.pnlPrincipal.Controls.Add(this.label1);
            this.pnlPrincipal.Controls.Add(this.lbVersion);
            this.pnlPrincipal.Controls.Add(this.label6);
            this.pnlPrincipal.Controls.Add(this.btnMinimize);
            this.pnlPrincipal.Controls.Add(this.btnCloseApp);
            this.pnlPrincipal.Controls.Add(this.pctLogo);
            this.pnlPrincipal.Controls.Add(this.btnReport);
            this.pnlPrincipal.Controls.Add(this.btnConn);
            this.pnlPrincipal.Controls.Add(this.cmbPriceList);
            this.pnlPrincipal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlPrincipal.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlPrincipal.Location = new System.Drawing.Point(0, 0);
            this.pnlPrincipal.Name = "pnlPrincipal";
            this.pnlPrincipal.Size = new System.Drawing.Size(426, 248);
            this.pnlPrincipal.TabIndex = 1;
            this.pnlPrincipal.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnlPrincipal_MouseDown);
            // 
            // prgLoading
            // 
            this.prgLoading.animated = false;
            this.prgLoading.animationIterval = 3;
            this.prgLoading.animationSpeed = 1;
            this.prgLoading.BackColor = System.Drawing.Color.White;
            this.prgLoading.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("prgLoading.BackgroundImage")));
            this.prgLoading.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.prgLoading.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F);
            this.prgLoading.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(146)))), ((int)(((byte)(97)))));
            this.prgLoading.LabelVisible = false;
            this.prgLoading.LineProgressThickness = 5;
            this.prgLoading.LineThickness = 2;
            this.prgLoading.Location = new System.Drawing.Point(168, 72);
            this.prgLoading.Margin = new System.Windows.Forms.Padding(10, 9, 10, 9);
            this.prgLoading.MaxValue = 100;
            this.prgLoading.Name = "prgLoading";
            this.prgLoading.ProgressBackColor = System.Drawing.Color.Gainsboro;
            this.prgLoading.ProgressColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(146)))), ((int)(((byte)(97)))));
            this.prgLoading.Size = new System.Drawing.Size(90, 90);
            this.prgLoading.TabIndex = 38;
            this.prgLoading.Value = 20;
            this.prgLoading.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.DimGray;
            this.label1.Location = new System.Drawing.Point(208, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 17);
            this.label1.TabIndex = 47;
            this.label1.Text = "Nivel Precio";
            // 
            // lbVersion
            // 
            this.lbVersion.AutoSize = true;
            this.lbVersion.BackColor = System.Drawing.Color.White;
            this.lbVersion.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.lbVersion.ForeColor = System.Drawing.Color.DimGray;
            this.lbVersion.Location = new System.Drawing.Point(187, 206);
            this.lbVersion.Name = "lbVersion";
            this.lbVersion.Size = new System.Drawing.Size(89, 17);
            this.lbVersion.TabIndex = 45;
            this.lbVersion.Text = "Versión 1.0.4.1";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.White;
            this.label6.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.label6.ForeColor = System.Drawing.Color.DimGray;
            this.label6.Location = new System.Drawing.Point(279, 206);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(130, 17);
            this.label6.TabIndex = 46;
            this.label6.Text = "Generador de Menú";
            // 
            // btnMinimize
            // 
            this.btnMinimize.BackBorderColor = System.Drawing.Color.Transparent;
            this.btnMinimize.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(252)))), ((int)(((byte)(254)))));
            this.btnMinimize.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(252)))), ((int)(((byte)(254)))));
            this.btnMinimize.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(252)))), ((int)(((byte)(254)))));
            this.btnMinimize.BorderRadiusBottomLeft = 16;
            this.btnMinimize.BorderRadiusBottomRight = 0;
            this.btnMinimize.BorderRadiusTopLeft = 16;
            this.btnMinimize.BorderRadiusTopRight = 0;
            this.btnMinimize.BorderSize = 0;
            this.btnMinimize.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMinimize.FlatAppearance.BorderSize = 0;
            this.btnMinimize.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Silver;
            this.btnMinimize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMinimize.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMinimize.ForeColor = System.Drawing.Color.Gray;
            this.btnMinimize.Location = new System.Drawing.Point(323, 12);
            this.btnMinimize.Name = "btnMinimize";
            this.btnMinimize.OverBackColor = System.Drawing.Color.Silver;
            this.btnMinimize.Size = new System.Drawing.Size(40, 30);
            this.btnMinimize.TabIndex = 43;
            this.btnMinimize.Text = " ─";
            this.btnMinimize.TextColor = System.Drawing.Color.Gray;
            this.btnMinimize.UseVisualStyleBackColor = false;
            this.btnMinimize.Click += new System.EventHandler(this.btnMinimize_Click);
            // 
            // btnCloseApp
            // 
            this.btnCloseApp.BackBorderColor = System.Drawing.Color.Transparent;
            this.btnCloseApp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(252)))), ((int)(((byte)(254)))));
            this.btnCloseApp.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(252)))), ((int)(((byte)(254)))));
            this.btnCloseApp.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(252)))), ((int)(((byte)(254)))));
            this.btnCloseApp.BorderRadiusBottomLeft = 0;
            this.btnCloseApp.BorderRadiusBottomRight = 16;
            this.btnCloseApp.BorderRadiusTopLeft = 0;
            this.btnCloseApp.BorderRadiusTopRight = 16;
            this.btnCloseApp.BorderSize = 0;
            this.btnCloseApp.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCloseApp.FlatAppearance.BorderSize = 0;
            this.btnCloseApp.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightCoral;
            this.btnCloseApp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCloseApp.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.btnCloseApp.ForeColor = System.Drawing.Color.Gray;
            this.btnCloseApp.Location = new System.Drawing.Point(369, 12);
            this.btnCloseApp.Name = "btnCloseApp";
            this.btnCloseApp.OverBackColor = System.Drawing.Color.LightCoral;
            this.btnCloseApp.Size = new System.Drawing.Size(40, 30);
            this.btnCloseApp.TabIndex = 44;
            this.btnCloseApp.Text = " x";
            this.btnCloseApp.TextColor = System.Drawing.Color.Gray;
            this.btnCloseApp.UseVisualStyleBackColor = false;
            this.btnCloseApp.Click += new System.EventHandler(this.btnCloseApp_Click);
            // 
            // pctLogo
            // 
            this.pctLogo.Image = global::MenuGenerator.Properties.Resources.main_logo1;
            this.pctLogo.Location = new System.Drawing.Point(12, 46);
            this.pctLogo.Name = "pctLogo";
            this.pctLogo.Size = new System.Drawing.Size(176, 139);
            this.pctLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pctLogo.TabIndex = 40;
            this.pctLogo.TabStop = false;
            // 
            // cmbPriceList
            // 
            this.cmbPriceList.BackColor = System.Drawing.Color.WhiteSmoke;
            this.cmbPriceList.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(146)))), ((int)(((byte)(97)))));
            this.cmbPriceList.BorderSize = 2;
            this.cmbPriceList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPriceList.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.cmbPriceList.ForeColor = System.Drawing.Color.DimGray;
            this.cmbPriceList.IconColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(146)))), ((int)(((byte)(97)))));
            this.cmbPriceList.ListBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(212)))), ((int)(((byte)(236)))), ((int)(((byte)(230)))));
            this.cmbPriceList.ListTextColor = System.Drawing.Color.DimGray;
            this.cmbPriceList.Location = new System.Drawing.Point(211, 81);
            this.cmbPriceList.MinimumSize = new System.Drawing.Size(200, 40);
            this.cmbPriceList.Name = "cmbPriceList";
            this.cmbPriceList.Padding = new System.Windows.Forms.Padding(2);
            this.cmbPriceList.Size = new System.Drawing.Size(200, 40);
            this.cmbPriceList.TabIndex = 121;
            this.cmbPriceList.Texts = "";
            // 
            // bunifuElipse1
            // 
            this.bunifuElipse1.ElipseRadius = 40;
            this.bunifuElipse1.TargetControl = this;
            // 
            // btnReport
            // 
            this.btnReport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(146)))), ((int)(((byte)(97)))));
            this.btnReport.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(146)))), ((int)(((byte)(97)))));
            this.btnReport.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnReport.BorderRadius = 20;
            this.btnReport.BorderSize = 0;
            this.btnReport.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReport.FlatAppearance.BorderSize = 0;
            this.btnReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReport.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReport.ForeColor = System.Drawing.Color.White;
            this.btnReport.Location = new System.Drawing.Point(211, 140);
            this.btnReport.Name = "btnReport";
            this.btnReport.OverBackColor = System.Drawing.Color.Empty;
            this.btnReport.Size = new System.Drawing.Size(198, 45);
            this.btnReport.TabIndex = 4;
            this.btnReport.Text = "Generar Menú";
            this.btnReport.TextColor = System.Drawing.Color.White;
            this.btnReport.UseVisualStyleBackColor = false;
            this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
            // 
            // btnConn
            // 
            this.btnConn.BackColor = System.Drawing.Color.White;
            this.btnConn.BackgroundColor = System.Drawing.Color.White;
            this.btnConn.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnConn.BorderRadius = 19;
            this.btnConn.BorderSize = 0;
            this.btnConn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnConn.FlatAppearance.BorderSize = 0;
            this.btnConn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConn.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.btnConn.ForeColor = System.Drawing.Color.White;
            this.btnConn.Image = ((System.Drawing.Image)(resources.GetObject("btnConn.Image")));
            this.btnConn.Location = new System.Drawing.Point(12, 191);
            this.btnConn.Name = "btnConn";
            this.btnConn.OverBackColor = System.Drawing.Color.Empty;
            this.btnConn.Size = new System.Drawing.Size(58, 44);
            this.btnConn.TabIndex = 1;
            this.btnConn.TextColor = System.Drawing.Color.White;
            this.btnConn.UseVisualStyleBackColor = false;
            this.btnConn.Click += new System.EventHandler(this.btnConn_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(426, 248);
            this.Controls.Add(this.pnlMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.LoginForm_Load);
            this.pnlMain.ResumeLayout(false);
            this.pnlPrincipal.ResumeLayout(false);
            this.pnlPrincipal.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pctLogo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private Appearance.CButton btnReport;
        private Appearance.CButton btnConn;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Panel pnlPrincipal;
        private Bunifu.Framework.UI.BunifuElipse bunifuElipse1;
        private Bunifu.Framework.UI.BunifuCircleProgressbar prgLoading;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label lbVersion;
        private System.Windows.Forms.Label label6;
        private CustomComponent.CButtonCBorder btnMinimize;
        private CustomComponent.CButtonCBorder btnCloseApp;
        private System.Windows.Forms.PictureBox pctLogo;
        private System.Windows.Forms.Label label1;
        internal ExportGasPerformance.Appearance.CDropDown cmbPriceList;
    }
}