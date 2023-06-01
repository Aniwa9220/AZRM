using System;
using System.Collections.Generic;

namespace AZRM2023v1.Models.SWD2
{
    public partial class Pacjent
    {
        public Pacjent()
        {
            Kartachorobies = new HashSet<Kartachoroby>();
            Zgłoszenies = new HashSet<Zgłoszenie>();
        }

        public int Idpacjenta { get; set; }
        public string? Imie { get; set; }
        public string? Nazwisko { get; set; }

        public virtual ICollection<Kartachoroby> Kartachorobies { get; set; }
        public virtual ICollection<Zgłoszenie> Zgłoszenies { get; set; }
    }
}
