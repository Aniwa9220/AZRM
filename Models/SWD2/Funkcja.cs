using System;
using System.Collections.Generic;

namespace AZRM2023v1.Models.SWD2
{
    public partial class Funkcja
    {
        public Funkcja()
        {
            AspNetUsers = new HashSet<AspNetUsers>();
        }

        public int Idfunkcji { get; set; }
        public string? Stanowisko { get; set; }

        public virtual ICollection<AspNetUsers> AspNetUsers { get; set; }
    }
}
