$.ajax({
    type: "POST",
    url: '/Usuario/GetSeccionesByUser',
    dataType: 'json',
    dataSrc: '',
    async: false,
    success: function (secciones) {
        if (secciones != null) {
            for (var seccion in secciones) {
                if (secciones.hasOwnProperty(seccion)) {

                    var dato = secciones[seccion].ClaseSeccion;
                    var estado = secciones[seccion].Estado;

                    if (dato != '.nada' && estado == 1)
                        $(dato).hide();
                    if (estado == 2)
                        $(dato).show();

                }
            }
        }
    }
});
/*
var perfil = $("#perfil").val();
var menuongoing = $("#menuongoing").val();
var menuhistorico = $("#menuhistorico").val();
var mapaongoing = $("#mapaongoing").val();
var graficoco2 = $("#graficoco2").val();
var mapadetalleservicio = $("#mapadetalleservicio").val();
var graficoexternotemp = $("#graficoexternotemp").val();
var graficoexternoco2 = $("#graficoexternoco2").val();


if (menuongoing == 0) { $(".misserviciosOngoing").hide(); }
if (menuhistorico == 0) { $(".misserviciosHistorico").hide(); }
if (mapaongoing == 0) { $(".cpestana2").hide(); $(".pestana2").hide(); }
if (graficoco2 == 0) { $(".divgraficoCO2").hide(); }
if (mapadetalleservicio == 0) { $(".divmapaLogistico").hide(); }
if (graficoexternotemp == 0) { $(".divgraficoCO2").hide(); }
if (graficoexternoco2 == 0) { $(".divgraficoCO2").hide(); }
*/
//if(perfil == 1){
//    $(".crearsolicitud").hide();
//    $(".gestionarreservaEdi").hide();  
//    $(".menulogistca").hide();
//    $(".menuestadistica").hide();
//    $(".menuclaims").hide();
//    $(".gestiondedatos").hide();
//}
//if (perfil == 2) {
//    $(".menulogistca").hide();
//    $(".menuestadistica").hide();
//    $(".menuclaims").hide();
//}
//if (perfil == 3) {
//    $(".menulogistca").hide();
//    $(".menuestadistica").hide();
//    $(".menuclaims").hide();
//}
//if (perfil == 4) {
//    $(".menulogistca").hide();
//    $(".menuclaims").hide();
//}

//if (perfil == 5) {
//    $(".gestionleaktest").hide();
//    $(".gestionarreservaEdi").hide();
//    $(".menuclaims").hide();
//}

//if (perfil == 6) {
//    $(".gestionleaktest").hide();
//    $(".gestionarreservaEdi").hide();
//    $(".menuclaims").hide();
//}

//if (perfil == 7) {
//    $(".crearsolicitud").hide();
//    $(".gestionarsolicitud").hide();
//    $(".inventariocontenedores").hide();
//    $(".disponiblescontenedores").hide();
//    $(".crearreserva").hide();
//}

//if (perfil == 8) {   
//    $(".gestionleaktest").hide();
//    $(".gestioncontenedores").hide();
//    $(".gestionarreservaEdi").hide();
//    $(".menuestadistica").hide();
//    $(".menuclaims").hide();
//    $(".gestiondedatos").hide();
//}

//if (perfil == 9) {
//    $(".gestionleaktest").hide();
//    $(".gestioncontenedores").hide();
//    $(".gestionarreservaEdi").hide();
//    $(".menuestadistica").hide();
//    $(".menuclaims").hide();
//    $(".gestiondedatos").hide();
//}

//if (perfil == 10) {
//    $(".crearsolicitud").hide();
//    $(".gestionarsolicitud").hide();
//    $(".gestionarreservaEdi").hide();
//    $(".menulogistca").hide();
//    $(".menuestadistica").hide();
//    $(".menuclaims").hide();
//    $(".gestiondedatos").hide();
//}

//if (perfil == 11) {

//}

//if (perfil == 12) {

//}

//if (perfil == 13) {

//}

//if (perfil == 14) {
//    $(".gestionleaktest").hide();
//    $(".gestioncontenedores").hide();
//    $(".gestionarreservaEdi").hide();
//    $(".menuestadistica").hide();
//    $(".menuclaims").hide();
//    $(".gestiondedatos").hide();
//}

//if (perfil == 16) {
//    $(".gestionleaktest").hide();
//    $(".gestioncontenedores").hide();
//    $(".menulogistca").hide();
//    $(".menuestadistica").hide();
//    $(".menuclaims").hide();
//    $(".gestiondedatos").hide();
//}


//if (perfil == 18) {

//}

//if (perfil == 19) {
//    $(".crearsolicitud").hide();
//    $(".gestionarsolicitud").hide();
//	$(".gestionarreservaEdi").hide();
//	$(".menuestadistica").hide();
//	$(".menuclaims").hide();
//	$(".gestiondedatos").hide();
//	$("#Tecnica").hide();
//	$("#DMS").hide();
//	$("#CRM").hide();
//	$("#PowerBI").hide();
//	$("#Planificacion").hide();
//	$("#ALAB").hide();
//	$("#botones").hide();
//}

//if (perfil == 1020 || perfil == 1021 || perfil == 1022) {
//    $(".mantenedorantepuerto").hide();
//    $(".mantenedorcourier").hide();
//    $(".mantenedorfreightforwarder").hide();
//    $(".mantenedorretrievalprovider").hide();
//    $(".mantenedorserviceprovider").hide();
//    $(".mantenedormaquinaria").hide();
//    $(".mantenedorpais").hide();
//    $(".mantenedornaviera").hide();
//    $(".mantenedornaviera").hide();
//}

//if (perfil == 1020 || perfil == 1021) {
//    $(".gestiondedatos").hide();
//}

