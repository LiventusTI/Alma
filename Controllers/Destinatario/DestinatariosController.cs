using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProyectoAlmaInicio.Models;
using ProyectoAlmaInicio.Controllers;
using static ProyectoAlmaInicio.Controllers.SidebarController;
using System.ComponentModel.DataAnnotations;
using static ProyectoAlmaInicio.Models.Clases;
using Syncfusion.EJ2.Popups;


namespace ProyectoAlmaInicio.Controllers.Destinatario
{
    public class DestinatariosController : Controller
    {
        // GET: Destinatarios
        public ActionResult DestinatariosView()
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

            //***** INFORMACION TABLA PRINCIPAL *****//
            List<Clases.Destinatario> listaDestinatarios = DestinatariosModels.GetDestinatariosCliente(IdUsuario);
            ViewBag.listaDestinatarios = listaDestinatarios;

            //***** LISTAS DESPLEGABLES MODAL *****//
            List<Clases.Objeto> lista_actividades_empresa = MantenedorModels.GetMantenedorActividadesEmpresa();
            ViewBag.lista_actividades_empresa = lista_actividades_empresa;

            List<Clases.Objeto> lista_commodities = MantenedorModels.GetCommoditiesServicioNuevo();
            ViewBag.lista_commodities = lista_commodities;

            List<Clases.Objeto> lista_paises = MantenedorModels.GetPaises();
            ViewBag.lista_paises = lista_paises;

            List<Clases.Objeto> lista_puertos = MantenedorModels.GetPuertosServicioNuevo();
            ViewBag.lista_puertos = lista_puertos;

            return View();
        }





        public ActionResult AgregarDestinatarioView()
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

            List<Clases.Objeto> lista_actividades_empresa = MantenedorModels.GetMantenedorActividadesEmpresa();
            ViewBag.lista_actividades_empresa = lista_actividades_empresa;

            List<Clases.Objeto> lista_commodities = MantenedorModels.GetCommoditiesServicioNuevo();
            ViewBag.lista_commodities = lista_commodities;

            List<Clases.Objeto> lista_paises = MantenedorModels.GetPaises();
            ViewBag.lista_paises = lista_paises;

            List<Clases.Objeto> lista_puertos = MantenedorModels.GetPuertosServicioNuevo();
            ViewBag.lista_puertos = lista_puertos;

            return View();
        }

        public ActionResult AgregarDestinatario()
        {
            if (Session["User"] == null)
            {
                return View("../Home/Login");
            }

            int IdUsuario = Convert.ToInt32(Session["PerfilInterior"]);
            DataFiltros data = new DataFiltros();
            data = GetPerfilDataByUser();

            Clases.Destinatario nuevoDestinatario = new Clases.Destinatario();


            if (Request.HttpMethod.ToString() == "POST")
            {
                nuevoDestinatario.empresa_destinatario = Request["empresa_destinatario"];
                nuevoDestinatario.actividad_empresa = Convert.ToInt32(Request["actividad_empresa"]);
                nuevoDestinatario.pais_contacto = Convert.ToInt32(Request["pais_contacto"]);
                nuevoDestinatario.puerto_contacto = Convert.ToInt32(Request["puerto_contacto"]);
                nuevoDestinatario.nombre_contacto = Request["nombre_contacto"];
                nuevoDestinatario.email_contacto = Request["email_contacto"];
                nuevoDestinatario.telefono_contacto = Request["telefono_contacto"];
                nuevoDestinatario.usuario_edita = IdUsuario;

                int id_destinatario = DestinatariosModels.NuevoDestinatario(nuevoDestinatario);

                if (id_destinatario != 0)
                {
                    List<CommodityEmpresa> listaCommoditiesEmpresa = new List<CommodityEmpresa>();
                    string lista_commodities = Request["lista_commodities"];

                    string[] array_commodities = lista_commodities.Split(',');
                    int contador_commodities = 0;
                    foreach (var item in array_commodities)
                    {
                        if (item != null && item != "")
                        {
                            contador_commodities++;
                            string nombre_commodity = "";
                            if (contador_commodities == 1)
                            {
                                nombre_commodity = item;
                            }
                            else
                            {
                                nombre_commodity = item.Substring(1, item.Length - 1);
                            }

                            DateTime? inicioTemporada = null;
                            DateTime? finTemporada = null;

                            CommodityEmpresa commodityEmpresa = new CommodityEmpresa();
                            commodityEmpresa.commodity_empresa = nombre_commodity;
                            commodityEmpresa.inicio_temporada = inicioTemporada;
                            commodityEmpresa.fin_temporada = finTemporada;
                            listaCommoditiesEmpresa.Add(commodityEmpresa);
                        }
                    }


                    if (listaCommoditiesEmpresa.Count != 0) //inserto en bd
                    {
                        int resultado = DestinatariosModels.InsertarCommodities(listaCommoditiesEmpresa, id_destinatario);
                    }
                }
            }

            return RedirectToAction("AgregarDestinatarioView", "Destinatarios", new { desplegarMensaje = 1 });
        }

        public Clases.DataFiltros GetPerfilDataByUser()
        {
            Clases.DataFiltros dataOcultar = new Clases.DataFiltros();
            int perfilData = Convert.ToInt32(Session["PerfilData"]);

            dataOcultar = ServicioModels.GetPerfilDataByUser(perfilData);

            return dataOcultar;
        }

        public JsonResult BorrarDestinatario(int id_destinatario)
        {
            int respuesta = 0;

            int id_dest = DestinatariosModels.BorrarDestinatario(id_destinatario);

            if (id_dest > 0)
            {

                respuesta = id_dest;
            }

            var dato = Json(respuesta, JsonRequestBehavior.AllowGet);
            dato.MaxJsonLength = Int32.MaxValue;
            return dato;
        }

        public JsonResult EditarDestinatario(int id_destinatario, string empresa, string actividad, string pais, string puerto, string nombre_contacto, string email, string telefono, string commodities)
        {
            int respuesta = 0;
            int id_dest = DestinatariosModels.EditarDestinatario(id_destinatario, empresa, actividad, pais, puerto, nombre_contacto, email, telefono);

            if (id_dest > 0)
            {
                respuesta = id_dest;

                int resultado_borrado_commodities = DestinatariosModels.BorrarCommoditiesDestinatario(id_destinatario);

                List<CommodityEmpresa> listaCommoditiesEmpresa = new List<CommodityEmpresa>();
                string[] array_commodities = commodities.Split(',');
                int contador_commodities = 0;
                foreach (var item in array_commodities)
                {
                    if (item != null && item != "")
                    {
                        contador_commodities++;
                        string nombre_commodity = "";
                        if (contador_commodities == 1)
                        {
                            nombre_commodity = item;
                        }
                        else
                        {
                            nombre_commodity = item.Substring(1, item.Length - 1);
                        }

                        DateTime? inicioTemporada = null;
                        DateTime? finTemporada = null;

                        CommodityEmpresa commodityEmpresa = new CommodityEmpresa();
                        commodityEmpresa.commodity_empresa = nombre_commodity;
                        commodityEmpresa.inicio_temporada = inicioTemporada;
                        commodityEmpresa.fin_temporada = finTemporada;
                        listaCommoditiesEmpresa.Add(commodityEmpresa);
                    }
                }


                if (listaCommoditiesEmpresa.Count != 0) //inserto en bd
                {
                    int resultado_commodity = DestinatariosModels.InsertarCommodities(listaCommoditiesEmpresa, id_destinatario);
                }

            }

            var dato = Json(respuesta, JsonRequestBehavior.AllowGet);
            dato.MaxJsonLength = Int32.MaxValue;
            return dato;
        }


        public JsonResult ObtenerDatosDestinatario(int id_destinatario)
        {
            Clases.Destinatario destinatario = new Clases.Destinatario();
            destinatario = DestinatariosModels.ObtenerDatosDestinatario(id_destinatario);

            var dato = Json(destinatario, JsonRequestBehavior.AllowGet);
            dato.MaxJsonLength = Int32.MaxValue;
            return dato;
        }

        public JsonResult ObtenerCommoditiesDestinatario(int id_destinatario)
        {
            List<Commodity> lista_commodities = new List<Commodity>();
            lista_commodities = DestinatariosModels.ObtenerCommoditiesDestinatario(id_destinatario);

            var dato = Json(lista_commodities, JsonRequestBehavior.AllowGet);
            dato.MaxJsonLength = Int32.MaxValue;
            return dato;
        }






        public class TextBoxModal
        {
            [Required(ErrorMessage = "Value is required")]
            public int id_servicio { get; set; }
            public string empresa_destinatario { get; set; }
            public string actividad_empresa { get; set; }
            public string commodity_empresa { get; set; }
            public string inicio_temporada { get; set; }
            public string fin_temporada { get; set; }
            public string pais_contacto { get; set; }
            public string ciudad_contacto { get; set; }
            public string puerto_contacto { get; set; }
            public string nombre_contacto { get; set; }
            public string email_contacto { get; set; }
            public string telefono_contacto { get; set; }
        }

    }
}