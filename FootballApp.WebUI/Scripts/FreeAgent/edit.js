function PopulateOnStart() {
    var country = $('#Country').val();
    var city = $('#City').val();
    $('#country-select option:contains("' + country + '")').prop('selected', true);
    var dom = "";
    var cityList = [];
    switch (country) {
        case "Serbia":
            cityList = ["Beograd", "Novi Sad", "Nis","Jakovo", "Other"];
            for (var i = 0; i < cityList.length; i++) {
                dom += '<option value="' + cityList[i] + '">' + cityList[i] + '</option>';

            }
            break;
        case "Russia":
            cityList = ["Moscow", "St. Petersburg", "Sochi", "Other"];
            for (var i = 0; i < cityList.length; i++) {
                dom += '<option value="' + cityList[i] + '">' + cityList[i] + '</option>';

            }
            break;
        case "England":
            cityList = ["London", "Manchester", "Birmingham", "Other"];
            for (var i = 0; i < cityList.length; i++) {
                dom += '<option value="' + cityList[i] + '">' + cityList[i] + '</option>';

            }
            break;
        case "Germany":
            cityList = ["Munich", "Frankfurt", "Berlin", "Other"];
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
    var dom = "<option disabled selected value>Select City</option>";
    var cityList = [];
    switch (value) {
        case "Serbia":
            cityList = ["Beograd", "Novi Sad", "Nis", "Other"];
            for (var i = 0; i < cityList.length; i++) {
                dom += '<option value="' + cityList[i] + '">' + cityList[i] + '</option>';

            }
            break;
        case "Russia":
            cityList = ["Moscow", "St. Petersburg", "Sochi", "Other"];
            for (var i = 0; i < cityList.length; i++) {
                dom += '<option value="' + cityList[i] + '">' + cityList[i] + '</option>';

            }
            break;
        case "England":
            cityList = ["London", "Manchester", "Birmingham", "Other"];
            for (var i = 0; i < cityList.length; i++) {
                dom += '<option value="' + cityList[i] + '">' + cityList[i] + '</option>';

            }
            break;
        case "Germany":
            cityList = ["Munich", "Frankfurt", "Berlin", "Other"];
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