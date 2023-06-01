using AZRM2023v1.Models.Domain;
using System;
using System.Collections.Generic;

namespace AZRM2023v1.Models.SWD2
{
    public class ZgłoszenieASAP : Zgłoszenie
    {
        public Zgłoszenie zgłoszenie { get; set; }


        public virtual Grafik Grafik { get; set; } = null!;
        public virtual IEnumerable<Grafik> Grafiki { get; set; } = null!;

      
        public virtual IEnumerable<Zgłoszenie> Głos { get; set; } 
        public virtual Kartachoroby IdkartychorobyNavigation { get; set; } = null!;
        public virtual Pacjent? IdpacjentaNavigation { get; set; }
        public virtual Szpital? IdszpitalaNavigation { get; set; }

        public List<Zgłoszenie> get24()
        {
            var Data = DateTime.Now.ToString("yyyy-MM-dd");

            var context = new SWD2Context();
            var view = context.Zgłoszenies.Where(p => p.Datazgłoszenia.ToString() == Data && p.Datazamkniecia == null).ToList();
            return view;
        }

        public List<Grafik> squadlist()
        {
            var Data = DateTime.Now.ToString("yyyy-MM-dd");
            var context = new SWD2Context();
            var view = context.Grafiks.Where
                (p => p.Dzieńdyżuru.ToString() == Data && p.Status != 1 && p.Idskład!=100).Distinct().OrderBy(p => p.Idskład).ToList();

            return view;
        }


    }
}
