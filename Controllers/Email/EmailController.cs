using ProyectoAlmaInicio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoAlmaInicio.Controllers
{
    public class EmailController : Controller
    {
        // GET: Email
        public ActionResult Index()
        {
            if (Session["User"] == null)
            {
                return View("../Home/Login");
            }
            return View();
        }

        [HttpPost]
        public void EmailContactoComercial(string Comentario)
        {
            int IdUsuario = Convert.ToInt32(Session["PerfilInterior"]);
            EmailModels.EmailContactoComercial(IdUsuario, Comentario);
        }
    }

}