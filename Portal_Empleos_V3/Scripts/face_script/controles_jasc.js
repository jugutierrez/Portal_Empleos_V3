

 


function soloNumeros(e) {
    var key = window.Event ? e.which : e.keyCode
    return (key >= 48 && key <= 57)
}
/*

$(document).ready(function (e) {

 

    $('[data-toggle="tooltip"]').tooltip();
    $('a[data-toggle="tab"]').on('show.bs.tab', function (e) {

        var $target = $(e.target);

        if ($target.parent().hasClass('disabled')) {
            return false;
        }
    });

    $(".next-step").click(function (e) {

        var $active = $('.nav-tabs li.active');
        $active.addClass('disabled');
        //$active.next().removeClass('disabled');
        nextTab($active);
        //document.getElementById('nombre_cuenta_usuario').focus();

    });
    $(".prev-step").click(function (e) {

        var $active = $('.nav-tabs li.active');
        $active.addClass('disabled');
        $active.prev().removeClass('disabled');
        prevTab($active);
        //document.getElementById('nombre_usuario').focus();

    });
});
function nextTab(elem) {
    $(elem).next().find('a[data-toggle="tab"]').click();

}

function prevTab(elem) {
    $(elem).prev().find('a[data-toggle="tab"]').click();
}

$(document).ready(function (e) {
    $('#id_ciudad').empty();
    $('#id_ciudad').append('<option selected="selected" value="">Seleccione...</option>');
    $('#id_ciudad').attr('readonly', true);
    $('#id_comuna').empty();
    $('#id_comuna').append('<option selected="selected" value="">Seleccione...</option>');
    $('#id_comuna').attr('readonly', true);
});

$(function () {


    $('#id_region').change(function () {

        $('#id_ciudad').empty();
        $('#id_ciudad').append('<option selected="selected" value="">Seleccione...</option>');
        $('#id_ciudad').attr('readonly', true);
        $('#id_comuna').empty();
        $('#id_comuna').append('<option selected="selected" value="">Seleccione...</option>');
        $('#id_comuna').attr('readonly', true);
        $.ajax({
            type: "GET",
            url: "/cuenta/obtener_ciudades",
            datatype: "Json",
            data: { ciudadId: $('#id_region').val() },
            success: function (data) {
                if (data != null) {

                    $.each(data, function (index, value2) {
                        $('#id_ciudad').append('<option value="' + value2.id_ciudad + '">' + value2.nombre_ciudad + '</option>');
                    });
               
                    $('#id_ciudad').attr('readonly', false);
                }
            }
        });
    });
    $('#id_ciudad').change(function () {

        $('#id_comuna').empty();
        $('#id_comuna').append('<option selected="selected" value="">Seleccione...</option>');
        $('#id_comuna').attr('readonly', true);
        $.ajax({
            type: "GET",
            url: "/cuenta/obtener_comunas",
            datatype: "Json",
            data: { comunaId: $('#id_ciudad').val() },
            success: function (data) {
                if (data != null) {
                    $.each(data, function (index, value) {
                        $('#id_comuna').append('<option value="' + value.id_comuna + '">' + value.nombre_comuna + '</option>');
                    });
                    $('#id_comuna').attr('readonly', false);
                }
            }
        });
    });
});


*/