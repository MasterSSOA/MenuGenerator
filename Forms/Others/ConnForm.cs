using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DocumentFormat.OpenXml.Office2013.Excel;
using Microsoft.VisualBasic.ApplicationServices;
using MenuGenerator.Appearance;
using MenuGenerator.Classes;
using MenuGenerator.Properties;
using Connection = MenuGenerator.Classes.Connection;

namespace MenuGenerator.Forms.Others
{
    public partial class ConnForm : Form
    {
        //Properties.
        bool tested = false;

        public ConnForm()
        {
            InitializeComponent();
        }
        private void ConnForm_Load(object sender, EventArgs e)
        {
            TryButtonLogic();
            if (Settings.Default.Server != "")
            {
                tbServer.Text = Settings.Default.Server;
            }
            if (Settings.Default.DataBase != "")
            {
                tbDataBase.Text = Settings.Default.DataBase;
            }
            if (Settings.Default.User != "")
            {
                tbUser.Text = Settings.Default.User;
            }
            if (Settings.Default.Pass != "")
            {
                tbPass.Text = Settings.Default.Pass;
            }
        }

        //Textboxs.
        private void tbServer_TextChanged(object sender, EventArgs e)
        {
            TryButtonLogic();
        }
        private void tbDataBase_TextChanged(object sender, EventArgs e)
        {
            TryButtonLogic();
        }
        private void tbUser_TextChanged(object sender, EventArgs e)
        {
            TryButtonLogic();
        }
        private void tbPass_TextChanged(object sender, EventArgs e)
        {
            TryButtonLogic();
        }

        //Buttons.
        private void btnTryConn_Click(object sender, EventArgs e)
        {
            //Conectando con la data.
            TestConnection();
        }
        private void btnSaveConn_Click(object sender, EventArgs e)
        {
            MainLogic();

            //Guardando la data.
            Settings.Default.Server = tbServer.Text;
            Settings.Default.DataBase = tbDataBase.Text;
            Settings.Default.User = tbUser.Text;
            Settings.Default.Pass = tbPass.Text;
            Settings.Default.Save();

        }

        //Private Methods.
        private void MainLogic() 
        {
            if (btnSaveConn.Text != "Modificar Configuración")
            {
                btnSaveConn.Text = "Modificar Configuración";
                btnSaveConn.Enabled = true;
                MessageBox.Show("Guardado Exitosamente.");
                foreach (Control txts in this.Controls)
                {
                    if (txts.GetType().Equals(typeof(TextBox)))
                    {
                        TextBox txt = (TextBox)txts;
                        txt.Enabled = false;
                    }
                }
            }
            else
            {
                btnSaveConn.Text = "Guardar Configuración";
                btnSaveConn.Enabled = false;
                foreach (Control txts in this.Controls)
                {
                    if (txts.GetType().Equals(typeof(TextBox)))
                    {
                        TextBox txt = (TextBox)txts;
                        txt.Enabled = true;
                    }
                }
            }

           
        }
        private void TryButtonLogic()
        {
            if (tbServer.Text.Equals(string.Empty) ||
               tbDataBase.Text.Equals(string.Empty) ||
               tbUser.Text.Equals(string.Empty) ||
               tbPass.Text.Equals(string.Empty))
            {
                btnTryConn.Enabled = false;
            }
            else
            {
                btnTryConn.Enabled = true;
            }
        }
        private void TestConnection()
        {
            //Declaración.
            string key = "qw5erty2uiofas1dfghjk3adsf6zc8fg";

            var server = tbServer.Text;
            var database = tbDataBase.Text;
            var user = tbUser.Text;
            var pass = tbPass.Text;

            var logic = (server.Length == 24 && database.Length == 24 &&
                         user.Length == 24 && pass.Length == 24);

            if (!logic)
            {
                server = server.EncryptData(key);
                database = database.EncryptData(key);
                user = user.EncryptData(key);
                pass = pass.EncryptData(key);
            }

            //Llamada de metodo.
            dbConn conn = new dbConn(server, database, user, pass);

            try
            {
                Connection.MakeConnection(conn).Open();
                MessageBox.Show("Se Ha Conectado Exitosamente!");

                tbServer.Enabled = false;
                tbDataBase.Enabled = false;
                tbUser.Enabled = false;
                tbPass.Enabled = false;

                tbServer.Text = server;
                tbDataBase.Text = database;
                tbUser.Text = user;
                tbPass.Text = pass;

                btnSaveConn.Enabled = true;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message);
            }
            Connection.MakeConnection(conn).Close();
            
        }

       
    }
}
