using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PRUEBAS_LOGIN.Models;
using System.Xml.Xsl;

namespace PRUEBAS_LOGIN.Controllers
{
    public class PresentarCarpetaController : Controller
    {
        static string cadena = "Data Source=dbusuario.database.windows.net;Initial Catalog=pruebasusuario;Persist Security Info=True;User ID=admindb;Password=Lanum120$";
        // GET: PresentarCarpeta
        public ActionResult PresentarCarpeta()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CargarImagen(Carpeta oCarpeta, Usuario oUsuario)
        {
            
            
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                var tim = oCarpeta.timbrado;
                var u = oUsuario.correo;
                var form = oCarpeta.formulario;
                var pla = oCarpeta.planos;
                var regInmob = oCarpeta.RegInmob;
                var tribuDeFalta = oCarpeta.TibunalDeFalta;
                var per = oCarpeta.permiso;

                SqlCommand cmd = new SqlCommand("insert into CARPETA(timbrado, id_usuario, formulario, planos, RegInmob, TibunalDeFalta, permiso) values(@Timbrado, (select id from USUARIO where correo = @Correo), @Formulario, @Planos, @RegInmob, @TibunalDeFalta, @Permiso)", cn);
                cmd.Parameters.AddWithValue("@Timbrado", tim);
                cmd.Parameters.AddWithValue("@Correo", u);
                cmd.Parameters.AddWithValue("@Formulario", form);
                cmd.Parameters.AddWithValue("@Planos", pla);
                cmd.Parameters.AddWithValue("@RegInmob", regInmob);
                cmd.Parameters.AddWithValue("@TibunalDeFalta", tribuDeFalta);
                cmd.Parameters.AddWithValue("@Permiso", per);







                cn.Open();

                cmd.ExecuteNonQuery().ToString();
                     


            }

            return RedirectToAction("Index", "ClienteProfesional");
        }

        public ActionResult CerrarSesion()
        {
            Session["usuario"] = null;
            return RedirectToAction("Login", "Acceso");
        }
    }
}