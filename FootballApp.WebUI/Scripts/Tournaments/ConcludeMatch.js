$('.conclude-match-button').click(function () {
    var id=$(this).val();
    $('#modal-concludematch').show();
    $.ajax({
        method: "GET",
        url: "/Tournament/GetMatchDetails",
        data:{
            id:id
        },
        success: function (result) {
            $('#match-id').val(id);
            $('#Team1Name').html(result.Team1Name);
            $('#Team1Id').val(result.Team1Id);
            $('#team1score').val(0);
            $('#Team2Name').html(result.Team2Name);
            $('#Team2Id').val(result.Team2Id);
            $('#team2score').val(0);

            $('#Team1Winner').attr('checked', false);
            $('#Team2Winner').attr('checked', false);
            $('#error').html('');
        }
        
    })
})
$('#concludematch-modal-close').click(function () {
    $('#modal-concludematch').hide();
})

$('.input-score').on('change', function () {
    var team1score = $("#team1score").val();
    var team2score = $("#team2score").val();
    if (team1score > team2score) {
        $('#Team1Winner').attr('checked', true);
    }
    else if (team2score > team1score) {
        $('#Team2Winner').attr('checked', true);
    }
    else {
        $('#Team1Winner').attr('checked', false);
        $('#Team2Winner').attr('checked', false);
    }
    
})
$('.match-score-submit').click(function () {
    if (Validate()) {
        var matchId = $('#match-id').val();
        var score1 = $('#team1score').val();
        var score2 = $('#team2score').val();
        var winnerId;
        if ($('#Team1Winner').prop('checked')) {
            winnerId = $('#Team1Id').val();
        } else {
            winnerId = $('#Team2Id').val();
        }
        $.ajax({
            method: 'POST',
            url: "/Tournament/ConcludeMatch",
            data: {
                matchId: matchId,
                score1: score1,
                score2: score2,
                winnerId: winnerId
            },
            success: function () {
                var match = $('#Match' + matchId);
                var matchteams= $(match).children(".match-teams").first();
                $(matchteams).children('.match-team').each(function () {
                    var matchname = $(this).children('.match-name').first().html();
                    if ($('#Team1Winner').prop('checked')) {

                        if (matchname == $('#Team1Name').html()) {
                            $(this).addClass('winner');
                        }
                    } else {
                        if (matchname == $('#Team2Name').html()) {
                            $(this).addClass('winner');
                        }
                    }
                })
                var matchteam = $(matchteams).children('.match-team');
                $(matchteam[0]).children('.match-score').html(score1);
                $(matchteam[1]).children('.match-score').html(score2);
                $('#modal-concludematch').hide();
                $("#Conclude" + matchId).hide();
            }

        })
    }
    else {
        $('#error').html('Please select winner.');  
    }

})
function Validate() {
    var team1 = $('#Team1Winner').prop('checked');
    var team2 = $('#Team2Winner').prop('checked');
    if (team1 == false && team2 == false) {
        return false;
    }
    else {
        return true;
    }
}