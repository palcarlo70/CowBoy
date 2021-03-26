/*PARAMETRI FUNZIONE 
targetId= nome della casella text box utilizzata per l'autocompletamento
count, timeout =per il sistema
service = PageMethods
method = metodo che restituisce i valori per l'autocomplete
targetV= text nascosta per memorizzare id del cliente selezionato
aggiorna = booleano che serve per verificare se dopo la selezione del cliente bisogna effettuare un upgrade per recuperare dati a lui collegati
*/




function AutoCompleteNew(targetId, count, timeout, service, method, targetV, txtRicerca) {
    var timeoutId;
    var request;
    var target = document.getElementById(targetId);
    var targetVal = document.getElementById(targetV);

    //var list = CreateListNew(target);

    var list = document.getElementById('ContentPlaceHolder1_lstRicercaMadre');
    if (txtRicerca == null)
        list = CreateListCercaMaschi(target);

    window.$addHandler(target, "keyup", function () {
        if (timeoutId != undefined) {
            clearTimeout(timeoutId);
        }
        if (request != undefined) {
            var executor = request.get_executor();
            if (executor.get_started()) {
                executor.abort();
            }
        }
        timeoutId = setTimeout(function () {
            request = service._staticInstance[method](target.value, count, success, function () { });
        }, timeout);
    });

    window.$addHandler(document, "click", function (e) {
        if ((e.srcElement == target) || (e.target == target)) {
            return false;
        }
        //if (txtRicerca == null) list.style.display = "none";
        //if (list.value != '' && target.value != '') {
        //    //target.value = list.text;
        //    targetVal.value = list.value;
        //    var mioVal = list.value;
        //    for (var index = 0; index < list.length; index++) {
        //        if (list[index].value == mioVal) {
        //            if (txtRicerca == null) {
        //                target.value = list[index].text;
        //            }
        //            else {
        //                txtRicerca.value = list[index].text;
        //            }//target.value = list[index].text;
        //            break;
        //        }
        //    }

        //} 
        return true;
    });

    function success(result) {
        list.options.length = 0;
        if (result != null) {
            list.setAttribute("size", result.length + 1);
            //list.style.display = "block";
            //list.style.position = "absolute";
            //var left = $(target).position().left;
            //list.style.left = left + "px";
           // list.style.cssText = 'width:230px;';
            for (var index = 0; index < result.length; index++) {
                var testo = result[index].Matricola;
                list.options.add(new Option(testo, result[index].Id, false));
            }
        } else {
            //  list.style.display = "none";
            document.getElementById(targetV).value = "";
        }

        

        //if (result != null) {
        //    list.setAttribute("size", result.length + 1);
        //    list.style.display = "block";
        //    list.style.position = "absolute";
        //    var left = $(target).position().left;
        //    list.style.left = left + "px";
        //    //list.style.width = target.clientWidth;
        //    list.style.cssText = 'width:230px;';
        //    // list.style.width=
        //    for (var index = 0; index < result.length; index++) {
        //        var testo = result[index].Matricola;
        //        list.options.add(new Option(testo, result[index].Id, false));
        //    }
        //} else {
        //    list.style.display = "none";
        //    document.getElementById(targetV).value = "";
        //}
    }
}


function CreateListNew(sender) {
    if (sender != undefined && document.getElementById('slNew') == undefined) {
       
        var div = document.createElement("div");
        var list = document.createElement("select");
        list.id = "slNew";
        list.style.position = "absolute";
        list.style.display = "none";
        list.style.zIndex = 9999;
        list.className = "form-control";
       
        //list.attributes.add('width', sender.clientWidth);
        div.appendChild(list);
        sender.parentNode.insertBefore(div, sender.nextSibling);
        return list;
    } else if(document.getElementById('slNew') != undefined) {

        return document.getElementById('slNew');
    }    
    else
        {
        return null;
    }

}



function AutoCompleteCercaMaschi(targetId, count, timeout, service, method, targetV, txtRicerca, list) {
    var timeoutId;
    var request;
    var target = document.getElementById(targetId);
    var targetVal = document.getElementById(targetV);
    

    //var list = document.getElementById('ContentPlaceHolder1_lstRicerca');
    if(txtRicerca==null)
       list= CreateListCercaMaschi(target);
    window.$addHandler(target, "keyup", function () {
        if (timeoutId != undefined) {
            clearTimeout(timeoutId);
        }
        if (request != undefined) {
            var executor = request.get_executor();
            if (executor.get_started()) {
                executor.abort();
            }
        }
        timeoutId = setTimeout(function () {
            request = service._staticInstance[method](target.value, count, success, function () { });
        }, timeout);
    });

    window.$addHandler(document, "click", function (e) {
        if ((e.srcElement == target) || (e.target == target)) {
            return false;
        }
        //if (txtRicerca == null) list.style.display = "none";
        //if (list.value != '' && target.value != '') {
        //    //target.value = list.text;
        //    targetVal.value = list.value;
        //    var mioVal = list.value;
        //    for (var index = 0; index < list.length; index++) {
        //        if (list[index].value == mioVal) {
        //            if (txtRicerca==null)
        //                {target.value = list[index].text;}
        //            else {
        //                txtRicerca.value = list[index].text;
        //            }
        //            break;
        //        }
        //    }
        //}
        return true;
    });

    function success(result) {
        list.options.length = 0;

        if (result != null) {
            list.setAttribute("size", result.length + 1);
            //list.style.display = "block";
            //list.style.position = "absolute";
            //var left = $(target).position().left;
            //list.style.left = left + "px";
            //list.style.cssText = 'width:230px;';
        for (var index = 0; index < result.length; index++) {
                var testo = result[index].Matricola;
                list.options.add(new Option(testo, result[index].Id, false));
            }
        } else {
          //  list.style.display = "none";
            document.getElementById(targetV).value = "";
        }
    }
}


function CreateListCercaMaschi(sender) {
    if (sender != undefined && document.getElementById('slCercaMaschi') == undefined) {
        var div = document.createElement("div");
        var list = document.createElement("select");
        list.id = "slCercaMaschi";
        list.style.position = "absolute";
        list.style.display = "none";
        list.style.zIndex = 9999;
        list.className = "form-control";
        div.appendChild(list);
        sender.parentNode.insertBefore(div, sender.nextSibling);
        return list;
    } else if (document.getElementById('slCercaMaschi') != undefined) {

        return document.getElementById('slCercaMaschi');
    }
    else {
        return null;
    }

}

function clickSerchMatricola(sesso) {
   document.getElementById('ContentPlaceHolder1_hfSessoRicerca').value=sesso;
   if (sesso == 'F') {
       $('#modalCercaMadre').modal('toggle');
       $('#modalCercaMadre').modal('show');
   } else if (sesso == 'M') {
       $('#modalCercaToro').modal('toggle');
       $('#modalCercaToro').modal('show');
   }
   else {
       $('#modalCercaToroNewParto').modal('toggle');
       $('#modalCercaToroNewParto').modal('show');
   } 

   
}

function cercaMatricolaBovini() {
    if (document.getElementById('ContentPlaceHolder1_hfSessoRicerca').value == 'F') {
        cercaBoviniFemmine();
    }
    else if (document.getElementById('ContentPlaceHolder1_hfSessoRicerca').value == 'M') {
        cercaBoviniMaschi();
    }
    else {
        cercaBoviniMaschiNewParto();
    }
}

function SelectMatricolaMadre(list, txtRicercaHf, txtRicerca) {
    //alert('ciao');
    //alert(list.value);
    txtRicercaHf.value = list.value;
    var mioVal = list.value;
    for (var index = 0; index < list.length; index++) {
        if (list[index].value == mioVal) {
           // alert('valore inserito: ' + list[index].text);
                txtRicerca.value = list[index].text;
            break;
        }
    }
   // alert(txtRicerca.value);
}

