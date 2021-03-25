using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProyectoAlmaInicio.Models;


namespace ProyectoAlmaInicio.Controllers
{
    public class UsuarioController : Controller
    {
        // GET: Usuario
        public ActionResult Index()
        {
            if (Session["User"] == null)
            {
                return View("../Home/Login");
            }

            ViewBag.User = Session["User"].ToString().ToUpper();
            ViewBag.Pass = Session["Pass"];
            return View();
        }

        // 
        public ActionResult ModificarDatosView()
        {
            if (Session["User"] == null)
            {
                return View("../Home/Login");
            }
            Clases.Usuario Usuario = new Clases.Usuario();
            Usuario = UsuarioModels.GetInfoUsuario(Session["user"].ToString().ToUpper());
            ViewBag.NombreUsuario = Usuario.NombreUsuario;
            ViewBag.Nombre = Usuario.Nombre;
            ViewBag.Contrasena = Usuario.Contrasena;
            ViewBag.Apellido = Usuario.Apellido;
            ViewBag.IdPerfil = Usuario.IdPerfil;
            ViewBag.IdPerfilData = Usuario.IdPerfilData;
            ViewBag.IdPerfilSeccion = Usuario.IdPerfilSeccion;
            return View("../Usuario/ModificarDatosView");
        }

        public int EditarContrasena(string pass, string passConfirm)
        {
            //if (Session["User"] == null)
            //{
            //    View("../Home/Login");
            //}
            //Editar Contraseña en BD INTRANET
            bool Valor = UsuarioModels.EditarContrasena(Session["user"].ToString().ToUpper(), pass);

            if (Valor == true) return 1;
            
            return 0;
        }

        public JsonResult GetSeccionesByUser()
        {
            List<Clases.Seccion> secciones = new List<Clases.Seccion>();

            int perfilSeccion = Convert.ToInt32(Session["PerfilSecciones"]);

            secciones = UsuarioModels.GetPerfilSeccionesByUser(perfilSeccion);
            var resultados = Json(secciones, JsonRequestBehavior.AllowGet);
            resultados.MaxJsonLength = Int32.MaxValue;
            return resultados;
        }

    }
}