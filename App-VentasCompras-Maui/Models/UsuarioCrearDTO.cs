using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_VentasCompras_Maui.Models
{
    public class UsuarioCrearDTO
    {
        public string? Email { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Rol { get; set; }

        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? NroCelular { get; set; }

        //ubicacion
        public string? Pais { get; set; }
        public string? Localidad { get; set; }
        public string? Provincia { get; set; }
        public string? CodigoPostal { get; set; }
        public string? Calle { get; set; }
        public string? NroCalle { get; set; }

    }
}
