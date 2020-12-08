$(function () {
    var chat = $.connection.teamChatHub;
    var teamName = $(".profile-name").html();
    chat.client.addNewMessageToPage = function (name, message, imgsource,DateTime, grp) {
        if (grp == teamName) {
            var img = "";
            if (imgsource != "") {
                img = '<img src="' + imgsource + '"/>';
            }
            else {
                img = '<img src="~/Content/Images/emptypfp.png" />';
            }
            $('#chat').append('<div class="chat-msg"><div class="chat-img">' + img + '</div><div class="chat-data"><p>' + name + '<span> ' + DateTime + '</span></p><p>'
                + message + '</p></div></div>');
        }
        else {
            console.log('you are not part of this grp');
        }
    };
    $('#message').focus();
    $.connection.hub.start().done(function () {
        $('#sendmessage').click(function () {
            $.ajax({
                method: 'POST',
                url: '/TeamChat/SendMsg',
                data: {
                    message: $('#message').val(),
                    grp: teamName
                },
                success: function (result) {
                    console.log(result);
                    $('#message').val("");
                }
            })
        })
    });
});

