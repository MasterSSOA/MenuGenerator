using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Office2013.Excel;
using Microsoft.VisualBasic.ApplicationServices;
using MenuGenerator.Properties;

namespace MenuGenerator.Classes
{
    public static class Connection
    {

        //public static SqlConnection toDatabase = new SqlConnection(@"data source=192.168.0.2;initial catalog=dbSysEstaciones;
        //                                                             persist security info=True;user id=sa;password=Master2008;encrypt=False;
        //                                                             MultipleActiveResultSets=True;App=EntityFramework");

        public static SqlConnection toDatabase = new SqlConnection(@"data source=MASTERSERVER;initial catalog=SYMA;
                                                                     persist security info=True;user id=sa;password=Pepepep@2313;encrypt=False;
                                                                     MultipleActiveResultSets=True;App=EntityFramework");


        public static SqlConnection MakeConnection(dbConn db)
        {

            string key = "qw5erty2uiofas1dfghjk3adsf6zc8fg";
            var server = db.server.DesencryptData(key);
            var database = db.database.DesencryptData(key);
            var user = db.user.DesencryptData(key);
            var pass = db.pass.DesencryptData(key);

            return new SqlConnection(@"data source=" + server + ";initial catalog=" + database + "; " +
                                                                  "persist security info=True;user id=" + user + ";password=" + pass + ";" +
                                                                  "encrypt=False;  MultipleActiveResultSets=True;App=EntityFramework");
        }
    }
}
