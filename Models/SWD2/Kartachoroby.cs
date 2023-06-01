using System;
using System.Collections.Generic;

namespace AZRM2023v1.Models.SWD2
{
    public partial class Kartachoroby
    {
        public Kartachoroby()
        {
            Zgłoszenies = new HashSet<Zgłoszenie>();
        }

        public int Idkarty { get; set; }
        public string? Idicd10 { get; set; }
        public int IDpacjenta { get; set; }

        public virtual Pacjent IDpacjentaNavigation { get; set; } = null!;
        public virtual Icd10? Idicd10Navigation { get; set; }
        public virtual ICollection<Zgłoszenie> Zgłoszenies { get; set; }
    }
}
