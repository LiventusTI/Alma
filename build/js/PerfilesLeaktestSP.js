var perfil = $("#perfil").val();

if (perfil == 1020) {
    $("#CancelarLeaktest").hide();
    $("#DescancelarLeaktest").hide();
    $("#EliminarLeaktest").hide();
    $("#HistoricoModificaciones").hide();
}

if (perfil == 1021) {
    $("#HistoricoModificaciones").hide();
}