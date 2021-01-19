$('.news-item').on('click', function () {
    var id = $(this).children('.news-Id').val();
    var time = $(this).children('.news-time').html();
    var title = $(this).children('.news-title').html();
    var textString = $(this).children('.news-text').html();
    var text = textString.trimLeft();
    var edit ='<a href="/News/Edit/'+id+'"class="friends-options-msg"><span class="edit-news-ico"></span></a>'
    $('.modal-title').html(title);
    $('.modal-time').html(time);
    $('.modal-text').html(text);
    $('.modal-controls').html(edit);
    console.log(id);
    $("#modal").show();
})
$('#modal-close').click(function () {
    $('#modal').hide();
})

$(document).mouseup(function (e) {
    var modal = $("#modal");

    if (modal.is(e.target)) {
        modal.hide();
    }
});
