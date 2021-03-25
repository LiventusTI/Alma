using ProyectoAlmaInicio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static ProyectoAlmaInicio.Controllers.SidebarController;

namespace ProyectoAlmaInicio.Controllers.HistoricoEnCurso
{
    public class HistoricoEnCursoController : Controller
    {
        // GET: HistoricoEnCurso
        //public ActionResult Index()
        //{
        //    return View();
        //}

        public ActionResult HistoricoEnCursoView()
        {
            if (Session["User"] == null)
            {
                return View("../Home/Login");
            }

            int IdUsuario = Convert.ToInt32(Session["PerfilInterior"]);
            int perfilSeccion = Convert.ToInt32(Session["PerfilSecciones"]);

            //***** CARGA DE MENU PRINCIPAL *****//
            List<Parentitem> parentitem = new List<Parentitem>();
            List<Clases.ItemMenu> lista_items = UsuarioModels.obtenerItemsMenu(perfilSeccion);
            foreach (var item in lista_items)
            {
                if (item.ruta == "")
                {
                    List<childItems> childitem = new List<childItems>();

                    parentitem.Add(new Parentitem
                    {
                        nodeId = item.id_item.ToString(),
                        nodeText = item.valor,
                        iconCss = item.icono,
                        child = childitem,
                    });

                    List<Clases.ItemMenu> lista_items2 = UsuarioModels.obtenerItems2Menu(item.id_item);
                    foreach (var item2 in lista_items2)
                    {
                        if (item2.valor == "En Tránsito" && UsuarioModels.GetTipoUser(IdUsuario) == 4 && IdUsuario != 39)
                        { item2.ruta = "/TransitoEnCursoComercial/TransitoEnCursoComercialView"; }
                        else if (item2.valor == "Históricos" && UsuarioModels.GetTipoUser(IdUsuario) == 4 && IdUsuario != 39)
                        { item2.ruta = "/HistoricoComercial/HistoricoComercialView"; }
                        childitem.Add(new childItems
                        {
                            nodeId = item.id_item.ToString() + "-" + item2.id_item.ToString(),
                            nodeText = item2.valor,
                            iconCss = item2.icono + " circulo-menu",
                            url = item2.ruta,
                        });
                    }
                }
                else
                {
                    parentitem.Add(new Parentitem
                    {
                        nodeId = item.id_item.ToString(),
                        nodeText = item.valor,
                        iconCss = item.icono + " icono-item2-menu",
                        url = item.ruta,
                    });
                }
            }

            ViewBag.dataSource2 = parentitem;
            //***** FIN CARGA DE MENU PRINCIPAL *****//


            Clases.DataFiltros data = new Clases.DataFiltros();
            data = GetPerfilDataByUser();

            List<Clases.Servicio> serviciosLista = new List<Clases.Servicio>();

            serviciosLista = ServicioModels.GetHistoricoServicios(IdUsuario, data);
            ViewBag.DataSource = serviciosLista;

            //CARGA TOTALIZADORES - INICIO
            int contador = 0, contadorModem = 0, contadorLuz = 0, contadorTemperatura = 0;

            contador = ServicioModels.GetContadorTotalHistorico(IdUsuario, data);
            ViewBag.ContadorTotal = contador;

            contadorModem = ServicioModels.GetContadorModemHistorico(IdUsuario, data);
            ViewBag.ContadorModem = contadorModem;

            contadorLuz = ServicioModels.GetContadorLuzHistorico(IdUsuario, data);
            ViewBag.ContadorLuz = contadorLuz;

            contadorTemperatura = ServicioModels.GetContadorTemperaturaHistorico(IdUsuario, data);
            ViewBag.ContadorTemperatura = contadorTemperatura;

            List<object> DataRange = new List<object>();
            DataRange.Add(new { Text = "10 Registros", Value = "10" });
            DataRange.Add(new { Text = "50 Registros", Value = "50" });
            DataRange.Add(new { Text = "100 Registros", Value = "100" });
            ViewBag.Data = DataRange;

            return View();
        }

        public Clases.DataFiltros GetPerfilDataByUser()
        {
            Clases.DataFiltros dataOcultar = new Clases.DataFiltros();
            int perfilData = Convert.ToInt32(Session["PerfilData"]);

            dataOcultar = ServicioModels.GetPerfilDataByUser(perfilData);

            return dataOcultar;
        }

    }
}