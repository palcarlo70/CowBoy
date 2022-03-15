
$(function () {
    popolaBovini();
    //popolaReparti("ddlReparto", "ddlAzienda");

    //CercaUtenti();

    //popolaAzienda("ddlUteAziendaModal");
    //popolaReparti("ddlUteRepartoModal", "ddlUteAziendaModal");
    //popolaContratti("ddlUteContrattoModal");
    //popolaOperazioni("ddlUteOperazione");


    //$("#txtUteNewPass").keyup(function () {
    //    CheckPass();
    //});
    //$("#txtUteConfPass").keyup(function () {
    //    CheckPass();
    //});

    //$("#ddlAzienda").change(function () {
    //    popolaReparti("ddlReparto", "ddlAzienda");
    //});

    //$("#ddlUteAziendaModal").change(function () {
    //    popolaReparti("ddlUteRepartoModal", "ddlUteAziendaModal");
    //});

    //popolaProfiliOrari();
});


function popolaBovini() {
    //var tipoOrdineOffertaFatt = $("#hdTipoOrdineOffertaFatt").val();
    //var statoOrdine = ""; var statoOrdPag = "";
    //PuliDettaglioOrdine();

    //var filtroText = $("#txtRicercaOrdini").val();
    //$("input.filtriStatoOrd:checked").each(function () {
    //    if (statoOrdine !== "") statoOrdine += ";";
    //    statoOrdine += $(this).attr("id").replace("stOrd_", "");
    //});

    //$("input.filtriStatoOrdPag:checked").each(function () {
    //    statoOrdPag += $(this).attr("id").replace("stOrdPag_", "") + ";";
    //});

    //if (idOrdine === "") idOrdine = null;

    //if (idOrdine === undefined || idOrdine === null) {
    //    $("#dtGrdOrder").find("tr:not(:first)").remove(); //Pulizia della griglia
    //    $("#lblNumRecordOrder").html("0");
    //}

    var pidAnagrafica = null;
    var psesso = null;
    var pmanze = null;
    var pinLattazione = null;
    var pinAsciutta = null;
    var pricercaLibera = null;
    var pinAzienda = null;



    //GetBovini(int ? idAnagrafica, string sesso, int ? manze, int ? inLattazione, int ? inAsciutta, string ricercaLibera, int ? inAzienda) ../home/
    //'@Url.Action("GetBovini", "home")',
    var conta = 0;
    $.ajax({
        url: "home/GetBovini",
        type: "POST",
        async: true,
        dataType: "json",
        
        data: {
            idAnagrafica: pidAnagrafica,
            sesso: psesso,
            manze: pmanze,
            inLattazione: pinLattazione,
            inAsciutta: pinAsciutta,
            ricercaLibera: pricercaLibera,
            inAzienda: pinAzienda
        },
        success: function (value) {
            var trParo = "<tr class=\"gradeA odd text-center\" role=\"row\" style=\"font-size: 9pt !important;\" >";
            var trDisp = "<tr class=\"gradeA odd text-center\" role=\"row\" style=\"font-size: 9pt !important;\">";


            var griglia = "dtGrdBoviniList";


            var tdOpFontMini = "<td class=\"text-left\" style=\"vertical-align: middle;padding: 0 !important;font-size: 8pt !important;\">";
            var tdOp = "<td class=\"text-left\" style=\"vertical-align: middle;padding: 0 !important;\">";
            var tdLeft = "<td class=\"text-left\" style=\"vertical-align: middle;padding: 0 !important;\">";
            var tdRight = "<td class=\"text-right\" style=\"vertical-align: middle;padding: 0 !important;\">";
            var tdCenter = "<td class=\"text-center\" style=\"vertical-align: middle;padding: 0 !important;\">";
            var tdCl = "</td>";
            var rigap = "";

            $("#" + griglia).find("tr:not(:first)").remove(); //Pulizia della griglia

            //if (value !== null) $("#lblNumRecordOrder").html(value.length);
            var fatEmessa
            $.each(value, function (index, pos) {
            try {
                var td;

                trParo = "<tr class=\"gradeAmat odd text-center rigaSelect" + conta + "\" role=\"row\" style=\"font-size: 8pt !important;\" >";
                trDisp = "<tr class=\"gradeAmat odd text-center rigaSelect" + conta + "\" role=\"row\" style=\"font-size: 8pt !important;\">";

                td = trParo;

                if (index % 2 === 1)
                    td = trDisp;

                //fatEmessa = pos.FattEmessa === 1 ? "<h5 class=\"panel-title\" style=\"color: green;\"> <i class=\"fa fa-check\"></i> </span></h5>" : "";

                rigap = td + tdLeft + "<a href=\"#\" onClick=\"getOrdiniSelectRow('" + pos.Id + "','rigaSelect" + conta + "')\" >" + pos.MatricolaAsl + "</a>" + tdCl +
                    tdCenter + pos.DataNascitaStringa + tdCl +
                    tdCenter + pos.Sesso + tdCl +
                    tdCenter + pos.DataUltimoPartoStringa + tdCl +
                    tdCenter + pos.MesiUltimoParto + tdCl +
                    tdCenter + pos.UltimoSaltoStringa + tdCl +
                    tdCenter + pos.GiorniUltimoSalto + tdCl +
                    tdCenter + pos.DataInAsciuttaStringa + tdCl +
                    + "</tr>";
                /**
                 
                 */

                $("#" + griglia +" tbody").append(rigap);


            } catch (e) {
                alert(e);
            }
            conta += 1;


            });
        },
        failure: function () {
            alert("Failed!");
        }
    });

}

