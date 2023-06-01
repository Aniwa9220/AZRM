using System;
using System.Collections.Generic;

namespace AZRM2023v1.Models.SWD2
{
    public partial class Szpital
    {
        public Szpital()
        {
            Zgłoszenies = new HashSet<Zgłoszenie>();
        }

        public int Idszpitala { get; set; }
        public string? Nazwa { get; set; }
        public string? Adres { get; set; }

        public virtual ICollection<Zgłoszenie> Zgłoszenies { get; set; }
    }
}
