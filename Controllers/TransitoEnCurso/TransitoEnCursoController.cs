using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProyectoAlmaInicio.Models;
using static ProyectoAlmaInicio.Controllers.SidebarController;
using Syncfusion.EJ2.Navigations;

namespace ProyectoAlmaInicio.Controllers
{
    public class TransitoEnCursoController : Controller
    {
        public ActionResult TransitoEnCursoView()
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

            serviciosLista = ServicioModels.GetServicios(IdUsuario, data);
            ViewBag.DataSource = serviciosLista;

            List<object> DataRange = new List<object>();
            DataRange.Add(new { Text = "10 Registros", Value = "10" });
            DataRange.Add(new { Text = "50 Registros", Value = "50" });
            DataRange.Add(new { Text = "100 Registros", Value = "100" });
            ViewBag.Data = DataRange;


            List<Clases.Contenedor> Contenedores = new List<Clases.Contenedor>();
            Contenedores = MantenedorModels.GetContenedores(IdUsuario);
            ViewBag.Contenedores = Contenedores;

            List<Clases.Naviera> Navieras = new List<Clases.Naviera>();
            Navieras = MantenedorModels.GetNavierasActivas(IdUsuario);
            ViewBag.Navieras = Navieras;

            List<string> Bookings = new List<string>();
            Bookings = MantenedorModels.GetBookings(IdUsuario);
            ViewBag.Bookings = Bookings;

            List<Clases.Nave> Nave = new List<Clases.Nave>();
            Nave = MantenedorModels.GetNaves(IdUsuario);
            ViewBag.Naves = Nave;

            List<Clases.PuertoDestino> PuertoOrigen = new List<Clases.PuertoDestino>();
            PuertoOrigen = MantenedorModels.GetPuertoOrigen(IdUsuario);
            ViewBag.PuertosOrigen = PuertoOrigen;

            List<Clases.PuertoDestino> PuertoDestino = new List<Clases.PuertoDestino>();
            PuertoDestino = MantenedorModels.GetPuertoDestino(IdUsuario);
            ViewBag.PuertosDestino = PuertoDestino;

            List<Clases.Commodity> Commodity = new List<Clases.Commodity>();
            Commodity = MantenedorModels.GetCommodityActivo(IdUsuario);
            ViewBag.Commoditys = Commodity;

            //CARGA TOTALIZADORES - INICIO
            int contador = 0, contadorModem = 0, contadorLuz = 0, contadorTemperatura = 0;

            contador = ServicioModels.GetContadorTotal(IdUsuario, data);
            ViewBag.ContadorTotal = contador;

            contadorModem = ServicioModels.GetContadorModem(IdUsuario, data);
            ViewBag.ContadorModem = contadorModem;
            //CARGA TOTALIZADORES - FIN

            contadorLuz = ServicioModels.GetContadorLuz(IdUsuario, data);
            ViewBag.ContadorLuz = contadorLuz;

            contadorTemperatura = ServicioModels.GetContadorTemperatura(IdUsuario, data);
            ViewBag.ContadorTemperatura = contadorTemperatura;

            List<Clases.Objeto> lista_sensores = MantenedorModels.GetTiposSensores();
            ViewBag.lista_sensores = lista_sensores;

            List<Clases.Objeto> lista_contenedores = MantenedorModels.GetContenedoresServicioNuevo();
            ViewBag.lista_contenedores = lista_contenedores;

            List<Clases.Objeto> lista_naves = MantenedorModels.GetNavesServicioNuevo();
            ViewBag.lista_naves = lista_naves;

            List<Clases.Objeto> lista_commodities = MantenedorModels.GetCommoditiesServicioNuevo();
            ViewBag.lista_commodities = lista_commodities;

            List<Clases.Objeto> lista_navieras = MantenedorModels.GetNavierasServicioNuevo();
            ViewBag.lista_navieras = lista_navieras;

            List<Clases.Objeto> lista_setpoint_co2 = MantenedorModels.GetSetpointsCO2ServicioNuevo();
            ViewBag.lista_setpoint_co2 = lista_setpoint_co2;

            List<Clases.Objeto> lista_setpoint_temp = MantenedorModels.GetSetpointsTemperaturaServicioNuevo();
            ViewBag.lista_setpoint_temp = lista_setpoint_temp;

            List<ToolbarItem> popupItems = new List<ToolbarItem>();
            popupItems.Add(new ToolbarItem { PrefixIcon = "e-copy-icon tb-icons", TooltipText = "Opciones", Text = "", Overflow = OverflowOption.Hide });
            ViewBag.popupItems = popupItems;


            //TABULADORES
            List<TabTabItem> orientationItems = new List<TabTabItem>();
            orientationItems.Add(new TabTabItem { Header = new TabHeader { Text = "Vista Servicios" }, Content = "#div-tab1" });
            orientationItems.Add(new TabTabItem { Header = new TabHeader { Text = "Vista Mapa" }, Content = "#div-tab2" });
            ViewBag.orientationItems = orientationItems;


            return View();
        }

        public ActionResult PruebaTransit()
        {
            List<Parentitem> parentitem = new List<Parentitem>();
            List<childItems> childitem = new List<childItems>();


            List<childItems> childitem4 = new List<childItems>();
            parentitem.Add(new Parentitem
            {
                nodeId = "04",
                nodeText = "Mis Servicios",
                iconCss = "icon-up-hand icon",
                child = childitem4
            });
            childitem4.Add(new childItems { nodeId = "04-01", nodeText = "Tránsito en Curso", iconCss = "icon-circle-thin icon", url = "/ProyectoAlma/TransitoEnCurso/TransitoEnCursoView", });
            childitem4.Add(new childItems { nodeId = "04-02", nodeText = "Históricos", iconCss = "icon-circle-thin icon", url = "/ProyectoAlma/HistoricoEnCurso/HistoricoEnCursoView", });
            List<childItems> childitem5 = new List<childItems>();
            parentitem.Add(new Parentitem
            {
                nodeId = "05",
                nodeText = "API Reference",
                iconCss = "icon-code icon",
                child = childitem4,
            });
            childitem5.Add(new childItems { nodeId = "05-01", nodeText = "Calendar", iconCss = "icon-circle-thin icon" });
            childitem5.Add(new childItems { nodeId = "05-02", nodeText = "DatePicker", iconCss = "icon-circle-thin icon" });
            childitem5.Add(new childItems { nodeId = "05-03", nodeText = "DateTimePicker", iconCss = "icon-circle-thin icon" });
            childitem5.Add(new childItems { nodeId = "05-04", nodeText = "DateRangePicker", iconCss = "icon-circle-thin icon" });
            childitem5.Add(new childItems { nodeId = "05-05", nodeText = "TimePicker", iconCss = "icon-circle-thin icon" });
            childitem5.Add(new childItems { nodeId = "05-06", nodeText = "SideBar", iconCss = "icon-circle-thin icon" });
            parentitem.Add(new Parentitem
            {
                nodeId = "06",
                nodeText = "Browser Compatibility",
                iconCss = "icon-chrome icon",

            });
            parentitem.Add(new Parentitem
            {
                nodeId = "07",
                nodeText = "Upgrade Packages",
                iconCss = "icon-up-hand icon",

            });
            parentitem.Add(new Parentitem
            {
                nodeId = "08",
                nodeText = "Release Notes",
                iconCss = "icon-bookmark-empty icon",

            });
            parentitem.Add(new Parentitem
            {
                nodeId = "09",
                nodeText = "FAQ",
                iconCss = "icon-help-circled icon",

            });
            parentitem.Add(new Parentitem
            {
                nodeId = "10",
                nodeText = "License",
                iconCss = "icon-doc-text icon",

            });

            ViewBag.dataSource2 = parentitem;

            return View();
        }


        public Clases.DataFiltros GetPerfilDataByUser()
        {
            Clases.DataFiltros dataOcultar = new Clases.DataFiltros();
            int perfilData = Convert.ToInt32(Session["PerfilData"]);

            dataOcultar = ServicioModels.GetPerfilDataByUser(perfilData);

            return dataOcultar;
        }

        public int CancelarServicioPorId(int idServicio, int idBD)
        {
            int retorno = 0;
            int IdUsuario = Convert.ToInt32(Session["PerfilInterior"]);

            retorno = ServicioModels.CancelarServicioPorId(idServicio, idBD, IdUsuario);

            return retorno;
        }

        public int FinalizarServicioPorId(int idServicio, int idBD)
        {
            int retorno = 0;
            int IdUsuario = Convert.ToInt32(Session["PerfilInterior"]);

            retorno = ServicioModels.FinalizarServicioPorId(idServicio, idBD, IdUsuario);

            return retorno;
        }

        public JsonResult GetPerfilOcultarColumnasByUser()
        {

            int perfilSeccion = Convert.ToInt32(Session["PerfilSecciones"]);
            List<string> columnas = new List<string>();

            columnas = UsuarioModels.GetPerfilOcultarColumnasByUser(perfilSeccion);

            var resultados = Json(columnas, JsonRequestBehavior.AllowGet);
            resultados.MaxJsonLength = Int32.MaxValue;

            return resultados;
        }




    }
}


