using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MinimartApi.Models
{


    public class StoreTimeTableModel
    {
        public int MinimartName { get; set; }
        public String WorkingDay { get; set; }
        //
        public DateTime OpeningTime { get; set; }
        public DateTime ClosingTime { get; set; }

    }

    public class StoreTimeTable : StoreTimeTableModel
    { 
        public int Id_Minimart { get; set; }
    }
}