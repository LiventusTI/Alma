using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using System.Data;
using static ProyectoAlmaInicio.Models.Clases;
using System.Text;
using System.Net;
using System.IO;
using System.Globalization;

namespace ProyectoAlmaInicio.Models
{
    // Puede agregar datos del perfil del usuario agregando más propiedades a la clase ApplicationUser. Para más información, visite http://go.microsoft.com/fwlink/?LinkID=317594.
    public class ServicioModels
    {
        static string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
        static string connectionAPIService = ConfigurationManager.ConnectionStrings["connectionAPIService"].ConnectionString;
        static string AlmaTest = ConfigurationManager.ConnectionStrings["AlmaTest"].ConnectionString;
        static string JorgeTest = ConfigurationManager.ConnectionStrings["JorgeTest"].ConnectionString;
        static string connectionStringTecnica = "Server=68.169.63.233;Port=5306;Uid=liventus_sa;Pwd=L1v3nt9ss4;Database=prometeo;Connect Timeout=30";

        static string apiKeyEmerson = "088B734E-321D-43A5-87BE-6D7C7633576F";//"DA639473-40DE-4216-84AB-9BB513C2AC7A";

        public static List<Clases.Servicio> GetServicios(int IdUsuario, Clases.DataFiltros filtroData)
        {

            List<Clases.Servicio> Servicios = new List<Clases.Servicio>();
            SqlConnection cnn;
            cnn = new SqlConnection(AlmaTest);
            string modemflag = "", co2flag = "", stringsensores = "", obtuvoDatos = "NO";
            DateTime? fecha;
            List<Sensor> sensores = new List<Sensor>();

            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("EXEC dbo.ConsultarServiciosUserPorPerfiles @USUARIO;", cnn);
                command.Parameters.Add("@USUARIO", SqlDbType.Int).Value = IdUsuario;
                command.CommandTimeout = 999;
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    modemflag = ""; co2flag = ""; stringsensores = ""; obtuvoDatos = "NO"; fecha = null; sensores = null;

                    DateTime? etaPuerto = null, fechaControlador = null, etd = null;

                    modemflag = reader["MODEM"].ToString();
                    co2flag = reader["SENSOR_CO2"].ToString();

                    if (modemflag == "SI")
                    {
                        co2flag = "SI";
                    }

                    if (reader["ETAPUERTO"] != DBNull.Value)
                    {
                        etaPuerto = Convert.ToDateTime(reader["ETAPUERTO"]);
                    }

                    if (reader["ETD"] != DBNull.Value)
                    {
                        etd = Convert.ToDateTime(reader["ETD"]);
                    }

                    if (reader["FECHACONTROLADOR"] != DBNull.Value)
                    {
                        fechaControlador = Convert.ToDateTime(reader["FECHACONTROLADOR"]);
                    }

                    /*if ((reader["ELIMINADO"] != DBNull.Value) && (Convert.ToInt32(reader["ELIMINADO"]) != 1))
                    {*/
                        sensores = ObtenerSensoresServicio(Convert.ToInt32(reader["ID_SERVICIO"]), Convert.ToInt32(reader["BD_ORIGEN"]));
                        if (sensores.Count != 0)
                        {
                            stringsensores = GenerarStringSensores(sensores);
                            obtuvoDatos = ServicioObtuvoDatos(sensores);

                            fecha = ConsultarPrimeraFechaSensor(Convert.ToInt32(reader["ID_SERVICIO"]), Convert.ToInt32(reader["BD_ORIGEN"]));
                            if (((fecha != null) && (fecha < fechaControlador)) || (fechaControlador == null)) fechaControlador = fecha;
                        }





                        int bdorigen = 0, idservicio = 0, idcommoditybd = 0, idcontenedorbd = 0, idlugarorigen = 0, idlugardestino = 0, eliminado = 0;

                        if (reader["BD_ORIGEN"] == DBNull.Value) bdorigen = 0;
                        else bdorigen = Convert.ToInt32(reader["BD_ORIGEN"]);

                        if (reader["ID_SERVICIO"] == DBNull.Value) idservicio = 0;
                        else idservicio = Convert.ToInt32(reader["ID_SERVICIO"]);

                        if (reader["ID_COMMODITY_BD"] == DBNull.Value) idcommoditybd = 0;
                        else idcommoditybd = Convert.ToInt32(reader["ID_COMMODITY_BD"]);

                        if (reader["ID_CONTENEDOR_BD"] == DBNull.Value) idcontenedorbd = 0;
                        else idcontenedorbd = Convert.ToInt32(reader["ID_CONTENEDOR_BD"]);

                        if (reader["ID_LUGAR_ORIGEN_BD"] == DBNull.Value) idlugarorigen = 0;
                        else idlugarorigen = Convert.ToInt32(reader["ID_LUGAR_ORIGEN_BD"]);

                        if (reader["ID_LUGAR_DESTINO_BD"] == DBNull.Value) idlugardestino = 0;
                        else idlugardestino = Convert.ToInt32(reader["ID_LUGAR_DESTINO_BD"]);

                        /*if (reader["ELIMINADO"] == DBNull.Value) eliminado = 0;
                        else eliminado = Convert.ToInt32(reader["ELIMINADO"]);
                        */

                        Servicios.Add(new Clases.Servicio
                        {
                            BDOrigen = bdorigen,
                            IdServicio = idservicio,
                            IdControlador = reader["CONTROLADOR"].ToString(),
                            FechaControlador = fechaControlador,
                            NumModem = reader["NUMMODEM"].ToString(),
                            Contenedor = reader["CONTENEDOR"].ToString(),
                            Booking = reader["BOOKING"].ToString(),
                            Commodity = reader["COMMODITY"].ToString(),
                            Naviera = reader["NAVIERA"].ToString(),
                            Nave = reader["NAVE"].ToString(),
                            PuertoOrigen = reader["PUERTOORIGEN"].ToString(),
                            PuertoDestino = reader["PUERTODESTINO"].ToString(),
                            EtaPuerto = etaPuerto,
                            Etd = etd,
                            Descripcion = reader["DESCRIPCION"].ToString(),
                            SetpointAC = reader["SETPOINT"].ToString(),
                            SetpointT = reader["TEMPERATURA"].ToString(),
                            Modem = reader["MODEM"].ToString(),
                            TemperaturaExt = reader["SENSOR_T"].ToString(),
                            HumedadExt = reader["SENSOR_HUM"].ToString(),
                            CO2Ext = co2flag,
                            AperturaPuerta = reader["SENSOR_LUZ"].ToString(),
                            IdCommodityBD = idcommoditybd,
                            IdContenedorBD = idcontenedorbd,
                            IdLugarOrigenBD = idlugarorigen,
                            IdLugarDestinoBD = idlugardestino,
                            //Eliminado = eliminado,

                            SensoresServicio = sensores,
                            StringSensores = stringsensores,
                            ObtieneDatos = obtuvoDatos

                            //IdNaviera = reader["ID_NAVIERA"].ToString(),
                            //IdFreightForWarder = reader["ID_FREIGHTFORWARDER"].ToString(),
                            //IdExportador = reader["ID_EXPORTADOR"].ToString(),


                        });
                    //}
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }

            return Servicios;
        }

        public static List<Clases.Servicio> GetServicios2(string idCliente="", string contenedor="")
        {
            string tipoCliente = "";
            string cliente = "";
            if(idCliente!="" && idCliente!=null)
            {
                string[] valores = idCliente.Split('-');
                tipoCliente = valores[0];
                cliente = valores[1];
            }
                
            
           

            List<Clases.Servicio> Servicios = new List<Clases.Servicio>();
            SqlConnection cnn;
            cnn = new SqlConnection(AlmaTest);
            string modemflag = "", co2flag = "", stringsensores = "", obtuvoDatos = "NO";
            DateTime? fecha;
            List<Sensor> sensores = new List<Sensor>();

            try
            {
                List<string> prueba = new List<string>();
                cnn.Open();
                SqlCommand command = new SqlCommand("EXEC dbo.ConsultarHistoricoServiciosUserPorPerfiles_FiltroCliente @USUARIO, @TIPO_CLIENTE, @IDCLIENTE, @CONTENEDOR;", cnn);
                command.Parameters.Add("@USUARIO", SqlDbType.Int).Value = 4;

                if (tipoCliente != null && tipoCliente!="")
                {
                    command.Parameters.Add("@TIPO_CLIENTE", SqlDbType.Int).Value = tipoCliente;
                }else
                {
                    command.Parameters.Add("@TIPO_CLIENTE", SqlDbType.Int).Value = DBNull.Value;
                }
                
                if(cliente!=null && cliente!="")
                {
                    command.Parameters.Add("@IDCLIENTE", SqlDbType.Int).Value = cliente;
                }else
                {
                    command.Parameters.Add("@IDCLIENTE", SqlDbType.Int).Value = DBNull.Value;
                }

                
                if(contenedor!=null)
                {
                    command.Parameters.Add("@CONTENEDOR", SqlDbType.VarChar).Value = contenedor;
                }else
                {
                    command.Parameters.Add("@CONTENEDOR", SqlDbType.VarChar).Value = DBNull.Value;
                }
                
                command.CommandTimeout = 999;
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                        prueba.Add(reader[i].ToString());
                    modemflag = ""; co2flag = ""; stringsensores = ""; obtuvoDatos = "NO"; fecha = null; sensores = null;

                    DateTime? etaPuerto = null, fechaControlador = null, etd = null;

                    modemflag = reader["MODEM"].ToString();
                    co2flag = reader["SENSOR_CO2"].ToString();

                    if (modemflag == "SI")
                    {
                        co2flag = "SI";
                    }

                    if (reader["ETAPUERTO"] != DBNull.Value)
                    {
                        etaPuerto = Convert.ToDateTime(reader["ETAPUERTO"]);
                    }
                    /*else {
                        etaPuerto = DateTime.Today;
                    }*/

                    if (reader["ETD"] != DBNull.Value)
                    {
                        etd = Convert.ToDateTime(reader["ETD"]);
                    }
                    /*else
                    {
                        etd = DateTime.Today;
                    }*/

                    if (reader["FECHACONTROLADOR"] != DBNull.Value)
                    {
                        fechaControlador = Convert.ToDateTime(reader["FECHACONTROLADOR"]);
                    }

                    /*if ((reader["ELIMINADO"] != DBNull.Value) && (Convert.ToInt32(reader["ELIMINADO"]) != 1))
                    {*/
                    sensores = ObtenerSensoresServicio(Convert.ToInt32(reader["ID_SERVICIO"]), Convert.ToInt32(reader["BD_ORIGEN"]));
                    if (sensores.Count != 0)
                    {
                        stringsensores = GenerarStringSensores(sensores);
                        obtuvoDatos = ServicioObtuvoDatos(sensores);

                        fecha = ConsultarPrimeraFechaSensor(Convert.ToInt32(reader["ID_SERVICIO"]), Convert.ToInt32(reader["BD_ORIGEN"]));
                        if (((fecha != null) && (fecha < fechaControlador)) || (fechaControlador == null)) fechaControlador = fecha;
                    }





                    int bdorigen = 0, idservicio = 0, idcommoditybd = 0, idcontenedorbd = 0, idlugarorigen = 0, idlugardestino = 0, eliminado = 0;

                    if (reader["BD_ORIGEN"] == DBNull.Value) bdorigen = 0;
                    else bdorigen = Convert.ToInt32(reader["BD_ORIGEN"]);

                    if (reader["ID_SERVICIO"] == DBNull.Value) idservicio = 0;
                    else idservicio = Convert.ToInt32(reader["ID_SERVICIO"]);

                    if (reader["ID_COMMODITY_BD"] == DBNull.Value) idcommoditybd = 0;
                    else idcommoditybd = Convert.ToInt32(reader["ID_COMMODITY_BD"]);

                    if (reader["ID_CONTENEDOR_BD"] == DBNull.Value) idcontenedorbd = 0;
                    else idcontenedorbd = Convert.ToInt32(reader["ID_CONTENEDOR_BD"]);

                    if (reader["ID_LUGAR_ORIGEN_BD"] == DBNull.Value) idlugarorigen = 0;
                    else idlugarorigen = Convert.ToInt32(reader["ID_LUGAR_ORIGEN_BD"]);

                    if (reader["ID_LUGAR_DESTINO_BD"] == DBNull.Value) idlugardestino = 0;
                    else idlugardestino = Convert.ToInt32(reader["ID_LUGAR_DESTINO_BD"]);

                    /*if (reader["ELIMINADO"] == DBNull.Value) eliminado = 0;
                    else eliminado = Convert.ToInt32(reader["ELIMINADO"]);
                    */

                    Servicios.Add(new Clases.Servicio
                    {
                        BDOrigen = bdorigen,
                        IdServicio = idservicio,
                        IdControlador = reader["CONTROLADOR"].ToString(),
                        FechaControlador = fechaControlador,
                        NumModem = reader["NUMMODEM"].ToString(),
                        Contenedor = reader["CONTENEDOR"].ToString(),
                        Booking = reader["BOOKING"].ToString(),
                        Commodity = reader["COMMODITY"].ToString(),
                        Naviera = reader["NAVIERA"].ToString(),
                        Nave = reader["NAVE"].ToString(),
                        PuertoOrigen = reader["PUERTOORIGEN"].ToString(),
                        PuertoDestino = reader["PUERTODESTINO"].ToString(),
                        EtaPuerto = etaPuerto,
                        Etd = etd,
                        Descripcion = reader["DESCRIPCION"].ToString(),
                        SetpointAC = reader["SETPOINT"].ToString(),
                        SetpointT = reader["TEMPERATURA"].ToString(),
                        Modem = reader["MODEM"].ToString(),
                        TemperaturaExt = reader["SENSOR_T"].ToString(),
                        HumedadExt = reader["SENSOR_HUM"].ToString(),
                        CO2Ext = co2flag,
                        AperturaPuerta = reader["SENSOR_LUZ"].ToString(),
                        IdCommodityBD = idcommoditybd,
                        IdContenedorBD = idcontenedorbd,
                        IdLugarOrigenBD = idlugarorigen,
                        IdLugarDestinoBD = idlugardestino,
                        //Eliminado = eliminado,

                        SensoresServicio = sensores,
                        StringSensores = stringsensores,
                        ObtieneDatos = obtuvoDatos,

                        Cliente = reader["NOMBRE_CLIENTE"].ToString()

                        //IdNaviera = reader["ID_NAVIERA"].ToString(),
                        //IdFreightForWarder = reader["ID_FREIGHTFORWARDER"].ToString(),
                        //IdExportador = reader["ID_EXPORTADOR"].ToString(),


                    });
                    //}
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }

            return Servicios;
        }

        public static List<Clases.Servicio> GetServiciosHistoricoComercial(string idCliente = "", string contenedor = "")
        {
            string tipoCliente = "";
            string cliente = "";
            if (idCliente != "" && idCliente != null)
            {
                string[] valores = idCliente.Split('-');
                tipoCliente = valores[0];
                cliente = valores[1];
            }




            List<Clases.Servicio> Servicios = new List<Clases.Servicio>();
            SqlConnection cnn;
            cnn = new SqlConnection(AlmaTest);
            string modemflag = "", co2flag = "", stringsensores = "", obtuvoDatos = "NO";
            DateTime? fecha;
            List<Sensor> sensores = new List<Sensor>();

            try
            {
                List<string> prueba = new List<string>();
                cnn.Open();
                SqlCommand command = new SqlCommand("EXEC dbo.ConsultarHistoricoServiciosUserPorPerfiles_FiltroCliente_HistComerc @USUARIO, @TIPO_CLIENTE, @IDCLIENTE, @CONTENEDOR;", cnn);
                command.Parameters.Add("@USUARIO", SqlDbType.Int).Value = 4;

                if (tipoCliente != null && tipoCliente != "")
                {
                    command.Parameters.Add("@TIPO_CLIENTE", SqlDbType.Int).Value = tipoCliente;
                }
                else
                {
                    command.Parameters.Add("@TIPO_CLIENTE", SqlDbType.Int).Value = DBNull.Value;
                }

                if (cliente != null && cliente != "")
                {
                    command.Parameters.Add("@IDCLIENTE", SqlDbType.Int).Value = cliente;
                }
                else
                {
                    command.Parameters.Add("@IDCLIENTE", SqlDbType.Int).Value = DBNull.Value;
                }


                if (contenedor != null)
                {
                    command.Parameters.Add("@CONTENEDOR", SqlDbType.VarChar).Value = contenedor;
                }
                else
                {
                    command.Parameters.Add("@CONTENEDOR", SqlDbType.VarChar).Value = DBNull.Value;
                }

                command.CommandTimeout = 999;
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                        prueba.Add(reader[i].ToString());
                    modemflag = ""; co2flag = ""; stringsensores = ""; obtuvoDatos = "NO"; fecha = null; sensores = null;

                    DateTime? etaPuerto = null, fechaControlador = null, etd = null;

                    modemflag = reader["MODEM"].ToString();
                    co2flag = reader["SENSOR_CO2"].ToString();

                    if (modemflag == "SI")
                    {
                        co2flag = "SI";
                    }

                    if (reader["ETAPUERTO"] != DBNull.Value)
                    {
                        etaPuerto = Convert.ToDateTime(reader["ETAPUERTO"]);
                    }
                    /*else {
                        etaPuerto = DateTime.Today;
                    }*/

                    if (reader["ETD"] != DBNull.Value)
                    {
                        etd = Convert.ToDateTime(reader["ETD"]);
                    }
                    /*else
                    {
                        etd = DateTime.Today;
                    }*/

                    if (reader["FECHACONTROLADOR"] != DBNull.Value)
                    {
                        fechaControlador = Convert.ToDateTime(reader["FECHACONTROLADOR"]);
                    }

                    /*if ((reader["ELIMINADO"] != DBNull.Value) && (Convert.ToInt32(reader["ELIMINADO"]) != 1))
                    {*/
                    sensores = ObtenerSensoresServicio(Convert.ToInt32(reader["ID_SERVICIO"]), Convert.ToInt32(reader["BD_ORIGEN"]));
                    if (sensores.Count != 0)
                    {
                        stringsensores = GenerarStringSensores(sensores);
                        obtuvoDatos = ServicioObtuvoDatos(sensores);

                        fecha = ConsultarPrimeraFechaSensor(Convert.ToInt32(reader["ID_SERVICIO"]), Convert.ToInt32(reader["BD_ORIGEN"]));
                        if (((fecha != null) && (fecha < fechaControlador)) || (fechaControlador == null)) fechaControlador = fecha;
                    }





                    int bdorigen = 0, idservicio = 0, idcommoditybd = 0, idcontenedorbd = 0, idlugarorigen = 0, idlugardestino = 0, eliminado = 0;

                    if (reader["BD_ORIGEN"] == DBNull.Value) bdorigen = 0;
                    else bdorigen = Convert.ToInt32(reader["BD_ORIGEN"]);

                    if (reader["ID_SERVICIO"] == DBNull.Value) idservicio = 0;
                    else idservicio = Convert.ToInt32(reader["ID_SERVICIO"]);

                    if (reader["ID_COMMODITY_BD"] == DBNull.Value) idcommoditybd = 0;
                    else idcommoditybd = Convert.ToInt32(reader["ID_COMMODITY_BD"]);

                    if (reader["ID_CONTENEDOR_BD"] == DBNull.Value) idcontenedorbd = 0;
                    else idcontenedorbd = Convert.ToInt32(reader["ID_CONTENEDOR_BD"]);

                    if (reader["ID_LUGAR_ORIGEN_BD"] == DBNull.Value) idlugarorigen = 0;
                    else idlugarorigen = Convert.ToInt32(reader["ID_LUGAR_ORIGEN_BD"]);

                    if (reader["ID_LUGAR_DESTINO_BD"] == DBNull.Value) idlugardestino = 0;
                    else idlugardestino = Convert.ToInt32(reader["ID_LUGAR_DESTINO_BD"]);

                    /*if (reader["ELIMINADO"] == DBNull.Value) eliminado = 0;
                    else eliminado = Convert.ToInt32(reader["ELIMINADO"]);
                    */

                    Servicios.Add(new Clases.Servicio
                    {
                        BDOrigen = bdorigen,
                        IdServicio = idservicio,
                        IdControlador = reader["CONTROLADOR"].ToString(),
                        FechaControlador = fechaControlador,
                        NumModem = reader["NUMMODEM"].ToString(),
                        Contenedor = reader["CONTENEDOR"].ToString(),
                        Booking = reader["BOOKING"].ToString(),
                        Commodity = reader["COMMODITY"].ToString(),
                        Naviera = reader["NAVIERA"].ToString(),
                        Nave = reader["NAVE"].ToString(),
                        PuertoOrigen = reader["PUERTOORIGEN"].ToString(),
                        PuertoDestino = reader["PUERTODESTINO"].ToString(),
                        EtaPuerto = etaPuerto,
                        Etd = etd,
                        Descripcion = reader["DESCRIPCION"].ToString(),
                        SetpointAC = reader["SETPOINT"].ToString(),
                        SetpointT = reader["TEMPERATURA"].ToString(),
                        Modem = reader["MODEM"].ToString(),
                        TemperaturaExt = reader["SENSOR_T"].ToString(),
                        HumedadExt = reader["SENSOR_HUM"].ToString(),
                        CO2Ext = co2flag,
                        AperturaPuerta = reader["SENSOR_LUZ"].ToString(),
                        IdCommodityBD = idcommoditybd,
                        IdContenedorBD = idcontenedorbd,
                        IdLugarOrigenBD = idlugarorigen,
                        IdLugarDestinoBD = idlugardestino,
                        //Eliminado = eliminado,

                        SensoresServicio = sensores,
                        StringSensores = stringsensores,
                        ObtieneDatos = obtuvoDatos,

                        Cliente = reader["NOMBRE_CLIENTE"].ToString()

                        //IdNaviera = reader["ID_NAVIERA"].ToString(),
                        //IdFreightForWarder = reader["ID_FREIGHTFORWARDER"].ToString(),
                        //IdExportador = reader["ID_EXPORTADOR"].ToString(),


                    });
                    //}
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }

            return Servicios;
        }

        public static List<Clases.Servicio> GetServicios3()
        {

            List<Clases.Servicio> Servicios = new List<Clases.Servicio>();
            SqlConnection cnn;
            cnn = new SqlConnection(AlmaTest);
            string modemflag = "", co2flag = "", stringsensores = "", obtuvoDatos = "NO";
            DateTime? fecha;
            List<Sensor> sensores = new List<Sensor>();

            try
            {
                List<string> prueba = new List<string>();
                cnn.Open();
                SqlCommand command = new SqlCommand("EXEC dbo.ConsultarServiciosUserPorPerfiles @USUARIO;", cnn);
                command.Parameters.Add("@USUARIO", SqlDbType.Int).Value = 39;
                command.CommandTimeout = 999;
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                        prueba.Add(reader[i].ToString());
                    modemflag = ""; co2flag = ""; stringsensores = ""; obtuvoDatos = "NO"; fecha = null; sensores = null;

                    DateTime? etaPuerto = null, fechaControlador = null, etd = null;

                    modemflag = reader["MODEM"].ToString();
                    co2flag = reader["SENSOR_CO2"].ToString();

                    if (modemflag == "SI")
                    {
                        co2flag = "SI";
                    }

                    if (reader["ETAPUERTO"] != DBNull.Value)
                    {
                        etaPuerto = Convert.ToDateTime(reader["ETAPUERTO"]);
                    }

                    if (reader["ETD"] != DBNull.Value)
                    {
                        etd = Convert.ToDateTime(reader["ETD"]);
                    }

                    if (reader["FECHACONTROLADOR"] != DBNull.Value)
                    {
                        fechaControlador = Convert.ToDateTime(reader["FECHACONTROLADOR"]);
                    }

                    /*if ((reader["ELIMINADO"] != DBNull.Value) && (Convert.ToInt32(reader["ELIMINADO"]) != 1))
                    {*/
                    sensores = ObtenerSensoresServicio(Convert.ToInt32(reader["ID_SERVICIO"]), Convert.ToInt32(reader["BD_ORIGEN"]));
                    if (sensores.Count != 0)
                    {
                        stringsensores = GenerarStringSensores(sensores);
                        obtuvoDatos = ServicioObtuvoDatos(sensores);

                        fecha = ConsultarPrimeraFechaSensor(Convert.ToInt32(reader["ID_SERVICIO"]), Convert.ToInt32(reader["BD_ORIGEN"]));
                        if (((fecha != null) && (fecha < fechaControlador)) || (fechaControlador == null)) fechaControlador = fecha;
                    }





                    int bdorigen = 0, idservicio = 0, idcommoditybd = 0, idcontenedorbd = 0, idlugarorigen = 0, idlugardestino = 0, eliminado = 0;

                    if (reader["BD_ORIGEN"] == DBNull.Value) bdorigen = 0;
                    else bdorigen = Convert.ToInt32(reader["BD_ORIGEN"]);

                    if (reader["ID_SERVICIO"] == DBNull.Value) idservicio = 0;
                    else idservicio = Convert.ToInt32(reader["ID_SERVICIO"]);

                    if (reader["ID_COMMODITY_BD"] == DBNull.Value) idcommoditybd = 0;
                    else idcommoditybd = Convert.ToInt32(reader["ID_COMMODITY_BD"]);

                    if (reader["ID_CONTENEDOR_BD"] == DBNull.Value) idcontenedorbd = 0;
                    else idcontenedorbd = Convert.ToInt32(reader["ID_CONTENEDOR_BD"]);

                    if (reader["ID_LUGAR_ORIGEN_BD"] == DBNull.Value) idlugarorigen = 0;
                    else idlugarorigen = Convert.ToInt32(reader["ID_LUGAR_ORIGEN_BD"]);

                    if (reader["ID_LUGAR_DESTINO_BD"] == DBNull.Value) idlugardestino = 0;
                    else idlugardestino = Convert.ToInt32(reader["ID_LUGAR_DESTINO_BD"]);

                    /*if (reader["ELIMINADO"] == DBNull.Value) eliminado = 0;
                    else eliminado = Convert.ToInt32(reader["ELIMINADO"]);
                    */

                    Servicios.Add(new Clases.Servicio
                    {
                        BDOrigen = bdorigen,
                        IdServicio = idservicio,
                        IdControlador = reader["CONTROLADOR"].ToString(),
                        FechaControlador = fechaControlador,
                        NumModem = reader["NUMMODEM"].ToString(),
                        Contenedor = reader["CONTENEDOR"].ToString(),
                        Booking = reader["BOOKING"].ToString(),
                        Commodity = reader["COMMODITY"].ToString(),
                        Naviera = reader["NAVIERA"].ToString(),
                        Nave = reader["NAVE"].ToString(),
                        PuertoOrigen = reader["PUERTOORIGEN"].ToString(),
                        PuertoDestino = reader["PUERTODESTINO"].ToString(),
                        EtaPuerto = etaPuerto,
                        Etd = etd,
                        Descripcion = reader["DESCRIPCION"].ToString(),
                        SetpointAC = reader["SETPOINT"].ToString(),
                        SetpointT = reader["TEMPERATURA"].ToString(),
                        Modem = reader["MODEM"].ToString(),
                        TemperaturaExt = reader["SENSOR_T"].ToString(),
                        HumedadExt = reader["SENSOR_HUM"].ToString(),
                        CO2Ext = co2flag,
                        AperturaPuerta = reader["SENSOR_LUZ"].ToString(),
                        IdCommodityBD = idcommoditybd,
                        IdContenedorBD = idcontenedorbd,
                        IdLugarOrigenBD = idlugarorigen,
                        IdLugarDestinoBD = idlugardestino,
                        //Eliminado = eliminado,

                        SensoresServicio = sensores,
                        StringSensores = stringsensores,
                        ObtieneDatos = obtuvoDatos

                        //IdNaviera = reader["ID_NAVIERA"].ToString(),
                        //IdFreightForWarder = reader["ID_FREIGHTFORWARDER"].ToString(),
                        //IdExportador = reader["ID_EXPORTADOR"].ToString(),


                    });
                    //}
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }

            return Servicios;
        }
        public static Clases.RangosBeneficio GetRangosServicio(int IdServicio)
        {
            Clases.RangosBeneficio rangos = new Clases.RangosBeneficio();

            SqlConnection cnn2;
            cnn2 = new SqlConnection(connectionAPIService);

            try
            {
                cnn2.Open();
                SqlCommand command2 = new SqlCommand("EXEC dbo.ConsultarRangosBeneficio @IDSERVICIO;", cnn2);
                command2.Parameters.Add("@IDSERVICIO", SqlDbType.Int).Value = IdServicio;

                SqlDataReader reader2 = command2.ExecuteReader();
                while (reader2.Read())
                {
                    rangos.RangoAltoCO2 = float.Parse(reader2["RANGO_ALTO_CO2"].ToString());
                    rangos.RangoBajoCO2 = float.Parse(reader2["RANGO_BAJO_CO2"].ToString());
                    rangos.RangoAltoO2 = float.Parse(reader2["RANGO_ALTO_O2"].ToString());
                    rangos.RangoBajoO2 = float.Parse(reader2["RANGO_BAJO_O2"].ToString());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn2.Close();
            }

            return rangos;
        }

        public static List<Clases.Servicio> GetHistoricoServicios(int IdUsuario, Clases.DataFiltros filtroData)
        {

            List<Clases.Servicio> Servicios = new List<Clases.Servicio>();
            SqlConnection cnn;
            cnn = new SqlConnection(AlmaTest);
            string modemflag = "", co2flag = "", stringsensores = "", obtuvoDatos = "NO";
            DateTime? fecha;
            List<Sensor> sensores = new List<Sensor>();


            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("EXEC dbo.ConsultarHistoricoServiciosUserPorPerfiles @USUARIO;", cnn);
                command.CommandTimeout = 999;
                command.Parameters.Add("@USUARIO", SqlDbType.Int).Value = IdUsuario;

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {

                    DateTime? etaPuerto = null, fechaControlador = null, etd = null;
                    modemflag = ""; co2flag = ""; stringsensores = ""; obtuvoDatos = "NO"; fecha = null; sensores = null;

                    modemflag = reader["MODEM"].ToString();
                    co2flag = reader["SENSOR_CO2"].ToString();

                    if (modemflag == "SI")
                    {
                        co2flag = "SI";
                    }

                    if (reader["ETAPUERTO"] != DBNull.Value)
                    {
                        etaPuerto = Convert.ToDateTime(reader["ETAPUERTO"]);
                    }

                    if (reader["ETD"] != DBNull.Value)
                    {
                        etd = Convert.ToDateTime(reader["ETD"]);
                    }

                    if (reader["FECHACONTROLADOR"] != DBNull.Value)
                    {
                        fechaControlador = Convert.ToDateTime(reader["FECHACONTROLADOR"]);
                    }

                    /*if ((reader["ELIMINADO"] != DBNull.Value) && (Convert.ToInt32(reader["ELIMINADO"]) != 1))
                    {*/
                        sensores = ObtenerSensoresServicio(Convert.ToInt32(reader["ID_SERVICIO"]), Convert.ToInt32(reader["BD_ORIGEN"]));
                        if (sensores.Count != 0)
                        {
                            stringsensores = GenerarStringSensores(sensores);
                            obtuvoDatos = ServicioObtuvoDatos(sensores);

                            fecha = ConsultarPrimeraFechaSensor(Convert.ToInt32(reader["ID_SERVICIO"]), Convert.ToInt32(reader["BD_ORIGEN"]));
                            if (((fecha != null) && (fecha < fechaControlador)) || (fechaControlador == null)) fechaControlador = fecha;
                        }

                        int bdorigen = 0, idservicio = 0, idcommoditybd = 0, idcontenedorbd = 0, idlugarorigen = 0, idlugardestino = 0, eliminado = 0;

                        if (reader["BD_ORIGEN"] == DBNull.Value) bdorigen = 0;
                        else bdorigen = Convert.ToInt32(reader["BD_ORIGEN"]);

                        if (reader["ID_SERVICIO"] == DBNull.Value) idservicio = 0;
                        else idservicio = Convert.ToInt32(reader["ID_SERVICIO"]);

                        if (reader["ID_COMMODITY_BD"] == DBNull.Value) idcommoditybd = 0;
                        else idcommoditybd = Convert.ToInt32(reader["ID_COMMODITY_BD"]);

                        if (reader["ID_CONTENEDOR_BD"] == DBNull.Value) idcontenedorbd = 0;
                        else idcontenedorbd = Convert.ToInt32(reader["ID_CONTENEDOR_BD"]);

                        if (reader["ID_LUGAR_ORIGEN_BD"] == DBNull.Value) idlugarorigen = 0;
                        else idlugarorigen = Convert.ToInt32(reader["ID_LUGAR_ORIGEN_BD"]);

                        if (reader["ID_LUGAR_DESTINO_BD"] == DBNull.Value) idlugardestino = 0;
                        else idlugardestino = Convert.ToInt32(reader["ID_LUGAR_DESTINO_BD"]);

                        /*if (reader["ELIMINADO"] == DBNull.Value) eliminado = 0;
                        else eliminado = Convert.ToInt32(reader["ELIMINADO"]);
                        */

                        Servicios.Add(new Clases.Servicio
                        {
                            BDOrigen = bdorigen,
                            IdServicio = idservicio,
                            IdControlador = reader["CONTROLADOR"].ToString(),
                            FechaControlador = fechaControlador,
                            NumModem = reader["NUMMODEM"].ToString(),
                            Contenedor = reader["CONTENEDOR"].ToString(),
                            Booking = reader["BOOKING"].ToString(),
                            Commodity = reader["COMMODITY"].ToString(),
                            Naviera = reader["NAVIERA"].ToString(),
                            Nave = reader["NAVE"].ToString(),
                            PuertoOrigen = reader["PUERTOORIGEN"].ToString(),
                            PuertoDestino = reader["PUERTODESTINO"].ToString(),
                            EtaPuerto = etaPuerto,
                            Etd = etd,
                            Descripcion = reader["DESCRIPCION"].ToString(),
                            SetpointAC = reader["SETPOINT"].ToString(),
                            SetpointT = reader["TEMPERATURA"].ToString(),
                            Modem = reader["MODEM"].ToString(),
                            TemperaturaExt = reader["SENSOR_T"].ToString(),
                            HumedadExt = reader["SENSOR_HUM"].ToString(),
                            CO2Ext = co2flag,
                            AperturaPuerta = reader["SENSOR_LUZ"].ToString(),
                            IdCommodityBD = idcommoditybd,
                            IdContenedorBD = idcontenedorbd,
                            IdLugarOrigenBD = idlugarorigen,
                            IdLugarDestinoBD = idlugardestino,
                            //Eliminado = eliminado,

                            SensoresServicio = sensores,
                            StringSensores = stringsensores,
                            ObtieneDatos = obtuvoDatos

                        });
                   // }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }

            return Servicios;
        }

        public static List<Clases.Mapa> GetMapa(List<Clases.Servicio> Servicios)
        {
            MySqlConnection conec;
            conec = new MySqlConnection(connectionStringTecnica);

            Clases.Mapa ubicacion = new Clases.Mapa();
            List<Clases.Mapa> Mapas = new List<Clases.Mapa>();
            try
            {
                foreach (Clases.Servicio servicio in Servicios)
                {
                    conec.Open();

                    int flagNull = 0;
                    MySqlCommand comando = new MySqlCommand("SELECT REGISTRATIONDATE, LATITUDE, LONGITUDE FROM logisticModem WHERE IDMODEM = (SELECT id FROM modem WHERE ESN = @NUMMODEM ORDER BY id DESC LIMIT 1) AND registrationDate >= @FECHACONTROLADOR AND registrationDate <= DATE_ADD(@ETAPOD, INTERVAL 10 DAY) AND LATITUDE IS NOT NULL ORDER BY registrationDate DESC LIMIT 1;", conec);
                    comando.Parameters.Add("@NUMMODEM", MySqlDbType.VarChar, 50).Value = servicio.NumModem;
                    comando.Parameters.Add("@FECHACONTROLADOR", MySqlDbType.DateTime).Value = servicio.FechaControlador;
                    comando.Parameters.Add("@ETAPOD", MySqlDbType.DateTime).Value = servicio.EtaPuerto;
                    comando.CommandTimeout = 10;
                    MySqlDataReader lectura = comando.ExecuteReader();

                    while (lectura.Read())
                    {
                        DateTime? fechaDato = null;

                        if (lectura["REGISTRATIONDATE"] != DBNull.Value)
                        {
                            fechaDato = Convert.ToDateTime(lectura["REGISTRATIONDATE"]);
                        }

                        if (lectura["LATITUDE"] != DBNull.Value && lectura["LONGITUDE"] != DBNull.Value)
                        {

                            flagNull = 1; //El resultado no ha sido null

                            Mapas.Add(new Clases.Mapa
                            {
                                BDOrigen = 1,
                                IdServicio = servicio.IdServicio,
                                ContenedorUbicacion = servicio.Contenedor,
                                FechaUbicacion = fechaDato,
                                Latitud = lectura["LATITUDE"].ToString(),
                                Longitud = lectura["LONGITUDE"].ToString()
                            });
                        }

                    }

                    conec.Close();

                    if (flagNull == 0)
                    {

                        conec.Open();

                        MySqlCommand comando2 = new MySqlCommand("SELECT REGISTRATIONDATE, LATITUDE, LONGITUDE FROM historyLogisticModem WHERE IDMODEM = (SELECT id FROM modem WHERE ESN = @NUMMODEM ORDER BY id DESC LIMIT 1) AND registrationDate >= @FECHACONTROLADOR AND registrationDate <= DATE_ADD(@ETAPOD, INTERVAL 10 DAY) AND latitude IS NOT NULL AND longitude IS NOT NULL order by registrationDate desc LIMIT 1; ", conec);
                        comando2.Parameters.Add("@NUMMODEM", MySqlDbType.VarChar, 50).Value = servicio.NumModem;
                        comando2.Parameters.Add("@FECHACONTROLADOR", MySqlDbType.DateTime).Value = servicio.FechaControlador;
                        comando2.Parameters.Add("@ETAPOD", MySqlDbType.DateTime).Value = servicio.EtaPuerto;
                        comando2.CommandTimeout = 10;
                        MySqlDataReader lectura2 = comando2.ExecuteReader();

                        while (lectura2.Read())
                        {
                            DateTime? fechaDato = null;

                            if (lectura2["REGISTRATIONDATE"] != DBNull.Value)
                            {
                                fechaDato = Convert.ToDateTime(lectura2["REGISTRATIONDATE"]);
                            }

                            if (lectura2["LATITUDE"] != DBNull.Value && lectura2["LONGITUDE"] != DBNull.Value)
                            {
                                Mapas.Add(new Clases.Mapa
                                {
                                    IdServicio = servicio.IdServicio,
                                    ContenedorUbicacion = servicio.Contenedor,
                                    FechaUbicacion = fechaDato,
                                    Latitud = lectura2["LATITUDE"].ToString(),
                                    Longitud = lectura2["LONGITUDE"].ToString()
                                });
                            }
                        }

                        conec.Close();

                    }// fin logica flag
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conec.Close();
            }

            return Mapas;
        }


        //** LLAMA A TODAS LAS UBICACIONES ASOCIADAS AL MODEM DE FORMA ORDENADA **//
        public static List<Clases.Mapa> GetMapaLogistico(Clases.Servicio servicio)
        {
            MySqlConnection conec;
            conec = new MySqlConnection(connectionStringTecnica);
            DateTime fechaC2 = DateTime.Today;

            if (servicio.NumModem.Trim() == "886B0FC67D46")
            {
                fechaC2 = Convert.ToDateTime(servicio.FechaControlador);
                fechaC2 = fechaC2.AddDays(4);
                fechaC2 = fechaC2.AddHours(10);
            }

            Clases.Mapa ubicacion;
            Clases.Mapa ubicacionAnterior = new Clases.Mapa { IdUbicacion = "0", Latitud = "0", Longitud = "0" };
            List<Clases.Mapa> Mapas = new List<Clases.Mapa>();
            try
            {
                conec.Open();
                MySqlCommand comando = new MySqlCommand("SELECT REGISTRATIONDATE, LATITUDE, LONGITUDE FROM historyLogisticModem WHERE IDMODEM = (SELECT id FROM modem WHERE ESN = @NUMMODEM ORDER BY id DESC LIMIT 1) AND registrationDate >= @FECHACONTROLADOR AND registrationDate <= @ETAPOD AND LATITUDE IS NOT NULL ORDER BY registrationDate DESC;", conec);
                comando.Parameters.Add("@NUMMODEM", MySqlDbType.VarChar, 50).Value = servicio.NumModem;
                if (servicio.NumModem.Trim() == "886B0FC67D46")
                {
                    comando.Parameters.Add("@FECHACONTROLADOR", MySqlDbType.DateTime).Value = fechaC2;
                }
                else
                {
                    comando.Parameters.Add("@FECHACONTROLADOR", MySqlDbType.DateTime).Value = servicio.FechaControlador;
                }


                comando.Parameters.Add("@ETAPOD", MySqlDbType.DateTime).Value = servicio.EtaPuerto5;
                comando.CommandTimeout = 60;
                MySqlDataReader lectura = comando.ExecuteReader();

                while (lectura.Read())
                {

                    DateTime? fechaDato = null;

                    if (lectura["REGISTRATIONDATE"] != DBNull.Value)
                    {
                        fechaDato = Convert.ToDateTime(lectura["REGISTRATIONDATE"]);
                    }

                    ubicacion = new Clases.Mapa
                    {
                        FechaUbicacion = fechaDato,
                        Latitud = lectura["LATITUDE"].ToString(),
                        Longitud = lectura["LONGITUDE"].ToString()
                    };

                    if ((ubicacionAnterior.Latitud != ubicacion.Latitud) || (ubicacionAnterior.Longitud != ubicacion.Longitud))
                    {
                        Mapas.Add(ubicacion);
                    }

                    ubicacionAnterior = ubicacion;

                }
                conec.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conec.Close();
            }

            return Mapas;
        }

        public static List<Clases.Mapa> GetMapaMarineTraffic(List<Clases.Servicio> Servicios)
        {
            SqlConnection conec;
            conec = new SqlConnection(connectionString);
            Clases.Mapa ubicacion = new Clases.Mapa();
            List<Clases.Mapa> Mapas = new List<Clases.Mapa>();
            try
            {
                foreach (Clases.Servicio servicio in Servicios)
                {
                    conec.Open();

                    SqlCommand comando = new SqlCommand("EXEC GetMapaMarineTraffic @IdServicio;", conec);

                    comando.Parameters.Add("@IdServicio", SqlDbType.Int).Value = servicio.IdServicio;

                    SqlDataReader lectura = comando.ExecuteReader();

                    while (lectura.Read())
                    {
                        Mapas.Add(new Clases.Mapa
                        {
                            IdUbicacion = "",
                            Latitud = lectura["LATITUD"].ToString(),
                            Longitud = lectura["LONGITUD"].ToString()
                        });

                    }

                    conec.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conec.Close();
            }

            return Mapas;
        }

        public static List<Clases.Mapa> GetMapaLogisticoEmerson(int IdServicio, int BDorigen)
        {
            SqlConnection conec;
            Clases.Mapa ubicacionAnterior = new Clases.Mapa { IdUbicacion = "0", Latitud = "0", Longitud = "0" };

            conec = new SqlConnection(AlmaTest);
            Clases.Mapa ubicacion = new Clases.Mapa();
            List<Clases.Mapa> Mapas = new List<Clases.Mapa>();

            try
            {
                conec.Open();

                SqlCommand comando = new SqlCommand("EXEC dbo.MapaUbicacionEmerson @IdServicio, @BDorigen;", conec);

                comando.Parameters.Add("@IdServicio", SqlDbType.Int).Value = IdServicio;
                comando.Parameters.Add("@BDorigen", SqlDbType.Int).Value = BDorigen;

                SqlDataReader lectura = comando.ExecuteReader();

                while (lectura.Read())
                {

                    DateTime? fechaDato = null;

                    if (lectura["fechaMedicion_UTC"] != DBNull.Value)
                    {
                        fechaDato = Convert.ToDateTime(lectura["fechaMedicion_UTC"]);
                    }

                    ubicacion = new Clases.Mapa
                    {
                        FechaUbicacion = fechaDato,
                        Latitud = (lectura["LATITUD"].ToString()).Replace(',','.'),      
                        Longitud = (lectura["LONGITUD"].ToString()).Replace(',', '.')
                    };

                    if ((ubicacionAnterior.Latitud != ubicacion.Latitud) || (ubicacionAnterior.Longitud != ubicacion.Longitud))
                    {
                        Mapas.Add(ubicacion);
                    }

                    ubicacionAnterior = ubicacion;

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conec.Close();
            }

            return Mapas;
        }

        public static List<Clases.Mapa> GetMapaEmerson(List<Clases.Servicio> Servicios)
        {
            SqlConnection conec;
            Clases.Mapa ubicacionAnterior = new Clases.Mapa { IdUbicacion = "0", Latitud = "0", Longitud = "0" };

            conec = new SqlConnection(AlmaTest);
            Clases.Mapa ubicacion = new Clases.Mapa();
            List<Clases.Mapa> Mapas = new List<Clases.Mapa>();

            try
            {
                foreach (Clases.Servicio servicio in Servicios)
                {
                    conec.Open();

                    SqlCommand comando = new SqlCommand("EXEC dbo.MapaUbicacionEmersonActual @IdServicio, @BDorigen;", conec);

                    comando.Parameters.Add("@IdServicio", SqlDbType.Int).Value = servicio.IdServicio;
                    comando.Parameters.Add("@BDorigen", SqlDbType.Int).Value = servicio.BDOrigen;

                    SqlDataReader lectura = comando.ExecuteReader();

                    while (lectura.Read())
                    {

                        DateTime? fechaDato = null;

                        if (lectura["fechaMedicion_UTC"] != DBNull.Value)
                        {
                            fechaDato = Convert.ToDateTime(lectura["fechaMedicion_UTC"]);
                        }

                        Mapas.Add(new Clases.Mapa
                        {
                            BDOrigen = servicio.BDOrigen,
                            IdServicio = servicio.IdServicio,
                            ContenedorUbicacion = servicio.Contenedor, //revisar
                            NumSensor = lectura["NUM_SENSOR"].ToString(),
                            FechaUbicacion = fechaDato,
                            Latitud = (lectura["LATITUD"].ToString()).Replace(',', '.'),
                            Longitud = (lectura["LONGITUD"].ToString()).Replace(',', '.')
                        });
                    }

                    conec.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conec.Close();
            }

            return Mapas;
        }

        public static List<Clases.Mapa> GetMapaLogisticoMarineTraffic(int IdServicio)
        {
            SqlConnection conec;
            conec = new SqlConnection(connectionString);
            Clases.Mapa ubicacion = new Clases.Mapa();
            List<Clases.Mapa> Mapas = new List<Clases.Mapa>();

            try
            {
                conec.Open();

                SqlCommand comando = new SqlCommand("EXEC GetMapaLogisticoMarineTraffic @IdServicio;", conec);

                comando.Parameters.Add("@IdServicio", SqlDbType.Int).Value = IdServicio;

                SqlDataReader lectura = comando.ExecuteReader();

                while (lectura.Read())
                {
                    Mapas.Add(new Clases.Mapa
                    {
                        IdUbicacion = "",
                        Latitud = lectura["LATITUD"].ToString(),
                        Longitud = lectura["LONGITUD"].ToString()
                    });

                }

                conec.Close();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conec.Close();
            }

            return Mapas;
        }

        public static Clases.Servicio GetConsultoUnServicio(int IdServicio, int BDorigen)
        {
            Clases.Servicio servicio = new Clases.Servicio();

            SqlConnection cnn2;
            cnn2 = new SqlConnection(AlmaTest);
            string modemflag = "", co2flag = "", stringsensores = "", obtuvoDatos = "NO";
            DateTime? fecha;
            List<Sensor> sensores = new List<Sensor>();

            try
            {
                cnn2.Open();
                SqlCommand command2 = new SqlCommand("EXEC dbo.ConsultoUnServicio @IDSERVICIO, @BDORIGEN;", cnn2);
                command2.Parameters.Add("@IDSERVICIO", SqlDbType.Int).Value = IdServicio;
                command2.Parameters.Add("@BDORIGEN", SqlDbType.Int).Value = BDorigen;

                SqlDataReader reader2 = command2.ExecuteReader();
                while (reader2.Read())
                {
                    modemflag = ""; co2flag = ""; stringsensores = ""; obtuvoDatos = "NO"; fecha = null; sensores = null;

                    modemflag = reader2["MODEM"].ToString();
                    co2flag = reader2["SENSOR_CO2"].ToString();

                    if (modemflag == "SI")
                    {
                        co2flag = "SI";
                    }

                    servicio.BDOrigen = Convert.ToInt32(reader2["BD_ORIGEN"]);
                    servicio.IdServicio = Convert.ToInt32(reader2["ID_SERVICIO"]);
                    servicio.Contenedor = reader2["CONTENEDOR"].ToString();

                    if (reader2["FECHACONTROLADOR"] != DBNull.Value)
                    {
                        servicio.FechaControlador = Convert.ToDateTime(reader2["FECHACONTROLADOR"]);
                    }

                    servicio.NumModem = reader2["NUMMODEM"].ToString();
                    servicio.IdControlador = reader2["CONTROLADOR"].ToString();
                    servicio.Booking = reader2["BOOKING"].ToString();
                    servicio.Commodity = reader2["COMMODITY"].ToString();
                    servicio.Naviera = reader2["NAVIERA"].ToString();
                    servicio.Nave = reader2["NAVE"].ToString();
                    servicio.PuertoOrigen = reader2["PUERTOORIGEN"].ToString();
                    servicio.PuertoDestino = reader2["PUERTODESTINO"].ToString();

                    if (reader2["ETAPUERTO"] != DBNull.Value)
                    {
                        servicio.EtaPuerto = Convert.ToDateTime(reader2["ETAPUERTO"], CultureInfo.CurrentCulture);
                    }

                    if (reader2["ETD"] != DBNull.Value)
                    {
                        servicio.Etd = Convert.ToDateTime(reader2["ETD"], CultureInfo.InvariantCulture);
                    }

                    servicio.Descripcion = reader2["DESCRIPCION"].ToString();
                    servicio.SetpointAC = reader2["SETPOINT"].ToString();
                    servicio.SetpointT = reader2["TEMPERATURA"].ToString();
                    servicio.Modem = reader2["MODEM"].ToString();
                    servicio.TemperaturaExt = reader2["SENSOR_T"].ToString();
                    servicio.HumedadExt = reader2["SENSOR_HUM"].ToString();

                    servicio.CO2Ext = co2flag;

                    servicio.AperturaPuerta = reader2["SENSOR_LUZ"].ToString();
                    if (reader2["ID_COMMODITY_BD"] != DBNull.Value) servicio.IdCommodityBD = Convert.ToInt32(reader2["ID_COMMODITY_BD"]);
                    if (reader2["ID_CONTENEDOR_BD"] != DBNull.Value) servicio.IdContenedorBD = Convert.ToInt32(reader2["ID_CONTENEDOR_BD"]);
                    if (reader2["ID_LUGAR_ORIGEN_BD"] != DBNull.Value) servicio.IdLugarOrigenBD = Convert.ToInt32(reader2["ID_LUGAR_ORIGEN_BD"]);
                    if (reader2["ID_LUGAR_DESTINO_BD"] != DBNull.Value) servicio.IdLugarDestinoBD = Convert.ToInt32(reader2["ID_LUGAR_DESTINO_BD"]);
                    if (reader2["ELIMINADO"] != DBNull.Value) servicio.Eliminado = Convert.ToInt32(reader2["ELIMINADO"]);

                    if (reader2["FECHA_FINALIZACION"] != DBNull.Value)
                    {
                        servicio.FechaFinalizacion = Convert.ToDateTime(reader2["FECHA_FINALIZACION"], CultureInfo.InvariantCulture);
                    }
                    servicio.IdEstadoServicioAlma = Convert.ToInt32(reader2["ID_ESTADOSERVICIO_ALMA"]);

                    sensores = ObtenerSensoresServicio(Convert.ToInt32(reader2["ID_SERVICIO"]), Convert.ToInt32(reader2["BD_ORIGEN"]));
                    if (sensores.Count != 0)
                    {
                        stringsensores = GenerarStringSensores(sensores);
                        obtuvoDatos = ServicioObtuvoDatos(sensores);

                        fecha = ConsultarPrimeraFechaSensor(Convert.ToInt32(reader2["ID_SERVICIO"]), Convert.ToInt32(reader2["BD_ORIGEN"]));
                        if (((fecha != null) && (fecha < servicio.FechaControlador)) || (servicio.FechaControlador == null)) servicio.FechaControlador = fecha;

                        servicio.SensoresServicio = sensores;
                        servicio.StringSensores = stringsensores;
                        servicio.ObtieneDatos = obtuvoDatos;

                    }

                    if (reader2["PAISORIGEN"] != DBNull.Value)
                    {
                        servicio.Origen = reader2["PAISORIGEN"].ToString();
                    }

                    //servicio.IdNaviera = reader["ID_NAVIERA"].ToString();
                    //servicio.IdFreightForWarder = reader["ID_FREIGHTFORWARDER"].ToString();
                    //servicio.IdExportador = reader["ID_EXPORTADOR"].ToString();



                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn2.Close();
            }
            return servicio;
        }

        public static DateTime? ConsultarPrimeraFechaSensor(int IdServicio, int bdServicio)
        {
            DateTime? fecha = null;

            SqlConnection cnn2;
            cnn2 = new SqlConnection(AlmaTest);

            try
            {
                cnn2.Open();
                SqlCommand command2 = new SqlCommand("EXEC dbo.ConsultarPrimeraFechaSensor @ID_SERVICIO, @ID_BD_SERVICIO;", cnn2);
                command2.Parameters.Add("@ID_SERVICIO", SqlDbType.Int).Value = IdServicio;
                command2.Parameters.Add("@ID_BD_SERVICIO", SqlDbType.Int).Value = bdServicio;

                SqlDataReader reader2 = command2.ExecuteReader();
                while (reader2.Read())
                {

                    if (reader2["FECHA_REGISTRO"] != DBNull.Value)
                    {
                        fecha = Convert.ToDateTime(reader2["FECHA_REGISTRO"]);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn2.Close();
            }
            return fecha;
        }

        public static List<Clases.Servicio> GetServiciosMapaFiltros(int IdUsuario, Clases.DataFiltros filtroData, int? contenedor = null, int? naviera = null, string booking = null, int? nave = null, int? porigen = null, int? pdestino = null, int? commodity = null, DateTime? etapuerto = null)
        {//arreglar tipo de datos
            List<Clases.Servicio> Servicios = new List<Clases.Servicio>();
            string modemflag = "", co2flag = "", stringsensores = "", obtuvoDatos = "NO";
            DateTime? fecha;
            List<Sensor> sensores = new List<Sensor>();

            SqlConnection cnn2;
            cnn2 = new SqlConnection(AlmaTest);

            try
            {
                cnn2.Open();
                SqlCommand command = new SqlCommand("ConsultarServiciosMapa2PorPerfiles", cnn2);
                //SqlCommand command = new SqlCommand("ConsultarServiciosMapa2", cnn2);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@USUARIO", SqlDbType.Int).Value = IdUsuario;

                if (contenedor == null)
                {
                    command.Parameters.Add("@ID_CONTENEDOR", SqlDbType.Int).Value = System.Data.SqlTypes.SqlInt32.Null;
                }
                else
                {
                    command.Parameters.Add("@ID_CONTENEDOR", SqlDbType.Int).Value = contenedor;
                }

                if (naviera == null)
                {
                    command.Parameters.Add("@ID_NAVIERA", SqlDbType.Int).Value = System.Data.SqlTypes.SqlInt32.Null;
                }
                else
                {
                    command.Parameters.Add("@ID_NAVIERA", SqlDbType.Int).Value = naviera;
                }

                if (booking == null)
                {
                    command.Parameters.Add("@BOOKING", SqlDbType.VarChar).Value = System.Data.SqlTypes.SqlString.Null;
                }
                else
                {
                    command.Parameters.Add("@BOOKING", SqlDbType.VarChar).Value = booking;
                }

                if (nave == null)
                {
                    command.Parameters.Add("@ID_NAVE", SqlDbType.Int).Value = System.Data.SqlTypes.SqlInt32.Null;
                }
                else
                {
                    command.Parameters.Add("@ID_NAVE", SqlDbType.Int).Value = nave;
                }

                if (porigen == null)
                {
                    command.Parameters.Add("@ID_POL", SqlDbType.Int).Value = System.Data.SqlTypes.SqlInt32.Null;
                }
                else
                {
                    command.Parameters.Add("@ID_POL", SqlDbType.Int).Value = porigen;
                }

                if (pdestino == null)
                {
                    command.Parameters.Add("@ID_POD", SqlDbType.Int).Value = System.Data.SqlTypes.SqlInt32.Null;
                }
                else
                {
                    command.Parameters.Add("@ID_POD", SqlDbType.Int).Value = pdestino;
                }

                if (commodity == null)
                {
                    command.Parameters.Add("@ID_COMMODITY", SqlDbType.Int).Value = System.Data.SqlTypes.SqlInt32.Null;
                }
                else
                {
                    command.Parameters.Add("@ID_COMMODITY", SqlDbType.Int).Value = commodity;
                }

                if (etapuerto == null)
                {
                    command.Parameters.Add("@ETAPOD", SqlDbType.DateTime).Value = System.Data.SqlTypes.SqlDateTime.Null;
                }
                else
                {
                    command.Parameters.Add("@ETAPOD", SqlDbType.DateTime).Value = etapuerto;
                }


                SqlDataReader reader2 = command.ExecuteReader();
                while (reader2.Read())
                {

                    DateTime? etaPuerto = null, fechaControlador = null, etd = null;
                    modemflag = ""; co2flag = ""; stringsensores = ""; obtuvoDatos = "NO"; fecha = null; sensores = null;

                    modemflag = reader2["MODEM"].ToString();
                    co2flag = reader2["SENSOR_CO2"].ToString();

                    if (modemflag == "SI")
                    {
                        co2flag = "SI";
                    }

                    if (reader2["ETAPUERTO"] != DBNull.Value)
                    {
                        etaPuerto = Convert.ToDateTime(reader2["ETAPUERTO"]);
                    }

                    if (reader2["ETD"] != DBNull.Value)
                    {
                        etd = Convert.ToDateTime(reader2["ETD"]);
                    }

                    if (reader2["FECHACONTROLADOR"] != DBNull.Value)
                    {
                        fechaControlador = Convert.ToDateTime(reader2["FECHACONTROLADOR"]);
                    }



                        sensores = ObtenerSensoresServicio(Convert.ToInt32(reader2["ID_SERVICIO"]), Convert.ToInt32(reader2["BD_ORIGEN"]));
                        if (sensores.Count != 0)
                        {
                            stringsensores = GenerarStringSensores(sensores);
                            obtuvoDatos = ServicioObtuvoDatos(sensores);

                            fecha = ConsultarPrimeraFechaSensor(Convert.ToInt32(reader2["ID_SERVICIO"]), Convert.ToInt32(reader2["BD_ORIGEN"]));
                            if (((fecha != null) && (fecha < fechaControlador)) || (fechaControlador == null)) fechaControlador = fecha;
                        }

                        int bdorigen = 0, idservicio = 0, idcommoditybd = 0, idcontenedorbd = 0, idlugarorigen = 0, idlugardestino = 0, eliminado = 0;

                        if (reader2["BD_ORIGEN"] == DBNull.Value) bdorigen = 0;
                        else bdorigen = Convert.ToInt32(reader2["BD_ORIGEN"]);

                        if (reader2["ID_SERVICIO"] == DBNull.Value) idservicio = 0;
                        else idservicio = Convert.ToInt32(reader2["ID_SERVICIO"]);

                        if (reader2["ID_COMMODITY_BD"] == DBNull.Value) idcommoditybd = 0;
                        else idcommoditybd = Convert.ToInt32(reader2["ID_COMMODITY_BD"]);

                        if (reader2["ID_CONTENEDOR_BD"] == DBNull.Value) idcontenedorbd = 0;
                        else idcontenedorbd = Convert.ToInt32(reader2["ID_CONTENEDOR_BD"]);

                        if (reader2["ID_LUGAR_ORIGEN_BD"] == DBNull.Value) idlugarorigen = 0;
                        else idlugarorigen = Convert.ToInt32(reader2["ID_LUGAR_ORIGEN_BD"]);

                        if (reader2["ID_LUGAR_DESTINO_BD"] == DBNull.Value) idlugardestino = 0;
                        else idlugardestino = Convert.ToInt32(reader2["ID_LUGAR_DESTINO_BD"]);
                        

                        Servicios.Add(new Clases.Servicio
                        {
                            BDOrigen = bdorigen,
                            IdServicio = idservicio,
                            IdControlador = reader2["CONTROLADOR"].ToString(),
                            FechaControlador = fechaControlador,
                            NumModem = reader2["NUMMODEM"].ToString(),
                            Contenedor = reader2["CONTENEDOR"].ToString(),
                            Booking = reader2["BOOKING"].ToString(),
                            Commodity = reader2["COMMODITY"].ToString(),
                            Naviera = reader2["NAVIERA"].ToString(),
                            Nave = reader2["NAVE"].ToString(),
                            PuertoOrigen = reader2["PUERTOORIGEN"].ToString(),
                            PuertoDestino = reader2["PUERTODESTINO"].ToString(),
                            EtaPuerto = etaPuerto,
                            Etd = etd,
                            Descripcion = reader2["DESCRIPCION"].ToString(),
                            SetpointAC = reader2["SETPOINT"].ToString(),
                            SetpointT = reader2["TEMPERATURA"].ToString(),
                            Modem = reader2["MODEM"].ToString(),
                            TemperaturaExt = reader2["SENSOR_T"].ToString(),
                            HumedadExt = reader2["SENSOR_HUM"].ToString(),
                            CO2Ext = co2flag,
                            AperturaPuerta = reader2["SENSOR_LUZ"].ToString(),
                            IdCommodityBD = idcommoditybd,
                            IdContenedorBD = idcontenedorbd,
                            IdLugarOrigenBD = idlugarorigen,
                            IdLugarDestinoBD = idlugardestino,


                            SensoresServicio = sensores,
                            StringSensores = stringsensores,
                            ObtieneDatos = obtuvoDatos
                            //IdNaviera = reader["ID_NAVIERA"].ToString(),
                            //IdFreightForWarder = reader["ID_FREIGHTFORWARDER"].ToString(),
                            //IdExportador = reader["ID_EXPORTADOR"].ToString(),


                        });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn2.Close();
            }
            return Servicios;
        }

        public static int GetContadorModem(int IdUsuario, Clases.DataFiltros filtroData)
        {
            int contador = 0;

            SqlConnection cnn2;
            cnn2 = new SqlConnection(AlmaTest);

            try
            {
                cnn2.Open();
                SqlCommand command2 = new SqlCommand("EXEC dbo.ConsultarContadorModemPorPerfiles @IDUSUARIO;", cnn2);
                command2.Parameters.Add("@IDUSUARIO", SqlDbType.Int).Value = IdUsuario;

                SqlDataReader reader2 = command2.ExecuteReader();
                while (reader2.Read())
                {
                    contador = Convert.ToInt32(reader2["CONTADOR_SERVICIOS"]);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn2.Close();
            }
            return contador;
        }

        public static int GetContadorTemperatura(int IdUsuario, Clases.DataFiltros filtroData)
        {
            int contador = 0;

            SqlConnection cnn2;
            cnn2 = new SqlConnection(AlmaTest);

            try
            {
                cnn2.Open();
                SqlCommand command2 = new SqlCommand("EXEC dbo.ConsultarContadorSensorTemperaturaPorPerfiles @IDUSUARIO;", cnn2);
                command2.Parameters.Add("@IDUSUARIO", SqlDbType.Int).Value = IdUsuario;

                SqlDataReader reader2 = command2.ExecuteReader();
                while (reader2.Read())
                {
                    contador = Convert.ToInt32(reader2["CONTADOR_SERVICIOS"]);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn2.Close();
            }
            return contador;
        }

        public static int GetContadorLuz(int IdUsuario, Clases.DataFiltros filtroData)
        {
            int contador = 0;

            SqlConnection cnn2;
            cnn2 = new SqlConnection(AlmaTest);

            try
            {
                cnn2.Open();
                SqlCommand command2 = new SqlCommand("EXEC dbo.ConsultarContadorSensorLuzPorPerfiles @IDUSUARIO;", cnn2);
                command2.Parameters.Add("@IDUSUARIO", SqlDbType.Int).Value = IdUsuario;

                SqlDataReader reader2 = command2.ExecuteReader();
                while (reader2.Read())
                {
                    contador = Convert.ToInt32(reader2["CONTADOR_SERVICIOS"]);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn2.Close();
            }
            return contador;
        }

        public static int GetContadorControlador(int IdUsuario, Clases.DataFiltros filtroData)
        {
            int contador = 0;

            SqlConnection cnn2;
            cnn2 = new SqlConnection(connectionAPIService);

            try
            {
                cnn2.Open();
                SqlCommand command2 = new SqlCommand("EXEC dbo.ConsultarContadorControladorPorPerfiles @IDUSUARIO, @MODEM_NULL, @CONTROLADOR_NULL, @MODEM_NOT_NULL, @CONTROLADOR_NOT_NULL;", cnn2);
                command2.Parameters.Add("@IDUSUARIO", SqlDbType.Int).Value = IdUsuario;

                command2.Parameters.Add("@MODEM_NULL", SqlDbType.Int).Value = filtroData.ModemNull;
                command2.Parameters.Add("@CONTROLADOR_NULL", SqlDbType.Int).Value = filtroData.ControladorNull;
                command2.Parameters.Add("@MODEM_NOT_NULL", SqlDbType.Int).Value = filtroData.ModemNotNull;
                command2.Parameters.Add("@CONTROLADOR_NOT_NULL", SqlDbType.Int).Value = filtroData.ControladorNotNull;

                SqlDataReader reader2 = command2.ExecuteReader();
                while (reader2.Read())
                {
                    contador = Convert.ToInt32(reader2["CONTADOR_SERVICIOS"]);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn2.Close();
            }
            return contador;
        }

        public static int GetContadorTotal(int IdUsuario, Clases.DataFiltros filtroData)
        {
            int contador = 0;

            SqlConnection cnn2;
            cnn2 = new SqlConnection(AlmaTest);

            try
            {
                cnn2.Open();
                SqlCommand command2 = new SqlCommand("EXEC dbo.ConsultarContadorTotalPorPerfiles @IDUSUARIO;", cnn2);
                command2.Parameters.Add("@IDUSUARIO", SqlDbType.Int).Value = IdUsuario;

                SqlDataReader reader2 = command2.ExecuteReader();
                while (reader2.Read())
                {
                    contador = Convert.ToInt32(reader2["CONTADOR_SERVICIOS"]);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn2.Close();
            }
            return contador;
        }

        public static int GetContadorModemHistorico(int IdUsuario, Clases.DataFiltros filtroData)
        {
            int contador = 0;

            SqlConnection cnn2;
            cnn2 = new SqlConnection(AlmaTest);

            try
            {
                cnn2.Open();
                SqlCommand command2 = new SqlCommand("EXEC dbo.ConsultarContadorModemHistoricoPorPerfiles @IDUSUARIO;", cnn2);
                command2.Parameters.Add("@IDUSUARIO", SqlDbType.Int).Value = IdUsuario;

                SqlDataReader reader2 = command2.ExecuteReader();
                while (reader2.Read())
                {
                    contador = Convert.ToInt32(reader2["CONTADOR_SERVICIOS"]);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn2.Close();
            }
            return contador;
        }

        public static int GetContadorControladorHistorico(int IdUsuario, Clases.DataFiltros filtroData)
        {
            int contador = 0;

            SqlConnection cnn2;
            cnn2 = new SqlConnection(connectionAPIService);

            try
            {
                cnn2.Open();
                SqlCommand command2 = new SqlCommand("EXEC dbo.ConsultarContadorControladorHistoricoPorPerfiles @IDUSUARIO, @MODEM_NULL, @CONTROLADOR_NULL, @MODEM_NOT_NULL, @CONTROLADOR_NOT_NULL;", cnn2);
                command2.Parameters.Add("@IDUSUARIO", SqlDbType.Int).Value = IdUsuario;

                command2.Parameters.Add("@MODEM_NULL", SqlDbType.Int).Value = filtroData.ModemNull;
                command2.Parameters.Add("@CONTROLADOR_NULL", SqlDbType.Int).Value = filtroData.ControladorNull;
                command2.Parameters.Add("@MODEM_NOT_NULL", SqlDbType.Int).Value = filtroData.ModemNotNull;
                command2.Parameters.Add("@CONTROLADOR_NOT_NULL", SqlDbType.Int).Value = filtroData.ControladorNotNull;



                SqlDataReader reader2 = command2.ExecuteReader();
                while (reader2.Read())
                {
                    contador = Convert.ToInt32(reader2["CONTADOR_SERVICIOS"]);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn2.Close();
            }
            return contador;
        }

        public static int GetContadorTemperaturaHistorico(int IdUsuario, Clases.DataFiltros filtroData)
        {
            int contador = 0;

            SqlConnection cnn2;
            cnn2 = new SqlConnection(AlmaTest);

            try
            {
                cnn2.Open();
                SqlCommand command2 = new SqlCommand("EXEC dbo.ConsultarContadorSensorTemperaturaHistoricoPorPerfiles @IDUSUARIO;", cnn2);
                command2.Parameters.Add("@IDUSUARIO", SqlDbType.Int).Value = IdUsuario;

                SqlDataReader reader2 = command2.ExecuteReader();
                while (reader2.Read())
                {
                    contador = Convert.ToInt32(reader2["CONTADOR_SERVICIOS"]);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn2.Close();
            }
            return contador;
        }

        public static int GetContadorLuzHistorico(int IdUsuario, Clases.DataFiltros filtroData)
        {
            int contador = 0;

            SqlConnection cnn2;
            cnn2 = new SqlConnection(AlmaTest);

            try
            {
                cnn2.Open();
                SqlCommand command2 = new SqlCommand("EXEC dbo.ConsultarContadorSensorLuzHistoricoPorPerfiles @IDUSUARIO;", cnn2);
                command2.Parameters.Add("@IDUSUARIO", SqlDbType.Int).Value = IdUsuario;

                SqlDataReader reader2 = command2.ExecuteReader();
                while (reader2.Read())
                {
                    contador = Convert.ToInt32(reader2["CONTADOR_SERVICIOS"]);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn2.Close();
            }
            return contador;
        }


        public static int GetContadorTotalHistorico(int IdUsuario, Clases.DataFiltros filtroData)
        {
            int contador = 0;

            SqlConnection cnn2;
            cnn2 = new SqlConnection(AlmaTest);

            try
            {
                cnn2.Open();
                SqlCommand command2 = new SqlCommand("EXEC dbo.ConsultarContadorTotalHistoricoPorPerfiles @IDUSUARIO;", cnn2);
                command2.Parameters.Add("@IDUSUARIO", SqlDbType.Int).Value = IdUsuario;

                SqlDataReader reader2 = command2.ExecuteReader();
                while (reader2.Read())
                {
                    contador = Convert.ToInt32(reader2["CONTADOR_SERVICIOS"]);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn2.Close();
            }
            return contador;
        }



        public static Clases.Totalizador GetContadores(int IdUsuario, Clases.DataFiltros data)
        {
            Clases.Totalizador Totalizadores = (new Clases.Totalizador
            {
                ContadorModem = GetContadorModem(IdUsuario, data),
                ContadorControlador = GetContadorControlador(IdUsuario, data),
                ContadorCO2 = 0,
                ContadorTemperatura = 0,
                ContadorLuz = 0,
                ContadorTotal = GetContadorTotal(IdUsuario, data)
            });

            return Totalizadores;
        }

        public static Clases.Totalizador GetContadoresHistorico(int IdUsuario, Clases.DataFiltros filtroData)
        {
            Clases.Totalizador Totalizadores = (new Clases.Totalizador
            {
                ContadorModem = GetContadorModemHistorico(IdUsuario, filtroData),
                ContadorControlador = GetContadorControladorHistorico(IdUsuario, filtroData),
                ContadorCO2 = 0,
                ContadorTemperatura = 0,
                ContadorLuz = 0,
                ContadorTotal = GetContadorTotalHistorico(IdUsuario, filtroData)
            });

            return Totalizadores;
        }


        public static List<Clases.Servicio> GetServiciosConModem(int IdUsuario, Clases.DataFiltros filtroData)
        {

            List<Clases.Servicio> Servicios = new List<Clases.Servicio>();
            SqlConnection cnn;
            cnn = new SqlConnection(AlmaTest);
            string modemflag = "", co2flag = "", stringsensores = "", obtuvoDatos = "NO";
            DateTime? fecha;
            List<Sensor> sensores = new List<Sensor>();


            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("EXEC dbo.ConsultarServiciosConModem @USUARIO;", cnn);
                command.Parameters.Add("@USUARIO", SqlDbType.Int).Value = IdUsuario;

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {

                    DateTime? etaPuerto = null, fechaControlador = null, etd = null;
                    modemflag = ""; co2flag = ""; stringsensores = ""; obtuvoDatos = "NO"; fecha = null; sensores = null;

                    modemflag = reader["MODEM"].ToString();
                    co2flag = reader["SENSOR_CO2"].ToString();

                    if (modemflag == "SI")
                    {
                        co2flag = "SI";
                    }

                    if (reader["ETAPUERTO"] != DBNull.Value)
                    {
                        etaPuerto = Convert.ToDateTime(reader["ETAPUERTO"]);
                    }

                    if (reader["ETD"] != DBNull.Value)
                    {
                        etd = Convert.ToDateTime(reader["ETD"]);
                    }

                    if (reader["FECHACONTROLADOR"] != DBNull.Value)
                    {
                        fechaControlador = Convert.ToDateTime(reader["FECHACONTROLADOR"]);
                    }

                    if ((reader["ELIMINADO"] != DBNull.Value) && (Convert.ToInt32(reader["ELIMINADO"]) != 1))
                    {
                        sensores = ObtenerSensoresServicio(Convert.ToInt32(reader["ID_SERVICIO"]), Convert.ToInt32(reader["BD_ORIGEN"]));
                        if (sensores.Count != 0)
                        {
                            stringsensores = GenerarStringSensores(sensores);
                            obtuvoDatos = ServicioObtuvoDatos(sensores);

                            fecha = ConsultarPrimeraFechaSensor(Convert.ToInt32(reader["ID_SERVICIO"]), Convert.ToInt32(reader["BD_ORIGEN"]));
                            if (((fecha != null) && (fecha < fechaControlador)) || (fechaControlador == null)) fechaControlador = fecha;
                        }

                        int bdorigen = 0, idservicio = 0, idcommoditybd = 0, idcontenedorbd = 0, idlugarorigen = 0, idlugardestino = 0, eliminado = 0;

                        if (reader["BD_ORIGEN"] == DBNull.Value) bdorigen = 0;
                        else bdorigen = Convert.ToInt32(reader["BD_ORIGEN"]);

                        if (reader["ID_SERVICIO"] == DBNull.Value) idservicio = 0;
                        else idservicio = Convert.ToInt32(reader["ID_SERVICIO"]);

                        if (reader["ID_COMMODITY_BD"] == DBNull.Value) idcommoditybd = 0;
                        else idcommoditybd = Convert.ToInt32(reader["ID_COMMODITY_BD"]);

                        if (reader["ID_CONTENEDOR_BD"] == DBNull.Value) idcontenedorbd = 0;
                        else idcontenedorbd = Convert.ToInt32(reader["ID_CONTENEDOR_BD"]);

                        if (reader["ID_LUGAR_ORIGEN_BD"] == DBNull.Value) idlugarorigen = 0;
                        else idlugarorigen = Convert.ToInt32(reader["ID_LUGAR_ORIGEN_BD"]);

                        if (reader["ID_LUGAR_DESTINO_BD"] == DBNull.Value) idlugardestino = 0;
                        else idlugardestino = Convert.ToInt32(reader["ID_LUGAR_DESTINO_BD"]);

                        if (reader["ELIMINADO"] == DBNull.Value) eliminado = 0;
                        else eliminado = Convert.ToInt32(reader["ELIMINADO"]);

                        Servicios.Add(new Clases.Servicio
                        {
                            BDOrigen = bdorigen,
                            IdServicio = idservicio,
                            IdControlador = reader["CONTROLADOR"].ToString(),
                            FechaControlador = fechaControlador,
                            NumModem = reader["NUMMODEM"].ToString(),
                            Contenedor = reader["CONTENEDOR"].ToString(),
                            Booking = reader["BOOKING"].ToString(),
                            Commodity = reader["COMMODITY"].ToString(),
                            Naviera = reader["NAVIERA"].ToString(),
                            Nave = reader["NAVE"].ToString(),
                            PuertoOrigen = reader["PUERTOORIGEN"].ToString(),
                            PuertoDestino = reader["PUERTODESTINO"].ToString(),
                            EtaPuerto = etaPuerto,
                            Etd = etd,
                            Descripcion = reader["DESCRIPCION"].ToString(),
                            SetpointAC = reader["SETPOINT"].ToString(),
                            SetpointT = reader["TEMPERATURA"].ToString(),
                            Modem = reader["MODEM"].ToString(),
                            TemperaturaExt = reader["SENSOR_T"].ToString(),
                            HumedadExt = reader["SENSOR_HUM"].ToString(),
                            CO2Ext = co2flag,
                            AperturaPuerta = reader["SENSOR_LUZ"].ToString(),
                            IdCommodityBD = idcommoditybd,
                            IdContenedorBD = idcontenedorbd,
                            IdLugarOrigenBD = idlugarorigen,
                            IdLugarDestinoBD = idlugardestino,
                            Eliminado = eliminado,

                            SensoresServicio = sensores,
                            StringSensores = stringsensores,
                            ObtieneDatos = obtuvoDatos
                            
                            //IdNaviera = reader["ID_NAVIERA"].ToString(),
                            //IdFreightForWarder = reader["ID_FREIGHTFORWARDER"].ToString(),
                            //IdExportador = reader["ID_EXPORTADOR"].ToString(),


                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }

            return Servicios;
        }

        public static List<Clases.Servicio> GetServiciosEmerson(int IdUsuario, Clases.DataFiltros filtroData)
        {

            List<Clases.Servicio> Servicios = new List<Clases.Servicio>();
            SqlConnection cnn;
            cnn = new SqlConnection(AlmaTest);

        
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("EXEC dbo.ConsultarServiciosEmerson @USUARIO;", cnn);
                command.Parameters.Add("@USUARIO", SqlDbType.Int).Value = IdUsuario;

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {

                    int bdorigen = 0, idservicio = 0;
                    string contenedor = "";

                    if (reader["BD_ORIGEN"] == DBNull.Value) bdorigen = 0;
                    else bdorigen = Convert.ToInt32(reader["BD_ORIGEN"]);

                    if (reader["ID_SERVICIO"] == DBNull.Value) idservicio = 0;
                    else idservicio = Convert.ToInt32(reader["ID_SERVICIO"]);

                    if (reader["CONTENEDOR"] == DBNull.Value) contenedor = "";
                    else contenedor = reader["CONTENEDOR"].ToString();

                    Servicios.Add(new Clases.Servicio
                    {
                        BDOrigen = bdorigen,
                        IdServicio = idservicio,
                        Contenedor = contenedor
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }

            return Servicios;
        }

        //INICIO VISTA HISTORICOS FILTROS
        public static List<Clases.Servicio> GetHistoricoServiciosFiltrados(int IdUsuario, Clases.DataFiltros filtroData, string fechaInicio, string fechaTermino)
        {

            List<Clases.Servicio> Servicios = new List<Clases.Servicio>();
            SqlConnection cnn;
            cnn = new SqlConnection(connectionAPIService);
            string modemflag = "", co2flag = "";

            int fechaInicioSemana = Convert.ToInt32(fechaInicio.Substring(6, 2));
            int fechaInicioAno = Convert.ToInt32(fechaInicio.Substring(0, 4));
            int fechaTerminoSemana = Convert.ToInt32(fechaTermino.Substring(6, 2));
            int fechaTerminoAno = Convert.ToInt32(fechaTermino.Substring(0, 4));

            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("EXEC dbo.ConsultarHistoricoServiciosFiltrosPorPerfiles @USUARIO, @MODEM_NULL, @CONTROLADOR_NULL, @MODEM_NOT_NULL, @CONTROLADOR_NOT_NULL, @FECHAINICIALANO, @FECHAINICIALSEMANA, @FECHATERMINOANO, @FECHATERMINOSEMANA;", cnn);
                //SqlCommand command = new SqlCommand("EXEC dbo.ConsultarHistoricoServiciosFiltros @USUARIO, @FECHAINICIALANO, @FECHAINICIALSEMANA, @FECHATERMINOANO, @FECHATERMINOSEMANA;", cnn);
                command.Parameters.AddWithValue("@USUARIO", IdUsuario);

                command.Parameters.Add("@MODEM_NULL", SqlDbType.Int).Value = filtroData.ModemNull;
                command.Parameters.Add("@CONTROLADOR_NULL", SqlDbType.Int).Value = filtroData.ControladorNull;
                command.Parameters.Add("@MODEM_NOT_NULL", SqlDbType.Int).Value = filtroData.ModemNotNull;
                command.Parameters.Add("@CONTROLADOR_NOT_NULL", SqlDbType.Int).Value = filtroData.ControladorNotNull;

                command.Parameters.AddWithValue("@FECHAINICIALANO", fechaInicioAno);
                command.Parameters.AddWithValue("@FECHAINICIALSEMANA", fechaInicioSemana);
                command.Parameters.AddWithValue("@FECHATERMINOANO", fechaTerminoAno);
                command.Parameters.AddWithValue("@FECHATERMINOSEMANA", fechaTerminoSemana);

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {

                    DateTime? etaPuerto = null, fechaControlador = null;

                    if (reader["ETAPUERTO"] != DBNull.Value)
                    {
                        etaPuerto = Convert.ToDateTime(reader["ETAPUERTO"]);
                    }
                    if (reader["FECHACONTROLADOR"] != DBNull.Value)
                    {
                        fechaControlador = Convert.ToDateTime(reader["FECHACONTROLADOR"]);
                    }

                    modemflag = reader["MODEM"].ToString();
                    co2flag = reader["SENSOR_CO2"].ToString();

                    if (modemflag == "SI")
                    {
                        co2flag = "SI";
                    }

                    Servicios.Add(new Clases.Servicio
                    {
                        BDOrigen = 1,
                        Contenedor = reader["CONTENEDOR"].ToString(),
                        Booking = reader["BOOKING"].ToString(),
                        Commodity = reader["COMMODITY"].ToString(),
                        Naviera = reader["NAVIERA"].ToString(),
                        Nave = reader["NAVE"].ToString(),
                        PuertoOrigen = reader["PUERTOORIGEN"].ToString(),
                        PuertoDestino = reader["PUERTODESTINO"].ToString(),
                        EtaPuerto = etaPuerto,
                        SetpointAC = reader["SETPOINT"].ToString(),
                        SetpointT = reader["TEMPERATURA"].ToString(),
                        Modem = reader["MODEM"].ToString(),
                        NumModem = reader["NUMMODEM"].ToString(),
                        CO2Ext = co2flag,
                        TemperaturaExt = reader["SENSOR_T"].ToString(),
                        AperturaPuerta = reader["SENSOR_LUZ"].ToString(),
                        //IdNaviera = reader["ID_NAVIERA"].ToString(),
                        //IdFreightForWarder = reader["ID_FREIGHTFORWARDER"].ToString(),
                        //IdExportador = reader["ID_EXPORTADOR"].ToString(),
                        IdServicio = Convert.ToInt32(reader["ID_SERVICIO"]),
                        IdControlador = reader["CONTROLADOR"].ToString(),
                        FechaControlador = fechaControlador,
                    });

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }

            return Servicios;
        }

        public static Clases.Totalizador GetContadoresHistoricoFiltrados(int IdUsuario, Clases.DataFiltros data, string fechaInicio, string fechaTermino)
        {
            Clases.Totalizador Totalizadores = (new Clases.Totalizador
            {
                ContadorModem = GetContadorModemHistoricoFechas(IdUsuario, data, fechaInicio, fechaTermino),
                ContadorControlador = GetContadorControladorHistoricoFechas(IdUsuario, data, fechaInicio, fechaTermino),
                ContadorCO2 = 0,
                ContadorTemperatura = 0,
                ContadorLuz = 0,
                ContadorTotal = GetContadorTotalHistoricoFechas(IdUsuario, data, fechaInicio, fechaTermino)
            });

            return Totalizadores;
        }

        public static int GetContadorModemHistoricoFechas(int IdUsuario, Clases.DataFiltros filtroData, string fechaInicio, string fechaTermino)
        {
            int contador = 0;

            SqlConnection cnn2;
            cnn2 = new SqlConnection(connectionAPIService);

            int fechaInicioSemana = Convert.ToInt32(fechaInicio.Substring(6, 2));
            int fechaInicioAno = Convert.ToInt32(fechaInicio.Substring(0, 4));
            int fechaTerminoSemana = Convert.ToInt32(fechaTermino.Substring(6, 2));
            int fechaTerminoAno = Convert.ToInt32(fechaTermino.Substring(0, 4));

            try
            {
                cnn2.Open();
                SqlCommand command2 = new SqlCommand("EXEC dbo.ConsultarContadorModemHistoricoSemanasPorPerfiles @USUARIO, @MODEM_NULL, @CONTROLADOR_NULL, @MODEM_NOT_NULL, @CONTROLADOR_NOT_NULL, @FECHAINICIALANO, @FECHAINICIALSEMANA, @FECHATERMINOANO, @FECHATERMINOSEMANA;", cnn2);
                command2.Parameters.AddWithValue("@USUARIO", IdUsuario);

                command2.Parameters.Add("@MODEM_NULL", SqlDbType.Int).Value = filtroData.ModemNull;
                command2.Parameters.Add("@CONTROLADOR_NULL", SqlDbType.Int).Value = filtroData.ControladorNull;
                command2.Parameters.Add("@MODEM_NOT_NULL", SqlDbType.Int).Value = filtroData.ModemNotNull;
                command2.Parameters.Add("@CONTROLADOR_NOT_NULL", SqlDbType.Int).Value = filtroData.ControladorNotNull;

                command2.Parameters.AddWithValue("@FECHAINICIALANO", fechaInicioAno);
                command2.Parameters.AddWithValue("@FECHAINICIALSEMANA", fechaInicioSemana);
                command2.Parameters.AddWithValue("@FECHATERMINOANO", fechaTerminoAno);
                command2.Parameters.AddWithValue("@FECHATERMINOSEMANA", fechaTerminoSemana);

                SqlDataReader reader2 = command2.ExecuteReader();
                while (reader2.Read())
                {
                    contador = Convert.ToInt32(reader2["CONTADOR_SERVICIOS"]);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn2.Close();
            }
            return contador;
        }

        public static int GetContadorControladorHistoricoFechas(int IdUsuario, Clases.DataFiltros filtroData, string fechaInicio, string fechaTermino)
        {
            int contador = 0;

            SqlConnection cnn2;
            cnn2 = new SqlConnection(connectionAPIService);

            int fechaInicioSemana = Convert.ToInt32(fechaInicio.Substring(6, 2));
            int fechaInicioAno = Convert.ToInt32(fechaInicio.Substring(0, 4));
            int fechaTerminoSemana = Convert.ToInt32(fechaTermino.Substring(6, 2));
            int fechaTerminoAno = Convert.ToInt32(fechaTermino.Substring(0, 4));

            try
            {
                cnn2.Open();
                SqlCommand command2 = new SqlCommand("EXEC dbo.ConsultarContadorControladorHistoricoSemanasPorPerfiles  @USUARIO, @MODEM_NULL, @CONTROLADOR_NULL, @MODEM_NOT_NULL, @CONTROLADOR_NOT_NULL , @FECHAINICIALANO, @FECHAINICIALSEMANA, @FECHATERMINOANO, @FECHATERMINOSEMANA;", cnn2);
                command2.Parameters.AddWithValue("@USUARIO", IdUsuario);

                command2.Parameters.Add("@MODEM_NULL", SqlDbType.Int).Value = filtroData.ModemNull;
                command2.Parameters.Add("@CONTROLADOR_NULL", SqlDbType.Int).Value = filtroData.ControladorNull;
                command2.Parameters.Add("@MODEM_NOT_NULL", SqlDbType.Int).Value = filtroData.ModemNotNull;
                command2.Parameters.Add("@CONTROLADOR_NOT_NULL", SqlDbType.Int).Value = filtroData.ControladorNotNull;

                command2.Parameters.AddWithValue("@FECHAINICIALANO", fechaInicioAno);
                command2.Parameters.AddWithValue("@FECHAINICIALSEMANA", fechaInicioSemana);
                command2.Parameters.AddWithValue("@FECHATERMINOANO", fechaTerminoAno);
                command2.Parameters.AddWithValue("@FECHATERMINOSEMANA", fechaTerminoSemana);

                SqlDataReader reader2 = command2.ExecuteReader();
                while (reader2.Read())
                {
                    contador = Convert.ToInt32(reader2["CONTADOR_SERVICIOS"]);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn2.Close();
            }
            return contador;
        }

        public static int GetContadorTotalHistoricoFechas(int IdUsuario, Clases.DataFiltros filtroData, string fechaInicio, string fechaTermino)
        {
            int contador = 0;

            SqlConnection cnn2;
            cnn2 = new SqlConnection(connectionAPIService);

            int fechaInicioSemana = Convert.ToInt32(fechaInicio.Substring(6, 2));
            int fechaInicioAno = Convert.ToInt32(fechaInicio.Substring(0, 4));
            int fechaTerminoSemana = Convert.ToInt32(fechaTermino.Substring(6, 2));
            int fechaTerminoAno = Convert.ToInt32(fechaTermino.Substring(0, 4));

            try
            {
                cnn2.Open();
                SqlCommand command2 = new SqlCommand("EXEC dbo.ConsultarContadorTotalHistoricoSemanasPorPerfiles  @USUARIO, @MODEM_NULL, @CONTROLADOR_NULL, @MODEM_NOT_NULL, @CONTROLADOR_NOT_NULL, @FECHAINICIALANO, @FECHAINICIALSEMANA, @FECHATERMINOANO, @FECHATERMINOSEMANA;", cnn2);
                command2.Parameters.AddWithValue("@USUARIO", IdUsuario);

                command2.Parameters.Add("@MODEM_NULL", SqlDbType.Int).Value = filtroData.ModemNull;
                command2.Parameters.Add("@CONTROLADOR_NULL", SqlDbType.Int).Value = filtroData.ControladorNull;
                command2.Parameters.Add("@MODEM_NOT_NULL", SqlDbType.Int).Value = filtroData.ModemNotNull;
                command2.Parameters.Add("@CONTROLADOR_NOT_NULL", SqlDbType.Int).Value = filtroData.ControladorNotNull;

                command2.Parameters.AddWithValue("@FECHAINICIALANO", fechaInicioAno);
                command2.Parameters.AddWithValue("@FECHAINICIALSEMANA", fechaInicioSemana);
                command2.Parameters.AddWithValue("@FECHATERMINOANO", fechaTerminoAno);
                command2.Parameters.AddWithValue("@FECHATERMINOSEMANA", fechaTerminoSemana);

                SqlDataReader reader2 = command2.ExecuteReader();
                while (reader2.Read())
                {
                    contador = Convert.ToInt32(reader2["CONTADOR_SERVICIOS"]);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn2.Close();
            }
            return contador;
        }

        //FIN VISTA HISTORICOS FILTROS

        public static Clases.DataFiltros GetPerfilDataByUser(int idPerfilData)
        {
            Clases.DataFiltros data = new Clases.DataFiltros();
            data.ModemNull = 0;
            data.ControladorNull = 0;
            data.ModemNotNull = 0;
            data.ControladorNotNull = 0;

            int val = 0;
            SqlConnection cnn;
            cnn = new SqlConnection(AlmaTest);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("ConsultarDataParaOcultar @PerfilData", cnn);
                command.Parameters.AddWithValue("@PerfilData", idPerfilData);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    val = Convert.ToInt32(reader["ID_TIPO_DATA"]);
                    //NombreTipoData = reader["NOMBRE_TIPO_DATA"].ToString()
                    if (val == 1)
                    {
                        data.ModemNull = 1;
                    }
                    else if (val == 2)
                    {
                        data.ControladorNull = 1;
                    }
                    else if (val == 3)
                    {
                        data.ModemNotNull = 1;
                    }
                    else if (val == 4)
                    {
                        data.ControladorNotNull = 1;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }
            return data;
        }

        public static List<Clases.Servicio> GetFechaInicioViaje(List<Clases.Servicio> Servicios)
        {
            MySqlConnection conec;
            conec = new MySqlConnection(connectionStringTecnica);

            Clases.Mapa ubicacion = new Clases.Mapa();
            List<Clases.Mapa> Mapas = new List<Clases.Mapa>();
            try
            {
                foreach (Clases.Servicio servicio in Servicios)
                {
                    conec.Open();
                    MySqlCommand comando = new MySqlCommand("SELECT CASE WHEN(SELECT s.travelStartDate FROM prometeo.logistic l INNER JOIN(SELECT id, travelStartDate FROM prometeo.service) s ON s.id = l.idService WHERE l.idController = (SELECT id FROM prometeo.controller WHERE esn = @ESN ORDER BY id DESC) AND s.travelStartDate IS NOT NULL AND s.travelStartDate BETWEEN(SELECT(DATE_ADD(@FECHA, INTERVAL - 30 DAY))) AND(SELECT(DATE_ADD(@FECHA, INTERVAL 30 DAY))) ORDER BY l.id DESC LIMIT 1; ) IS NULL THEN(SELECT s.travelStartDate FROM prometeo.historyLogistic l INNER JOIN(SELECT id, travelStartDate FROM prometeo.service) s ON s.id = l.idService WHERE l.idController = (SELECT id FROM prometeo.controller WHERE esn = @ESN ORDER BY id DESC) AND s.travelStartDate IS NOT NULL AND s.travelStartDate BETWEEN(SELECT(DATE_ADD(@FECHA, INTERVAL - 30 DAY))) AND(SELECT(DATE_ADD(@FECHA, INTERVAL 30 DAY))) ORDER BY l.id DESC LIMIT 1; ) ELSE(SELECT s.travelStartDate FROM prometeo.logistic l INNER JOIN(SELECT id, travelStartDate FROM prometeo.service) s ON s.id = l.idService WHERE l.idController = (SELECT id FROM prometeo.controller WHERE esn = @ESN ORDER BY id DESC) AND s.travelStartDate IS NOT NULL AND s.travelStartDate BETWEEN(SELECT(DATE_ADD(@FECHA, INTERVAL - 30 DAY))) AND(SELECT(DATE_ADD(@FECHA, INTERVAL 30 DAY))) ORDER BY l.id DESC LIMIT 1;) END AS travelStartDate;", conec);
                    comando.Parameters.Add("@ESN", MySqlDbType.VarChar, 50).Value = servicio.IdControlador;
                    comando.Parameters.Add("@FECHA", MySqlDbType.DateTime).Value = servicio.FechaControlador;
                    comando.CommandTimeout = 10;
                    MySqlDataReader lectura = comando.ExecuteReader();

                    while (lectura.Read())
                    {
                        if ((lectura["travelStartDate"] != DBNull.Value) && (Convert.ToDateTime(lectura["travelStartDate"]) < servicio.FechaControlador))
                        {
                            servicio.FechaControlador = Convert.ToDateTime(lectura["travelStartDate"]);
                        }
                    }

                    conec.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conec.Close();
            }

            return Servicios;
        }


        public static Clases.Servicio GetFechaInicioViajeUnServicio(Clases.Servicio Servicio)
        {
            MySqlConnection conec;
            conec = new MySqlConnection(connectionStringTecnica);
            try
            {
                conec.Open();
                MySqlCommand comando = new MySqlCommand("SELECT CASE WHEN(SELECT s.travelStartDate FROM prometeo.logistic l INNER JOIN(SELECT id, travelStartDate FROM prometeo.service) s ON s.id = l.idService WHERE l.idController = (SELECT id FROM prometeo.controller WHERE esn = @ESN ORDER BY id DESC) AND s.travelStartDate IS NOT NULL AND s.travelStartDate BETWEEN(SELECT(DATE_ADD(@FECHA, INTERVAL - 30 DAY))) AND(SELECT(DATE_ADD(@FECHA, INTERVAL 30 DAY))) ORDER BY l.id DESC LIMIT 1; ) IS NULL THEN(SELECT s.travelStartDate FROM prometeo.historyLogistic l INNER JOIN(SELECT id, travelStartDate FROM prometeo.service) s ON s.id = l.idService WHERE l.idController = (SELECT id FROM prometeo.controller WHERE esn = @ESN ORDER BY id DESC) AND s.travelStartDate IS NOT NULL AND s.travelStartDate BETWEEN(SELECT(DATE_ADD(@FECHA, INTERVAL - 30 DAY))) AND(SELECT(DATE_ADD(@FECHA, INTERVAL 30 DAY))) ORDER BY l.id DESC LIMIT 1; ) ELSE(SELECT s.travelStartDate FROM prometeo.logistic l INNER JOIN(SELECT id, travelStartDate FROM prometeo.service) s ON s.id = l.idService WHERE l.idController = (SELECT id FROM prometeo.controller WHERE esn = @ESN ORDER BY id DESC) AND s.travelStartDate IS NOT NULL AND s.travelStartDate BETWEEN(SELECT(DATE_ADD(@FECHA, INTERVAL - 30 DAY))) AND(SELECT(DATE_ADD(@FECHA, INTERVAL 30 DAY))) ORDER BY l.id DESC LIMIT 1;) END AS travelStartDate;", conec);
                comando.Parameters.Add("@ESN", MySqlDbType.VarChar, 50).Value = Servicio.IdControlador;
                comando.Parameters.Add("@FECHA", MySqlDbType.DateTime).Value = Servicio.FechaControlador;
                comando.CommandTimeout = 10;
                MySqlDataReader lectura = comando.ExecuteReader();

                while (lectura.Read())
                {
                    if ((lectura["travelStartDate"] != DBNull.Value) && (Convert.ToDateTime(lectura["travelStartDate"]) < Servicio.FechaControlador))
                    {
                        Servicio.FechaControlador = Convert.ToDateTime(lectura["travelStartDate"]);
                    }
                }

                conec.Close();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conec.Close();
            }

            return Servicio;
        }

        public static List<Clases.Seccion> ValidoServicio(int IdServicio, int BDorigen)
        {
            List<Clases.Seccion> Secciones = new List<Clases.Seccion>();
            SqlConnection cnn;
            cnn = new SqlConnection(AlmaTest);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("EXEC dbo.ValidarDetalleServicio @IDSERVICIO, @BD", cnn);
                command.Parameters.Add("@IDSERVICIO", SqlDbType.Int).Value = IdServicio;
                command.Parameters.Add("@BD", SqlDbType.Int).Value = BDorigen;

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Secciones.Add(new Clases.Seccion
                    {
                        IdSeccion = Convert.ToInt32(reader["ID_SECCION"]),
                        NombreSeccion = reader["NOMBRE_SECCION"].ToString(),
                        ClaseSeccion = reader["CLASE_SECCION"].ToString(),
                        Estado = Convert.ToInt32(reader["ESTADO"])
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }
            return Secciones;
        }

        public static List<Clases.Mediciones> ObtenerMedicionesSensoresTemp(int IdServicio = 0, int BDOrigen = 0)
        {
            List<Clases.Mediciones> mediciones = new List<Clases.Mediciones>();
            SqlConnection cnn;
            cnn = new SqlConnection(AlmaTest);

            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("EXEC dbo.ConsultarMedicionesSensoresTemp @ID_SERVICIO, @ID_BD_ORIGEN", cnn);
                command.Parameters.Add("@ID_SERVICIO", SqlDbType.Int).Value = IdServicio;
                command.Parameters.Add("@ID_BD_ORIGEN", SqlDbType.Int).Value = BDOrigen;
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    mediciones.Add(new Clases.Mediciones
                    {
                        ValorMedido = float.Parse(reader["VALOR_MEDIDO"].ToString()),
                        FechaMedicion = Convert.ToDateTime(reader["FECHA_MEDICION"].ToString()),
                        ID_REL_SENSORSERVICIO = Convert.ToInt32(reader["ID_REL_SENSORSERVICIO"])
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }

            return mediciones;
        }

        public static List<Clases.Mediciones> ObtenerMedicionesSensoresCO2(int IdServicio = 0, int BDOrigen = 0)
        {
            List<Clases.Mediciones> mediciones = new List<Clases.Mediciones>();

            SqlConnection cnn;
            cnn = new SqlConnection(AlmaTest);

            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("EXEC dbo.ConsultarMedicionesSensoresCO2_Intranet @ID_SERVICIO, @ID_BD_ORIGEN", cnn);
                command.Parameters.Add("@ID_SERVICIO", SqlDbType.Int).Value = IdServicio;
                command.Parameters.Add("@ID_BD_ORIGEN", SqlDbType.Int).Value = BDOrigen;
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    mediciones.Add(new Clases.Mediciones
                    {
                        ValorMedido = float.Parse(reader["VALOR_MEDIDO"].ToString()),
                        FechaMedicion = Convert.ToDateTime(reader["FECHA_MEDICION"].ToString()),
                        ID_REL_SENSORSERVICIO = Convert.ToInt32(reader["ID_REL_SENSORSERVICIO"]),
                        ID_SERVICEDATA = Convert.ToInt32(reader["ID_SERVICEDATA"].ToString())
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }

            return mediciones;
        }


        public static List<Clases.Mediciones> ObtenerMedicionesSensoresLuz(int IdServicio = 0, int BDOrigen = 0)
        {
            List<Clases.Mediciones> mediciones = new List<Clases.Mediciones>();

            SqlConnection cnn;
            cnn = new SqlConnection(AlmaTest);

            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("EXEC dbo.ConsultarMedicionesSensoresLuz @ID_SERVICIO, @ID_BD_ORIGEN", cnn);
                command.Parameters.Add("@ID_SERVICIO", SqlDbType.Int).Value = IdServicio;
                command.Parameters.Add("@ID_BD_ORIGEN", SqlDbType.Int).Value = BDOrigen;

                int apertura = 0;
                DateTime? fecha = null;

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    if (reader["APERTURO_PUERTAS"] != DBNull.Value)
                    {
                        apertura = Convert.ToInt32(reader["APERTURO_PUERTAS"].ToString());
                    }

                    if (reader["FECHA_MEDICION"] != DBNull.Value)
                    {
                        fecha = Convert.ToDateTime(reader["FECHA_MEDICION"].ToString());
                    }

                    mediciones.Add(new Clases.Mediciones
                    {

                        AperturoPuertas = Convert.ToInt32(reader["APERTURO_PUERTAS"]),
                        FechaMedicion = fecha,
                        ID_REL_SENSORSERVICIO = Convert.ToInt32(reader["ID_REL_SENSORSERVICIO"])
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }

            return mediciones;
        }

        public static ServicioNuevo NuevoServicioAlma(ServicioNuevo Servicio)
        {
            ServicioNuevo servicio_respuesta = new ServicioNuevo();

            SqlConnection cnn2;
            cnn2 = new SqlConnection(AlmaTest);
            try
            {
                cnn2.Open();
                //SqlCommand command2 = new SqlCommand("EXEC IngresarServicioAlma @usuario, @sensores, @numero_serie, @contenedor, @direccion_completa_origen, @alias_origen, @pais_origen, @region_origen, @ciudad_origen, @comuna_origen, @calle_origen, @numero_calle_origen, @codigo_postal_origen, @latitud_origen, @longitud_origen, @direccion_completa_destino, @alias_destino, @pais_destino, @region_destino, @ciudad_destino, @comuna_destino, @calle_destino, @numero_calle_destino, @codigo_postal_destino, @latitud_destino, @longitud_destino, @etd, @eta, @booking, @nave_id, @commodity, @naviera_id, @setpoint_co2_id, @setpoint_temp_id;", cnn2);
                SqlCommand command2 = new SqlCommand("EXEC IngresarServicioAlma @id_destinatario, @usuario, @contenedor, @alias_origen, @alias_destino, @etd, @eta, @booking, @nave, @commodity, @naviera, @setpoint_co2_id, @setpoint_temp_id, @descripcion, @num_modem;", cnn2);
                if (Servicio.id_destinatario == null) Servicio.id_destinatario = 0;
                command2.Parameters.Add("@id_destinatario", SqlDbType.Int).Value = Servicio.id_destinatario;
                command2.Parameters.Add("@usuario", SqlDbType.Int).Value = Servicio.usuario;
                //command2.Parameters.Add("@sensores", SqlDbType.Int).Value = Servicio.sensores;
                //command2.Parameters.Add("@numero_serie", SqlDbType.VarChar).Value = Servicio.numero_serie;
                command2.Parameters.Add("@contenedor", SqlDbType.VarChar).Value = Servicio.contenedor;
                //command2.Parameters.Add("@direccion_completa_origen", SqlDbType.VarChar).Value = Servicio.lugar_origen;
                command2.Parameters.Add("@alias_origen", SqlDbType.VarChar).Value = Servicio.lugar_origen;
                //command2.Parameters.Add("@pais_origen", SqlDbType.VarChar).Value = Servicio.pais_origen;
                //command2.Parameters.Add("@region_origen", SqlDbType.VarChar).Value = Servicio.region_origen;
                //command2.Parameters.Add("@ciudad_origen", SqlDbType.VarChar).Value = Servicio.ciudad_origen;
                //command2.Parameters.Add("@comuna_origen", SqlDbType.VarChar).Value = Servicio.comuna_origen;
                //command2.Parameters.Add("@calle_origen", SqlDbType.VarChar).Value = Servicio.calle_origen;
                //command2.Parameters.Add("@numero_calle_origen", SqlDbType.VarChar).Value = Servicio.numero_calle_origen;
                //command2.Parameters.Add("@codigo_postal_origen", SqlDbType.VarChar).Value = Servicio.codigo_postal_origen;
                //command2.Parameters.Add("@latitud_origen", SqlDbType.VarChar).Value = Servicio.latitud_origen;
                //command2.Parameters.Add("@longitud_origen", SqlDbType.VarChar).Value = Servicio.longitud_origen;
                //command2.Parameters.Add("@direccion_completa_destino", SqlDbType.VarChar).Value = Servicio.lugar_destino;
                command2.Parameters.Add("@alias_destino", SqlDbType.VarChar).Value = Servicio.lugar_destino;
                //command2.Parameters.Add("@pais_destino", SqlDbType.VarChar).Value = Servicio.pais_destino;
                //command2.Parameters.Add("@region_destino", SqlDbType.VarChar).Value = Servicio.region_destino;
                //command2.Parameters.Add("@ciudad_destino", SqlDbType.VarChar).Value = Servicio.ciudad_destino;
                //command2.Parameters.Add("@comuna_destino", SqlDbType.VarChar).Value = Servicio.comuna_destino;
                //command2.Parameters.Add("@calle_destino", SqlDbType.VarChar).Value = Servicio.calle_destino;
                //command2.Parameters.Add("@numero_calle_destino", SqlDbType.VarChar).Value = Servicio.numero_calle_destino;
                //command2.Parameters.Add("@codigo_postal_destino", SqlDbType.VarChar).Value = Servicio.codigo_postal_destino;
                //command2.Parameters.Add("@latitud_destino", SqlDbType.VarChar).Value = Servicio.latitud_destino;
                //command2.Parameters.Add("@longitud_destino", SqlDbType.VarChar).Value = Servicio.longitud_destino;
                if (Servicio.etd == null)
                {
                    command2.Parameters.Add("@etd", SqlDbType.DateTime).Value = System.Data.SqlTypes.SqlDateTime.Null;
                }
                else
                {
                    command2.Parameters.Add("@etd", SqlDbType.DateTime).Value = Servicio.etd;
                }

                if (Servicio.eta == null)
                {
                    command2.Parameters.Add("@eta", SqlDbType.DateTime).Value = System.Data.SqlTypes.SqlDateTime.Null;
                }
                else
                {
                    command2.Parameters.Add("@eta", SqlDbType.DateTime).Value = Servicio.eta;
                }

                command2.Parameters.Add("@booking", SqlDbType.VarChar).Value = Servicio.booking;
                if (Servicio.nave == null) Servicio.nave = "";
                command2.Parameters.Add("@nave", SqlDbType.VarChar).Value = Servicio.nave;
                if (Servicio.commodity == null) Servicio.commodity = "0";
                command2.Parameters.Add("@commodity", SqlDbType.VarChar).Value = Servicio.commodity;
                if (Servicio.naviera == null) Servicio.naviera = "";
                command2.Parameters.Add("@naviera", SqlDbType.VarChar).Value = Servicio.naviera;
                command2.Parameters.Add("@setpoint_co2_id", SqlDbType.Int).Value = Servicio.id_setpoint_co2;
                command2.Parameters.Add("@setpoint_temp_id", SqlDbType.Int).Value = Servicio.id_setpoint_temp;
                if (Servicio.descripcion == null) Servicio.descripcion = "";
                command2.Parameters.Add("@descripcion", SqlDbType.VarChar).Value = Servicio.descripcion;
                command2.Parameters.Add("@num_modem", SqlDbType.VarChar).Value = Servicio.NumModem;

                SqlDataReader reader2 = command2.ExecuteReader();
                while (reader2.Read())
                {
                    servicio_respuesta.id = Convert.ToInt32(reader2["ID_SERVICIO"]);
                    servicio_respuesta.id_bd = Convert.ToInt32(reader2["ID_BD"]);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn2.Close();
            }
            return servicio_respuesta;
        }

        public static ServicioNuevo EditarNuevoServicioAlma(ServicioNuevo Servicio)
        {
            ServicioNuevo servicio_respuesta = new ServicioNuevo();

            SqlConnection cnn2;
            cnn2 = new SqlConnection(AlmaTest);
            try
            {
                cnn2.Open();
                //SqlCommand command2 = new SqlCommand("EXEC IngresarServicioAlma @usuario, @sensores, @numero_serie, @contenedor, @direccion_completa_origen, @alias_origen, @pais_origen, @region_origen, @ciudad_origen, @comuna_origen, @calle_origen, @numero_calle_origen, @codigo_postal_origen, @latitud_origen, @longitud_origen, @direccion_completa_destino, @alias_destino, @pais_destino, @region_destino, @ciudad_destino, @comuna_destino, @calle_destino, @numero_calle_destino, @codigo_postal_destino, @latitud_destino, @longitud_destino, @etd, @eta, @booking, @nave_id, @commodity, @naviera_id, @setpoint_co2_id, @setpoint_temp_id;", cnn2);
                SqlCommand command2 = new SqlCommand("EXEC EditarNuevoServicioAlma @id_destinatario, @usuario, @contenedor, @alias_origen, @alias_destino, @etd, @eta, @booking, @nave, @commodity, @naviera, @setpoint_co2_id, @setpoint_temp_id, @descripcion, @num_modem;", cnn2);
                if (Servicio.id_destinatario == null || Servicio.id_destinatario==0) Servicio.id_destinatario = 0;
                command2.Parameters.Add("@id_destinatario", SqlDbType.Int).Value = Servicio.id_destinatario;
                command2.Parameters.Add("@usuario", SqlDbType.Int).Value = Servicio.usuario;
                command2.Parameters.Add("@contenedor", SqlDbType.VarChar).Value = Servicio.contenedor;
                command2.Parameters.Add("@alias_origen", SqlDbType.VarChar).Value = Servicio.lugar_origen;
                command2.Parameters.Add("@alias_destino", SqlDbType.VarChar).Value = Servicio.lugar_destino;
                if (Servicio.etd == null)
                {
                    command2.Parameters.Add("@etd", SqlDbType.DateTime).Value = System.Data.SqlTypes.SqlDateTime.Null;
                }
                else
                {
                    command2.Parameters.Add("@etd", SqlDbType.DateTime).Value = Servicio.etd;
                }

                if (Servicio.eta == null)
                {
                    command2.Parameters.Add("@eta", SqlDbType.DateTime).Value = System.Data.SqlTypes.SqlDateTime.Null;
                }
                else
                {
                    command2.Parameters.Add("@eta", SqlDbType.DateTime).Value = Servicio.eta;
                }

                command2.Parameters.Add("@booking", SqlDbType.VarChar).Value = Servicio.booking;
                if (Servicio.nave == null) Servicio.nave = "";
                command2.Parameters.Add("@nave", SqlDbType.VarChar).Value = Servicio.nave;
                if (Servicio.commodity == null) Servicio.commodity = "0";
                command2.Parameters.Add("@commodity", SqlDbType.VarChar).Value = Servicio.commodity;
                if (Servicio.naviera == null) Servicio.naviera = "";
                command2.Parameters.Add("@naviera", SqlDbType.VarChar).Value = Servicio.naviera;
                command2.Parameters.Add("@setpoint_co2_id", SqlDbType.Float).Value = Servicio.id_setpoint_co2;
                command2.Parameters.Add("@setpoint_temp_id", SqlDbType.Float).Value = Servicio.id_setpoint_temp;
                command2.Parameters.Add("@descripcion", SqlDbType.VarChar).Value = Servicio.descripcion;
                command2.Parameters.Add("@num_modem", SqlDbType.VarChar).Value = Servicio.NumModem;

                SqlDataReader reader2 = command2.ExecuteReader();
                while (reader2.Read())
                {
                    servicio_respuesta.id = Convert.ToInt32(reader2["ID_SERVICIO"]);
                    servicio_respuesta.id_bd = Convert.ToInt32(reader2["ID_BD"]);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn2.Close();
            }
            return servicio_respuesta;
        }


        public static ServicioNuevo EditarServicioAlma(ServicioNuevo Servicio)
        {
            ServicioNuevo servicio_respuesta = new ServicioNuevo();

            SqlConnection cnn2;
            cnn2 = new SqlConnection(AlmaTest);
            try
            {
                cnn2.Open();
                SqlCommand command2 = new SqlCommand("EXEC EditarServicioAlma @usuario, @id_servicio, @direccion_completa_origen, @alias_origen, @pais_origen, @region_origen, @ciudad_origen, @comuna_origen, @calle_origen, @numero_calle_origen, @codigo_postal_origen, @latitud_origen, @longitud_origen, @direccion_completa_destino, @alias_destino, @pais_destino, @region_destino, @ciudad_destino, @comuna_destino, @calle_destino, @numero_calle_destino, @codigo_postal_destino, @latitud_destino, @longitud_destino, @etd, @eta, @booking, @nave_id, @commodity, @naviera_id, @setpoint_co2_id, @setpoint_temp_id;", cnn2);
                command2.Parameters.Add("@usuario", SqlDbType.Int).Value = Servicio.usuario;
                command2.Parameters.Add("@id_servicio", SqlDbType.Int).Value = Servicio.id;
                command2.Parameters.Add("@direccion_completa_origen", SqlDbType.VarChar).Value = Servicio.lugar_origen;
                command2.Parameters.Add("@alias_origen", SqlDbType.VarChar).Value = Servicio.nombre_lugar_origen;
                command2.Parameters.Add("@pais_origen", SqlDbType.VarChar).Value = Servicio.pais_origen;
                command2.Parameters.Add("@region_origen", SqlDbType.VarChar).Value = Servicio.region_origen;
                command2.Parameters.Add("@ciudad_origen", SqlDbType.VarChar).Value = Servicio.ciudad_origen;
                command2.Parameters.Add("@comuna_origen", SqlDbType.VarChar).Value = Servicio.comuna_origen;
                command2.Parameters.Add("@calle_origen", SqlDbType.VarChar).Value = Servicio.calle_origen;
                command2.Parameters.Add("@numero_calle_origen", SqlDbType.VarChar).Value = Servicio.numero_calle_origen;
                command2.Parameters.Add("@codigo_postal_origen", SqlDbType.VarChar).Value = Servicio.codigo_postal_origen;
                command2.Parameters.Add("@latitud_origen", SqlDbType.VarChar).Value = Servicio.latitud_origen;
                command2.Parameters.Add("@longitud_origen", SqlDbType.VarChar).Value = Servicio.longitud_origen;
                command2.Parameters.Add("@direccion_completa_destino", SqlDbType.VarChar).Value = Servicio.lugar_destino;
                command2.Parameters.Add("@alias_destino", SqlDbType.VarChar).Value = Servicio.nombre_lugar_destino;
                command2.Parameters.Add("@pais_destino", SqlDbType.VarChar).Value = Servicio.pais_destino;
                command2.Parameters.Add("@region_destino", SqlDbType.VarChar).Value = Servicio.region_destino;
                command2.Parameters.Add("@ciudad_destino", SqlDbType.VarChar).Value = Servicio.ciudad_destino;
                command2.Parameters.Add("@comuna_destino", SqlDbType.VarChar).Value = Servicio.comuna_destino;
                command2.Parameters.Add("@calle_destino", SqlDbType.VarChar).Value = Servicio.calle_destino;
                command2.Parameters.Add("@numero_calle_destino", SqlDbType.VarChar).Value = Servicio.numero_calle_destino;
                command2.Parameters.Add("@codigo_postal_destino", SqlDbType.VarChar).Value = Servicio.codigo_postal_destino;
                command2.Parameters.Add("@latitud_destino", SqlDbType.VarChar).Value = Servicio.latitud_destino;
                command2.Parameters.Add("@longitud_destino", SqlDbType.VarChar).Value = Servicio.longitud_destino;
                if (Servicio.etd == null)
                {
                    command2.Parameters.Add("@etd", SqlDbType.DateTime).Value = System.Data.SqlTypes.SqlDateTime.Null;
                }
                else
                {
                    command2.Parameters.Add("@etd", SqlDbType.DateTime).Value = Servicio.etd;
                }

                if (Servicio.eta == null)
                {
                    command2.Parameters.Add("@eta", SqlDbType.DateTime).Value = System.Data.SqlTypes.SqlDateTime.Null;
                }
                else
                {
                    command2.Parameters.Add("@eta", SqlDbType.DateTime).Value = Servicio.eta;
                }

                command2.Parameters.Add("@booking", SqlDbType.VarChar).Value = Servicio.booking;
                command2.Parameters.Add("@nave_id", SqlDbType.Int).Value = Servicio.id_nave;
                if (Servicio.commodity == null) Servicio.commodity = "";
                command2.Parameters.Add("@commodity", SqlDbType.VarChar).Value = Servicio.commodity;
                command2.Parameters.Add("@naviera_id", SqlDbType.Int).Value = Servicio.id_naviera;
                command2.Parameters.Add("@setpoint_co2_id", SqlDbType.Int).Value = Servicio.id_setpoint_co2;
                command2.Parameters.Add("@setpoint_temp_id", SqlDbType.Int).Value = Servicio.id_setpoint_temp;
                SqlDataReader reader2 = command2.ExecuteReader();
                while (reader2.Read())
                {
                    servicio_respuesta.id = Convert.ToInt32(reader2["RESPUESTA"]);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn2.Close();
            }
            return servicio_respuesta;
        }

        public static ServicioNuevo ObtenerDatosServicio(int IdServicio, int IdBDServicio)
        {
            ServicioNuevo servicio = new ServicioNuevo();
            servicio.id = 0;

            SqlConnection cnn2;
            cnn2 = new SqlConnection(AlmaTest);
            try
            {
                cnn2.Open();
                SqlCommand command2 = new SqlCommand("EXEC ConsultarServicioDeContenedor @ID_SERVICIO, @ID_BD_SERVICIO;", cnn2);

                command2.Parameters.Add("@ID_SERVICIO", SqlDbType.Int).Value = IdServicio;
                command2.Parameters.Add("@ID_BD_SERVICIO", SqlDbType.Int).Value = IdBDServicio;
                SqlDataReader reader2 = command2.ExecuteReader();
                while (reader2.Read())
                {
                    servicio.id = Convert.ToInt32(reader2["ID_SERVICIO"]);
                    servicio.lugar_origen = reader2["ORIGEN"].ToString();
                    servicio.lugar_destino = reader2["DESTINO"].ToString();
                    if (reader2["ETD"] == DBNull.Value)
                    {
                        servicio.etd = null;
                        servicio.etd_string = "";
                    }
                    else
                    {
                        servicio.etd = Convert.ToDateTime(reader2["ETD"]);
                        servicio.etd_string = reader2["ETD"].ToString().Substring(0, 10);
                    }

                    if (reader2["ETAPUERTO"] == DBNull.Value)
                    {
                        servicio.eta = null;
                        servicio.eta_string = "";
                    }
                    else
                    {
                        servicio.eta = Convert.ToDateTime(reader2["ETAPUERTO"]);
                        servicio.eta_string = reader2["ETAPUERTO"].ToString().Substring(0, 10);
                    }



                    servicio.booking = reader2["BOOKING"].ToString();
                    //servicio.id_nave = Convert.ToInt32(reader2["ID_NAVE1"]);
                    //servicio.id_naviera = Convert.ToInt32(reader2["ID_NAVIERA"]);
                    servicio.nave = reader2["NAVE"].ToString();
                    servicio.naviera = reader2["NAVIERA"].ToString();
                    servicio.commodity = reader2["COMMODITY"].ToString();
                    servicio.setpoint_co2 = reader2["SETPOINT_CO2"].ToString();
                    servicio.setpoint_temp = reader2["SETPOINT_TEMP"].ToString();
                    //servicio.tipo_sensor1 = reader2["TIPO_SENSOR1"].ToString();
                    //servicio.tipo_sensor2 = reader2["TIPO_SENSOR2"].ToString();
                    //servicio.tipo_sensor3 = reader2["TIPO_SENSOR3"].ToString();
                    //servicio.tipo_sensor4 = reader2["TIPO_SENSOR4"].ToString();
                    //servicio.tipo_sensor5 = reader2["TIPO_SENSOR5"].ToString();
                    //servicio.numero_sensor1 = reader2["NUMERO_SENSOR1"].ToString();
                    //servicio.numero_sensor2 = reader2["NUMERO_SENSOR2"].ToString();
                    //servicio.numero_sensor3 = reader2["NUMERO_SENSOR3"].ToString();
                    //servicio.numero_sensor4 = reader2["NUMERO_SENSOR4"].ToString();
                    //servicio.numero_sensor5 = reader2["NUMERO_SENSOR5"].ToString();
                    servicio.descripcion = reader2["DESCRIPCION"].ToString();
                    servicio.contenedor= reader2["CONTENEDOR"].ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn2.Close();
            }
            return servicio;
        }

        public static List<Sensor> ObtenerSensoresServicio(int IdServicio, int IdBDServicio)
        {
            List<Sensor> sensores = new List<Sensor>();
            Sensor servicio = new Sensor();

            SqlConnection cnn2;
            cnn2 = new SqlConnection(AlmaTest);
            try
            {
                cnn2.Open();
                SqlCommand command2 = new SqlCommand("EXEC dbo.ConsultarSensoresEmersonPorIdServicio @ID_SERVICIO, @ID_BD_SERVICIO;", cnn2);
                command2.Parameters.Add("@ID_SERVICIO", SqlDbType.VarChar).Value = IdServicio;
                command2.Parameters.Add("@ID_BD_SERVICIO", SqlDbType.VarChar).Value = IdBDServicio;
                //command2.Parameters.Add("@ID_BD_SERVICIO", SqlDbType.VarChar).Value = IdBDServicio;
                SqlDataReader reader2 = command2.ExecuteReader();
                DateTime? fechaProg = null;
                while (reader2.Read())
                {
                    if(reader2["SENSOR"] != DBNull.Value && reader2["SENSOR"].ToString() != "") {
                        if (reader2["FECHA_PROGRAMACION"] != DBNull.Value)
                        {
                            fechaProg = Convert.ToDateTime(reader2["FECHA_PROGRAMACION"]);
                        }
                        sensores.Add(new Clases.Sensor
                        {
                            tipo_sensor = reader2["TIPO_SENSOR"].ToString(),
                            id_tipo_sensor = Convert.ToInt32(reader2["ID_TIPO_SENSOR"]),
                            numSerie = reader2["SENSOR"].ToString(),
                            recibioDatos = reader2["RECIBIO_DATOS"].ToString(),
                            fechaProgramacion = fechaProg,
                            proveedor = reader2["PROVEEDOR"].ToString(),
                            usuarioProgramador = reader2["NOMBRE_USUARIO"].ToString(),
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn2.Close();
            }
            return sensores;
        }

        public static List<Medicion> ObtenerTipoMedicionPorTipoSensor(int tipo_sensor)
        {
            List<Medicion> lista_mediciones = new List<Medicion>();
            Medicion medicion = new Medicion();

            SqlConnection cnn2;
            cnn2 = new SqlConnection(AlmaTest);
            try
            {
                cnn2.Open();
                SqlCommand command2 = new SqlCommand("EXEC dbo.ConsultarTipoMedicionPorTipoSensor @TIPO;", cnn2);
                command2.Parameters.Add("@TIPO", SqlDbType.Int).Value = tipo_sensor;
                SqlDataReader reader2 = command2.ExecuteReader();
                while (reader2.Read())
                {
                    if (reader2["NOMBRE_MEDICION"] != DBNull.Value && reader2["NOMBRE_MEDICION"].ToString() != "")
                    {
                        lista_mediciones.Add(new Clases.Medicion
                        {
                            id_tipo_medicion = Convert.ToInt32(reader2["ID_TIPO_MEDICION"]),
                            nombre_medicion = reader2["NOMBRE_MEDICION"].ToString(),
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn2.Close();
            }
            return lista_mediciones;
        }

        public static List<Sensor> ObtenerSensoresServicioPorContenedor(string Contenedor)
        {
            List<Sensor> sensores = new List<Sensor>();
            Sensor servicio = new Sensor();

            SqlConnection cnn2;
            cnn2 = new SqlConnection(AlmaTest);
            try
            {
                cnn2.Open();
                SqlCommand command2 = new SqlCommand("EXEC dbo.ConsultarSensoresEmersonPorContenedor @CONTENEDOR;", cnn2);
                command2.Parameters.Add("@CONTENEDOR", SqlDbType.VarChar).Value = Contenedor;
                //command2.Parameters.Add("@ID_BD_SERVICIO", SqlDbType.VarChar).Value = IdBDServicio;
                SqlDataReader reader2 = command2.ExecuteReader();
                while (reader2.Read())
                {
                    sensores.Add(new Clases.Sensor
                    {
                        //tipo_sensor = reader2["PROVEEDOR"].ToString()+" - "+reader2["TIPO_SENSOR"].ToString(),
                        tipo_sensor = reader2["TIPO_SENSOR"].ToString(),
                        id_tipo_sensor = Convert.ToInt32(reader2["ID_TIPO_SENSOR"]),
                        numSerie = reader2["SENSOR"].ToString()
                    });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn2.Close();
            }
            return sensores;
        }

        public static ServicioNuevo ObtenerDatosServicioPorContenedor(string Contenedor)
        {
            ServicioNuevo servicio = new ServicioNuevo();
            servicio.id = 0;
            Contenedor = Contenedor.Replace("-", "");

            SqlConnection cnn2;
            cnn2 = new SqlConnection(AlmaTest);
            try
            {
                cnn2.Open();
                SqlCommand command2 = new SqlCommand("EXEC dbo.ConsultarServicioPorContenedor @CONTENEDOR;", cnn2);
                command2.Parameters.Add("@CONTENEDOR", SqlDbType.VarChar).Value = Contenedor;
                SqlDataReader reader2 = command2.ExecuteReader();
                while (reader2.Read())
                {
                    servicio.id = Convert.ToInt32(reader2["ID_SERVICIO"]);
                    servicio.id_bd = Convert.ToInt32(reader2["ID_BD_SERVICIO"]);
                    servicio.lugar_origen = reader2["ORIGEN"].ToString();
                    servicio.lugar_destino = reader2["DESTINO"].ToString();
                    if (reader2["ETD"] == DBNull.Value)
                    {
                        servicio.etd = null;
                        servicio.etd_string = "";
                    }
                    else
                    {
                        servicio.etd = Convert.ToDateTime(reader2["ETD"]);
                        servicio.etd_string = reader2["ETD"].ToString().Substring(0, 10);
                    }

                    if (reader2["ETAPUERTO"] == DBNull.Value)
                    {
                        servicio.eta = null;
                        servicio.eta_string = "";
                    }
                    else
                    {
                        servicio.eta = Convert.ToDateTime(reader2["ETAPUERTO"]);
                        servicio.eta_string = reader2["ETAPUERTO"].ToString().Substring(0, 10);
                    }

                    servicio.booking = reader2["BOOKING"].ToString();
                    servicio.nave = reader2["NAVE"].ToString();
                    servicio.naviera = reader2["NAVIERA"].ToString();
                    servicio.commodity = reader2["COMMODITY"].ToString();
                    servicio.setpoint_co2 = reader2["SETPOINT_CO2"].ToString();
                    servicio.setpoint_temp = reader2["SETPOINT_TEMP"].ToString();
                    servicio.id_setpoint_co2 = float.Parse(reader2["SETPOINT_CO2"].ToString());
                    servicio.id_setpoint_temp = float.Parse(reader2["SETPOINT_TEMP"].ToString());
                    servicio.descripcion = reader2["DESCRIPCION"].ToString();
                    servicio.contenedor = reader2["CONTENEDOR"].ToString();
                    servicio.NumModem = reader2["NUMMODEM"].ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn2.Close();
            }
            return servicio;
        }        

        public static List<Clases.Servicio> GetSensoresProgramados(int IdUsuario, Clases.DataFiltros filtroData)
        {

            List<Clases.Servicio> Servicios = new List<Clases.Servicio>();
            SqlConnection cnn;
            cnn = new SqlConnection(AlmaTest);
            string modemflag = "", co2flag = "";

            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("EXEC dbo.ConsultarSensoresProgramados @USUARIO", cnn);
                //SqlCommand command = new SqlCommand("EXEC dbo.ConsultarServiciosUser @USUARIO;", cnn);
                command.Parameters.Add("@USUARIO", SqlDbType.Int).Value = IdUsuario;

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {

                    DateTime? etaPuerto = null, fechaProgramacion = null, etd = null;

                    if (reader["ETA_POD"] != DBNull.Value)
                    {
                        etaPuerto = Convert.ToDateTime(reader["ETA_POD"]);
                    }
                    if (reader["FECHAREGISTRO"] != DBNull.Value)
                    {
                        fechaProgramacion = Convert.ToDateTime(reader["FECHAREGISTRO"]);
                    }
                    if (reader["ETD"] != DBNull.Value)
                    {
                        etd = Convert.ToDateTime(reader["ETD"]);
                    }


                    Servicios.Add(new Clases.Servicio
                    {
                        BDOrigen = Convert.ToInt32(reader["BD_SERVICIO"]),
                        Contenedor = reader["CONTENEDOR"].ToString(),
                        Booking = reader["BOOKING"].ToString(),
                        Commodity = reader["COMMODITY"].ToString(),
                        Naviera = reader["NAVIERA"].ToString(),
                        Nave = reader["NAVE"].ToString(),
                        Origen = reader["ORIGEN"].ToString(),
                        Destino = reader["DESTINO"].ToString(),
                        EtaPuerto = etaPuerto,
                        SetpointAC = reader["SETPOINT_CO2"].ToString(),
                        SetpointT = reader["TEMPERATURA"].ToString(),
                        CO2Ext = reader["SENSOR_CO2"].ToString(),
                        TemperaturaExt = reader["SENSOR_T"].ToString(),
                        AperturaPuerta = reader["SENSOR_LUZ"].ToString(),
                        IdServicio = Convert.ToInt32(reader["ID_SERVICIO"]),
                        FechaProgramacion = fechaProgramacion,
                        NumSerieSensor = reader["NUM_SERIE_SENSOR"].ToString(),
                        Etd = etd
                    });

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }

            return Servicios;
        }


        public static ServicioNuevo ConsultarServiciosPorId(int id_Servicio, int id_bd)
        {
            ServicioNuevo servicio = new ServicioNuevo();
            servicio.id = 0;


            if (id_bd == 2)
            {
                SqlConnection cnn2;
                cnn2 = new SqlConnection(AlmaTest);
                try
                {
                    cnn2.Open();
                    SqlCommand command2 = new SqlCommand("EXEC ConsultarServiciosPorId @ID_SERVICIO;", cnn2);
                    command2.Parameters.Add("@ID_SERVICIO", SqlDbType.Int).Value = id_Servicio;
                    SqlDataReader reader2 = command2.ExecuteReader();
                    while (reader2.Read())
                    {
                        servicio.id = Convert.ToInt32(reader2["ID_SERVICIO"]);
                        servicio.contenedor = reader2["CONTENEDOR"].ToString();
                        servicio.lugar_origen = reader2["ORIGEN"].ToString();
                        servicio.lugar_destino = reader2["DESTINO"].ToString();
                        servicio.etd = Convert.ToDateTime(reader2["ETD"]);
                        servicio.eta = Convert.ToDateTime(reader2["ETA_POD"]);
                        servicio.etd_string = reader2["ETD"].ToString();
                        servicio.eta_string = reader2["ETA_POD"].ToString();
                        servicio.booking = reader2["BOOKING"].ToString();
                        servicio.id_nave = 0;
                        servicio.id_naviera = 0;
                        servicio.nave = reader2["NAVE"].ToString();
                        servicio.naviera = reader2["NAVIERA"].ToString();
                        servicio.commodity = reader2["COMMODITY"].ToString();
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    cnn2.Close();
                }
            }


            return servicio;
        }

        public static ServicioNuevo EliminarServicioPorId(int id_Servicio, int id_bd)
        {
            ServicioNuevo servicio = new ServicioNuevo();
            servicio.id = 0;


            if (id_bd == 2)
            {
                SqlConnection cnn2;
                cnn2 = new SqlConnection(AlmaTest);
                try
                {
                    cnn2.Open();
                    SqlCommand command2 = new SqlCommand("EXEC EliminarServicioPorId @ID_SERVICIO;", cnn2);
                    command2.Parameters.Add("@ID_SERVICIO", SqlDbType.Int).Value = id_Servicio;
                    SqlDataReader reader2 = command2.ExecuteReader();
                    while (reader2.Read())
                    {
                        servicio.id = Convert.ToInt32(reader2["RESPUESTA"]);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    cnn2.Close();
                }
            }
            return servicio;
        }

        public static string RegistrarServicioEmerson(ServicioNuevo Servicio, string numero_serie)
        {
            string respuesta = "";

            LoginRequest loginRequest = new LoginRequest();
            LoginResponse loginResponse = new LoginResponse();
            //loginRequest.UserName = "MCAMPOS@liventusglobal.com";//"MLAGOS@liventusglobal.com";
            loginRequest.UserName = "MLAGOS@liventusglobal.com"; ;
            //loginRequest.Password = "N$neiPVeV7";//"QSP>D:dHc&";
            loginRequest.Password = "pKnuP6W&jZIy8&";// "GcW%cygf&L";
            loginResponse = LoginEmerson(loginRequest);


            DeﬁneTripRequest defineTripRequest = new DeﬁneTripRequest();
            DeﬁneTripResponse defineTripResponse = new DeﬁneTripResponse();
            List<StopLocation> locations = new List<StopLocation>();

            string prefijo = "";
            if (Servicio.id_bd == 1) { prefijo = "S"; }
            else { prefijo = "A"; }

            //PROBAR ASIGNACION DE PREFIJO

            defineTripRequest.TripID = prefijo + Servicio.id + "-" + numero_serie;
            //defineTripRequest.TripID = prefijo + Servicio.id;
            defineTripRequest.TrackerID = numero_serie;

            StopLocation location = new StopLocation();
            location.LocationName = "Default Origin";
            location.LocationID = "99";
            location.Address1 = "Cantwell";
            //if (Servicio.numero_calle_origen != "")
            //{
            //    location.Address1 = Servicio.calle_origen + " " + Servicio.numero_calle_origen;
            //}
            //else
            //{
            //    location.Address1 = Servicio.calle_origen;
            //}
            location.City = "Cantwell";
            location.State = "Alaska";
            location.Zip = "99729";
            location.Country = "Estados Unidos";
            location.LocationType = "0";
            locations.Add(location);

            StopLocation location2 = new StopLocation();
            location2.LocationName = "Default Destination";
            location2.LocationID = "100";
            location2.Address1 = "Cantwell";
            //if (Servicio.numero_calle_destino != "")
            //{
            //    location2.Address1 = Servicio.calle_destino + " " + Servicio.numero_calle_destino;
            //}
            //else
            //{
            //    location2.Address1 = Servicio.calle_destino;
            //}
            location2.City = "Cantwell";
            location2.State = "Alaska";
            location2.Zip = "99729";
            location2.Country = "Estados Unidos";
            location2.LocationType = "1";
            locations.Add(location2);

            defineTripRequest.Locations = locations;
            defineTripResponse = DefineTrip(defineTripRequest, loginResponse.AccessToken);

            if (defineTripResponse.ErrorCode == "0")
            {
                respuesta = "Servicio de Emerson creado correctamente";
            }
            else
            {
                respuesta = "Error al asignar/programar sensor al contenedor, las posibles razones del error son: Número de serie mal digitado, no existe conexión con los servidores de Emerson o error no identificado.";
            }
            return respuesta;
        }

        public static LoginResponse LoginEmerson(LoginRequest loginRequest)
        {
            LoginResponse loginResponse = new LoginResponse();

            string postString = Newtonsoft.Json.JsonConvert.SerializeObject(loginRequest);

            byte[] data = UTF8Encoding.UTF8.GetBytes(postString);

            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

            HttpWebRequest request = WebRequest.Create("https://api.locustraxx.com/api/edi/Login") as HttpWebRequest;
            request.Timeout = 10000;
            request.Method = "POST";
            request.ContentLength = data.Length;
            request.ContentType = "application/json; charset=utf-8";
            request.Headers.Add("X-LT-ApiKey", apiKeyEmerson);

            Stream postStream = request.GetRequestStream();
            postStream.Write(data, 0, data.Length);

            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            StreamReader reader = new StreamReader(response.GetResponseStream());
            string body = reader.ReadToEnd();
            //string body = "{\"AccessToken\":\"eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6Ik1MQUdPU0BsaXZlbnR1c2dsb2JhbC5jb20iLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3VzZXJkYXRhIjoiQUFFQUFBRC8vLy8vQVFBQUFBQUFBQUFNQWdBQUFFTlBkbVZ5YzJsbmFIUkJjR2tzSUZabGNuTnBiMjQ5TVM0d0xqQXVNQ3dnUTNWc2RIVnlaVDF1WlhWMGNtRnNMQ0JRZFdKc2FXTkxaWGxVYjJ0bGJqMXVkV3hzQlFFQUFBQXlUM1psY25OcFoyaDBRWEJwTGtOdmJuUnliMnhzWlhKekxrVmthVU52Ym5SeWIyeHNaWElyU25kMFZYTmxja1JoZEdFQkFBQUFGenhCY0dsTFpYaythMTlmUW1GamEybHVaMFpwWld4a0FRSUFBQUFHQXdBQUFDUkVRVFl6T1RRM015MDBNRVJGTFRReU1UWXRPRFJCUWkwNVFrSTFNVE5ETWtGRE4wRUwiLCJuYmYiOjE1Nzg0MTQ4MTEsImV4cCI6MTU3ODQxNjAxMSwiaWF0IjoxNTc4NDE0ODExfQ.CwxjCW7nHgU0xYVuBRAVp1MvQL_Cmjohhi6_q2VdXh4\",\"RefreshToken\":\"b2e4f938-70df-494d-8653-7b99a4eb7260\",\"ErrorCode\":0}";

            loginResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<LoginResponse>(body);

            return loginResponse;
        }

        public static DeﬁneTripResponse DefineTrip(DeﬁneTripRequest deﬁneTripRequest, string autorization)
        {
            DeﬁneTripResponse defineTripResponse = new DeﬁneTripResponse();

            string postString = Newtonsoft.Json.JsonConvert.SerializeObject(deﬁneTripRequest);

            byte[] data = UTF8Encoding.UTF8.GetBytes(postString);

            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

            HttpWebRequest request = WebRequest.Create("https://api.locustraxx.com/api/edi/DefineTrip") as HttpWebRequest;
            request.Timeout = 999999;
            request.Method = "POST";
            request.ContentLength = data.Length;
            request.ContentType = "application/json; charset=utf-8";
            request.Headers.Add("Authorization", "Bearer " + autorization);

            Stream postStream = request.GetRequestStream();
            postStream.Write(data, 0, data.Length);



            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            StreamReader reader = new StreamReader(response.GetResponseStream());
            string body = reader.ReadToEnd();
            //string body = "{\"ErrorCode\":0,\"ErrorDescription\":\"\"}";

            defineTripResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<DeﬁneTripResponse>(body);

            return defineTripResponse;
        }



        public static List<Clases.Sensor> ConsultarSensoresPorIdServicio(int id_Servicio, int BD_origen)
        {
            List<Clases.Sensor> lista_sensores = new List<Clases.Sensor>();

            DateTime? progdate = null;
            SqlConnection cnn2;
            cnn2 = new SqlConnection(AlmaTest);
            try
            {
                cnn2.Open();
                SqlCommand command2 = new SqlCommand("EXEC ConsultarSensoresPorIdServicio @ID_SERVICIO, @ID_BD_SERVICIO;", cnn2);
                command2.Parameters.Add("@ID_SERVICIO", SqlDbType.Int).Value = id_Servicio;
                command2.Parameters.Add("@ID_BD_SERVICIO", SqlDbType.Int).Value = BD_origen;
                SqlDataReader reader2 = command2.ExecuteReader();
                while (reader2.Read())
                {
                    if (reader2["SENSOR"].ToString() != null)
                    {
                        if(reader2["FECHA_PROGRAMACION"]!=DBNull.Value)
                        {
                            progdate = Convert.ToDateTime(reader2["FECHA_PROGRAMACION"].ToString());
                        }
                        lista_sensores.Add(new Clases.Sensor
                        {

                            SensorId = reader2["SENSOR"].ToString(),
                            tipo_sensor = reader2["TIPO_SENSOR"].ToString(),
                            proveedor = reader2["PROVEEDOR"].ToString(),
                            bd = BD_origen,
                            fechaProgramacion = progdate,
                            usuarioProgramador= reader2["NOMBRE_USUARIO"].ToString(),
                            recibioDatos= reader2["RECIBIO_DATOS"].ToString()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn2.Close();
            }


            return lista_sensores;
        }

        public static string ValidarSensorEnServicio(string numero_sensor)
        {
            string respuesta = "";
            SqlConnection cnn2;
            cnn2 = new SqlConnection(AlmaTest);
            try
            {
                cnn2.Open();
                SqlCommand command2 = new SqlCommand("EXEC ValidarSensorEnServicio @SENSOR;", cnn2);
                command2.Parameters.Add("@SENSOR", SqlDbType.VarChar).Value = numero_sensor;
                SqlDataReader reader2 = command2.ExecuteReader();
                while (reader2.Read())
                {
                    respuesta = reader2["RESPUESTA"].ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn2.Close();
            }

            return respuesta;
        }

        public static bool ValidarContenedorEnServicio(string contenedor)
        {
            bool respuesta = false;
            int response = 0;
            SqlConnection cnn2;
            cnn2 = new SqlConnection(AlmaTest);
            try
            {
                cnn2.Open();
                SqlCommand command2 = new SqlCommand("EXEC ValidarContenedorEnServicio @CONTENEDOR;", cnn2);
                command2.Parameters.Add("@CONTENEDOR", SqlDbType.VarChar).Value = contenedor;
                SqlDataReader reader2 = command2.ExecuteReader();
                while (reader2.Read())
                {
                    response = Convert.ToInt32(reader2["RESPUESTA"].ToString());
                }

                if(response==0)
                {
                    return false;
                }else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn2.Close();
            }

            return respuesta;
        }

        public static int CancelarServicioPorId(int idServicio, int idBD, int IdUsuario)
        {
            int respuesta = 1;
            SqlConnection cnn2;
            cnn2 = new SqlConnection(JorgeTest);
            try
            {
                cnn2.Open();
                SqlCommand command2 = new SqlCommand("EXEC CancelarServicioById @IdServicio, @BD, @IdUsuario;", cnn2);
                command2.Parameters.Add("@IdServicio", SqlDbType.Int).Value = idServicio;
                command2.Parameters.Add("@BD", SqlDbType.Int).Value = idBD;
                command2.Parameters.Add("@IdUsuario", SqlDbType.Int).Value = IdUsuario;

                SqlDataReader reader2 = command2.ExecuteReader();
                while (reader2.Read())
                {
                    respuesta = Convert.ToInt32(reader2["RESPUESTA"]);
                }
            }
            catch (Exception ex)
            {
                respuesta = 0;
                throw ex;
            }
            finally
            {
                cnn2.Close();
            }

            return respuesta;
        }

        public static int FinalizarServicioPorId(int idServicio, int idBD, int IdUsuario)
        {
            int respuesta = 1;
            SqlConnection cnn2;
            cnn2 = new SqlConnection(JorgeTest);

            try
            {
                cnn2.Open();
                SqlCommand command2 = new SqlCommand("EXEC FinalizarServicioById @IdServicio, @BD, @IdUsuario;", cnn2);
                command2.Parameters.Add("@IdServicio", SqlDbType.Int).Value = idServicio;
                command2.Parameters.Add("@BD", SqlDbType.Int).Value = idBD;
                command2.Parameters.Add("@IdUsuario", SqlDbType.Int).Value = IdUsuario;

                SqlDataReader reader2 = command2.ExecuteReader();
                while (reader2.Read())
                {
                    respuesta = Convert.ToInt32(reader2["RESPUESTA"]);
                }
            }
            catch (Exception ex)
            {
                respuesta = 0;
                throw ex;
            }
            finally
            {
                cnn2.Close();
            }

            return respuesta;
        }


        public static string GenerarStringSensores(List<Clases.Sensor> SensoresServicio)
        {
            string cadena = "";

            foreach (Clases.Sensor sensor in SensoresServicio)
            {
                cadena = " " + cadena + sensor.numSerie + " |";
            }


            return cadena.TrimEnd('|');
        }


        public static string ServicioObtuvoDatos(List<Clases.Sensor> SensoresServicio)
        {
            foreach (Clases.Sensor sensor in SensoresServicio)
            {
                if (sensor.recibioDatos == "SI") return sensor.recibioDatos;
            }

            return "NO";
        }

        public static ServicioNuevo AsociarSensorContenedor(int id_Servicio, int id_bd, string tipo_sensor, string numero_sensor, string usuario)
        {
            ServicioNuevo servicio = new ServicioNuevo();
            servicio.id = 0;


            /*if (id_bd == 2)
            {*/
                SqlConnection cnn2;
                cnn2 = new SqlConnection(AlmaTest);
                try
                {
                    cnn2.Open();
                    SqlCommand command2 = new SqlCommand("EXEC AsociarSensorContenedor @USUARIO, @TIPO_SENSOR, @NUMERO_SENSOR, @ID_SERVICIO, @ID_BD_SERVICIO;", cnn2);
                    command2.Parameters.Add("@USUARIO", SqlDbType.VarChar).Value = usuario;
                    command2.Parameters.Add("@TIPO_SENSOR", SqlDbType.VarChar).Value = tipo_sensor;
                    command2.Parameters.Add("@NUMERO_SENSOR", SqlDbType.VarChar).Value = numero_sensor;
                    command2.Parameters.Add("@ID_SERVICIO", SqlDbType.Int).Value = id_Servicio;
                    command2.Parameters.Add("@ID_BD_SERVICIO", SqlDbType.Int).Value = id_bd;
                    SqlDataReader reader2 = command2.ExecuteReader();
                    while (reader2.Read())
                    {
                        servicio.id = Convert.ToInt32(reader2["RESPUESTA"]);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    cnn2.Close();
                }
            /*}*/
            return servicio;
        }

        public static int SetExcepcionSensor(int codigoError, string mensajeError)
        {

            SqlConnection cnn;
            int validar = 0; //ok

            cnn = new SqlConnection(AlmaTest);
            try
            {
                cnn.Open();

                SqlCommand command = new SqlCommand("EXEC dbo.InsertarExcepcionTripStatus @CodigoError, @MensajeError", cnn);

                command.Parameters.Add("@CodigoError", SqlDbType.Int).Value = codigoError;
                command.Parameters.Add("@MensajeError", SqlDbType.VarChar, 50).Value = mensajeError;

                validar = command.ExecuteNonQuery();
                cnn.Close();
            }
            catch (Exception ex)
            {
                validar = 1;
                cnn.Close();
                throw ex;
            }
            finally
            {
                cnn.Close();
            }

            return validar;
        }


        public static int EliminarSensor(int idServicio, string numero_sensor)
        {

            SqlConnection cnn;
            int validar = 0; //ok

            cnn = new SqlConnection(AlmaTest);
            try
            {
                cnn.Open();

                SqlCommand command = new SqlCommand("EXEC dbo.EliminarSensor @ID_SERVICIO, @NUM_SENSOR", cnn);

                command.Parameters.Add("@ID_SERVICIO", SqlDbType.Int).Value = idServicio;
                command.Parameters.Add("@NUM_SENSOR", SqlDbType.VarChar, 50).Value = numero_sensor;

                validar = command.ExecuteNonQuery();
                cnn.Close();
            }
            catch (Exception ex)
            {
                validar = 1;
                cnn.Close();
                throw ex;
            }
            finally
            {
                cnn.Close();
            }

            return validar;
        }

        public static Boolean ValidarModem(string numSerie)
        {

            SqlConnection cnn2;
            cnn2 = new SqlConnection(AlmaTest);

            try
            {
                cnn2.Open();

            SqlCommand command2 = new SqlCommand("EXEC dbo.ValidarModem @num_modem;", cnn2);

            command2.Parameters.Add("@num_modem", SqlDbType.VarChar).Value = numSerie;

            SqlDataReader reader2 = command2.ExecuteReader();
            while (reader2.Read())
            {
                if (reader2["ID_MODEM"] == DBNull.Value) return false;

                if (Convert.ToInt32(reader2["ID_MODEM"]) != 0)
                {
                    return true;

                }
                else {

                    return false;
                }

            }
            
             
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn2.Close();
            }

            return false;
        }

        public static int GetConsultarIdCliente(int IdUsuario)
        {
            int idCliente = 0;

            SqlConnection cnn2;
            cnn2 = new SqlConnection(AlmaTest);

            try
            {
                cnn2.Open();
                SqlCommand command2 = new SqlCommand("SELECT ID_CLIENTE FROM USUARIO WHERE ID_USUARIO = @IDUSUARIO;", cnn2);
                command2.Parameters.Add("@IDUSUARIO", SqlDbType.Int).Value = IdUsuario;

                SqlDataReader reader2 = command2.ExecuteReader();
                while (reader2.Read())
                {
                    idCliente = Convert.ToInt32(reader2["ID_CLIENTE"]);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn2.Close();
            }
            return idCliente;
        }

        public static bool permisoDetalleContenedor(int IdUsuario, int bd, int idServicio)
        {
            bool respuesta_final = false;
            int respuesta = 0;

            SqlConnection cnn2;
            cnn2 = new SqlConnection(AlmaTest);

            try
            {
                cnn2.Open();
                SqlCommand command2 = new SqlCommand("EXEC dbo.ValidarPermisoDetalleContenedor @ID_USUARIO, @ID_BD, @ID_SERVICIO;", cnn2);
                command2.Parameters.Add("@ID_USUARIO", SqlDbType.Int).Value = IdUsuario;
                command2.Parameters.Add("@ID_BD", SqlDbType.Int).Value = bd;
                command2.Parameters.Add("@ID_SERVICIO", SqlDbType.Int).Value = idServicio;

                SqlDataReader reader2 = command2.ExecuteReader();
                while (reader2.Read())
                {
                    respuesta = Convert.ToInt32(reader2["RESPUESTA"]);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn2.Close();
            }

            if (respuesta == 1)
            {
                respuesta_final = true;
            }

            return respuesta_final;
        }

        public static ServiceData DetectarAperturaPuerta(string numSerie)
        {
            int servicioId = Int32.Parse(numSerie.Substring(1, numSerie.Length - 1));
            /*string BD = numSerie.Substring(0, 1);
            int BD_origen = 0;

            if (BD == "S")
            {
                BD_origen = 1;
            }
            else if (BD == "A")
            {
                BD_origen = 2;
            }
            */
            SqlConnection cnn2;
            cnn2 = new SqlConnection(AlmaTest);
            ServiceData aperturaPuerta = new ServiceData();
            DateTime? fechaApertura = null, fechaControlador2 = null, fechaServiceData = null;

            try
            {
                cnn2.Open();

                SqlCommand command2 = new SqlCommand("EXEC dbo.ConsultarAperturaPuertaServiceData @servicioId;", cnn2);
                command2.CommandTimeout = 999;

                command2.Parameters.Add("@servicioId", SqlDbType.Int).Value = servicioId;

                SqlDataReader reader2 = command2.ExecuteReader();
                while (reader2.Read())
                {
                    if (reader2["FECHA_APERTURA"] != DBNull.Value)
                    {
                        fechaApertura = Convert.ToDateTime(reader2["FECHA_APERTURA"]);
                    }

                    if (reader2["FECHA_CONTROLADOR2"] != DBNull.Value)
                    {
                        fechaControlador2 = Convert.ToDateTime(reader2["FECHA_CONTROLADOR2"]);
                    }

                    if (reader2["ULT_SERVICEDATA"] != DBNull.Value)
                    {
                        fechaServiceData = Convert.ToDateTime(reader2["ULT_SERVICEDATA"]);
                    }


                    aperturaPuerta.FechaMedicion = fechaApertura;
                    aperturaPuerta.Temperatura = Convert.ToInt32(reader2["MEDICION_TEMP"]);
                    aperturaPuerta.CO2 = Convert.ToInt32(reader2["MEDICION_CO2"]);
                    aperturaPuerta.O2 = Convert.ToInt32(reader2["MEDICION_O2"]);
                    aperturaPuerta.SetpointCO2 = Convert.ToInt32(reader2["SETPOINT_CO2"]);
                    aperturaPuerta.FechaControlador2 = fechaControlador2;
                    aperturaPuerta.IdCommodity = Convert.ToInt32(reader2["ID_COMMODITY"]);
                    aperturaPuerta.UltServiceDataEdit = fechaServiceData;
                    aperturaPuerta.ContadorOutliersBD = Convert.ToInt32(reader2["OUTLIERS_MODIFICADOS"]);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn2.Close();
            }

            return aperturaPuerta;
        }


        public static int ServiceDataActualizado(List<StackedLineChartData100> lista_med_co2, int idServicio)
        {

            SqlConnection cnn;
            int validar = 0; //ok

            cnn = new SqlConnection(connectionAPIService);
            try
            {
                foreach (StackedLineChartData100 medicion_co2 in lista_med_co2)
                {
                    cnn.Open();

                    SqlCommand command = new SqlCommand("EXEC dbo.InsertarNuevoServiceData @IdServicio, @IdServiceData, @MedicionCO2, @FechaMedicion", cnn);

                    command.Parameters.Add("@IdServicio", SqlDbType.Int).Value = idServicio;
                    command.Parameters.Add("@IdServiceData", SqlDbType.Int).Value = medicion_co2.id_serviceData;
                    command.Parameters.Add("@MedicionCO2", SqlDbType.Float, 10).Value = medicion_co2.y;
                    command.Parameters.Add("@FechaMedicion", SqlDbType.DateTime).Value = medicion_co2.x;

                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        validar = Convert.ToInt32(reader["validado_ok"]);
                    }

                    cnn.Close();
                }
            }
            catch (Exception ex)
            {
                validar = 1;
                cnn.Close();
                throw ex;
            }
            finally
            {
                cnn.Close();
            }

            return validar;
        }
        public static List<Clases.Objeto> GetClientsNameId()
        {
            SqlConnection cnn;
            List<Clases.Objeto> Lista_Clientesnameid = new List<Clases.Objeto>();
            cnn = new SqlConnection(AlmaTest);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("EXEC dbo.ObtenerTodosClientes;", cnn);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Clases.Objeto objeto = new Clases.Objeto();
                    //objeto.Name = reader["NOMBRE"].ToString();
                    objeto.Name = reader["CLIENTE"].ToString();
                    objeto.Code = reader["MEZCLA"].ToString();
                    
                    Lista_Clientesnameid.Add(objeto);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }
            return Lista_Clientesnameid;
        }
        public static bool ValidarRegistrosSensorSinServicio(string num_sensor)
        {
            bool respuesta = false;
            SqlConnection cnn;
            cnn = new SqlConnection(AlmaTest);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("EXEC ValidarRegistrosSensorSinServicio @num_sensor", cnn);
                command.Parameters.Add("@num_sensor", SqlDbType.VarChar, 50).Value = num_sensor;
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int respuesta_procedimiento = Convert.ToInt32(reader["RESPUESTA"]);
                    if (respuesta_procedimiento == 1)
                    {
                        respuesta = true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }
            return respuesta;
        }

    }
}

