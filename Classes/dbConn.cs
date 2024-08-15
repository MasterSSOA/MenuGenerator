using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MenuGenerator.Properties;

namespace MenuGenerator.Classes
{
    public class dbConn
    {
        //Atributes.
        public string server { get; set; } = Settings.Default.Server;
        public string database { get; set; } = Settings.Default.DataBase;
        public string user { get; set; } = Settings.Default.User;
        public string pass { get; set; } = Settings.Default.Pass;

        //Constructor.
        public dbConn(string server = "0", string database = "0", string user = "0", string pass = "0")
        {
            if (!(server.Equals("0") && database.Equals("0") && user.Equals("0") && pass.Equals("0")))
            {

                this.server = server;
                this.database = database;
                this.user = user;
                this.pass = pass;
            }
        }
    }
}
