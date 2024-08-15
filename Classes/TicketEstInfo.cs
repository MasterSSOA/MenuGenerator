using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenuGenerator.Classes
{
    public class TicketEstInfo
    {
        //Properties.
        public string Number { get; set; }
        public string Value { get; set; }
        public string Station { get; set; }
        public string DateRegistered { get; set; }
        public string Turn { get; set; }
        public string Employee { get; set; }

        //Constructor.
        public TicketEstInfo(string Number, string Value, string Station, string DateRegistered, string Turn, string Employee)
        {
            this.Number = Number;
            this.Value = Value;
            this.Station = Station;
            this.DateRegistered = DateRegistered;
            this.Turn = Turn;
            this.Employee = Employee;
        }
    }
}
