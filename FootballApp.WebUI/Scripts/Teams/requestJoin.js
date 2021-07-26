function RequestJoin(element) {
    var id = element.value;
    $.ajax({
        method: 'POST',
        url: '/Team/SendTeamJoinRequest',
        data: {
            teamId: id
        },
        success: function (result) {
            if (result == true) {
                $('#nonmember-msg').html('Uspešno ste poslali zahtev.');
                $('#request-button').prop('disabled', true);
            }
            else {

                $('#nonmember-msg').html('Došlo je do greške pri slanju zahteva');
            }
        }
    })
}