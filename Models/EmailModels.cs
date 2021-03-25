using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using System.Data;

namespace ProyectoAlmaInicio.Models
{
    public class EmailModels
    {
        static string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
        static string connectionAPIService = ConfigurationManager.ConnectionStrings["connectionAPIService"].ConnectionString;
        //static string connectionStringTecnica = "Server=68.169.63.233;Port=5306;Uid=liventus_sa;Pwd=L1v3nt9ss4;Database=prometeo";

        public static Clases.Usuario GetUsuarioEmail(int IdUsuario)
        {
            SqlConnection cnn;
            Clases.Usuario Usuario = new Clases.Usuario();
            cnn = new SqlConnection(connectionAPIService);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("EXEC dbo.ConsultarUsuarioEmail @USUARIO;", cnn);
                command.Parameters.Add("@USUARIO", SqlDbType.Int).Value = IdUsuario;

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Usuario.Nombre = reader["NOMBRE_CLIENTE"].ToString();
                    Usuario.TipoUsuario = reader["NOMBRE_TIPOCLIENTE"].ToString();
                    Usuario.Correo = reader["EMAIL"].ToString();
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


        public static void EmailContactoComercial(int IdUsuario, String Comentario)
        {
            Clases.Usuario Usuario = new Clases.Usuario();
            Usuario = GetUsuarioEmail(IdUsuario);

            System.Net.Mail.MailMessage mmsg = new System.Net.Mail.MailMessage();

            //Direccion de correo electronico a la que queremos enviar el mensaje
            mmsg.To.Add("soportealma@liventusglobal.com");  //mlagos@liventusglobal.com");  

            //Nota: La propiedad To es una colección que permite enviar el mensaje a más de un destinatario

            //Asunto
            mmsg.Subject = "Formulario de Contacto - Proyecto Alma";
            mmsg.SubjectEncoding = System.Text.Encoding.UTF8;

            //Cuerpo del Mensaje
            mmsg.Body = "<p>Estimados:  <p/>" +
                        "<p>El cliente <b>" + Usuario.Nombre + "</b> de tipo <b>" + Usuario.TipoUsuario + "</b> ha enviado el siguiente comentario; </p>" +
                        "<p><i>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &#34; " + Comentario + " &#34; </i><br></p>" +
                        "<p><br>En caso de requerir contactar al cliente, su email es : <b>" + Usuario.Correo + "</b>.</p>";

            mmsg.BodyEncoding = System.Text.Encoding.UTF8;
            mmsg.IsBodyHtml = true; //Si no queremos que se envíe como HTML


            //Correo electronico desde la que enviamos el mensaje
            mmsg.From = new System.Net.Mail.MailAddress("appservicios@liventusglobal.com");

            /*-------------------------CLIENTE DE CORREO----------------------*/

            //Creamos un objeto de cliente de correo
            System.Net.Mail.SmtpClient cliente = new System.Net.Mail.SmtpClient();

            //Hay que crear las credenciales del correo emisor
            cliente.Credentials = new System.Net.NetworkCredential("appservicios@liventusglobal.com", "Huc01455");

            //Lo siguiente es obligatorio si enviamos el mensaje desde Gmail
            cliente.Port = 587;
            cliente.EnableSsl = true;

            //Para Gmail "smtp.gmail.com";
            cliente.Host = "outlook.office365.com";

            /*-------------------------ENVIO DE CORREO----------------------*/

            try
            {
                //Enviamos el mensaje      
                cliente.Send(mmsg);

            }
            catch (System.Net.Mail.SmtpException ex)
            {
                return;
            }



            return;
        }
    }
}

