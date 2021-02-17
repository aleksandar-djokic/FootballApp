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
                    dom += '<div class="available-tournaments-item"><div>Name:' + t.Name + '</div><div>Round ' + t.CurrentRound + '/' + t.NumberOfRounds + '</div><div>Participants:' + t.CurrentNumberParticipants + '/' + t.NumberOfParticipants + '</div><div><button onclick="JoinTournament(this)" value="' + t.Id + '"style = "border-radius:50%;height:40px;width:40px; outline:none;border:none; background-color:green;color:white;"> Join</button ></div></div>';
                })
            }
            else {
                dom = '<p class="available-tournaments-empty">There are currently no active tournaments for you.<p>';
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
                    dom += '<div class="tournament-item"><div>Name:' + t.Name + '</div><div>Round ' + t.CurrentRound + '/' + t.NumberOfRounds + '</div><div>Participants:' + t.CurrentNumberParticipants + '/' + t.NumberOfParticipants + '</div><div><button style="border-radius:50%;height:40px;width:40px; outline:none;border:none; background-color:hotpink;color:white;">View</button></div>';
                    
                }
                $('.tournament-list').html(dom);
            }
        }
    })
};