$('#freeagent-button').click(function () {
    var teamId = $('#team-id').val();
    $('#freeagent-items').html("");
    $.ajax({
        method: 'GET',
        url: '/FreeAgent/GetFreeAgents',
        data: {
            teamId: teamId
        },
        success: function (result) {
            var dom = "";
            if (result.length > 0) {
                $.each(result, function (i, result) {
                    var img = "";
                    if (result.ImageSource != "") {
                        img = '<img src="' + result.ImageSource + '"/>';
                    }
                    else {
                        img = '<img src="/Content/Images/emptypfp.png" />';
                    }
                    var controls = '<div class="freeagent-controls"><button class="freeagent-invite" value="'+result.FreeAgentId+'" onclick="Request(this)">Pozovi</button></div>';
                    

                    dom += '<div class="freeagent-item"><div class="freeagent-data"><div class="freeagent-image">' + img + '</div><div class="freeagent-info"><p class="freeagent-name">' + result.Name + '</p><p class="freeagent-location">' + result.Country +','+result.City + '</p></div></div>' + controls + '</div>';
                })
            }
            if (!result.length > 0) {
                dom = '<p class="freeagent-emptymsg">Trenutno nema slobodnih agenata.</p>';
            }
            $('#freeagent-items').html(dom);
        }

    })
    $('#modal-freeagents').show();
})
$('#freeagent-modal-close').click(function () {
    $('#modal-freeagents').hide();
})

function Request(element) {
    id = element.value;
    var teamId = $('#team-id').val();
    $.ajax({
        method: 'POST',
        url: '/FreeAgent/SendRequestToFreeAgent',
        data: {
            agentId:id,
            teamId:teamId
        },
        success: function (result) {
            if (result == true) {
                element.closest('.freeagent-item').remove();
            }
            var requests = $('#freeagent-items').children('.freeagent-item');
            var msg = "";
            if (!requests.length > 0) {
                msg = '<p class="freeagent-emptymsg">Trenutno nema slobodnih agenata.</p>'
                $('#freeagent-items').append(msg);
            }
            $("#Members-button").trigger("click");
        }
    })
}
//Search free agents
$('#country-select').change(function () {
    var value = $('#country-select').val();
    $('#city-select').html("");
    $('#error-msg').text("");
    var dom = '<option disabled selected value="">Select City</option>';
    var cityList = [];
    switch (value) {
        case "Srbija":
            cityList = ["Svi", "Beograd", "Novi Sad", "Niš", "Drugo"];
            for (var i = 0; i < cityList.length; i++) {
                dom += '<option value="' + cityList[i] + '">' + cityList[i] + '</option>';

            }
            break;
        case "Rusija":
            cityList = ["Svi", "Moskva", "Sankt Peterburg", "Soči", "Drugo"];
            for (var i = 0; i < cityList.length; i++) {
                dom += '<option value="' + cityList[i] + '">' + cityList[i] + '</option>';

            }
            break;
        case "Engleska":
            cityList = ["Svi", "London", "Mančester", "Birmingen", "Drugo"];
            for (var i = 0; i < cityList.length; i++) {
                dom += '<option value="' + cityList[i] + '">' + cityList[i] + '</option>';

            }
            break;
        case "Nemačka":
            cityList = ["Svi","Minhen", "Frankfurt", "Berlin", "Drugo"];
            for (var i = 0; i < cityList.length; i++) {
                dom += '<option value="' + cityList[i] + '">' + cityList[i] + '</option>';

            }
            break;
    }
    if (value != "Svi") {   

        $('#city-select').append(dom);
        $('#city-select').removeAttr('disabled');
    }
    else {
        $('#city-select').attr('disabled',true);
    }
    SearchAgents();
})
$('#city-select').change(function () {
    SearchAgents();
})
function SearchAgents() {

    var teamId = $('#team-id').val();
    var city = $('#city-select').val() == null ? "" : $('#city-select').val();
    var country = $('#country-select').val();

    $.ajax({
        method: 'GET',
        url: '/FreeAgent/SearchAgents',
        data: {
            teamId:teamId,
            Country: country,
            City: city
        },
        success: function (result) {
            var dom = "";
            if (result.length > 0) {
                $.each(result, function (i, result) {
                    var img = "";
                    if (result.ImageSource != "") {
                        img = '<img src="' + result.ImageSource + '"/>';
                    }
                    else {
                        img = '<img src="/Content/Images/emptypfp.png" />';
                    }
                    var controls = '<div class="freeagent-controls"><button class="freeagent-invite" value="' + result.FreeAgentId + '" onclick="Request(this)">Pozovi</button></div>';


                    dom += '<div class="freeagent-item"><div class="freeagent-data"><div class="freeagent-image">' + img + '</div><div class="freeagent-info"><p class="freeagent-name">' + result.Name + '</p><p class="freeagent-location">' + result.Country + ',' + result.City + '</p></div></div>' + controls + '</div>';
                })
            }
            if (!result.length > 0) {
                dom = '<p class="freeagent-emptymsg">Nema slobodnih igrača za odabranu lokaciju.</p>';
            }
            $('#freeagent-items').html(dom);
        }

    })
}