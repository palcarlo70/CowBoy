$(document).ready(function () {

    $(".classData").datepicker({ dateFormat: 'dd/mm/yy', changeMonth: true, changeYear: true });







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

//function cercaBoviniFemmine() {
//    var txtComune = document.getElementById('ContentPlaceHolder1_hfCercaMadreVal');
//    if (txtComune !== undefined) {
//        AutoCompleteNew('ContentPlaceHolder1_txtCercaMadre', 10, 500, window.PageMethods, 'GetListFem', 'ContentPlaceHolder1_hfCercaMadreVal');
//    }
//}

//function cercaBoviniMaschi() {
//    var txtComune = document.getElementById('ContentPlaceHolder1_hfCercaMadreVal');
//    if (txtComune !== undefined) {
//        AutoCompleteCercaMaschi('ContentPlaceHolder1_txtCercaPadre', 10, 500, window.PageMethods, 'GetListMas', 'ContentPlaceHolder1_hfCercaMadreVal');
//    }
//}

function cercaBoviniFemmine() {
    var txtComune = document.getElementById('ContentPlaceHolder1_txtCercaMatricola');
    if (txtComune !== undefined) {
        AutoCompleteNew('ContentPlaceHolder1_txtCercaMatricolaMadre', 10, 500, window.PageMethods, 'GetListFem', 'ContentPlaceHolder1_hfCercaMadreVal', document.getElementById('ContentPlaceHolder1_txtCercaMadre'));
    }
}

function cercaBoviniMaschi() {
    var txtComune = document.getElementById('ContentPlaceHolder1_txtCercaMatricola');
    if (txtComune !== undefined) {
        AutoCompleteCercaMaschi('ContentPlaceHolder1_txtCercaMatricola', 10, 500, window.PageMethods, 'GetListMas', 'ContentPlaceHolder1_hfCercaPadreVal', document.getElementById('ContentPlaceHolder1_txtCercaPadre'), document.getElementById('ContentPlaceHolder1_lstRicerca'));
    }
}

function cercaBoviniMaschiNewParto() {
    var txtComune = document.getElementById('ContentPlaceHolder1_txtMatricolaPadreNewParto');
    if (txtComune !== undefined) {
        AutoCompleteCercaMaschi('ContentPlaceHolder1_txtMatricolaPadreNewParto', 10, 500, window.PageMethods, 'GetListMas', 'ContentPlaceHolder1_hfCercaPadreNewPartoVal', document.getElementById('ContentPlaceHolder1_txtCercaPadreNewParto'), document.getElementById('ContentPlaceHolder1_lstMatricolaPadreNewParto'));
    }
}

function DeleteTxtMatricola(txt) {
    txt.value = '';
}

function PuliziaRicerche() {
    document.getElementById("ContentPlaceHolder1_txtCercaMatricola").value = '';
    document.getElementById("ContentPlaceHolder1_lstRicerca").options.length = 0;
    document.getElementById("ContentPlaceHolder1_txtCercaMatricolaMadre").value = '';
    document.getElementById("ContentPlaceHolder1_lstRicercaMadre").options.length = 0;
    return true;
}

function SaveAnagraficaVerifica() {


    if (document.getElementById('ContentPlaceHolder1_TxtMatricola').value.trim() === "") {
        alert('Specificare la matricola USL del bovino');
        return false;
    } else if (document.getElementById('ContentPlaceHolder1_txtDataNascita').value.trim() === "") {
        alert('Specificare la data di nascita');
        return false;
    } else if (isValidDate(document.getElementById('ContentPlaceHolder1_txtDataNascita'), "La data di Nascita") === false) {
        return false;
    } else if (document.getElementById('ContentPlaceHolder1_chFBovino').checked === false && document.getElementById('ContentPlaceHolder1_chMBovino').checked === false) {
        alert('Specificare il sesso del Bovino');
        return false;
    } else if (document.getElementById('ContentPlaceHolder1_txtCercaMadre').value.trim() !== "" && document.getElementById('ContentPlaceHolder1_hfCercaMadreVal').value.trim() === "") {
        alert('Attenzione la Matricola della MADRE inserita non è presente in archivio');
        return false;
    } else if (document.getElementById('ContentPlaceHolder1_txtCercaPadre').value.trim() !== "" && document.getElementById('ContentPlaceHolder1_hfCercaPadreVal').value.trim() === "") {
        alert('Attenzione la Matricola del PADRE inserita non è presente in archivio');
        return false;
    }
    //else if (document.getElementById('ContentPlaceHolder1_fuAllegatiNewBovino').value.trim() !== "") {
    //    if (check_extension(document.getElementById('ContentPlaceHolder1_fuAllegatiNewBovino').value) === false)
    //        return false;
    //}
    if (document.getElementById('ContentPlaceHolder1_txtDataUscita').value.trim() !== "") {
        if (isValidDate(document.getElementById('ContentPlaceHolder1_txtDataUscita'), "La Data di Uscita") === false) {
            {
                // alert('Formato data Uscita non valido');
                return false;
            }
        }
    }

    return true;

}

function isValidDate(dateStr, tipoData) {

    var datePat = /^(\d{1,2})(\/|-)(\d{1,2})\2(\d{2}|\d{4})$/;
    //var re = /^(\d{1,2})\/(\d{1,2})\/(\d{4})$/;

    var matchArray = dateStr.value.match(datePat); // is the format ok?
    if (matchArray === null) {
        alert(tipoData + ": il formato della data non è valido.");
        return false;
    }
    var month = matchArray[3]; // parse date into variables
    var day = matchArray[1];
    var year = matchArray[4];
    var d = new Date(month + "-" + day + "-" + year);


    if (month < 1 || month > 12) { // check month range
        alert(tipoData + ": il mese deve essere compreso tra 1 e 12.");
        return false;
    }
    if (day < 1 || day > 31) {
        alert(tipoData + ": Il giorno deve essere compreso tra 1 e 31.");
        return false;
    }
    if ((month === 4 || month === 6 || month === 9 || month === 11) && day === 31) {
        alert(tipoData + ": il mese di " + month + " non ha 31 giorni!");
        return false;
    }
    if (month === 2) { // check for february 29th
        var isleap = (year % 4 === 0 && (year % 100 !== 0 || year % 400 === 0));
        if (day > 29 || (day === 29 && !isleap)) {
            alert(tipoData + ": Febbraio " + year + " non ha " + day + " giorni!");
            return false;
        }
    }
    var anno = (new Date()).getFullYear() - 15;
    if (year < anno || year > (new Date()).getFullYear()) {
        alert(tipoData + ": L\'anno deve essere compreso tra il " + anno + " - e il " + (new Date()).getFullYear());
        //form.focus();
        return false;
    }
    var dataOggi = (new Date()).getTime();
    if (d.getTime() > dataOggi) {
        alert(tipoData + ': Attenzione la data di nascita non può essere maggiore della data odierna');
        return false;
    }
    return true; // date is valid
}

function OpenNewParto(btn) {
    var aperto = document.getElementById('ContentPlaceHolder1_hfPartiChiusi').value;
    if (aperto === "1") {
        alert('Attenzione il precedente PARTO non risulta chiuso. Per procedere chiuderlo inserendo un parto o aborto o registrare un nuovo salto.');
        return false;
    }
    //procedo a settare la data e cancellare la matricola del toro
    document.getElementById('ContentPlaceHolder1_txtCercaPadreNewParto').value = "";
    var today = new Date();
    var dd = today.getDate();
    var mm = today.getMonth() + 1; //January is 0!
    var yyyy = today.getFullYear();

    if (dd < 10) {
        dd = '0' + dd;
    }

    if (mm < 10) {
        mm = '0' + mm;
    }

    today = dd + '/' + mm + '/' + yyyy;
    document.getElementById('ContentPlaceHolder1_txtNewDatasalto').value = today;
    return true;
}



function SaveVerificaNewParto() {

    if (isValidDate(document.getElementById('ContentPlaceHolder1_txtNewDatasalto')) === false) {
        return false;
    }
    if (document.getElementById('ContentPlaceHolder1_txtNewDatasalto').value.trim() === "") {
        alert('Specificare la data del salto');
        return false;
    }
    else if (document.getElementById('ContentPlaceHolder1_txtCercaPadreNewParto').value.trim() === "") {
        alert('Specificare la matricola USL del Toro');
        return false;
    }
    return true;
}


function isValidDate(dateStr) {

    var datePat = /^(\d{1,2})(\/|-)(\d{1,2})\2(\d{2}|\d{4})$/;
    //var re = /^(\d{1,2})\/(\d{1,2})\/(\d{4})$/;

    var matchArray = dateStr.value.match(datePat); // is the format ok?
    if (matchArray === null) {
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
    if ((month === 4 || month === 6 || month === 9 || month === 11) && day === 31) {
        alert("il mese di " + month + " non ha 31 giorni!");
        return false;
    }
    if (month === 2) { // check for february 29th
        var isleap = (year % 4 === 0 && (year % 100 !== 0 || year % 400 === 0));
        if (day > 29 || (day === 29 && !isleap)) {
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
        alert('Attenzione la data non può essere maggiore della data odierna');
        return false;
    }
    return true;  // date is valid
}

function OpenNewFoto() {
    //document.getElementById('ContentPlaceHolder1_txtNewNameFoto').value = '';
    document.getElementById('ContentPlaceHolder1_chNewPrincipaleFoto').checked = false;
    document.getElementById('ContentPlaceHolder1_fuNewFoto').value = null;
}
function SaveVerificaSaveNewFoto() {

   
    var nomeFile = document.getElementById('ContentPlaceHolder1_fuNewFoto').value;
    
    if (nomeFile === "") {
        alert("Caricare il file immagine da allegare");
        return false;
    }
    // isolo l'estensione
    var extFile = nomeFile.substr(nomeFile.lastIndexOf(".") + 1);
    extFile = extFile.toLowerCase();
    // verifico che sia fra quelle permesse
    var permesse = "#jpg#gif#png#JPG#GIF#PNG#";
 
    if (permesse.indexOf("#" + extFile + "#") === -1) {
        alert("Caricare solo file immagine di tipo: *.jpg, *.gif, *.png");
        return false;
        //args.IsValid = false;
    }
   


    return true;

}