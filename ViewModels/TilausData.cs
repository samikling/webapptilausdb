using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppTilausDB.ViewModels
{
    public class TilausData
    {
        public int TilausID { get; set; }
        public int AsiakasID { get; set; }
        public string Toimitusosoite { get; set; }
        public string Postinumero { get; set; }
        public Nullable<DateTime> Tilauspvm { get; set; }
        public Nullable<DateTime> Toimituspvm { get; set; }
        public int TilausriviID { get; set; }
        public int TuoteID { get; set; }
        public int Maara { get; set; }
        public float Ahinta { get; set; }
        public string Nimi { get; set; }

    }
}