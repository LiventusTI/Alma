using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProyectoAlmaInicio.Models;
using System.Web.UI;
using ProyectoAlmaInicio.Controllers.Trazabilidad;
using static ProyectoAlmaInicio.Controllers.SidebarController;

namespace ProyectoAlmaInicio.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(string user, string pass)
        {
            Clases.Usuario Usuario = new Clases.Usuario();
            Clases.Usuario usuario = new Clases.Usuario();
            if (user == null && pass == null)
            {
                Session.Add("User", Session["User"]);
                Session.Add("Pass", Session["Pass"]);
                Session.Add("Nombre", Session["Nombre"]);
                Session.Add("Perfil", Session["Perfil"]);
                Session.Add("PerfilInterior", Session["PerfilInterior"]);
                //Session.Add("SP", Session["SP"]);
                Session.Add("Correo", Session["Correo"]);
                Session.Add("PerfilData", Session["perfilData"]);
                Session.Add("PerfilSecciones", Session["perfilSeccion"]);
                return View();
            }

            if (user == "admin")
            {
                string auxuser = user;
            }
            else
            {
                string auxuser = user.ToUpper();
            }

            Usuario = UsuarioModels.VerificarUsuario(user, pass);
            Session.Add("Nombre", Usuario.Nombre);
            Session.Add("Perfil", Usuario.IdPerfil);

            int perfilaplicacion = 0;
            int perfilSeccion = 0;
            int perfilData = 0;
            if (Usuario.NombreUsuario != null)
            {
                
                usuario = UsuarioModels.GetPerfilByUser(Usuario.NombreUsuario);
                perfilaplicacion = usuario.IdPerfil;
                Session.Add("PerfilInterior", perfilaplicacion);
                Session.Add("Correo", usuario.Correo);

                perfilData = usuario.IdPerfilData;
                perfilSeccion = usuario.IdPerfilSeccion;

                Session.Add("PerfilData", perfilData);
                Session.Add("PerfilSecciones", perfilSeccion);

            }
            else
            {
                Response.Write("<script>alert('Usuario o Contraseña Incorrectos');</script>");
                return View("Login");

            }

            if (Usuario.NombreUsuario != null)
            {


                if ((user == null) || (user == ""))
                {
                    Session.Add("User", Session["User"]);
                }
                else
                {
                    Session.Add("User", user);
                    //add a username Cookie
                    Response.Cookies["User"].Value = user;
                    Response.Cookies["User"].Expires = DateTime.Now.AddDays(10);
                }

                if ((pass == null) || (pass == ""))
                {
                    Session.Add("Pass", Session["Pass"]);
                }
                else
                {
                    Session.Add("Pass", pass);
                }

                if (Session["User"] == null)
                {
                    return View("Login");
                }

                if (Session["Nombre"] == null)
                {
                    return View("Login");
                }
                
                    Session.Add("Nombre", Usuario.Nombre + " " + Usuario.Apellido);

                    int IdUsuario = Convert.ToInt32(Session["PerfilInterior"]);
                    int sesion = TrazabilidadModels.SetTrazabilidadSesion(IdUsuario);
                    Session.Add("Sesion", sesion);
                //return View("../TransitoEnCurso/TransitoEnCursoView");
                // return RedirectToAction("TransitoEnCursoView");
                if (usuario.IdTipoUsuario == 4 && IdUsuario != 39)
                {
                    return RedirectToAction("TransitoEnCursoComercialView", "TransitoEnCursoComercial", new { area = "" });
                    //return View(modelValue);
                }
                else
                {
                    return RedirectToAction("TransitoEnCursoView", "TransitoEnCurso", new { area = "" });
                }

            }
            else
            {

                Response.Write("<script>alert('Usuario o Contraseña Incorrectos');</script>");
                return View("Login");
            }
        }

        public ActionResult Login()
        {
            //setear en cero//

            Session["User"] = null;
            Session["PerfilInterior"] = 0;
            Session["Nombre"] = null;
            Session["Pass"] = null;
            Session["Perfil"] = null;
            Session["Correo"] = null;
            Session["perfilData"] = 0;
            Session["perfilSeccion"] = 0;


            return View(); //return View("../Home/Login");
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult ModificarPass()
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

            Clases.Usuario Usuario = new Clases.Usuario();
            Usuario = UsuarioModels.GetInfoUsuario(Session["user"].ToString().ToUpper());
            ViewBag.NombreUsuario = Usuario.NombreUsuario;
            ViewBag.Nombre = Usuario.Nombre;
            ViewBag.Contrasena = Usuario.Contrasena;
            ViewBag.ConfirmarContrasena = Usuario.Contrasena;
            ViewBag.Apellido = Usuario.Apellido;
            ViewBag.IdPerfil = Usuario.IdPerfil;

            

            return View();
        }

        public ActionResult CorreoSoporte()
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

            return View();
        }

        public class spacingModel
        {
            public double[] cellSpacing { get; set; }
            public double aspectRatio { get; set; }
        }


    }
}