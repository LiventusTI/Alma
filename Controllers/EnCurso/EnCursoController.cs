using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProyectoAlmaInicio.Controllers;

namespace ProyectoAlmaInicio.Controllers
{
    public class EnCursoController : Controller
    {
        // GET: GridOverview
        public ActionResult EnCursoView()
        {
            List<object> DataRange = new List<object>();
            DataRange.Add(new { Text = "10 Registros", Value = "10" });
            DataRange.Add(new { Text = "50 Registros", Value = "50" });
            DataRange.Add(new { Text = "100 Registros", Value = "100" });

            ViewBag.Data = DataRange;
            
            return View();
        }
    }
}

