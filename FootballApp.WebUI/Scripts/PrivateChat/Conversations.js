$('#searchbar').keyup(function () {
    var toSearch = $('#searchbar').val();
    $('#search-results').html("");
    if (toSearch != "") {
        $.ajax({
            method: 'Get',
            url: '/PrivateChat/GetFriend',
            data: {
                toSearch: toSearch
            },
            success: function (result) {
                var dom = "";
                if (result.length > 0) {
                    $.each(result, function (i, result) {
                        var img = "";
                        if (result.ImageSource != "") {
                            img = '<img src="' + result.ImageSource + '" />';
                        }
                        else {
                            img = '<img src="/Content/Images/emptypfp.png" />'
                        }
                        dom += '<a class="search-result" href="/PrivateChat/GetChat?id=' + result.Id + '"> <div class="search-result-img">' + img + '</div><p>' + result.Name + '</p></a>';
                    })
                }
                else {
                    dom += '<p>Nema rezultata</p>';
                }
                $('#search-results').append(dom);
                $('.triangle').show();
                $('.box').show();
            }
        })
    } else {
        var dom = '<p>Nema rezultata</p>';
        $('#search-results').append(dom);
    }
    
})
$('#searchbar').click(function () {
    if ($('#searchbar').val() != "") {
        $('#searchbar').keyup();
    }
})

$(document).mouseup(function (e) {
    var searchbar = $("#searchbar");
    var modal2 = $("#search-results");

    if (!modal2.is(e.target) && modal2.has(e.target).length === 0 && !searchbar.is(e.target)) {
        $('.triangle').hide();
        $('.box').hide();
    }
});
window.onload = function () {
    var chat = $.connection.privateChatHub;
    chat.client.addNewMessageToPage = function (sender, message, DateTime, conversation) {
        console.log(sender + ' ' + message + ' ' + DateTime + ' ' + conversation);
        var element = document.getElementById(conversation);
        var proba = $('#' + conversation);
        if ($('#' + conversation).length != 0) {

            if ($('#' + conversation).is('.read')) {
                $('#' + conversation).removeClass('read');
                $('#' + conversation).addClass('unread');
                var conversationInfo = $('#' + conversation).children('.Conversation-info').first();
                $(conversationInfo).children('p').children('.time').first().html(DateTime);
                $(conversationInfo).children('.conversation-msg').first().html(sender + ':' + message);
            }
            else {
                var conversationInfo = $('#' + conversation).children('.Conversation-info').first();
                $(conversationInfo).children('p').children('.time').first().html(DateTime);
                $(conversationInfo).children('.conversation-msg').first().html(sender + ':' + message);
            }
            $('#conversations').prepend($('#' + conversation));
        }
        else {
            if ($(".empty-conversations").length > 0) {
                $('#conversations').html("");
            }
            $.ajax({
                method: 'GET',
                url: '/PrivateChat/GetConversation',
                data: {
                    conversationId: conversation
                },
                success: function (result) {
                    if (result != null) {
                       
                        var img = "";
                        if (result.ImageSource != "") {
                            img = '<img src="' + result.ImageSource + '"/>';
                        } else {
                            img = '<img src="/Content/Images/emptypfp.png" />';
                        }
                        var isRead = "";
                        if (result.isRead) {
                            isRead = "read";
                        } else {
                            isRead = "unread";
                        }

                        dom = '<a href="/PrivateChat/Chat?conversationId=' + result.Id + '" class="conversation ' + isRead + '" id="' + result.Id + '"><div class="conversation-img">' + img + '</div><div class="Conversation-info"><p>' + result.UserName + ' <span class="time">' + result.Time + '</span></p><p class="conversation-msg">' + result.MessageSender + ':' + result.Message + '</p><p class="conversation-notification" style="display:block">1</p></div> </a>';
                        $('#conversations').prepend($(dom));
                    }
                }

            })
        }
    }

    $.connection.hub.start().done(function () {
        var myName = $('#myName').val();
        chat.server.joinGroup(myName);
    });
};
//$(function () {
//    var chat = $.connection.privateChatHub;
//    chat.client.addNewMessageToPage = function (sender, message, DateTime, conversation) {
//        console.log(sender + ' ' + message + ' ' + DateTime + ' ' + conversation);
//        var element = document.getElementById(conversation);
//        var proba = $('#' + conversation);
//        if ($('#' + conversation).length != 0) {

//            if ($('#' + conversation).is('.read')) {
//                $('#' + conversation).removeClass('read');
//                $('#' + conversation).addClass('unread');
//                var conversationInfo = $('#' + conversation).children('.Conversation-info').first();
//                $(conversationInfo).children('p').children('.time').first().html(DateTime);
//                $(conversationInfo).children('.conversation-msg').first().html(sender + ':' + message);
//            }
//            else {
//                var conversationInfo = $('#' + conversation).children('.Conversation-info').first();
//                $(conversationInfo).children('p').children('.time').first().html(DateTime);
//                $(conversationInfo).children('.conversation-msg').first().html(sender + ':' + message);
//            }
//            $('#conversations').prepend($('#' + conversation));
//        }
//        else {
//            $.ajax({
//                method: 'GET',
//                url: '/PrivateChat/GetConversation',
//                data: {
//                    conversationId:conversation
//                },
//                success: function (result) {
//                    if (result != null) {
//                        console.log('Hello there General kenobi');
//                        var img = "";
//                        if (result.ImageSource != "") {
//                            img = '<img src="' + result.ImageSource + '"/>';
//                        } else {
//                            img = '<img src="/Content/Images/emptypfp.png" />';
//                        }
//                        var isRead = "";
//                        if (result.isRead) {
//                            isRead = "read";
//                        } else {
//                            isRead = "unread";
//                        }

//                        dom = '<a href="/PrivateChat/Chat?conversationId=' + result.Id + '" class="conversation ' + isRead + '" id="' + result.Id + '"><div class="conversation-img">' + img + '</div><div class="Conversation-info"><p>' + result.UserName + ' <span class="time">' + result.Time + '</span></p><p class="conversation-msg">' + result.MessageSender + ':' + result.Message + '</p><p class="conversation-notification"></p></div> </a>';
//                        $('#conversations').prepend($(dom));
//                    }
//                }

//            })
//        }
//    }

//    $.connection.hub.start().done(function () {
//        var myName = $('#myName').val();
//        chat.server.joinGroup(myName);
//    });


//});