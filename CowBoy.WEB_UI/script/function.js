
//Open The First Accordion Section When Page Loads
// $('.accordion-header').first().toggleClass('active-header').toggleClass('inactive-header');
// $('.accordion-content').first().slideDown().toggleClass('open-content');



function NewSalto() {

    document.getElementById("ContentPlaceHolder1_divSaltoDettaglio").style.visibility = "visible";
    //document.getElementById("ContentPlaceHolder1_divSaltoDettaglio").style.visibility = "hidden";
    var parm = document.getElementById("ContentPlaceHolder1_ddlTori");
    parm.selectedIndex = 0;
    document.getElementById('ContentPlaceHolder1_idSalto').value = "0";//id salto per un salto nuovo
    document.getElementById('ContentPlaceHolder1_txtDataSalto').value = "";
    //abilito i pulsanti salva e cancella
    ChangeButtonStyle('btn btn-success btnStandardWidth', "1");
}

function NewFiglio() {
    document.getElementById("ContentPlaceHolder1_divEditFigli").style.visibility = "visible";
    document.getElementById('ContentPlaceHolder1_txtMatriUslFiglio').value = "";
    document.getElementById('ContentPlaceHolder1_txtMatriAzFigli').value = "";
    document.getElementById('ContentPlaceHolder1_hfIdFiglio').value = "0";
    document.getElementById('ContentPlaceHolder1_chFfiglio').checked = false;
    document.getElementById('ContentPlaceHolder1_chMfiglio').checked = false;
    //abilito il pulsante salva
    document.getElementById('ContentPlaceHolder1_btnSalvaFiglio').setAttribute('class', 'btn btn-success btnStandardWidth');
    document.getElementById("ContentPlaceHolder1_btnDettaglioFiglio").style.visibility = "hidden";
}



function GetSelectedRow(lnk) {
    document.getElementById("ContentPlaceHolder1_divSaltoDettaglio").style.visibility = "visible";

    var row = lnk.parentNode.parentNode;
    // var rowIndex = row.rowIndex - 1;


    var data = row.cells[1].innerHTML; //.getElementsByTagName("input")[0].value;
    var idToro = row.cells[2].innerHTML;//valore della boundfield
    var mAtricola = row.cells[3].innerHTML;//valore della boundfield
    var idSalto = row.cells[4].innerHTML;//valore della boundfield
    document.getElementById('ContentPlaceHolder1_idSalto').value = idSalto;

    document.getElementById('ContentPlaceHolder1_txtDataSalto').value = data;

    var parm = document.getElementById("ContentPlaceHolder1_ddlTori");
    parm.selectorText = mAtricola;

    //selezionare il valore della dropdownlist
    var objSelect = document.getElementById("ContentPlaceHolder1_ddlTori");
    setSelectedValue(objSelect, mAtricola, idToro);

    //abilito i pulsanti salva e cancella
    ChangeButtonStyle('btn btn-success btnStandardWidth');
}


function GetSelectedRowFigli(lnk) {
    document.getElementById("ContentPlaceHolder1_divEditFigli").style.visibility = "visible";

    var row = lnk.parentNode.parentNode;
 
    var mAtriUsl = row.cells[1].innerHTML; //.getElementsByTagName("input")[0].value;
    var mAtricolaAzi = row.cells[2].innerHTML;//valore della boundfield
    var idfiglio = row.cells[3].innerHTML;//valore della boundfield
    var Foto = row.cells[4].innerHTML;
    var Sesso = row.cells[5].innerHTML;
    if (Sesso == "M") {
        document.getElementById('ContentPlaceHolder1_chFfiglio').checked = false;
        document.getElementById('ContentPlaceHolder1_chMfiglio').checked = true;
    } else {
        document.getElementById('ContentPlaceHolder1_chFfiglio').checked = true;
        document.getElementById('ContentPlaceHolder1_chMfiglio').checked = false;
    }

    document.getElementById("ContentPlaceHolder1_btnDettaglioFiglio").style.visibility = "visible";
    document.getElementById('ContentPlaceHolder1_hfIdFiglio').value = idfiglio;

    if (mAtriUsl != "&nbsp;")
    document.getElementById('ContentPlaceHolder1_txtMatriUslFiglio').value = mAtriUsl;
    if (mAtricolaAzi != "&nbsp;")
    document.getElementById('ContentPlaceHolder1_txtMatriUslFiglio').value = mAtricolaAzi;


    if (Foto != "") {
        var fullPath = document.getElementById('ContentPlaceHolder1_hfPercorsoFoto').value + "\\" + idfiglio +"\\" + Foto;
        document.getElementById("ContentPlaceHolder1_divFotoFiglio").style.visibility = "visible";
        document.getElementById('ContentPlaceHolder1_imgAfotoFilgio').href = fullPath;
        document.getElementById('ContentPlaceHolder1_imgFotoFiglio').src = fullPath;

    }

    //abilito il pulsante salva Figlio
    document.getElementById('ContentPlaceHolder1_btnSalvaFiglio').setAttribute('class', 'btn btn-success btnStandardWidth');
}


function setSelectedValue(selectObj, valueToSet, idToro) {
    var stringPulita = valueToSet.replace(/^\s+|\s+$/g, '').replace('&nbsp;', '');


    for (var i = 0; i < selectObj.options.length; i++) {
        if (selectObj.options[i].text.replace(/^\s+|\s+$/g, '').replace('&nbsp;', '') == stringPulita) {
            selectObj.options[i].selected = true;
            return false;
        }
    }
    //verifico che l'elemento non sia vuoto 

    //aggiungo l'elemento non trovato
    var opt = document.createElement("option");
    document.getElementById("ContentPlaceHolder1_ddlTori").options.add(opt);
    opt.text = stringPulita;
    opt.value = idToro;
    return false;
}

function ChangeButtonStyle(value, verifica) {

    document.getElementById('ContentPlaceHolder1_btnSaveSalto2').setAttribute('class', value);

    if (verifica != "1") {
        document.getElementById('ContentPlaceHolder1_btnDeleteSalto').setAttribute('class', value);
    } else {
        document.getElementById('ContentPlaceHolder1_btnDeleteSalto').setAttribute('class', 'btn disabled btnStandardWidth');
    }
}

function SaveSalto(value) {

    var parm = document.getElementById("ContentPlaceHolder1_ddlTori");
    if (parm.selectedIndex == 0) {
        alert('Selezionare il toro utilizzato nel salto');
        return false;
    }

    if (document.getElementById('ContentPlaceHolder1_txtDataSalto').value == "") {
        alert('Selezionare la data del Salto');
        return false;
    }


    ChangeButtonStyle(value, "");
    return true;
}

function SaveFiglio(value) {
    if (document.getElementById('ContentPlaceHolder1_txtMatriUslFiglio').value == "") {
        alert('Inserire la matricola Usl');
        return false;
    }

    if (document.getElementById('ContentPlaceHolder1_chFfiglio').checked == false && document.getElementById('ContentPlaceHolder1_chMfiglio').checked == false) {
        alert('Specificare il sesso del figlio/a');
        return false;
    }

    return true;
}

function DeleteSalto() {

}

function Verifica() {
    var cc = document.getElementById('ContentPlaceHolder1_hfIdPartoSalto').value;
    if (cc == "") {
        alert('Selezionare un record o crearne uno nuovo prima di procedere al salvataggio ');
        return false;
    }
    return true;
}

function SavePartoVerifica() {
    //verifico che la data di asciutta sia inferiore alla data di parto
    if (document.getElementById('ContentPlaceHolder1_txtDataParto').value != "" && document.getElementById('ContentPlaceHolder1_txtDataParto').value != "") {
        var dataAsc = new Date(document.getElementById('ContentPlaceHolder1_txtDataAsciutta').value);
        var dataPar = new Date(document.getElementById('ContentPlaceHolder1_txtDataParto').value);
        if (dataAsc >= dataPar) {
            alert('Attenzione la data di asciutta non puà essere maggiore o uguale alla data del parto');
            return false;
        }
    }

    //se il check aborto è selezionata l'unico campo obbligatorio è la data del parto che in questo caso indicala data di aborto
    if (document.getElementById('ContentPlaceHolder1_chAborto').checked == true) {
        if (document.getElementById('ContentPlaceHolder1_txtDataParto').value == "") {
            alert('Selezionare la data parto che in questo caso indica la data di Aborto');
            return false;
        }
    } else {
        // se inserisco la data del parto devo avere anche la data di asciutta
        if (document.getElementById('ContentPlaceHolder1_txtDataAsciutta').value == "" && document.getElementById('ContentPlaceHolder1_txtDataParto').value != "") {
            alert('Selezionare la data di asciutta');
            return false;
        }
        if (document.getElementById('ContentPlaceHolder1_txtDataParto').value != "") {
            var c = parseInt(document.getElementById('ContentPlaceHolder1_txtFv').value) + parseInt(document.getElementById('ContentPlaceHolder1_txtFm').value) + parseInt(document.getElementById('ContentPlaceHolder1_txtMv').value) + parseInt(document.getElementById('ContentPlaceHolder1_txtMm').value);
            if (c == 0) {
                alert('Attenzione prima di procedere inserire il numero dei vitelli nati');
                return false;
            }
            if (c > 4) {
                alert('Attenzione il numero dei vitelli nati (somma delle femmine e maschi vivi e morti) è esagerato.');
                return false;
            }
        }
    }
    return true;
}


function onblurVitellini(controL) {
    if (controL.value == "")
        controL.value = 0;
}

function checkAbborto(clickedid) {
    if (clickedid.checked == false) {
        return false;
    } else {
        var box = confirm("Avete impostato parto Abbortito. Siete sicuri?");
        if (box == true)
            return true;
        else
            clickedid.checked = false;
        //document.getElementById(clickedid).checked = false;

    }
    return false;
}

function checkSesso(clickedid, controlFalse, viewDiv, chTrue) {
    if (clickedid.checked == true) {
        document.getElementById(controlFalse).checked = false;
    }
    if (viewDiv != null) {
        if (document.getElementById(chTrue).checked == true) {
            document.getElementById(viewDiv).style.visibility = "visible";
        } else {
            document.getElementById(viewDiv).style.visibility = "hidden";
        }
    }
}

function FotoFiglioSelezionata() {

    var input = document.getElementById('ContentPlaceHolder1_fileInputImage');
    var inputeee = document.getElementById('fileInputImage');

    //if (input.value != null)
    //    document.getElementById('ContentPlaceHolder1_lblPercorsoFotoFiglio').value = input.value;
    var fullPath = input.value;


    if (fullPath) {
        document.getElementById("ContentPlaceHolder1_divFotoFiglio").style.visibility = "visible";
        document.getElementById('ContentPlaceHolder1_imgAfotoFilgio').href = fullPath;
        document.getElementById('ContentPlaceHolder1_imgFotoFiglio').src = fullPath;
        //var startIndex = (fullPath.indexOf('\\') >= 0 ? fullPath.lastIndexOf('\\') : fullPath.lastIndexOf('/'));
        //var filename = fullPath.substring(startIndex);
        //if (filename.indexOf('\\') === 0 || filename.indexOf('/') === 0) {
        //    filename = filename.substring(1);
    }
    // document.getElementById('ContentPlaceHolder1_lblPercorsoFotoFiglio').textContent = filename;
}


function extractNumber(obj, decimalPlaces, allowNegative) {
    var temp = obj.value;

    // avoid changing things if already formatted correctly
    var reg0Str = '[0-9]*';
    if (decimalPlaces > 0) {
        reg0Str += '\\,?[0-9]{0,' + decimalPlaces + '}';
    } else if (decimalPlaces < 0) {
        reg0Str += '\\,?[0-9]*';
    }
    reg0Str = allowNegative ? '^-?' + reg0Str : '^' + reg0Str;
    reg0Str = reg0Str + '$';
    var reg0 = new RegExp(reg0Str);
    if (reg0.test(temp)) return true;

    // first replace all non numbers
    var reg1Str = '[^0-9' + (decimalPlaces != 0 ? ',' : '') + (allowNegative ? '-' : '') + ']';
    var reg1 = new RegExp(reg1Str, 'g');
    temp = temp.replace(reg1, '');

    if (allowNegative) {
        // replace extra negative
        var hasNegative = temp.length > 0 && temp.charAt(0) == '-';
        var reg2 = /-/g;
        temp = temp.replace(reg2, '');
        if (hasNegative) temp = '-' + temp;
    }

    if (decimalPlaces != 0) {
        var reg3 = /\./g;
        var reg3Array = reg3.exec(temp);
        if (reg3Array != null) {
            // keep only first occurrence of .
            //  and the number of places specified by decimalPlaces or the entire string if decimalPlaces < 0
            var reg3Right = temp.substring(reg3Array.index + reg3Array[0].length);
            reg3Right = reg3Right.replace(reg3, '');
            reg3Right = decimalPlaces > 0 ? reg3Right.substring(0, decimalPlaces) : reg3Right;
            temp = temp.substring(0, reg3Array.index) + ',' + reg3Right;
        }
    }

    obj.value = temp;
}


function blockNonNumbers(obj, e, allowDecimal, allowNegative) {
    var key;
    var isCtrl = false;
    var keychar;
    var reg;

    if (window.event) {
        key = e.keyCode;
        isCtrl = window.event.ctrlKey;
    }
    else if (e.which) {
        key = e.which;
        isCtrl = e.ctrlKey;
    }

    if (isNaN(key)) return true;

    keychar = String.fromCharCode(key);

    // check for backspace or delete, or if Ctrl was pressed
    if (key == 8 || isCtrl) {
        return true;
    }

    reg = /\d/;
    var isFirstN = allowNegative ? keychar == '-' && obj.value.indexOf('-') == -1 : false;
    var isFirstD = allowDecimal ? keychar == ',' && obj.value.indexOf(',') == -1 : false;

    return isFirstN || isFirstD || reg.test(keychar);
}
