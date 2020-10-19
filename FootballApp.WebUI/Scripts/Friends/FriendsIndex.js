$('.friends-button').click(function (event) {
    $('.friends-content-item').hide();
    $('.friends-button').removeClass("active");

    var navName = "#" + $(event.target).html();
    $(event.target).addClass("active");
    $(navName).show();

})