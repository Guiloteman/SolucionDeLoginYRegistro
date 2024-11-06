using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PRUEBAS_LOGIN.Models
{
    public class Usuario
    {
        public int id { get; set; }
        public string correo { get; set; }
        public string clave { get; set; }
        public string nombre { get; set; }
        public int dni { get; set; }
        public int matricula { get; set; }
        public int telefono { get; set; }





        public string confirmarClave { get; set; }
    }
}