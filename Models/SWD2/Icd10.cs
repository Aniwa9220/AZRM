using System;
using System.Collections.Generic;

namespace AZRM2023v1.Models.SWD2
{
    public partial class Icd10
    {
        public Icd10()
        {
            Kartachorobies = new HashSet<Kartachoroby>();
        }

        public string Idicd10 { get; set; } = null!;
        public string Opis { get; set; } = null!;

        public virtual ICollection<Kartachoroby> Kartachorobies { get; set; }
    }
}
