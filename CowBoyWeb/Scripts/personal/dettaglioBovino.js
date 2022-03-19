$(function () {
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
});






$(document).on("change", "#inputNewImage", function () {
    if (this.files.length > 0) {
        $("#spanNewImage").text(this.files[0].name);
        $("#imgUploadUteNew").attr("src", URL.createObjectURL(this.files[0]))
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
        url: "../Home/UploadImage",
        type: "POST",
        data: formData,
        processData: false,
        contentType: false,
        success: function (response) {
            if (response !== null) {
                var path = response[0];
                if (path !== "" && path !== null) {
                    $("#imgBov").attr("src", "../img/FotoUte/" + path);
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
    var path = "../img/FotoBov/" + $("#txtBovPathImg").val();
    $("#imgBov").attr("src", path);
}



function ApriPopUpNewImage() {
    $("#modalBovNewImagePopUp").modal('show');
    $('#inputNewImage').val('');
    $('#spanNewImage').text("File non selezionato");
    $("#imgUploadUteNew").attr("src", "");
}