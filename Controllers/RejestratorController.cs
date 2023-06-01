using AZRM2023v1.Models.Domain;
using AZRM2023v1.Models.SWD2;
using AZRM2023v1.Repositories.Implementation;
using Microsoft.AspNetCore.Mvc;

namespace AZRM2023v1.Controllers
{
    public class RejestratorController : Controller
    {

        public IActionResult Error()
        {
            return View();
        }

        public IActionResult PanelRejestracji()
        {
            try
            {
                return View();

            }
            catch (Exception ex)
            {
                return RedirectToAction("Error");
            }
        }

        public IActionResult Rejestracja()
        {
            try
            {
                var manager = new DyspozytorManager();
                var index = manager.GetZlecenias().ToList();
                return View(index);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error");
            }
        }

        [HttpGet]

        public IActionResult Rejestruj()

        {
            try
            { 

                return View();
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error");
            }
        }
        [HttpPost]

        public ActionResult Rejestrujsie(Pacjent pacjent, Kartachoroby karta, Zgłoszenie zgłoszenie) 
        {
            try
            {

                var manager = new DyspozytorManager();

                manager.AddPax(pacjent);



                karta.IDpacjenta = pacjent.Idpacjenta;
                manager.AddKarta(karta);
                zgłoszenie.Datarejestracji = DateTime.Now;
                zgłoszenie.Idpacjenta = karta.IDpacjenta;
                zgłoszenie.Idkartychoroby = karta.Idkarty;
                zgłoszenie.Idskład = 100;
                zgłoszenie.Datazgłoszenia = DateTime.Now;
                manager.AddZlecenie(zgłoszenie);

                return RedirectToAction("Rejestracja");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error");
            }
      

        }

        [HttpGet]
        public IActionResult UpdateZlecenie(int id)

        {
            try
            {

                using (var context = new SWD2Context())
                {

                    var manager = new DyspozytorManager();
                    var call = manager.GetZlecenie(id);

                    return View(call);
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error");
            }
        }
        [HttpPost]
        public IActionResult UpdateZlecenie(Zgłoszenie call)
        {
            try
            {
                using (var context = new SWD2Context())
                {

                    var manager = new DyspozytorManager();
                   


                    var index = manager.UpdateZlecenie(call);
                    return RedirectToAction("Rejestracja");

                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error");
            }
        }

       

    }
}
