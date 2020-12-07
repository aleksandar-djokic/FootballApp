$('.profilenav-button').click(function (event) {
    $('.profile-content-item').hide();
    $('.profilenav-button').removeClass("active");

    var navName = "#" + $(event.target).html();
    $(event.target).addClass("active");
    $(navName).show();

})

$('#Members-button').click(function () {
    $('#member-list').html('');
    var id = $('#team-id').val();
    $.ajax({
        method: 'GET',
        url: '/Team/GetTeamMembers',
        data: {
            teamId:id
        },
        success: function (result) {
            if (result.list.length > 0) {
                var dom = "";
                var Id = result.userId;
                isOwner = result.isUserOwner;
                $.each(result.list, function (i, result) {
                    var options = '';
                    if (isOwner == true && result.Id != Id) {
                        options = '<div class="member-item-controls"><button value="' + result.Id + '"class="member-item-options" onclick="OpenMemberOptionDropdown(this)"><span class="dot"></span><span class="dot"></span><span class="dot"></span></button></div>';
                    }
                    var img = "";
                    if (result.ImageSource != "") {
                        img = '<img class="member-image" src="' + result.ImageSource + '"/>';
                    }
                    else {
                        img = '<img class="member-image" src="/Content/Images/emptypfp.png" />';
                    }
                    dom += '<div class="member-item"><div class="member-info">' + img + '<p>' + result.Name + '</p></div>' + options + '</div>';
                })
            }
            if (isOwner == true) {
                dom+='<div class="member-dropdown" id="member-dropdown"></div>'
            }

            $('#member-list').html(dom);
        }
    })
})

/* Open member options dropdown*/
function OpenMemberOptionDropdown(element) {
    $('#member-dropdown').html("");
    var id = element.value;
    var teamid = $('#team-id').val();
    $.ajax({
        method: "Get",
        url: "/Team/GetUserRole",
        data: {
            Id: id,
            teamId: teamid
        },
        success: function (result) {
            var controls = ""
            if (result == "Admin") {
                controls = '<button class="member-dropdown-item" value="' + id + '" onclick="PromoteToOwner(this)">Promote to Leader</button><button class="member-dropdown-item" value="' + id +'" onclick="DemoteToMember(this)">Demote to Member</button>'
            }
            else if (result == "Member") {
                controls = '<button class="member-dropdown-item" value="' + id + '" onclick="PromoteToOwner(this)">Promote to Leader</button><button class="member-dropdown-item" value="' + id + '" onclick="PromoteToAdmin(this)">Promote to Administrator</button>'
            }
            $('#member-dropdown').html(controls);
        }
    })
    var x = element.getBoundingClientRect().left + 35;
    var y = element.getBoundingClientRect().top + 35;
    $('#member-dropdown').show();
    $('#member-dropdown').css({ top: y, left: x });
}
function PromoteToOwner(element) {
    var id = element.value;
    var teamid = $('#team-id').val();
    $.ajax({
        method: 'Post',
        url: '/Team/PromoteUserToOwner',
        data: {
            userId: id,
            TeamId: teamid
        },
        success: function (result) {
            if (result) {
                $('#Members-button').click();
            }
            $('#members-dropdown').hide();
            location.reload();
        }
    })
}
function PromoteToAdmin(element) {
    var id = element.value;
    var teamid = $('#team-id').val();
    $.ajax({
        method: 'Post',
        url: '/Team/PromoteUserToAdmin',
        data: {
            userId: id,
            TeamId: teamid
        },
        success: function (result) {
            if (result) {
                $('#Members-button').click();
            }
            $('#members-dropdown').hide();
        }
    })
}
function DemoteToMember(element) {
    var id = element.value;
    var teamid = $('#team-id').val();
    $.ajax({
        method: 'Post',
        url: '/Team/PromoteUserToMember',
        data: {
            userId: id,
            TeamId: teamid
        },
        success: function (result) {
            if (result) {
                $('#Members-button').click();
            }
            $('#members-dropdown').hide();
        }
    })
}
$('#Matches-button').click(function () {
    /*$('#matches-list').html("");*/
    var id = $('#team-id').val();
    $.ajax({
        method: 'GET',
        url: '/Match/GetMatches',
        data: {
            teamId:id
        },
        success: function (result) {
            if (result.length > 0) {
                var dom = "";
                $.each(result, function (i, result) {
                    var team1img = (result.Team1Image != "") ? '<img class="match-team-image" src="' + result.Team1Image + '"/>' : '<img class="match-team-image" src="/Content/Images/emptypfp.png" />';
                    var team2img = (result.Team2Image != "") ? '<img class="match-team-image" src="' + result.Team2Image + '"/>' : '<img class="match-team-image" src="/Content/Images/emptypfp.png" />';

                    dom += '<div class="match-item"><input class="matchId" type="hidden" value="' + result.Id + '"><div class="match-team1">' + team1img + '<p class="match-team-name">' + result.Team1Name + '</p></div><div class="match-info">VS <p class="match-date-location">Date:' + result.Time + ' Location:' + result.Location + '</p></div><div class="match-team2"><p class="match-team-name">' + result.Team2Name + '</p>' + team2img + '</div></div>';


                })

            }
            else {
                /*Add if empty msg*/
            }
            $('#matches-list').html(dom);
        }
    })
})

$('#OwnerLeaveButton').click(function () {
    $('#leave-modal').show();
})
$('#leave-modal-close').click(function () {
    $('#leave-modal').hide();
})

$('#MemberLeaveButton').click(function () {
    var id = $('#team-id').val();
    $.ajax({
        method: 'Post',
        url: '/Team/MemberLeaveTeam',
        data: {
            teamId: id
        },
        success: function (result) {
            if (result) {
                window.location.replace("/Team");
            }
        }
    })
})
$('#disabandon-team').click(function () {
    $('#leave-modal').hide();
    $('#disabandon-modal').show();
    
})
$('#disabandon-modal-close').click(function () {
    $('#disabandon-modal').hide();
})
$('#disabandon-no').click(function () {
    $('#disabandon-modal').hide();
})
$('#disabandon-yes').click(function (){
    var teamId = $('#team-id').val();
    $.ajax({
        method: 'Post',
        url: '/Team/DisabandonTeam',
        data: {
            teamId: teamId
        },
        success: function (result) {
            if (result) {
                window.location.replace("/Team");
            }
        }
    })
})

$('#transfer-team-ownership').click(function () {
    $('#leave-modal').hide();
    $('#transfer-ownership-modal').show();
})
$('#transfer-ownership-close').click(function () {
    $('#transfer-ownership-modal').hide();
})
$('#transfer-cancel').click(function () {
    $('#transfer-ownership-modal').hide();
})
$('#transfer-confirm').click(function () {
    $('#ownership-transfer-error').html("");
    var teamId = $('#team-id').val();
    var memberName = $('#transfer-membername').val();

    $.ajax({
        method: 'POST',
        url: '/Team/TransferOwnershipAndLeave',
        data: {
            teamId: teamId,
            memberName: memberName

        },
        success: function (result) {
            if (result.resultvalue) {
                window.location.replace("/Team");
            }
            else {
                $('#ownership-transfer-error').html(result.resultmsg);
            }
        }

    })

})