using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using System.Data;

namespace ProyectoAlmaInicio.Models
{
    // Puede agregar datos del perfil del usuario agregando más propiedades a la clase ApplicationUser. Para más información, visite http://go.microsoft.com/fwlink/?LinkID=317594.
    public class MantenedorModels
    {
        static string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
        static string connectionAPIService = ConfigurationManager.ConnectionStrings["connectionAPIService"].ConnectionString;
        static string AlmaTest = ConfigurationManager.ConnectionStrings["AlmaTest"].ConnectionString;
        //static string connectionStringTecnica = "Server=68.169.63.233;Port=5306;Uid=liventus_sa;Pwd=L1v3nt9ss4;Database=prometeo";

        public static List<Clases.Naviera> GetNavierasActivas(int IdUsuario)
        {
            SqlConnection cnn;
            List<Clases.Naviera> Naviera = new List<Clases.Naviera>();
            cnn = new SqlConnection(connectionAPIService);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("EXEC dbo.ConsultarMantenedorNavieras @USUARIO;", cnn);
                command.Parameters.Add("@USUARIO", SqlDbType.Int).Value = IdUsuario;

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Naviera.Add(new Clases.Naviera
                    {
                        Nombre = reader["NOMBRE"].ToString(),
                        Id = Convert.ToInt32(reader["ID_NAVIERA"]),
                        Activo = Convert.ToInt32(reader["ESTADO"])
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
            return Naviera;
        }

        //---
        public static List<Clases.Commodity> GetCommodityActivo(int IdUsuario)
        {
            SqlConnection cnn;
            List<Clases.Commodity> Commodity = new List<Clases.Commodity>();
            cnn = new SqlConnection(connectionAPIService);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("EXEC dbo.ConsultarMantenedorCommodity @USUARIO;", cnn);
                command.Parameters.Add("@USUARIO", SqlDbType.Int).Value = IdUsuario;

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Commodity.Add(new Clases.Commodity
                    {
                        Nombre = reader["NOMBRE"].ToString(),
                        Id = Convert.ToInt32(reader["ID_COMMODITY"]),
                        Activo = Convert.ToInt32(reader["ESTADO"])
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
            return Commodity;
        }

        //---
        public static List<Clases.Contenedor> GetContenedores(int IdUsuario)
        {
            SqlConnection cnn;
            List<Clases.Contenedor> Contenedores = new List<Clases.Contenedor>();
            cnn = new SqlConnection(connectionAPIService);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("EXEC dbo.ConsultarMantenedorContenedores @USUARIO;", cnn);
                command.Parameters.Add("@USUARIO", SqlDbType.Int).Value = IdUsuario;

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Contenedores.Add(new Clases.Contenedor
                    {
                        NumeroContenedor = reader["CONTENEDOR"].ToString(),
                        IdContenedor = Convert.ToInt32(reader["ID_CONTENEDOR"]),
                        EstadoContenedor = Convert.ToInt32(reader["ESTADO"])
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
            return Contenedores;
        }

        //---        
        public static List<Clases.PuertoDestino> GetPuertoOrigen(int IdUsuario)
        {
            SqlConnection cnn;
            List<Clases.PuertoDestino> PuertoDestino = new List<Clases.PuertoDestino>();
            cnn = new SqlConnection(connectionAPIService);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("EXEC dbo.ConsultarMantenedorPuertoOrigen @USUARIO;", cnn);
                command.Parameters.Add("@USUARIO", SqlDbType.Int).Value = IdUsuario;

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    PuertoDestino.Add(new Clases.PuertoDestino
                    {
                        Nombre = reader["NOMBRE"].ToString(),
                        Id = Convert.ToInt32(reader["ID_PUERTOORIGEN"]),
                        IdCiudad = Convert.ToInt32(reader["ID_CIUDAD"]),
                        Activo = Convert.ToInt32(reader["ESTADO"]),
                        NombreCiudad = reader["NOMBRECIUDAD"].ToString()
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
            return PuertoDestino;
        }

        //---        
        public static List<Clases.PuertoDestino> GetPuertoDestino(int IdUsuario)
        {
            SqlConnection cnn;
            List<Clases.PuertoDestino> PuertoDestino = new List<Clases.PuertoDestino>();
            cnn = new SqlConnection(connectionAPIService);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("EXEC dbo.ConsultarMantenedorPuertoDestino @USUARIO;", cnn);
                command.Parameters.Add("@USUARIO", SqlDbType.Int).Value = IdUsuario;

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    PuertoDestino.Add(new Clases.PuertoDestino
                    {
                        Nombre = reader["NOMBRE"].ToString(),
                        Id = Convert.ToInt32(reader["ID_PUERTOORIGEN"]),
                        IdCiudad = Convert.ToInt32(reader["ID_CIUDAD"]),
                        Activo = Convert.ToInt32(reader["ESTADO"]),
                        NombreCiudad = reader["NOMBRECIUDAD"].ToString()
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
            return PuertoDestino;
        }

        //---
        public static List<Clases.Objeto> GetPuertosServicioNuevo()
        {
            SqlConnection cnn;
            List<Clases.Objeto> Tipos_Sensores = new List<Clases.Objeto>();
            cnn = new SqlConnection(AlmaTest);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("EXEC dbo.ConsultarMantenedorPuertos;", cnn);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Clases.Objeto objeto = new Clases.Objeto();
                    objeto.Code = reader["ID_PUERTOORIGEN"].ToString();
                    objeto.Name = reader["NOMBRE"].ToString();
                    Tipos_Sensores.Add(objeto);
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
            return Tipos_Sensores;
        }


        //---
        public static List<Clases.Nave> GetNaves(int IdUsuario)
        {
            SqlConnection cnn;
            List<Clases.Nave> Nave = new List<Clases.Nave>();
            cnn = new SqlConnection(connectionAPIService);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("EXEC dbo.ConsultarMantenedorNaves @USUARIO;", cnn);
                command.Parameters.Add("@USUARIO", SqlDbType.Int).Value = IdUsuario;

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Nave.Add(new Clases.Nave
                    {
                        NombreNave = reader["NOMBRENAVE"].ToString(),
                        IdNave = Convert.ToInt32(reader["ID_NAVE"]),
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
            return Nave;
        }

        //---
        public static List<string> GetBookings(int IdUsuario)
        {
            SqlConnection cnn;
            List<string> Bookings = new List<string>();
            cnn = new SqlConnection(connectionAPIService);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("EXEC dbo.ConsultarMantenedorBookings @USUARIO;", cnn);
                command.Parameters.Add("@USUARIO", SqlDbType.Int).Value = IdUsuario;

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Bookings.Add(reader["BOOKING"].ToString());
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
            return Bookings;
        }

        public static List<Clases.Objeto> GetTiposSensores()
        {
            SqlConnection cnn;
            List<Clases.Objeto> Tipos_Sensores = new List<Clases.Objeto>();
            cnn = new SqlConnection(AlmaTest);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("EXEC dbo.ConsultarTipoSensores;", cnn);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Clases.Objeto objeto = new Clases.Objeto();
                    objeto.Code = reader["ID"].ToString();
                    objeto.Name = reader["SENSOR"].ToString();
                    objeto.Pic = "image_sensor" + objeto.Code;
                    Tipos_Sensores.Add(objeto);
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
            return Tipos_Sensores;
        }


        public static List<Clases.Objeto> GetContenedoresServicioNuevo()
        {
            SqlConnection cnn;
            List<Clases.Objeto> Lista_Contenedores = new List<Clases.Objeto>();
            cnn = new SqlConnection(connectionAPIService);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("EXEC dbo.ConsultarContenedoresNuevoServicioAlma;", cnn);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Clases.Objeto objeto = new Clases.Objeto();
                    objeto.Code = reader["CONTENEDOR"].ToString();
                    objeto.Name = reader["CONTENEDOR"].ToString();
                    Lista_Contenedores.Add(objeto);
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
            return Lista_Contenedores;
        }


        public static List<Clases.Objeto> GetCommoditiesServicioNuevo()
        {
            SqlConnection cnn;
            List<Clases.Objeto> Lista_Commodities = new List<Clases.Objeto>();
            cnn = new SqlConnection(AlmaTest);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("EXEC dbo.ConsultarCommoditiesNuevoServicioAlma;", cnn);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Clases.Objeto objeto = new Clases.Objeto();
                    objeto.Code = reader["COMMODITY"].ToString();
                    objeto.Name = reader["COMMODITY"].ToString();
                    Lista_Commodities.Add(objeto);
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
            return Lista_Commodities;
        }

        public static List<Clases.Objeto> GetCommoditiesMantenedor()
        {
            SqlConnection cnn;
            List<Clases.Objeto> Lista_Commodities = new List<Clases.Objeto>();
            cnn = new SqlConnection(AlmaTest);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("EXEC dbo.ConsultarCommoditiesMantenedor;", cnn);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Clases.Objeto objeto = new Clases.Objeto();
                    objeto.Code = reader["COMMODITY"].ToString();
                    objeto.Name = reader["COMMODITY"].ToString();
                    Lista_Commodities.Add(objeto);
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
            return Lista_Commodities;
        }


        public static List<Clases.Objeto> GetNavesServicioNuevo()
        {
            SqlConnection cnn;
            List<Clases.Objeto> Lista_Naves = new List<Clases.Objeto>();
            cnn = new SqlConnection(AlmaTest);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("EXEC dbo.ConsultarNavesNuevoServicioAlma;", cnn);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Clases.Objeto objeto = new Clases.Objeto();
                    objeto.Code = reader["ID"].ToString();
                    objeto.Name = reader["NAVE"].ToString();
                    Lista_Naves.Add(objeto);
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
            return Lista_Naves;
        }


        public static List<Clases.Objeto> GetNavierasServicioNuevo()
        {
            SqlConnection cnn;
            List<Clases.Objeto> Lista_Navieras = new List<Clases.Objeto>();
            cnn = new SqlConnection(AlmaTest);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("EXEC dbo.ConsultarNavierasNuevoServicioAlma;", cnn);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Clases.Objeto objeto = new Clases.Objeto();
                    objeto.Code = reader["ID"].ToString();
                    objeto.Name = reader["NAVIERA"].ToString();
                    Lista_Navieras.Add(objeto);
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
            return Lista_Navieras;
        }


        public static List<Clases.Objeto> GetSetpointsCO2ServicioNuevo()
        {
            SqlConnection cnn;
            List<Clases.Objeto> Lista_Setpoints = new List<Clases.Objeto>();
            cnn = new SqlConnection(AlmaTest);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("EXEC dbo.ConsultarSetpointsCO2NuevoServicioAlma;", cnn);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Clases.Objeto objeto = new Clases.Objeto();
                    objeto.Code = reader["ID"].ToString();
                    objeto.Name = reader["SETPOINT"].ToString() + "%";
                    Lista_Setpoints.Add(objeto);
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
            return Lista_Setpoints;
        }


        public static List<Clases.Objeto> GetSetpointsTemperaturaServicioNuevo()
        {
            SqlConnection cnn;
            List<Clases.Objeto> Lista_Setpoints = new List<Clases.Objeto>();
            cnn = new SqlConnection(AlmaTest);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("EXEC dbo.ConsultarSetpointsTemperaturaNuevoServicioAlma;", cnn);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Clases.Objeto objeto = new Clases.Objeto();
                    objeto.Code = reader["ID"].ToString();
                    objeto.Name = reader["SETPOINT"].ToString() + "°C";
                    Lista_Setpoints.Add(objeto);
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
            return Lista_Setpoints;
        }
        public static List<Clases.Objeto> GetDestinatarios(int IdUsuario)
        {
            SqlConnection cnn;
            List<Clases.Objeto> lista_destinatarios = new List<Clases.Objeto>();
            cnn = new SqlConnection(AlmaTest);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("EXEC dbo.ConsultarDestinatarios @IdUsuario;", cnn);
                command.Parameters.Add("@IdUsuario", SqlDbType.Int).Value = IdUsuario;
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Clases.Objeto objeto = new Clases.Objeto();
                    objeto.Code = reader["ID_DESTINATARIO"].ToString();
                    objeto.Name = reader["NOMBRE_EMPRESA"].ToString();
                    lista_destinatarios.Add(objeto);
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
            return lista_destinatarios;
        }


        public static List<Clases.Objeto> GetPaises()
        {
            SqlConnection cnn;
            List<Clases.Objeto> Lista_Paises = new List<Clases.Objeto>();
            cnn = new SqlConnection(connectionAPIService);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("EXEC dbo.ConsultarMantenedorPaises;", cnn);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Clases.Objeto objeto = new Clases.Objeto();
                    objeto.Code = reader["ID_PAIS"].ToString();
                    objeto.Name = reader["NOMBRE"].ToString();
                    Lista_Paises.Add(objeto);
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
            return Lista_Paises;
        }

        public static List<Clases.Objeto> GetMantenedorActividadesEmpresa()
        {
            SqlConnection cnn;
            List<Clases.Objeto> Lista_Actividades = new List<Clases.Objeto>();
            cnn = new SqlConnection(AlmaTest);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("EXEC dbo.MantenedorActividadesEmpresa;", cnn);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Clases.Objeto objeto = new Clases.Objeto();
                    objeto.Code = reader["ID_ACTIVIDAD"].ToString();
                    objeto.Name = reader["NOMBRE_ACTIVIDAD"].ToString();
                    Lista_Actividades.Add(objeto);
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
            return Lista_Actividades;
        }

        public static List<Clases.Objeto> GetMantenedorDestinatariosCliente(int IdUsuario)
        {
            SqlConnection cnn;
            List<Clases.Objeto> Lista_Actividades = new List<Clases.Objeto>();
            cnn = new SqlConnection(AlmaTest);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("EXEC dbo.ConsultarDestinatarios @IdUsuario;", cnn);
                command.Parameters.Add("@IdUsuario", SqlDbType.Int).Value = IdUsuario;
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Clases.Objeto objeto = new Clases.Objeto();
                    objeto.Code = reader["ID_DESTINATARIO"].ToString();
                    objeto.Name = reader["NOMBRE_EMPRESA"].ToString();
                    Lista_Actividades.Add(objeto);
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
            return Lista_Actividades;
        }

        public static List<Clases.Objeto> GetSetpointCO2()
        {
            SqlConnection cnn;
            List<Clases.Objeto> Lista_Setpoints = new List<Clases.Objeto>();
            cnn = new SqlConnection(connectionAPIService);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("EXEC dbo.ConsultarMantenedorSetpointsCO2;", cnn);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Clases.Objeto objeto = new Clases.Objeto();
                    objeto.Code = reader["ID_SETPOINT"].ToString();
                    objeto.Name = reader["SETPOINT_CO2"].ToString() + reader["SETPOINT_O2"].ToString();
                    Lista_Setpoints.Add(objeto);
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
            return Lista_Setpoints;
        }


    }
}

