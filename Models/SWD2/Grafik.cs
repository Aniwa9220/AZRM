using System;
using System.Collections.Generic;

namespace AZRM2023v1.Models.SWD2
{
    public partial class Grafik
    {
        public Grafik()
        {
            Zgłoszenies = new HashSet<Zgłoszenie>();
        }

        public int Lp { get; set; }
        public int Idskład { get; set; }
        public DateTime Dzieńdyżuru { get; set; }
        public int Zmiana { get; set; }
        public int Status { get; set; }
        public virtual ICollection<Zgłoszenie> Zgłoszenies { get; set; }
    }
}
