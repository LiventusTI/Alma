using ProyectoAlmaInicio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static ProyectoAlmaInicio.Controllers.SidebarController;

namespace ProyectoAlmaInicio.Controllers.BI
{
    public class BIController : Controller
    {
        // GET: BI
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult InformeBIView()
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

            Clases.Usuario usuario = UsuarioModels.GetInfoUsuarioCliente(IdUsuario);

            if (usuario.IdCliente == 1582)
            {
                ViewBag.src = "https://app.powerbi.com/view?r=eyJrIjoiZTcyYWVkYzMtNzAwNy00NDI5LThiYjMtMTg1ZGFiMWViMzQ3IiwidCI6IjUxOGZkMGU5LTA1MTEtNGYzZC1hMmZhLWIxN2ZjOGM0YTNlYiIsImMiOjR9";
                //"https://app.powerbi.com/reportEmbed?reportId=3d4101f5-2b58-4492-be61-fd2e33e913e0&groupId=09cb5223-1eb5-45c5-84cc-b9afd2b559b8&autoAuth=true&ctid=518fd0e9-0511-4f3d-a2fa-b17fc8c4a3eb&config=eyJjbHVzdGVyVXJsIjoiaHR0cHM6Ly93YWJpLXNvdXRoLWNlbnRyYWwtdXMtcmVkaXJlY3QuYW5hbHlzaXMud2luZG93cy5uZXQvIn0%3D";
                //"https://app.powerbi.com/view?r=eyJrIjoiNmRlM2U2NmMtMjJlZS00OWYyLWEzMzYtYjlhMTk4ZTNlOTBiIiwidCI6IjUxOGZkMGU5LTA1MTEtNGYzZC1hMmZhLWIxN2ZjOGM0YTNlYiIsImMiOjR9&pageName=ReportSectione390f9c84a0a48b89d31";
                //"https://app.powerbi.com/view?r=eyJrIjoiYmQ1OGQxOTEtN2I3OS00MDRkLWI3NGItMzIzN2Q4YTQ5Yzc5IiwidCI6IjUxOGZkMGU5LTA1MTEtNGYzZC1hMmZhLWIxN2ZjOGM0YTNlYiIsImMiOjR9&pageName=ReportSectione390f9c84a0a48b89d31";
            }
            else if (usuario.IdCliente == 1232)
            {
                ViewBag.src = "https://app.powerbi.com/view?r=eyJrIjoiYTAwMWFmNTktNjVjMi00ZmYxLTljNmUtYTQxZTNhNjVkN2FjIiwidCI6IjUxOGZkMGU5LTA1MTEtNGYzZC1hMmZhLWIxN2ZjOGM0YTNlYiIsImMiOjR9";
            }
            else if (usuario.IdPerfil == 39)
            {
                ViewBag.src = "https://app.powerbi.com/view?r=eyJrIjoiMjRjMWJhN2EtNTZmNC00NDE4LTk4OGUtNmNlYTA0ZjQyNjVjIiwidCI6IjUxOGZkMGU5LTA1MTEtNGYzZC1hMmZhLWIxN2ZjOGM0YTNlYiIsImMiOjR9&pageName=ReportSectione390f9c84a0a48b89d31";
            }
            else
            {
                ViewBag.src = "";
            }

            //            ViewBag.idCliente = ;
            //***** FIN CARGA DE MENU PRINCIPAL *****//

            return View();
        }
    }
}