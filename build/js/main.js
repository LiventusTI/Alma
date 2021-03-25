// preparing language file
var aLangKeys=new Array();
aLangKeys['es']=new Array();
aLangKeys['en']=new Array();
aLangKeys['es']['inicio'] = 'Inicio Sesión';
aLangKeys['en']['inicio'] = 'Login';
aLangKeys['es']['usuario'] = 'Nombre Usuario';
aLangKeys['en']['usuario'] = 'User Name';
aLangKeys['es']['contrasena'] = 'Contraseña';
aLangKeys['en']['contrasena'] = 'Password';
aLangKeys['es']['idioma'] = 'Idioma';
aLangKeys['en']['idioma'] = 'Language';
aLangKeys['es']['botoninicio'] = 'Entrar';
aLangKeys['en']['botoninicio'] = 'Sign in';
aLangKeys['es']['proyectoAlma'] = 'Proyecto Alma ';
aLangKeys['en']['proyectoAlma'] = 'Alma Proyect ';
aLangKeys['es']['derechosReservados'] = '© 2019 Todos los Derechos Reservados';
aLangKeys['en']['derechosReservados'] = '© 2019 All Rights Reserved';

aLangKeys['en']['pruebaJorge'] = "Paso la prueba!!!!!"; //"Contactar a Equipo de Soporte";
aLangKeys['en']['tituloEnTransito'] = "ON GOING SERVICES"; //'SERVICIOS ON GOING';


$(document).ready(function () {
    // onclick behavior
    var Lenguaje = 'es';
    var text = "Lenguaje";
    setCookie(text, Lenguaje);

$('#idioma').change(function () {
        //var lang = $(this).attr('id'); // obtain language id
        var lang = $(this).val();
        
        // translate all translatable elements
        if (lang == "es_ES") {
            $("#idioma").val("es_ES");
            Lenguaje = "es";
        } else {
            $("#idioma").val("en_GB");
            Lenguaje = "en";
        }
        setCookie(text, Lenguaje);

        $('.tr').each(function(i){
            $(this).text(aLangKeys[Lenguaje][$(this).attr('key')]);
        });
});

function setCookie(name,value) {
    document.cookie = name + "=" + value + ";path=/";
}

function getCookie(lang) {
    var name = lang + "=";
    var decodedCookie = decodeURIComponent(document.cookie);
    var ca = decodedCookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') {
            c = c.substring(1);
        }
        if (c.indexOf(name) == 0) {
            return c.substring(name.length, c.length);
        }
    }
    return "";
}

});