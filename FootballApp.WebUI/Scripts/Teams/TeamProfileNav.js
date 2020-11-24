$('.profilenav-button').click(function (event) {
    $('.profile-content-item').hide();
    $('.profilenav-button').removeClass("active");

    var navName = "#" + $(event.target).html();
    $(event.target).addClass("active");
    $(navName).show();

})

$('#Members-button').click(function () {
    $('#member-list').html('');
    var id = $('#team-id').val();
    $.ajax({
        method: 'GET',
        url: '/Team/GetTeamMembers',
        data: {
            teamId:id
        },
        success: function (result) {
            if (result.length > 0) {
                var dom = "";
                $.each(result, function (i, result) {
                   
                    var img = "";
                    if (result.ImageSource != "") {
                        img = '<img class="member-image" src="' + result.ImageSource + '"/>';
                    }
                    else {
                        img = '<img class="member-image" src="/Content/Images/emptypfp.png" />';
                    }
                    dom += '<div class="member-item"><div class="member-info">' + img + '<p>' + result.Name + '</p></div></div>';
                })
            }
            else {

            }
            $('#member-list').html(dom);
        }
    })
})