using PRUEBAS_LOGIN.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ServiceModel.Channels;
using System.IO;
using System.Drawing;

namespace PRUEBAS_LOGIN.Controllers
{
    public class MisCarpetasController : Controller
    {

        static string cadena = "Data Source=dbusuario.database.windows.net;Initial Catalog=pruebasusuario;Persist Security Info=True;User ID=admindb;Password=Lanum120$";
        // GET: MisCarpetas
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Mostrar(Usuario oUsuario)
        {
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                var u = oUsuario.correo;
                

                SqlCommand cmd = new SqlCommand("select * from  CARPETA where id_usuario = (select id from USUARIO where correo = @Correo)", cn);
                cmd.Parameters.AddWithValue("@Correo", u);

                
                
                cn.Open();
                
                if (!cmd.ExecuteNonQuery().ToString().Contains(""))
                {
                    View(cmd);
                }
            }
            return View();
        }
    }
}