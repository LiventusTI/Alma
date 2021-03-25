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

namespace ProyectoAlmaInicio.Controllers.AgregarContenedor
{
    public class AgregarContenedorController : Controller
    {
        private static Dictionary<char, int> _Alphabet;

        private static Dictionary<char, int> Alphabet
        {
            get
            {
                if (_Alphabet == null)
                {
                    //If _Alphabed is null Initialise new dictionary and fill it
                    _Alphabet = new Dictionary<char, int>();

                    //Add Letters
                    _Alphabet.Add('A', 10);
                    _Alphabet.Add('B', 12);
                    _Alphabet.Add('C', 13);
                    _Alphabet.Add('D', 14);
                    _Alphabet.Add('E', 15);
                    _Alphabet.Add('F', 16);
                    _Alphabet.Add('G', 17);
                    _Alphabet.Add('H', 18);
                    _Alphabet.Add('I', 19);
                    _Alphabet.Add('J', 20);
                    _Alphabet.Add('K', 21);
                    _Alphabet.Add('L', 23);
                    _Alphabet.Add('M', 24);
                    _Alphabet.Add('N', 25);
                    _Alphabet.Add('O', 26);
                    _Alphabet.Add('P', 27);
                    _Alphabet.Add('Q', 28);
                    _Alphabet.Add('R', 29);
                    _Alphabet.Add('S', 30);
                    _Alphabet.Add('T', 31);
                    _Alphabet.Add('U', 32);
                    _Alphabet.Add('V', 34);
                    _Alphabet.Add('W', 35);
                    _Alphabet.Add('X', 36);
                    _Alphabet.Add('Y', 37);
                    _Alphabet.Add('Z', 38);

                    //Add Numbers
                    _Alphabet.Add('0', 0);
                    _Alphabet.Add('1', 1);
                    _Alphabet.Add('2', 2);
                    _Alphabet.Add('3', 3);
                    _Alphabet.Add('4', 4);
                    _Alphabet.Add('5', 5);
                    _Alphabet.Add('6', 6);
                    _Alphabet.Add('7', 7);
                    _Alphabet.Add('8', 8);
                    _Alphabet.Add('9', 9);
                }

                return _Alphabet;
            }
        }

        public ActionResult AgregarContenedorView()
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

            List<Clases.Objeto> listaDestinatarios = MantenedorModels.GetMantenedorDestinatariosCliente(IdUsuario);
            ViewBag.listaDestinatarios = listaDestinatarios;

            List<Clases.Objeto> lista_sensores = MantenedorModels.GetTiposSensores();
            ViewBag.lista_sensores = lista_sensores;

            List<Clases.Objeto> lista_contenedores = MantenedorModels.GetContenedoresServicioNuevo();
            ViewBag.lista_contenedores = lista_contenedores;

            List<Clases.Objeto> lista_naves = MantenedorModels.GetNavesServicioNuevo();
            ViewBag.lista_naves = lista_naves;

            List<Clases.Objeto> lista_puertos = MantenedorModels.GetPuertosServicioNuevo();
            ViewBag.lista_puertosorigen = lista_puertos;
            ViewBag.lista_puertosdestino = lista_puertos;

            List<Clases.Objeto> lista_commodities = MantenedorModels.GetCommoditiesServicioNuevo();
            ViewBag.lista_commodities = lista_commodities;

            List<Clases.Objeto> lista_navieras = MantenedorModels.GetNavierasServicioNuevo();
            ViewBag.lista_navieras = lista_navieras;

            List<Clases.Objeto> lista_setpoint_co2 = MantenedorModels.GetSetpointsCO2ServicioNuevo();
            ViewBag.lista_setpoint_co2 = lista_setpoint_co2;

            List<Clases.Objeto> lista_setpoint_temp = MantenedorModels.GetSetpointsTemperaturaServicioNuevo();
            ViewBag.lista_setpoint_temp = lista_setpoint_temp;

            return View();
        }

        public class TextBoxModal
        {
            [Required(ErrorMessage = "Value is required")]
            public int id_servicio { get; set; }
            public string numero_serie { get; set; }
            public string tipo_sensor { get; set; }
            public string contenedor { get; set; }
            public string booking { get; set; }
            public string nave { get; set; }
            public string commodity { get; set; }
            public string naviera { get; set; }
            public string setpoint_co2 { get; set; }
            public string setpoint_temp { get; set; }
            public string lugar_origen { get; set; }
            public string lugar_destino { get; set; }
            public string nombre_lugar_origen { get; set; }
            public string nombre_lugar_destino { get; set; }
            public string pais_input { get; set; }
            public string region_input { get; set; }
            public string ciudad_input { get; set; }
            public string comuna_input { get; set; }
            public string localidad_input { get; set; }
            public string numero_input { get; set; }
            public string codigo_postal_input { get; set; }
            public string latitude_input { get; set; }
            public string longitude_input { get; set; }
            public string pais_input2 { get; set; }
            public string region_input2 { get; set; }
            public string ciudad_input2 { get; set; }
            public string comuna_input2 { get; set; }
            public string localidad_input2 { get; set; }
            public string numero_input2 { get; set; }
            public string codigo_postal_input2 { get; set; }
            public string latitude_input2 { get; set; }
            public string longitude_input2 { get; set; }
            public string servicio_app_servicios { get; set; }


            //Para agregar servicio
            public int id_servicio_agregar { get; set; }
            public string numero_serie_agregar { get; set; }
            public string tipo_sensor_agregar { get; set; }
            public string contenedor_agregar { get; set; }
            public string booking_agregar { get; set; }
            public string nave_agregar { get; set; }
            public string commodity_agregar { get; set; }
            public string naviera_agregar { get; set; }
            public string setpoint_co2_agregar { get; set; }
            public string setpoint_temp_agregar { get; set; }
            public string lugar_origen_agregar { get; set; }
            public string lugar_destino_agregar { get; set; }
            public string nombre_lugar_origen_agregar { get; set; }
            public string nombre_lugar_destino_agregar { get; set; }
            public string pais_input_agregar { get; set; }
            public string region_input_agregar { get; set; }
            public string ciudad_input_agregar { get; set; }
            public string comuna_input_agregar { get; set; }
            public string localidad_input_agregar { get; set; }
            public string numero_input_agregar { get; set; }
            public string codigo_postal_input_agregar { get; set; }
            public string latitude_input_agregar { get; set; }
            public string longitude_input_agregar { get; set; }
            public string pais_input2_agregar { get; set; }
            public string region_input2_agregar { get; set; }
            public string ciudad_input2_agregar { get; set; }
            public string comuna_input2_agregar { get; set; }
            public string localidad_input2_agregar { get; set; }
            public string numero_input2_agregar { get; set; }
            public string codigo_postal_input2_agregar { get; set; }
            public string latitude_input2_agregar { get; set; }
            public string longitude_input2_agregar { get; set; }
            public string servicio_app_servicios_agregar { get; set; }
        }

        public partial class TextboxesController : Controller
        {
            TextBoxModal textbox = new TextBoxModal();
            // GET: TextboxFor
            public ActionResult TextboxFor()
            {
                textbox.numero_serie = "1111";
                return View(textbox);
            }
            [HttpPost]
            public ActionResult TextboxFor(TextBoxModal model)
            {
                //posted value is obtained from the model
                textbox.numero_serie = model.numero_serie;
                return View(textbox);
            }

            // GET: DefaultFunctionalities
            public ActionResult DefaultFunctionalities()
            {
                return View();
            }
        }

        public partial class DialogController : Controller
        {
            // GET: Dialog
            public ActionResult CustomDialogs()
            {
                List<DialogDialogButton> buttons = new List<DialogDialogButton>() { };
                buttons.Add(new DialogDialogButton() { Click = "alertBtnClick", ButtonModel = new customButtonModel() { content = "Dismiss", isPrimary = true } });
                ViewBag.AlertButton = buttons;
                List<DialogDialogButton> button = new List<DialogDialogButton>() { };
                button.Add(new DialogDialogButton() { Click = "confirmBtnClick", ButtonModel = new confirmButtonModel() { content = "Yes", isPrimary = true } });
                button.Add(new DialogDialogButton() { Click = "confirmBtnClick", ButtonModel = new confirmButtonModel() { content = "No" } });
                ViewBag.ConfirmButton = button;
                List<DialogDialogButton> btn = new List<DialogDialogButton>() { };
                btn.Add(new DialogDialogButton() { Click = "promptBtnClick", ButtonModel = new promptButtonModel() { content = "Connect", isPrimary = true } });
                btn.Add(new DialogDialogButton() { Click = "promptBtnClick", ButtonModel = new promptButtonModel() { content = "Cancel" } });
                ViewBag.PromptButton = btn;
                return View();
            }
        }
        public class customButtonModel
        {
            public string content { get; set; }
            public bool isPrimary { get; set; }
        }
        public class confirmButtonModel
        {
            public string content { get; set; }
            public bool isPrimary { get; set; }
        }
        public class promptButtonModel
        {
            public string content { get; set; }
            public bool isPrimary { get; set; }
        }

        public Clases.DataFiltros GetPerfilDataByUser()
        {
            Clases.DataFiltros dataOcultar = new Clases.DataFiltros();
            int perfilData = Convert.ToInt32(Session["PerfilData"]);

            dataOcultar = ServicioModels.GetPerfilDataByUser(perfilData);

            //.var resultados = Json(dataOcultar, JsonRequestBehavior.AllowGet);
            //.MaxJsonLength = Int32.MaxValue;

            return dataOcultar;
        }

        public ActionResult AgregarServicio()
        {
            if (Session["User"] == null)
            {
                return View("../Home/Login");
            }

            int IdUsuario = Convert.ToInt32(Session["PerfilInterior"]);

            Clases.DataFiltros data = new Clases.DataFiltros();
            data = GetPerfilDataByUser();

            ServicioNuevo Servicio = new ServicioNuevo();
            ServicioNuevo servicio_respuesta = new ServicioNuevo();
            ViewBag.desplegarMensaje = 0;

            // SOLICITUD DE GUARDAR SERVICIO (POST)
            if (Request.HttpMethod.ToString() == "POST")
            {
                Servicio.usuario = IdUsuario.ToString();
                Servicio.sensores = Request["sensores_agregar"];
                Servicio.numero_serie = Request["numero_serie_agregar"];
                Servicio.contenedor = Request["contenedor_agregar"];
                Servicio.lugar_origen = "";
                Servicio.lugar_destino = "";
                Servicio.nombre_lugar_origen = "";
                Servicio.pais_origen = "";
                Servicio.region_origen = "";
                Servicio.ciudad_origen = "";
                Servicio.comuna_origen = "";
                Servicio.calle_origen = "";
                Servicio.numero_calle_origen = "";
                Servicio.codigo_postal_origen = "";
                Servicio.latitud_origen = "";
                Servicio.longitud_origen = "";
                Servicio.nombre_lugar_destino = "";
                Servicio.pais_destino = "";
                Servicio.region_destino = "";
                Servicio.ciudad_destino = "";
                Servicio.comuna_destino = "";
                Servicio.calle_destino = "";
                Servicio.numero_calle_destino = "";
                Servicio.codigo_postal_destino = "";
                Servicio.latitud_destino = "";
                Servicio.longitud_destino = "";
                Servicio.etd = null;
                Servicio.eta = null;
                Servicio.booking = "";
                Servicio.id_nave = 0;
                Servicio.commodity = "";
                Servicio.id_naviera = 0;
                Servicio.id_setpoint_co2 = 0;
                Servicio.id_setpoint_temp = 0;

                DateTime? etd = null;
                if (Request["etd_agregar"] != "" && Request["etd_agregar"] != null)
                {
                    etd = Convert.ToDateTime(Request["etd_agregar"]);
                }

                DateTime? eta = null;
                if (Request["eta_agregar"] != "" && Request["eta_agregar"] != null)
                {
                    eta = Convert.ToDateTime(Request["eta_agregar"]);
                }

                Servicio.lugar_origen = Request["lugar_origen_agregar"];
                Servicio.lugar_destino = Request["lugar_destino_agregar"];
                Servicio.nombre_lugar_origen = Request["nombre_lugar_origen_agregar"];
                Servicio.pais_origen = Request["pais_input_agregar"];
                Servicio.region_origen = Request["region_input_agregar"];
                Servicio.ciudad_origen = Request["ciudad_input_agregar"];
                Servicio.comuna_origen = Request["comuna_input_agregar"];
                Servicio.calle_origen = Request["localidad_input_agregar"];
                Servicio.numero_calle_origen = Request["numero_input_agregar"];
                Servicio.codigo_postal_origen = Request["codigo_postal_input_agregar"];
                Servicio.latitud_origen = Request["latitude_input_agregar"];
                Servicio.longitud_origen = Request["longitude_input_agregar"];
                Servicio.nombre_lugar_destino = Request["nombre_lugar_destino_agregar"];
                Servicio.pais_destino = Request["pais_input2_agregar"];
                Servicio.region_destino = Request["region_input2_agregar"];
                Servicio.ciudad_destino = Request["ciudad_input2_agregar"];
                Servicio.comuna_destino = Request["comuna_input2_agregar"];
                Servicio.calle_destino = Request["localidad_input2_agregar"];
                Servicio.numero_calle_destino = Request["numero_input2_agregar"];
                Servicio.codigo_postal_destino = Request["codigo_postal_input2_agregar"];
                Servicio.latitud_destino = Request["latitude_input2_agregar"];
                Servicio.longitud_destino = Request["longitude_input2_agregar"];
                Servicio.etd = etd;
                Servicio.eta = eta;
                Servicio.booking = Request["booking_agregar"];

                Servicio.nave = Request["nave_agregar"];
                Servicio.commodity = Request["commodity_agregar"];
                Servicio.naviera = Request["naviera_agregar"];
                Servicio.id_setpoint_co2 = float.Parse(Request["setpoint_co2_agregar"]);
                Servicio.id_setpoint_temp = float.Parse(Request["setpoint_temp_agregar"]);

                if (Convert.ToInt32(Request["servicio_app_servicios"]) == 0)
                {
                    //DateTime? etd = null;
                    //if (Request["etd_agregar"] != "" && Request["etd_agregar"] != null)
                    //{
                    //    etd = Convert.ToDateTime(Request["etd_agregar"]);
                    //}

                    //DateTime? eta = null;
                    //if (Request["eta_agregar"] != "" && Request["eta_agregar"] != null)
                    //{
                    //    eta = Convert.ToDateTime(Request["eta_agregar"]);
                    //}

                    //Servicio.lugar_origen = Request["lugar_origen_agregar"];
                    //Servicio.lugar_destino = Request["lugar_destino_agregar"];
                    //Servicio.nombre_lugar_origen = Request["nombre_lugar_origen_agregar"];
                    //Servicio.pais_origen = Request["pais_input_agregar"];
                    //Servicio.region_origen = Request["region_input_agregar"];
                    //Servicio.ciudad_origen = Request["ciudad_input_agregar"];
                    //Servicio.comuna_origen = Request["comuna_input_agregar"];
                    //Servicio.calle_origen = Request["localidad_input_agregar"];
                    //Servicio.numero_calle_origen = Request["numero_input_agregar"];
                    //Servicio.codigo_postal_origen = Request["codigo_postal_input_agregar"];
                    //Servicio.latitud_origen = Request["latitude_input_agregar"];
                    //Servicio.longitud_origen = Request["longitude_input_agregar"];
                    //Servicio.nombre_lugar_destino = Request["nombre_lugar_destino_agregar"];
                    //Servicio.pais_destino = Request["pais_input2_agregar"];
                    //Servicio.region_destino = Request["region_input2_agregar"];
                    //Servicio.ciudad_destino = Request["ciudad_input2_agregar"];
                    //Servicio.comuna_destino = Request["comuna_input2_agregar"];
                    //Servicio.calle_destino = Request["localidad_input2_agregar"];
                    //Servicio.numero_calle_destino = Request["numero_input2_agregar"];
                    //Servicio.codigo_postal_destino = Request["codigo_postal_input2_agregar"];
                    //Servicio.latitud_destino = Request["latitude_input2_agregar"];
                    //Servicio.longitud_destino = Request["longitude_input2_agregar"];
                    //Servicio.etd = etd;
                    //Servicio.eta = eta;
                    //Servicio.booking = Request["booking_agregar"];
                    //Servicio.id_nave = Convert.ToInt32(Request["nave_agregar"]);
                    //Servicio.commodity = Request["commodity_agregar"];
                    //Servicio.id_naviera = Convert.ToInt32(Request["naviera_agregar"]);
                    //Servicio.id_setpoint_co2 = Convert.ToInt32(Request["setpoint_co2_agregar"]);
                    //Servicio.id_setpoint_temp = Convert.ToInt32(Request["setpoint_temp_agregar"]);

                    if (Servicio.sensores != null)
                    {
                        if (Servicio.sensores != null && Servicio.numero_serie != null && Servicio.contenedor != null && Servicio.lugar_origen != null && Servicio.lugar_destino != null)
                        {
                            if (Servicio.sensores != "" && Servicio.numero_serie != "" && Servicio.contenedor != "" && Servicio.lugar_origen != "" && Servicio.lugar_destino != "")
                            {
                                servicio_respuesta = ServicioModels.NuevoServicioAlma(Servicio);
                                if (Convert.ToInt32(servicio_respuesta.id) > 0)
                                {
                                    Servicio.id = servicio_respuesta.id;
                                    Servicio.id_origen = servicio_respuesta.id_origen;
                                    Servicio.id_destino = servicio_respuesta.id_destino;
                                    Servicio.id_bd = servicio_respuesta.id_bd;
                                    //ServicioModels.RegistrarServicioEmerson(Servicio);
                                    return RedirectToAction("EditarServicios", "Servicio", new { desplegarMensaje = 1 });
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (Servicio.sensores != null)
                    {
                        if (Servicio.sensores != null && Servicio.numero_serie != null && Servicio.contenedor != null)
                        {
                            if (Servicio.sensores != "" && Servicio.numero_serie != "" && Servicio.contenedor != "")
                            {
                                servicio_respuesta = ServicioModels.NuevoServicioAlma(Servicio);
                                return RedirectToAction("EditarServicios", "Servicio", new { desplegarMensaje = 1 });
                            }
                        }
                    }
                }
            }


            if (Session["User"] == null)
            {
                return View("../Home/Login");
            }

            //CARGA DE MENU
            List<Parentitem> parentitem = new List<Parentitem>();
            List<childItems> childitem = new List<childItems>();

            List<Clases.Seccion> secciones = new List<Clases.Seccion>();
            int perfilSeccion = Convert.ToInt32(Session["PerfilSecciones"]);
            secciones = UsuarioModels.GetPerfilSeccionesByUser(perfilSeccion);

            if (!secciones.Any(x => x.ClaseSeccion == "#misservicios"))
            {

                List<childItems> childitem4 = new List<childItems>();
                parentitem.Add(new Parentitem
                {
                    nodeId = "04",
                    nodeText = "Contenedores",
                    iconCss = "fa fa-5x fa-ship",
                    child = childitem4,
                });

                if (!secciones.Any(x => x.ClaseSeccion == "#TransitoEnCurso"))
                {
                    childitem4.Add(new childItems { nodeId = "04-01", nodeText = "En Tránsito", iconCss = "icon-circle-thin icon", url = "/TransitoEnCurso/TransitoEnCursoView", });
                }
                if (!secciones.Any(x => x.ClaseSeccion == "#HistoricoEnCurso"))
                {
                    childitem4.Add(new childItems { nodeId = "04-02", nodeText = "Históricos", iconCss = "icon-circle-thin icon", url = "/HistoricoEnCurso/HistoricoEnCursoView", });
                }
                if (!secciones.Any(x => x.ClaseSeccion == "#ProgramacionSensores"))
                {
                    childitem4.Add(new childItems { nodeId = "04-03", nodeText = "Programación Sensores", iconCss = "icon-circle-thin icon", url = "/Servicio/EditarServicios", });
                }
            }

            if (!secciones.Any(x => x.ClaseSeccion == "#configuracionusuario"))
            {

                List<childItems> childitem5 = new List<childItems>();
                parentitem.Add(new Parentitem
                {
                    nodeId = "05",
                    nodeText = "Configuración Usuario",
                    iconCss = "fa fa-5x fa-user",
                    child = childitem5,
                });

                if (!secciones.Any(x => x.ClaseSeccion == "#ModificarPass"))
                {
                    childitem5.Add(new childItems { nodeId = "05-01", nodeText = "Modificar Contraseña", iconCss = "icon-circle-thin icon", url = "/Home/ModificarPass", });
                }
                if (!secciones.Any(x => x.ClaseSeccion == "#CorreoSoporte"))
                {
                    childitem5.Add(new childItems { nodeId = "05-02", nodeText = "Enviar Correo a Soporte", iconCss = "icon-circle-thin icon", url = "/Home/CorreoSoporte", });
                }
                if (!secciones.Any(x => x.ClaseSeccion == "#CerrarSesion"))
                {
                    childitem5.Add(new childItems { nodeId = "05-03", nodeText = "Cerrar Sesión", iconCss = "icon-circle-thin icon", url = "/Home/Login", });
                }
            }

            ViewBag.dataSource2 = parentitem;
            //CARGA DE MENU


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

            return View();

        }

        public bool ValidarNumeroContenedor(string containerNumberToCheck)
        {
            //Clean the input string from Chars that are not in the Alphabed
            string containerNumber = CleanConNumberString(containerNumberToCheck);

            //Return true if the input string is empty
            //Used mostly for DataGridView to set the False validation only on false Container Numbers
            //and not empty ones
            if (containerNumber == string.Empty) return true;

            //Return False if the input string has not enough Characters
            if (containerNumber.Length != 11) return false;

            //Get the Sum of the ISO Formula
            double summ = GetSumm(containerNumber);

            //Calculate the Check number with the ISO Formula
            double tempCheckNumber = summ - (Math.Floor(summ / 11) * 11);

            //Set temCheckNumber 0 if it is 10 - In somme cases this is needed
            if (tempCheckNumber == 10) tempCheckNumber = 0;

            //Return true if the calculated check number matches with the input check number
            if (tempCheckNumber == GetCheckNumber(containerNumber))
                return true;

            //If no match return false
            return false;
        }

        private static string CleanConNumberString(string inputString)
        {
            //Set all Chars to Upper
            string resultString = inputString.ToUpper();

            //Loop Trough all chars
            foreach (char c in inputString)
            {
                //Remove Char if its not in the ISO Alphabet
                if (!Alphabet.Keys.Contains(c))
                    resultString = resultString.Replace(c.ToString(), string.Empty); //Remove chars with the String.Replace Method
            }

            //Return the cleaned String
            return resultString;
        }

        private static int GetCheckNumber(string inputString)
        {
            //Loop if string is longer than 1
            if (inputString.Length > 1)
            {
                //Get the last char of the string
                char checkChar = inputString[inputString.Length - 1];

                //Initialise a integer
                int CheckNumber = 0;

                //Parse the last char to a integer
                if (Int32.TryParse(checkChar.ToString(), out CheckNumber))
                    return CheckNumber; //Return the integer if the parsing can be done

            }

            //If parsing can´t be done and the string has just 1 char or is empty
            //Return 11 (A number that can´t be a check number!!!)
            return 11;
        }

        private static double GetSumm(string inputString)
        {
            //Set summ to 0
            double summ = 0;

            //Calculate only if the container string is not empty
            if (inputString.Length > 1)
            {
                //Loop through all chars in the container string
                //EXCEPT the last char!!!
                for (int i = 0; i < inputString.Length - 1; i++)
                {
                    //Get the current char
                    char temChar = inputString[i];

                    //Initialise a integer to represent the char number in the ISO Alphabet
                    //Set it to 0
                    int charNumber = 0;

                    //If Char exists in the Table get it´s number
                    if (Alphabet.Keys.Contains(temChar))
                        charNumber = Alphabet[temChar];

                    //Add the char number to the sum using the ISO Formula
                    summ += charNumber * (Math.Pow(2, i));
                }
            }

            //Return the calculated summ
            return summ;
        }



    }
}