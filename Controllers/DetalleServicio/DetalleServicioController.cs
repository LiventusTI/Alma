using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProyectoAlmaInicio.Models;
using static ProyectoAlmaInicio.Controllers.SidebarController;
using static ProyectoAlmaInicio.Models.Clases;
using Syncfusion.EJ2.Navigations;
using ProyectoAlmaInicio.ServiceReferenceDescifrado;
using System.ServiceModel;

namespace ProyectoAlmaInicio.Controllers.DetalleServicio
{
    public class DetalleServicioController : Controller
    {
        // GET: DetalleServicio
        static DateTime? primerMuestra = null, ultimaMuestra = null;
        static double? primerMuestraCO2 = 0, ultimaMuestraCO2 = 0;
        static DateTime? ultimoServiceData = null;
        static int contMuestrasRevisadas = 0; // inicio query BD outlier modificados 
        static Boolean flag_Zscore = false;


        public ActionResult DetalleServicioView()
        {
            if (Session["User"] == null)
            {
                return View("../Home/Login");
            }

            int IdUsuario = Convert.ToInt32(Session["PerfilInterior"]);
            int perfilSeccion = Convert.ToInt32(Session["PerfilSecciones"]);

            string servicio = Request.Params.Get("IdServicio"); // S31998 (2) - S32139 (1)
            bool permiso = permisoDetalleContenedor(IdUsuario, servicio);
            //if (!permiso)
            //{
            //    return View("../Shared/SinPermiso");
            //}

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


            bool posee_sensor_temp = false;
            bool posee_sensor_co2 = false;
            bool posee_sensor_co2_alma = false;
            bool posee_sensor_o2 = false;
            bool posee_sensor_luz = false;

            List<TabTabItem> orientationItems = new List<TabTabItem>();
            List<object> pestana1 = new List<object>();
            List<object> pestana2 = new List<object>();
            List<object> pestana3 = new List<object>();
            List<object> pestana4 = new List<object>();
            List<StackedLineChartData100> lista_med_co2 = new List<StackedLineChartData100>();
            List<StackedLineChartData100> lista_med_temp = new List<StackedLineChartData100>();
            List<StackedLineChartData100> lista_med_luz = new List<StackedLineChartData100>();


            List<Clases.Sensor> lista_sensores = ConsultarSensoresEmersonPorIdServicio2(servicio);
            int contador_sensores = 0;
            int contador_sensores_temp = 0;
            bool posee_mediciones_temp = false;
            bool posee_mediciones_co2 = false;
            bool posee_mediciones_co2_alma = false;
            bool posee_mediciones_luz = false;
            int contador_sensores_co2 = 0;
            int contador_sensores_luz = 0;
            foreach (var sensor in lista_sensores)
            {
                contador_sensores++;

                List<Medicion> lista_mediciones = ServicioModels.ObtenerTipoMedicionPorTipoSensor(sensor.id_tipo_sensor);
                int contador_mediciones = 0;

                foreach (var medicion in lista_mediciones)
                {
                    contador_mediciones++;

                    if (medicion.id_tipo_medicion == 1) // T°
                    {
                        lista_med_temp = GetGraficoTemp2(servicio);
                        if (lista_med_temp.Count > 0) posee_mediciones_temp = true;
                        contador_sensores_temp++;
                        if (contador_sensores_temp == 1 && sensor.recibioDatos == "SI") { ViewBag.sensor_temp_1 = sensor.numSerie; }
                        else if (contador_sensores_temp == 2 && sensor.recibioDatos == "SI") { ViewBag.sensor_temp_2 = sensor.numSerie; }
                        else if (contador_sensores_temp == 3 && sensor.recibioDatos == "SI") { ViewBag.sensor_temp_3 = sensor.numSerie; }
                        else if (contador_sensores_temp == 4 && sensor.recibioDatos == "SI") { ViewBag.sensor_temp_4 = sensor.numSerie; }
                        else if (contador_sensores_temp == 5 && sensor.recibioDatos == "SI") { ViewBag.sensor_temp_5 = sensor.numSerie; }
                        posee_sensor_temp = true;
                        pestana1.Add(new { Name = "Aun no se han recibido descargas de datos para el sensor de Temperatura asociado", Role = "N° Sensor: " + sensor.numSerie + "   -   Tipo Sensor: " + sensor.tipo_sensor });
                    }
                    else if (medicion.id_tipo_medicion == 2) // Humedad % - O2
                    {
                        posee_sensor_o2 = true;
                        pestana2.Add(new { Name = "Aun no se han recibido descargas de datos para el sensor de O2 asociado", Role = "N° Sensor: " + sensor.numSerie + "   -   Tipo Sensor: " + sensor.tipo_sensor });
                    }
                    else if (medicion.id_tipo_medicion == 3) // CO2
                    {
                        lista_med_co2 = GetGraficoCO2_2(servicio);
                        if (lista_med_co2.Count > 0) posee_mediciones_co2 = true;
                        contador_sensores_co2++;
                        if (contador_sensores_co2 == 1 && sensor.recibioDatos == "SI") { ViewBag.sensor_co2_1 = sensor.numSerie; }
                        else if (contador_sensores_co2 == 2 && sensor.recibioDatos == "SI") { ViewBag.sensor_co2_2 = sensor.numSerie; }
                        else if (contador_sensores_co2 == 3 && sensor.recibioDatos == "SI") { ViewBag.sensor_co2_3 = sensor.numSerie; }
                        else if (contador_sensores_co2 == 4 && sensor.recibioDatos == "SI") { ViewBag.sensor_co2_4 = sensor.numSerie; }
                        else if (contador_sensores_co2 == 5 && sensor.recibioDatos == "SI") { ViewBag.sensor_co2_5 = sensor.numSerie; }
                        posee_sensor_co2 = true;
                        pestana3.Add(new { Name = "Aun no se han recibido descargas de datos para el sensor de CO2 asociado", Role = "N° Sensor: " + sensor.numSerie + "   -   Tipo Sensor: " + sensor.tipo_sensor });

                        if (sensor.id_tipo_sensor == 5) // Modem ALMA
                        {
                            posee_sensor_co2_alma = true;
                            if (sensor.recibioDatos == "SI")
                            {
                                posee_mediciones_co2_alma = true;
                            }
                            /*else
                            {
                                pestana3.Add(new { Name = "Aun no se han recibido descargas de datos para el sensor de CO2 asociado", Role = "N° Sensor: " + sensor.numSerie + "   -   Tipo Sensor: " + sensor.tipo_sensor });
                            }*/
                        }
                    }
                    else if (medicion.id_tipo_medicion == 4) // LUZ
                    {
                        lista_med_luz = GetGraficoLuz2(servicio);
                        if (lista_med_luz.Count > 0) posee_mediciones_luz = true;
                        contador_sensores_luz++;
                        if (contador_sensores_luz == 1 && sensor.recibioDatos == "SI") { ViewBag.sensor_luz_1 = sensor.numSerie; }
                        else if (contador_sensores_luz == 2 && sensor.recibioDatos == "SI") { ViewBag.sensor_luz_2 = sensor.numSerie; }
                        else if (contador_sensores_luz == 3 && sensor.recibioDatos == "SI") { ViewBag.sensor_luz_3 = sensor.numSerie; }
                        else if (contador_sensores_luz == 4 && sensor.recibioDatos == "SI") { ViewBag.sensor_luz_4 = sensor.numSerie; }
                        else if (contador_sensores_luz == 5 && sensor.recibioDatos == "SI") { ViewBag.sensor_luz_5 = sensor.numSerie; }
                        posee_sensor_luz = true;
                        pestana4.Add(new { Name = "Aun no se han recibido descargas de datos para el sensor de Luz asociado", Role = "N° Sensor: " + sensor.numSerie + "   -   Tipo Sensor: " + sensor.tipo_sensor });
                    }
                }
            }



            if (posee_sensor_co2_alma == false)
            {
                bool resp_co2_liventus = poseeGraficoLiventus(servicio);
                if (resp_co2_liventus)
                {
                    posee_sensor_co2_alma = true;
                    posee_mediciones_co2_alma = true;

                    lista_med_co2 = GetGraficoCO2_2(servicio);
                    if (lista_med_co2.Count > 0) posee_mediciones_co2 = true;
                    contador_sensores_co2++;
                    if (contador_sensores_co2 == 1) { ViewBag.sensor_co2_1 = "XXXXX"; }
                    else if (contador_sensores_co2 == 2) { ViewBag.sensor_co2_2 = "XXXXX"; }
                    else if (contador_sensores_co2 == 3) { ViewBag.sensor_co2_3 = "XXXXX"; }
                    else if (contador_sensores_co2 == 4) { ViewBag.sensor_co2_4 = "XXXXX"; }
                    else if (contador_sensores_co2 == 5) { ViewBag.sensor_co2_5 = "XXXXX"; }
                    posee_sensor_co2 = true;
                }
            }

            //SAQUE ESTO DE LAS CONDICIONES DE ABAJO PARA QUE SE CUMPLAN Y NO VALIDEN

            orientationItems.Add(new TabTabItem { Header = new TabHeader { Text = "CO2" }, Content = "#div-co2" });
            orientationItems.Add(new TabTabItem { Header = new TabHeader { Text = "T°" }, Content = "#div-temp" });
            orientationItems.Add(new TabTabItem { Header = new TabHeader { Text = "LUZ" }, Content = "#div-luz" });
            //orientationItems.Add(new TabTabItem { Header = new TabHeader { Text = "O2" }, Content = "#div-o2" });

            if (!posee_sensor_temp)
            {
                pestana1.Add(new { Name = "En esta oportunidad no contrataste el servicio de monitoreo de Temperatura. Ponte en contacto para que en tu próximo viaje si cuentes con el servicio.", Role = " " });
            }
            if (!posee_sensor_o2)
            {
                pestana2.Add(new { Name = "En esta oportunidad no contrataste el servicio de monitoreo de O2. Ponte en contacto para que en tu próximo viaje si cuentes con el servicio.", Role = " " });
            }
            if (posee_sensor_co2 || posee_sensor_co2_alma)
            {
                if (posee_sensor_co2)
                {
                    StackedLineChartData100 st = GetRangoBeneficio(servicio);
                    ViewBag.rango_beneficio_sup = st.high;
                    ViewBag.rango_beneficio_inf = st.low;
                }

            }
            if (!posee_sensor_co2 && !posee_sensor_co2_alma)
            {
                pestana3.Add(new { Name = "En esta oportunidad no contrataste el servicio de monitoreo de CO2. Ponte en contacto para que en tu próximo viaje si cuentes con el servicio.", Role = " " });
            }
            if (!posee_sensor_luz)
            {
                pestana4.Add(new { Name = "En esta oportunidad no contrataste el servicio de monitoreo de Luz. Ponte en contacto para que en tu próximo viaje si cuentes con el servicio.", Role = " " });
            }



            // CALCULAR FECHA LÍMITE GRÁFICOS
            if (!posee_mediciones_co2_alma && !posee_mediciones_temp && !posee_mediciones_co2 && !posee_mediciones_luz)
            {
                ViewBag.year = 1;
                ViewBag.month = 1;
                ViewBag.day = 1;
            }
            else
            {
                if (posee_mediciones_co2_alma || posee_mediciones_co2)
                {
                    if (lista_med_co2.Count > 0)
                    {
                        DateTime? fechaInicial = lista_med_co2[0].x;
                        DateTime? fechaFinal = lista_med_co2[lista_med_co2.Count - 1].x;
                        int numDias = 0;

                        TimeSpan difFechas = Convert.ToDateTime(fechaFinal) - Convert.ToDateTime(fechaInicial);

                        if (difFechas.Days < 30)
                        {
                            numDias = 30 - difFechas.Days;
                        }

                        fechaFinal = Convert.ToDateTime(fechaFinal).Date.AddDays(numDias+1);
                        ViewBag.year = Convert.ToDateTime(fechaFinal).Year;
                        ViewBag.month = Convert.ToDateTime(fechaFinal).Month;
                        ViewBag.day = Convert.ToDateTime(fechaFinal).Day;
                    }
                }
                else if (posee_mediciones_temp)
                {

                    if (lista_med_temp.Count > 0)
                    {
                        DateTime? fechaInicial = lista_med_temp[0].x;
                        DateTime? fechaFinal = lista_med_temp[lista_med_temp.Count - 1].x;
                        int numDias = 0;

                        TimeSpan difFechas = Convert.ToDateTime(fechaFinal) - Convert.ToDateTime(fechaInicial);

                        if (difFechas.Days < 30)
                        {
                            numDias = 30 - difFechas.Days;
                        }

                        fechaFinal = Convert.ToDateTime(fechaFinal).Date.AddDays(numDias+1);
                        ViewBag.year = Convert.ToDateTime(fechaFinal).Year;
                        ViewBag.month = Convert.ToDateTime(fechaFinal).Month;
                        ViewBag.day = Convert.ToDateTime(fechaFinal).Day;
                    }
                }
                else if (posee_mediciones_luz)
                {

                    if (lista_med_luz.Count > 0)
                    {
                        DateTime? fechaInicial = lista_med_luz[0].x;
                        DateTime? fechaFinal = lista_med_luz[lista_med_luz.Count - 1].x;
                        int numDias = 0;
                        TimeSpan difFechas = Convert.ToDateTime(fechaFinal) - Convert.ToDateTime(fechaInicial);

                        if (difFechas.Days < 30)
                        {
                            numDias = 30 - difFechas.Days;
                        }

                        fechaFinal = Convert.ToDateTime(fechaFinal).Date.AddDays(numDias+1);
                        ViewBag.year = Convert.ToDateTime(fechaFinal).Year;
                        ViewBag.month = Convert.ToDateTime(fechaFinal).Month;
                        ViewBag.day = Convert.ToDateTime(fechaFinal).Day;
                    }
                }
            }

            ViewBag.verDivGraficos = 1;
            ViewBag.pestana1Data = pestana1;
            ViewBag.pestana2Data = pestana2;
            ViewBag.pestana3Data = pestana3;
            ViewBag.pestana4Data = pestana4;
            ViewBag.poseeMedicionesCO2Alma = posee_mediciones_co2_alma;
            ViewBag.poseeMedicionesTemp = posee_mediciones_temp;
            ViewBag.poseeMedicionesCO2 = posee_mediciones_co2;
            ViewBag.poseeMedicionesLuz = posee_mediciones_luz;
            ViewBag.orientationItems = orientationItems;
            ViewBag.styleData = new string[] { "Default", "Fill", "Accent" };
            ViewBag.orientationData = new string[] { "Top", "Bottom", "Left", "Right" };

            return View();
        }

        public ActionResult Prueba()
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

            return View();
        }


        [HttpPost]
        public ActionResult GetGraficoCO2(string IdServicio)
        {
            int servicioId = Int32.Parse(IdServicio.Substring(1, IdServicio.Length - 1));
            string BD = IdServicio.Substring(0, 1);
            int BD_origen = 0;

            if (BD == "S")
            {
                BD_origen = 1;
            }
            else if (BD == "A")
            {
                BD_origen = 2;
            }

            List<Clases.Mediciones> medicionesCO2 = ServicioModels.ObtenerMedicionesSensoresCO2(servicioId, BD_origen);
            List<StackedLineChartData100> chartDataCO2 = new List<StackedLineChartData100>();

            //WSDataControllerClient WS = new WSDataControllerClient();

            //Uri BaseAddress = new Uri("http://tempuri.org/IWSDataController/CargarDataController");
            //BasicHttpBinding basicHttpBinding = new BasicHttpBinding();

            //int RetornoWS = WS.CargarDataController(servicioId);
            //medicionesCO2 = ServicioModels.ObtenerMedicionesSensoresCO2(servicioId, BD_origen);

            for (int i = 0; i < medicionesCO2.Count; i++)
            {
                StackedLineChartData100 valornuevo = new StackedLineChartData100();
                valornuevo.id_rel_sensor_servicio = medicionesCO2[i].ID_REL_SENSORSERVICIO;
                valornuevo.x = medicionesCO2[i].FechaMedicion;

                if (valornuevo.id_rel_sensor_servicio == 1) valornuevo.y = medicionesCO2[i].ValorMedido;
                else valornuevo.y = medicionesCO2[i].ValorMedido / 10000;

                chartDataCO2.Add(valornuevo);
            }

            return Json(chartDataCO2);
        }

        public List<StackedLineChartData100> GetGraficoCO2_2(string IdServicio)
        {
            int servicioId = Int32.Parse(IdServicio.Substring(1, IdServicio.Length - 1));
            string BD = IdServicio.Substring(0, 1);
            int BD_origen = 0;

            if (BD == "S")
            {
                BD_origen = 1;
            }
            else if (BD == "A")
            {
                BD_origen = 2;
            }

            List<Clases.Mediciones> medicionesCO2 = ServicioModels.ObtenerMedicionesSensoresCO2(servicioId, BD_origen);
            List<StackedLineChartData100> chartDataCO2 = new List<StackedLineChartData100>();

            for (int i = 0; i < medicionesCO2.Count; i++)
            {
                StackedLineChartData100 valornuevo = new StackedLineChartData100();
                valornuevo.id_rel_sensor_servicio = medicionesCO2[i].ID_REL_SENSORSERVICIO;
                valornuevo.x = medicionesCO2[i].FechaMedicion;
                valornuevo.id_serviceData = medicionesCO2[i].ID_SERVICEDATA;
                if (valornuevo.id_rel_sensor_servicio == 1) valornuevo.y = medicionesCO2[i].ValorMedido;
                else valornuevo.y = medicionesCO2[i].ValorMedido / 10000;
                chartDataCO2.Add(valornuevo);
            }

            return chartDataCO2;
        }

        [HttpPost]
        public StackedLineChartData100 GetRangoBeneficio(string IdServicio)
        {
            int servicioId = Int32.Parse(IdServicio.Substring(1, IdServicio.Length - 1));
            string BD = IdServicio.Substring(0, 1);
            int BD_origen = 0;

            if (BD == "S")
            {
                BD_origen = 1;
            }
            else if (BD == "A")
            {
                BD_origen = 2;
            }

            ServicioNuevo datos_servicio = ServicioModels.ObtenerDatosServicio(servicioId, BD_origen);
            double rango_beneficio_max_co2 = Convert.ToDouble(datos_servicio.setpoint_co2) + 3;
            double rango_beneficio_min_co2 = Convert.ToDouble(datos_servicio.setpoint_co2) - 3;

            if (datos_servicio.commodity == "AVOCADOS")
            {
                if (rango_beneficio_max_co2 > 13) rango_beneficio_max_co2 = 13;
                if (rango_beneficio_min_co2 < 6) rango_beneficio_min_co2 = 6;
            }

            if (datos_servicio.commodity == "BLUEBERRIES")
            {
                if (rango_beneficio_max_co2 > 16) rango_beneficio_max_co2 = 16;
                if (rango_beneficio_min_co2 < 7) rango_beneficio_min_co2 = 7;
            }

            StackedLineChartData100 st = new StackedLineChartData100();
            st.high = rango_beneficio_max_co2;
            st.low = rango_beneficio_min_co2;
            return st;
        }


        [HttpPost]
        public ActionResult GetGraficoTemp(string IdServicio)
        {
            List<Clases.Mediciones> medicionesTemp = new List<Clases.Mediciones>();
            int servicioId = Int32.Parse(IdServicio.Substring(1, IdServicio.Length - 1));
            string BD = IdServicio.Substring(0, 1);
            int BD_origen = 0;

            if (BD == "S")
            {
                BD_origen = 1;
            }
            else if (BD == "A")
            {
                BD_origen = 2;
            }

            medicionesTemp = ServicioModels.ObtenerMedicionesSensoresTemp(servicioId, BD_origen);
            List<StackedLineChartData100> chartDataTemp = new List<StackedLineChartData100>();

            for (int i = 0; i < medicionesTemp.Count; i++)
            {
                StackedLineChartData100 valornuevo = new StackedLineChartData100();
                valornuevo.x = medicionesTemp[i].FechaMedicion;
                valornuevo.y = medicionesTemp[i].ValorMedido;
                valornuevo.id_rel_sensor_servicio = medicionesTemp[i].ID_REL_SENSORSERVICIO;
                chartDataTemp.Add(valornuevo);
            }

            return Json(chartDataTemp);
        }


        [HttpPost]
        public List<StackedLineChartData100> GetGraficoTemp2(string IdServicio)
        {
            int servicioId = Int32.Parse(IdServicio.Substring(1, IdServicio.Length - 1));
            string BD = IdServicio.Substring(0, 1);
            int BD_origen = 0;

            if (BD == "S")
            {
                BD_origen = 1;
            }
            else if (BD == "A")
            {
                BD_origen = 2;
            }

            List<Clases.Mediciones> medicionesTemp = ServicioModels.ObtenerMedicionesSensoresTemp(servicioId, BD_origen);
            List<StackedLineChartData100> chartDataTemp = new List<StackedLineChartData100>();

            for (int i = 0; i < medicionesTemp.Count; i++)
            {
                StackedLineChartData100 valornuevo = new StackedLineChartData100();
                valornuevo.x = medicionesTemp[i].FechaMedicion;
                valornuevo.y = medicionesTemp[i].ValorMedido;
                valornuevo.id_rel_sensor_servicio = medicionesTemp[i].ID_REL_SENSORSERVICIO;
                chartDataTemp.Add(valornuevo);
            }

            return chartDataTemp;
        }


        [HttpPost]
        public ActionResult GetGraficoLuz(string IdServicio)
        {
            int servicioId = Int32.Parse(IdServicio.Substring(1, IdServicio.Length - 1));
            string BD = IdServicio.Substring(0, 1);
            int BD_origen = 0;

            if (BD == "S")
            {
                BD_origen = 1;
            }
            else if (BD == "A")
            {
                BD_origen = 2;
            }

            List<Clases.Mediciones> medicionesLuz = ServicioModels.ObtenerMedicionesSensoresLuz(servicioId, BD_origen);
            List<StackedLineChartData100> chartDataLuz = new List<StackedLineChartData100>();

            for (int i = 0; i < medicionesLuz.Count; i++)
            {
                StackedLineChartData100 valornuevo = new StackedLineChartData100();
                valornuevo.x = medicionesLuz[i].FechaMedicion;
                valornuevo.y = medicionesLuz[i].AperturoPuertas;
                valornuevo.id_rel_sensor_servicio = medicionesLuz[i].ID_REL_SENSORSERVICIO;
                chartDataLuz.Add(valornuevo);
            }

            return Json(chartDataLuz);
        }

        public List<StackedLineChartData100> GetGraficoLuz2(string IdServicio)
        {
            int servicioId = Int32.Parse(IdServicio.Substring(1, IdServicio.Length - 1));
            string BD = IdServicio.Substring(0, 1);
            int BD_origen = 0;

            if (BD == "S")
            {
                BD_origen = 1;
            }
            else if (BD == "A")
            {
                BD_origen = 2;
            }

            List<Clases.Mediciones> medicionesLuz = ServicioModels.ObtenerMedicionesSensoresLuz(servicioId, BD_origen);
            List<StackedLineChartData100> chartDataLuz = new List<StackedLineChartData100>();

            for (int i = 0; i < medicionesLuz.Count; i++)
            {
                StackedLineChartData100 valornuevo = new StackedLineChartData100();
                valornuevo.x = medicionesLuz[i].FechaMedicion;
                valornuevo.y = medicionesLuz[i].AperturoPuertas;
                valornuevo.id_rel_sensor_servicio = medicionesLuz[i].ID_REL_SENSORSERVICIO;
                chartDataLuz.Add(valornuevo);
            }

            return chartDataLuz;
        }

        public JsonResult GetMapaLogistico(string IdServicio)
        {
            string BD = IdServicio.Substring(0, 1);
            int servicioId = Int32.Parse(IdServicio.Substring(1, IdServicio.Length - 1));
            int BD_origen = 0;

            if (BD == "S")
            {
                BD_origen = 1;
            }
            else if (BD == "A")
            {
                BD_origen = 2;
            }

            List<Clases.Servicio> listaServicio = new List<Clases.Servicio>();
            Clases.Servicio servicio = new Clases.Servicio();
            List<Clases.Mapa> rutaEmerson = new List<Clases.Mapa>();
            List<Clases.Mapa> rutaMapa = new List<Clases.Mapa>();
            //Clases.RangosBeneficio rangos = new Clases.RangosBeneficio();
            //rangos = ServicioModels.GetRangosServicio(servicioId);

            servicio = ServicioModels.GetConsultoUnServicio(servicioId, BD_origen);
            if (servicio.SetpointAC != null && servicio.SetpointAC != "")
            {
                servicio.cantSetpointCO2 = Convert.ToInt32(((servicio.SetpointAC).Substring(0, 2)).TrimEnd(' '));
            }
            else
            {
                servicio.cantSetpointCO2 = 0;
            }


            listaServicio.Add(servicio);

            if (servicio.FechaControlador == null)
            {
            }

            ViewBag.servicio = servicio; //{ProyectoAlmaInicio.Models.Clases.Servicio}

            //REEMPLAZAR FECHA POR API TECNICA
            //servicio = ServicioModels.GetFechaInicioViajeUnServicio2(servicio);
            //servicio.FechaControlador = ServicioModels.ConsultarPrimeraFechaSensor(servicioId, BD_origen);

            if (servicio.EtaPuerto != null) servicio.EtaPuerto5 = (servicio.EtaPuerto).Value.Date.AddDays(10);

            /*if (servicio.EtaPuerto != null) servicio.EtaPuerto5 = (servicio.EtaPuerto).Value.Date.AddDays(10);
            else servicio.EtaPuerto5 = servicio.FechaControlador; //eliminar*/

            DateTime? fechaActual = DateTime.Now.ToLocalTime();
            int IdUsuario = Convert.ToInt32(Session["PerfilInterior"]);

            servicio.IdCliente = ServicioModels.GetConsultarIdCliente(IdUsuario);//change

            if (servicio.IdCliente != 1253 && servicio.IdCliente != 1208)
            {
                if (servicio.IdEstadoServicioAlma == 4 && servicio.EtaPuerto5 > servicio.FechaFinalizacion)
                {
                    servicio.EtaPuerto5 = servicio.FechaFinalizacion;
                }

                if (servicio.EtaPuerto5 > fechaActual.Value.Date.AddDays(-1) && servicioId != 31272)
                {
                    servicio.EtaPuerto5 = fechaActual.Value.Date.AddDays(-1);
                }
            }
            else
            {
                servicio.EtaPuerto5 = fechaActual;
            }


            //si id cliente, servicio.EtaPuerto5 = fecha actual

            if (servicio.NumModem != "" && servicio.NumModem != " ")
            {
                rutaMapa = ServicioModels.GetMapaLogistico(servicio);

                // si servicio es de fuera de chile, se ocultan las ubicaciones en chile
                if (rutaMapa.Count() != 0)
                {
                    if (servicio.Origen != "CHILE" && servicio.Origen != "")
                    {
                        rutaMapa = procesoEliminacionUbi(rutaMapa);
                    }
                }
            }

            rutaEmerson = ServicioModels.GetMapaLogisticoEmerson(servicioId, BD_origen);

            List<Clases.Mapa> nuevaRutaMix = new List<Clases.Mapa>();
            List<Clases.Mapa> rutaMixFinal = new List<Clases.Mapa>();

            nuevaRutaMix = rutaEmerson.Union(rutaMapa).ToList();
            nuevaRutaMix = nuevaRutaMix.OrderByDescending(x => x.FechaUbicacion).Reverse().ToList();

            for (int count = 0; count < nuevaRutaMix.Count; count++)
            {
                if (servicio.FechaControlador != null && servicio.FechaControlador < nuevaRutaMix[count].FechaUbicacion)
                    rutaMixFinal.Add(nuevaRutaMix[count]);

            }


            servicio.UbicacionHistModem = rutaMixFinal;
            servicio.UbicacionHistEmerson = rutaEmerson;

            var dato = Json(servicio, JsonRequestBehavior.AllowGet);
            dato.MaxJsonLength = Int32.MaxValue;

            return dato;
        }


        public bool poseeGraficoLiventus(string IdServicio)
        {
            bool respuesta = false;

            string BD = IdServicio.Substring(0, 1);
            int servicioId = Int32.Parse(IdServicio.Substring(1, IdServicio.Length - 1));
            int BD_origen = 0;

            if (BD == "S")
            {
                BD_origen = 1;
            }
            else if (BD == "A")
            {
                BD_origen = 2;
            }

            Clases.Servicio servicio = new Clases.Servicio();
            servicio = ServicioModels.GetConsultoUnServicio(servicioId, BD_origen);

            // 15-02-2021 // El idControlador es vacio, no null //
            if (servicio.IdControlador != null && servicio.IdControlador != " " && servicio.FechaControlador != null)
            {
                respuesta = true;
            }

            return respuesta;
        }


        public JsonResult ValidoServicio(string IdServicio)
        {
            string BD = IdServicio.Substring(0, 1);
            int servicioId = Int32.Parse(IdServicio.Substring(1, IdServicio.Length - 1));
            int BD_origen = 0;

            if (BD == "S")
            {
                BD_origen = 1;
            }
            else if (BD == "A")
            {
                BD_origen = 2;
            }

            List<Clases.Seccion> secciones = new List<Clases.Seccion>();

            secciones = ServicioModels.ValidoServicio(servicioId, BD_origen);

            var dato = Json(secciones, JsonRequestBehavior.AllowGet);
            dato.MaxJsonLength = Int32.MaxValue;

            return dato;
        }

        public JsonResult ConsultarSensoresEmersonPorIdServicio(string codigo_servicio)
        {
            string BD = codigo_servicio.Substring(0, 1);
            int BD_origen = 0;
            if (BD == "S")
            {
                BD_origen = 1;
            }
            else if (BD == "A")
            {
                BD_origen = 2;
            }

            int servicioId = Int32.Parse(codigo_servicio.Substring(1, codigo_servicio.Length - 1));
            var resultados = Json(null, JsonRequestBehavior.AllowGet);
            List<Clases.Sensor> lista_sensores = ServicioModels.ObtenerSensoresServicio(servicioId, BD_origen); //ConsultarSensoresPorIdServicio(servicioId, BD_origen);
            resultados = Json(lista_sensores, JsonRequestBehavior.AllowGet);
            resultados.MaxJsonLength = Int32.MaxValue;
            return resultados;
        }

        public List<Clases.Sensor> ConsultarSensoresEmersonPorIdServicio2(string codigo_servicio)
        {
            string BD = codigo_servicio.Substring(0, 1);
            int BD_origen = 0;
            if (BD == "S")
            {
                BD_origen = 1;
            }
            else if (BD == "A")
            {
                BD_origen = 2;
            }

            int servicioId = Int32.Parse(codigo_servicio.Substring(1, codigo_servicio.Length - 1));
            var resultados = Json(null, JsonRequestBehavior.AllowGet);
            List<Clases.Sensor> lista_sensores = ServicioModels.ObtenerSensoresServicio(servicioId, BD_origen);
            /*Clases.Servicio servicio = ServicioModels.GetConsultoUnServicio(servicioId, BD_origen);
            if (servicio.NumModem != null || servicio.IdControlador != null) {

                Clases.Sensor sensorControlador = new Clases.Sensor();
                sensorControlador.tipo_sensor = "CO2 Generico";
                sensorControlador.numSerie = servicio.IdControlador;
                if (servicio.NumModem != "") sensorControlador.numSerie = servicio.NumModem;
            }*/

            return lista_sensores;
        }

        public int FinalizarServicioPorId(string idServicio)
        {
            string BD = idServicio.Substring(0, 1);
            int BD_origen = 0;

            if (BD == "S")
            {
                BD_origen = 1;
            }
            else if (BD == "A")
            {
                BD_origen = 2;
            }
            int servicioId = Int32.Parse(idServicio.Substring(1, idServicio.Length - 1));

            int retorno = 0;
            int IdUsuario = Convert.ToInt32(Session["PerfilInterior"]);

            retorno = ServicioModels.FinalizarServicioPorId(servicioId, BD_origen, IdUsuario);

            return retorno;
        }

        public ActionResult MedicionesSinOutlier(string IdServicio)
        {
            List<StackedLineChartData100> medicionesCO2_sin_outLier = new List<StackedLineChartData100>();
            List<StackedLineChartData100> medicionesCO2_sin_outLier_sensores = new List<StackedLineChartData100>();
            List<StackedLineChartData100> medicionesCO2_CO2_del_fin = new List<StackedLineChartData100>();

            ServiceData aperturaPuerta = new ServiceData();
            int servicioId = Int32.Parse(IdServicio.Substring(1, IdServicio.Length - 1));
            string BD = IdServicio.Substring(0, 1);
            int BD_origen = 0;
            int inicial = 0;

            if (BD == "S")
            {
                BD_origen = 1;
            }
            else if (BD == "A")
            {
                BD_origen = 2;
            }

            if (BD_origen == 1) //no solo se utiliza para apertura puerta, tambien para registros puntuales que se necesitaron obtener 
            {
                aperturaPuerta = ServicioModels.DetectarAperturaPuerta(IdServicio);

                DateTime inicioZscore = new DateTime(2020, 08, 27);

                if (aperturaPuerta.FechaControlador2 < inicioZscore)
                { //revisar formato fecha
                    flag_Zscore = false;
                }
            }

            //Contador
            contMuestrasRevisadas = aperturaPuerta.ContadorOutliersBD;

            List<StackedLineChartData100> lista_med_co2 = GetGraficoCO2_2(IdServicio);



            if (flag_Zscore == true)
            {
                if (lista_med_co2.Count > 0)
                {
                    List<double> listaMedicionesCO2 = new List<double>();

                    for (int i = 0; i < lista_med_co2.Count; i++)
                    {
                        if (lista_med_co2[i].id_rel_sensor_servicio == 1)
                        {
                            //FechaControlador2 llamese a valor avocado(+48hrs) o aradano (+34hrs)
                            if (lista_med_co2[i].x < aperturaPuerta.FechaMedicion && (aperturaPuerta.FechaControlador2 == null || aperturaPuerta.FechaControlador2 < lista_med_co2[i].x))
                            {
                                if (inicial == 0) inicial = i;

                                //if (aperturaPuerta.UltServiceDataEdit < lista_med_co2[i].id_serviceData)
                                //{
                                //decimal dec = new decimal(lista_med_co2[i].y);
                                double d = (double)lista_med_co2[i].y;

                                listaMedicionesCO2.Add(d);
                                //}
                            }
                            /*else
                            {
                                medicionesCO2_CO2_del_fin.Add(lista_med_co2[i]);
                            }*/
                        }
                        else
                        {
                            medicionesCO2_sin_outLier_sensores.Add(lista_med_co2[i]);
                        }
                    }

                    if (listaMedicionesCO2.Count > 0)
                    {
                        //medicionesCO2_sin_outLier = medicionesCO2_sin_outLier.OrderByDescending(x => x.x).ToList(); //.Reverse().ToList();
                        StackedLineChartData100 rangoBeneficio = new StackedLineChartData100();

                        //rangoBeneficio = GetRangoBeneficio(IdServicio);
                        //double minRB = rangoBeneficio.low;
                        //double maxRB = rangoBeneficio.high;
                        //inicial = 0;

                        ZscoreOut objeto_outlier = StatisticalOutLierAnalysis(listaMedicionesCO2, 1.2, inicial);
                        List<float> outLierCO2 = objeto_outlier.outliers;
                        List<int> posicionOutlierCO2 = objeto_outlier.posiciones;

                        medicionesCO2_sin_outLier = modificarOutLier(lista_med_co2, outLierCO2, aperturaPuerta.IdCommodity, servicioId, aperturaPuerta.UltServiceDataEdit, posicionOutlierCO2, aperturaPuerta.SetpointCO2);

                        while (medicionesCO2_sin_outLier == null || medicionesCO2_sin_outLier.Count == 0)
                        {
                            inicial = 0;
                            listaMedicionesCO2 = new List<double>();
                            medicionesCO2_sin_outLier = GetGraficoCO2_2(IdServicio);

                            for (int i = 0; i < medicionesCO2_sin_outLier.Count; i++)
                            {
                                if (medicionesCO2_sin_outLier[i].id_rel_sensor_servicio == 1)
                                {
                                    if (medicionesCO2_sin_outLier[i].x < aperturaPuerta.FechaMedicion && (aperturaPuerta.FechaControlador2 == null || aperturaPuerta.FechaControlador2 < medicionesCO2_sin_outLier[i].x))
                                    {
                                        //guardar i y sumarlo a las posiciones ;)
                                        if (inicial == 0) inicial = i;

                                        //decimal dec = new decimal(medicionesCO2_sin_outLier[i].y);
                                        double d = (double)medicionesCO2_sin_outLier[i].y;

                                        listaMedicionesCO2.Add(d);
                                    }
                                }
                            }

                            objeto_outlier = StatisticalOutLierAnalysis(listaMedicionesCO2, 1.2, inicial);
                            outLierCO2 = objeto_outlier.outliers;
                            posicionOutlierCO2 = objeto_outlier.posiciones;

                            medicionesCO2_sin_outLier = modificarOutLier(medicionesCO2_sin_outLier, outLierCO2, aperturaPuerta.IdCommodity, servicioId, ultimoServiceData, posicionOutlierCO2, aperturaPuerta.SetpointCO2);
                        }

                        //medicionesCO2_sin_outLier = quitarOutLier(lista_med_co2, outLierCO2);
                    }
                }
                return Json(medicionesCO2_sin_outLier_sensores.Concat(medicionesCO2_sin_outLier));
            }
            else
            {
                return Json(lista_med_co2);
            }
        }

        public static ZscoreOut /*List<float>*/ StatisticalOutLierAnalysis(List<double> allNumbers, double constante, int inicial)
        {
            List<float> normalNumbers = new List<float>();
            List<float> outLierNumbers = new List<float>();
            List<int> outLierPosicion = new List<int>();

            ZscoreOut OutliersObj = new ZscoreOut();

            double[] decimalNumbers1 = new double[allNumbers.Count];
            double[] decimalNumbers2;
            // double avg = allNumbers.Average();
            //0.75 cuartil de la distribucion normal.
            int i = 0, j = 0, contador_cero = 0, largo_arreglo = 0;
            foreach (var elem in allNumbers)
            {
                if (elem != 0)
                {
                    decimalNumbers1[i] = elem;
                    i++;
                }
                else
                {
                    contador_cero++;
                }
            }

            decimalNumbers2 = decimalNumbers1;

            if (contador_cero != 0)
            {
                largo_arreglo = decimalNumbers2.Length;
                System.Array.Resize(ref decimalNumbers2, largo_arreglo - contador_cero);
            }

            double avg = decimalNumbers2.Average(); //revisar promedio posiciones vacias
            double standardDeviation = Math.Sqrt(allNumbers.Average(v => Math.Pow(v - avg, 2)));

            var di = new List<int>();  //List where all di will be stored
            var median = Median(decimalNumbers2);  //See the link how to write it
                                                   //foreach (var elem in allNumbers)
                                                   //{
                                                   //    di.Add(Math.Abs(elem - median));
                                                   //}

            foreach (double number in allNumbers)
            {
                if (number != 0)
                {

                    float vOut = Convert.ToSingle(number);

                    if ((Math.Abs(number - avg)) > (constante * standardDeviation))
                    {
                        outLierNumbers.Add(vOut); //AQUI
                        outLierPosicion.Add(j + inicial); //AQUI

                    }
                    else
                        normalNumbers.Add(vOut);
                }
                j++;
            }

            OutliersObj.outliers = outLierNumbers;
            OutliersObj.posiciones = outLierPosicion;

            return OutliersObj;

        }

        public static double Median(double[] xs)
        {
            Array.Sort(xs);
            return xs[xs.Length / 2];
        }

        /*public static List<StackedLineChartData100> quitarOutLier(List<StackedLineChartData100> lista_med_co2, List<float> outLierCO2)
        {

            List<StackedLineChartData100> medicionesCO2_sin_outLier = new List<StackedLineChartData100>();

            int j = 0;
            DateTime? primerFechaCiclo = null, ultimaFechaCiclo = null;
            for (int i = 0; i < lista_med_co2.Count; i++)
            {

                if (j < outLierCO2.Count && lista_med_co2[i].y == outLierCO2[j])//.TrimStart(outLierCO2[i]);
                {
                    //aqui la logica 

                    j++;
                }
                else
                {
                    primerFechaCiclo = lista_med_co2[i].x;

                    medicionesCO2_sin_outLier.Add(lista_med_co2[i]);
                    ultimaFechaCiclo = lista_med_co2[i].x;
                }
            }

            return medicionesCO2_sin_outLier;
        }*/


        public static List<StackedLineChartData100> modificarOutLier(List<StackedLineChartData100> lista_med_co2, List<float> outLierCO2, int idCommodity, int idServicio, DateTime? UltServiceDataEdit, List<int> posicionOutlierCO2, int setpointCO2)
        {

            List<StackedLineChartData100> medicionesCO2_sin_outLier = new List<StackedLineChartData100>();
            List<StackedLineChartData100> medicionesCO2_modificadas = new List<StackedLineChartData100>();

            int j = 0, contadorCiclo = 0, posicion = 0, posicionInicial = 0, contMuestrasModificadas = 0, /*contMuestrasRevisadas = 0,*/ flagJ = 0;
            int retorno = 0;
            Boolean flagOutlier = true, flag_sobre_RB = false, flag_anterior = false, aprobarModificacion = false;
            /*DateTime? primerMuestra = null, ultimaMuestra = null;*/
            double? /*primerMuestraCO2 = 0, ultimaMuestraCO2 = 0,*/ promedio = 0, pctMuestrasModificadas = 0, pctMuestrasRevisadas = 0;
            List<int> posicionesOutliers = new List<int>();
            List<int> posicionesSinOutliers = new List<int>();
            TimeSpan? intervalo;
            TimeSpan intervaloNotNull;
            int rangoTotalMinutos = 0;

            for (int i = 0; i < lista_med_co2.Count; i++)
            {
                if (UltServiceDataEdit < lista_med_co2[i].x)
                {
                    flagJ = 1;

                    pctMuestrasModificadas = (contMuestrasModificadas * 100) / lista_med_co2.Count;
                    pctMuestrasRevisadas = (contMuestrasRevisadas * 100) / lista_med_co2.Count;
                    if (pctMuestrasRevisadas >= 25) //j == outLierCO2.Count
                    {
                        break;
                    }
                    else if (lista_med_co2[i].y == 0 || (j < outLierCO2.Count && i == posicionOutlierCO2[j]))//(j < outLierCO2.Count && lista_med_co2[i].y == outLierCO2[j]) //SI ES OUTLIER
                    {

                        //if (lista_med_co2[i].y < minRB || maxRB < lista_med_co2[i].y || lista_med_co2[i].y == 0) //Y ESTA FUERA DEL RB o el valor medido es cero
                        //{
                        contadorCiclo++; //cantidad de outlier por ciclo
                        posicionesOutliers.Add(i);
                        //flagOutlier = true;
                        ultimaMuestra = null;
                        ultimaMuestraCO2 = 0;

                        //}
                        if (lista_med_co2[i].y != 0)
                        {
                            j++;
                        }

                        contMuestrasRevisadas++;
                    }
                    else if (j == outLierCO2.Count || i != posicionOutlierCO2[j]) // lista_med_co2[i].y != outLierCO2[j])//(minRB < lista_med_co2[i].y && lista_med_co2[i].y < maxRB)//SI NO ES OUTLIER Y ESTA DENTRO DEL RB //lista_med_co2[i].y != outLierCO2[j]
                    {
                        if (contadorCiclo > 0 && primerMuestra != null) //ya hay muestra inicial y outlier intermedio //flagOutlier == true
                        {
                            ultimaMuestra = lista_med_co2[i].x;
                            ultimaMuestraCO2 = lista_med_co2[i].y;
                            ultimoServiceData = ultimaMuestra;

                            promedio = (ultimaMuestraCO2 + primerMuestraCO2) / 2;

                            //Reemplazo outlier muestras entre primerMuestra y ultimaMuestra por el promedio.
                            posicionInicial = posicionesOutliers[0];
                            intervalo = (ultimaMuestra - primerMuestra);

                            //de quien mas cerca se encuentre (Rango beneficio) asumo que esta abajo o arriba
                            //double RangoBeneficioMax = Math.Abs( lista_med_co2[posicionInicial].y - maxRB );
                            //double RangoBeneficioMin = Math.Abs( lista_med_co2[posicionInicial].y - minRB );



                            if (intervalo.HasValue)
                            {
                                intervaloNotNull = intervalo.Value;

                                if (idCommodity == 40 || idCommodity == 15 || idCommodity == 39 || idCommodity == 13 || idCommodity == 32 || idCommodity == 11)
                                {
                                    //if (lista_med_co2[posicionInicial].y < minRB)
                                    if (lista_med_co2[posicionInicial].y < setpointCO2)
                                    {
                                        //if (intervaloNotNull.Days == 0 && intervaloNotNull.Hours < 20)
                                        if (intervaloNotNull.Days == 0 && intervaloNotNull.Hours < 10)
                                        {
                                            aprobarModificacion = true;
                                            rangoTotalMinutos = intervaloNotNull.Days * 24 * 60 + intervaloNotNull.Hours * 60 + intervaloNotNull.Minutes;
                                        }
                                        else
                                        {
                                            aprobarModificacion = false;
                                        }
                                    }

                                    //if (lista_med_co2[posicionInicial].y > maxRB)
                                    else if (lista_med_co2[posicionInicial].y > setpointCO2)
                                    {
                                        //if ((intervaloNotNull.Days == 1 && intervaloNotNull.Hours <= 8) || (intervaloNotNull.Days == 0)) //32
                                        if ((intervaloNotNull.Days == 0 && intervaloNotNull.Hours <= 16) || (intervaloNotNull.Days == 0)) //16
                                        {
                                            aprobarModificacion = true;
                                            rangoTotalMinutos = intervaloNotNull.Days * 24 * 60 + intervaloNotNull.Hours * 60 + intervaloNotNull.Minutes;

                                        }
                                        else
                                        {
                                            aprobarModificacion = false;
                                        }
                                    }
                                }
                                else
                                {
                                    //if (lista_med_co2[posicionInicial].y < minRB)
                                    if (lista_med_co2[posicionInicial].y < setpointCO2)
                                    {
                                        //if (intervaloNotNull.Days == 0 && intervaloNotNull.Hours <= 5)
                                        if ((intervaloNotNull.Days == 0 && intervaloNotNull.Hours == 2 && intervaloNotNull.Minutes <= 30)
                                             || (intervaloNotNull.Days == 0 && intervaloNotNull.Hours < 2))
                                        {
                                            aprobarModificacion = true;
                                            rangoTotalMinutos = intervaloNotNull.Days * 24 * 60 + intervaloNotNull.Hours * 60 + intervaloNotNull.Minutes;
                                        }
                                        else
                                        {
                                            aprobarModificacion = false;
                                        }
                                    }

                                    //if (lista_med_co2[posicionInicial].y > maxRB)
                                    else if (lista_med_co2[posicionInicial].y > setpointCO2)
                                    {
                                        //if ((intervaloNotNull.Days == 3 && intervaloNotNull.Hours <= 5) || (intervaloNotNull.Days <= 2)) //77 
                                        if ((intervaloNotNull.Days == 1 && intervaloNotNull.Hours <= 14) || (intervaloNotNull.Days < 1)) //38
                                        {
                                            aprobarModificacion = true;
                                            rangoTotalMinutos = intervaloNotNull.Days * 24 * 60 + intervaloNotNull.Hours * 60 + intervaloNotNull.Minutes;
                                        }
                                        else
                                        {
                                            aprobarModificacion = false;
                                        }
                                    }
                                }
                            }

                            if (aprobarModificacion == true)
                            {
                                /* ultimaMuestra = lista_med_co2[i].x;
                                    ultimaMuestraCO2 = lista_med_co2[i].y;
                                    ultimoServiceData = ultimaMuestra;

                                    promedio = (ultimaMuestraCO2 + primerMuestraCO2) / 2;
                                    }}
                                */ //PARA OBSERVAR


                                //convertir intervalo = valor absoluto (ultimaMuestra - primerMuestra); desde dias a minutos

                                //TimeSpan? intervalo = (ultimaMuestra - primerMuestra);


                                double? m = (ultimaMuestraCO2 - primerMuestraCO2) / (rangoTotalMinutos - 0); //CALCULO LA PENDIENTE, m

                                int rangoTramo = 0;
                                double? Y = 0;
                                TimeSpan? intervaloTramo;
                                TimeSpan intervaloTramoNotNull;

                                for (int cont = 0; cont < contadorCiclo; cont++)
                                {
                                    posicion = posicionesOutliers[cont];

                                    intervaloTramo = lista_med_co2[posicion].x - primerMuestra;

                                    if (intervaloTramo.HasValue)
                                    {
                                        intervaloTramoNotNull = intervaloTramo.Value;

                                        rangoTramo = intervaloTramoNotNull.Days * 24 * 60 + intervaloTramoNotNull.Hours * 60 + intervaloTramoNotNull.Minutes;
                                    }

                                    Y = m * rangoTramo + primerMuestraCO2; //ECUACION DE LA RECTA, para obtener mi Y referente al valor del eje X

                                    lista_med_co2[posicion].y = Y;// ya no utilizamos promedio;

                                    medicionesCO2_modificadas.Add(lista_med_co2[posicion]);


                                    pctMuestrasRevisadas = ((contMuestrasRevisadas - (contadorCiclo - cont)) * 100) / lista_med_co2.Count;
                                    if (pctMuestrasRevisadas >= 25) break;
                                }
                                aprobarModificacion = false;
                                ServicioModels.ServiceDataActualizado(medicionesCO2_modificadas, idServicio);
                                retorno = 1;
                            }



                            contadorCiclo = 0;
                            posicionesOutliers = null;
                            posicionesOutliers = new List<int>();
                        }

                        /*posicionesSinOutliers = null;
                        posicionesSinOutliers = new List<int>();*/

                        flagOutlier = false;
                        primerMuestra = lista_med_co2[i].x; //fecha actual
                        primerMuestraCO2 = lista_med_co2[i].y;
                        ultimaMuestra = null;

                        if (retorno == 1)
                            return null;
                    }


                    /*if (contadorCiclo > 0 && primerMuestra != null && ultimaMuestra == null && pctMuestrasRevisadas < 25)
                    {

                        for (int cont = 0; cont < contadorCiclo; cont++)
                        {
                            posicion = posicionesOutliers[cont];
                            lista_med_co2.RemoveAt(posicion);
                            pctMuestrasRevisadas = ((contMuestrasRevisadas - (contadorCiclo - cont)) * 100) / lista_med_co2.Count;
                            if (pctMuestrasRevisadas >= 25) break;
                        }
                    }*/
                }
                else if (j < posicionOutlierCO2.Count && i == posicionOutlierCO2[j] && flagJ == 0)
                {
                    j++;
                }

                else if (UltServiceDataEdit == lista_med_co2[i].x && flagJ == 0)
                {
                    primerMuestra = lista_med_co2[i].x;
                    primerMuestraCO2 = lista_med_co2[i].y;
                }
            }

            //Inserto nuevo serviceData
            //ServicioModels.ServiceDataActualizado(lista_med_co2, idServicio);
            /*primerMuestra == null;
            primerMuestraCO2*/

            return lista_med_co2;
        }

        public bool permisoDetalleContenedor(int usuario_id, string servicio)
        {
            string BD = servicio.Substring(0, 1);
            int BD_origen = 0;

            if (BD == "S")
            {
                BD_origen = 1;
            }
            else if (BD == "A")
            {
                BD_origen = 2;
            }

            int servicioId = Int32.Parse(servicio.Substring(1, servicio.Length - 1));

            bool respuesta = ServicioModels.permisoDetalleContenedor(usuario_id, BD_origen, servicioId);

            return respuesta;
        }

        public List<Mapa> procesoEliminacionUbi(List<Mapa> mapa_original)
        {
            List<Mapa> mapa_retorno = new List<Mapa>();

            string num_sensor_anterior = "";
            int contador_total_mediciones = 0;
            List<Mapa> mapa_sensor = new List<Mapa>();
            List<Mapa> mapa_editado = new List<Mapa>();

            // se le agrega una última medición falsa, con el fin de que se ejecute 
            // la logica de eliminación para el último sensor
            Mapa map = new Mapa();
            map.NumSensor = "XXXX";
            mapa_original.Add(map);

            foreach (var item in mapa_original)
            {
                contador_total_mediciones++;

                // Se ejecuta eliminacion de ubicaciones si se produce un cambio de sensor
                if ((item.NumSensor == null || item.NumSensor != num_sensor_anterior) && contador_total_mediciones > 1)
                {
                    mapa_editado = procesoEliminacionUbicaciones(mapa_sensor);

                    foreach (var item2 in mapa_editado)
                    {
                        mapa_retorno.Add(item2);
                    }

                    mapa_sensor = new List<Mapa>();
                }

                mapa_sensor.Add(item);


                num_sensor_anterior = item.NumSensor;
            }

            return mapa_retorno;
        }

        public List<Mapa> procesoEliminacionUbicaciones(List<Mapa> mapa_original)
        {
            List<Mapa> mapa_retorno = new List<Mapa>();
            int contador_mediciones = 0;

            foreach (var item in mapa_original)
            {
                contador_mediciones++;

                // si primera medición posee ubicación errada se oculta
                if (contador_mediciones == mapa_original.Count())
                {
                    bool ubicacion_errada = false;

                    float latitud = float.Parse(item.Latitud.Replace('.', ','));
                    float longitud = float.Parse(item.Longitud.Replace('.', ','));

                    // si logitud esta en rango de santiago - laboratorio liventus
                    if (latitud > -33.7 && latitud < -33.2)
                    {
                        if (longitud > -72.0 && longitud < -70.0)
                        {
                            ubicacion_errada = true;
                        }
                    }
                    // si logitud esta en rango de bodega en valparaiso phillipi
                    else if (latitud > -33.1 && latitud < -33.0)
                    {
                        if (longitud > -71.6 && longitud < -70.5)
                        {
                            ubicacion_errada = true;
                        }
                    }

                    if (ubicacion_errada == false)
                    {
                        mapa_retorno.Add(item);
                    }
                }
                else
                {
                    mapa_retorno.Add(item);
                }
            }

            return mapa_retorno;
        }

    }
}