using AZRM2023v1.Models.Domain;
using AZRM2023v1.Models.DTO;
using AZRM2023v1.Models.SWD2;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using System.Data;

namespace AZRM2023v1.Repositories.Implementation
{
    public class KierownikManager : ZgłoszenieASAP
    {
        public KierownikManager() { }

        public ZgłoszenieASAP GetInt(int id)
        {

            var context = new SWD2Context();     
            ZgłoszenieASAP zg = new();
            var view = zg.get24();
            
            zg.Głos = view.Where(s => s.Idskład == id).ToList(); ;
            return zg;
        
        }

        public ZgłoszenieASAP TakeIntSkład(string name)
        {
            
            
                var context = new DataBaseContext();
                var context1 = new SWD2Context();
                var model = context.Users.SingleOrDefault(p => p.UserName == name);
                var Data = DateTime.Now;
                var Idsquad = context1.Składs.SingleOrDefault(p => p.Idpracownika == model.Idpracownika && p.Dzieńpracy == Data);
            if (Idsquad == null)
            { return null;}
            else
            {
                var manager = new KierownikManager();
                var index = manager.GetInt(Idsquad.Idskład);

                return index;

            }

        }

        public Zgłoszenie GetZlecenie(int id)
        {
            var context = new SWD2Context();
            var call = context.Zgłoszenies.SingleOrDefault(x => x.Idzgłoszenia == id);

            return call;
        }
        [HttpPost]
        public KierownikManager UpdateZlecenie(Zgłoszenie call)
        {
            using (var context = new SWD2Context())
            {
                context.Zgłoszenies.Update(call);
                context.SaveChanges();
            }
            return this;
        }

        public Pacjent GetPax(int id)
        {
            using (var context = new SWD2Context())
            {
              var pax =  context.Pacjents.SingleOrDefault(x=>x.Idpacjenta == id);
              return pax;
            }
            

        }

        [HttpPost]
        public KierownikManager UpdatePax(Pacjent pacjent)
        {
            using (var context = new SWD2Context())
            {
                context.Pacjents.Update(pacjent);
                context.SaveChanges();
            }
            return this;
        }

        public Kartachoroby GetKarta(int id)
        {
            var context = new SWD2Context();
            
            var karta = context.Kartachorobies.SingleOrDefault(x=>x.Idkarty==id);

            return karta;
        }
        [HttpPost]
        public KierownikManager UpdateKarta (Kartachoroby karta)
        {
            using (var context = new SWD2Context())
            {
                context.Kartachorobies.Update(karta);
                context.SaveChanges();
            }
            return this;
        }

        public Szpital GetSzpital(int id)
        {
            using (var context = new SWD2Context())
            {
                var szpital = context.Szpitals.SingleOrDefault(x => x.Idszpitala == id);
                return szpital;

            }
        }

        public KierownikManager UpdateSzpital(Szpital szpital)
        {
            using (var context = new SWD2Context())
            {
                context.Szpitals.Update(szpital);
                context.SaveChanges();
            }
            return this;
        }


    }
}
