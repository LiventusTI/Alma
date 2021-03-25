using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace ProyectoAlmaInicio.Models
{
    public class Clases
    {
        public class Perfil
        {
            public int Id { get; set; }
            public string Nombre { get; set; }
            public string Descripcion { get; set; }
            public int Activo { get; set; }
        }

        public class CambioPass
        {
            public string pass { get; set; }
            public string passConfirm { get; set; }
            public string nombre { get; set; }
            public string user { get; set; }
        }

        public class Usuario
        {
            public string NombreUsuario { get; set; }
            public int IdTipoUsuario { get; set; }
            public string TipoUsuario { get; set; }
            public string Contrasena { get; set; }
            public string Nombre { get; set; }
            public string Apellido { get; set; }
            public int IdPerfil { get; set; }
            public int IdPerfilData { get; set; }
            public int IdPerfilSeccion { get; set; }
            public string Correo { get; set; }
            public int IdServiceProvider { get; set; }
            public int IdSesion { get; set; }
            public int IdCliente { get; set; }

            //Visualizacion de secciones
            public List<Seccion> SeccionesOcultar { get; set; }
            public List<Data> DataParaQuitar { get; set; }

            //FILTRO DATA
            public int quitarDataModem { get; set; }
            public int quitarDataControlador { get; set; }
            public int quitarDataSensores { get; set; }


            //public int MenuOngoing { get; set; }
            //public int MenuHistorico { get; set; }
            //public int MapaOngoing { get; set; }
            //public int GraficoCO2 { get; set; }
            //public int MapaDetalleServicio { get; set; }
            //public int GraficoExternoTemp { get; set; }
            //public int GraficoExternoCO2 { get; set; }

        }

        public class Seccion
        {
            public int IdSeccion { get; set; }
            public string NombreSeccion { get; set; }
            public string ClaseSeccion { get; set; }
            public int Estado { get; set; }
        }

        public class Data
        {
            public int IdTipoData { get; set; }
            public string NombreTipoData { get; set; }
        }


        public class Documento
        {
            public int IdDocumento { get; set; }
            public string NombreDocumento { get; set; }
            public string TipoDocumento { get; set; }
            public string Tipo { get; set; }
            public string Path { get; set; }
            public string TipoIcono { get; set; }
            public string Destinatario { get; set; }
            public string Commodity { get; set; }
            public string TipoRevision { get; set; }
            public string LugarRevision { get; set; }
            public DateTime? FechaRevision { get; set; }
            public string Empresa { get; set; }
            public string Contenedores { get; set; }
            public string Notas { get; set; }
        }

        public class ListadoDocumentos
        {
            public List<Documento> Documentos { get; set; }
            public string Path { get; set; }
        }

        public class Validar
        {
            public string Mensaje { get; set; }

            public Int32 validador { get; set; }

        }

        public class Servicio
        {
            public int IdCliente { get; set; }

            public int BDOrigen { get; set; }

            public DateTime? FechaControlador { get; set; }
            public int IdServicio { get; set; }
            public int IdReserva { get; set; }

            public string Exportador { get; set; }
            public string FreightForWarder { get; set; }
            public string Contenedor { get; set; }
            public string Booking { get; set; }
            public string Commodity { get; set; }
            public string Naviera { get; set; }
            public string Nave { get; set; }
            public string PuertoOrigen { get; set; }
            public string PuertoDestino { get; set; }
            public DateTime? EtaPuerto { get; set; }
            public string SetpointAC { get; set; }
            public string SetpointT { get; set; }
            public string Modem { get; set; }
            public string CO2Ext { get; set; }
            public string TemperaturaExt { get; set; }
            public string HumedadExt { get; set; }
            public string AperturaPuerta { get; set; }
            public string EstadoServicio { get; set; }

            public string IdControlador { get; set; }
            public string Viaje { get; set; }

            public string NumModem { get; set; }
            public Mapa UbicacionModem { get; set; }

            public List<Mapa> UbicacionHistModem { get; set; }

            public List<Mapa> UbicacionHistEmerson { get; set; }

            //rangos beneficio para graficio
            public float RangoAltoCO2 { get; set; }
            public float RangoBajoCO2 { get; set; }
            public float RangoAltoO2 { get; set; }
            public float RangoBajoO2 { get; set; }

            //etapod + 5
            public DateTime? EtaPuerto5 { get; set; }
            //Emerson
            public string NumSerieSensor { get; set; }
            public DateTime? FechaProgramacion { get; set; }
            public string Origen { get; set; }
            public string Destino { get; set; }
            public string EstadoSensor { get; set; }
            public DateTime? Etd { get; set; }
            public string Descripcion { get; set; }

            public List<Sensor> SensoresServicio { get; set; }
            public string StringSensores { get; set; }

            public int IdCommodityBD { get; set; }
            public int IdContenedorBD { get; set; }

            public int IdLugarOrigenBD { get; set; }
            public int IdLugarDestinoBD { get; set; }

            public int Eliminado { get; set; }

            public string ObtieneDatos { get; set; }

            public int LlevaModem { get; set; }

            public int cantSetpointCO2 { get; set; }
            public DateTime? FechaFinalizacion { get; set; }
            public int IdEstadoServicioAlma { get; set; }

            public string Cliente { get; set; }


        }

        public class MapasInicio
        {
            public List<Mapa> UbicacionHistModem { get; set; }
            public List<Mapa> UbicacionHistEmerson { get; set; }
        }

        public class RangosBeneficio
        {
            public float RangoAltoCO2 { get; set; }
            public float RangoBajoCO2 { get; set; }
            public float RangoAltoO2 { get; set; }
            public float RangoBajoO2 { get; set; }
        }

        public class Mapa
        {
            public string NumSensor { get; set; }
            public int BDOrigen { get; set; }
            public int IdServicio { get; set; }
            public string Longitud { get; set; }
            public string Latitud { get; set; }
            public string IdUbicacion { get; set; }
            public string ContenedorUbicacion { get; set; }
            public DateTime? FechaUbicacion { get; set; }

        }


        public class DataFiltros
        {
            public int ModemNull { get; set; }
            public int ControladorNull { get; set; }
            public int ModemNotNull { get; set; }
            public int ControladorNotNull { get; set; }

        }

        public class Naviera
        {
            public int Id { get; set; }
            public string Nombre { get; set; }
            public int Activo { get; set; }
        }

        public class Nave
        {
            public Int32 IdNave { get; set; }
            public Int32 IdNaviera { get; set; }
            public string NombreNaviera { get; set; }
            public string NombreNave { get; set; }
            public Int32 Estado { get; set; }

        }

        public class Commodity
        {
            public int Id { get; set; }
            public string Nombre { get; set; }
            public int Activo { get; set; }
        }

        public class Contenedor
        {
            public int IdContenedor { get; set; }
            public string NumeroContenedor { get; set; }
            public string NombreMaquinaria { get; set; }
            public string CajaContenedor { get; set; }
            public int AnoContenedor { get; set; }
            public string Pais { get; set; }
            public string Ciudad { get; set; }
            public string Deposito { get; set; }
            public string Naviera { get; set; }
            public int IdReserva { get; set; }
            public DateTime? Fecha { get; set; }
            public string Estado { get; set; }
            public int Scrubber { get; set; }
            public int EstadoContenedor { get; set; }
            public int IdMaquinaria { get; set; }
            public int IdCaja { get; set; }
            public int KitCortina { get; set; }
            public string Controlador { get; set; }
            public string Bateria { get; set; }
            public DateTime? FechaBateria { get; set; }
            public int DiasBateria { get; set; }

        }

        public class PuertoOrigen
        {
            public int Id { get; set; }
            public int IdCiudad { get; set; }
            public string Nombre { get; set; }
            public int Activo { get; set; }
            public string NombreCiudad { get; set; }
            public int IdPais { get; set; }
            public string NombrePais { get; set; }
            public int IdContinente { get; set; }
            public string NombreContinente { get; set; }

        }

        public class PuertoDestino
        {
            public int Id { get; set; }
            public int IdCiudad { get; set; }
            public string Nombre { get; set; }
            public int Activo { get; set; }
            public string NombreCiudad { get; set; }
            public int IdPais { get; set; }
            public string NombrePais { get; set; }
            public int IdContinente { get; set; }
            public string NombreContinente { get; set; }

        }

        public class Totalizador
        {
            public int ContadorModem { get; set; }
            public int ContadorControlador { get; set; }
            public int ContadorCO2 { get; set; }
            public int ContadorTemperatura { get; set; }
            public int ContadorLuz { get; set; }
            public int ContadorTotal { get; set; }
        }


        //WEB SERVICE PS03 MARINE TRAFFIC
        public class ExportVesselFull
        {
            public string MMSI { get; set; }
            public string IMO { get; set; }
            public string SHIP_ID { get; set; }
            public string LAT { get; set; }
            public string LON { get; set; }
            public string SPEED { get; set; }
            public string HEADING { get; set; }
            public string COURSE { get; set; }
            public string STATUS { get; set; }
            public DateTime TIMESTAMP { get; set; }
            public string DSRC { get; set; }
            public string UTC_SECONDS { get; set; }
            public string SHIPNAME { get; set; }
            public string SHIPTYPE { get; set; }
            public string CALLSIGN { get; set; }
            public string FLAG { get; set; }
            public string LENGTH { get; set; }
            public string WIDTH { get; set; }
            public string GRT { get; set; }
            public string DWT { get; set; }
            public string DRAUGHT { get; set; }
            public string YEAR_BUILT { get; set; }
            public string ROT { get; set; }
            public string TYPE_NAME { get; set; }
            public string AIS_TYPE_SUMMARY { get; set; }
            public string DESTINATION { get; set; }
            public DateTime ETA { get; set; }
            public string CURRENT_PORT { get; set; }
            public string LAST_PORT { get; set; }
            public DateTime LAST_PORT_TIME { get; set; }
            public string CURRENT_PORT_ID { get; set; }
            public string CURRENT_PORT_UNLOCODE { get; set; }
            public string CURRENT_PORT_COUNTRY { get; set; }
            public string LAST_PORT_ID { get; set; }
            public string LAST_PORT_UNLOCODE { get; set; }
            public string LAST_PORT_COUNTRY { get; set; }
            public string NEXT_PORT_ID { get; set; }
            public string NEXT_PORT_UNLOCODE { get; set; }
            public string NEXT_PORT_NAME { get; set; }
            public string NEXT_PORT_COUNTRY { get; set; }
            public DateTime ETA_CALC { get; set; }
            public DateTime ETA_UPDATED { get; set; }
            public string DISTANCE_TO_GO { get; set; }
            public string DISTANCE_TRAVELLED { get; set; }
            public string AVG_SPEED { get; set; }
            public string MAX_SPEED { get; set; }
        }

        //SENSORES EMERSON

        public class Mediciones
        {
            public int ID_REL_SENSORSERVICIO { get; set; }
            public DateTime? FechaMedicion { get; set; }
            public float ValorMedido { get; set; }
            public int AperturoPuertas { get; set; }
            public int ID_SERVICEDATA { get; set; }
        }

        public class StackedLineChartData100
        {
            public DateTime? x;
            public Nullable<double> y;
            public double high;
            public double low;
            public int id_rel_sensor_servicio;
            public int id_serviceData;
        }

        public class Objeto
        {
            public string Name { get; set; }
            public string Code { get; set; }
            public string Pic { get; set; }
        }

        public class ServicioNuevo
        {

            public int EsModem { get; set; }

            public int id { get; set; }
            public string usuario { get; set; }
            public string sensores { get; set; }
            public string numero_serie { get; set; }
            public string contenedor { get; set; }
            public int id_origen { get; set; }
            public string lugar_origen { get; set; }
            public string nombre_lugar_origen { get; set; }
            public string pais_origen { get; set; }
            public string region_origen { get; set; }
            public string ciudad_origen { get; set; }
            public string comuna_origen { get; set; }
            public string calle_origen { get; set; }
            public string numero_calle_origen { get; set; }
            public string codigo_postal_origen { get; set; }
            public string latitud_origen { get; set; }
            public string longitud_origen { get; set; }
            public int id_destino { get; set; }
            public string lugar_destino { get; set; }
            public string nombre_lugar_destino { get; set; }
            public string pais_destino { get; set; }
            public string region_destino { get; set; }
            public string ciudad_destino { get; set; }
            public string comuna_destino { get; set; }
            public string calle_destino { get; set; }
            public string numero_calle_destino { get; set; }
            public string codigo_postal_destino { get; set; }
            public string latitud_destino { get; set; }
            public string longitud_destino { get; set; }
            public DateTime? etd { get; set; }
            public DateTime? eta { get; set; }
            public string etd_string { get; set; }
            public string eta_string { get; set; }
            public string booking { get; set; }
            public int id_nave { get; set; }
            public string nave { get; set; }
            public string commodity { get; set; }
            public int id_naviera { get; set; }
            public string naviera { get; set; }
            public float id_setpoint_co2 { get; set; }
            public float id_setpoint_temp { get; set; }
            public string setpoint_co2 { get; set; }
            public string setpoint_temp { get; set; }
            public int id_bd { get; set; }
            public string descripcion { get; set; }

            //Emerson
            public string tipo_sensor1 { get; set; }
            public string tipo_sensor2 { get; set; }
            public string tipo_sensor3 { get; set; }
            public string tipo_sensor4 { get; set; }
            public string tipo_sensor5 { get; set; }
            public string numero_sensor1 { get; set; }
            public string numero_sensor2 { get; set; }
            public string numero_sensor3 { get; set; }
            public string numero_sensor4 { get; set; }
            public string numero_sensor5 { get; set; }

            public string NumModem { get; set; }
            public int id_destinatario { get; set; }

        }



        public class Error
        {
            public string ErrorCode { get; set; }
            public string ErrorDescription { get; set; }
            public string AccessToken { get; set; }
        }


        public class Headers
        {
            public string ContentType { get; set; }
            public string XLTApiKey { get; set; }
            public string Authorization { get; set; }
        }

        public class LoginRequest
        {
            public string UserName { get; set; }//-- obligatorio
            public string Password { get; set; }//-- obligatorio
        }

        public class LoginResponse
        {
            public string ErrorCode { get; set; }
            public string AccessToken { get; set; }
            public string RefreshToken { get; set; }
        }

        public class DeﬁneTripRequest
        {
            public string TripID { get; set; }//-- obligatorio
            public string TripAction { get; set; }
            public string TrackerID { get; set; }//-- obligatorio
            public string LaneDescription { get; set; }
            public string CarrierTcode { get; set; }
            public string CustomerCode { get; set; }
            public string TempLowCritical { get; set; }
            public string TempHighCritical { get; set; }
            public List<StopLocation> Locations { get; set; }//-- obligatorio
            public string RepID { get; set; }
            public string RepFirstName { get; set; }
            public string RepLastName { get; set; }
            public string RepPhone { get; set; }
            public string RepEmail { get; set; }
            public string ShipperName { get; set; }
            public string ReceiverName { get; set; }
            public string CarrierName { get; set; }
            public string CarrierEmails { get; set; }
            public string ShipperEmails { get; set; }
            public string ReceiverEmails { get; set; }
            public string RetailerID { get; set; }
            public string DriverName { get; set; }
            public string DriverPhone { get; set; }
            public string EstimatedFlightMinutes { get; set; }
            public string EstimatedLoadingMinutes { get; set; }
        }

        public class StopLocation
        {
            public string LocationName { get; set; }//-- obligatorio
            public string LocationID { get; set; }//-- obligatorio
            public string Address1 { get; set; }//-- obligatorio
            public string Address2 { get; set; }
            public string City { get; set; }//-- obligatorio
            public string State { get; set; }//-- obligatorio
            public string Zip { get; set; }//-- obligatorio
            public string Country { get; set; }//-- obligatorio
            public string LocationContact { get; set; }
            public string LocationPhoneNumber { get; set; }
            public string LocationType { get; set; }//-- obligatorio
            public string StopDateUTC { get; set; }
            public string StopTypeCode { get; set; }
        }

        public class DeﬁneTripResponse
        {
            public string ErrorCode { get; set; }
            public string ErrorDescription { get; set; }
        }

        public class GetTripStatusRequest
        {
            public string LastTripStatusSequenceID { get; set; }
        }

        public class GetTripStatusResponse
        {
            public int ErrorCode { get; set; }
            public string ErrorDescription { get; set; }
            public List<TripStatus> TripStatusList { get; set; }
        }

        public class TripStatus
        {
            public int TripStatusSequenceID { get; set; }
            public string TrackerID { get; set; }
            public string LaneDescription { get; set; }
            public string Latitude { get; set; }
            public string Longitude { get; set; }
            public string DateTimeAcquiredUTC { get; set; } //revisar
            public string TripID { get; set; }
            public string CarrierTcode { get; set; }
            public string CustomerCode { get; set; }
            public string ShipmentStatusCode { get; set; }
            public string EventCity { get; set; }
            public string EventState { get; set; }
            public string EventZip { get; set; }
            public int BatteryPct { get; set; }
            public int AlertCode { get; set; }
            public long AlertID { get; set; }
            public string AlertDesc { get; set; }
            public double AlertValue { get; set; }
            public double AlertThresholdLowCritical { get; set; }
            public double AlertThresholdHighCritical { get; set; }
            public List<Sensor> Sensors { get; set; }
            public int ReturnCode { get; set; }
            public string ExceptionMessage { get; set; }
        }

        public class Sensor
        {
            public string SensorId { get; set; }
            public float SensorValue { get; set; }
            public int bd { get; set; }
            public string tipo_sensor { get; set; }
            public int id_tipo_sensor { get; set; }
            public string proveedor { get; set; }
            public DateTime? fechaProgramacion { get; set; }
            public string usuarioProgramador { get; set; }
            public string recibioDatos { get; set; }
            public string numSerie { get; set; }
            public int CargadoEnEmerson { get; set; }
            public int Existe { get; set; }
            public int EsModem { get; set; }

        }

        public class Medicion
        {
            public string nombre_medicion { get; set; }
            public int id_tipo_medicion { get; set; }
        }

        public class ConfiguracionFinalizacionViajes
        {
            public int id_condicion { get; set; }
            public int cantidad_ingresada { get; set; }
            public DateTime? fecha_ingresada { get; set; }
            public int grupo { get; set; }
            public string fecha { get; set; }
        }

        public class ServiceData
        {
            public int IdServicio { get; set; }
            public DateTime? FechaMedicion { get; set; }
            public int BateriaPct { get; set; }
            public int Scrubber { get; set; }
            public int Valvula { get; set; }
            public float Temperatura { get; set; }
            public float CO2 { get; set; }
            public float O2 { get; set; }
            public float Humedad { get; set; }
            public int SetpointCO2 { get; set; }
            public DateTime? FechaControlador2 { get; set; }
            public int IdCommodity { get; set; }
            public DateTime? UltServiceDataEdit { get; set; }
            public int ContadorOutliersBD { get; set; }

        }

        public class ZscoreOut
        {
            public List<float> outliers { get; set; }
            public List<int> posiciones { get; set; }
        }

        public class ItemMenu
        {
            public int id_item { get; set; }
            public string valor { get; set; }
            public string ruta { get; set; }
            public string icono { get; set; }
        }

        public class Destinatario
        {
            public int id_servicio { get; set; }
            public int id_destinatario { get; set; }
            public string empresa_destinatario { get; set; }
            public int actividad_empresa { get; set; }
            public List<CommodityEmpresa> lista_commodity_empresa { get; set; }
            public string lista_commodities { get; set; }
            public int pais_contacto { get; set; }
            public int puerto_contacto { get; set; }
            public string nombre_contacto { get; set; }
            public string email_contacto { get; set; }
            public string telefono_contacto { get; set; }
            public int usuario_edita { get; set; }
            public string nombre_pais_contacto { get; set; }
            public string nombre_puerto_contacto { get; set; }
            public string nombre_actividad_empresa { get; set; }
        }

        public class CommodityEmpresa
        {
            public string commodity_empresa { get; set; }
            public DateTime? inicio_temporada { get; set; }
            public DateTime? fin_temporada { get; set; }
        }

        public class PerfilNotificacion
        {
            public int idPerfilNotificacion { get; set; }
            //public string nombrePerfil { get; set; }
            public Boolean activoTrue { get; set; }
            public int idCommodity { get; set; }
            public int idSetpoint { get; set; }
            public int idFrecuencia { get; set; }

            public string commodity { get; set; }
            public string setpoint { get; set; }
            public string activo { get; set; }
            public string frecuencia { get; set; }
            public float variacion_sup_temp { get; set; }
            public float variacion_inf_temp { get; set; }
            public int variacion_temp_editar { get; set; }
            public int limite_sup_co2 { get; set; }
            public int limite_inf_co2 { get; set; }
            public int limite_sup_temp { get; set; }
            public int limite_inf_temp { get; set; }
            public string contactosString { get; set; }

            public List<Contacto> contactos { get; set; }

        }
        public class Contacto
        {

            public int id_contacto { get; set; }
            public string nombre_contacto { get; set; }
            public string empresa_contacto { get; set; }
            public string email_contacto { get; set; }
            public string cargo_contacto { get; set; }
        }

        public class ReturnIngreso
        {

            public int idPerfilNotificacion { get; set; }
            public int validar { get; set; }
        }

    }
}