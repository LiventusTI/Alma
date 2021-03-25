using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using static ProyectoAlmaInicio.Models.Clases;

namespace ProyectoAlmaInicio.Models
{
    public class DestinatariosModels
    {
        static string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
        static string connectionAPIService = ConfigurationManager.ConnectionStrings["connectionAPIService"].ConnectionString;
        static string AlmaTest = ConfigurationManager.ConnectionStrings["AlmaTest"].ConnectionString;
        static string MacaTest = ConfigurationManager.ConnectionStrings["MacaTest"].ConnectionString;
        static string JorgeTest = ConfigurationManager.ConnectionStrings["JorgeTest"].ConnectionString;
        static string connectionStringTecnica = "Server=68.169.63.233;Port=5306;Uid=liventus_sa;Pwd=L1v3nt9ss4;Database=prometeo";
        static string apiKeyEmerson = "088B734E-321D-43A5-87BE-6D7C7633576F";//"DA639473-40DE-4216-84AB-9BB513C2AC7A";

        public static int NuevoDestinatario(Destinatario nuevoDestinatario)
        {
            SqlConnection cnn2;
            cnn2 = new SqlConnection(AlmaTest);
            try
            {
                cnn2.Open();
                SqlCommand command2 = new SqlCommand("EXEC IngresarDestinatario " +
                    "@empresaDestinatario, @actividadEmpresa, @paisContacto, @puertoContacto, @nombreContacto, @emailContacto, @telefonoContacto, @usuario", cnn2);

                command2.Parameters.Add("@empresaDestinatario", SqlDbType.VarChar, 100).Value = nuevoDestinatario.empresa_destinatario;
                command2.Parameters.Add("@actividadEmpresa", SqlDbType.Int).Value = nuevoDestinatario.actividad_empresa;
                command2.Parameters.Add("@paisContacto", SqlDbType.Int).Value = nuevoDestinatario.pais_contacto;
                command2.Parameters.Add("@puertoContacto", SqlDbType.Int).Value = nuevoDestinatario.puerto_contacto;
                command2.Parameters.Add("@nombreContacto", SqlDbType.VarChar, 50).Value = nuevoDestinatario.nombre_contacto;
                command2.Parameters.Add("@emailContacto", SqlDbType.VarChar, 50).Value = nuevoDestinatario.email_contacto;
                command2.Parameters.Add("@telefonoContacto", SqlDbType.VarChar, 50).Value = nuevoDestinatario.telefono_contacto;
                command2.Parameters.Add("@usuario", SqlDbType.Int).Value = nuevoDestinatario.usuario_edita;

                SqlDataReader reader2 = command2.ExecuteReader();
                while (reader2.Read())
                {
                    return Convert.ToInt32(reader2["ID_DESTINATARIO"]);
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

            return 0;
        }

        public static int InsertarCommodities(List<CommodityEmpresa> commodities, int idDestinatario)
        {
            SqlConnection cnn2;
            cnn2 = new SqlConnection(AlmaTest);
            try
            {
                foreach (CommodityEmpresa commodity in commodities)
                {
                    cnn2.Open();
                    SqlCommand command2 = new SqlCommand("EXEC IngresarCommodityDestinatario @commodityEmpresa, @idDestinatario", cnn2);

                    command2.Parameters.Add("@commodityEmpresa", SqlDbType.VarChar, 50).Value = commodity.commodity_empresa;
                    command2.Parameters.Add("@idDestinatario", SqlDbType.Int).Value = idDestinatario;

                    SqlDataReader reader2 = command2.ExecuteReader();
                    /*while (reader2.Read())
                    {
                        return 0;
                    }*/
                    cnn2.Close();
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

            return 0;
        }


        public static List<Clases.Destinatario> GetDestinatariosCliente(int IdUsuario)
        {
            SqlConnection cnn;
            List<Clases.Destinatario> listaDestinatarios = new List<Clases.Destinatario>();
            Clases.Destinatario objeto = new Clases.Destinatario();

            cnn = new SqlConnection(AlmaTest);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("EXEC dbo.ConsultarDestinatarios @IdUsuario;", cnn);
                command.Parameters.Add("@IdUsuario", SqlDbType.Int).Value = IdUsuario;

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    objeto = new Clases.Destinatario();
                    objeto.id_destinatario = Convert.ToInt32(reader["ID_DESTINATARIO"]);
                    objeto.nombre_pais_contacto = reader["PAIS"].ToString();
                    objeto.nombre_puerto_contacto = reader["PUERTO"].ToString();
                    objeto.empresa_destinatario = reader["NOMBRE_EMPRESA"].ToString();
                    objeto.nombre_contacto = reader["CONTACTO"].ToString();
                    objeto.email_contacto = reader["EMAIL"].ToString();
                    objeto.telefono_contacto = reader["TELEFONO"].ToString();
                    objeto.nombre_actividad_empresa = reader["ACTIVIDAD"].ToString();
                    objeto.lista_commodities = reader["COMMODITIES"].ToString();
                    listaDestinatarios.Add(objeto);
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

            return listaDestinatarios;
        }

        public static int BorrarDestinatario(int id_destinatario)
        {
            SqlConnection cnn;
            int respuesta = 0;

            cnn = new SqlConnection(AlmaTest);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("EXEC dbo.BorrarDestinatario @id_destinatario;", cnn);
                command.Parameters.Add("@id_destinatario", SqlDbType.Int).Value = id_destinatario;

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    respuesta = Convert.ToInt32(reader["RESPUESTA"]);
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

        public static Destinatario ObtenerDatosDestinatario(int id_destinatario)
        {
            SqlConnection cnn;
            Destinatario destinatario = new Destinatario();

            cnn = new SqlConnection(AlmaTest);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("EXEC dbo.ObtenerDatosDestinatario @id_destinatario;", cnn);
                command.Parameters.Add("@id_destinatario", SqlDbType.Int).Value = id_destinatario;

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    destinatario.id_destinatario = Convert.ToInt32(reader["ID_DESTINATARIO"]);
                    destinatario.empresa_destinatario = reader["NOMBRE_EMPRESA"].ToString();
                    destinatario.nombre_actividad_empresa = reader["ACTIVIDAD"].ToString();
                    destinatario.nombre_pais_contacto = reader["PAIS"].ToString();
                    destinatario.nombre_puerto_contacto = reader["PUERTO"].ToString();

                    destinatario.nombre_contacto = reader["NOMBRE_CONTACTO"].ToString();
                    destinatario.email_contacto = reader["EMAIL_CONTACTO"].ToString();
                    destinatario.telefono_contacto = reader["TELEFONO_CONTACTO"].ToString();
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

            return destinatario;
        }

        public static List<Commodity> ObtenerCommoditiesDestinatario(int id_destinatario)
        {
            SqlConnection cnn;
            List<Commodity> lista_commodities = new List<Commodity>();
            Commodity commodity = new Commodity();

            cnn = new SqlConnection(AlmaTest);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("EXEC dbo.ObtenerCommoditiesDestinatario @id_destinatario;", cnn);
                command.Parameters.Add("@id_destinatario", SqlDbType.Int).Value = id_destinatario;

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    commodity = new Clases.Commodity();
                    commodity.Nombre = reader["COMMODITY"].ToString();
                    lista_commodities.Add(commodity);
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

            return lista_commodities;
        }


        public static int EditarDestinatario(int id_destinatario, string empresa, string actividad, string pais, string puerto, string nombre_contacto, string email, string telefono)
        {
            int respuesta = 0;
            SqlConnection cnn2;
            cnn2 = new SqlConnection(AlmaTest);
            try
            {
                cnn2.Open();
                SqlCommand command2 = new SqlCommand("EXEC EditarDestinatario " +
                    "@idDestinatario, @empresaDestinatario, @actividadEmpresa, @paisContacto, @puertoContacto, @nombreContacto, @emailContacto, @telefonoContacto", cnn2);
                command2.Parameters.Add("@idDestinatario", SqlDbType.Int).Value = id_destinatario;
                command2.Parameters.Add("@empresaDestinatario", SqlDbType.VarChar, 100).Value = empresa;
                command2.Parameters.Add("@actividadEmpresa", SqlDbType.VarChar, 100).Value = actividad;
                command2.Parameters.Add("@paisContacto", SqlDbType.VarChar, 100).Value = pais;
                command2.Parameters.Add("@puertoContacto", SqlDbType.VarChar, 100).Value = puerto;
                command2.Parameters.Add("@nombreContacto", SqlDbType.VarChar, 50).Value = nombre_contacto;
                command2.Parameters.Add("@emailContacto", SqlDbType.VarChar, 50).Value = email;
                command2.Parameters.Add("@telefonoContacto", SqlDbType.VarChar, 50).Value = telefono;
                SqlDataReader reader2 = command2.ExecuteReader();
                while (reader2.Read())
                {
                    respuesta = Convert.ToInt32(reader2["ID_DESTINATARIO"]);
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


        public static int BorrarCommoditiesDestinatario(int id_destinatario)
        {
            int respuesta = 0;
            SqlConnection cnn2;
            cnn2 = new SqlConnection(AlmaTest);
            try
            {
                cnn2.Open();
                SqlCommand command2 = new SqlCommand("EXEC BorrarCommoditiesDestinatario @idDestinatario", cnn2);
                command2.Parameters.Add("@idDestinatario", SqlDbType.Int).Value = id_destinatario;
                SqlDataReader reader2 = command2.ExecuteReader();
                while (reader2.Read())
                {
                    respuesta = Convert.ToInt32(reader2["ID_DESTINATARIO"]);
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
    }
}