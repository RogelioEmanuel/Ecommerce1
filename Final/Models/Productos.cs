//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Final.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Productos
    {
        public int idProducto { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public Nullable<decimal> precio { get; set; }
        public Nullable<System.DateTime> ult_actualizacion { get; set; }
        public string imagen { get; set; }
        public Nullable<int> existencia { get; set; }
        public Nullable<int> stock { get; set; }
        public Nullable<byte> estado { get; set; }
        public Nullable<int> idCategoria { get; set; }
    
        public virtual Categorias Categorias { get; set; }
    }
}
