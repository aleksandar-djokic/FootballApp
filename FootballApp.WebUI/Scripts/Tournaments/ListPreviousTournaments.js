$('#previous-tournaments').click(function () {
    var teamId = $('#team-id').val();
    $.ajax({
        method: "GET",
        url: "/Tournament/ListPreviousTournaments",
        data: {
            id: teamId
        },
        success: function (Tournaments) {
            var dom = "";
            if (Tournaments.length > 0) {
                $.each(Tournaments, function (i, t) {
                    dom += '<div class="previous-tournaments-item"><div>Name:' + t.Name + '</div><div>Round ' + t.CurrentRound + '/' + t.NumberOfRounds + '</div><div>Participants:' + t.CurrentNumberParticipants + '/' + t.NumberOfParticipants + '</div><div><a href="/Tournament/TournamentProfile/' + t.Id +'"class="view-tournament-button">View</a></div></div>';
                })
            }
            else {
                dom = '<p class="previous-tournaments-empty">There are currently no active tournaments for you.<p>';
            }
            $('#previous-tournaments-list').html(dom);
        }
    })
    $('#modal-previous-tournaments').show();
});
$("#previoustournaments-modal-close").click(function () {
    $('#modal-previous-tournaments').hide();
})