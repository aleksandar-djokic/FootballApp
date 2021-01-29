$('#open-ticket').on('click', function () {
    $("#modal").show();
})
$('#modal-close').click(function () {
    $('#modal').hide();
})

$(document).mouseup(function (e) {
    var modal = $("#modal");

    if (modal.is(e.target)) {
        modal.hide();
    }
});
$('#create-button').click(function () {
    event.preventDefault();
    var title = $('#Ticket-Title').val();
    var message = $('#Ticket-Message').val();
    if (title == "" || message == "") {
        if (title == "") {
            $('#Ticket-Title').parent().siblings('.error-label').html("Empty Title");
        } else {
            $('#Ticket-Title').parent().siblings('.error-label').html("");
        }
        if (message == "") {
            $('#Ticket-Message').parent().siblings('.error-label').html("Empty Message");
        } else {
            $('#Ticket-Message').parent().siblings('.error-label').html("");
        }

    } else {
        $('#ticket-form').submit();
    }

})
