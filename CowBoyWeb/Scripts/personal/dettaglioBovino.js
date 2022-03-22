﻿$(function () {
    //$("#txtDataNascita").datepicker({
    //    format: "dd/mm/yyyy",
    //    autoclose: true
    //});
    //$('.datepicker').datepicker({
    //    format: 'mm/dd/yyyy',
    //    autoclose: true
    //});
    //$('.datepicker').on('changeDate', function (event) {
    //    $(this).datepicker('hide');
    //});
    $('#txtDataNascita').datepicker({}).on('change', function () { $('.datepicker').hide(); });
    $('#txtDataUscita').datepicker({}).on('change', function () { $('.datepicker').hide(); });

    var pidAnagrafica = $("#hdIdBovino").val();
    //pulisci();
    if (pidAnagrafica !== null && pidAnagrafica !== undefined) getDatiBovino(pidAnagrafica);
});



$(document).on("change", "#inputNewImage", function () {
    if (this.files.length > 0) {
        $("#spanNewImage").text(this.files[0].name);
        $("#imgUploadUteNew").attr("src", URL.createObjectURL(this.files[0]));
    }
});

function BovNewImageUpload() {
    if ($('#inputNewImage').val() == "") {
        alert("Scegli un file");
        return false
    }
    var file = $("#inputNewImage")[0].files[0];
    var formData = new FormData();
    formData.append("file", file);

    $.ajax({
        url: "../UploadImage",
        type: "POST",
        data: formData,
        processData: false,
        contentType: false,
        success: function (response) {
            if (response !== null) {
                var path = response[0];
                if (path !== "" && path !== null) {
                    $("#imgBov").attr("src", "/img/FotoBov/" + path);
                    $("#txtBovPathImg").val(path);
                    $("#modalBovNewImagePopUp").modal('hide');
                }
            }
        },
        failure: function (response) {
            alert("Errore nel caricamento");
        }
    });

}

function ChangeNewImage() {
    var path = "/img/FotoBov/" + $("#txtBovPathImg").val();
    $("#imgBov").attr("src", path);
}



function ApriPopUpNewImage() {
    $("#modalBovNewImagePopUp").modal('show');
    $('#inputNewImage').val('');
    $('#spanNewImage').text("File non selezionato");
    $("#imgUploadUteNew").attr("src", "");
}

function checkSesso(clickedid, controlFalse, viewDiv, chTrue) {
    if (clickedid.checked === true) {
        document.getElementById(controlFalse).checked = false;
    }
    if (viewDiv != null) {
        if (document.getElementById(chTrue).checked === true) {
            document.getElementById(viewDiv).style.visibility = "visible";
        } else {
            document.getElementById(viewDiv).style.visibility = "hidden";
            //$("#chToroDaMonta").checked = false;
            //$("#chToroArtificiale").checked = false;
            $('#chToroDaMonta').prop('checked', false);
            $('#chToroArtificiale').prop('checked', false);
        }
    }
}

function getDatiBovino(pidAnagrafica) {
    
    $.ajax({
        url: "../GetBovini",
        type: "POST",
        async: true,
        dataType: "json",

        data: {
            idAnagrafica: pidAnagrafica,
            sesso: null,
            manze: null,
            inLattazione: null,
            inAsciutta: null,
            ricercaLibera: null,
            inAzienda: null
        },
        success: function (value) {
            //.Bovino = new BoviniDto()
            //{
            //    Id = lst.Id,
            //    MatricolaAsl = lst.MatricolaAsl,
            //    MatricolaAz = lst.MatricolaAz,
            //    Nome = lst.Nome,
            //    IdMadre = lst.IdMadre,
            //    MatricolaASLMadre = lst.MatricolaASLMadre,
            //    IdPadre = lst.IdPadre,
            //    MatricolaASLPadre = lst.MatricolaASLPadre,
            //    DataNascita = lst.DataNascita,
            //    DataNascitaStringa = lst.DataNascitaStringa,
            //    DataFine = lst.DataFine,
            //    DataFineStringa = lst.DataFineStringa,
            //    Note = lst.Note,
            //    IdParto = lst.IdParto,
            //    IdFoto = lst.IdFoto,
            //    ToroArtificiale = lst.ToroArtificiale,
            //    ToroDaMonta = lst.ToroDaMonta,
            //    Sesso = lst.Sesso,
            //    NomeFoto = string.IsNullOrEmpty(lst.NomeFoto) ? "imgNull.png" : lst.NomeFoto, /img/FotoBov/
            //    FotoPrincipale = lst.FotoPrincipale,
            //    DataInAsciutta = lst.DataInAsciutta,
            //    DataInAsciuttaStringa = lst.DataInAsciuttaStringa,
            //    DataUltimoPartoStringa = lst.DataUltimoPartoStringa,
            //    MesiUltimoParto = lst.MesiUltimoParto,
            //    UltimoSalto = lst.UltimoSalto,
            //    UltimoSaltoStringa = lst.UltimoSaltoStringa,
            //    GiorniUltimoSalto = lst.GiorniUltimoSalto

            //};
            
            $("#txtMatricolaUsl").val(value.MatricolaAsl);
            $("#txtMatricolaUsl").val(value.MatricolaAz);
            $("#txtNome").val(value.Nome);
            $("#txtDataNascita").val(value.DataNascitaStringa);
            $("#txtDataUscita").val(value.DataFineStringa);
            $("#txtCercaMadre").val(value.MatricolaASLMadre);
            $("#hdIdMadre").val(value.IdMadre);
            $("#txtCercaPadre").val(value.MatricolaASLPadre);
            $("#hdIdPadre").val(value.IdPadre);

            if (value.Sesso === "F") {
                $('#chFBovino').prop('checked', true);
                $('#chMBovino').prop('checked', false);
            } else {
                $('#chFBovino').prop('checked', false);
                $('#chMBovino').prop('checked', true);
            }

            if (value.ToroDaMonta === 1) $('#chToroDaMonta').prop('checked', true);
            if (value.ToroArtificiale === 1) $('#chToroArtificiale').prop('checked', true);

        },
        failure: function () {
            alert("Failed!");
        }
    });
}
function pulisci() {
    $("#txtMatricolaUsl").val("");
    $("#txtMatricolaUsl").val("");
    $("#txtNome").val("");
    $("#txtDataNascita").val("");
    $("#txtDataUscita").val("");
    $("#txtCercaMadre").val("");
    $("#hdIdMadre").val("");
    $("#txtCercaPadre").val("");
    $("#hdIdPadre").val("");


    $('#chFBovino').prop('checked', false);
    $('#chMBovino').prop('checked', false);


    $('#chToroDaMonta').prop('checked', false);
    $('#chToroArtificiale').prop('checked', false);
}