using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using Fertitec.Models;

namespace Fertitec.Controllers
{
    [Authorize]
    public class PagoController : Controller
    {
        private contextFertitec db = new contextFertitec();
        private string NumConfirPago; 

        // GET: Pago
        public ActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult CrearOrden()
        {
            if (!User.Identity.IsAuthenticated)
            {
                Session["CrearOrden"] = "pend";
                return RedirectToAction("Login", "Account");
            }

            string correo = User.Identity.Name;

            //var db = new contextFertitec();
            string fechaCreacion = DateTime.Today.ToShortDateString();
            string fechaProbEntrega = DateTime.Today.AddDays(3).ToShortDateString();
            var cliente = (from c in db.Clientes
                           where c.email == correo
                           select c).ToList().FirstOrDefault();

            Session["dirCliente"] = cliente.calle + " " + cliente.colonia + " " + cliente.estado;
            Session["fechaOrden"] = fechaCreacion;
            Session["fPEntreg"] = fechaProbEntrega;

            if (cliente.num_tarjeta.StartsWith("4"))
                Session["tTarj"] = "1";
            if (cliente.num_tarjeta.StartsWith("5"))
                Session["tTarj"] = "2";
            if (cliente.num_tarjeta.StartsWith("3"))
                Session["tTarj"] = "3";
            Session["nTarj"] = cliente.num_tarjeta;
            return View();
        }
        public ActionResult Pagar(string tipoPago)
        {
            string correo = User.Identity.Name;


            DateTime fechaCreacion = DateTime.Today;
            DateTime fechaProbEntrega = fechaCreacion.AddDays(3);
            var cliente = (from c in db.Clientes
                           where c.email == correo
                           select c).ToList().FirstOrDefault();
            int idClient = cliente.idCliente;

            if (tipoPago.Equals("T"))
            {
                if (!validaPago(cliente))
                {
                    return RedirectToAction("pagoNoAceptado");
                }
                else
                {
                    var dirEnt = (from d in db.dirEntregas
                                  where d.idCliente == cliente.idCliente
                                  select d).ToList().FirstOrDefault();

                    int idDir = dirEnt.id_dirEnt;
                    return RedirectToAction("pagoAceptado", routeValues: new { idC = idClient, idD = idDir });
                }
            }
            if (tipoPago.Equals("P"))
            {
                var dirEnt = (from d in db.dirEntregas
                              where d.idCliente == cliente.idCliente
                              select d).ToList().FirstOrDefault();

                int idDir = dirEnt.id_dirEnt;
                validaPago(cliente);
                return RedirectToAction("pagoPaypal", routeValues: new { idC = idClient, idD = idDir });
            }
                return View();
            }

            public ActionResult pagoNoAceptado()
            {
                return View();
            }
        public ActionResult pagoAceptado(int idC, int idD)
        {
            //Aceptando el pago creamos los datos de la orden y orden producto 
            Orden orden_cliente = new Orden();
            int id = 0;
            if (!(db.Orden.Max(o => (int?)o.idOrden) == null))
            {
                id = db.Orden.Max(o => o.idOrden);
            }
            else
            {
                id = 0;
            }
            id++;
            orden_cliente.idOrden = id;
            orden_cliente.fecha_creacion_ = DateTime.Today;
            orden_cliente.num_confirmacion = Session["nConfirma"].ToString();
            var carro = Session["cart"] as List<Item>;
            var total = carro.Sum(item => item.Product.precio * item.Cantidad);
            orden_cliente.Total = Convert.ToDecimal(total);
            orden_cliente.idCliente = idC;
            orden_cliente.id_dirEnt = idD;
            db.Orden.Add(orden_cliente);
            db.SaveChanges();

            Orden_Producto ordenProd;
            List<Orden_Producto> listaProdOrd = new List<Orden_Producto>();
            foreach (Item linea in carro)
            {
                ordenProd = new Orden_Producto();
                ordenProd.idOrden = orden_cliente.idOrden;
                ordenProd.idProducto = linea.Product.idProducto;
                ordenProd.cantidad = linea.Cantidad;
                db.Orden_Producto.Add(ordenProd);
            }
            db.SaveChanges();

            Session["cart"] = null;
            Session["nConfirma"] = null;
            Session["itemTotal"] = 0;
            return View();
            }
        public ActionResult pagoPaypal(int idC, int idD)
        {
            Session["idDir"] = idC;
            Session["idClient"] = idD;

            return View();
        }
        public ActionResult pagandoPaypal(int idC, int idD)
        {
            Session["idDir"] = idC;
            Session["idClient"] = idD;

            return View();
        }

        private bool validaPago(Clientes cliente)
        {
            bool retorna = true;
            //Se debe comunicar con el sistema de pago enviando los datos de la tarjeta 
            //y los datos del cliente, en este ejemplo falta el codigo postal, tambien 
            //debe inlcuirse
            //Y el sistema de pago nos regresa un numero de confirmacion 
            // vamos a simularlo usando un numero random pero seguro 
            int randomvalue;

            using (RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider())
            {
                byte[] val = new byte[6];
                crypto.GetBytes(val);
                randomvalue = BitConverter.ToInt32(val, 1);
            }

            NumConfirPago = Math.Abs(randomvalue * 1000).ToString();
            Session["nConfirma"] = NumConfirPago;
            return retorna;
        }
        }
    }