using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FertilizantesV1.Models;

namespace FertilizantesV1.Controllers
{
    [Authorize]
    public class UsuarioController : Controller
    {
        private contextTienda db = new contextTienda();
        // GET: Usuario
        public ActionResult Index(string email)
        {
            if (User.Identity.IsAuthenticated)
            {
                string correo = email;
                string rol = "";

                using (db)
                {
                    var query = from st in db.empleado
                                where st.email == correo
                                select st;
                    var lista = query.ToList();
                    if (lista.Count > 0)
                    {
                        var empleado = query.FirstOrDefault<empleado>();
                        string[] nombres = empleado.nombre.ToString().Split(' ');
                        Session["name"] = nombres[0];
                        Session["usr"] = empleado.nombre;
                        rol = empleado.rol.ToString().TrimEnd();
                    }
                    else
                    {
                        var query1 = from st in db.cliente
                                     where st.email == correo
                                     select st;
                        var lista1 = query.ToList();
                        if (lista1.Count > 0)
                        {
                            var cliente = query1.FirstOrDefault<cliente>();
                            string[] nombres = cliente.nombre.ToString().Split(' ');
                            Session["name"] = nombres[0];
                            Session["usr"] = cliente.nombre;
                            rol = "cliente";
                        }
                    }

                }
                if (rol == "Comprador")
                {
                    return RedirectToAction("Index", "HomeEnviador");
                }
                if (rol == "Enviador")
                {
                    return RedirectToAction("Index", "Envios");
                }
                if (rol == "Chateador")
                {
                    return RedirectToAction("Index", "Chat");
                }
                if (rol == "Cliente")
                {
                    return RedirectToAction("Index", "Home");
                }
                if (rol == "Admin")
                {
                    return RedirectToAction("Index", "Admin");
                }
            }
            else
            {
                return RedirectToAction("Index", "HomeEnviador");
            }
            return RedirectToAction("Index", "HomeEnviador");
        }
    }
}