using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProyectoAlmaInicio.Models;

namespace ProyectoAlmaInicio.Controllers.Trazabilidad
{
    public class TrazabilidadController : Controller
    {
        // GET: Trazabilidad
        public ActionResult Index()
        {
            if (Session["User"] == null)
            {
                return View("../Home/Login");
            }
            return View();
        }

        public JsonResult SetTrazabilidadSesion()
        {
            Clases.Servicio servicio = new Clases.Servicio();
            int IdUsuario = Convert.ToInt32(Session["PerfilInterior"]);

            int exitoso = TrazabilidadModels.SetTrazabilidadSesion(IdUsuario);

            if (exitoso != 0)
            {

                /*if (Session["User"] == null)
                {
                    return View("../Home/Login");
                }*/
                Clases.Usuario Usuario = new Clases.Usuario();
                Usuario = UsuarioModels.GetInfoUsuario(Session["user"].ToString().ToUpper());
                ViewBag.NombreUsuario = Usuario.NombreUsuario;
                ViewBag.Nombre = Usuario.Nombre;
                ViewBag.Contrasena = Usuario.Contrasena;
                ViewBag.Apellido = Usuario.Apellido;
                ViewBag.IdPerfil = Usuario.IdPerfil;
                ViewBag.IdPerfilData = Usuario.IdPerfilData;
                ViewBag.IdPerfilSeccion = Usuario.IdPerfilSeccion;
                //Session.Add("Sesion", exitoso);
                ViewBag.IdSesion = exitoso; /////////////////aqui es nuevo

            }

            var dato = Json(exitoso, JsonRequestBehavior.AllowGet);
            dato.MaxJsonLength = Int32.MaxValue;

            return dato;
        }


        public JsonResult SetTrazabilidadVista(int TipoVista, String NombreVista)
        {
            Clases.Servicio servicio = new Clases.Servicio();
            int Sesion = Convert.ToInt32(Session["Sesion"]);

            int exitoso = TrazabilidadModels.SetTrazabilidadVista(TipoVista, Sesion, NombreVista);

            var dato = Json(exitoso, JsonRequestBehavior.AllowGet);
            dato.MaxJsonLength = Int32.MaxValue;

            return dato;
        }
        /*

        public JsonResult SetTrazabilidadBoton(int TipoBoton, String NombreBoton)
        {
            Clases.Servicio servicio = new Clases.Servicio();
            int Sesion = Convert.ToInt32(Session["Sesion"]);

            int exitoso = TrazabilidadModels.SetTrazabilidadVista(TipoBoton, Sesion, NombreVista);

            var dato = Json(exitoso, JsonRequestBehavior.AllowGet);
            dato.MaxJsonLength = Int32.MaxValue;

            return dato;
        }*/
    }
}