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
                    dom += '<div class="previous-tournaments-item"><div>Naziv:' + t.Name + '</div><div>Runda ' + t.CurrentRound + '/' + t.NumberOfRounds + '</div><div>Učesnici:' + t.CurrentNumberParticipants + '/' + t.NumberOfParticipants + '</div><div><a href="/Tournament/TournamentProfile/' + t.Id +'"class="view-tournament-button">Poseti</a></div></div>';
                })
            }
            else {
                dom = '<p class="previous-tournaments-empty">Nema prethodnih turnira.<p>';
            }
            $('#previous-tournaments-list').html(dom);
        }
    })
    $('#modal-previous-tournaments').show();
});
$("#previoustournaments-modal-close").click(function () {
    $('#modal-previous-tournaments').hide();
})