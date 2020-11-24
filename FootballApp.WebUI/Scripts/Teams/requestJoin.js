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
                $('#nonmember-msg').html('You have successfully sent join request.');
                $('#request-button').prop('disabled', true);
            }
            else {

                $('#nonmember-msg').html('Your request failed');
            }
        }
    })
}