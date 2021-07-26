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
                    dom += '<div class="tournament-item"><div>Naziv:' + t.Name + '</div><div>Runda ' + t.CurrentRound + '/' + t.NumberOfRounds + '</div><div>Učesnici:' + t.CurrentNumberParticipants + '/' + t.NumberOfParticipants + '</div><div><a href="/Tournament/TournamentProfile/'+t.Id+'"class="view-tournament-button">Poseti</a></div></div>';
                })
            }
            $('.tournament-list').html(dom);
        }
    })
})