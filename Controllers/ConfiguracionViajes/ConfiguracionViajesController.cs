using System;

using System.Collections.Generic;

using System.ComponentModel.DataAnnotations;

using System.Linq;

using System.Web;

using System.Web.Mvc;

using ProyectoAlmaInicio.Models;

using static ProyectoAlmaInicio.Controllers.SidebarController;



namespace ProyectoAlmaInicio.Controllers.ConfiguracionViajes

{

    public class ConfiguracionViajesController : Controller

    {

        // GET: ConfiguracionViajes

        public ActionResult ConfiguracionView()

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





            List<Clases.Objeto> condiciones = ConfiguracionViajesModels.obtenerCondicionesFinViajes();





            HttpCookie cookie = Request.Cookies.Get("Lenguaje");

            if (cookie.Value == "en")

            {

                condiciones[0].Name = "FULFILLMENT ETA POD + 5 DAYS";

                condiciones[1].Name = "DOOR OPENING DETECTION";

            }



            ViewBag.condiciones_finalizacion_viajes = condiciones;





            List<Clases.PerfilNotificacion> listaNotificaciones = obtenerPerfilesNotificacionUser();

            ViewBag.listaNotificaciones = listaNotificaciones;



            List<Clases.Objeto> lista_commodities = MantenedorModels.GetCommoditiesMantenedor();

            ViewBag.lista_commodities = lista_commodities;



            //List<Clases.Objeto> lista_setpoint_co2 = MantenedorModels.GetSetpointCO2(); //GetSetpointsCO2ServicioNuevo();

            //ViewBag.lista_setpoint_co2 = lista_setpoint_co2;



            List<Clases.Objeto> lista_frecuencias = obtenerParametrosFrecuenciaEnvio();

            ViewBag.lista_frecuencias = lista_frecuencias;



            List<Clases.Contacto> lista_contactos = new List<Clases.Contacto>();

            ViewBag.lista_contactos = lista_contactos;





            return View();

        }



        public int agregarConfiguracionFinViaje(int condicion1, string tipo_condicion, int condicion2)

        {

            int IdUsuario = Convert.ToInt32(Session["PerfilInterior"]);

            int respuesta = ConfiguracionViajesModels.agregarConfiguracionFinViaje(IdUsuario, condicion1, tipo_condicion, condicion2);

            return respuesta;

        }



        public JsonResult verConfiguracionFinViaje()

        {

            int IdUsuario = Convert.ToInt32(Session["PerfilInterior"]);

            List<Clases.ConfiguracionFinalizacionViajes> respuesta = ConfiguracionViajesModels.verConfiguracionFinViaje(IdUsuario);

            return Json(respuesta, JsonRequestBehavior.AllowGet);

        }



        public int editarConfiguracionFinViaje(int condicion1, string tipo_condicion, int condicion2)

        {

            int IdUsuario = Convert.ToInt32(Session["PerfilInterior"]);

            int respuesta = ConfiguracionViajesModels.editarConfiguracionFinViaje(IdUsuario, condicion1, tipo_condicion, condicion2);

            return respuesta;

        }





        public List<Clases.PerfilNotificacion> obtenerPerfilesNotificacionUser()

        {

            int IdUsuario = Convert.ToInt32(Session["PerfilInterior"]);

            List<Clases.PerfilNotificacion> listaNotificaciones = ConfiguracionViajesModels.obtenerPerfilesNotificacionUser(IdUsuario);



            return listaNotificaciones;

        }



        public List<Clases.Objeto> obtenerParametrosFrecuenciaEnvio()

        {

            List<Clases.Objeto> lista_frecuencias = ConfiguracionViajesModels.obtenerParametrosFrecuenciaEnvio();



            return lista_frecuencias;

        }



        public class TextBoxModal

        {

            [Required(ErrorMessage = "Value is required")]

            //Para agregar servicio

            public int idPerfilNotificacion { get; set; }

            //public string nombrePerfil { get; set; }

            public string commodity { get; set; }

            //public string setpoint { get; set; }

            public string activo { get; set; }

            public Boolean activoTrue { get; set; }

            public float variacion_sup_temp { get; set; }

            public float variacion_inf_temp { get; set; }

            public float variacion_sup_temp_editar { get; set; }

            public float variacion_inf_temp_editar { get; set; }



            /*ublic string limite_sup_co2 { get; set; }

            public string limite_inf_co2 { get; set; }

            public string limite_sup_temp { get; set; }

            public string limite_inf_temp { get; set; }*/



            public List<Clases.Contacto> lista_contactos { get; set; }



            public string nombre_contacto { get; set; }

            public string empresa_contacto { get; set; }

            public string email_contacto { get; set; }

            public string cargo_contacto { get; set; }

        }





        /*public void CrearPerfilNotificacion()

        {

            int IdUsuario = Convert.ToInt32(Session["PerfilInterior"]);

 

            Clases.DataFiltros data = new Clases.DataFiltros();

            //data = GetPerfilDataByUser();

 

            Clases.PerfilNotificacion perfilNotificacion = new Clases.PerfilNotificacion();

            //ServicioNuevo servicio_respuesta = new ServicioNuevo();

            ViewBag.desplegarMensaje = 0;

 

            // SOLICITUD DE GUARDAR SERVICIO (POST)

            if (Request.HttpMethod.ToString() == "POST")

            {

               

                perfilNotificacion.idCommodity = Convert.ToInt32(Request["commodity_agregar"]);

 

                perfilNotificacion.idFrecuencia = Convert.ToInt32(Request["frecuencia_agregar"]);

                perfilNotificacion.variacion_sup_temp = Convert.ToInt32(Request["variacion_sup_temp"]);

                perfilNotificacion.variacion_inf_temp = Convert.ToInt32(Request["variacion_inf_temp"]);

 

                int IdPerfilNotificacion = ConfiguracionViajesModels.InsertarNotificacionUsuario(IdUsuario, perfilNotificacion);

 

            }

 

 

        }*/



        public int IngresoOK()
        {

            return 5;

        }





        public int EliminarPerfilNotificacion(int idPerfilNotificacion)

        {

            int msje = ConfiguracionViajesModels.eliminarPerfilNotificacion(idPerfilNotificacion);



            return msje;

        }



        public JsonResult ObtenerDatosNotificacion(int idPerfilNotificacion)

        {

            ViewBag.lista_contactos_editar = "";

            //int IdPerfilNotificacion_int = Convert.ToInt32(IdPerfilNotificacion);

            int IdUsuario = Convert.ToInt32(Session["PerfilInterior"]);



            Clases.PerfilNotificacion datosNotificacion = ConfiguracionViajesModels.ObtenerDatosNotificacion(idPerfilNotificacion);



            List<Clases.Contacto> contactos = ConfiguracionViajesModels.ObtenerContactosNotificacion(idPerfilNotificacion);



            datosNotificacion.contactos = contactos;

            ViewBag.lista_contactos_editar = contactos;

            ViewBag.idPerfilNotificacion = idPerfilNotificacion;



            var dato = Json(datosNotificacion, JsonRequestBehavior.AllowGet);

            dato.MaxJsonLength = Int32.MaxValue;

            return dato;

        }





        public void CargaContactoNotificacion(int idPerfilNotificacion, string cargo, string empresa, string nombre, string email)

        {

            Clases.Contacto contacto = new Clases.Contacto();



            contacto.cargo_contacto = cargo;

            contacto.empresa_contacto = empresa;

            contacto.nombre_contacto = nombre;

            contacto.email_contacto = email;



            int validar = ConfiguracionViajesModels.AgregarContactoNotificacion(contacto, idPerfilNotificacion);



            //1 = ok , 0 nok



        }



        public int CargaDatosNotificacion(string commodity_agregar, string frecuencia_agregar, string variacion_sup_temp, string variacion_inf_temp)
        {



            int IdUsuario = Convert.ToInt32(Session["PerfilInterior"]);

            //Clases.ReturnIngreso retorno = new Clases.ReturnIngreso();



            int idPerfilNotificacion = 0;



            Clases.DataFiltros data = new Clases.DataFiltros();

            //data = GetPerfilDataByUser();



            Clases.PerfilNotificacion perfilNotificacion = new Clases.PerfilNotificacion();

            //ServicioNuevo servicio_respuesta = new ServicioNuevo();

            ViewBag.desplegarMensaje = 0;



            variacion_sup_temp = variacion_sup_temp.Replace('.', ',');

            variacion_inf_temp = variacion_inf_temp.Replace('.', ',');



            perfilNotificacion.commodity = commodity_agregar;

            perfilNotificacion.frecuencia = frecuencia_agregar;

            perfilNotificacion.variacion_sup_temp = float.Parse(variacion_sup_temp);

            perfilNotificacion.variacion_inf_temp = float.Parse(variacion_inf_temp);



            idPerfilNotificacion = ConfiguracionViajesModels.InsertarNotificacionUsuario(IdUsuario, perfilNotificacion);



            return idPerfilNotificacion;



        }



        public int EditarDatosNotificacion(int id_perfil_notificacion, string commodity_editar, string frecuencia_editar, string variacion_sup_temp_editar, string variacion_inf_temp_editar)

        {

            int IdUsuario = Convert.ToInt32(Session["PerfilInterior"]);

            int validar = 0;



            Clases.DataFiltros data = new Clases.DataFiltros();

            //data = GetPerfilDataByUser();



            Clases.PerfilNotificacion perfilNotificacion = new Clases.PerfilNotificacion();

            //ServicioNuevo servicio_respuesta = new ServicioNuevo();

            ViewBag.desplegarMensaje = 0;



            //variacion_sup_temp_editar = variacion_sup_temp_editar.Replace('.', ',');

            //variacion_inf_temp_editar = variacion_inf_temp_editar.Replace('.', ',');



            perfilNotificacion.commodity = commodity_editar;

            perfilNotificacion.frecuencia = frecuencia_editar;

            perfilNotificacion.variacion_sup_temp = float.Parse(variacion_sup_temp_editar);

            perfilNotificacion.variacion_inf_temp = float.Parse(variacion_inf_temp_editar);



            validar = ConfiguracionViajesModels.EditarNotificacionUsuario(IdUsuario, perfilNotificacion, id_perfil_notificacion);



            return validar;



        }







        public int ActualizarEstadoNotificacion(int idPerfilNotificacion, Boolean estadoNotificacion)
        {

            int validar = 0;



            validar = ConfiguracionViajesModels.ActualizarEstadoPerfilNotificacion(idPerfilNotificacion, estadoNotificacion);



            return validar;

        }





    }

}