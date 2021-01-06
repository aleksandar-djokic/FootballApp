//messageCount so we can use lazy loading
var messageCount = 0;
//get messages onload
$(document).ready(function () {
    GetMessages();
    $('#chat').animate({ scrollTop: $('#chat')[0].scrollHeight });
    //SignalR
    var chat = $.connection.teamChatHub;
    var teamName = $(".profile-name").html();
    console.log("HERE I AM");
    chat.client.addNewMessageToPage = function (name, message, imgsource, DateTime) {
        var elem = $('#chat');
        var isScrollBottom = false;
        if (elem[0].scrollHeight - elem.scrollTop() == elem.outerHeight()) {
            isScrollBottom = true;
        }
        var img = "";
        if (imgsource != "") {
            img = '<img src="' + imgsource + '"/>';
        }
        else {
            img = '<img src="~/Content/Images/emptypfp.png" />';
        }
        $('#chat').append('<div class="chat-msg"><div class="chat-img">' + img + '</div><div class="chat-data"><p class="chat-name">' + name + '<span class="chat-time"> ' + DateTime + '</span></p><p>'
            + message + '</p></div></div>');
        messageCount += 1;
        if (isScrollBottom) {
            $('#chat').animate({ scrollTop: $('#chat')[0].scrollHeight });
        }


    };
    $('#message').focus();
    $.connection.hub.start().done(function () {
        chat.server.joinGroup(teamName);
        $('#sendmessage').click(function () {
            var teamId = $('#team-id').val();
            if ($('#message').val() != "") {

                $.ajax({
                    method: 'POST',
                    url: '/TeamChat/SendMsg',
                    data: {
                        message: $('#message').val(),
                        grp: teamName,
                        teamId: teamId
                    },
                    success: function (result) {
                        console.log(result);
                        $('#message').val("");
                    }
                })
            }
        })
    });


});
//read Notifics
function ReadChatNotifications() {
    var chatButton = $('#Chat-button').first();
    var chatNotific = $(chatButton).siblings(".notification").first();
    var teamNotification = $("#team-notification").first();
    var bl = parseInt($(chatNotific).html());
    if ($(chatNotific).length > 0 && parseInt($(chatNotific).html()) > 0) {
        var numofchat = parseInt($(chatNotific).html());
        var numofteam = parseInt($(teamNotification).html());
        if ((numofteam - numofchat) > 0) {
            $(teamNotification).html(numofteam - numofchat);
            $(chatNotific).html(0);
            $(chatNotific).hide();
        }
        else {
            $(teamNotification).html(0);
            $(teamNotification).hide();
            $(chatNotific).html(0);
            $(chatNotific).hide(); 
        }
    }
}
//Submit message on Enter
$('#message').keypress(function (e) {
    if (e.which == 13) {
        $('#sendmessage').click();
    }
})
//Get X messages on Load-messages button click (lazy loading)
$('#Load-messages-btn').click(function () {
    var teamId = $('#team-id').val();
    $.ajax({
        method: 'GET',
        url: '/TeamChat/GetMessages',
        data: {
            teamId: teamId,
            messageCount: messageCount

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
                        img = '<img src="~/Content/Images/emptypfp.png" />';
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
//Inital msg load
function GetMessages() {
    var teamId = $('#team-id').val();
    $.ajax({
        method: 'GET',
        url: '/TeamChat/GetMessages',
        data: {
            teamId: teamId,
            messageCount: messageCount

        },
        success:function (result){

            if (result.length > 0) {
               
                result.slice().reverse().forEach(function (result) {
                    var img = "";
                    if (result.ImageSource != "") {
                        img = '<img src="' + result.ImageSource + '"/>';
                    }
                    else {
                        img = '<img src="~/Content/Images/emptypfp.png" />';
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
//    var chat = $.connection.teamChatHub;
//    var teamName = $(".profile-name").html();
//    chat.client.addNewMessageToPage = function (name, message, imgsource, DateTime) {
//        var elem = $('#chat');
//        var isScrollBottom = false;
//        if (elem[0].scrollHeight - elem.scrollTop() == elem.outerHeight()) {
//            isScrollBottom = true;
//        }
//        var img = "";
//        if (imgsource != "") {
//            img = '<img src="' + imgsource + '"/>';
//        }
//        else {
//            img = '<img src="~/Content/Images/emptypfp.png" />';
//        }
//        $('#chat').append('<div class="chat-msg"><div class="chat-img">' + img + '</div><div class="chat-data"><p class="chat-name">' + name + '<span class="chat-time"> ' + DateTime + '</span></p><p>'
//            + message + '</p></div></div>');
//        messageCount += 1;
//        if (isScrollBottom) {
//            $('#chat').animate({ scrollTop: $('#chat')[0].scrollHeight });
//        }
//        console.log("HERE I AM");
       
//    };
//    $('#message').focus();
//    $.connection.hub.start().done(function () {
//        chat.server.joinGroup(teamName);
//        $('#sendmessage').click(function () {
//            var teamId = $('#team-id').val();
//            if ($('#message').val() != "") {

//                $.ajax({
//                    method: 'POST',
//                    url: '/TeamChat/SendMsg',
//                    data: {
//                        message: $('#message').val(),
//                        grp: teamName,
//                        teamId: teamId
//                    },
//                    success: function (result) {
//                        console.log(result);
//                        $('#message').val("");
//                    }
//                })
//            }
//        })
//    });
    

//});

