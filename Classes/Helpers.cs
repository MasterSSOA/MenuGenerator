using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using MenuGenerator.Forms.Others;
using MenuGenerator.Properties;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Drawing.Diagrams;
using ExportGasPerformance.Forms.Mains;
using MenuGenerator.Database.DataSet;

namespace MenuGenerator.Classes
{
    public static class Helpers
    {
        //Mover los Formularios.         
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        public extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        public extern static void SendMessage(this System.IntPtr hWnd, int wMesg, int wParam, int lParam);

        //Animación de Form.
        [DllImport("user32.DLL", EntryPoint = "AnimateWindow")]
        public extern static bool AnimateWindow(IntPtr hwnd, int dwTime, int dwFlags);


        //Encriptación de datos.
        internal static string EncryptData(this string Data, string key)
        {
            byte[] iv = new byte[16];
            byte[] array;

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = iv;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter((Stream)cryptoStream))
                        {
                            streamWriter.Write(Data);
                        }
                        array = memoryStream.ToArray();
                    }
                }
            }

            return Convert.ToBase64String(array);
        }
        internal static string DesencryptData(this string EncryptedData, string key)
        {
            byte[] iv = new byte[16];
            byte[] buffer = Convert.FromBase64String(EncryptedData);

            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = iv;
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream(buffer))
                {
                    using (CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader streamReader = new StreamReader((Stream)cryptoStream))
                        {
                            return streamReader.ReadToEnd();
                        }
                    }
                }
            }
        }

        //Selección de Ubicación de Base de Datos.
        internal static int GetDatabaseRoute(int none = 0)
        {
            int Code = 0;

            try
            {
                //Abrir ventana.
                DialogResult result;
                string Message = string.Empty;
                string Title  = "Selección de Base de Datos";

                if (string.IsNullOrEmpty(Settings.Default["DataBasePath"].ToString()))
                {
                    //Settings.Default["DataBasePath"] = fileDialog.FileName.ToString();
                    Settings.Default.Save();

                    Settings.Default["DataBasePath"] = @"|DataDirectory|\Data Base\dbEasyCash.mdf";
                    Settings.Default.Save();
                }
                else if (none != 0)
                {
                    MessageBoxButtons buttons = MessageBoxButtons.OK;
                    Message = "No se encuentra la ruta de la Base de Datos";
                    result = MessageBox.Show(Message + Environment.NewLine + "" +
                                             "Favor, Revise!", Title, buttons);

                    if (result.Equals(DialogResult.OK))
                    {
                        //Settings.Default["DataBasePath"] = fileDialog.FileName.ToString();
                        Settings.Default.Save();
                    }
                }

                if (!string.IsNullOrEmpty(Settings.Default["DataBasePath"].ToString()) || none != 0)
                    Code = 1;
                else
                    Code = 0;
            }
            catch (IOException ex)
            {
                MessageBox.Show("Error :" + ex.Message);
            }

            return Code;
        }

        //------------------------------------------------------------------------------------------------------

        //Database Querys.
        //Habilitar Tickets Individual.
        internal static void EnableIndividualTicket(this string ticket, int state = 0) 
        {
            dbConn db = new dbConn();
            var conn = Connection.MakeConnection(db);

            SqlCommand command;
            int successful = 0;
            string script;

            try
            {
                conn.Open();

                //Paso 1: Identifcar si existe registrado en una estación.
                script = @"SELECT * 
                           FROM TICKETS_EST 
                           WHERE NUMERO = '" + ticket + "'";
                command = new SqlCommand(script, conn);

                if (command.ExecuteScalar() == null)
                {

                    //Paso 2: Aplicar cambio.
                    script = @"UPDATE TICKETS_PRE
                               SET ESTADO = '"+ state + "' " +
                               "WHERE NUMERO = '" + ticket + "'";
                    command = new SqlCommand(script, conn);
                    successful = command.ExecuteNonQuery();

                    if (successful.Equals(0))
                    {
                        MessageBox.Show("El Ticket Restistrado No Existe", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show("El Ticket Fue Habilitado Correctamente.");
                    }

                }
                else
                {
                    var message = "El Ticket Restistrado Se Encuentra En Una Estación. Contactar Operación Nativa";
                    MessageBox.Show(message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message);
            }
            conn.Close();
        }

        //Habilitar Tickets Multiples.
        internal static void EnableMultiplesTickets(this string tickets, int state = 0)
        {
            dbConn db = new dbConn();
            var conn = Connection.MakeConnection(db);

            SqlCommand command;
            int successful = 0;
            string _tickets;
            string script;
            List<string> badList = new List<string>();
            List<string> goodList = new List<string>();

            try
            {
                conn.Open();

                //Paso 1: Separación y organización de tickets.
                tickets = tickets.Replace("\n", " ");
                tickets = tickets.Replace("\r", " ");
                goodList = tickets.Split(',').ToList();

                foreach (var ticket in tickets.Split(',').ToList())
                {
                    //Paso 2: Identifcar si existe registrado en una estación.
                    script = @"SELECT * 
                               FROM TICKETS_EST 
                               WHERE NUMERO = '" + ticket + "'";
                    command = new SqlCommand(script, conn);

                    if (command.ExecuteScalar() != null)
                    {
                        badList.Add(ticket);
                        goodList.Remove(ticket);
                    }
                }

                //Paso 2.1: Mostrar en caso de que exista.
                if (1 < badList.Count)
                {
                    _tickets = "";
                    var newLine = Environment.NewLine;
                    for (int i = 0; i < badList.Count; i++)
                    {
                        _tickets += "'" + badList[i] + "'";
                        if (!i.Equals(badList.Count - 1))
                        {
                            _tickets += ", ";
                        }
                    }

                    MessageBox.Show("Los Tickets : "+ newLine + "" +
                                    "" + _tickets + " " + newLine + "" +
                                    "Se Encuentran Registrado En Una Estación.");

                }
                else if (badList.Count.Equals(1))
                {
                    MessageBox.Show("El Ticket : "+ badList[0] + " se Encuentra Registrado En Una Estación.");
                }

                //Paso 3: Aplicar cambio.
                if (1 < goodList.Count)
                {
                    _tickets = "";
                    var newLine = Environment.NewLine;
                    for (int i = 0; i < goodList.Count; i++)
                    {
                        _tickets += "'"+ goodList[i] + "'";
                        if (!i.Equals(goodList.Count - 1))
                        {
                            _tickets += ", ";
                        }
                    }

                    script = @"UPDATE TICKETS_PRE
                               SET ESTADO = '"+ state +"' " +
                               "WHERE NUMERO in (" + _tickets + ")";

                    command = new SqlCommand(script, conn);
                    successful = command.ExecuteNonQuery();

                    if (successful.Equals(0))
                    {
                        MessageBox.Show("Los Tickets Restistrados No Existen", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (1 < successful)
                    {
                        MessageBox.Show("Los Tickets Fueron Habilitados Correctamente.");

                    }
                    else if(successful.Equals(1))
                    {
                        MessageBox.Show("El Ticket Fue Habilitado Correctamente.");
                    }

                }
                else if (goodList.Count.Equals(1))
                {
                    script = @"UPDATE TICKETS_PRE
                               SET ESTADO = '2'
                               WHERE NUMERO = '" + goodList[0] + ";";

                    command = new SqlCommand(script, conn);
                    successful = command.ExecuteNonQuery();

                    if (successful.Equals(0))
                    {
                        MessageBox.Show("El Ticket Restistrado No Existe", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show("El Ticket Fue Habilitado Correctamente.");
                    }
                }
                else
                {
                    MessageBox.Show("No Se Habilito Ningún Ticket.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message);
            }
            conn.Close();
        }

        //Habilitar Tickets En Rango.
        internal static void EnableRangeTickets(this string ticket1, string ticket2, int state = 0)
        {
            dbConn db = new dbConn();
            var conn = Connection.MakeConnection(db);

            SqlCommand command;
            int successful = 0;
            string _tickets;
            string script;
            List<string> badList = new List<string>();
            List<string> goodList = new List<string>();

            try
            {
                conn.Open();

                //Paso 1: Separación y organización de tickets.
                _tickets = "";
                var value1 = Convert.ToInt32(ticket1);
                var value2 = Convert.ToInt32(ticket2);

                while (value1 <= value2)
                {
                    goodList.Add(value1.ToString());
                    value1++;
                }

                var tickets = goodList;

                foreach (var ticket in tickets)
                {
                    //Paso 2: Identifcar si existe registrado en una estación.
                    script = @"SELECT * 
                               FROM TICKETS_EST 
                               WHERE NUMERO = '" + ticket + "'";
                    command = new SqlCommand(script, conn);

                    if (command.ExecuteScalar() != null)
                    {
                        badList.Add(ticket);
                        goodList.Remove(ticket);
                    }
                }

                //Paso 2.1: Mostrar en caso de que exista.
                if (1 < badList.Count)
                {
                    _tickets = "";
                    var newLine = Environment.NewLine;
                    for (int i = 0; i < badList.Count; i++)
                    {
                        _tickets += "'" + badList[i] + "'";
                        if (!i.Equals(badList.Count - 1))
                        {
                            _tickets += ", ";
                        }
                    }

                    MessageBox.Show("Los Tickets : " + newLine + "" +
                                    "" + _tickets + " " + newLine + "" +
                                    "Se Encuentran Registrado En Una Estación.");

                }
                else if (badList.Count.Equals(1))
                {
                    MessageBox.Show("El Ticket : " + badList[0] + " se Encuentra Registrado En Una Estación.");
                }

                //Paso 3: Aplicar cambio.
                if (1 < goodList.Count)
                {
                    _tickets = "";
                    var newLine = Environment.NewLine;
                    for (int i = 0; i < goodList.Count; i++)
                    {
                        _tickets += "'" + goodList[i] + "'";
                        if (!i.Equals(goodList.Count - 1))
                        {
                            _tickets += ", ";
                        }
                    }

                    script = @"UPDATE TICKETS_PRE
                               SET ESTADO = '" + state + "' " +
                               "WHERE NUMERO in (" + _tickets + ")";

                    command = new SqlCommand(script, conn);
                    successful = command.ExecuteNonQuery();

                    if (successful.Equals(0))
                    {
                        MessageBox.Show("Los Tickets Restistrados No Existen", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (1 < successful)
                    {
                        MessageBox.Show("Los Tickets Fueron Habilitados Correctamente.");

                    }
                    else if (successful.Equals(1))
                    {
                        MessageBox.Show("El Ticket Fue Habilitado Correctamente.");
                    }

                }
                else if (goodList.Count.Equals(1))
                {
                    script = @"UPDATE TICKETS_PRE
                               SET ESTADO = '2'
                               WHERE NUMERO = '" + goodList[0] + ";";

                    command = new SqlCommand(script, conn);
                    successful = command.ExecuteNonQuery();

                    if (successful.Equals(0))
                    {
                        MessageBox.Show("El Ticket Restistrado No Existe", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show("El Ticket Fue Habilitado Correctamente.");
                    }
                }
                else
                {
                    MessageBox.Show("No Se Habilito Ningún Ticket.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message);
            }
            conn.Close();
        }

        //Verificar Tickets Individual.
        internal static void IndividualTicketVerification(this string ticket)
        {
            dbConn db = new dbConn();
            var conn = Connection.MakeConnection(db);

            SqlCommand command;
            bool inStation = false;
            string script;

            try
            {
                conn.Open();

                //Paso 1: Identifcar si existe registrado en una estación.
                script = @"SELECT * 
                           FROM TICKETS_EST 
                           WHERE NUMERO = '" + ticket + "'";
                command = new SqlCommand(script, conn);

                //Paso 2: Verificar si el ticket esta registrado en una estación.
                inStation = (command.ExecuteScalar() != null) ? true : false;

                //Paso 3: Verificar Estado.
                script = @" SELECT ESTADO 
                            FROM TICKETS_PRE
                            WHERE NUMERO = '" + ticket + "'";
                command = new SqlCommand(script, conn);
                SqlDataReader Data = command.ExecuteReader();

                if (Data.Read())
                {
                    string estado = "";
                    string adicional = "";

                    if (inStation)
                    {
                        adicional = " / ESTA REGISTRADO EN UNA ESTACION";
                    }


                    switch (Convert.ToInt16(Data[0].ToString()))
                    {
                        case 0:
                            estado = "LISTO PARA IMPRIMIR";
                            break;
                        case 1:
                            if (!inStation)
                            {
                                estado = "USADO / PERO NO ESTA REGISTRADO EN ESTACION";
                            }
                            else
                            {
                                estado = "USADO";
                            }

                            break;
                        case 2:
                            estado = "LISTO PARA USAR";
                            break;
                    }

                    MessageBox.Show("El Ticket esta " + estado + adicional);
                }
                else
                {
                    MessageBox.Show("El Ticket Restistrado No Existe", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message);
            }
            conn.Close();
        }

        //Verificar Tickets En Rango.
        internal static void RangeTicketsVerification(this string ticket1, string ticket2)
        {
            dbConn db = new dbConn();
            var conn = Connection.MakeConnection(db);

            SqlCommand command;
            string message;
            var nl = Environment.NewLine;
            string script;
            List<string> tickets = new List<string>();

            try
            {
                conn.Open();

                //Paso 1: Separación y organización de tickets.
                message = "";

                var value1 = Convert.ToInt32(ticket1);
                var value2 = Convert.ToInt32(ticket2);
                var inStation = false;

                while (value1 <= value2)
                {
                    tickets.Add(value1.ToString());
                    value1++;
                }

                script = @" SELECT t1.NUMERO
                            FROM tickets_pre t1
                            LEFT JOIN tickets_est t2 ON t2.NUMERO = t1.NUMERO
                            WHERE t2.NUMERO IS NULL
                            AND t1.NUMERO IN ("+ tickets + ") " +
                           " ORDER BY t1.NUMERO ";
                command = new SqlCommand(script, conn);
                SqlDataReader Data = command.ExecuteReader();

                if (Data.Read())
                {
                    string estado = "";
                    string adicional = "";

                    switch (Convert.ToInt16(Data[1].ToString()))
                    {
                        case 0:
                            estado = "LISTO PARA IMPRIMIR";
                            break;
                        case 1:
                            if (!inStation)
                            {
                                estado = "USADO / PERO NO ESTA REGISTRADO EN ESTACION";
                            }
                            else
                            {
                                estado = "USADO";
                            }

                            break;
                        case 2:
                            estado = "LISTO PARA USAR";
                            break;
                    }
                }
                else
                {
                    message = "Los Tickets Digitados No Existen";
                }





                    foreach (var ticket in tickets)
                {
                    //Paso 2: Identifcar si existe registrado en una estación.
                    script = @"SELECT * 
                               FROM TICKETS_EST 
                               WHERE NUMERO = '" + ticket + "'";
                    command = new SqlCommand(script, conn);
                    inStation = (command.ExecuteScalar() != null) ? true : false;

                    //Paso 3: Verificar Estado.
                    script = @" SELECT NUMERO, ESTADO 
                                FROM TICKETS_PRE
                                WHERE NUMERO in (" + ticket + ") " +
                               "order by NUMERO";
                    command = new SqlCommand(script, conn);
                    Data = command.ExecuteReader();

                    if (Data.Read())
                    {
                        string estado = "";
                        string adicional = "";

                        if (inStation)
                        {
                            adicional = " / ESTA REGISTRADO EN UNA ESTACION";
                        }

                        switch (Convert.ToInt16(Data[1].ToString()))
                        {
                            case 0:
                                estado = "LISTO PARA IMPRIMIR";
                                break;
                            case 1:
                                if (!inStation)
                                {
                                    estado = "USADO / PERO NO ESTA REGISTRADO EN ESTACION";
                                }
                                else
                                {
                                    estado = "USADO";
                                }

                                break;
                            case 2:
                                estado = "LISTO PARA USAR";
                                break;
                        }

                        message += ticket + " esta " + estado + adicional + nl;
                    }
                    else
                    {
                        message += ticket + " No Existe " + nl;
                    }
                    Data.Close();
                }

                MessageBox.Show(message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message);
            }
            conn.Close();

        }

        //Cargar Datos (Modificación de Datos Tickets).
        // -- Centros.
        internal static DataTable GetcmbCenters()
        {
            string script;
            dbConn db = new dbConn();
            var conn = Connection.MakeConnection(db);
            var dataTable = new DataTable();
            SqlDataReader data;
            SqlCommand command;

            try
            {
                conn.Open();
                script = @"SELECT    CFP_CODIGO AS id
		                            ,CFP_NOMBRE AS descripcion
                            FROM TBCFGPRECIO
                            WHERE CFP_COMPANIA = '2'
                              AND CFP_ESTADO = '1'";

                command = new SqlCommand(script, conn);
                data = command.ExecuteReader();
                dataTable.Load(data);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message);
            }
            conn.Close();

            return dataTable;
        }

        // -- Obtener los datos de los tickets del turno buscado. (Todos)
        internal static List<double> GetAllTurnTickets(this string center, string date, string turn)
        {
            dbConn db = new dbConn();
            var conn = Connection.MakeConnection(db);
            List<double> dataLoaded = new List<double>() { 0, 0 };
            SqlDataReader data;
            SqlCommand command;
            string script;

            try
            {
                conn.Open();
                script = @" SELECT COUNT(*) AS CANTIDAD, SUM(VALOR) AS TOTAL
                            FROM tickets_est
                            WHERE centro = '" + center + "' " +
                            "AND FECHA = '" + date + "' " +
                            "AND TURNO = '" + turn + "'; ";

                command = new SqlCommand(script, conn);
                data = command.ExecuteReader();
                if (data.Read())
                {
                    dataLoaded = new List<double>() 
                    {
                       Convert.ToDouble(data[0].ToString()),
                       Convert.ToDouble(data[1].ToString())
                    };
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message);
            }
            conn.Close();

            return dataLoaded;
        }

        // -- Obtener los datos de los tickets del turno buscado.
        internal static List<TicketEstInfo> GetTicketsInfo(this string tickets)
        {
            dbConn db = new dbConn();
            var conn = Connection.MakeConnection(db);
            List<TicketEstInfo> dataLoaded = new List<TicketEstInfo>();
            TicketEstInfo ticket;
            SqlDataReader data;
            SqlCommand command;
            string script;

            try
            {
                conn.Open();
                script = @" SELECT   A.NUMERO
	                                ,A.VALOR 
	                                ,B.CENTRO_DESC AS ESTACION
	                                ,CONVERT(VARCHAR, A.FECHA, 103) AS FECHA
	                                ,A.TURNO AS TURNO   
                                	,A.NUM_EMP AS LLENADOR
                            FROM tickets_est AS A
                                INNER JOIN CENTRO_DIST_EST AS B
	                                ON A.CENTRO = B.CENTRO
                            WHERE A.NUMERO IN ( " + tickets + " ); ";

                command = new SqlCommand(script, conn);
                data = command.ExecuteReader();

                while (data.Read())
                {
                    ticket = new TicketEstInfo(
                                                data[0].ToString(), 
                                                data[1].ToString(), 
                                                data[2].ToString(), 
                                                data[3].ToString(),
                                                data[4].ToString(),
                                                data[5].ToString()
                                              );
                    dataLoaded.Add(ticket);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message);
            }
            conn.Close();

            return dataLoaded;
        }

        // -- Validar los tickets que no existen en una estacion registrada. (Parcial)
        internal static List<string> GetBadTickets(this string tSearch, List<TicketEstInfo> tickets)
        {
            tSearch = tSearch.Replace("\n", "");
            tSearch = tSearch.Replace(" ", "");
            tSearch = tSearch.Replace("\r", "");
            var ticketsList = tSearch.Split(',').ToList();

            var badtickets = new List<string>();

            foreach (var ticket in ticketsList)
            {
                if (!tickets.Exists(t => t.Number.Equals(ticket)))
                {
                   badtickets.Add(ticket);
                }
            }
            return badtickets;
        }

        // Modificar Tickets.
        internal static void ModAllTurnTicket(this string center, string date, string turn, string setValue)
        {
            dbConn db = new dbConn();
            var conn = Connection.MakeConnection(db);
            SqlCommand command;
            int success = 0;
            string script;

            try
            {
                conn.Open();
                script = @" UPDATE tickets_est
                            "+ setValue + " " +
                            "WHERE CENTRO = '" + center + "' " +
                            "AND FECHA = '" + date + "' " +
                            "AND TURNO = '" + turn + "'; ";

                command = new SqlCommand(script, conn);
                success = command.ExecuteNonQuery();

                if (1 < success)
                {
                    MessageBox.Show("Los Tickets Fueron Modificados Correctamente.");
                }
                else if(success.Equals(1))
                {
                    MessageBox.Show("El Ticket Fue Modificado Correctamente.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message);
            }
            conn.Close();
        }

        // Modificar Tickets.
        internal static void ModParcialTurnTicket(this string tickets, string setValue)
        {
            dbConn db = new dbConn();
            var conn = Connection.MakeConnection(db);
            SqlCommand command;
            int success = 0;
            string script;

            //Depuración.
            tickets = tickets.Replace("\n", "");
            tickets = tickets.Replace(" ", "");
            var ticketsList = tickets.Split(',').ToList();

            var _tickets = "";
            for (int i = 0; i < ticketsList.Count; i++)
            {
                _tickets += "'" + ticketsList[i] + "'";
                if (!i.Equals(ticketsList.Count - 1))
                {
                    _tickets += ", ";
                }
            }

            try
            {
                conn.Open();
                script = @" UPDATE tickets_est
                            " + setValue + " " +
                            "WHERE NUMERO IN (" + _tickets + "); ";

                command = new SqlCommand(script, conn);
                success = command.ExecuteNonQuery();

                if (1 < success)
                {
                    MessageBox.Show("Los Tickets Fueron Modificados Correctamente.");
                }
                else if (success.Equals(1))
                {
                    MessageBox.Show("El Ticket Fue Modificado Correctamente.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message);
            }
            conn.Close();
        }

        //Obtener reporte de ventas de tickets.
        internal static DataTable GetDataTicketReport(this DateTime date1, DateTime date2)
        {
            dbConn db = new dbConn();
            var conn = Connection.MakeConnection(db);
            DataTable dataTable = new DataTable();


            SqlCommand command;
            SqlDataReader InfoList;
            string script;

            try
            {
                conn.Open();

                //Paso 1: Identifcar si existe registrado en una estación.
                script = @"
                            DECLARE @FECHA_INI VARCHAR(10);
                            DECLARE @FECHA_FIN VARCHAR(10);

                            SET @FECHA_INI = '" + date1.ToString("MM/dd/yyyy") + "' " +
                          " SET @FECHA_FIN = '" + date2.ToString("MM/dd/yyyy") + "' " +

                        @"  SELECT   E.DESCRIPCION AS ESTACION
	                                ,B.REFERENCIAS AS PRODUCTO  
	                                ,C.DESCUENTO
      	                            ,CAST(D.PRECIO AS DECIMAL(10, 2)) PRECIO
      	                            ,COUNT(*) AS [TICKETS CONSUMIDOS]                                                                           
      	                            ,CAST(SUM(A.GALONES) AS DECIMAL(10, 2)) AS GALONES
      	                            ,CAST((SUM(A.VALOR) ) AS DECIMAL(10, 2)) AS VENTAS
      	                            ,'TICKETS - SALIDA DE ALMACEN' as DESCRIPCION  

                            FROM TICKETS_EST A 
	                            INNER JOIN PRODUCTOS_EST B  
		                            ON  A.CENTRO = B.CENTRO AND A.COD_PROD = B.COD_PROD
	                            INNER JOIN MANGUERAS_EST C  
		                            ON  A.CENTRO = C.CENTRO AND A.MANGUERA = C.MANGUERA  
                            INNER JOIN TRANS_EST_D D  
		                            ON  A.CENTRO = D.CENTRO AND A.COD_PROD = D.COD_PROD AND 
		                                A.MANGUERA = D.MANGUERA AND A.FECHA = D.FECHA AND A.TURNO = D.TURNO
	                            INNER JOIN CENTRO_DIST_EST E
		                            ON  A.CENTRO = E.CENTRO                    

                            WHERE A.FECHA BETWEEN @FECHA_INI 
   		                                      AND @FECHA_FIN                                                            

                            GROUP BY B.REFERENCIAS
                                    ,D.PRECIO
	                                ,C.DESCUENTO
	                                ,A.CENTRO
	                                ,E.DESCRIPCION

                            ORDER BY A.CENTRO
                                	,D.PRECIO DESC
	                                ,ESTACION ";

                command = new SqlCommand(script, conn);
                command.CommandTimeout = 800;

                InfoList = command.ExecuteReader();
                dataTable.Load(InfoList);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message);
            }

            conn.Close();
            return dataTable;
        }

        //Obtener reporte de ventas globales por niveles de precios.
        internal static DataTable GetDataGlobalSalesReport(this DateTime date1, DateTime date2, string center)
        {
            dbConn db = new dbConn();
            var conn = Connection.MakeConnection(db);
            DataTable dataTable = new DataTable();


            SqlCommand command;
            SqlDataReader InfoList;
            string script;

            try
            {
                conn.Open();

                //Paso 1: Identifcar si existe registrado en una estación.
                script = @"
                             --Inicio Script.

                                -----------------------------------------------------------------------------------------------------
                                -----------------------------------------------COMBUSTIBLE-------------------------------------------
                                -----------------------------------------------------------------------------------------------------

                                DECLARE @CENTRO AS VARCHAR(3);
                                DECLARE @FECHA1 AS VARCHAR(10);
                                DECLARE @FECHA2 AS VARCHAR(10); " +

                                " SET @CENTRO = '" + center + "'; " +
                                " SET @FECHA1 = '" + date1.ToString("MM/dd/yyyy") + "';" +
                                " SET @FECHA2 = '" + date2.ToString("MM/dd/yyyy") + "';" +

                            @"   SELECT 	 SUBSTRING(Producto, 4, 21) AS [Producto]
	                                ,[Precio]
	                                ,[Galones]
                                FROM
                                (
                                SELECT   	PRODUCTO AS [Producto]
		                                ,CONVERT( VARCHAR, Precio, -1) AS [Precio]
		                                ,CONVERT( VARCHAR, SUM(Galones), -1) AS [Galones]
                                FROM (

                                SELECT	
		                                CASE
			
                                            --GASOIL OPTIMO
                                            WHEN MANGUERA IN (
								                                SELECT MANGUERA
								                                FROM MANGUERAS_EST
								                                WHERE CENTRO = @CENTRO
								                                AND COD_PROD_ASIG = 'GO-OPTM'
								                                AND DESCUENTO = '15.0'
								                                AND ESTADO = 'A')				THEN 'A. Gasoil Optimo (-15)'

			                                WHEN MANGUERA IN (
								                                SELECT MANGUERA
								                                FROM MANGUERAS_EST
								                                WHERE CENTRO = @CENTRO
								                                AND COD_PROD_ASIG = 'GO-OPTM'
								                                AND DESCUENTO = '10.0'
								                                AND ESTADO = 'A')				THEN 'B. Gasoil Optimo (-10)'

			                                WHEN MANGUERA IN (
								                                SELECT MANGUERA
								                                FROM MANGUERAS_EST
								                                WHERE CENTRO = @CENTRO
								                                AND COD_PROD_ASIG = 'GO-OPTM'
								                                AND DESCUENTO = '5.0'
								                                AND ESTADO = 'A')				THEN 'C. Gasoil Optimo (-5)'

                                            WHEN MANGUERA IN (
								                                SELECT MANGUERA
								                                FROM MANGUERAS_EST
								                                WHERE CENTRO = @CENTRO
								                                AND COD_PROD_ASIG = 'GO-OPTM'
								                                AND DESCUENTO = '0.0'
								                                AND ESTADO = 'A')				THEN 'D. Gasoil Optimo'

                                             --GASOIL REGULAR
                                            WHEN MANGUERA IN (
								                                SELECT MANGUERA
								                                FROM MANGUERAS_EST
								                                WHERE CENTRO = @CENTRO
								                                AND COD_PROD_ASIG = 'GO-NORM'
								                                AND DESCUENTO = '15.0'
								                                AND ESTADO = 'A')				THEN 'E. Gasoil Regular (-15)'

			                                WHEN MANGUERA IN (
								                                SELECT MANGUERA
								                                FROM MANGUERAS_EST
								                                WHERE CENTRO = @CENTRO
								                                AND COD_PROD_ASIG = 'GO-NORM'
								                                AND DESCUENTO = '10.0'
								                                AND ESTADO = 'A')				THEN 'F. Gasoil Regular (-10)'

			                                WHEN MANGUERA IN (
								                                SELECT MANGUERA
								                                FROM MANGUERAS_EST
								                                WHERE CENTRO = @CENTRO
								                                AND COD_PROD_ASIG = 'GO-NORM'
								                                AND DESCUENTO = '5.0'
								                                AND ESTADO = 'A')				THEN 'G. Gasoil Regular (-5)'

                                            WHEN MANGUERA IN (
								                                SELECT MANGUERA
								                                FROM MANGUERAS_EST
								                                WHERE CENTRO = @CENTRO
								                                AND COD_PROD_ASIG = 'GO-NORM'
								                                AND DESCUENTO = '0.0'
								                                AND ESTADO = 'A')				THEN 'H. Gasoil Regular'

                                            --GASOILA PREMIUM
                                            WHEN MANGUERA IN (
								                                SELECT MANGUERA
								                                FROM MANGUERAS_EST
								                                WHERE CENTRO = @CENTRO
								                                AND COD_PROD_ASIG = 'MOG-PREM'
								                                AND DESCUENTO = '15.0'
								                                AND ESTADO = 'A')				THEN 'I. Gasolina Premium (-15)'

			                                WHEN MANGUERA IN (
								                                SELECT MANGUERA
								                                FROM MANGUERAS_EST
								                                WHERE CENTRO = @CENTRO
								                                AND COD_PROD_ASIG = 'MOG-PREM'
								                                AND DESCUENTO = '10.0'
								                                AND ESTADO = 'A')				THEN 'J. Gasolina Premium (-10)'

			                                WHEN MANGUERA IN (
								                                SELECT MANGUERA
								                                FROM MANGUERAS_EST
								                                WHERE CENTRO = @CENTRO
								                                AND COD_PROD_ASIG = 'MOG-PREM'
								                                AND DESCUENTO = '5.0'
								                                AND ESTADO = 'A')				THEN 'K. Gasolina Premium (-5)'

                                            WHEN MANGUERA IN (
								                                SELECT MANGUERA
								                                FROM MANGUERAS_EST
								                                WHERE CENTRO = @CENTRO
								                                AND COD_PROD_ASIG = 'MOG-PREM'
								                                AND DESCUENTO = '0.0'
								                                AND ESTADO = 'A')				THEN 'L. Gasolina Premium'

                                            --GASOILA REGULAR
                                            WHEN MANGUERA IN (
								                                SELECT MANGUERA
								                                FROM MANGUERAS_EST
								                                WHERE CENTRO = @CENTRO
								                                AND COD_PROD_ASIG = 'MOG-REG'
								                                AND DESCUENTO = '15.0'
								                                AND ESTADO = 'A')				THEN 'N. Gasolina Regular (-15)'

			                                WHEN MANGUERA IN (
								                                SELECT MANGUERA
								                                FROM MANGUERAS_EST
								                                WHERE CENTRO = @CENTRO
								                                AND COD_PROD_ASIG = 'MOG-REG'
								                                AND DESCUENTO = '10.0'
								                                AND ESTADO = 'A')				THEN 'M. Gasolina Regular (-10)'

			                                WHEN MANGUERA IN (
								                                SELECT MANGUERA
								                                FROM MANGUERAS_EST
								                                WHERE CENTRO = @CENTRO
								                                AND COD_PROD_ASIG = 'MOG-REG'
								                                AND DESCUENTO = '5.0'
								                                AND ESTADO = 'A')				THEN 'O. Gasolina Regular (-5)'

                                            WHEN MANGUERA IN (
								                                SELECT MANGUERA
								                                FROM MANGUERAS_EST
								                                WHERE CENTRO = @CENTRO
								                                AND COD_PROD_ASIG = 'MOG-REG'
								                                AND DESCUENTO = '0.0'
								                                AND ESTADO = 'A')				THEN 'P. Gasolina Regular'
			
	                                    END AS [Producto]
	                                  ,MONTH(FECHA) AS [Mes]
	                                  ,DATENAME(YEAR, FECHA)  AS [year]
	                                  ,CAST(PRECIO AS MONEY) AS [Precio]
                                      ,CAST(SUM(LECT_FIN - LECT_INI) AS MONEY) AS [Galones]
                                FROM TRANS_EST_D
                                WHERE   CENTRO = @CENTRO
	                                AND (FECHA >= @FECHA1 AND FECHA  <= @FECHA2)		--11 jun turn 2 2019 - 31 jul 2023
                                GROUP BY MANGUERA
	                                ,DATENAME(YEAR, FECHA)
	                                ,MONTH(FECHA)
                                    ,Precio 

                                ) AS BASE
                                GROUP BY [year]
		                                ,MES
		                                ,PRODUCTO
                                        ,Precio 

                                )
                                BASE


                                -----------------------------------------------------------------------------------------------------
                                -------------------------------------------------TICKETS---------------------------------------------
                                -----------------------------------------------------------------------------------------------------


                                SELECT 	 SUBSTRING(Producto, 4, 21) AS [Producto]
	                                    ,[Galones]
	                                    ,[Tickets]
                                FROM
                                (
                                SELECT   	 PRODUCTO AS [Producto]
		                                    ,CONVERT( VARCHAR, SUM(Galones), -1) AS [Galones]
		                                    ,CONVERT( VARCHAR, SUM(VALOR), -1) AS [Tickets]
                                FROM (

                                SELECT	
		                                CASE
			
                                            --GASOIL OPTIMO
                                            WHEN ME.MANGUERA IN (
								                                SELECT ME.MANGUERA
								                                FROM MANGUERAS_EST
								                                WHERE CENTRO = @CENTRO
								                                AND COD_PROD_ASIG = 'GO-OPTM'
								                                AND DESCUENTO = '15.0'
								                                AND ESTADO = 'A')				THEN 'A. Gasoil Optimo (-15)'

			                                WHEN ME.MANGUERA IN (
								                                SELECT ME.MANGUERA
								                                FROM MANGUERAS_EST
								                                WHERE CENTRO = @CENTRO
								                                AND COD_PROD_ASIG = 'GO-OPTM'
								                                AND DESCUENTO = '10.0'
								                                AND ESTADO = 'A')				THEN 'B. Gasoil Optimo (-10)'

			                                WHEN ME.MANGUERA IN (
								                                SELECT ME.MANGUERA
								                                FROM MANGUERAS_EST
								                                WHERE CENTRO = @CENTRO
								                                AND COD_PROD_ASIG = 'GO-OPTM'
								                                AND DESCUENTO = '5.0'
								                                AND ESTADO = 'A')				THEN 'C. Gasoil Optimo (-5)'

                                            WHEN ME.MANGUERA IN (
								                                SELECT ME.MANGUERA
								                                FROM MANGUERAS_EST
								                                WHERE CENTRO = @CENTRO
								                                AND COD_PROD_ASIG = 'GO-OPTM'
								                                AND DESCUENTO = '0.0'
								                                AND ESTADO = 'A')				THEN 'D. Gasoil Optimo'

                                             --GASOIL REGULAR
                                            WHEN ME.MANGUERA IN (
								                                SELECT ME.MANGUERA
								                                FROM MANGUERAS_EST
								                                WHERE CENTRO = @CENTRO
								                                AND COD_PROD_ASIG = 'GO-NORM'
								                                AND DESCUENTO = '15.0'
								                                AND ESTADO = 'A')				THEN 'E. Gasoil Regular (-15)'

			                                WHEN ME.MANGUERA IN (
								                                SELECT ME.MANGUERA
								                                FROM MANGUERAS_EST
								                                WHERE CENTRO = @CENTRO
								                                AND COD_PROD_ASIG = 'GO-NORM'
								                                AND DESCUENTO = '10.0'
								                                AND ESTADO = 'A')				THEN 'F. Gasoil Regular (-10)'

			                                WHEN ME.MANGUERA IN (
								                                SELECT ME.MANGUERA
								                                FROM MANGUERAS_EST
								                                WHERE CENTRO = @CENTRO
								                                AND COD_PROD_ASIG = 'GO-NORM'
								                                AND DESCUENTO = '5.0'
								                                AND ESTADO = 'A')				THEN 'G. Gasoil Regular (-5)'

                                            WHEN ME.MANGUERA IN (
								                                SELECT ME.MANGUERA
								                                FROM MANGUERAS_EST
								                                WHERE CENTRO = @CENTRO
								                                AND COD_PROD_ASIG = 'GO-NORM'
								                                AND DESCUENTO = '0.0'
								                                AND ESTADO = 'A')				THEN 'H. Gasoil Regular'

                                            --GASOILA PREMIUM
                                            WHEN ME.MANGUERA IN (
								                                SELECT ME.MANGUERA
								                                FROM MANGUERAS_EST
								                                WHERE CENTRO = @CENTRO
								                                AND COD_PROD_ASIG = 'MOG-PREM'
								                                AND DESCUENTO = '15.0'
								                                AND ESTADO = 'A')				THEN 'I. Gasolina Premium (-15)'

			                                WHEN ME.MANGUERA IN (
								                                SELECT ME.MANGUERA
								                                FROM MANGUERAS_EST
								                                WHERE CENTRO = @CENTRO
								                                AND COD_PROD_ASIG = 'MOG-PREM'
								                                AND DESCUENTO = '10.0'
								                                AND ESTADO = 'A')				THEN 'J. Gasolina Premium (-10)'

			                                WHEN ME.MANGUERA IN (
								                                SELECT ME.MANGUERA
								                                FROM MANGUERAS_EST
								                                WHERE CENTRO = @CENTRO
								                                AND COD_PROD_ASIG = 'MOG-PREM'
								                                AND DESCUENTO = '5.0'
								                                AND ESTADO = 'A')				THEN 'K. Gasolina Premium (-5)'

                                            WHEN ME.MANGUERA IN (
								                                SELECT ME.MANGUERA
								                                FROM MANGUERAS_EST
								                                WHERE CENTRO = @CENTRO
								                                AND COD_PROD_ASIG = 'MOG-PREM'
								                                AND DESCUENTO = '0.0'
								                                AND ESTADO = 'A')				THEN 'L. Gasolina Premium'

                                            --GASOILA REGULAR
                                            WHEN ME.MANGUERA IN (
								                                SELECT ME.MANGUERA
								                                FROM MANGUERAS_EST
								                                WHERE CENTRO = @CENTRO
								                                AND COD_PROD_ASIG = 'MOG-REG'
								                                AND DESCUENTO = '15.0'
								                                AND ESTADO = 'A')				THEN 'N. Gasolina Regular (-15)'

			                                WHEN ME.MANGUERA IN (
								                                SELECT ME.MANGUERA
								                                FROM MANGUERAS_EST
								                                WHERE CENTRO = @CENTRO
								                                AND COD_PROD_ASIG = 'MOG-REG'
								                                AND DESCUENTO = '10.0'
								                                AND ESTADO = 'A')				THEN 'M. Gasolina Regular (-10)'

			                                WHEN ME.MANGUERA IN (
								                                SELECT ME.MANGUERA
								                                FROM MANGUERAS_EST
								                                WHERE CENTRO = @CENTRO
								                                AND COD_PROD_ASIG = 'MOG-REG'
								                                AND DESCUENTO = '5.0'
								                                AND ESTADO = 'A')				THEN 'O. Gasolina Regular (-5)'

                                            WHEN ME.MANGUERA IN (
								                                SELECT ME.MANGUERA
								                                FROM MANGUERAS_EST
								                                WHERE CENTRO = @CENTRO
								                                AND COD_PROD_ASIG = 'MOG-REG'
								                                AND DESCUENTO = '0.0'
								                                AND ESTADO = 'A')				THEN 'P. Gasolina Regular'
			
	                                    END AS [Producto]

	    	                                ,DATENAME(YEAR, FECHA)  AS [year]
		                                ,MONTH(FECHA) AS [Mes]
		                                ,SUM(TE.GALONES)AS [Galones]
		                                ,SUM(TE.VALOR) AS [Valor]
                                FROM TICKETS_EST AS TE
	                                INNER JOIN MANGUERAS_EST ME
		                                ON  TE.MANGUERA = ME.MANGUERA
		                                AND TE.CENTRO = ME.CENTRO
		                                AND TE.COD_PROD = ME.COD_PROD_ASIG
                                WHERE ME.CENTRO = @CENTRO
                                AND (TE.FECHA >= @FECHA1 AND TE.FECHA  <= @FECHA2)	
                                GROUP BY ME.MANGUERA
	                                ,DATENAME(YEAR, FECHA)
	                                ,MONTH(FECHA)
                                ) AS BASE
                                GROUP BY  [year]
	                                 ,MES
	                                 ,PRODUCTO
                                )
                                BASE


                                --Fin Script. ";

                command = new SqlCommand(script, conn);
                command.CommandTimeout = 2000;

                InfoList = command.ExecuteReader();
                dataTable.Load(InfoList);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message);
            }

            conn.Close();
            return dataTable;
        }

        //Generar reporte de ventas.
        internal static void GenerateReportToExcel(this DateTime date1, DateTime date2, bool isTicketReport = true, string center = "0")
        {
            var datatable = new DataTable();

            try
            {
                datatable = (isTicketReport) ? date1.GetDataTicketReport(date2) : date1.GetDataGlobalSalesReport(date2, center);

                using (SaveFileDialog saveFile = new SaveFileDialog() { Filter = "Excel Workbook | *.xlsx" })
                {
                    if (saveFile.ShowDialog() == DialogResult.OK)
                    {
                        try
                        {
                            using (XLWorkbook workbook = new XLWorkbook())
                            {
                                workbook.Worksheets.Add(datatable, "Reporte de Ventas Global");
                                workbook.SaveAs(saveFile.FileName);
                            }

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error : " + ex.Message);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message);
            }
        }

        //Generar Menú.
        internal static DataTable GetMenuInfo(string precioId)
        {
            var dataTable = new DataTable();
            var conn = Connection.toDatabase;

            SqlCommand command;
            SqlDataReader InfoList;
            string script;

            try
            {
                conn.Open();
                script = string.Format(@"

                         SELECT    TBPRODUCTO.PRD_CODIGO [id]
                                  ,PRD_DESCR [name]
                                  ,CASE
                                      WHEN PRD_IMP_PORC != 0 THEN
                                      CAST(((PRD_IMP_PORC / 100) * UPR_PRECIO) AS DECIMAL(9,2))
                                      ELSE
                                      0
                                  END [tax]
                                  ,CAST(UPR_PRECIO AS DECIMAL(9,2)) [price]
                                  ,CFP_NOMBRE [priceName]
                              ,_base64 AS imageBase64
                              FROM TBPRODUCTO
                              CROSS APPLY (select PRD_FOTO as '*' for xml path('')) T (_base64)
                              INNER JOIN TBUNDPRCPRD
                                  ON TBPRODUCTO.PRD_COMPANIA = TBUNDPRCPRD.UPR_COMPANIA
                                      AND TBPRODUCTO.PRD_CODIGO = TBUNDPRCPRD.PRD_CODIGO
					          INNER JOIN TBCFGPRECIO
                                  ON TBUNDPRCPRD.CFP_CODIGO = TBCFGPRECIO.CFP_CODIGO
                                      AND TBUNDPRCPRD.UPR_COMPANIA = TBCFGPRECIO.CFP_COMPANIA
							          AND CFP_ESTADO = 1
                              WHERE TBPRODUCTO.PRD_COMPANIA = '2'
                                      AND PRD_ESTADO = 'A'
                                      AND TBCFGPRECIO.CFP_CODIGO = '{0}' 
                    ", precioId);
                command = new SqlCommand(script, conn);
                command.CommandTimeout = 2000;
                InfoList = command.ExecuteReader();
                dataTable.Load(InfoList);
            }
            catch (Exception e)
            {
                var message = (e.GetType().Name == "SqlException") ? "Error de Conexión" : e.Message;
                MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            conn.Close();
            return dataTable;
        }
        internal static void GenerateMenuReport(this DataTable dtInfo)
        {
            try
            {
                var Reporte = (ReportForm)Application.OpenForms["ReportForm"];

                if (Reporte == null)
                {
                    Reporte = new ReportForm();
                    Reporte.Show();
                }
                else
                    Reporte.Visible = true;

                //NOTA: Se debe crear una tabla en el dataSet con estos mismos campos para asignar cada columna
                //      dentro del reporte. Una vez creada... se puede borrar la tabla en el dataset.
                //      existe una tabla llamada TablaNovedades1 solo quitar el 1 para asignar nuevamente.

                //Asignación.
                Reporte.rptMenuReport.SetDisplayMode(DisplayMode.Normal);
                Reporte.rptMenuReport.RefreshReport();

                // Cargando los datos.
                //Gasolina Premium.
                DSMenu dsConsult = new DSMenu();
                dtInfo.TableName = "dtMenu";
                dsConsult.Tables.Add(dtInfo);
                ReportDataSource dataSource1 = new ReportDataSource("dtMenu", dsConsult.Tables["dtMenu"]);

                //Aplicación.
                Reporte.CloseReports();
                Reporte.rptMenuReport.Visible = true;
                Reporte.rptMenuReport.LocalReport.DataSources.Clear();
                Reporte.rptMenuReport.LocalReport.DataSources.Add(dataSource1);
                Reporte.rptMenuReport.LocalReport.Refresh();
                Reporte.rptMenuReport.RefreshReport();
                Reporte.rptMenuReport.SetDisplayMode(DisplayMode.PrintLayout);
                Reporte.rptMenuReport.ZoomMode = ZoomMode.Percent;
                Reporte.rptMenuReport.ZoomPercent = 100;
                dsConsult.Tables.Remove(dtInfo);


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        //Es intenger.
        internal static bool ItsInt(this string text) => int.TryParse(text, out int val);

    }


}

