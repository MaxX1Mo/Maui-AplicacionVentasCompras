using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_VentasCompras_Maui.Models
{
    public class ProductoCrearDTO
    {

        public string? NombreProducto { get; set; }

        public string? Descripcion { get; set; }

        public int? IDUsuario { get; set; }

        public decimal? Precio { get; set; }

        public string? Imagen { get; set; }
        
        public string? EstadoProducto { get; set; }

        public string? EstadoVenta { get; set; }

        public int? Cantidad { get; set; }

        public string? NombreCategoria { get; set; }
    }
}
