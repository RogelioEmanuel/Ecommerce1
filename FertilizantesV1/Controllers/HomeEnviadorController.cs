using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FertilizantesV1.Controllers
{
    [Authorize]
    public class HomeEnviadorController : Controller
    {
        // GET: HomeEnviador
        public ActionResult Index()
        {
            return View();
        }
    }
}