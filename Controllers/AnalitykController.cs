using AZRM2023v1.Repositories.Implementation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace AZRM2023v1.Controllers
{
    [Authorize(Roles = "Analityk")]
  
    public class AnalitykController : Controller
    {


        public IActionResult Error()
        {
            return View();
        }

        public IActionResult Display()
        {
            return View();
        }

        public IActionResult kadra()
        {
            return View();
        }

        public IActionResult statICD10()
        {
            return View();
        }

        public IActionResult statPrac()
        {
            return View();
        }

        public IActionResult NN()
        {
            try
            {
                var manager = new AnalitykManager();
                var nn = manager.GetNN();

                return View(nn);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error");
            }
        }

        public IActionResult ALL()
        {
            try
            {
                var manager = new AnalitykManager();
                var nn = manager.GetAll();

                return View(nn);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error");
            }
        }

        public IActionResult SA()
        {
            try
            {
                var manager = new AnalitykManager();
                var sa = manager.GetSA();

                return View(sa);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error");
            }
        }

        public IActionResult PracRezerw()
        {
            try
            {
                var manager = new AnalitykManager();
                var pr = manager.GetPR();

                return View(pr);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error");
            }
        }

        public IActionResult obsada()
        {
            try
            {
                var manager = new AnalitykManager();
                var ob = manager.GetOB();

                return View(ob);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error");
            }
        }

        public IActionResult HistTransp()
        {
            try
            {
                var manager = new AnalitykManager();
                var ht = manager.GetHT();

                return View(ht);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error");
            }
        }

        public IActionResult intPrac()
        {
            try
            {
                var manager = new AnalitykManager();
                var ip = manager.GetIleP();

                return View(ip);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error");
            }
        }


        public IActionResult pracPrac ()
        {
            try
            {
                var manager = new AnalitykManager();
                var pp = manager.GetPP();

                return View(pp);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error");
            }
        }

        public IActionResult ilePax()
        {
            try
            {
                var manager = new AnalitykManager();
                var ppax = manager.GetPax();

                return View(ppax);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error");
            }
        }

        public IActionResult HistZgłoszeń()
        {
            try
            {
                var manager = new AnalitykManager();
                var zg = manager.GetZG();

                return View(zg);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error");
            }
        }

        public IActionResult SPRM()
        {
            try
            {
                var manager = new AnalitykManager();
                var sprm = manager.GetSPRM();

                return View(sprm);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error");
            }
        }

        public IActionResult ARCH()
        {

            return View();
        }
    }
}
