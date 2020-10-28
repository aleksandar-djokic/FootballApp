$('.profilenav-button').click(function (event) {
    $('.profile-content-item').hide();
    $('.profilenav-button').removeClass("active");

    var navName = "#" + $(event.target).html();
    $(event.target).addClass("active");
    $(navName).show();

})