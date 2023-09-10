using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app.Cinema
{
    public class Screening
    {
        #region Public Members
        public string Movie { get; set; }
        public List<DateTime> DateTimeList { get; set; }
        #endregion
    }
}
