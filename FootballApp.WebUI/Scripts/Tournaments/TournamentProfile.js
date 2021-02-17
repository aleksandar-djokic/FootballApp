$(window).on('load',function () {
    var firstRound = $('.profile-nav-button').first();
    $(firstRound).click();
})
$('.profile-nav-button').click(function (event) {
    $('.profile-content-item').hide();
    $('.profile-nav-item').removeClass("active");

    var navName = "#" + $(event.target).html();
    var navitem = $(event.target).closest(".profile-nav-item").first();
    $(navitem).addClass("active");
    $(navName).show();

})
