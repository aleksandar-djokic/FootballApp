
$('#pending-members').click(function () {
    var teamId = $('#team-id').val();
    $.ajax({
        method: 'GET',
        url: '/Team/GetTeamRequests',
        data: {
            teamId:teamId
        },
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
                    var teammsg = "";
                    var controls = "";
                    if (result.Requestor == "Team") {
                        teammsg = "You have invited "+result.Username+" to join.";
                        controls = '<div class="request-controls"><button class="decline-request" onclick="DeclineRequest(this)" value="' + result.RequestId + '"><span class="decline-request-ico"></span></button></div>';
                    }
                    else {
                        teammsg = result.Username + " requested to join.";
                        controls = '<div class="request-controls"><button class="accept-request" onclick="AcceptRequest(this)" value="' + result.RequestId + '"><span class="accept-request-ico"></span></button><button class="decline-request" onclick="DeclineRequest(this)" value="' + result.RequestId + '"><span class="decline-request-ico"></span></button></div>';
                    }
                    dom += '<div class="request-item"><div class="request-data"><div class="request-image">' + img + '</div><div class="request-info"><p class="teamrequest-msg">' + teammsg + '</p></div></div>' + controls + '</div>';
                })
            }
            if (!result.length > 0 ) {
                dom = '<p class="invite-emptymsg">Currently you have no pending members.</p>';
            }
            $('#request-items').html(dom);
        }

    })
    $('#modal-pending-requests').show();

})
//Decline request
function DeclineRequest(element) {
    var id = element.value;
    $.ajax({
        method: 'POST',
        url: '/Team/DeclineRequest',
        data: {
            requestId: id
        },
        success:function (result) {
            if (result == true) {
                element.closest('.request-item').remove();
            }
            var requests = $('#request-items').children('.requestitem');
            var msg = "";
            if (!requests.length > 0) {
                msg = '<p class="invite-emptymsg">Currently you have no invites.</p>'
                $('#request-items').append(msg);
            }
        }
        })
}
//AcceptRequest
function AcceptRequest(element) {
    var id = element.value;
    $.ajax({
        method: 'POST',
        url: '/Team/AcceptRequest',
        data: {
            requestId:id
        },
        success: function (result) {
            var dom = "";
            var img = "";
            var member = result.member;
            if (result.value == true) {
                if (member.ImageSource != "") {
                    img = '<img class="member-image" src="' + member.ImageSource + '"/>';
                }
                else {
                    img = '<img class="member-image" src="/Content/Images/emptypfp.png" />';
                }
                dom += '<div class="member-item"><div class="member-info">' + img + '<p>' + member.Name + '</p></div></div>';
                $('#member-list').append(dom);
                element.closest('.request-item').remove();
                var requests = $('#request-items').children('.requestitem');
                var msg = "";
                if (!requests.length > 0) {
                    msg = '<p class="invite-emptymsg">Currently you have no invites.</p>'
                    $('#request-items').append(msg);
                }
                
            }
        }
    })
}
//close modal
$('#pending-modal-close').click(function () {
    $('#modal-pending-requests').hide();
})
$(document).mouseup(function (e) {
    var modal = $("#modal-pending-requests");

    if (modal.is(e.target)) {
        modal.hide();
    }
});