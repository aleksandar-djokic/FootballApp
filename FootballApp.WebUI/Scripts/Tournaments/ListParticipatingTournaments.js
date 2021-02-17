$('#Tournaments-button').click(function () {
    var teamId = $('#team-id').val();
    $.ajax({
        method: "GET",
        url: "/Tournament/ListParticipatingTournaments",
        data: {
            id: teamId
        },
        success: function (Tournaments) {
            var dom = "";
            if (Tournaments.length > 0) {
                $.each(Tournaments, function (i, t) {
                    dom += '<div class="tournament-item"><div>Name:' + t.Name + '</div><div>Round ' + t.CurrentRound + '/' + t.NumberOfRounds + '</div><div>Participants:' + t.CurrentNumberParticipants + '/' + t.NumberOfParticipants + '</div><div><a href="/Tournament/TournamentProfile/'+t.Id+'"class="view-tournament-button">View</a></div></div>';
                })
            }
            $('.tournament-list').html(dom);
        }
    })
})