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

        static string cadena = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\guile\\source\\repos\\SolucionDeLoginYRegistro\\PRUEBAS_LOGIN\\App_Data\\Database1.mdf;Integrated Security=True";

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

            if(oUsuario.Clave == oUsuario.ConfirmarClave)
            {
                oUsuario.Clave = ConvertirSha256(oUsuario.Clave);
            }
            else
            {
                ViewData["Mensaje"] = "Las Contraseñas no coinciden";
                return View();
            }

            using (SqlConnection cn = new SqlConnection(cadena))
            {
                SqlCommand cmd = new SqlCommand("sp_RegistrarUsuario", cn);
                cmd.Parameters.AddWithValue("Correo", oUsuario.Correo);
                cmd.Parameters.AddWithValue("Clave", oUsuario.Clave);
                cmd.Parameters.AddWithValue("Nombre", oUsuario.Nombre);
                cmd.Parameters.AddWithValue("Dni", oUsuario.Dni);
                cmd.Parameters.AddWithValue("Matricula", oUsuario.Matricula);
                cmd.Parameters.AddWithValue("Telefono", oUsuario.Telefono);
                cmd.Parameters.AddWithValue("Id_Rol", oUsuario.Id_Rol);
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
            oUsuario.Clave = ConvertirSha256(oUsuario.Clave);

            using (SqlConnection cn = new SqlConnection(cadena))
            {
                SqlCommand cmd = new SqlCommand("sp_ValidarUsuario", cn);
                cmd.Parameters.AddWithValue("Correo", oUsuario.Correo);
                cmd.Parameters.AddWithValue("Clave", oUsuario.Clave);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();

                oUsuario.Id_Rol = Convert.ToInt32(cmd.ExecuteScalar().ToString());
            }

            if(oUsuario.Id_Rol == 1)
            {
                Session["usuario"] = oUsuario;
                return RedirectToAction("Index", "Inspector");
            }
            else
            {
                if(oUsuario.Id_Rol == 2)
                {
                    Session["usuario"] = oUsuario;
                    return RedirectToAction("Index", "Legales");
                }else
                {
                    if(oUsuario.Id_Rol == 3)
                    {
                        Session["usuario"] = oUsuario;
                        return RedirectToAction("Index", "Director");
                    }else
                    {
                        if(oUsuario.Id_Rol == 4)
                        {
                            Session["usuario"] = oUsuario;
                            return RedirectToAction("Index", "Supervisor");
                        }else
                        {
                            if(oUsuario.Id_Rol == 5)
                            {
                                Session["usuario"] = oUsuario;
                                return RedirectToAction("Index", "Mesa");
                            }else
                            {
                                if (oUsuario.Id_Rol == 6)
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
                        }
                    }
                }
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