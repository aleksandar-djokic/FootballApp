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
})
$('#city-select').change(function () {
    $('#error-msg').text("");
})
$('#create-button').click(function () {
    var countryVal = $('#country-select').val();
    var cityVal = $('#city-select').val();
    var msg = "";
    if (cityVal == null) {

        msg = "You must select City.";
        $('#error-msg').text(msg);
    }
    else {
        $.ajax({
            method: 'POST',
            url: '/FreeAgent/Create',
            data: {
                countryValue: countryVal,
                cityValue: cityVal

            },
            success: function (result) {
                if (result) {
                    location.replace("https://localhost:44300/FreeAgent");
                }
                else {
                    $('#error-msg').text('It appears that some kind of error occurred,try again later.');
                }
            }
        });
    }
  
})