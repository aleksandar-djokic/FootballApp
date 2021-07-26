$("#creatematch-button").click(function () {
    $("#modal-create-matches").show();
})
$("#creatematches-modal-close").click(function () {
    $("#modal-create-matches").hide();
    resetCreateInputValues();
    $('#creatematch-errors').html("");
})
$("#create-match-button").click(function () {
    var teamId = $('#team-id').val();
    var teamName = $('#creatematch-TeamName').val();
    var Adress = $('#creatematch-Adress').val();
    var DateString = $('#creatematch-DateTime').val();
    if (validate(teamId, teamName, Adress, DateString)) {

        var DateTime = new Date(DateString);
        $.ajax({
            method: 'POST',
            url: '/Match/Create',
            data: {
                team1Id: teamId,
                team2Name: teamName,
                Adress: Adress,
                dateTime: DateTime.toISOString()

            },
            success: function (result) {
                if (result.resultvalue) {
                    $('#creatematch-errors').append('<p class="creatematch-success">' + result.resultmsg + '</p>');
                    resetCreateInputValues();
                    
                }
                else {
                    $('#creatematch-errors').append('<p class="creatematch-error">'+result.resultmsg+'</p>');
                }
                console.log(result);
            }
        })
    }
})
function validate(teamid, teamname, adress, dateString) {
    $('#creatematch-errors').html("");
    var result = true;
    var dateTime = new Date(dateString);
    if (teamid == null) {
        result = false;
        
    }
    if (teamname == "") {
        $('#creatematch-errors').append('<p class="creatematch-error">Naziv tima mora biti popunjen.</p>');
        result = false;
    }
    if (adress == "") {
        $('#creatematch-errors').append('<p class="creatematch-error">Adresa mora biti popunjena.</p>');
        result = false;
    }
    if (dateString == "") {
        $('#creatematch-errors').append('<p class="creatematch-error">Datum nije izabran.</p>');
        result = false;
    }
    if (Date.now() > dateTime.getTime()) {
        $('#creatematch-errors').append('<p class="creatematch-error">Molimo vas izaberite vreme u budućnosti.</p>');
        result = false;
    }
    return result;
}
function resetCreateInputValues() {
     $('#creatematch-TeamName').val("");
     $('#creatematch-Adress').val("");
     $('#creatematch-DateTime').val("");
}