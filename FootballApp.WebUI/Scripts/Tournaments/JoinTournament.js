$('#join-tournament').click(function () {
    var teamId = $('#team-id').val();
    $.ajax({
        method: "GET",
        url: "/Tournament/ListAvailableTournaments",
        data: {
            id: teamId
        },
        success: function (Tournaments) {
            var dom = "";
            if (Tournaments.length > 0) {
                $.each(Tournaments, function (i, t) {
                    dom += '<div class="available-tournaments-item"><div>Naziv:' + t.Name + '</div><div>Runda ' + t.CurrentRound + '/' + t.NumberOfRounds + '</div><div>Učesnici:' + t.CurrentNumberParticipants + '/' + t.NumberOfParticipants + '</div><div><button onclick="JoinTournament(this)" value="' + t.Id + '"style = "padding:5px; outline:none;border:none; background-color:green;color:white;">Učestvuj</button ></div></div>';
                })
            }
            else {
                dom = '<p class="available-tournaments-empty">Nema aktivnih turnira.<p>';
            }
            $('#available-tournaments').html(dom);
        }
    })
    $('#modal-join-tournaments').show();
});
$("#jointournaments-modal-close").click(function () {
    $('#modal-join-tournaments').hide();
})
function JoinTournament(element) {
    var tournamentId = element.value;
    var teamId = $('#team-id').val();
    $.ajax({
        method: 'POST',
        url: '/Tournament/JoinTournament',
        data: {
            teamId: teamId,
            tournamentId: tournamentId
        },
        success: function (result) {
            if (result.result) {
                var parent = $(element).parents('.available-tournaments-item').first();
                $(parent).remove();
                var dom = "";
                if (result.tournament != null) {
                    var t = result.tournament;
                    dom += '<div class="tournament-item"><div>Naziv:' + t.Name + '</div><div>Runda ' + t.CurrentRound + '/' + t.NumberOfRounds + '</div><div>Učesnici:' + t.CurrentNumberParticipants + '/' + t.NumberOfParticipants + '</div><div><a href="/Tournament/TournamentProfile/'+t.Id+'" class="view-tournament-button">Poseti</a></div>';
                    
                }
                $('.tournament-list').append(dom);
            }
        }
    })
};