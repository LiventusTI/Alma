using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace ProyectoAlmaInicio.Models
{
    // Puede agregar datos del perfil del usuario agregando más propiedades a la clase ApplicationUser. Para más información, visite http://go.microsoft.com/fwlink/?LinkID=317594.
    public class UsuarioModels
    {
        static string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
        static string AlmaTest = ConfigurationManager.ConnectionStrings["AlmaTest"].ConnectionString;
        //static string connetionString = ConfigurationManager.ConnectionStrings["connetionString"].ConnectionString;

        public static Clases.Usuario VerificarUsuario(string NombreUsuario, string Contrasena)
        {
            Clases.Usuario Usuario = new Clases.Usuario();
            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("SELECT USUARIO, NOMBRE_USUARIO, APELLIDO_USUARIO, ID_USUARIO, CONTRASENA FROM USUARIO WHERE USUARIO = @user AND CONTRASENA = @pass", cnn);
                command.Parameters.AddWithValue("@user", NombreUsuario);
                command.Parameters.AddWithValue("@pass", Contrasena);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    Usuario.NombreUsuario = reader["USUARIO"].ToString();
                    Usuario.Nombre = reader["NOMBRE_USUARIO"].ToString();
                    Usuario.Apellido = reader["APELLIDO_USUARIO"].ToString();
                    Usuario.IdPerfil = Convert.ToInt32(reader["ID_USUARIO"]);
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
            /*
            Usuario.NombreUsuario = NombreUsuario;
            Usuario.Nombre = "PRUEBA";
            Usuario.Apellido = "APRUEBA";
            Usuario.IdPerfil = '1';
            */
            return Usuario;
        }

        public static Clases.Usuario GetPerfilByUser(string NombreUsuario)
        {
            Clases.Usuario Usuario = new Clases.Usuario();
            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();

                SqlCommand command = new SqlCommand("SELECT ID_USUARIO, ID_TIPO_USUARIO, NOMBRE_USUARIO, APELLIDO_USUARIO, USUARIO, EMAIL, ID_PERFIL_SECCION, ID_PERFIL_DATA  FROM USUARIO WHERE ESTADO = @estado AND USUARIO = @user", cnn);
                //SqlCommand command = new SqlCommand("SELECT U.ID_USUARIO, U.NOMBRE_USUARIO, U.APELLIDO_USUARIO, U.USUARIO, U.EMAIL, P.* FROM USUARIO U, PERFIL_SECCION P WHERE U.ID_PERFIL = P.ID_PERFIL AND U.USUARIO = @user", cnn);
                command.Parameters.AddWithValue("@user", NombreUsuario);
                command.Parameters.AddWithValue("@estado", 1);

                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    Usuario.IdPerfil = Convert.ToInt32(reader["ID_USUARIO"]);

                    Usuario.IdPerfilData = Convert.ToInt32(reader["ID_PERFIL_DATA"]);
                    Usuario.IdPerfilSeccion = Convert.ToInt32(reader["ID_PERFIL_SECCION"]);
                    Usuario.IdTipoUsuario = Convert.ToInt32(reader["ID_TIPO_USUARIO"]);

                    //if (reader["ID_SERVICEPROVIDER"] != DBNull.Value)
                    //{
                    //    Usuario.IdServiceProvider = Convert.ToInt32(reader["ID_SERVICEPROVIDER"]);
                    //}
                    //else
                    //{
                    //    Usuario.IdServiceProvider = 0;
                    //}

                    if (reader["EMAIL"] != DBNull.Value)
                    {
                        Usuario.Correo = reader["EMAIL"].ToString(); ;
                    }
                    else
                    {
                        Usuario.Correo = "";
                    }

                    //Usuario.MenuOngoing = Convert.ToInt32(reader["MENU_ONGOING"]);
                    //Usuario.MenuHistorico = Convert.ToInt32(reader["MENU_HISTORICO"]);
                    //Usuario.MapaOngoing = Convert.ToInt32(reader["MAPA_ONGOING"]);
                    //Usuario.GraficoCO2 = Convert.ToInt32(reader["GRAFICO_CO2"]);
                    //Usuario.MapaDetalleServicio = Convert.ToInt32(reader["MAPA_DETALLE_SERVICIO"]);
                    //Usuario.GraficoExternoTemp = Convert.ToInt32(reader["GRAFICO_EXTERNO_TEMP"]);
                    //Usuario.GraficoExternoCO2 = Convert.ToInt32(reader["GRAFICO_EXTERNO_CO2"]);
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


            //Usuario.IdPerfil = 1;
            ////Usuario.IdServiceProvider = 2;
            //Usuario.Correo = "MLAGOS@LIVENTUSGLOBAL.COM";

            return Usuario;
        }

        internal static Clases.Usuario GetInfoUsuario(object p)
        {
            throw new NotImplementedException();
        }

        public static Clases.Usuario GetInfoUsuario(string NombreUsuario)
        {
            Clases.Usuario Usuario = new Clases.Usuario();
            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("SELECT USUARIO, NOMBRE_USUARIO, APELLIDO_USUARIO, ID_USUARIO, CONTRASENA from USUARIO WHERE USUARIO = @user", cnn);
                command.Parameters.AddWithValue("@user", NombreUsuario);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    Usuario.NombreUsuario = reader["USUARIO"].ToString();   //LOGIN
                    Usuario.Nombre = reader["NOMBRE_USUARIO"].ToString();   //NOMBRE 
                    Usuario.Apellido = reader["APELLIDO_USUARIO"].ToString();  //APELLIDO
                    Usuario.IdPerfil = Convert.ToInt32(reader["ID_USUARIO"]);  //ID DEL PERFIL
                    Usuario.Contrasena = reader["CONTRASENA"].ToString(); //CONTRASEÑA
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
            return Usuario;
        }


        public static List<Clases.Seccion> GetPerfilSeccionesByUser(int idPerfilSeccion)
        {
            List<Clases.Seccion> Secciones = new List<Clases.Seccion>();
            SqlConnection cnn;
            cnn = new SqlConnection(AlmaTest);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("ConsultarClaseSeccionDePerfil @PerfilSeccion", cnn);
                command.Parameters.AddWithValue("@PerfilSeccion", idPerfilSeccion);

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

        public static List<string> GetPerfilOcultarColumnasByUser(int idPerfilSeccion)
        {
            List<Clases.Seccion> Secciones = new List<Clases.Seccion>();
            SqlConnection cnn;
            List<string> Columnas = new List<string>();
            cnn = new SqlConnection(AlmaTest);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("ConsultarOcultarColumnasDePerfil @PerfilSeccion", cnn);
                command.Parameters.AddWithValue("@PerfilSeccion", idPerfilSeccion);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    /*Secciones.Add(new Clases.Seccion
                    {
                        IdSeccion = Convert.ToInt32(reader["ID_SECCION"]),
                        NombreSeccion = reader["NOMBRE_SECCION"].ToString(),
                        ClaseSeccion = reader["CLASE_SECCION"].ToString(),
                        Estado = Convert.ToInt32(reader["ESTADO"])
                    });*/

                    Columnas.Add(reader["CLASE_SECCION"].ToString());

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
            return Columnas;
        }

        public static bool EditarContrasena(string NombreUsuario, string pass)
        {
            Clases.Usuario Usuario = new Clases.Usuario();
            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("UPDATE USUARIO SET CONTRASENA = @pass WHERE USUARIO = @user", cnn);
                command.Parameters.AddWithValue("@user", NombreUsuario);
                command.Parameters.AddWithValue("@pass", pass);
                //SqlDataReader reader = command.ExecuteReader();
                int validar = command.ExecuteNonQuery();
                if (validar == 0)
                {
                    return false;
                }
                else
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
                cnn.Close();
            }
        }

        public static List<Clases.ItemMenu> obtenerItemsMenu(int idPerfilSeccion)
        {
            List<Clases.ItemMenu> Lista_ItemsMenu = new List<Clases.ItemMenu>();
            SqlConnection cnn;
            cnn = new SqlConnection(AlmaTest);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("ObtenerItemsMenu @PERFIL_SECCION", cnn);
                command.Parameters.AddWithValue("@PERFIL_SECCION", idPerfilSeccion);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Lista_ItemsMenu.Add(new Clases.ItemMenu
                    {
                        id_item = Convert.ToInt32(reader["ID_ITEM"]),
                        valor = reader["VALOR"].ToString(),
                        ruta = reader["RUTA"].ToString(),
                        icono = reader["ICONO"].ToString(),
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
            return Lista_ItemsMenu;
        }

        public static List<Clases.ItemMenu> obtenerItems2Menu(int id_item)
        {
            List<Clases.ItemMenu> Lista_ItemsMenu = new List<Clases.ItemMenu>();
            SqlConnection cnn;
            cnn = new SqlConnection(AlmaTest);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("SELECT * FROM ITEM2 WHERE ID_ITEM = @id_item", cnn);
                command.Parameters.AddWithValue("@id_item", id_item);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Lista_ItemsMenu.Add(new Clases.ItemMenu
                    {
                        id_item = Convert.ToInt32(reader["ID_ITEM"]),
                        valor = reader["VALOR"].ToString(),
                        ruta = reader["RUTA"].ToString(),
                        icono = reader["ICONO"].ToString(),
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
            return Lista_ItemsMenu;
        }

        public static Clases.Usuario GetInfoUsuarioCliente(int idUsuario)
        {
            Clases.Usuario Usuario = new Clases.Usuario();
            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("SELECT USUARIO, ID_USUARIO, ID_CLIENTE from USUARIO WHERE ID_USUARIO = @iduser", cnn);
                command.Parameters.AddWithValue("@iduser", idUsuario);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    Usuario.IdPerfil = Convert.ToInt32(reader["ID_USUARIO"]);  //ID DEL PERFIL
                    Usuario.IdCliente = Convert.ToInt32(reader["ID_CLIENTE"]);  //ID DEL EXPORTADOR/NAVIERA/FFW

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
            return Usuario;
        }

        public static int GetTipoUser(int idUsuario)
        {
            Clases.Usuario Usuario = new Clases.Usuario();
            SqlConnection cnn;
            cnn = new SqlConnection(connectionString);
            int tipoUsuario = 0;

            try
            {
                cnn.Open();

                SqlCommand command = new SqlCommand("SELECT ID_TIPO_USUARIO FROM USUARIO WHERE ESTADO = @estado AND ID_USUARIO = @user", cnn);
                //SqlCommand command = new SqlCommand("SELECT U.ID_USUARIO, U.NOMBRE_USUARIO, U.APELLIDO_USUARIO, U.USUARIO, U.EMAIL, P.* FROM USUARIO U, PERFIL_SECCION P WHERE U.ID_PERFIL = P.ID_PERFIL AND U.USUARIO = @user", cnn);
                command.Parameters.AddWithValue("@user", idUsuario);
                command.Parameters.AddWithValue("@estado", 1);

                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    tipoUsuario = Convert.ToInt32(reader["ID_TIPO_USUARIO"]);  //ID TIPO PERFIL
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
            return tipoUsuario;
        }

    }

}