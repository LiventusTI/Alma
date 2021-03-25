using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace ProyectoAlmaInicio.Models
{
    // Puede agregar datos del perfil del usuario agregando más propiedades a la clase ApplicationUser. Para más información, visite http://go.microsoft.com/fwlink/?LinkID=317594.
    public class DocumentsModels
    {
        static string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
        static string connectionAPIService = ConfigurationManager.ConnectionStrings["connectionAPIService"].ConnectionString;
        static string AlmaTest = ConfigurationManager.ConnectionStrings["AlmaTest"].ConnectionString;
        static string MacaTest = ConfigurationManager.ConnectionStrings["MacaTest"].ConnectionString;

        static string JorgeTest = ConfigurationManager.ConnectionStrings["JorgeTest"].ConnectionString;
        static string connectionStringTecnica = "Server=68.169.63.233;Port=5306;Uid=liventus_sa;Pwd=L1v3nt9ss4;Database=prometeo";

        static string apiKeyEmerson = "088B734E-321D-43A5-87BE-6D7C7633576F";//"DA639473-40DE-4216-84AB-9BB513C2AC7A";


        public static List<Clases.Documento> GetDocumentos(int idPerfilUsuario)
        {
            List<Clases.Documento> Documentos = new List<Clases.Documento>();
            SqlConnection cnn;
            cnn = new SqlConnection(AlmaTest);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("ConsultarDocumentosPorUsuario @PerfilUsuario", cnn);
                command.Parameters.AddWithValue("@PerfilUsuario", idPerfilUsuario);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    DateTime? fecha_revision = null;

                    if (reader["FECHA_REVISION"] != DBNull.Value)
                    {
                        fecha_revision = Convert.ToDateTime(reader["FECHA_REVISION"]);
                    }



                    string nombre_archivo = reader["NOMBRE_DOCUMENTO"].ToString();
                    string tipo_archivo = nombre_archivo.Split('.')[1];

                    string tipo_icono = "file";
                    string tipo_icono_pdf = "file-pdf-o";
                    string tipo_icono_word = "file-word-o";
                    string tipo_icono_excel = "file-excel-o";
                    string tipo_icono_texto = "file-text";
                    string tipo_icono_imagen = "file-image-o";
                    string tipo_icono_video = "file-video-o";

                    if (tipo_archivo == "pdf")
                    {
                        tipo_icono = tipo_icono_pdf;
                    }
                    else if (tipo_archivo == "docx")
                    {
                        tipo_icono = tipo_icono_word;
                    }
                    else if (tipo_archivo == "xlsx")
                    {
                        tipo_icono = tipo_icono_excel;
                    }
                    else if (tipo_archivo == "txt")
                    {
                        tipo_icono = tipo_icono_texto;
                    }
                    else if (tipo_archivo == "png" || tipo_archivo == "jpeg")
                    {
                        tipo_icono = tipo_icono_imagen;
                    }
                    else if (tipo_archivo == "mp4")
                    {
                        tipo_icono = tipo_icono_video;
                    }

                    Documentos.Add(new Clases.Documento
                    {
                        IdDocumento = Convert.ToInt32(reader["ID_DOCUMENTO"]),
                        NombreDocumento = reader["NOMBRE_DOCUMENTO"].ToString(),
                        TipoDocumento = reader["TIPO_DOCUMENTO"].ToString(),
                        Tipo = "." + tipo_archivo,
                        TipoIcono = tipo_icono,
                        Path = reader["RUTA_DOCUMENTO"].ToString(),
                        Destinatario = reader["DESTINATARIO"].ToString(),
                        Commodity = reader["COMMODITY"].ToString(),
                        TipoRevision = reader["TIPO_REVISION"].ToString(),
                        LugarRevision = reader["LUGAR_REVISION"].ToString(),
                        FechaRevision = fecha_revision,
                        Empresa = reader["EMPRESA"].ToString(),
                        Contenedores = reader["CONTENEDORES"].ToString(),
                        Notas = reader["NOTA"].ToString()
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
            return Documentos;
        }

        public static int GuardarDocumento(string nombre_doc, string tipo_doc, string ruta_doc, int peso_doc, int id_usuario, string destinatario, string commodity, string tipo_revision, string lugar_revision, string fecha_revision, string empresa, string notas)
        {
            int respuesta = 0;
            DateTime? fecha = Convert.ToDateTime("01-01-1900 00:00:00");
            if (fecha_revision != "")
            {
                fecha = Convert.ToDateTime(fecha_revision);
            }


            SqlConnection cnn;
            cnn = new SqlConnection(AlmaTest);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("EXEC dbo.GuardarDocumento @nombre_doc, @tipo_doc, @ruta_doc, @peso_doc, @id_usuario, @destinatario, @commodity, @tipo_revision, @lugar_revision, @fecha_revision, @empresa, @notas;", cnn);
                command.Parameters.Add("@nombre_doc", SqlDbType.NVarChar).Value = nombre_doc;
                command.Parameters.Add("@tipo_doc", SqlDbType.NVarChar).Value = tipo_doc;
                command.Parameters.Add("@ruta_doc", SqlDbType.NVarChar).Value = ruta_doc;
                command.Parameters.Add("@peso_doc", SqlDbType.Int).Value = peso_doc;
                command.Parameters.Add("@id_usuario", SqlDbType.Int).Value = id_usuario;
                command.Parameters.Add("@destinatario", SqlDbType.NVarChar).Value = destinatario;
                command.Parameters.Add("@commodity", SqlDbType.NVarChar).Value = commodity;
                command.Parameters.Add("@tipo_revision", SqlDbType.NChar).Value = tipo_revision;
                command.Parameters.Add("@lugar_revision", SqlDbType.NVarChar).Value = lugar_revision;
                command.Parameters.Add("@fecha_revision", SqlDbType.DateTime).Value = fecha;
                command.Parameters.Add("@empresa", SqlDbType.NVarChar).Value = empresa;
                command.Parameters.Add("@notas", SqlDbType.NVarChar).Value = notas;

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

        public static int GuardarContenedorDocumento(int id_doc, string contenedor)
        {
            int respuesta = 0;

            SqlConnection cnn;
            cnn = new SqlConnection(AlmaTest);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("EXEC dbo.GuardarContenedorDocumento @id_doc, @contenedor;", cnn);
                command.Parameters.Add("@id_doc", SqlDbType.Int).Value = id_doc;
                command.Parameters.Add("@contenedor", SqlDbType.NVarChar).Value = contenedor;

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

        public static int BorrarDocumento(int id_doc)
        {
            int respuesta = 0;

            SqlConnection cnn;
            cnn = new SqlConnection(AlmaTest);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("EXEC dbo.BorrarDocumento @id_doc;", cnn);
                command.Parameters.Add("@id_doc", SqlDbType.Int).Value = id_doc;

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

        public static int ValidarArchivoExistente(string nombre_archivo)
        {
            int respuesta = 0;

            SqlConnection cnn;
            cnn = new SqlConnection(AlmaTest);
            try
            {
                cnn.Open();
                SqlCommand command = new SqlCommand("EXEC dbo.ValidarArchivoExistente @nombre_archivo;", cnn);
                command.Parameters.Add("@nombre_archivo", SqlDbType.NVarChar).Value = nombre_archivo;

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

    }
}