$(".news-item").click(function () {
    var time = $(this).children(".news-time").first().html();
    var title = $(this).children(".news-title").first().html();
    var text = "&emsp;" + $(this).children(".news-text").first().html();
    $('.modal-news-wrap').children(".modal-time").first().html(time);
    $('.modal-news-wrap').children(".modal-title").first().html(title);
    $('.modal-news-wrap').children(".modal-text").first().html(text);
    $('#modal').show();
})

$("#modal-close").click(function () {
    $("#modal").hide();
})