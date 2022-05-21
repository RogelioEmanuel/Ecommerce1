using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Fertitec.Models;

namespace Fertitec.Models
{
    public class ProdCarro
    {
        private contextFertitec db = new contextFertitec();
        private List<Productos> products;
        public ProdCarro()
        {
            products = db.Productos.ToList();
        }

        public List<Productos> findAll()
        {
            return this.products;
        }

        public Productos find(int id)
        {
            Productos pp = this.products.Single(p => p.idProducto.Equals(id));
            return pp;
        }
    }
}