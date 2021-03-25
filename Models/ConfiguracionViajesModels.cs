using System;

using System.Collections.Generic;

using System.ComponentModel;

using System.ComponentModel.DataAnnotations;

using System.Configuration;

using System.Data;

using System.Data.SqlClient;

using System.Web.Mvc;



namespace ProyectoAlmaInicio.Models

{

    public class ConfiguracionViajesModels

    {

        static string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

        static string connectionAPIService = ConfigurationManager.ConnectionStrings["connectionAPIService"].ConnectionString;

        static string AlmaTest = ConfigurationManager.ConnectionStrings["AlmaTest"].ConnectionString;

        static string connectionStringTecnica = "Server=68.169.63.233;Port=5306;Uid=liventus_sa;Pwd=L1v3nt9ss4;Database=prometeo";

        static string apiKeyEmerson = "088B734E-321D-43A5-87BE-6D7C7633576F";//"DA639473-40DE-4216-84AB-9BB513C2AC7A";





        public static List<Clases.Objeto> obtenerCondicionesFinViajes()

        {

            List<Clases.Objeto> lista_condiciones = new List<Clases.Objeto>();

            SqlConnection conec = new SqlConnection(AlmaTest);

            try

            {

                conec.Open();



                SqlCommand comando = new SqlCommand("SELECT * FROM CONDICIONES_FIN_VIAJES WHERE ESTADO = 1;", conec);

                SqlDataReader lectura = comando.ExecuteReader();



                while (lectura.Read())

                {

                    if (lectura["ID_CONDICION"] != DBNull.Value)

                    {

                        lista_condiciones.Add(new Clases.Objeto()

                        {

                            Code = lectura["ID_CONDICION"].ToString(),

                            Name = lectura["CONDICION"].ToString()

                        });

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



            return lista_condiciones;

        }







        public static int agregarConfiguracionFinViaje(int id_usuario, int condicion1, string tipo_condicion, int condicion2)

        {

            int respuesta = 0;

            SqlConnection cnn2;

            cnn2 = new SqlConnection(AlmaTest);

            try

            {

                cnn2.Open();

                SqlCommand command2 = new SqlCommand("EXEC AgregarConfiguracionFinalizacionViajes @id_usuario, @id_condicion1, @tipo_condicion, @id_condicion2;", cnn2);

                command2.Parameters.Add("@id_usuario", SqlDbType.Int).Value = id_usuario;

                command2.Parameters.Add("@id_condicion1", SqlDbType.VarChar).Value = condicion1;

                command2.Parameters.Add("@tipo_condicion", SqlDbType.VarChar).Value = tipo_condicion;

                command2.Parameters.Add("@id_condicion2", SqlDbType.VarChar).Value = condicion2;



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

            return respuesta;

        }



        public static List<Clases.ConfiguracionFinalizacionViajes> verConfiguracionFinViaje(int id_usuario)

        {

            int respuesta = 0;

            int cantidad_ingresada = 0;

            DateTime? fecha_ingresada = null;

            DateTime? fecha = null;



            List<Clases.ConfiguracionFinalizacionViajes> lista_condiciones = new List<Clases.ConfiguracionFinalizacionViajes>();





            SqlConnection cnn2;

            cnn2 = new SqlConnection(AlmaTest);

            try

            {

                cnn2.Open();

                SqlCommand command2 = new SqlCommand("EXEC VerConfiguracionFinalizacionViajes @id_usuario;", cnn2);

                command2.Parameters.Add("@id_usuario", SqlDbType.Int).Value = id_usuario;



                SqlDataReader reader2 = command2.ExecuteReader();

                while (reader2.Read())

                {

                    respuesta = Convert.ToInt32(reader2["RESPUESTA"]);

                    if (respuesta == 1)

                    {

                        if (reader2["CANTIDAD_INGRESADA"] != DBNull.Value) cantidad_ingresada = Convert.ToInt32(reader2["CANTIDAD_INGRESADA"]);

                        if (reader2["FECHA_INGRESADA"] != DBNull.Value) fecha_ingresada = Convert.ToDateTime(reader2["FECHA_INGRESADA"]);



                        lista_condiciones.Add(new Clases.ConfiguracionFinalizacionViajes()

                        {

                            id_condicion = Convert.ToInt32(reader2["ID_CONDICION"]),

                            cantidad_ingresada = cantidad_ingresada,

                            fecha_ingresada = fecha_ingresada,

                            grupo = Convert.ToInt32(reader2["GRUPO"]),

                            fecha = reader2["FECHA_REGISTRO"].ToString()

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



            return lista_condiciones;

        }



        public static int editarConfiguracionFinViaje(int id_usuario, int condicion1, string tipo_condicion, int condicion2)

        {

            int respuesta = 0;

            SqlConnection cnn2;

            cnn2 = new SqlConnection(AlmaTest);

            try

            {

                cnn2.Open();

                SqlCommand command2 = new SqlCommand("EXEC EditarConfiguracionFinalizacionViajes @id_usuario, @id_condicion1, @tipo_condicion, @id_condicion2;", cnn2);

                command2.Parameters.Add("@id_usuario", SqlDbType.Int).Value = id_usuario;

                command2.Parameters.Add("@id_condicion1", SqlDbType.VarChar).Value = condicion1;

                command2.Parameters.Add("@tipo_condicion", SqlDbType.VarChar).Value = tipo_condicion;

                command2.Parameters.Add("@id_condicion2", SqlDbType.VarChar).Value = condicion2;



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

            return respuesta;

        }



        public static List<Clases.Objeto> obtenerParametrosFrecuenciaEnvio()

        {

            List<Clases.Objeto> lista_frecuencias = new List<Clases.Objeto>();

            SqlConnection conec = new SqlConnection(AlmaTest);

            try

            {

                conec.Open();



                SqlCommand comando = new SqlCommand("SELECT * FROM FRECUENCIA_ENVIO_NOTIFICACION;", conec);

                SqlDataReader lectura = comando.ExecuteReader();



                while (lectura.Read())

                {

                    if (lectura["ID_FRECUENCIA_ENVIO"] != DBNull.Value)

                    {

                        lista_frecuencias.Add(new Clases.Objeto()

                        {

                            Code = lectura["FRECUENCIA_ENVIO"].ToString(),

                            Name = lectura["FRECUENCIA_ENVIO"].ToString()

                        });

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



            return lista_frecuencias;

        }





        public static List<Clases.PerfilNotificacion> obtenerPerfilesNotificacionUser(int id_usuario)

        {

            Boolean activoTemp = false;

            List<Clases.PerfilNotificacion> lista_frecuencias = new List<Clases.PerfilNotificacion>();

            SqlConnection conec = new SqlConnection(AlmaTest);

            try

            {

                conec.Open();



                SqlCommand comando = new SqlCommand("EXEC ConsultarAgendaPerfilNotificacion @IDUSUARIO;", conec);

                comando.Parameters.Add("@IDUSUARIO", SqlDbType.Int).Value = id_usuario;



                SqlDataReader lectura = comando.ExecuteReader();



                while (lectura.Read())

                {

                    if (lectura["ID_PERFIL_NOTIFICACION"] != DBNull.Value)

                    {



                        if (lectura["ACTIVO"].ToString() == "ON") activoTemp = true;

                        else activoTemp = false;



                        lista_frecuencias.Add(new Clases.PerfilNotificacion()

                        {

                            idPerfilNotificacion = Convert.ToInt32(lectura["ID_PERFIL_NOTIFICACION"]),

                            //nombrePerfil = lectura["NOMBRE_PERFIL_NOTIFICACION"].ToString(),

                            commodity = lectura["COMMODITY"].ToString(),

                            variacion_sup_temp = float.Parse(lectura["VARIACION_SUP_TEMP"].ToString()),

                            variacion_inf_temp = float.Parse(lectura["VARIACION_INF_TEMP"].ToString()),

                            //setpoint = lectura["SETPOINT_CO2"].ToString() + lectura["SETPOINT_O2"].ToString(),

                            activo = lectura["ACTIVO"].ToString(),

                            activoTrue = activoTemp



                        });

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



            return lista_frecuencias;

        }





        public static int InsertarNotificacionUsuario(int IdUsuario, Clases.PerfilNotificacion perfilNotificacion)

        {

            Clases.ReturnIngreso retorno = new Clases.ReturnIngreso();

            retorno.validar = 0;

            //int validado = 0;



            int IdPerfilNotificacion = 0;

            SqlConnection conec = new SqlConnection(AlmaTest);

            try

            {

                conec.Open();



                SqlCommand comando = new SqlCommand("EXEC InsertarNotificacionUsuario @IDUSUARIO, @commodity, @frecuencia, @variacion_sup_temp, @variacion_inf_temp;", conec);

                comando.Parameters.Add("@IDUSUARIO", SqlDbType.Int).Value = IdUsuario;



                //comando.Parameters.Add("@nombrePerfil", SqlDbType.VarChar, 100).Value = perfilNotificacion.nombrePerfil;

                comando.Parameters.Add("@commodity", SqlDbType.VarChar, 80).Value = perfilNotificacion.commodity;

                //comando.Parameters.Add("@id_setpoint", SqlDbType.Int).Value = perfilNotificacion.idSetpoint;



                comando.Parameters.Add("@frecuencia", SqlDbType.VarChar, 80).Value = perfilNotificacion.frecuencia;

                comando.Parameters.Add("@variacion_sup_temp", SqlDbType.Float).Value = perfilNotificacion.variacion_sup_temp;

                comando.Parameters.Add("@variacion_inf_temp", SqlDbType.Float).Value = perfilNotificacion.variacion_inf_temp;





                /*comando.Parameters.Add("@limite_inf_co2", SqlDbType.Int).Value = perfilNotificacion.limite_inf_co2;

                comando.Parameters.Add("@limite_sup_co2", SqlDbType.Int).Value = perfilNotificacion.limite_sup_co2;

                comando.Parameters.Add("@limite_inf_temp", SqlDbType.Int).Value = perfilNotificacion.limite_inf_temp;

                comando.Parameters.Add("@limite_sup_temp", SqlDbType.Int).Value = perfilNotificacion.limite_sup_temp;

                */

                SqlDataReader lectura = comando.ExecuteReader();



                while (lectura.Read())

                {

                    if (lectura["ID_PERFIL_NOTIFICACION"] != DBNull.Value)

                    {

                        IdPerfilNotificacion = Convert.ToInt32(lectura["ID_PERFIL_NOTIFICACION"].ToString());

                        //validado = Convert.ToInt32(lectura["VALIDADO"].ToString());

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



            //retorno.idPerfilNotificacion = IdPerfilNotificacion;

            //retorno.validar = validado;



            return IdPerfilNotificacion;

        }



        public static int EditarNotificacionUsuario(int IdUsuario, Clases.PerfilNotificacion perfilNotificacion, int id_perfil_notificacion)

        {



            int validar = 0;

            SqlConnection conec = new SqlConnection(AlmaTest);

            try

            {

                conec.Open();



                SqlCommand comando = new SqlCommand("EXEC EditarNotificacionUsuario @IDUSUARIO, @id_perfil_notificacion, @commodity, @frecuencia, @variacion_sup_temp, @variacion_inf_temp;", conec);

                comando.Parameters.Add("@IDUSUARIO", SqlDbType.Int).Value = IdUsuario;

                comando.Parameters.Add("@id_perfil_notificacion", SqlDbType.Int).Value = id_perfil_notificacion;



                comando.Parameters.Add("@commodity", SqlDbType.VarChar, 80).Value = perfilNotificacion.commodity;

                comando.Parameters.Add("@frecuencia", SqlDbType.VarChar, 80).Value = perfilNotificacion.frecuencia;

                comando.Parameters.Add("@variacion_sup_temp", SqlDbType.Float).Value = perfilNotificacion.variacion_sup_temp;

                comando.Parameters.Add("@variacion_inf_temp", SqlDbType.Float).Value = perfilNotificacion.variacion_inf_temp;



                SqlDataReader lectura = comando.ExecuteReader();



                while (lectura.Read())

                {

                    if (lectura["VALIDAR"] != DBNull.Value)

                    {

                        validar = Convert.ToInt32(lectura["VALIDAR"].ToString());

                        //validado = Convert.ToInt32(lectura["VALIDADO"].ToString());

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



            return validar;

        }



        public static int eliminarPerfilNotificacion(int idPerfilNotificacion)

        {

            int exitoso = 0;

            SqlConnection conec = new SqlConnection(AlmaTest);

            try

            {

                conec.Open();



                SqlCommand comando = new SqlCommand("EXEC EliminarPerfilNotificacion @idPerfilNotificacion;", conec);

                comando.Parameters.Add("@idPerfilNotificacion", SqlDbType.Int).Value = idPerfilNotificacion;



                SqlDataReader lectura = comando.ExecuteReader();



                conec.Close();



                exitoso = 1;



            }

            catch (Exception ex)

            {

                throw ex;

            }

            finally

            {

                conec.Close();

            }



            return exitoso;

        }







        public static Clases.PerfilNotificacion ObtenerDatosNotificacion(int IdPerfilNotificacion)

        {

            Clases.PerfilNotificacion notificacion = new Clases.PerfilNotificacion();

            SqlConnection conec = new SqlConnection(AlmaTest);

            try

            {

                conec.Open();



                SqlCommand comando = new SqlCommand("EXEC ConsultarPerfilNotificacion @IdPerfilNotificacion;", conec);

                comando.Parameters.Add("@IdPerfilNotificacion", SqlDbType.Int).Value = IdPerfilNotificacion;



                SqlDataReader lectura = comando.ExecuteReader();



                while (lectura.Read())

                {

                    if (lectura["ID_PERFIL_NOTIFICACION"] != DBNull.Value)

                    {

                        notificacion.idPerfilNotificacion = IdPerfilNotificacion;



                        notificacion.commodity = lectura["COMMODITY"].ToString();

                        notificacion.idCommodity = Convert.ToInt32(lectura["ID_COMMODITY"]);



                        notificacion.activo = lectura["ACTIVO"].ToString();

                        if (notificacion.activo == "ON") notificacion.activoTrue = true;

                        else notificacion.activoTrue = false;



                        notificacion.idFrecuencia = Convert.ToInt32(lectura["ID_FRECUENCIA_ENVIO"]);

                        notificacion.frecuencia = lectura["FRECUENCIA_ENVIO"].ToString();





                        notificacion.variacion_sup_temp = float.Parse(lectura["VARIACION_SUP_TEMP"].ToString());

                        notificacion.variacion_inf_temp = float.Parse(lectura["VARIACION_INF_TEMP"].ToString());

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



            return notificacion;

        }





        public static List<Clases.Contacto> ObtenerContactosNotificacion(int idPerfilNotificacion)

        {

            List<Clases.Contacto> listaContactos = new List<Clases.Contacto>();

            SqlConnection conec = new SqlConnection(AlmaTest);

            try

            {

                conec.Open();



                SqlCommand comando = new SqlCommand("EXEC ConsultarContactosPerfilNotificacion @idPerfilNotificacion;", conec);

                comando.Parameters.Add("@idPerfilNotificacion", SqlDbType.Int).Value = idPerfilNotificacion;



                SqlDataReader lectura = comando.ExecuteReader();



                while (lectura.Read())

                {

                    if (lectura["ID_CONTACTO_NOTIFICACION"] != DBNull.Value)

                    {

                        listaContactos.Add(new Clases.Contacto()

                        {

                            id_contacto = Convert.ToInt32(lectura["ID_CONTACTO_NOTIFICACION"]),

                            nombre_contacto = lectura["NOMBRE_CONTACTO"].ToString(),

                            email_contacto = lectura["EMAIL_CONTACTO"].ToString(),

                            cargo_contacto = lectura["CARGO_CONTACTO"].ToString(),

                            empresa_contacto = lectura["EMPRESA_CONTACTO"].ToString()



                        });

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



            return listaContactos;

        }





        public static int AgregarContactoNotificacion(Clases.Contacto contacto, int idPerfilNotificacion)

        {

            int validado = 0;



            SqlConnection conec = new SqlConnection(AlmaTest);

            try

            {

                conec.Open();



                SqlCommand comando = new SqlCommand("EXEC InsertarContactoNotificacion @idPerfilNotificacion, @nombre_contacto, @empresa_contacto, @email_contacto, @cargo_contacto;", conec);

                comando.Parameters.Add("@idPerfilNotificacion", SqlDbType.Int).Value = idPerfilNotificacion;



                comando.Parameters.Add("@nombre_contacto", SqlDbType.VarChar, 80).Value = contacto.nombre_contacto;

                comando.Parameters.Add("@empresa_contacto", SqlDbType.VarChar, 80).Value = contacto.empresa_contacto;

                comando.Parameters.Add("@email_contacto", SqlDbType.VarChar, 80).Value = contacto.email_contacto;

                comando.Parameters.Add("@cargo_contacto", SqlDbType.VarChar, 80).Value = contacto.cargo_contacto;

                SqlDataReader lectura = comando.ExecuteReader();



                conec.Close();

                validado = 1;

            }

            catch (Exception ex)

            {

                throw ex;

            }

            finally

            {

                conec.Close();

            }



            return validado;

        }



        public static int ActualizarEstadoPerfilNotificacion(int idPerfilNotificacion, Boolean estadoNotificacion)

        {

            int validado = 0;

            int estado = 0;



            if (estadoNotificacion == true) estado = 0;

            else estado = 1;



            SqlConnection conec = new SqlConnection(AlmaTest);

            try

            {

                conec.Open();



                SqlCommand comando = new SqlCommand("EXEC ActualizarEstadoPerfilNotificacion @idPerfilNotificacion, @estado;", conec);

                comando.Parameters.Add("@idPerfilNotificacion", SqlDbType.Int).Value = idPerfilNotificacion;

                comando.Parameters.Add("@estado", SqlDbType.Int).Value = estado;



                SqlDataReader lectura = comando.ExecuteReader();



                conec.Close();

                validado = 1;

            }

            catch (Exception ex)

            {

                throw ex;

            }

            finally

            {

                conec.Close();

            }



            return validado;

        }



    }

}

