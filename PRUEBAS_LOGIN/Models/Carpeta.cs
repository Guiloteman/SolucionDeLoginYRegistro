using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PRUEBAS_LOGIN.Models
{
    public class Carpeta
    {
        public int id { get; set; }

        public string timbrado { get; set; }

        public int id_usuario { get; set; }
        public int formulario { get; set; }
        public int planos { get; set; }
        public int RegInmob { get; set; }
        public int TibunalDeFalta { get; set; }
        public int permiso { get; set; }
    }
}