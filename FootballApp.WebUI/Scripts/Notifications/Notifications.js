window.addEventListener('load', function () {
    $.ajax({
        method: "GET",
        url: "/Notification/GetNotifications",
        success: function (result) {
            var friendNotifications = result.friendNotifications;
            var privateChatNotifications = result.privateChatNotifications;
            var teamChatNotifications = result.teamNotifications.teamChatNotifications;
            var teamMatchNotifications = result.teamNotifications.teamMatchNotifications;
            var teamMemberNotifications = result.teamNotifications.teamMemberNotifications;
            var teamInviteNotifications = result.teamNotifications.teamInviteNotifications;
            //display friend notifications
            if (friendNotifications.length > 0) {
                $('#friends-notification').html(friendNotifications.length);
                $('#friends-notification').show();
                if ($('#pending-friends-notification').length != 0) {
                    $('#pending-friends-notification').html(friendNotifications.length);
                    $('#pending-friends-notification').show();
                }

            }
            //display private chat notifications
            if (privateChatNotifications.length > 0) {
                $('#privatechat-notification').html(privateChatNotifications.length);
                $('#privatechat-notification').show();
                if ($('#conversations').length != 0) {
                    $.each(privateChatNotifications, function (i, notification) {
                        var id = notification.ConversationId;
                        if ($('#' + id).length != 0) {
                            var conversationInfo = $('#' + id).children('.Conversation-info').first();
                            var notification = $(conversationInfo).children('.conversation-notification').first();
                            var numberofnotifications = 1;
                            if ($(notification).html() != "") {
                                numberofnotifications = parseInt($(notification).html()) + 1;
                            }
                            $(notification).html(numberofnotifications);
                            $(notification).show();
                        }
                    })
                }
            }
            if (teamChatNotifications.length > 0 || teamMatchNotifications.length > 0 || teamMemberNotifications.length > 0 || teamInviteNotifications>0) {
                var NumOfTeamNotifications = teamChatNotifications.length + teamMatchNotifications.length + teamMemberNotifications.length + teamInviteNotifications;
                $('#team-notification').html(NumOfTeamNotifications);
                $('#team-notification').show();
                //For Team Index view
                //Pending invites
                if (teamInviteNotifications > 0) {
                    var pending = $("button[name='Pending-Team-Invites']").first();
                    if ($(pending).length > 0) {
                        var pendingInviteNotific = $(pending).children('.button-notification').first();
                        $(pendingInviteNotific).html(teamInviteNotifications);
                        $(pendingInviteNotific).css("display", "inline-block");

                    }
                }

                //All teams on their own 
                if ($('.teamlist-team').length != 0) {
                    //Going trough all the chatnotifications to append them to correct teams
                    $.each(teamChatNotifications, function (i, notification){
                        var id = notification.TeamId;
                        $('.teamlist-team').each(function (i, obj) {
                            var data = $(this).children('.teamlist-data').first();
                            var teamid =parseInt( $(data).children('.teamlist-teamId').first().val());
                            if (id == teamid) {
                                var info = $(data).children('.teamlist-info').first();
                                var notifications = $(info).children('.team-notification').first();
                                var numberofnotifications = 1;
                                if ($(notifications).html() != "") {
                                    numberofnotifications = parseInt($(notifications).html()) + 1;
                                }
                                $(notifications).html(numberofnotifications);
                                $(notifications).show();
                            }
                        });
                    });
                     //Going trough all the match notifications to append them to correct teams
                    $.each(teamMatchNotifications, function (i, notification) {
                        var id = notification.TeamId;
                        $('.teamlist-team').each(function (i, obj) {
                            var data = $(this).children('.teamlist-data').first();
                            var teamid = parseInt($(data).children('.teamlist-teamId').first().val());
                            if (id == teamid) {
                                var info = $(data).children('.teamlist-info').first();
                                var notifications = $(info).children('.team-notification').first();
                                var numberofnotifications = 1;
                                if ($(notifications).html() != "") {
                                    numberofnotifications = parseInt($(notifications).html()) + 1;
                                }
                                $(notifications).html(numberofnotifications);
                                $(notifications).show();
                            }
                        });
                    });
                     //Going trough all the member notifications to append them to correct teams
                    $.each(teamMemberNotifications, function (i, notification) {
                        var id = notification.TeamId;
                        $('.teamlist-team').each(function (i, obj) {
                            var data = $(this).children('.teamlist-data').first();
                            var teamid = parseInt($(data).children('.teamlist-teamId').first().val());
                            if (id == teamid) {
                                var info = $(data).children('.teamlist-info').first();
                                var notifications = $(info).children('.team-notification').first();
                                var numberofnotifications = 1;
                                if ($(notifications).html() != "") {
                                    numberofnotifications = parseInt($(notifications).html()) + 1;
                                }
                                $(notifications).html(numberofnotifications);
                                $(notifications).show();
                            }
                        });
                    });
                    }
                    // For Team Profile View
                    var x = $('#team-id');
                    if ($('#team-id') != null) {
                        var teamId =parseInt($('#team-id').val());
                        $.each(teamChatNotifications, function (i, notification) {
                            var id = notification.TeamId;
                            if (id == teamId) {
                                //var chat = $("button[name='Profile-Nav-Chat']").first();
                                //var notification = $(chat).siblings(".notification").first();
                                //var numberofnotifications = 1;
                                //if ($(notification).html() != "") {
                                //    numberofnotifications = parseInt($(notification).html()) + 1;
                                //}
                                //$(notification).html(numberofnotifications);
                                //$(notification).show();
                                var teamnotification = $('#team-notification').first();
                                if (teamnotification.length > 0) {
                                    var number = parseInt($(teamnotification).html());
                                    number--;
                                    if (number > 0) {
                                        $(teamnotification).html(number);
                                    }
                                    else {
                                        $(teamnotification).html(0);
                                        $(teamnotification).hide();
                                    }
                                }
                            }
                            
                        });
                        //Going trough all the match notifications to append them to correct teams
                        $.each(teamMatchNotifications, function (i, notification) {
                            var notificStatus = notification.Type;
                            var id = notification.TeamId;
                            if (id == teamId) {
                                var Match = $("button[name='Profile-Nav-Matches']").first();
                                var notification = $(Match).siblings(".notification").first();
                                var numberofnotifications = 1;
                                if ($(notification).html() != "") {
                                    numberofnotifications = parseInt($(notification).html()) + 1;
                                }
                                $(notification).html(numberofnotifications);
                                $(notification).show();
                                if (notificStatus == "Pending")
                                {
                                    
                                    if ($('#pending-matches-button').length > 0) {
                                        var pending = $('#pending-matches-button').children(".button-notification").first();
                                        var numberofpending = 1;
                                        if ($(pending).html() != "") {
                                            numberofpending = parseInt($(notification).html()) + 1;
                                        }
                                        $(pending).html(numberofnotifications);
                                        $(pending).css("display", "inline-block");
                                    }
                                }
                            }
                        });
                        //Going trough all the member notifications to append them to correct teams
                        $.each(teamMemberNotifications, function (i, notification) {
                            var id = notification.TeamId;
                            if (id == teamId) {
                                var Member = $("button[name='Profile-Nav-Members']").first();
                                var notification = $(Member).siblings(".notification").first();
                                var pendingNotific = $('#pending-members').children(".button-notification").first();
                                var numberofnotifications = 1;
                                if ($(notification).html() != "") {
                                    numberofnotifications = parseInt($(notification).html()) + 1;
                                }
                                $(notification).html(numberofnotifications);
                                $(notification).show();
                                $(pendingNotific).html(numberofnotifications);
                                $(pendingNotific).css("display", "inline-block");
                            }
                        });
                }
            }
        }
    })
})


$(document).ready(function () {
    console.log("Hello world");
    
    var notificationCon = $.hubConnection();
    var notificationHub = notificationCon.createHubProxy('notificationHub');
    notificationHub.on('addNewTeamNotific', function (teamId) {
        var audio = new Audio('/Content/Audio/notific.mp3');
        audio.play();
        var teamNotification = $('#team-notification');
        var teamNotificationNumber = 1;
        if ($(teamNotification).html != "" && parseInt($(teamNotification).html())>0 ) {

            teamNotificationNumber = parseInt($(teamNotification).html()) + 1;

        } 
        $(teamNotification).html(teamNotificationNumber);
        $(teamNotification).show();
        if ($('.teamlist-team').length != 0) {
            $('.teamlist-team').each(function (i, obj) {
                var data = $(this).children('.teamlist-data').first();
                var teamid = parseInt($(data).children('.teamlist-teamId').first().val());
                if (teamId == teamid) {
                    var info = $(data).children('.teamlist-info').first();
                    var notifications = $(info).children('.team-notification').first();
                    var numberofnotifications = 1;
                    if ($(notifications).html() != "") {
                        numberofnotifications = parseInt($(notifications).html()) + 1;
                    }
                    $(notifications).html(numberofnotifications);
                    $(notifications).show();
                }
            });
           
        }
        var x = $('#team-id');
        if (parseInt($('#team-id').val()) == teamId) {
            var chat = $("button[name='Profile-Nav-Chat']").first();
            var chatNotification = $(chat).siblings(".notification").first();
            var numberofnotifications = 1;
            if ($(chatNotification).html() != "") {
                numberofnotifications = parseInt($(chatNotification).html()) + 1;
            }
            $(chatNotification).html(numberofnotifications);
            $(chatNotification).show();
        }

            
    });
    notificationHub.on("addNewChatNotific", function (conversationId) {
        var audio = new Audio('/Content/Audio/notific.mp3');
        audio.play();
        if (!($("input[name='Chat-conversation-id'").length > 0) || parseInt($("input[name='Chat-conversation-id'").val()) != conversationId) {

            var chatNotification = $('#privatechat-notification');
            var notificNumber = 1;
            if ($(chatNotification).html != "" && parseInt($(chatNotification).html()) > 0) {
                notificNumber = parseInt($(chatNotification).html()) + 1;
            }
            $('#privatechat-notification').html(notificNumber);
            $('#privatechat-notification').show();
            if ($('#conversations').length != 0) {

                if ($('#' + conversationId).length != 0) {
                    var conversationInfo = $('#' + conversationId).children('.Conversation-info').first();
                    var notification = $(conversationInfo).children('.conversation-notification').first();
                    var numberofnotifications = 1;
                    if ($(notification).html() != "") {
                        numberofnotifications = parseInt($(notification).html()) + 1;
                    }
                    $(notification).html(numberofnotifications);
                    $(notification).show();
                }
             }
        }

    });
    var myname = "";
    $.ajax({
        method: "GET",
        url: "/Account/GetUserName",
        success: function (result) {
            myname = result;
        }
    })
    notificationCon.start().done(function () {
        
        if (myname != "") {

            notificationHub.invoke("joinGroup",myname);
        }
        
    });


});