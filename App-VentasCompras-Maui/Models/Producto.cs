using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Graphics;
using Newtonsoft.Json;

namespace App_VentasCompras_Maui.Models
{
    public class Producto
    {
        public int? IDProducto { get; set; }

        public string? NombreProducto { get; set; }

        public string? Descripcion { get; set; }


        public decimal? Precio { get; set; }

        public string? Imagen { get; set; }

        // Usuario
        public int? IDUsuario { get; set; }

        public string? Username { get; set; }

        public string? Email { get; set; }

        public string? NroCelular { get; set; }

        // ProductoVenta
        //public int? IDProductoVenta { get; set; }

        public DateTime? Fecha { get; set; }
        public string? EstadoProducto { get; set; }

        public string? EstadoVenta { get; set; }

        public int? Cantidad { get; set; }

        // Categoria
        public string? NombreCategoria { get; set; }

       //public int? IDCategoria { get; set; }

        //public Usuario? usuario { get; set; }

        public Color EstadoColor =>
      EstadoVenta switch
      {
          "Activo" => Colors.Blue,
          "Finalizado" => Colors.Red,
          "Detenido" => Colors.Orange,
          _ => Colors.Gray
      };

        public Color FondoEstado =>
            EstadoVenta switch
            {
                "Activo" => Colors.LightGray,
                "Final  izado" => Color.FromArgb("#666"),
                "Detenido" => Color.FromArgb("#999"),
                _ => Color.FromArgb("#222")
            };
    }
}
