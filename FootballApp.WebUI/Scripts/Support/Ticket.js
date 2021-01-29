$("#send-button").click(function () {
    var id = $('#ticket-id').html();
    var message = $('#message').val();

    $.ajax({
        method: "POST",
        url: "/Support/SendMessage",
        data: {
            id:id,
            message:message,
        },
        success: function (data) {
            if (data.result) {

                var dom;
                if (data.Name == "Administrator") {
                    dom = '<div class="message-admin"><p>' + data.Name + '</p><p>' + data.Time + '</p><p>' + data.Message + '</p></div>';
                } else {
                    dom = '<div class="message"><p>' + data.Name + '</p><p>' + data.Time + '</p><p>' + data.Message + '</p></div>';
                }
                $('.messages').first().append(dom);
                $('#message').val("");
            }
        }

    })
})
$("#close-button").click(function () {
    var id = $('#ticket-id').html();
    $.ajax({
        method: "POST",
        url: "/Support/CloseTicket",
        data: {
            id: id
        },
        success: function (result) {
            if (result) {
                $('.message-input').hide();
                $('#close-button').hide();
            }
        }

    })
})