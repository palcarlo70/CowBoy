
$(function () {
    popolaBovini();
    popolaProssimiEventi();
});


function popolaBovini() {   

    var pidAnagrafica = null;
    var psesso = '';
    var pmanze = null;
    var pinLattazione = null;
    var pinAsciutta = null;    
    var pinAzienda = null;
    var pricercaLibera = $("#txtRicercaLibera").val();
    if ($('#ckFemmina').is(':checked')) psesso = 'F;';
    if ($('#ckMaschi').is(':checked')) psesso += 'M';
    if ($('#ckManze').is(':checked')) pmanze = 1;
    if ($('#ckLattazione').is(':checked')) pinLattazione = 1;
    if ($('#ckAsciutta').is(':checked')) pinAsciutta = 1;
    if ($('#ckVendute').is(':checked')) pinAzienda = 0;

    $("#lblNumRecordBovini").html("0");
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

            if (value !== null) $("#lblNumRecordBovini").html(value.length);
            var fatEmessa
            $.each(value, function (index, pos) {
            try {
                var td;

                trParo = "<tr class=\"gradeBTmb gradeA odd text-center rigaSelTimbroTutte" + conta + "\" role=\"row\" style=\"font-size: 10pt !important;\" >";
                trDisp = "<tr class=\"gradeBTmb gradeB odd text-center rigaSelTimbroTutte" + conta + "\" role=\"row\" style=\"font-size: 10pt !important;\">";

                td = trParo;

                if (index % 2 === 1)
                    td = trDisp;

                //fatEmessa = pos.FattEmessa === 1 ? "<h5 class=\"panel-title\" style=\"color: green;\"> <i class=\"fa fa-check\"></i> </span></h5>" : "";

                rigap = td + tdCenter + "<a href=\"#\" onClick=\"getOrdiniSelectRow('" + pos.Id + "','rigaSelect" + conta + "')\" >" + pos.MatricolaAsl + "</a>" + tdCl +
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

function cercaText(txt) {
    //if ($("#txtRicercaLibera").val().length > 3)
        popolaBovini();
}

function popolaProssimiEventi() {
    //
    $.ajax({
        url: "home/GetProssimiSaltiAsciutteParti",
        type: "POST",
        async: true,
        dataType: "json",
        data: {            
        },
        success: function (value) {
            var trParo = "<tr class=\"gradeA odd text-center\" role=\"row\" style=\"font-size: 9pt !important;\" >";
            var trDisp = "<tr class=\"gradeA odd text-center\" role=\"row\" style=\"font-size: 9pt !important;\">";

            var conta = 0;

            var tdOpFontMini = "<td class=\"text-left\" style=\"vertical-align: middle;padding: 0 !important;font-size: 8pt !important;\">";
            var tdOp = "<td class=\"text-left\" style=\"vertical-align: middle;padding: 0 !important;\">";
            var tdLeft = "<td class=\"text-left\" style=\"vertical-align: middle;padding: 0 !important;\">";
            var tdRight = "<td class=\"text-right\" style=\"vertical-align: middle;padding: 0 !important;\">";
            var tdCenter = "<td class=\"text-center\" style=\"vertical-align: middle;padding: 0 !important;\">";
            var tdCl = "</td>";
            var rigap = "";

            var griglia = "dtGrdProssimiSalti";
            $("#" + griglia).find("tr:not(:first)").remove(); //Pulizia della griglia

            if (value.BoviniCoprire !== null) { $("#lblNumRecordProssimiSalti").html(value.BoviniCoprire.length); } else { $("#lblNumRecordProssimiSalti").html(0); } //NumRecord
            
            $.each(value.BoviniCoprire, function (index, pos) {
                try {
                    var td;

                    trParo = "<tr class=\"gradeBTmb gradeA odd text-center rigaSelTimbroPE" + conta + "\" role=\"row\" style=\"font-size: 10pt !important;\" >";
                    trDisp = "<tr class=\"gradeBTmb gradeB odd text-center rigaSelTimbroPE" + conta + "\" role=\"row\" style=\"font-size: 10pt !important;\">";

                    td = trParo;

                    if (index % 2 === 1)
                        td = trDisp;

                    rigap = td + tdCenter + "<a href=\"#\" onClick=\"getOrdiniSelectRow('" + pos.Id + "','rigaSelect" + conta + "')\" >" + pos.MatricolaAsl + "</a>" + tdCl +
                        tdCenter + pos.EtaMesi + tdCl +
                        tdCenter + pos.GiorniUltimoParto + tdCl +
                        tdCenter + pos.ManzaVacca + tdCl +
                        + "</tr>";
                    $("#" + griglia + " tbody").append(rigap);
                } catch (e) {
                    alert(e);
                }
                conta += 1;
            });

            griglia = "dtGrdProssimiParti";
            $("#" + griglia).find("tr:not(:first)").remove(); //Pulizia della griglia

            if (value.BoviniDaPartorire !== null) { $("#lblNumRecordProssimiParti").html(value.BoviniDaPartorire.length); } else { $("#lblNumRecordProssimiParti").html(0); } //NumRecord

            $.each(value.BoviniDaPartorire, function (index, pos) {
                try {
                    var td;

                    trParo = "<tr class=\"gradeBTmb gradeA odd text-center rigaSelTimbroPE" + conta + "\" role=\"row\" style=\"font-size: 10pt !important;\" >";
                    trDisp = "<tr class=\"gradeBTmb gradeB odd text-center rigaSelTimbroPE" + conta + "\" role=\"row\" style=\"font-size: 10pt !important;\">";

                    td = trParo;

                    if (index % 2 === 1)
                        td = trDisp;
                    rigap = td + tdCenter + "<a href=\"#\" onClick=\"getOrdiniSelectRow('" + pos.Id + "','rigaSelect" + conta + "')\" >" + pos.MatricolaAsl + "</a>" + tdCl +
                        tdCenter + pos.DataPartoStringa + tdCl +
                        + "</tr>";
                    $("#" + griglia + " tbody").append(rigap);
                } catch (e) {
                    alert(e);
                }
                conta += 1;
            });

            griglia = "dtGrdProssimeAsciutte";
            $("#" + griglia).find("tr:not(:first)").remove(); //Pulizia della griglia

            if (value.BoviniDaAsciutta !== null) { $("#lblNumRecordProssimeAsciutte").html(value.BoviniDaAsciutta.length); } else { $("#lblNumRecordProssimeAsciutte").html(0); } //NumRecord

            $.each(value.BoviniDaAsciutta, function (index, pos) {
                try {
                    var td;

                    trParo = "<tr class=\"gradeBTmb gradeA odd text-center rigaSelTimbroPE" + conta + "\" role=\"row\" style=\"font-size: 10pt !important;\" >";
                    trDisp = "<tr class=\"gradeBTmb gradeB odd text-center rigaSelTimbroPE" + conta + "\" role=\"row\" style=\"font-size: 10pt !important;\">";

                    td = trParo;

                    if (index % 2 === 1)
                        td = trDisp;
                    /*Id = Convert.ToInt32(dr["idAnagrafica"].ToString()),
                           MatricolaAsl = dr["MatricolaASL"].ToString(),
                           DataNascita = !dr.IsNull("DataNascita") ? DateTime.Parse(dr["DataNascita"].ToString()) : (DateTime?)null,
                           DataNascitaStringa = !dr.IsNull("DataNascita") ? DateTime.Parse(dr["DataNascita"].ToString()).ToString("dd/MM/yy") : string.Empty,
                           DataMessaInAsciutta = !dr.IsNull("DataMessaInAsciutta") ? DateTime.Parse(dr["DataMessaInAsciutta"].ToString()) : (DateTime?)null,
                           DataMessaInAsciuttaStringa = !dr.IsNull("DataMessaInAsciutta") ? DateTime.Parse(dr["DataMessaInAsciutta"].ToString()).ToString("dd/MM/yy") : string.Empty*/

                    rigap = td + tdCenter + "<a href=\"#\" onClick=\"getOrdiniSelectRow('" + pos.Id + "','rigaSelect" + conta + "')\" >" + pos.MatricolaAsl + "</a>" + tdCl +
                        tdCenter + pos.DataMessaInAsciuttaStringa + tdCl +
                        + "</tr>";
                    $("#" + griglia + " tbody").append(rigap);
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