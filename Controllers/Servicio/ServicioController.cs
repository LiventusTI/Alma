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
using System.Globalization;

namespace ProyectoAlmaInicio.Controllers
{
    public class ServicioController : Controller
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

        // Servicios OnGoing
        public ActionResult OnGoingView()
        {
            if (Session["User"] == null)
            {
                return View("../Home/Login");
            }
            return View();
        }

        // Servicios Historicos
        public ActionResult HistoricosView()
        {
            if (Session["User"] == null)
            {
                return View("../Home/Login");
            }
            return View();
        }

        // Detalle del servicio
        public ActionResult DetalleServicioView()
        {
            if (Session["User"] == null)
            {
                return View("../Home/Login");
            }
            return View();
        }

        // Detalle del servicio ROJO!!!--------------------------------------------
        public ActionResult DetalleServicioRojoView()
        {
            if (Session["User"] == null)
            {
                return View("../Home/Login");
            }
            return View();
        }

        // Relacionar sensores con contendedor del servicio 
        public ActionResult RelacionarSensoresView()
        {
            if (Session["User"] == null)
            {
                return View("../Home/Login");
            }
            return View();
        }

        public class TextBoxModal
        {
            [Required(ErrorMessage = "Value is required")]
            public int validarbuscar_contenedor { get; set; }
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
            public string numero_serie_agregar2 { get; set; }
            public string numero_serie_agregar3 { get; set; }
            public string numero_serie_agregar4 { get; set; }
            public string numero_serie_agregar5 { get; set; }
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
            public string descripcion_agregar { get; set; }
            public string contenedor_agregar2 { get; set; }
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

        public JsonResult GetServicios()
        {
            List<Clases.Servicio> totalServicios = new List<Clases.Servicio>();

            int IdUsuario = Convert.ToInt32(Session["PerfilInterior"]);

            Clases.DataFiltros data = new Clases.DataFiltros();
            data = GetPerfilDataByUser();

            totalServicios = ServicioModels.GetServicios(IdUsuario, data);

            //REEMPLAZAR FECHA POR API TECNICA
            //totalServicios = ServicioModels.GetFechaInicioViaje(totalServicios);

            var resultados = Json(totalServicios, JsonRequestBehavior.AllowGet);
            resultados.MaxJsonLength = Int32.MaxValue;
            return resultados;
        }

        public JsonResult GetHistoricoServicios()
        {
            List<Clases.Servicio> totalServicios = new List<Clases.Servicio>();

            int IdUsuario = Convert.ToInt32(Session["PerfilInterior"]);

            Clases.DataFiltros data = new Clases.DataFiltros();
            data = GetPerfilDataByUser();
            
            totalServicios = ServicioModels.GetHistoricoServicios(IdUsuario, data);

            //REEMPLAZAR FECHA POR API TECNICA
            //totalServicios = ServicioModels.GetFechaInicioViaje(totalServicios);

            var resultados = Json(totalServicios, JsonRequestBehavior.AllowGet);
            resultados.MaxJsonLength = Int32.MaxValue;
            return resultados;
        }

        public JsonResult GetMapa()
        {
            List<Clases.Servicio> totalServicios = new List<Clases.Servicio>();
            List<Clases.Servicio> totalServiciosAPIServicio = new List<Clases.Servicio>();
            List<Clases.Servicio> totalServiciosAlma = new List<Clases.Servicio>();

            int IdUsuario = Convert.ToInt32(Session["PerfilInterior"]);
            Clases.MapasInicio UbicacionMapas = new Clases.MapasInicio();

            Clases.DataFiltros data = new Clases.DataFiltros();
            data = GetPerfilDataByUser();

            totalServicios = ServicioModels.GetServiciosConModem(IdUsuario, data);
            totalServiciosAlma = ServicioModels.GetServiciosEmerson(IdUsuario, data);
            totalServiciosAPIServicio = ServicioModels.GetServicios(IdUsuario, data);

            totalServiciosAlma = totalServiciosAlma.Union(totalServiciosAPIServicio).ToList();
            var resultados = Json(null, JsonRequestBehavior.AllowGet);

            if (totalServicios.Count != 0)
            {
                UbicacionMapas.UbicacionHistModem = ServicioModels.GetMapa(totalServicios);
            }

            if (totalServiciosAlma.Count != 0)
            {
                UbicacionMapas.UbicacionHistEmerson = ServicioModels.GetMapaEmerson(totalServiciosAlma);
            }

            resultados = Json(UbicacionMapas, JsonRequestBehavior.AllowGet);
            resultados.MaxJsonLength = Int32.MaxValue;

            return resultados;
        }

        public JsonResult GetMapaLogistico(int IdServicio, int BDorigen)
        {

            Clases.Servicio servicio = new Clases.Servicio();
            List<Clases.Mapa> rutaEmerson = new List<Clases.Mapa>();
            List<Clases.Mapa> rutaMapa = new List<Clases.Mapa>();
            Clases.RangosBeneficio rangos = new Clases.RangosBeneficio();
            rangos = ServicioModels.GetRangosServicio(IdServicio);


            servicio = ServicioModels.GetConsultoUnServicio(IdServicio, BDorigen);


            //REEMPLAZAR FECHA POR API TECNICA
            if (BDorigen == 1) //bd aplicacion de servicios
            {
                servicio = ServicioModels.GetFechaInicioViajeUnServicio(servicio);
            }

            /*servicio.RangoAltoCO2 = rangos.RangoAltoCO2;
            servicio.RangoBajoCO2 = rangos.RangoBajoCO2;
            servicio.RangoAltoO2 = rangos.RangoAltoO2;
            servicio.RangoBajoO2 = rangos.RangoBajoO2;*/
            servicio.EtaPuerto5 = (servicio.EtaPuerto).Value.Date.AddDays(10);

            rutaMapa = ServicioModels.GetMapaLogistico(servicio);
            rutaEmerson = ServicioModels.GetMapaLogisticoEmerson(IdServicio, servicio.BDOrigen);

            servicio.UbicacionHistModem = rutaMapa;
            servicio.UbicacionHistEmerson = rutaEmerson;

            var dato = Json(servicio, JsonRequestBehavior.AllowGet);
            dato.MaxJsonLength = Int32.MaxValue;

            return dato;
        }
        public JsonResult GetRangosServicio(int IdServicio)
        {

            Clases.RangosBeneficio rangos = new Clases.RangosBeneficio();
            rangos = ServicioModels.GetRangosServicio(IdServicio);

            var dato = Json(rangos, JsonRequestBehavior.AllowGet);
            dato.MaxJsonLength = Int32.MaxValue;

            return dato;
        }

        public JsonResult GetConsultoUnServicio(int IdServicio, int BDOrigen)
        {
            Clases.Servicio servicio = new Clases.Servicio();

            servicio = ServicioModels.GetConsultoUnServicio(IdServicio, BDOrigen);
            var dato = Json(servicio, JsonRequestBehavior.AllowGet);
            dato.MaxJsonLength = Int32.MaxValue;

            return dato;
        }

        public JsonResult GetServiciosMapaFiltros(int? contenedor = null, int? naviera = null, string booking = null, int? nave = null, int? porigen = null, int? pdestino = null, int? commodity = null, DateTime? etapuerto = null)
        {
            int IdUsuario = Convert.ToInt32(Session["PerfilInterior"]);
            var resultados = Json(null, JsonRequestBehavior.AllowGet);

            if (contenedor == 0) { contenedor = null; }
            if (naviera == 0) { naviera = null; }
            if (nave == 0) { nave = null; }
            if (porigen == 0) { porigen = null; }
            if (pdestino == 0) { pdestino = null; }
            if (commodity == 0) { commodity = null; }
            if (booking == "") { booking = null; }
            

            Clases.DataFiltros data = new Clases.DataFiltros();
            data = GetPerfilDataByUser();

            List<Clases.Servicio> totalServicios = new List<Clases.Servicio>();
            totalServicios = ServicioModels.GetServiciosMapaFiltros(IdUsuario, data, contenedor, naviera, booking, nave, porigen, pdestino, commodity, etapuerto);

            if (totalServicios.Count != 0)
            {
                List<Clases.Mapa> totalServiciosMapa = new List<Clases.Mapa>();
                totalServiciosMapa = ServicioModels.GetMapa(totalServicios);
                resultados = Json(totalServiciosMapa, JsonRequestBehavior.AllowGet);
            }

            resultados.MaxJsonLength = Int32.MaxValue;
            return resultados;
        }

        public JsonResult GetContadores()
        {
            Clases.Totalizador contador = new Clases.Totalizador();
            int IdUsuario = Convert.ToInt32(Session["PerfilInterior"]);

            Clases.DataFiltros data = new Clases.DataFiltros();
            data = GetPerfilDataByUser();

            contador = ServicioModels.GetContadores(IdUsuario, data);

            var resultado = Json(contador, JsonRequestBehavior.AllowGet);
            return resultado;
        }

        public JsonResult GetContadorTotal()
        {
            int contador = 0;
            int IdUsuario = Convert.ToInt32(Session["PerfilInterior"]);

            Clases.DataFiltros data = new Clases.DataFiltros();
            data = GetPerfilDataByUser();

            contador = ServicioModels.GetContadorTotal(IdUsuario, data);


            var resultado = Json(contador, JsonRequestBehavior.AllowGet);
            return resultado;
        }

        public JsonResult GetContadoresHistorico()
        {
            Clases.Totalizador contador = new Clases.Totalizador();
            int IdUsuario = Convert.ToInt32(Session["PerfilInterior"]);

            Clases.DataFiltros data = new Clases.DataFiltros();
            data = GetPerfilDataByUser();

            contador = ServicioModels.GetContadoresHistorico(IdUsuario, data);

            var resultado = Json(contador, JsonRequestBehavior.AllowGet);
            return resultado;
        }

        public JsonResult GetContadorTotalHistorico()
        {
            int contador = 0;
            int IdUsuario = Convert.ToInt32(Session["PerfilInterior"]);

            Clases.DataFiltros data = new Clases.DataFiltros();
            data = GetPerfilDataByUser();

            contador = ServicioModels.GetContadorTotalHistorico(IdUsuario, data);


            var resultado = Json(contador, JsonRequestBehavior.AllowGet);
            return resultado;
        }

        public JsonResult GetHistoricoServiciosFiltrados(string fechaInicio = "", string fechaTermino = "")
        {
            List<Clases.Servicio> totalServicios = new List<Clases.Servicio>();
            int IdUsuario = Convert.ToInt32(Session["PerfilInterior"]);

            Clases.DataFiltros data = new Clases.DataFiltros();
            data = GetPerfilDataByUser();

            if (fechaInicio != "" && fechaTermino != "")
                totalServicios = ServicioModels.GetHistoricoServiciosFiltrados(IdUsuario, data, fechaInicio, fechaTermino);

            //REEMPLAZAR FECHA POR API TECNICA
            //totalServicios = ServicioModels.GetFechaInicioViaje(totalServicios);

            //else totalServicios = ServicioModels.GetHistoricoServicios(IdUsuario);

            var resultados = Json(totalServicios, JsonRequestBehavior.AllowGet);
            resultados.MaxJsonLength = Int32.MaxValue;
            return resultados;
        }

        public JsonResult GetContadoresHistoricoFiltrados(string fechaInicio = "", string fechaTermino = "")
        {
            Clases.Totalizador contador = new Clases.Totalizador();
            int IdUsuario = Convert.ToInt32(Session["PerfilInterior"]);

            Clases.DataFiltros data = new Clases.DataFiltros();
            data = GetPerfilDataByUser();

            if (fechaInicio != "" && fechaTermino != "")
                contador = ServicioModels.GetContadoresHistoricoFiltrados(IdUsuario, data, fechaInicio, fechaTermino);
            //else contador = ServicioModels.GetContadoresHistorico(IdUsuario);

            var resultado = Json(contador, JsonRequestBehavior.AllowGet);
            return resultado;
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

        public JsonResult GetMapaMarineTraffic()
        {
            List<Clases.Servicio> totalServicios = new List<Clases.Servicio>();

            int IdUsuario = Convert.ToInt32(Session["PerfilInterior"]);

            Clases.DataFiltros data = new Clases.DataFiltros();
            data = GetPerfilDataByUser();

            totalServicios = ServicioModels.GetServicios(IdUsuario, data);
            var resultados = Json(null, JsonRequestBehavior.AllowGet);

            if (totalServicios.Count != 0)
            {
                List<Clases.Mapa> totalServiciosMapa = new List<Clases.Mapa>();
                totalServiciosMapa = ServicioModels.GetMapaMarineTraffic(totalServicios);

                resultados = Json(totalServiciosMapa, JsonRequestBehavior.AllowGet);
            }

            resultados.MaxJsonLength = Int32.MaxValue;

            return resultados;
        }

        public JsonResult GetMapaEmerson()
        {
            List<Clases.Servicio> totalServicios = new List<Clases.Servicio>();

            int IdUsuario = Convert.ToInt32(Session["PerfilInterior"]);

            Clases.DataFiltros data = new Clases.DataFiltros();
            data = GetPerfilDataByUser();

            totalServicios = ServicioModels.GetServicios(IdUsuario, data);
            var resultados = Json(null, JsonRequestBehavior.AllowGet);

            if (totalServicios.Count != 0)
            {
                List<Clases.Mapa> totalServiciosMapa = new List<Clases.Mapa>();
                totalServiciosMapa = ServicioModels.GetMapaEmerson(totalServicios);

                resultados = Json(totalServiciosMapa, JsonRequestBehavior.AllowGet);
            }

            resultados.MaxJsonLength = Int32.MaxValue;

            return resultados;
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
                Servicio.id_destinatario = Convert.ToInt32(Request["destinatario"]);
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


                if (Request["setpoint_co2_agregar"] != "" && Request["setpoint_co2_agregar"] != null)
                {
                    Servicio.id_setpoint_co2 = float.Parse(Request["setpoint_co2_agregar"]);
                }


                if (Request["setpoint_temp_agregar"] != "" && Request["setpoint_temp_agregar"] != null)
                {
                    Servicio.id_setpoint_temp = float.Parse(Request["setpoint_co2_agregar"]);
                }

                Servicio.descripcion = Request["descripcion_agregar"];

                //campos de sensores
                Servicio.tipo_sensor1 = Request["sensores_agregar"];
                Servicio.tipo_sensor2 = Request["sensores_agregar2"];
                Servicio.tipo_sensor3 = Request["sensores_agregar3"];
                Servicio.tipo_sensor4 = Request["sensores_agregar4"];
                Servicio.tipo_sensor5 = Request["sensores_agregar5"];
                Servicio.numero_sensor1 = Request["numero_serie_agregar"];
                Servicio.numero_sensor2 = Request["numero_serie_agregar2"];
                Servicio.numero_sensor3 = Request["numero_serie_agregar3"];
                Servicio.numero_sensor4 = Request["numero_serie_agregar4"];
                Servicio.numero_sensor5 = Request["numero_serie_agregar5"];

                if (ValidarModem(Servicio.numero_sensor1))
                {
                    Servicio.NumModem = Servicio.numero_sensor1;
                }
                else
                {
                    if (ValidarModem(Servicio.numero_sensor2))
                    {
                        Servicio.NumModem = Servicio.numero_sensor2;
                    }
                    else
                    {
                        if (ValidarModem(Servicio.numero_sensor3))
                        {
                            Servicio.NumModem = Servicio.numero_sensor3;
                        }
                        else
                        {
                            if (ValidarModem(Servicio.numero_sensor4))
                            {
                                Servicio.NumModem = Servicio.numero_sensor4;
                            }
                            else
                            {
                                if (ValidarModem(Servicio.numero_sensor5))
                                {
                                    Servicio.NumModem = Servicio.numero_sensor5;
                                }
                                else
                                {
                                    //no hay modem, modem invalido
                                    Servicio.NumModem = "";
                                }
                            }
                        }
                    }
                }
                //Servicio.contenedor = "GONZALEZ 2321";
                Servicio.contenedor = Servicio.contenedor.Replace("-", "");

                servicio_respuesta = ServicioModels.NuevoServicioAlma(Servicio);

                if (Convert.ToInt32(servicio_respuesta.id) > 0)
                {
                    Servicio.id = servicio_respuesta.id;
                    Servicio.id_bd = servicio_respuesta.id_bd;
                    List<Sensor> sensoresEditados = new List<Sensor>();

                    if (Servicio.tipo_sensor1 != null && Servicio.tipo_sensor1 != "" && Servicio.numero_sensor1 != "" && Servicio.numero_sensor1 != null)
                    {
                        Sensor sensor = new Sensor();
                        sensor.tipo_sensor = Servicio.tipo_sensor1;
                        sensor.numSerie = Servicio.numero_sensor1;

                        if (!ValidarModem(sensor.numSerie))
                        {
                            string respuestaConexion = ServicioModels.RegistrarServicioEmerson(Servicio, Servicio.numero_sensor1);
                            if (respuestaConexion == "Servicio de Emerson creado correctamente")
                            {
                                sensor.CargadoEnEmerson = 1;
                                ServicioModels.AsociarSensorContenedor(Servicio.id, Servicio.id_bd, Servicio.tipo_sensor1, Servicio.numero_sensor1, Servicio.usuario);

                            }
                            else
                            {
                                // Aunque la respuesta de emerson sea erronea, si sensor se encuentra en registros de Alma pero sin servicio asociado, se asocia el sensor al servicio nuevo
                                bool sensorPoseeRegistros = ServicioModels.ValidarRegistrosSensorSinServicio(Servicio.numero_sensor1);
                                if (sensorPoseeRegistros)
                                {
                                    sensor.CargadoEnEmerson = 1;
                                    ServicioModels.AsociarSensorContenedor(Servicio.id, Servicio.id_bd, Servicio.tipo_sensor1, Servicio.numero_sensor1, Servicio.usuario);
                                }
                                else
                                {
                                    sensor.CargadoEnEmerson = 0;
                                    ServicioModels.SetExcepcionSensor(999, "Sensor: " + Servicio.numero_sensor1 + " del tipo " + Servicio.tipo_sensor1 + " no se puedo agregar al servicio " + Servicio.id + " de la base de datos " + Servicio.id_bd);
                                }
                            }

                            sensoresEditados.Add(sensor);
                        }

                        if (Servicio.tipo_sensor2 != null && Servicio.tipo_sensor2 != "" && Servicio.numero_sensor2 != "" && Servicio.numero_sensor2 != null)
                        {
                            Sensor sensor2 = new Sensor();
                            sensor2.tipo_sensor = Servicio.tipo_sensor2;
                            sensor2.numSerie = Servicio.numero_sensor2;

                            if (!ValidarModem(sensor2.numSerie))
                            {
                                string respuestaConexion2 = ServicioModels.RegistrarServicioEmerson(Servicio, Servicio.numero_sensor2);
                                if (respuestaConexion2 == "Servicio de Emerson creado correctamente")
                                {
                                    sensor2.CargadoEnEmerson = 1;
                                    ServicioModels.AsociarSensorContenedor(Servicio.id, Servicio.id_bd, Servicio.tipo_sensor2, Servicio.numero_sensor2, Servicio.usuario);
                                }
                                else
                                {
                                    // Aunque la respuesta de emerson sea erronea, si sensor se encuentra en registros de Alma pero sin servicio asociado, se asocia el sensor al servicio nuevo
                                    bool sensorPoseeRegistros = ServicioModels.ValidarRegistrosSensorSinServicio(Servicio.numero_sensor2);
                                    if (sensorPoseeRegistros)
                                    {
                                        sensor2.CargadoEnEmerson = 1;
                                        ServicioModels.AsociarSensorContenedor(Servicio.id, Servicio.id_bd, Servicio.tipo_sensor2, Servicio.numero_sensor2, Servicio.usuario);
                                    }
                                    else
                                    {
                                        sensor2.CargadoEnEmerson = 0;
                                        ServicioModels.SetExcepcionSensor(999, "Sensor: " + Servicio.numero_sensor2 + " del tipo " + Servicio.tipo_sensor2 + " no se puedo agregar al servicio " + Servicio.id + " de la base de datos " + Servicio.id_bd);
                                    }
                                }

                                sensoresEditados.Add(sensor2);
                            }

                            if (Servicio.tipo_sensor3 != null && Servicio.tipo_sensor3 != "" && Servicio.numero_sensor3 != "" && Servicio.numero_sensor3 != null)
                            {
                                Sensor sensor3 = new Sensor();
                                sensor3.tipo_sensor = Servicio.tipo_sensor3;
                                sensor3.numSerie = Servicio.numero_sensor3;

                                if (!ValidarModem(sensor3.numSerie))
                                {
                                    string respuestaConexion3 = ServicioModels.RegistrarServicioEmerson(Servicio, Servicio.numero_sensor3);
                                    if (respuestaConexion3 == "Servicio de Emerson creado correctamente")
                                    {
                                        sensor3.CargadoEnEmerson = 1;
                                        ServicioModels.AsociarSensorContenedor(Servicio.id, Servicio.id_bd, Servicio.tipo_sensor3, Servicio.numero_sensor3, Servicio.usuario);
                                    }
                                    else
                                    {
                                        // Aunque la respuesta de emerson sea erronea, si sensor se encuentra en registros de Alma pero sin servicio asociado, se asocia el sensor al servicio nuevo
                                        bool sensorPoseeRegistros = ServicioModels.ValidarRegistrosSensorSinServicio(Servicio.numero_sensor3);
                                        if (sensorPoseeRegistros)
                                        {
                                            sensor3.CargadoEnEmerson = 1;
                                            ServicioModels.AsociarSensorContenedor(Servicio.id, Servicio.id_bd, Servicio.tipo_sensor3, Servicio.numero_sensor3, Servicio.usuario);
                                        }
                                        else
                                        {
                                            sensor3.CargadoEnEmerson = 0;
                                            ServicioModels.SetExcepcionSensor(999, "Sensor: " + Servicio.numero_sensor3 + " del tipo " + Servicio.tipo_sensor3 + " no se puedo agregar al servicio " + Servicio.id + " de la base de datos " + Servicio.id_bd);
                                        }
                                    }

                                    sensoresEditados.Add(sensor3);
                                }

                                if (Servicio.tipo_sensor4 != null && Servicio.tipo_sensor4 != "" && Servicio.numero_sensor4 != "" && Servicio.numero_sensor4 != null)
                                {
                                    Sensor sensor4 = new Sensor();
                                    sensor4.tipo_sensor = Servicio.tipo_sensor4;
                                    sensor4.numSerie = Servicio.numero_sensor4;

                                    if (!ValidarModem(sensor4.numSerie))
                                    {
                                        string respuestaConexion4 = ServicioModels.RegistrarServicioEmerson(Servicio, Servicio.numero_sensor4);
                                        if (respuestaConexion4 == "Servicio de Emerson creado correctamente")
                                        {
                                            sensor4.CargadoEnEmerson = 1;
                                            ServicioModels.AsociarSensorContenedor(Servicio.id, Servicio.id_bd, Servicio.tipo_sensor4, Servicio.numero_sensor4, Servicio.usuario);
                                        }
                                        else
                                        {
                                            // Aunque la respuesta de emerson sea erronea, si sensor se encuentra en registros de Alma pero sin servicio asociado, se asocia el sensor al servicio nuevo
                                            bool sensorPoseeRegistros = ServicioModels.ValidarRegistrosSensorSinServicio(Servicio.numero_sensor4);
                                            if (sensorPoseeRegistros)
                                            {
                                                sensor4.CargadoEnEmerson = 1;
                                                ServicioModels.AsociarSensorContenedor(Servicio.id, Servicio.id_bd, Servicio.tipo_sensor4, Servicio.numero_sensor4, Servicio.usuario);
                                            }
                                            else
                                            {
                                                sensor4.CargadoEnEmerson = 0;
                                                ServicioModels.SetExcepcionSensor(999, "Sensor: " + Servicio.numero_sensor4 + " del tipo " + Servicio.tipo_sensor4 + " no se puedo agregar al servicio " + Servicio.id + " de la base de datos " + Servicio.id_bd);
                                            }
                                        }

                                        sensoresEditados.Add(sensor4);
                                    }

                                    if (Servicio.tipo_sensor5 != null && Servicio.tipo_sensor5 != "" && Servicio.numero_sensor5 != "" && Servicio.numero_sensor5 != null)
                                    {
                                        Sensor sensor5 = new Sensor();
                                        sensor5.tipo_sensor = Servicio.tipo_sensor5;
                                        sensor5.numSerie = Servicio.numero_sensor5;

                                        if (!ValidarModem(sensor4.numSerie))
                                        {
                                            string respuestaConexion5 = ServicioModels.RegistrarServicioEmerson(Servicio, Servicio.numero_sensor5);
                                            if (respuestaConexion5 == "Servicio de Emerson creado correctamente")
                                            {
                                                sensor5.CargadoEnEmerson = 1;
                                                ServicioModels.AsociarSensorContenedor(Servicio.id, Servicio.id_bd, Servicio.tipo_sensor5, Servicio.numero_sensor5, Servicio.usuario);
                                            }
                                            else
                                            {
                                                // Aunque la respuesta de emerson sea erronea, si sensor se encuentra en registros de Alma pero sin servicio asociado, se asocia el sensor al servicio nuevo
                                                bool sensorPoseeRegistros = ServicioModels.ValidarRegistrosSensorSinServicio(Servicio.numero_sensor5);
                                                if (sensorPoseeRegistros)
                                                {
                                                    sensor5.CargadoEnEmerson = 1;
                                                    ServicioModels.AsociarSensorContenedor(Servicio.id, Servicio.id_bd, Servicio.tipo_sensor5, Servicio.numero_sensor5, Servicio.usuario);
                                                }
                                                else
                                                {
                                                    sensor5.CargadoEnEmerson = 0;
                                                    ServicioModels.SetExcepcionSensor(999, "Sensor: " + Servicio.numero_sensor5 + " del tipo " + Servicio.tipo_sensor5 + " no se puedo agregar al servicio " + Servicio.id + " de la base de datos " + Servicio.id_bd);
                                                }
                                            }

                                            sensoresEditados.Add(sensor5);
                                        }
                                    }
                                }
                            }
                        }
                    }

                    string listaSensoresSinError = "";
                    string listaSensoresError = "";
                    foreach (Clases.Sensor sensorAct in sensoresEditados)
                    {
                        if (sensorAct.CargadoEnEmerson == 1)
                        {
                            if (listaSensoresSinError == "")
                            {
                                listaSensoresSinError = sensorAct.numSerie;
                            }
                            else
                            {
                                listaSensoresSinError = listaSensoresSinError + "-" + sensorAct.numSerie;
                            }

                        }
                        else
                        {
                            if (listaSensoresError == "")
                            {
                                listaSensoresError = sensorAct.numSerie;
                            }
                            else
                            {
                                listaSensoresError = listaSensoresError + "-" + sensorAct.numSerie;
                            }
                        }
                    }



                    //string respuestaConexion = ServicioModels.RegistrarServicioEmerson(Servicio);
                    //if (respuestaConexion == "Servicio de Emerson creado correctamente")
                    //{
                    return RedirectToAction("TransitoEnCursoView", "TransitoEnCurso", new { desplegarMensaje = 1, sensoresConError = listaSensoresError, sensoresSinError = listaSensoresSinError });
                    //}
                    //else
                    //{
                    //    return RedirectToAction("AgregarContenedorView", "AgregarContenedor", new { desplegarMensaje = 4 });
                    //}
                }
                else
                {
                    return RedirectToAction("AgregarContenedorView", "AgregarContenedor", new { desplegarMensaje = 4 });
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

        public ActionResult EditarServicioPorModal()
        {
            int IdUsuario = Convert.ToInt32(Session["PerfilInterior"]);

            Clases.DataFiltros data = new Clases.DataFiltros();
            data = GetPerfilDataByUser();

            ServicioNuevo Servicio = new ServicioNuevo();
            ServicioNuevo servicio_respuesta = new ServicioNuevo();
            ViewBag.desplegarMensaje = 0;

            // SOLICITUD DE GUARDAR SERVICIO (POST)
            if (Request.HttpMethod.ToString() == "POST")
            {
                Servicio.contenedor = Request["contenedor_agregar2"];
                Servicio.usuario = IdUsuario.ToString();
                Servicio.sensores = Request["sensores_agregar"];
                Servicio.numero_serie = Request["numero_serie_agregar"];

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
                Servicio.descripcion = Request["descripcion_agregar"];

                //campos de sensores
                Servicio.tipo_sensor1 = Request["sensores_agregar"];
                Servicio.tipo_sensor2 = Request["sensores_agregar2"];
                Servicio.tipo_sensor3 = Request["sensores_agregar3"];
                Servicio.tipo_sensor4 = Request["sensores_agregar4"];
                Servicio.tipo_sensor5 = Request["sensores_agregar5"];
                Servicio.numero_sensor1 = Request["numero_serie_agregar"];
                Servicio.numero_sensor2 = Request["numero_serie_agregar2"];
                Servicio.numero_sensor3 = Request["numero_serie_agregar3"];
                Servicio.numero_sensor4 = Request["numero_serie_agregar4"];
                Servicio.numero_sensor5 = Request["numero_serie_agregar5"];


                if (ValidarModem(Servicio.numero_sensor1))
                {
                    Servicio.NumModem = Servicio.numero_sensor1;
                    Servicio.EsModem = 1;
                }
                else {
                    if (ValidarModem(Servicio.numero_sensor2))
                    {
                        Servicio.NumModem = Servicio.numero_sensor2;
                        Servicio.EsModem = 1;
                    }
                    else {
                        if (ValidarModem(Servicio.numero_sensor3))
                        {
                            Servicio.NumModem = Servicio.numero_sensor3;
                            Servicio.EsModem = 1;
                        }
                        else {
                            if (ValidarModem(Servicio.numero_sensor4))
                            {
                                Servicio.NumModem = Servicio.numero_sensor4;
                                Servicio.EsModem = 1;
                            }
                            else {
                                if (ValidarModem(Servicio.numero_sensor5))
                                {
                                    Servicio.NumModem = Servicio.numero_sensor5;
                                    Servicio.EsModem = 1;
                                }
                                else {
                                    //no hay modem, modem invalido
                                    Servicio.NumModem = "";
                                    Servicio.EsModem = 0;
                                }
                            }
                        }
                    }
                }

                servicio_respuesta = ServicioModels.EditarNuevoServicioAlma(Servicio);

                Servicio.id = servicio_respuesta.id;
                Servicio.id_bd = servicio_respuesta.id_bd;

                List<Sensor> sensoresSinEditar = ServicioModels.ObtenerSensoresServicio(Servicio.id, Servicio.id_bd);
                List<Sensor> sensoresEditados = new List<Sensor>();

                if (Servicio.numero_sensor1 != null && Servicio.numero_sensor1 != "")
                {
                    if (Servicio.tipo_sensor1 != null && Servicio.tipo_sensor1 != "" && !ValidarModem(Servicio.numero_sensor1))
                    {
                        Sensor sensor = new Sensor();
                        sensor.tipo_sensor = Servicio.tipo_sensor1;
                        sensor.numSerie = Servicio.numero_sensor1;
                        sensoresEditados.Add(sensor);
                    }

                    if (Servicio.numero_sensor2 != null && Servicio.numero_sensor2 != "" )
                    {
                        if (Servicio.tipo_sensor2 != null && Servicio.tipo_sensor2 != "" && !ValidarModem(Servicio.numero_sensor2))
                        {
                            Sensor sensor2 = new Sensor();
                            sensor2.tipo_sensor = Servicio.tipo_sensor2;
                            sensor2.numSerie = Servicio.numero_sensor2;
                            sensoresEditados.Add(sensor2);
                        }

                        if (Servicio.numero_sensor3 != null && Servicio.numero_sensor3 != "")
                        {
                            if (Servicio.tipo_sensor3 != null && Servicio.tipo_sensor3 != "" && !ValidarModem(Servicio.numero_sensor3))
                            {
                                Sensor sensor3 = new Sensor();
                                sensor3.tipo_sensor = Servicio.tipo_sensor3;
                                sensor3.numSerie = Servicio.numero_sensor3;
                                sensoresEditados.Add(sensor3);
                            }

                            if (Servicio.numero_sensor4 != null && Servicio.numero_sensor4 != "")
                            {
                                if (Servicio.tipo_sensor4 != null && Servicio.tipo_sensor4 != "" && !ValidarModem(Servicio.numero_sensor4))
                                {
                                    Sensor sensor4 = new Sensor();
                                    sensor4.tipo_sensor = Servicio.tipo_sensor4;
                                    sensor4.numSerie = Servicio.numero_sensor4;
                                    sensoresEditados.Add(sensor4);
                                }

                                if (Servicio.numero_sensor5 != null && Servicio.numero_sensor5 != "")
                                {
                                    if (Servicio.tipo_sensor4 != null && Servicio.tipo_sensor4 != "" && !ValidarModem(Servicio.numero_sensor5))
                                    {
                                        Sensor sensor5 = new Sensor();
                                        sensor5.tipo_sensor = Servicio.tipo_sensor5;
                                        sensor5.numSerie = Servicio.numero_sensor5;
                                        sensoresEditados.Add(sensor5);
                                    }
                                }
                            }
                        }
                    }
                }

                List<Sensor> SensoresActuales = CompararActualizarSensor(sensoresEditados, sensoresSinEditar, Servicio);
                string listaSensoresSinError = "";
                string listaSensoresError = "";
                foreach (Clases.Sensor sensorAct in SensoresActuales)
                {
                    if(sensorAct.CargadoEnEmerson == 1) //16-02-2021 Cambiado a '1', ya que al estar en '0' arrojaba mensaje de error, estando incorrecta la validacion (se copia del agregar servicio)
                    { 
                        if(listaSensoresError == "")
                        {
                            listaSensoresError = sensorAct.numSerie;
                        }else
                        {
                            listaSensoresError = listaSensoresError + "-" + sensorAct.numSerie;
                        }
                        
                    }
                    else
                    {
                        if (listaSensoresSinError == "")
                        {
                            listaSensoresSinError = sensorAct.numSerie;
                        }
                        else
                        {
                            listaSensoresSinError = listaSensoresSinError + "-" + sensorAct.numSerie;
                        }
                    }
                }


                //string respuestaConexion = ServicioModels.RegistrarServicioEmerson(Servicio);
                //if (respuestaConexion == "Servicio de Emerson creado correctamente")
                //{
                    return RedirectToAction("TransitoEnCursoView", "TransitoEnCurso", new { desplegarMensaje = 2, sensoresConError=listaSensoresError, sensoresSinError=listaSensoresSinError});
                //}
                //else
                //{
                //    return RedirectToAction("TransitoEnCursoView", "TransitoEnCurso", new { desplegarMensaje = 4, sensoresConError = listaSensoresError, sensoresSinError = listaSensoresSinError });
                //}
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


        public JsonResult EditarServicio(int destinatario=0, string contenedor="", string origen="", string destino="", string etd="", string eta="", string booking="", string nave="", string commodity="", string naviera="", string setpoint_co2="", string setpoint_temp="", string descripcion="", string tipo_sensor1="", string numero_sensor1="", string tipo_sensor2="", string numero_sensor2="", string tipo_sensor3="", string numero_sensor3="", string tipo_sensor4="", string numero_sensor4="", string tipo_sensor5="", string numero_sensor5="")
        {
            CultureInfo myCIintl = new CultureInfo("es-ES", true);

            int IdUsuario = Convert.ToInt32(Session["PerfilInterior"]);
            ServicioNuevo Servicio = new ServicioNuevo();
            ServicioNuevo servicio_respuesta = new ServicioNuevo();

            DateTime? etd_final = null;
            if (etd != null)
            {
                etd_final = Convert.ToDateTime(etd);

            }

            DateTime? eta_final = null;
            if (eta != "" && eta != null)
            {
                eta_final = Convert.ToDateTime(eta);
            }

            Servicio.id_destinatario = destinatario;
            Servicio.contenedor = contenedor;
            Servicio.usuario = IdUsuario.ToString();
            Servicio.lugar_origen = origen;
            Servicio.lugar_destino = destino;
            Servicio.etd_string = etd;
            Servicio.eta_string = eta;
            Servicio.etd = etd_final;
            Servicio.eta = eta_final;
            Servicio.booking = booking;
            Servicio.nave = nave;
            Servicio.commodity = commodity;
            Servicio.naviera = naviera;
            Servicio.setpoint_co2 = setpoint_co2;
            Servicio.setpoint_temp = setpoint_temp;
            Servicio.id_setpoint_co2 = float.Parse(setpoint_co2);
            Servicio.id_setpoint_temp = float.Parse(setpoint_temp);
            Servicio.descripcion = descripcion;
            Servicio.tipo_sensor1 = tipo_sensor1;
            Servicio.tipo_sensor2 = tipo_sensor2;
            Servicio.tipo_sensor3 = tipo_sensor3;
            Servicio.tipo_sensor4 = tipo_sensor4;
            Servicio.tipo_sensor5 = tipo_sensor5;
            Servicio.numero_sensor1 = numero_sensor1;
            Servicio.numero_sensor2 = numero_sensor2;
            Servicio.numero_sensor3 = numero_sensor3;
            Servicio.numero_sensor4 = numero_sensor4;
            Servicio.numero_sensor5 = numero_sensor5;

            if (ValidarModem(Servicio.numero_sensor1))
            {
                Servicio.NumModem = Servicio.numero_sensor1;
                Servicio.EsModem = 1;
            }
            else
            {
                if (ValidarModem(Servicio.numero_sensor2))
                {
                    Servicio.NumModem = Servicio.numero_sensor2;
                    Servicio.EsModem = 1;
                }
                else
                {
                    if (ValidarModem(Servicio.numero_sensor3))
                    {
                        Servicio.NumModem = Servicio.numero_sensor3;
                        Servicio.EsModem = 1;
                    }
                    else
                    {
                        if (ValidarModem(Servicio.numero_sensor4))
                        {
                            Servicio.NumModem = Servicio.numero_sensor4;
                            Servicio.EsModem = 1;
                        }
                        else
                        {
                            if (ValidarModem(Servicio.numero_sensor5))
                            {
                                Servicio.NumModem = Servicio.numero_sensor5;
                                Servicio.EsModem = 1;
                            }
                            else
                            {
                                //no hay modem, modem invalido
                                Servicio.NumModem = "";
                                Servicio.EsModem = 0;
                            }
                        }
                    }
                }
            }

            servicio_respuesta = ServicioModels.EditarNuevoServicioAlma(Servicio);

            Servicio.id = servicio_respuesta.id;
            Servicio.id_bd = servicio_respuesta.id_bd;

            List<Sensor> sensoresSinEditar = ServicioModels.ObtenerSensoresServicio(Servicio.id, Servicio.id_bd);
            List<Sensor> sensoresEditados = new List<Sensor>();

            if (Servicio.numero_sensor1 != null && Servicio.numero_sensor1 != "")
            {
                Sensor sensor = new Sensor();
                sensor.tipo_sensor = Servicio.tipo_sensor1;
                sensor.numSerie = Servicio.numero_sensor1;

                if (!ValidarModem(sensor.numSerie))
                {
                    sensoresEditados.Add(sensor);
                }

                if (Servicio.numero_sensor2 != null && Servicio.numero_sensor2 != "")
                {
                    Sensor sensor2 = new Sensor();
                    sensor2.tipo_sensor = Servicio.tipo_sensor2;
                    sensor2.numSerie = Servicio.numero_sensor2;

                    if (!ValidarModem(sensor2.numSerie))
                    {
                        sensoresEditados.Add(sensor2);
                    }

                    if (Servicio.numero_sensor3 != null && Servicio.numero_sensor3 != "")
                    {
                        Sensor sensor3 = new Sensor();
                        sensor3.tipo_sensor = Servicio.tipo_sensor3;
                        sensor3.numSerie = Servicio.numero_sensor3;

                        if (!ValidarModem(sensor3.numSerie))
                        {
                            sensoresEditados.Add(sensor3);
                        }

                        if (Servicio.numero_sensor4 != null && Servicio.numero_sensor4 != "")
                        {
                            Sensor sensor4 = new Sensor();
                            sensor4.tipo_sensor = Servicio.tipo_sensor4;
                            sensor4.numSerie = Servicio.numero_sensor4;

                            if (!ValidarModem(sensor4.numSerie))
                            {
                                sensoresEditados.Add(sensor4);
                            }

                            if (Servicio.numero_sensor4 != null && Servicio.numero_sensor4 != "")
                            {
                                Sensor sensor5 = new Sensor();
                                sensor5.tipo_sensor = Servicio.tipo_sensor5;
                                sensor5.numSerie = Servicio.numero_sensor5;

                                if (!ValidarModem(sensor5.numSerie))
                                {
                                    sensoresEditados.Add(sensor5);
                                }
                            }
                        }
                    }
                }
            }

            List<Sensor> lista_sensores_resultados = CompararActualizarSensor(sensoresEditados, sensoresSinEditar, Servicio);
            var resultados = Json(null, JsonRequestBehavior.AllowGet);
            resultados = Json(lista_sensores_resultados, JsonRequestBehavior.AllowGet);
            resultados.MaxJsonLength = Int32.MaxValue;
            return resultados;
        }


        public static List<Sensor> CompararActualizarSensor(List<Sensor> sensoresEditados, List<Sensor> sensoresSinEditar, ServicioNuevo Servicio)
        {
            List<Sensor> lista_sensores_respuesta = new List<Sensor>();

            foreach (Sensor sensor_actual in sensoresSinEditar)
            {
                //sensor_actual.Existe = 0; 
                sensor_actual.CargadoEnEmerson = 0;

                //if (sensor_actual.tipo_sensor != "CO2 Generico")
                //{
                    foreach (Sensor sensor_editado in sensoresEditados)
                    {
                        //sensor_editado.Existe = 0; 
                        sensor_editado.CargadoEnEmerson = 0;

                        if (sensor_editado.numSerie == sensor_actual.numSerie)
                        {
                            sensor_actual.Existe = 1;
                            sensor_editado.Existe = 1;

                            if (sensor_editado.tipo_sensor != sensor_actual.tipo_sensor)
                            {
                                //cambiar tipo de sensor
                            }
                        }
                    }
                //}
            }

            foreach (Sensor sensor_actual in sensoresSinEditar)
            {
                if (sensor_actual.Existe == 0 && sensor_actual.tipo_sensor != "CO2 Generico")
                {
                    //descativar relacion_sensor_servicio
                    ServicioModels.EliminarSensor(Servicio.id, sensor_actual.numSerie);

                    Sensor sensor_eliminado = new Sensor();
                    sensor_eliminado.numSerie = sensor_actual.numSerie;
                    sensor_eliminado.tipo_sensor = sensor_actual.tipo_sensor;
                    sensor_eliminado.Existe = 0;
                    lista_sensores_respuesta.Add(sensor_eliminado);
                }
            }

            foreach (Sensor sensor_editado in sensoresEditados)
            {
                if (sensor_editado.Existe == 0 && sensor_editado.tipo_sensor != "CO2 Generico")
                {
                    Sensor sensor_agregado = new Sensor();
                    sensor_agregado.numSerie = sensor_editado.numSerie;
                    sensor_agregado.tipo_sensor = sensor_editado.tipo_sensor;

                    string respuestaConexion = ServicioModels.RegistrarServicioEmerson(Servicio, sensor_editado.numSerie);
                    if (respuestaConexion == "Servicio de Emerson creado correctamente")
                    {
                        //agregar en base de datos
                        sensor_editado.CargadoEnEmerson = 1;
                        ServicioModels.AsociarSensorContenedor(Servicio.id, Servicio.id_bd, sensor_editado.tipo_sensor, sensor_editado.numSerie, Servicio.usuario);
                        sensor_agregado.Existe = 1;
                    }
                    else
                    {
                        // Aunque la respuesta de emerson sea erronea, si sensor se encuentra en registros de Alma pero sin servicio asociado, se asocia el sensor al servicio nuevo
                        bool sensorPoseeRegistros = ServicioModels.ValidarRegistrosSensorSinServicio(sensor_editado.numSerie);
                        if (sensorPoseeRegistros)
                        {
                            sensor_editado.CargadoEnEmerson = 1;
                            sensor_agregado.Existe = 1;
                            ServicioModels.AsociarSensorContenedor(Servicio.id, Servicio.id_bd, sensor_editado.tipo_sensor, sensor_editado.numSerie, Servicio.usuario);
                        }
                        else
                        {
                            sensor_editado.CargadoEnEmerson = 0;
                            sensor_agregado.Existe = 2;
                            ServicioModels.SetExcepcionSensor(999, "Sensor: " + sensor_editado.numSerie + " del tipo " + sensor_editado.tipo_sensor + " no se puede agregar al servicio " + Servicio.id + " de la base de datos " + Servicio.id_bd);                            
                        }
                    }

                    lista_sensores_respuesta.Add(sensor_agregado);
                }
            }







            /*foreach (Sensor Edit in sensoresEditados)
            {
                Edit.Existe = 0; Edit.Nuevo = 0; Edit.Eliminado = 0;

                foreach (Sensor NotEdit in sensoresSinEditar)
                {
                    NotEdit.Existe = 0; NotEdit.Nuevo = 0; NotEdit.Eliminado = 0; Edit.CargadoEnEmerson = 0;

                    if (Edit.numSerie == NotEdit.numSerie)
                    { 
                        Edit.Existe = 1;    //Existe, quizas ha sido modificado
                        NotEdit.Existe = 1;   //Existe, no ha sido eliminado

                        if (Edit.tipo_sensor != NotEdit.tipo_sensor) {
                            //actualizo en bd el tipo de sensor
                        }
                        break;
                    }

                    if (NotEdit == sensoresSinEditar[sensoresSinEditar.Count - 1]) { //nuevo 
                        Edit.Nuevo = 1;
                        //insertar en EMERSON Y BD

                        string respuestaConexion = ServicioModels.RegistrarServicioEmerson(Servicio, NotEdit.numSerie);
                        if (respuestaConexion == "Servicio de Emerson creado correctamente")
                        {
                            Edit.CargadoEnEmerson = 1;
                            ServicioModels.AsociarSensorContenedor(Servicio.id, Servicio.id_bd, NotEdit.tipo_sensor, NotEdit.numSerie, Servicio.usuario);
                            //agregar en base de datos
                        }
                        else
                        {
                            ServicioModels.SetExcepcionSensor(999, "Sensor: " + NotEdit.numSerie + " del tipo " + NotEdit.tipo_sensor + " no se puedo agregar al servicio " + Servicio.id + " de la base de datos " + Servicio.id_bd);
                            //agregar en excepciones
                        }
                        
                    }
                }
            }*/

            /*foreach (Sensor NotEdit in sensoresSinEditar)
            {
                if (NotEdit.Existe != 1) {
                    //eliminado, actualizar bd

                    ServicioModels.EliminarSensor(Servicio.id, NotEdit.numSerie);
                }
            }*/

            return lista_sensores_respuesta;
        }

        public ActionResult EditarServicios()
        {
            int IdUsuario = Convert.ToInt32(Session["PerfilInterior"]);

            Clases.DataFiltros data = new Clases.DataFiltros();
            data = GetPerfilDataByUser();

            ServicioNuevo Servicio = new ServicioNuevo();
            ServicioNuevo servicio_respuesta = new ServicioNuevo();
            ViewBag.desplegarMensaje = 0;

            // SOLICITUD DE EDITAR SERVICIO (POST)
            if (Request.HttpMethod.ToString() == "POST")
            {
                Servicio.id = Convert.ToInt32(Request["id_servicio"]);
                Servicio.usuario = IdUsuario.ToString();
                Servicio.sensores = "";
                Servicio.numero_serie = "";
                Servicio.contenedor = Request["contenedor"];
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

                if (Convert.ToInt32(Request["servicio_app_servicios"]) == 0)
                {
                    DateTime? etd = null;
                    if (Request["etd"] != "" && Request["etd"] != null)
                    {
                        etd = Convert.ToDateTime(Request["etd"]);
                    }

                    DateTime? eta = null;
                    if (Request["eta"] != "" && Request["eta"] != null)
                    {
                        eta = Convert.ToDateTime(Request["eta"]);
                    }

                    Servicio.lugar_origen = Request["lugar_origen"];
                    Servicio.lugar_destino = Request["lugar_destino"];
                    Servicio.nombre_lugar_origen = ""; // Request["nombre_lugar_origen"];
                    Servicio.pais_origen = ""; // Request["pais_input"];
                    Servicio.region_origen = ""; // Request["region_input"];
                    Servicio.ciudad_origen = ""; // Request["ciudad_input"];
                    Servicio.comuna_origen = ""; // Request["comuna_input"];
                    Servicio.calle_origen = ""; // Request["localidad_input"];
                    Servicio.numero_calle_origen = ""; // Request["numero_input"];
                    Servicio.codigo_postal_origen = ""; // Request["codigo_postal_input"];
                    Servicio.latitud_origen = ""; // Request["latitude_input"];
                    Servicio.longitud_origen = ""; // Request["longitude_input"];
                    Servicio.nombre_lugar_destino = ""; // Request["nombre_lugar_destino"];
                    Servicio.pais_destino = ""; // Request["pais_input2"];
                    Servicio.region_destino = ""; // Request["region_input2"];
                    Servicio.ciudad_destino = ""; // Request["ciudad_input2"];
                    Servicio.comuna_destino = ""; // Request["comuna_input2"];
                    Servicio.calle_destino = ""; // Request["localidad_input2"];
                    Servicio.numero_calle_destino = ""; // Request["numero_input2"];
                    Servicio.codigo_postal_destino = ""; // Request["codigo_postal_input2"];
                    Servicio.latitud_destino = ""; // Request["latitude_input2"];
                    Servicio.longitud_destino = ""; // Request["longitude_input2"];
                    Servicio.etd = etd;
                    Servicio.eta = eta;
                    Servicio.booking = Request["booking"];
                    Servicio.id_nave = Convert.ToInt32(Request["nave"]);
                    Servicio.commodity = Request["commodity"];
                    Servicio.id_naviera = Convert.ToInt32(Request["naviera"]);

                    string setpoint_co2_string = Request["setpoint_co2"];
                    string setpoint_temp_string = Request["setpoint_temp"];

                    if (Request["setpoint_co2"] != null) Servicio.id_setpoint_co2 = Convert.ToInt32(Request["setpoint_co2"]);
                    if (Request["setpoint_temp"] != null) Servicio.id_setpoint_temp = Convert.ToInt32(Request["setpoint_temp"]);

                    if (Servicio.contenedor != null && Servicio.lugar_origen != null && Servicio.lugar_destino != null)
                    {
                        if (Servicio.contenedor != "" && Servicio.lugar_origen != "" && Servicio.lugar_destino != "")
                        {
                            servicio_respuesta = ServicioModels.EditarServicioAlma(Servicio);
                            if (Convert.ToInt32(servicio_respuesta.id) > 0)
                            {
                                return RedirectToAction("EditarServicios", "Servicio", new { desplegarMensaje = 2 });
                            }
                        }
                    }

                }
            }




            List<Clases.Servicio> serviciosLista = new List<Clases.Servicio>();

            serviciosLista = ServicioModels.GetSensoresProgramados(IdUsuario, data);
            ViewBag.DataSource = serviciosLista;

            /*            List<Clases.Servicio> totalServicios = new List<Clases.Servicio>();

            totalServicios = ServicioModels.GetServicios(IdUsuario, data);

            var resultados = Json(totalServicios, JsonRequestBehavior.AllowGet);
            resultados.MaxJsonLength = Int32.MaxValue;

            ViewBag.DataSource = resultados;*/

            List<object> DataRange = new List<object>();
            DataRange.Add(new { Text = "10 Registros", Value = "10" });
            DataRange.Add(new { Text = "50 Registros", Value = "50" });
            DataRange.Add(new { Text = "100 Registros", Value = "100" });
            ViewBag.Data = DataRange;

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

        public Boolean ValidarModem(string numSerie)
        {

            Boolean val = false;

            if (numSerie != null && numSerie != "")
            {
                val = ServicioModels.ValidarModem(numSerie);
            }

            return val;
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

        public JsonResult ObtenerDatosServicio(int IdServicio, int IdBDServicio)
        {
            var resultados = Json(null, JsonRequestBehavior.AllowGet);
            ServicioNuevo Servicio = ServicioModels.ObtenerDatosServicio(IdServicio,IdBDServicio);
            resultados = Json(Servicio, JsonRequestBehavior.AllowGet);
            resultados.MaxJsonLength = Int32.MaxValue;
            return resultados;
        }

        public JsonResult ObtenerSensoresServicio(int IdServicio, int IdBDServicio)
        {
            var resultados = Json(null, JsonRequestBehavior.AllowGet);
            List<Sensor> Sensores = ServicioModels.ObtenerSensoresServicio(IdServicio, IdBDServicio);
            resultados = Json(Sensores, JsonRequestBehavior.AllowGet);
            resultados.MaxJsonLength = Int32.MaxValue;
            return resultados;
        }

        public JsonResult ObtenerSensoresServicioPorContenedor(string Contenedor)
        {
            var resultados = Json(null, JsonRequestBehavior.AllowGet);
            List<Sensor> Sensores = ServicioModels.ObtenerSensoresServicioPorContenedor(Contenedor);
            resultados = Json(Sensores, JsonRequestBehavior.AllowGet);
            resultados.MaxJsonLength = Int32.MaxValue;
            return resultados;
        }

        public JsonResult ObtenerDatosServicioPorContenedor(string Contenedor)
        {
            var resultados = Json(null, JsonRequestBehavior.AllowGet);
            ServicioNuevo Servicio = ServicioModels.ObtenerDatosServicioPorContenedor(Contenedor);
            resultados = Json(Servicio, JsonRequestBehavior.AllowGet);
            resultados.MaxJsonLength = Int32.MaxValue;
            return resultados;
        }

        

        public JsonResult ConsultarServiciosPorId(int id_servicio, int id_bd)
        {
            var resultados = Json(null, JsonRequestBehavior.AllowGet);
            ServicioNuevo Servicio = ServicioModels.ConsultarServiciosPorId(id_servicio, id_bd);
            resultados = Json(Servicio, JsonRequestBehavior.AllowGet);
            resultados.MaxJsonLength = Int32.MaxValue;
            return resultados;
        }

        public JsonResult EliminarServicioPorId(int id_servicio, int id_bd)
        {
            var resultados = Json(null, JsonRequestBehavior.AllowGet);
            ServicioNuevo Servicio = ServicioModels.EliminarServicioPorId(id_servicio, id_bd);
            resultados = Json(Servicio, JsonRequestBehavior.AllowGet);
            resultados.MaxJsonLength = Int32.MaxValue;
            return resultados;
        }

        // GET: Servicio/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Servicio/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Servicio/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Servicio/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Servicio/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Servicio/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Servicio/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        public string ValidarSensorEnServicio(string num_sensor)
        {
            string retorno = ServicioModels.ValidarSensorEnServicio(num_sensor);

            return retorno;

        }
        public bool ValidarContenedorEnServicio(string contenedor)
        {
            bool retorno = ServicioModels.ValidarContenedorEnServicio(contenedor);

            return retorno;

        }

    }
}
