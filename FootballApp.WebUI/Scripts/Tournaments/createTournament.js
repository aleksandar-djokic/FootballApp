$('#participants-select').on('change', function () {
    var value = parseInt($(this).val());
    $('#NumberOfParticipants').val(value);
})