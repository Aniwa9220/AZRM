using System;
using System.Collections.Generic;

namespace AZRM2023v1.Models.SWD2
{
    public partial class Zgłoszenie
    {
        public int Idzgłoszenia { get; set; }
        public int Idskład { get; set; }
        public int Idpacjenta { get; set; }
        public DateTime Datazgłoszenia { get; set; }
        public DateTime? Datazamkniecia { get; set; }
        public string? Danezgłoszenia { get; set; }
        public int Idszpitala { get; set; }
        public int Idkartychoroby { get; set; }
        public DateTime? Datarejestracji { get; set; }
        public virtual Grafik Grafik { get; set; } = null!;
      //  public virtual IEnumerable<Grafik> Grafiki { get; set; } = null!;

      //  public virtual IEnumerable<Zgłoszenie> Głos { get; set; } // dopisane 
        public virtual Kartachoroby IdkartychorobyNavigation { get; set; } = null!;
        public virtual Pacjent? IdpacjentaNavigation { get; set; }
        public virtual Szpital? IdszpitalaNavigation { get; set; }
    }
}
