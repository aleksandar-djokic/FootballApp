//Open modal
$('#pending-button').click(function () {
    //Populate list of Invites
    $('#invite-items').html('');
    $.ajax({
        method: 'GET',
        url: '/Team/GetTeamInvites',
        success: function (result) {
            var dom = "";
            if (result.length > 0) {
                $.each(result, function (i, result) {
                    var img = "";
                    if (result.ImageSource != "") {
                        img = '<img src="' + result.ImageSource + '"/>';
                    }
                    else {
                        img = '<img src="/Content/Images/emptypfp.png" />';
                    }
                    dom += '<div class="invite-item"><div class="invite-data"><div class="invite-image">' + img + '</div><div class="invite-info"><p class="teaminvite-msg">' + result.FriendName + ' has invited you to join ' + result.TeamName + '</p></div></div><div class="invite-controls"><button class="accept-invite" onclick="AcceptInvite(this)" value="' + result.InviteId + '"><span class="accept-invite-ico"></span></button><button class="decline-invite" onclick="DeclineInvite(this)" value="' + result.InviteId + '"><span class="decline-invite-ico"></span></button></div></div>';
                })
            }
            else {
                dom +='<p class="invite-emptymsg">Currently you have no invites.</p>'
            }
            $('#invite-items').html(dom);

        }
    })
    $('#modal').show();
})
//Accept
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
                dom = '<div class="teamlist-team"><div class="teamlist-data"><div class="teamlist-image">' + img + '</div><div class="teamlist-info"><p class="teamlist-name">' + result.team.Name + '</p><p class="teamlist-description">' + result.team.Description + '</p></div></div><div class="teamlist-button-wrap"> <a class="teamlist-button" href="/Team/TeamProfile?teamId=' + result.team.Id + '">Visit profile</a></div></div>';
                var errorMsg = $('#teamlist').children('.error-msg');
                if (errorMsg != null) {
                    $('#teamlist').html(dom);
                }
                else {

                $('#teamlist').append(dom);
                }
                
                element.closest('.invite-item').remove();
                var invites = $('#invite-items').children('.invite-item');
                var invitemsg = "";
                if (!invites.length>0) {
                    invitemsg = '<p class="invite-emptymsg">Currently you have no invites.</p>'
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
            var invitemsg = "";
            if (!invites.length>0) {
                invitemsg = '<p class="invite-emptymsg">Currently you have no invites.</p>'
                $('#invite-items').append(invitemsg);
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
