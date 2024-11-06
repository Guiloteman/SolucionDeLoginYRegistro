using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PRUEBAS_LOGIN.Models
{
    public abstract class Empleado
    {
        public string NombreYApellido { get; set; }
        public int Dni { get; set; }
        public int Telefono { get; set; }
        public string Correo { get; set; }
        public string Domicilio { get; set; }





        public string Clave { get; set; }
        public string ConfirmarClave { get; set; }
    }
}