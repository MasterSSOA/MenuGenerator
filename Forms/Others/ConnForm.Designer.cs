
namespace MenuGenerator.Forms.Others
{
    partial class ConnForm
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
            this.lbUser = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbServer = new System.Windows.Forms.TextBox();
            this.tbDataBase = new System.Windows.Forms.TextBox();
            this.tbPass = new System.Windows.Forms.TextBox();
            this.tbUser = new System.Windows.Forms.TextBox();
            this.btnSaveConn = new MenuGenerator.Appearance.CButton();
            this.btnTryConn = new MenuGenerator.Appearance.CButton();
            this.SuspendLayout();
            // 
            // lbUser
            // 
            this.lbUser.AutoSize = true;
            this.lbUser.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbUser.Location = new System.Drawing.Point(12, 20);
            this.lbUser.Name = "lbUser";
            this.lbUser.Size = new System.Drawing.Size(71, 20);
            this.lbUser.TabIndex = 3;
            this.lbUser.Text = "Servidor :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(11, 61);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(111, 20);
            this.label4.TabIndex = 11;
            this.label4.Text = "Base de Datos :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(11, 105);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 20);
            this.label1.TabIndex = 13;
            this.label1.Text = "Usuario :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(11, 149);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 20);
            this.label2.TabIndex = 15;
            this.label2.Text = "Contraseña :";
            // 
            // tbServer
            // 
            this.tbServer.Enabled = false;
            this.tbServer.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.tbServer.Location = new System.Drawing.Point(127, 17);
            this.tbServer.MaxLength = 150;
            this.tbServer.Name = "tbServer";
            this.tbServer.Size = new System.Drawing.Size(214, 27);
            this.tbServer.TabIndex = 1;
            this.tbServer.TextChanged += new System.EventHandler(this.tbServer_TextChanged);
            // 
            // tbDataBase
            // 
            this.tbDataBase.Enabled = false;
            this.tbDataBase.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.tbDataBase.Location = new System.Drawing.Point(127, 61);
            this.tbDataBase.MaxLength = 150;
            this.tbDataBase.Name = "tbDataBase";
            this.tbDataBase.Size = new System.Drawing.Size(214, 27);
            this.tbDataBase.TabIndex = 2;
            this.tbDataBase.TextChanged += new System.EventHandler(this.tbDataBase_TextChanged);
            // 
            // tbPass
            // 
            this.tbPass.Enabled = false;
            this.tbPass.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.tbPass.Location = new System.Drawing.Point(127, 146);
            this.tbPass.MaxLength = 150;
            this.tbPass.Name = "tbPass";
            this.tbPass.Size = new System.Drawing.Size(214, 27);
            this.tbPass.TabIndex = 4;
            this.tbPass.UseSystemPasswordChar = true;
            this.tbPass.TextChanged += new System.EventHandler(this.tbPass_TextChanged);
            // 
            // tbUser
            // 
            this.tbUser.Enabled = false;
            this.tbUser.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.tbUser.Location = new System.Drawing.Point(127, 102);
            this.tbUser.MaxLength = 150;
            this.tbUser.Name = "tbUser";
            this.tbUser.Size = new System.Drawing.Size(214, 27);
            this.tbUser.TabIndex = 3;
            this.tbUser.TextChanged += new System.EventHandler(this.tbUser_TextChanged);
            // 
            // btnSaveConn
            // 
            this.btnSaveConn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(135)))), ((int)(((byte)(31)))));
            this.btnSaveConn.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(135)))), ((int)(((byte)(31)))));
            this.btnSaveConn.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnSaveConn.BorderRadius = 10;
            this.btnSaveConn.BorderSize = 0;
            this.btnSaveConn.FlatAppearance.BorderSize = 0;
            this.btnSaveConn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSaveConn.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveConn.ForeColor = System.Drawing.Color.White;
            this.btnSaveConn.Location = new System.Drawing.Point(179, 195);
            this.btnSaveConn.Name = "btnSaveConn";
            this.btnSaveConn.OverBackColor = System.Drawing.Color.Empty;
            this.btnSaveConn.Size = new System.Drawing.Size(164, 30);
            this.btnSaveConn.TabIndex = 6;
            this.btnSaveConn.Text = "Modificar Configuración";
            this.btnSaveConn.TextColor = System.Drawing.Color.White;
            this.btnSaveConn.UseVisualStyleBackColor = false;
            this.btnSaveConn.Click += new System.EventHandler(this.btnSaveConn_Click);
            // 
            // btnTryConn
            // 
            this.btnTryConn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(135)))), ((int)(((byte)(31)))));
            this.btnTryConn.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(135)))), ((int)(((byte)(31)))));
            this.btnTryConn.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.btnTryConn.BorderRadius = 10;
            this.btnTryConn.BorderSize = 0;
            this.btnTryConn.FlatAppearance.BorderSize = 0;
            this.btnTryConn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTryConn.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTryConn.ForeColor = System.Drawing.Color.White;
            this.btnTryConn.Location = new System.Drawing.Point(12, 195);
            this.btnTryConn.Name = "btnTryConn";
            this.btnTryConn.OverBackColor = System.Drawing.Color.Empty;
            this.btnTryConn.Size = new System.Drawing.Size(146, 30);
            this.btnTryConn.TabIndex = 5;
            this.btnTryConn.Text = "Probar Conexión";
            this.btnTryConn.TextColor = System.Drawing.Color.White;
            this.btnTryConn.UseVisualStyleBackColor = false;
            this.btnTryConn.Click += new System.EventHandler(this.btnTryConn_Click);
            // 
            // ConnForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(357, 248);
            this.Controls.Add(this.tbPass);
            this.Controls.Add(this.tbUser);
            this.Controls.Add(this.tbDataBase);
            this.Controls.Add(this.tbServer);
            this.Controls.Add(this.btnSaveConn);
            this.Controls.Add(this.btnTryConn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lbUser);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Location = new System.Drawing.Point(455, 333);
            this.MaximumSize = new System.Drawing.Size(455, 333);
            this.Name = "ConnForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.ConnForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lbUser;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private Appearance.CButton btnTryConn;
        private Appearance.CButton btnSaveConn;
        private System.Windows.Forms.TextBox tbServer;
        private System.Windows.Forms.TextBox tbDataBase;
        private System.Windows.Forms.TextBox tbPass;
        private System.Windows.Forms.TextBox tbUser;
    }
}