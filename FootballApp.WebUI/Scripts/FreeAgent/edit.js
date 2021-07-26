function PopulateOnStart() {
    var country = $('#Country').val();
    var city = $('#City').val();
    $('#country-select option:contains("' + country + '")').prop('selected', true);
    var dom = "";
    var cityList = [];
    switch (country) {
        case "Srbija":
            cityList = ["Beograd", "Novi Sad", "Niš", "Drugo"];
            for (var i = 0; i < cityList.length; i++) {
                dom += '<option value="' + cityList[i] + '">' + cityList[i] + '</option>';

            }
            break;
        case "Rusija":
            cityList = ["Moskva", "Sankt Peterburg", "Soči", "Drugo"];
            for (var i = 0; i < cityList.length; i++) {
                dom += '<option value="' + cityList[i] + '">' + cityList[i] + '</option>';

            }
            break;
        case "Engleska":
            cityList = ["London", "Mančester", "Birmingen", "Drugo"];
            for (var i = 0; i < cityList.length; i++) {
                dom += '<option value="' + cityList[i] + '">' + cityList[i] + '</option>';

            }
            break;
        case "Nemačka":
            cityList = ["Minhen", "Frankfurt", "Berlin", "Drugo"];
            for (var i = 0; i < cityList.length; i++) {
                dom += '<option value="' + cityList[i] + '">' + cityList[i] + '</option>';

            }
            break;
    }
    $('#city-select').append(dom);
    $('#city-select').removeAttr('disabled');
    $('#city-select option:contains("' + city + '")').prop('selected', true);
    
}
$('#city-select').change(function () {
    var val = $('#city-select').val();
    $('#City').val(val);
})
$(window).on('load', function () {
    this.PopulateOnStart();
});

$('#country-select').change(function () {
    var value = $('#country-select').val();
    $('#city-select').html("");
    $('#error-msg').text("");
    var dom = "<option disabled selected value>Izaberi grad</option>";
    var cityList = [];
    switch (value) {
        case "Srbija":
            cityList = ["Beograd", "Novi Sad", "Niš", "Drugo"];
            for (var i = 0; i < cityList.length; i++) {
                dom += '<option value="' + cityList[i] + '">' + cityList[i] + '</option>';

            }
            break;
        case "Rusija":
            cityList = ["Moskva", "Sankt Peterburg", "Soči", "Drugo"];
            for (var i = 0; i < cityList.length; i++) {
                dom += '<option value="' + cityList[i] + '">' + cityList[i] + '</option>';

            }
            break;
        case "Engleska":
            cityList = ["London", "Mančester", "Birmingen", "Drugo"];
            for (var i = 0; i < cityList.length; i++) {
                dom += '<option value="' + cityList[i] + '">' + cityList[i] + '</option>';

            }
            break;
        case "Nemačka":
            cityList = ["Minhen", "Frankfurt", "Berlin", "Drugo"];
            for (var i = 0; i < cityList.length; i++) {
                dom += '<option value="' + cityList[i] + '">' + cityList[i] + '</option>';

            }
            break;
    }
    $('#city-select').append(dom);
    $('#city-select').removeAttr('disabled');
    $('.create-button').removeAttr('disabled');
    $('#Country').val(value);
})