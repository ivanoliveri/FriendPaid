
    $("#accordion").accordion({
        collapsible: true
    });

    $("#AceptarSolicitudContacto").click(function () {
            $(this).parent().parent().fadeOut(2000);
    });

    $("#RechazarSolicitudDeContacto").click(function () {
            $(this).parent().parent().fadeOut(2000);
    });
    $("#PagarPagoPendiente").click(function () {
            $(this).parent().parent().fadeOut(2000);
    });
