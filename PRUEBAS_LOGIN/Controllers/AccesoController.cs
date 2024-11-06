using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using PRUEBAS_LOGIN.Models;
using System.Data.SqlClient;
using System.Data;
using System.Web.Services.Description;

namespace PRUEBAS_LOGIN.Controllers
{
    public class AccesoController : Controller
    {

        static string cadena = "Data Source=dbusuario.database.windows.net;Initial Catalog=pruebasusuario;Persist Security Info=True;User ID=admindb;Password=Lanum120$";

        // GET: Acceso
        public ActionResult Login()
        {
            return View();
        }


        public ActionResult Registrar()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Registrar(Usuario oUsuario)
        {
            bool registrado;
            string mensaje;

            if(oUsuario.clave == oUsuario.confirmarClave)
            {
                oUsuario.clave = ConvertirSha256(oUsuario.clave);
            }
            else
            {
                ViewData["Mensaje"] = "Las Contraseñas no coinciden";
                return View(); 
            }

            using (SqlConnection cn = new SqlConnection(cadena))
            {
                SqlCommand cmd = new SqlCommand("sp_RegistrarUsuario", cn);
                cmd.Parameters.AddWithValue("Correo", oUsuario.correo);
                cmd.Parameters.AddWithValue("Clave", oUsuario.clave);
                cmd.Parameters.AddWithValue("Nombre", oUsuario.nombre);
                cmd.Parameters.AddWithValue("Dni", oUsuario.dni);
                cmd.Parameters.AddWithValue("Matricula", oUsuario.matricula);
                cmd.Parameters.AddWithValue("Telefono", oUsuario.telefono);
                cmd.Parameters.Add("Registrado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 100).Direction = ParameterDirection.Output;
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();

                cmd.ExecuteNonQuery();

                registrado = Convert.ToBoolean(cmd.Parameters["Registrado"].Value);
                mensaje = cmd.Parameters["Mensaje"].Value.ToString();
            }

            ViewData["Mensaje"] = mensaje;

            if (registrado)
            {
                return RedirectToAction("Login", "Acceso");
            }
            else
            {
                return View();
            }
        }
                                                                                                                     

        [HttpPost]
        public ActionResult Login(Usuario oUsuario)
        {
            oUsuario.clave = ConvertirSha256(oUsuario.clave);

            using (SqlConnection cn = new SqlConnection(cadena))
            {
                SqlCommand cmd = new SqlCommand("sp_ValidarUsuario", cn);
                cmd.Parameters.AddWithValue("Correo", oUsuario.correo);
                cmd.Parameters.AddWithValue("Clave", oUsuario.clave);
                cmd.CommandType = CommandType.StoredProcedure;

                
                cn.Open();
                
                oUsuario.id = Convert.ToInt32(cmd.ExecuteScalar().ToString());
                
            }


            if (oUsuario.id != 0) 
            {
                Session["usuario"] = oUsuario;
                
                return RedirectToAction("Index", "ClienteProfesional");
            }
            else
            {
                ViewData["Mensaje"] = "Usuario no encontrado";
                return View();
            }
                
        }


        public static string ConvertirSha256(string texto)
        {
            StringBuilder sb = new StringBuilder();
            using (SHA256 hash = SHA256Managed.Create())
            {
                Encoding enc = Encoding.UTF8;
                byte[] result = hash.ComputeHash(enc.GetBytes(texto));

                foreach (byte b in result)
                {
                    sb.Append(b.ToString("x2"));
                }
            }
            return sb.ToString();
        }

      
    }
}