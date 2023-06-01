using AZRM2023v1.Models.Domain;

namespace AZRM2023v1.Models.SWD2
{
    public class RegModel
    {

        public Pacjent pacjent { get; set; }
        public Kartachoroby karta { get; set; }
        public Zgłoszenie zgłoszenie { get; set; }

        public virtual Szpital szpital { get; set;}
        public virtual Icd10 icd { get; set; }

        public List<Szpital> szpitalist()
        {
            
            var context = new SWD2Context();
            var view = context.Szpitals.ToList();

            return view;
        }
        public List<Icd10> indexlist()
        {

            var context = new SWD2Context();
            var view = context.Icd10s.ToList();

            return view;
        }
    }
}
