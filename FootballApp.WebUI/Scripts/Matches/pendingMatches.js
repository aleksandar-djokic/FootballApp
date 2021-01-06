$("#pending-matches-button").click(function () {
    $(".pending-match-error").html("");
    var teamId = $('#team-id').val();
    $.ajax({
        method: 'GET',
        url: '/Match/GetPendingMatches',
        data: {
            teamid: teamId
        },
        success: function (result) {
            var dom = "";
            if (result.length > 0 && result != null) {
                $.each(result, function (i, result) {
                    var img = "";
                    var date = '<p class="pending-match-date">Date:' + result.Date + '</p>';
                    var location = '<p class="pending-match-location">Location:' + result.Location + '</p>';
                    var msg = "";
                    var controls = "";
                    if (result.ImageSource != "") {
                        img = '<div class="pending-match-image"><img src="' + result.ImageSource + '"/></div>';
                    }
                    else {
                        
                        img = '<div class="pending-match-image"><img src="/Content/Images/emptypfp.png"/></div>';
                    }
                    if (parseInt(teamId) == parseInt(result.TeamId)) {

                        msg = '<p class="pending-match-msg">Your team challenged ' + result.TeamName + ' to a match.</p>';
                        controls = '<div class="pending-match-controls"><button class="decline-pending-match" value="'+result.Id+'" onclick="Decline(this)"><span class="decline-pending-match-ico"></span></button></div >';
                    }
                    else {
                        msg = '<p class="pending-match-msg">' + result.TeamName + ' challenged your team to a match.</p>';
                        controls = '<div class="pending-match-controls"><button class="accept-pending-match" value="' + result.Id + '" onclick="Accept(this)"> <span class="accept-pending-match-ico"></span></button><button class="decline-pending-match"value="' + result.Id +'"onclick="Decline(this)"><span class="decline-pending-match-ico"></span></button></div >';
                    }
                    dom += '<div class="pending-match"><div class="pending-match-data">' + img + '<div class="pending-match-info">' + date + location + msg + '</div></div>'+controls+'</div>';

                })
                $('#pending-matches').html(dom);
                var Match = $("button[name='Profile-Nav-Matches']").first();
                var notification = $(Match).siblings(".notification").first();
                var pending = $('#pending-matches-button').children(".button-notification").first();
                var teamNotification = $('#team-notification').first();
                var numberOfPending = parseInt(pending.html());
                if (pending.length>0 && numberOfPending > 0) {

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
                    $(notification).html(0);
                    $(notification).hide();
                    $(pending).html(0);
                    $(pending).hide();
                }
               
            }
            else {
                $('#pending-matches').html('<p class="pending-match-empty">There are no pending matches.<p>');
            }
        }
    })
    $("#modal-pending-matches").show();
})
function Decline(element) {
    $(".pending-match-error").html("");

    var matchId = element.value;
    $.ajax({
        method: "POST",
        url: "/Match/Decline",
        data:{
            matchId:matchId
        },
        success: function (result) {
            if (result == true) {
                element.closest('.pending-match').remove();
            }
            var matches = $('#pending-matches').children('.pending-match');
            var msg = "";
            if (!matches.length > 0) {
                msg = '<p class="pending-match-empty">There are no pending matches.<p>';
                $('#pending-matches').append(msg);
            }
        }
    })
}
function Accept(element) {
    var matchId = element.value;
    $(".pending-match-error").html("");
    $.ajax({
        method: "POST",
        url: "/Match/Accept",
        data: {
            matchId: matchId
        },
        success: function (result) {
            if (result.resultvalue) {
                element.closest('.pending-match').remove();
            }
            else {
                $(".pending-match-error").html(result.resultmsg);
                element.closest('.pending-match').remove();

            }
            var matches = $('#pending-matches').children('.pending-match');
            var msg = "";
            if (!matches.length > 0) {
                msg = '<p class="pending-match-empty">There are no pending matches.<p>';
                $('#pending-matches').append(msg);
            }
            $('#Matches-button').click();
        }
    })
}
$("#pendingmatches-modal-close").click(function () {
    $("#modal-pending-matches").hide();
})