
namespace MenuGenerator.Appearance
{
    partial class CTextBox
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tb1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // tb1
            // 
            this.tb1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tb1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tb1.Location = new System.Drawing.Point(10, 7);
            this.tb1.Name = "tb1";
            this.tb1.Size = new System.Drawing.Size(230, 15);
            this.tb1.TabIndex = 0;
            this.tb1.Click += new System.EventHandler(this.tb1_Click);
            this.tb1.TextChanged += new System.EventHandler(this.tb1_TextChanged);
            this.tb1.Enter += new System.EventHandler(this.tb1_Enter);
            this.tb1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tb1_KeyPress);
            this.tb1.KeyDown += this.tb1_KeyDown;
            this.tb1.Leave += new System.EventHandler(this.tb1_Leave);
            this.tb1.MouseEnter += new System.EventHandler(this.tb1_MouseEnter);
            this.tb1.MouseLeave += new System.EventHandler(this.tb1_MouseLeave);
            // 
            // CTextBox
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.tb1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "CTextBox";
            this.Padding = new System.Windows.Forms.Padding(10, 7, 10, 7);
            this.Size = new System.Drawing.Size(250, 30);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tb1;
    }
}
