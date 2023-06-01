using AZRM2023v1.Models.Domain;
using AZRM2023v1.Models.SWD2;
using AZRM2023v1.Repositories.Implementation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Data;

namespace AZRM2023v1.Controllers
{
    public class KierownikZRMController : Controller
    {
        public IActionResult Error()
        {
            return View();  
        }
            public IActionResult Display()
        {
            try
            {
                var name = TempData["UserName"].ToString();
                TempData.Keep("UserName");
                var manager = new KierownikManager();
                var index = manager.TakeIntSkład(name);
                if (index == null)
                { return RedirectToAction("Error"); }
                else
                {
                    return View(index);
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

                    context.Zgłoszenies.Update(call);
                    context.SaveChanges();
                }
                return View(call);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error");
            }
        }
        public IActionResult Kontakt()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Aktualizacja(int id)
        {
            try
            {

                var manager = new KierownikManager();
                var zg = new RegModel();
                var index = manager.GetZlecenie(id);
                var pacjent = manager.GetPax(index.Idpacjenta);
                var karta = manager.GetKarta(index.Idkartychoroby);
                var szpital = manager.GetSzpital(index.Idszpitala);
                karta.IDpacjenta = pacjent.Idpacjenta;
                
                zg.karta = karta;
                zg.pacjent = pacjent;
                zg.zgłoszenie = index;
         
                return View(zg);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error");
            }
            }

        [HttpPost]
        public ActionResult Aktualizacja(RegModel index)   
        {
            try
            {
                var manager = new KierownikManager();

                manager.UpdatePax(index.pacjent);
                manager.UpdateKarta(index.karta);

                manager.UpdateZlecenie(index.zgłoszenie);



                return RedirectToAction("Display");

            }
            catch (Exception ex)
            {
                return RedirectToAction("Error");
            }
        }


        public ActionResult Zamknij(int id) 
        {
            try
            {
                var manager = new DyspozytorManager();
                var call = manager.GetZlecenie(id);
                call.Datazamkniecia = DateTime.Now;
                manager.UpdateZlecenie(call);
                return RedirectToAction("Display");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error");
            }
        }

    }
    
    
}
