$("#pending-matches-button").click(function () {
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
                        img = '<div class=""pending-match-image"><img src="' + result.ImageSource + '"/></div>';
                    }
                    else {
                        
                        img = '<div class="pending-match-image"><img src="/Content/Images/emptypfp.png"/></div>';
                    }
                    if (parseInt(teamId) == parseInt(result.TeamId)) {

                        msg = '<p class="pending-match-msg">Your team challenged ' + result.TeamName + ' to a match.</p>';
                        controls = '<div class="pending-match-controls"><button class="decline-pending-match"><span class="decline-pending-match-ico"></span></button></div >';
                    }
                    else {
                        msg = '<p class="pending-match-msg">' + result.TeamName + ' challenged your team to a match.</p>';
                        controls = '<div class="pending-match-controls"><button class="accept-pending-match" > <span class="accept-pending-match-ico"></span></button><button class="decline-pending-match"><span class="decline-pending-match-ico"></span></button></div >';
                    }
                    dom += '<div class="pending-match"><div class="pending-match-data">' + img + '<div class="pending-match-info">' + date + location + msg + '</div></div>'+controls+'</div>';

                })
                $('#pending-matches').html(dom);
            }
            else {

            }
        }
    })
    $("#modal-pending-matches").show();
})
$("#pendingmatches-modal-close").click(function () {
    $("#modal-pending-matches").hide();
})