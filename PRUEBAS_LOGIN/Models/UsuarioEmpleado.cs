using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PRUEBAS_LOGIN.Models
{
    public class UsuarioEmpleado : Empleado
    {
        public int Id_legajo { get; set; }
        public string Rol {  get; set; }
    }
}