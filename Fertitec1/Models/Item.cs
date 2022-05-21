using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fertitec.Models
{
    public class Item
    {
        public Productos Product
        {
            get;
            set;
        }
         
        public int Cantidad
        {
            get;
            set;
        }
    }
}