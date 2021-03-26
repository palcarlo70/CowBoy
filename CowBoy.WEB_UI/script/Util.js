//var margini = 105;

$(document).ready(function () {
    //checkBrowser();
    //calcolaAltezze();
    //$(".classData").datepicker({ dateFormat: 'dd/mm/yy', changeMonth: true, changeYear: true });

    $(".classDataDA").datepicker({
        //defaultDate: "+1w",
        //changeMonth: true,
        //numberOfMonths: 2,
        onClose: function (selectedDate) {
            $(".classDataA").datepicker("option", "minDate", selectedDate);
        },
        dateFormat: 'dd/mm/yy', changeMonth: true, changeYear: true
    });
    $(".classDataA").datepicker({
        //defaultDate: "+1w",
        //changeMonth: true,
        //numberOfMonths: 2,
        onClose: function (selectedDate) {
            $(".classDataDA").datepicker("option", "maxDate", selectedDate);
        },
        dateFormat: 'dd/mm/yy', changeMonth: true, changeYear: true
    });
});

//$(window).resize(function() {
//    calcolaAltezze();
//});

///** CALCOLA LE ALTEZZE DEI DIV CHE STRUTTURANO LA PAGINA**/
//function calcolaAltezze() {
//    $("#divDettaglioBug").height($(window).height() - $("#HEADER").outerHeight() - $("#FOOTER").outerHeight() - 25);
//    $("#divGrilgiaRicercaBug, #divNotifiche").height($(window).height() - $("#HEADER").outerHeight() - $("#FOOTER").outerHeight() /*- margini*/ - $("#divFiltriRicercaBug").outerHeight());
//    $("#divGrilgiaRicercaAttivita, #divNotifiche").height($(window).height() - $("#HEADER").outerHeight() - $("#FOOTER").outerHeight() /*- margini*/ - $("#divFiltriRicercaAttivita").outerHeight());
//}

//function checkBrowser() {
//    $.browser = {};
//    $.browser.mozilla = /mozilla/.test(navigator.userAgent.toLowerCase()) && !/webkit/.test(navigator.userAgent.toLowerCase());
//    $.browser.webkit = /webkit/.test(navigator.userAgent.toLowerCase());
//    $.browser.opera = /opera/.test(navigator.userAgent.toLowerCase());
//    $.browser.msie = /msie/.test(navigator.userAgent.toLowerCase());
//    $.browser.chrome = /chrome/.test(navigator.userAgent.toLowerCase());

//    //var userAgent = navigator.userAgent.toLowerCase();
//    // Is this a version of IE?
//    if ($.browser.msie) { margini = 110; }
//    // Is this a version of Chrome?
//    if ($.browser.chrome) { margini = 85; }
//    // Is this a version of Safari?
//    if ($.browser.safari) {}
//    // Is this a version of firefox?
//    if ($.browser.mozilla) {}
//}

////function onOverLogo(check) {
////    if(check==1)
////        $("#logo").attr('src', '../Immagini/Bug_Nero.png');
////    else
////        $("#logo").attr('src', '../Immagini/Bug_Bianco.png');
////}

