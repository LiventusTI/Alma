using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProyectoAlmaInicio.Models;
using Syncfusion.EJ2.Navigations;
using System.IO;
using Syncfusion.EJ2.Popups;
using Syncfusion.EJ2.PdfViewer;
using System.Web.Http;
using Newtonsoft.Json;
using System.Reflection;
using static ProyectoAlmaInicio.Controllers.SidebarController;
using static ProyectoAlmaInicio.Models.Clases;

namespace ProyectoAlmaInicio.Controllers.BI
{
    public class DocumentsController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }

        // GET: Documents
        public ActionResult DocumentsView()
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

            //llamar modelo bd
            List<Clases.Documento> documentos = new List<Clases.Documento>();
            documentos = DocumentsModels.GetDocumentos(IdUsuario);
            ViewBag.Documentos = documentos;

            // Lista Desplegable Destinatarios
            List<Clases.Objeto> lista_destinatarios = MantenedorModels.GetDestinatarios(IdUsuario);
            ViewBag.lista_destinatarios = lista_destinatarios;

            // Lista Desplegable Commodities
            List<Clases.Objeto> lista_commodities = MantenedorModels.GetCommoditiesServicioNuevo();
            ViewBag.lista_commodities = lista_commodities;

            // Lista Desplegable Tipos Revisiones
            List<Objeto> lista_tipos_revisiones = new List<Objeto>();

            Objeto tipo_revision1 = new Objeto();
            tipo_revision1.Name = "Origen";
            tipo_revision1.Code = "1";

            Objeto tipo_revision2 = new Objeto();
            tipo_revision2.Name = "Destino";
            tipo_revision2.Code = "2";

            Objeto tipo_revision3 = new Objeto();
            tipo_revision3.Name = "Otro";
            tipo_revision3.Code = "3";

            lista_tipos_revisiones.Add(tipo_revision1);
            lista_tipos_revisiones.Add(tipo_revision2);
            lista_tipos_revisiones.Add(tipo_revision3);
            ViewBag.lista_tipos_revisiones = lista_tipos_revisiones;

            // Lista Desplegable Contenedores
            List<Clases.Objeto> lista_contenedores = MantenedorModels.GetContenedoresServicioNuevo();
            ViewBag.lista_contenedores = lista_contenedores;


            return View();
        }



        public ActionResult PDFView()
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

            return View();
        }

        public FileResult Download(string fileName)
        {
            //string filePath = @"C:\Users\LIVENTUS\source\repos\Proyecto Alma 10-08-2020\Proyecto Alma\ProyectoAlmaInicio\RepositorioAlma\" + fileName;
            string filePath = @"E:\ProyectoAlma\RepositorioAlma\" + fileName;
            var file = File(filePath, System.Net.Mime.MediaTypeNames.Application.Pdf, fileName);
            return file;
        }

        public JsonResult Upload()
        {
            for (int i = 0; i < Request.Files.Count; i++)
            {
                HttpPostedFileBase file = Request.Files[i];
                int fileSize = file.ContentLength;
                string fileName = file.FileName;
                string mimeType = file.ContentType;
                System.IO.Stream fileContent = file.InputStream;

                try
                {
                    file.SaveAs(Server.MapPath("~/RepositorioAlma/") + fileName);
                }
                catch (Exception ex)
                {
                    return Json("No se logró subir el archivo. /n/nExcepción: " + ex.ToString());
                }
            }

            return Json("Se subió el archivo con éxito");
        }

        public JsonResult DeleteFile(string fileName)
        {
            string fullPath = Request.MapPath("~/RepositorioAlma/" + fileName);
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }

            return Json("Se eliminó el archivo con éxito");
        }


        public JsonResult GuardarDocumento(string nombre_doc, string tipo_doc, string ruta_doc, int peso_doc, string destinatario, string commodity, string tipo_revision, string lugar_revision, string fecha_revision, string empresa, string notas, string[] contenedores)
        {
            int respuesta = 0;
            int IdUsuario = Convert.ToInt32(Session["PerfilInterior"]);
            int id_documento = DocumentsModels.GuardarDocumento(nombre_doc, tipo_doc, ruta_doc, peso_doc, IdUsuario, destinatario, commodity, tipo_revision, lugar_revision, fecha_revision, empresa, notas);
            if (id_documento > 0 && contenedores != null)
            {
                for (int i = 0; i < contenedores.Count(); i++)
                {
                    respuesta = DocumentsModels.GuardarContenedorDocumento(id_documento, contenedores[i]);
                }
            }

            respuesta = id_documento;
            var dato = Json(respuesta, JsonRequestBehavior.AllowGet);
            dato.MaxJsonLength = Int32.MaxValue;

            return dato;
        }

        public JsonResult BorrarDocumento(int id_doc)
        {
            int respuesta = 0;
            int IdUsuario = Convert.ToInt32(Session["PerfilInterior"]);
            int id_documento = DocumentsModels.BorrarDocumento(id_doc);
            if (id_documento > 0)
            {
                respuesta = id_documento;
            }

            var dato = Json(respuesta, JsonRequestBehavior.AllowGet);
            dato.MaxJsonLength = Int32.MaxValue;
            return dato;
        }

        public JsonResult ValidarArchivoExistente(string nombre_archivo)
        {
            int respuesta = 0;
            respuesta = DocumentsModels.ValidarArchivoExistente(nombre_archivo);

            var dato = Json(respuesta, JsonRequestBehavior.AllowGet);
            dato.MaxJsonLength = Int32.MaxValue;
            return dato;
        }
        
    }
    public class jsonObjects
    {
        public string document { get; set; }
        public string password { get; set; }
        public string zoomFactor { get; set; }
        public string isFileName { get; set; }
        public string xCoordinate { get; set; }
        public string yCoordinate { get; set; }
        public string pageNumber { get; set; }
        public string documentId { get; set; }
        public string hashId { get; set; }
        public string sizeX { get; set; }
        public string sizeY { get; set; }
        public string startPage { get; set; }
        public string endPage { get; set; }
    }
}