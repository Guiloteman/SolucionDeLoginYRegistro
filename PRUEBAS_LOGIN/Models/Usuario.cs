using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PRUEBAS_LOGIN.Models
{
    public class Usuario
    {
        public int Id_Usuario { get; set; }
        public string Correo { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public int Dni { get; set; }
        public int Matricula { get; set; }
        public int Telefono { get; set; }
        public int Id_Rol { get; set; }



        public string ConfirmarClave { get; set; }
    }
}