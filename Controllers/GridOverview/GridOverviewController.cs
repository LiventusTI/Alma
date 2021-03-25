using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace ProyectoAlmaInicio.Controllers
{
    public class GridOverviewController : Controller
    {
        // GET: GridOverview
        public ActionResult GridOverviewView()
        {
            List<object> DataRange = new List<object>();
            DataRange.Add(new { Text = "1.000 Registros", Value = "1000" });
            DataRange.Add(new { Text = "10.000 Registros", Value = "10000" });
            DataRange.Add(new { Text = "100.000 Registros", Value = "100000" });
            ViewBag.Data = DataRange;
            return View();
        }
    }
}
