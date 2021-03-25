using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using System.Data;
using System.Web.Mvc;

namespace ProyectoAlmaInicio.Models
{
    public class TrazabilidadModels
    {
        static string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
        static string connectionAPIService = ConfigurationManager.ConnectionStrings["connectionAPIService"].ConnectionString;
        static string connectionStringTecnica = "Server=68.169.63.233;Port=5306;Uid=liventus_sa;Pwd=L1v3nt9ss4;Database=prometeo";

        public static int SetTrazabilidadSesion(int IdUsuario)
        {
            int IdSesion = 0;
            SqlConnection cnn, cnn2;
            List<Clases.Naviera> Naviera = new List<Clases.Naviera>();
            cnn = new SqlConnection(connectionString);
            cnn2 = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("INSERT INTO [DBAlma].[dbo].[SESION] VALUES (@IDUSUARIO, getdate());", cnn);
                command.Parameters.Add("@IDUSUARIO", SqlDbType.Int).Value = IdUsuario;

                int validar = command.ExecuteNonQuery();
                cnn.Close();

                if (validar == 1)
                {
                    cnn2.Open();
                    SqlCommand command2 = new SqlCommand("SELECT MAX(ID_SESION) AS ID_SESION FROM [DBAlma].[dbo].[SESION] WHERE ID_USUARIO = @IDUSUARIO;", cnn2);
                    command2.Parameters.Add("@IDUSUARIO", SqlDbType.Int).Value = IdUsuario;

                    SqlDataReader reader2 = command2.ExecuteReader();
                    while (reader2.Read())
                    {
                        IdSesion = Convert.ToInt32(reader2["ID_SESION"]);
                    }

                    return IdSesion;
                }
                else
                {
                    return 0;
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

        public static int SetTrazabilidadVista(int TipoVista, int Sesion, String NombreVista)
        {
            SqlConnection cnn;
            //List<Clases.Naviera> Naviera = new List<Clases.Naviera>();
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("INSERT INTO [DBAlma].[dbo].[OBJETO_VISITADO] VALUES (@IDSESION,@TIPOVISTA,@NOMBREOBJETO,getdate());", cnn);
                command.Parameters.Add("@IDSESION", SqlDbType.Int).Value = Sesion;
                command.Parameters.Add("@TIPOVISTA", SqlDbType.Int).Value = TipoVista;
                command.Parameters.Add("@NOMBREOBJETO", SqlDbType.VarChar, 50).Value = NombreVista;

                int validar = command.ExecuteNonQuery();
                return validar;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnn.Close();
            }
        }




    }
}

