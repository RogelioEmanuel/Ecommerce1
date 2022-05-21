﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fertitec.Models
{
    public class PedidoCliente
    {
        private contextFertitec db = new contextFertitec();
        private List<Orden_Producto> detalle_orden;

        public PedidoCliente()
        {
            detalle_orden = db.Orden_Producto.ToList();
        }

        public Orden Orden
        {
            get;
            set;
        }
        public string Fecha
        {
            get;
            set;
        }
        public string envio
        {
            get;
            set;
        }
        public string status
        {
            get;
            set;
        }
        public string Total
        {
            get;
            set;
        }

        public List<Orden_Producto> ordenProductos
        {
            get;
            set;
        }
    }
}