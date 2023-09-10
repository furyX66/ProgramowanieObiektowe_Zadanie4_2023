using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app.Cinema
{
    public class CinemaHall //Describes cinema hall for every avaliable movie
    {
        #region Public members
        public int HallNumber { get; set; }
        public List<Seat>? SeatList { get; set; }
        public DateTime ScreeningTime { get; set; }
        public string? Movie { get; set; }
        #endregion
    }
}
