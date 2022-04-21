using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FertilizantesV1.Controllers
{
    [Authorize]
    public class EnviosController : Controller
    {
        // GET: Envios
        public ActionResult Envios()
        {
            return View();
        }
        public ActionResult DetallesEnvio()
        {
            ViewBag.Message = "Detalles de envío";

            return View();
        }
        public ActionResult CambioStatus()
        {
            ViewBag.Message = "Cambio de Status";

            return View();
        }
    }
}