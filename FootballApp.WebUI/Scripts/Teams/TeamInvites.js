//Open modal
$('#pending-button').click(function () {
    //Populate list of Invites
    $('#invite-items').html('');
    $.ajax({
        method: 'GET',
        url: '/Team/GetTeamInvites',
        success: function (result) {
            var dom = "";
            if (result.resultInvites.length > 0) {
                $.each(result.resultInvites, function (i, result) {
                    var img = "";
                    if (result.ImageSource != "") {
                        img = '<img src="' + result.ImageSource + '"/>';
                    }
                    else {
                        img = '<img src="/Content/Images/emptypfp.png" />';
                    }
                    dom += '<div class="invite-item"><div class="invite-data"><div class="invite-image">' + img + '</div><div class="invite-info"><p class="teaminvite-msg">' + result.FriendName + ' te je pozvao da se pridružiš' + result.TeamName + '</p></div></div><div class="invite-controls"><button class="accept-invite" onclick="AcceptInvite(this)" value="' + result.InviteId + '"><span class="accept-invite-ico"></span></button><button class="decline-invite" onclick="DeclineInvite(this)" value="' + result.InviteId + '"><span class="decline-invite-ico"></span></button></div></div>';
                })
            }
            if (result.resultRequests.length > 0) {
                $.each(result.resultRequests, function (i, result) {
                    var img = "";
                    if (result.ImageSource != "") {
                        img = '<img src="' + result.ImageSource + '"/>';
                    }
                    else {
                        img = '<img src="/Content/Images/emptypfp.png" />';
                    }
                    var teammsg = "";
                    var controls = "";
                    if (result.Requestor == "User") {
                        teammsg = "Zahtevaš da se pridružiš " + result.TeamName;
                        controls = '<div class="request-controls"><button class="decline-request" onclick="DeclineRequest(this)" value="' + result.RequestId + '"><span class="decline-request-ico"></span></button></div>';
                    }
                    else {
                        teammsg = result.TeamName + " te poziva da im se pridružiš";
                        controls = '<div class="request-controls"><button class="accept-request" onclick="AcceptRequest(this)" value="' + result.RequestId + '"><span class="accept-request-ico"></span></button><button class="decline-request" onclick="DeclineRequest(this)" value="' + result.RequestId + '"><span class="decline-request-ico"></span></button></div>';
                    }
                    dom += '<div class="request-item"><div class="request-data"><div class="request-image">' + img + '</div><div class="request-info"><p class="teamrequest-msg">' + teammsg + '</p></div></div>' + controls + '</div>';
                })
            }
            if (!result.resultRequests.length > 0 && !result.resultInvites.length > 0) {
                dom = '<p class="invite-emptymsg">Trenutno nemate zahteva.</p>';
            }
            $('#invite-items').html(dom);

        }
    })
    $('#modal').show();
    var pending = $("button[name='Pending-Team-Invites']").first().children(".button-notification").first();
    var teamNotification = $('#team-notification').first();
    var numberOfPending = parseInt(pending.html());
    if (pending.length > 0 && numberOfPending > 0) {

        var numberOfnotifications = parseInt(teamNotification.html());
        if ($(teamNotification).html != "" && numberOfnotifications > 0) {
            numberOfnotifications -= numberOfPending;
            if (numberOfnotifications > 0) {
                $(teamNotification).html(numberOfnotifications);

            }
            else {
                $(teamNotification).html(0);
                $(teamNotification).hide();
            }
        }

        $(pending).html(0);
        $(pending).hide();
    }
})
//Accept Invite
function AcceptInvite(element) {
    var id = element.value;
    $.ajax({
        method: 'POST',
        url: '/Team/AcceptTeamInvite',
        data: {
            inviteId: id
        },
        success: function (result) {
            var dom = "";
            var img = "";
            if (result.value == true) {
                if (result.team.ImageSource != "") {
                    img = '<img src="' + result.team.ImageSource + '"/>';
                }
                else {
                    img = '<img src="/Content/Images/emptypfp.png" />';
                }
                dom = '<div class="teamlist-team"><div class="teamlist-data"><input class="teamlist-teamId"type="hidden" value="@item.Id"/><div class="teamlist-image">' + img + '</div><div class="teamlist-info"><p class="teamlist-name">' + result.team.Name + '</p><p class="teamlist-description">' + result.team.Description + '</p></div></div><div class="teamlist-button-wrap"> <a class="teamlist-button" href="/Team/TeamProfile?teamId=' + result.team.Id + '">Poseti</a></div></div>';
                var errorMsg = $('#teamlist').children('.error-msg').html();
                if (errorMsg != null) {
                    $('#teamlist').html(dom);
               
                }
                else {

                    $('#teamlist').append(dom);

                }
                
                element.closest('.invite-item').remove();
                var invites = $('#invite-items').children('.invite-item');
                var requests = $('#invite-items').children('.request-item');
                var invitemsg = "";
                if (!invites.length > 0 && !requests.length>0) {
                    invitemsg = '<p class="invite-emptymsg">Trenutno nemate zahteva.</p>'
                    $('#invite-items').append(invitemsg);
                }

            }
        }
        
    })
}
//Decline invite
function DeclineInvite(element) {
    var id = element.value;
    $.ajax({
        method: 'POST',
        url: '/Team/DeclineTeamInvite',
        data: {
            inviteId:id
        },
        success: function (result) {
            if (result == true) {
                element.closest('.invite-item').remove();
            }
            var invites = $('#invite-items').children('.invite-item');
            var requests = $('#invite-items').children('.request-item');
            var invitemsg = "";
            if (!invites.length > 0 && !requests.length > 0) {
                invitemsg = '<p class="invite-emptymsg">Trenutno nemate zahteva.</p>'
                $('#invite-items').append(invitemsg);
            }
            
        }
    })
}
//Decline Request
function DeclineRequest(element) {
    var id = element.value;
    $.ajax({
        method: 'POST',
        url: '/Team/DeclineTeamRequest',
        data: {
            requestId: id
        },
        success: function (result) {
            if (result == true) {
                element.closest('.request-item').remove();
            }
            var invites = $('#invite-items').children('.invite-item');
            var requests = $('#invite-items').children('.request-item');
            var invitemsg = "";
            if (!invites.length > 0 && !requests.length > 0) {
                invitemsg = '<p class="invite-emptymsg">Trenutno nemate zahteva.</p>'
                $('#invite-items').append(invitemsg);
            }

        }
    })
}
function AcceptRequest(element) {
    var id = element.value;
    $.ajax({
        method: 'POST',
        url: '/Team/AcceptRequestUser',
        data: {
            requestId:id
        },
        success: function (result) {
            var dom = "";
            var img = "";
            if (result.value == true) {
                if (result.team.ImageSource != "") {
                    img = '<img src="' + result.team.ImageSource + '"/>';
                }
                else {
                    img = '<img src="/Content/Images/emptypfp.png" />';
                }
                dom = '<div class="teamlist-team"><div class="teamlist-data"><input class="teamlist-teamId"type="hidden" value="@item.Id"/><div class="teamlist-image">' + img + '</div><div class="teamlist-info"><p class="teamlist-name">' + result.team.Name + '</p><p class="teamlist-description">' + result.team.Description + '</p></div></div><div class="teamlist-button-wrap"> <a class="teamlist-button" href="/Team/TeamProfile?teamId=' + result.team.Id + '">Poseti</a></div></div>';
                var errorMsg = $('#teamlist').children('.error-msg').html();
                if (errorMsg != null) {
                    $('#teamlist').html(dom);
                }
                else {

                    $('#teamlist').append(dom);
                }

                element.closest('.request-item').remove();
                var invites = $('#invite-items').children('.invite-item');
                var requests = $('#invite-items').children('.request-item');
                var invitemsg = "";
                if (!invites.length > 0 && !requests.length > 0) {
                    invitemsg = '<p class="invite-emptymsg">Trenutno nemate zahteva.</p>'
                    $('#invite-items').append(invitemsg);
                }

            }
        }
    })
}
//Close modal
$('#modal-close').click(function () {
    $('#modal').hide();
})

$(document).mouseup(function (e) {
    var modal = $("#modal");

    if (modal.is(e.target)) {
        modal.hide();
    }
});
