using ProyectoAlmaInicio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoAlmaInicio.Controllers
{
    public class MantenedorController : Controller
    {
        // GET: Mantenedor
        public ActionResult Index()
        {
            if (Session["User"] == null)
            {
                return View("../Home/Login");
            }
            return View();
        }

        [HttpPost]
        public string GetNavierasActivas()
        {
            List<Clases.Naviera> Naviera = new List<Clases.Naviera>();
            int IdUsuario = Convert.ToInt32(Session["PerfilInterior"]);
            Naviera = MantenedorModels.GetNavierasActivas(IdUsuario);
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(Naviera);
            return datos;
        }
        //
        [HttpPost]
        public string GetCommodityActivo()
        {
            List<Clases.Commodity> Commodity = new List<Clases.Commodity>();
            int IdUsuario = Convert.ToInt32(Session["PerfilInterior"]);
            Commodity = MantenedorModels.GetCommodityActivo(IdUsuario);
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(Commodity);
            return datos;
        }
        //
        [HttpPost]
        public string GetContenedores(int Estado)
        {
            List<Clases.Contenedor> Contenedores = new List<Clases.Contenedor>();
            int IdUsuario = Convert.ToInt32(Session["PerfilInterior"]);
            Contenedores = MantenedorModels.GetContenedores(IdUsuario);
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(Contenedores);
            return datos;
        }
        //
        [HttpPost]
        public string GetPuertoOrigen()
        {
            List<Clases.PuertoDestino> PuertoDestino = new List<Clases.PuertoDestino>();
            int IdUsuario = Convert.ToInt32(Session["PerfilInterior"]);
            PuertoDestino = MantenedorModels.GetPuertoOrigen(IdUsuario);
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(PuertoDestino);
            return datos;
        }
        //
        [HttpPost]
        public string GetPuertoDestino()
        {
            List<Clases.PuertoDestino> PuertoDestino = new List<Clases.PuertoDestino>();
            int IdUsuario = Convert.ToInt32(Session["PerfilInterior"]);
            PuertoDestino = MantenedorModels.GetPuertoDestino(IdUsuario);
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(PuertoDestino);
            return datos;
        }
        //
        [HttpPost]
        public string GetNaves()
        {
            List<Clases.Nave> Nave = new List<Clases.Nave>();
            int IdUsuario = Convert.ToInt32(Session["PerfilInterior"]);
            Nave = MantenedorModels.GetNaves(IdUsuario);
            string datos = Newtonsoft.Json.JsonConvert.SerializeObject(Nave);
            return datos;
        }

        [HttpPost]
        public List<string> GetBookings()
        {
            List<string> Bookings = new List<string>();
            int IdUsuario = Convert.ToInt32(Session["PerfilInterior"]);
            Bookings = MantenedorModels.GetBookings(IdUsuario);
            ViewBag.Bookings = Bookings;

            return Bookings;
        }


    }
}