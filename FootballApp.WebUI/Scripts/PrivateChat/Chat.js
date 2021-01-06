var messageCount = 0;
var myName = $('#myName').val();
var recieverName = $('#recieverName').html();
//onload
window.onload = function () {
    GetMessages();
    $('#chat').animate({ scrollTop: $('#chat')[0].scrollHeight });

    //SignalR
    var chat = $.connection.privateChatHub;
    var recieverName = $("#recieverName").html();
    chat.client.addNewMessageToPage = function (sender, message, DateTime, conversation) {

        var elem = $('#chat');
        var isScrollBottom = false;
        if (elem[0].scrollHeight - elem.scrollTop() == elem.outerHeight()) {
            isScrollBottom = true;
        }
        var img = $('#recieverImg').clone().show();
        var img = img[0].outerHTML;

        $('#chat').append('<div class="chat-msg"><div class="chat-img">' + img + '</div><div class="chat-data"><p class="chat-name">' + sender + '<span class="chat-time"> ' + DateTime + '</span></p><p>'
            + message + '</p></div></div>');
        messageCount += 1;
        if (isScrollBottom) {
            $('#chat').animate({ scrollTop: $('#chat')[0].scrollHeight });
        }
        $.ajax({
            method: 'Post',
            url: '/PrivateChat/ReadMsg',
            data: {
                conversationId: conversation
            }
        })

    };
    $('#message').focus();
    $.connection.hub.start().done(function () {
        chat.server.joinGroup(myName);
        $('#sendmessage').click(function () {
            if ($('#message').val() != "") {

                $.ajax({
                    method: 'POST',
                    url: '/PrivateChat/SendMsg',
                    data: {
                        message: $('#message').val(),
                        recieverName: recieverName,
                        conversationId:$('#Id').val()
                    },
                    success: function (result) {
                        messageCount += 1;
                        var img = $('#myImage').clone().show();/*display none issue*/
                        img = img[0].outerHTML;
                        var msg = $('#message').val();
                        var myName = $('#myName').val();
                        var time = new Date().toLocaleString();
                        var elem = $('#chat');
                        var isScrollBottom = false;
                        if (elem[0].scrollHeight - elem.scrollTop() == elem.outerHeight()) {
                            isScrollBottom = true;
                        }
                        $('#chat').append('<div class="chat-msg"><div class="chat-img">' + img + '</div><div class="chat-data"><p class="chat-name">' + myName + '<span class="chat-time"> ' + time + '</span></p><p>'
                            + msg + '</p></div></div>');
                        if (isScrollBottom) {
                            $('#chat').animate({ scrollTop: $('#chat')[0].scrollHeight });
                        }
                        console.log(result);
                        $('#message').val("");
                    }
                })
            }
        })
    });

}
//Submit message on Enter
$('#message').keypress(function (e) {
    if (e.which == 13) {
        $('#sendmessage').click();
        $('#chat').animate({ scrollTop: $('#chat')[0].scrollHeight });
    }
})
//Get X messages on Load-messages button click (lazy loading)
$('#Load-messages-btn').click(function () {
    var conversationId = $('#Id').val();
    $.ajax({
        method: 'GET',
        url: '/PrivateChat/GetMessages',
        data: {
            messageCount: messageCount,
            conversationId: conversationId

        },
        success: function (result) {

            if (result.length > 0) {
                var dom = ""
                result.slice().reverse().forEach(function (result) {
                    var img = "";
                    if (result.ImageSource != "") {
                        img = '<img src="' + result.ImageSource + '"/>';
                    }
                    else {
                        img = '<img src="/Content/Images/emptypfp.png" />';
                    }
                    dom += '<div class="chat-msg"><div class="chat-img">' + img + '</div><div class="chat-data"><p class="chat-name">' + result.UserName + '<span class="chat-time"> ' + result.Time + '</span></p><p>'
                        + result.Message + '</p></div></div>';

                });
                var firstmsg = $('.chat-msg').first();
                $(dom).insertBefore(firstmsg);
                messageCount += result.length;
            }

        }
    });
})
//initial msg load
function GetMessages() {
    var conversationId = $('#Id').val();
    $.ajax({
        method: 'GET',
        url: '/PrivateChat/GetMessages',
        data: {
            messageCount: messageCount,
            conversationId: conversationId

        },
        success: function (result) {

            if (result.length > 0) {

                result.slice().reverse().forEach(function (result) {
                    var img = "";
                    if (result.ImageSource != "") {
                        img = '<img src="' + result.ImageSource + '"/>';
                    }
                    else {
                        img = '<img src="/Content/Images/emptypfp.png" />';
                    }
                    $('#chat').append('<div class="chat-msg"><div class="chat-img">' + img + '</div><div class="chat-data"><p class="chat-name">' + result.UserName + '<span class="chat-time"> ' + result.Time + '</span></p><p>'
                        + result.Message + '</p></div></div>');
                });
                messageCount += result.length;
            }

        }
    })
}

//$(function () {
//    var chat = $.connection.privateChatHub;
//    var recieverName = $("#recieverName").html();
//    chat.client.addNewMessageToPage = function (sender, message, DateTime, conversation) {
        
//        var elem = $('#chat');
//        var isScrollBottom = false;
//        if (elem[0].scrollHeight - elem.scrollTop() == elem.outerHeight()) {
//            isScrollBottom = true;
//        }
//        var img = $('#recieverImg').clone().show();
//        var img = img[0].outerHTML;
        
//        $('#chat').append('<div class="chat-msg"><div class="chat-img">' + img + '</div><div class="chat-data"><p class="chat-name">' + sender + '<span class="chat-time"> ' + DateTime + '</span></p><p>'
//            + message + '</p></div></div>');
//        messageCount += 1;
//        if (isScrollBottom) {
//            $('#chat').animate({ scrollTop: $('#chat')[0].scrollHeight });
//        }
//        $.ajax({
//            method: 'Post',
//            url: '/PrivateChat/ReadMsg',
//            data: {
//                conversationId: conversation
//            }
//        })

//    };
//    $('#message').focus();
//    $.connection.hub.start().done(function () {
//        chat.server.joinGroup(myName);
//        $('#sendmessage').click(function () {
//            if ($('#message').val() != "") {

//                $.ajax({
//                    method: 'POST',
//                    url: '/PrivateChat/SendMsg',
//                    data: {
//                        message: $('#message').val(),
//                        recieverName: recieverName,
//                        conversationId:$('#Id').val()
//                    },
//                    success: function (result) {
//                        messageCount += 1;
//                        var img = $('#myImage').clone().show();/*display none issue*/
//                        img = img[0].outerHTML;
//                        var msg = $('#message').val();
//                        var myName = $('#myName').val();
//                        var time = new Date().toLocaleString();
//                        var elem = $('#chat');
//                        var isScrollBottom = false;
//                        if (elem[0].scrollHeight - elem.scrollTop() == elem.outerHeight()) {
//                            isScrollBottom = true;
//                        }
//                        $('#chat').append('<div class="chat-msg"><div class="chat-img">' + img + '</div><div class="chat-data"><p class="chat-name">' + myName + '<span class="chat-time"> ' + time + '</span></p><p>'
//                            + msg + '</p></div></div>');
//                        if (isScrollBottom) {
//                            $('#chat').animate({ scrollTop: $('#chat')[0].scrollHeight });
//                        }
//                        console.log(result);
//                        $('#message').val("");
//                    }
//                })
//            }
//        })
//    });


//});