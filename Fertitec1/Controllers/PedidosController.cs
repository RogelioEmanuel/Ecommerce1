using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Fertitec.Models;
namespace Fertitec.Controllers
{
    [Authorize]
    public class PedidosController : Controller
    {
        contextFertitec db = new contextFertitec();
        // GET: Pedidos
        public ActionResult Index()
        {
            string correo = User.Identity.Name;
            Clientes cl = (from c in db.Clientes
                           where c.email == correo
                           select c
                           ).ToList().FirstOrDefault();

            int id = cl.idCliente;

            var query = from o in db.Orden
                        where o.idCliente == id
                        orderby o.fecha_creacion_ ascending
                        select o;

            List<Orden> ordenes = query.ToList();

            List<PedidoCliente> pedidos = new List<PedidoCliente>();
            PedidoCliente pedido;
            List<Orden_Producto> ordPed;
            List<ItemPedido> itemPed = new List<ItemPedido>();

            ItemPedido iPed;

            foreach (Orden o in ordenes)
            {
                pedido = new PedidoCliente();
                pedido.Orden = o;
                pedido.Fecha = o.fecha_creacion_.ToShortDateString();
                if (o.fecha_envio.HasValue)
                {
                    pedido.envio = o.fecha_envio.GetValueOrDefault().ToShortDateString();
                }
                else
                {
                    pedido.envio = "Proximamente";
                }
                if (o.fecha_entrega.HasValue)
                {
                    pedido.status = o.fecha_entrega.GetValueOrDefault().ToShortDateString();
                }
                else
                {
                    pedido.status = "Sin entregar";
                }
                pedido.Total = o.Total.ToString();
                pedidos.Add(pedido);
                ordPed = (from oP in db.Orden_Producto
                          where oP.idOrden == o.idOrden
                          select oP).ToList();
                pedido.ordenProductos = ordPed;
                foreach(Orden_Producto op in ordPed)
                {
                    iPed = new ItemPedido();
                    iPed.idOrd = op.idOrden;
                    iPed.Product = db.Productos.First(p => p.idProducto == op.idProducto);
                    iPed.Cantidad = Convert.ToInt32(op.cantidad);
                    itemPed.Add(iPed);
                }
            }
            Session["misPedidos"] = pedidos;
            Session["Pedido"] = itemPed;


            return View();
        }
    }
}