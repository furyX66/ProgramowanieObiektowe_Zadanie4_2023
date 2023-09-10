using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using app.Interfaces;

namespace app.Cinema
{
    public class Seat : ISeat
    {
        #region Public members
        public int Row { get; set; }
        public int Number { get; set; }
        public bool IsAvailable { get; set; }
        #endregion
    }
}
