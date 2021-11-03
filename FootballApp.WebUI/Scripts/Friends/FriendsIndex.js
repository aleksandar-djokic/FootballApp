//Function for tabular view
$('.friends-button').click(function (event) {
    $('.friends-content-item').hide();
    $('.friends-nav-item').removeClass("active");

    var navName = "#" + $(event.target).html();
    $(event.target).closest('.friends-nav-item').addClass("active");
    $('.addfriends-success').html('');
    $('.addfriends-fail').html('');
    $(navName).show();

})
// AddFriend
$('#add-friend-button').click(function () {

    var user = $('#Add-friend-input').val();
    $('.addfriends-success').html('')
    $('.addfriends-fail').html('')
    $.ajax({
        method: 'POST',
        url: '/Friends/AddFriend',
        data: {
            userName:user,
        },
        success: function (result) {
            if (result.resultvalue == true) {
                $('.addfriends-success').html('Zahtev za prijateljstvo uspešno poslat')  ;
            }
            else {
                $('.addfriends-fail').html(result.resultmsg);
            }
            $('#Add-friend-input').val('');
        }
    });
})
//List Friend Requests
$('#pending-nav').click(function () {
    $('#Pending-requests').html('');
    $.ajax({
        method: 'GET',
        url: '/Friends/GetRequests',
        success: function (result) {
            var dom = "";
            if (result.resultmsg == 'full') {

                $.each(result.resultitems, function (i, result) {
                    var img = '';
                    var msg = '';
                    var controls = '';
                    if (result.ImageSource != "") {
                        img = '<img class="request-image" src="' + result.ImageSource + '"/>';
                    }
                    else {
                        img = '<img class="request-image" src="/Content/Images/emptypfp.png" />';
                    }
                    if (result.Direction == "Incoming") {
                        msg = "Dolazni zahtev za prijateljstvo."
                        controls = '<div class="request-options-incoming"><button onclick="AddFriend(this)" value="' + result.Id + '" title = "Accept request!" class="request-accept" > <span class="request-accept-ico"></span></button > <button onclick="DeclineFriendRequest(this)" value="' + result.Id + '"title="Decline request!" class="request-decline"><span class="request-decline-ico"></span></button></div > ';
                    }
                    else {
                        msg = "Odlazni zahtev za prijateljstvo"
                        controls = '<div class="request-options-outgoing"><button onclick="CancelFriendRequest(this)" value="' + result.Id + '"title="Cancel request!"class="request-cancel"><span class="request-decline-ico"></span></button></div>';
                    }
                    dom += '<div class="request" id="' + result.Id + '"><div class="request-info">' + img + '<div class="request-data"><div class="request-data-name">' + result.Name + '</div><div class="request-data-direction">' + msg + '</div></div></div>' + controls + '</div>';
                })
            }
            else {
                dom = '<div class="pendingfriends-msg">Trenutno nemate zahteve za prijateljstvo.</div>';
            }
            $('#Pending-requests').html(dom);
           
        }
    })
    $('#friends-notification').html("");
    $('#friends-notification').hide("");
    $('#pending-friends-notification').html("");
    $('#pending-friends-notification').hide();
})

$('#All-nav').click(function () {
    $('#allfriends').html('');
    $.ajax({
        method: 'POST',
        url: '/Friends/GetFriends',
        success: function (result) {
            var dom = "";
            if (result.length > 0 && result != null) {
                $.each(result, function (i, result) {
                    var img = '';
                    if (result.ImageSource != "") {
                        img = '<img class="friends-image" src="' + result.ImageSource + '"/>';
                    }
                    else {
                        img = '<img class="friends-image" src="/Content/Images/emptypfp.png" />';
                    }
                    dom += '<div class="friends-item" id="friend-' + result.Id + '"> <div class="friends-info">' + img + '<p>' + result.Name + '</p></div><div class="friends-options"> <a href="/PrivateChat/GetChat?id=' + result.Id + '"class="friends-options-msg"><span class="friends-msg-ico"></span></a><button class="friends-options-dropdown" onclick="OpenDropdown(this)" value="' + result.Id + '"></button></div></div>'
                })
            }
            else {
                dom = '<div class="allfriends-msg">Trenutno nemaš prijatelja.Kako bi dodao prijatelja klikni na "Dodaj".</div>';
            }
            $('#allfriends').html(dom);
        }
    })
})


//Control functions

//Accept friend request
function AddFriend (element) {
    var id = element.value;
    $.ajax({
        method: 'POST',
        url: '/Friends/AcceptFriendRequest',
        data: {
            requestUserId:id
        },
        success: function (result) {
            if (result == true) {
                $('#' + id).remove();
            }
            if (!($('#Pending-requests').children().length > 0)) {
                $('#Pending-requests').html('<div class="pendingfriends-msg">Trenutno nemate zahteve za prijateljstvo.</div>');

            }
        }


    })
}
//Decline friend request
function DeclineFriendRequest(element) {
    var id = element.value;
    $.ajax({
        method: 'POST',
        url: '/Friends/DeclineFriendRequest',
        data: {
            requestUserId: id
        },
        success: function (result) {
            if (result == true) {
                $('#' + id).remove();
            }
            if (!($('#Pending-requests').children().length > 0)) {
                $('#Pending-requests').html('<div class="pendingfriends-msg">Trenutno nemate zahteve za prijateljstvo.</div>');

            }
        }
    })
}
//Cancel friend request

function CancelFriendRequest(element) {
    var id = element.value;
    $.ajax({
        method: 'POST',
        url: '/Friends/CancelFriendRequest',
        data: {
            requestUserId: id
        },
        success: function (result) {
            if (result == true) {
                $('#' + id).remove();
            }
            if (!($('#Pending-requests').children().length > 0)) {
                $('#Pending-requests').html('<div class="pendingfriends-msg">Trenutno nemate zahteve za prijateljstvo.</div>');
            
            }
        }
    })
}

//Dropdown functions
function OpenDropdown(element) {
    var id = element.value;
    $('#remove-friend').val(id);
    $('#invite-friend').val(id);
    var x = element.getBoundingClientRect().left + 40;
    var y = element.getBoundingClientRect().top + 50;
    $('#dropdown').show();
    $('#dropdown').css({ top: y, left: x });

}
//Dropdown hide
$(document).mouseup(function (e) {
    var container = $("#dropdown");
    var modal = $("#invite-modal");

    // if the target of the click isn't the container nor a descendant of the container
    if (!container.is(e.target) && container.has(e.target).length === 0) {
        container.hide();
    }
    if (modal.is(e.target)) {
        modal.hide();
    }
});
//Remove friend
function RemoveFriend(element) {
    var id = element.value;
    $.ajax({
        method: 'POST',
        url: '/Friends/RemoveFriend',
        data: {
            friendUserId: id
        },
        success: function (result) {
            if (result == true) {
                $('#friend-' + id).remove();
                $('#dropdown').hide();
            }
            if (!($('#allfriends').children().length > 0)) {
                $('#allfriends').html('<div class="allfriends-msg">Trenutno nemaš prijatelja.Kako bi dodao prijatelja klikni na "Dodaj".</div>');

            }
        }
    })
}
//Invite modal open
function OpenInviteModal(element) {
    $('#invite-result-msgsuccess').html("");
    $('#invite-result-msgerror').html("");
    $('#dropdown').hide();
    $('#invite-modal').show();
}
//Close
function CloseModal() {
    $('#invite-modal').hide();
}
//Populate
$(document).ready(function () {
    $.ajax({
        method: "GET",
        url: '/Team/GetTeams',
        success: function (result) {
            var modalitems = $('#invite-modal-items');
            var dom = "";
            if (result.length > 0) {
                $.each(result, function (i, result) {
                    var img = '';
                    if (result.ImageSource != "") {
                        img = '<img src="' + result.ImageSource + '"/>';
                    }
                    else {
                        img = '<img src="/Content/Images/emptypfp.png" />';
                    }
                    dom += '<div class="invite-modal-item"><div class="inviteteam-data"><div class="inviteteam-image">' + img + '</div><div class="inviteteam-info"><p class="inviteteam-name">' + result.Name + '</p><p class="inviteteam-description>"' + result.Description + '</p></div></div><div class="inviteteam-button-wrap"><a class="inviteteam-button" onclick="Invite(this,' + result.Id + ')">Pozovi</a></div></div>'
                })
            }
            else {
                dom = '<p class="teaminvite-msg">Nisi član ni jednog tima</p>';
            }
            modalitems.html(dom);
        }

    })
});
//Invite Friend to a team
function Invite(element,teamid) {
    var friendid = $("#invite-friend").val();
    $('#invite-result-msgsuccess').html("");
    $('#invite-result-msgerror').html("");
    $.ajax({
        method: "POST",
        url: '/Team/Invite',
        data: {
            inviteeId: friendid,
            teamId: teamid
        },
        success: function (result) {
            if (result.value) {
                $('#invite-result-msgsuccess').html("Zahtev uspešno poslat.");
            }
            else {
                $('#invite-result-msgerror').html(result.msg);
            }
        }

    });
}