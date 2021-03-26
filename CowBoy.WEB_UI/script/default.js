

$(document).ready(function () {

    $(".classData").datepicker({ dateFormat: 'dd/mm/yy', changeMonth: true, changeYear: true });

});

function cercaBoviniFemmine() {
    var txtComune = document.getElementById('ContentPlaceHolder1_txtCercaMatricola');
    if (txtComune != undefined) {
        AutoCompleteNew('ContentPlaceHolder1_txtCercaMatricolaMadre', 10, 500, window.PageMethods, 'GetListFem', 'ContentPlaceHolder1_hfCercaMadreVal', document.getElementById('ContentPlaceHolder1_txtCercaMadre'));
    }
}

function cercaBoviniMaschi() {
    var txtComune = document.getElementById('ContentPlaceHolder1_txtCercaMatricola');
    if (txtComune != undefined) {
        AutoCompleteCercaMaschi('ContentPlaceHolder1_txtCercaMatricola', 10, 500, window.PageMethods, 'GetListMas', 'ContentPlaceHolder1_hfCercaPadreVal', document.getElementById('ContentPlaceHolder1_txtCercaPadre'), document.getElementById('ContentPlaceHolder1_lstRicerca'));
    }
}

function FotoNewBovinoSelezionata() {

    var input = document.getElementById('ContentPlaceHolder1_fileInputImageNewBovino');

    var fullPath = input.value;


    if (fullPath) {
        document.getElementById("ContentPlaceHolder1_divFotoFiglio").style.visibility = "visible";
        document.getElementById('ContentPlaceHolder1_imgAfotoFilgio').href = fullPath;
        document.getElementById('ContentPlaceHolder1_imgFotoFiglio').src = fullPath;
    }
}


function PuliziaRicerche() {
    document.getElementById("ContentPlaceHolder1_txtCercaMatricola").value = '';
    document.getElementById("ContentPlaceHolder1_lstRicerca").options.length = 0;
    document.getElementById("ContentPlaceHolder1_txtCercaMatricolaMadre").value = '';
    document.getElementById("ContentPlaceHolder1_lstRicercaMadre").options.length = 0;
    return true;
}

var verificaMatricola = false;
function SaveVerificaSaveNewBovino() {


    if (isValidDate(document.getElementById('ContentPlaceHolder1_txtDataNascita')) == false) {
        return false;
    }
    else if (document.getElementById('ContentPlaceHolder1_txtNewMatricolaASL').value.trim() == "") {
        alert('Specificare la matricola USL del bovino');
        return false;
    }
    else if (document.getElementById('ContentPlaceHolder1_txtDataNascita').value.trim() == "") {

        alert('Specificare la data di nascita');
        return false;
    }
    else if (document.getElementById('ContentPlaceHolder1_chFfiglio').checked == false && document.getElementById('ContentPlaceHolder1_chMfiglio').checked == false) {
        alert('Specificare il sesso del Bovino');
        return false;
    }
    else if (document.getElementById('ContentPlaceHolder1_txtCercaMadre').value.trim() != "" && document.getElementById('ContentPlaceHolder1_hfCercaMadreVal').value.trim() == "") {
        alert('Attenzione la Matricola della MADRE inserita non è presente in archivio');
        return false;
    }
    else if (document.getElementById('ContentPlaceHolder1_txtCercaPadre').value.trim() != "" && document.getElementById('ContentPlaceHolder1_hfCercaPadreVal').value.trim() == "") {
        alert('Attenzione la Matricola del PADRE inserita non è presente in archivio');
        return false;
    }
    else if (document.getElementById('ContentPlaceHolder1_fuAllegatiNewBovino').value.trim() != "") {
        if (check_extension(document.getElementById('ContentPlaceHolder1_fuAllegatiNewBovino').value) == false)
            return false;
    }


    return true;

}


function isValidDate(dateStr) {

    var datePat = /^(\d{1,2})(\/|-)(\d{1,2})\2(\d{2}|\d{4})$/;
    //var re = /^(\d{1,2})\/(\d{1,2})\/(\d{4})$/;

    var matchArray = dateStr.value.match(datePat); // is the format ok?
    if (matchArray == null) {
        alert("il formato della data non è valido.");
        return false;
    }
    var month = matchArray[3]; // parse date into variables
    var day = matchArray[1];
    var year = matchArray[4];
    var d = new Date(month + "-" + day + "-" + year);


    if (month < 1 || month > 12) { // check month range
        alert("il mese deve essere compreso tra 1 e 12.");
        return false;
    }
    if (day < 1 || day > 31) {
        alert("Il giorno deve essere compreso tra 1 e 31.");
        return false;
    }
    if ((month == 4 || month == 6 || month == 9 || month == 11) && day == 31) {
        alert("il mese di " + month + " non ha 31 giorni!");
        return false;
    }
    if (month == 2) { // check for february 29th
        var isleap = (year % 4 == 0 && (year % 100 != 0 || year % 400 == 0));
        if (day > 29 || (day == 29 && !isleap)) {
            alert("Febbraio " + year + " non ha " + day + " giorni!");
            return false;
        }
    }
    var anno = (new Date()).getFullYear() - 15;
    if (year < anno || year > (new Date()).getFullYear()) {
        alert("L\'anno deve essere compreso tra il " + anno + " - e il " + (new Date()).getFullYear());
        //form.focus();
        return false;
    }
    var dataOggi = (new Date()).getTime();
    if (d.getTime() > dataOggi) {
        alert('Attenzione la data di nascita non può essere maggiore della data odierna');
        return false;
    }
    return true;  // date is valid
}


var _validFileExtensions = [".jpg", ".jpeg", ".bmp", ".gif", ".png"];

function check_extension(filename) {
    if (filename.length > 0) {
        var blnValid = false;
        for (var j = 0; j < _validFileExtensions.length; j++) {
            var sCurExtension = _validFileExtensions[j];
            if (filename.substr(filename.length - sCurExtension.length, sCurExtension.length).toLowerCase() == sCurExtension.toLowerCase()) {
                blnValid = true;
                break;
            }
        }
        if (!blnValid) {
            alert("Attenzione, il file " + filename + " non è valido, in allegato potete inserire solo file con estensione: " + _validFileExtensions.join(", "));
            return false;
        }
    }
    return true;
}

function VerificaOnlyMatricola(my) {
    window.PageMethods._staticInstance['CheckedMatricolaOnly'](document.getElementById('ContentPlaceHolder1_txtNewMatricolaASL').value, success, function () {
        alert('Verifica univocità della matricola fallita');

    });

}

function success(result) {
    verificaMatricola = result;
    if (verificaMatricola == false) {
        alert('Attenzione la matricola USL inserita è già presente in archivio');
        document.getElementById('ContentPlaceHolder1_txtNewMatricolaASL').value = '';
        document.getElementById('ContentPlaceHolder1_txtNewMatricolaASL').focus();
    }
}

function PuliziaPnlNewBovino() {
    document.getElementById('ContentPlaceHolder1_txtNewMatricolaASL').value = '';
    document.getElementById('ContentPlaceHolder1_txtDataNascita').value = '';
    document.getElementById('ContentPlaceHolder1_chFfiglio').checked = false; document.getElementById('ContentPlaceHolder1_chMfiglio').checked = false;
    document.getElementById('ContentPlaceHolder1_txtCercaMadre').value = '';
    document.getElementById('ContentPlaceHolder1_hfCercaMadreVal').value = '';
    document.getElementById('ContentPlaceHolder1_txtCercaPadre').value = '';
    document.getElementById('ContentPlaceHolder1_hfCercaPadreVal').value = '';
    document.getElementById('ContentPlaceHolder1_txtnNewMatricolaAzienda').value = '';
    document.getElementById('ContentPlaceHolder1_txtNewName').value = '';
    document.getElementById('ContentPlaceHolder1_txtNewName').value = '';
    document.getElementById('ContentPlaceHolder1_chToroDaMonta').checked = false; document.getElementById('ContentPlaceHolder1_chToroArtificiale').checked = false;

}

function DeleteTxtMatricola(txt) {
    txt.value = '';
}

function SerchClick(buttonName, e) {

    var key;
  
    if (window.event)
        key = window.event.keyCode;     //IE
    else
        key = e.which;     //firefox


   
    if (key == 13) {
        //Get the button the user wants to have clicked
        
        var btn = document.getElementById(buttonName);
        
        if (btn != null) { //If we find the button click it
            btn.click();
            event.keyCode = 0;
        
        }
    }
}

