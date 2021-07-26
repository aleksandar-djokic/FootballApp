$('#country-select').change(function () {
    var value = $('#country-select').val();
    $('#city-select').html("");
    $('#error-msg').text("");
    var dom = "<option disabled selected value>Select City</option>";
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
})
$('#city-select').change(function () {
    $('#error-msg').text("");
})
$('#create-button').click(function () {
    var countryVal = $('#country-select').val();
    var cityVal = $('#city-select').val();
    var msg = "";
    if (cityVal == null) {

        msg = "Morate izabrati grad.";
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
                    $('#error-msg').text('Izgleda da je došlo do neke greške,molimo pokušajte ponovo.');
                }
            }
        });
    }
  
})