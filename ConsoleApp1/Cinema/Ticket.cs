using app.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app.Cinema
{
    public class Ticket : ITicket 
    {
        #region Public members
        public string? Movie { get; set; }
        public DateTime ScreeningTime { get; set; }
        public string? CustomerName { get; set; }
        public int Row { get; set; }
        public int Number { get; set; }
        public int TicketNumber { get; set; }
        #endregion
    }
}
