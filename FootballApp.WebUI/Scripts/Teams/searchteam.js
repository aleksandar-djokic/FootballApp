$('#search-button').click(function () {
    var teamName = $('#search-bar').val();

    $.ajax({
        method: 'GET',
        url: '/Team/SearchTeam',
        data: {
            Name: teamName,
        },
        success: function (result) {
            var teamlist = $('.search-list');
            teamlist.html('');
            var dom = "";
            var timovi = result.teamlist;

            if (timovi.length > 0) {
                $.each(timovi, function (i, timovi) {
                    var img = '';
                    if (timovi.ImageSource != "") {
                        img = '<img src="' + timovi.ImageSource+'"/>';
                    }
                    else {
                        img = '<img src="/Content/Images/emptypfp.png" />';
                    }

                    dom += '<div class="search-team" ><div class="searchteam-data"><div class="searchteam-image">' + img + '</div><div class="searchteam-info"><p class="searchteam-name">' + timovi.Name + '</p><p class="searchteam-description">'+timovi.Description+'</p></div></div><div class="searchteam-button-wrap"><a class="searchteam-button" >Visit</a></div> </div ></div>';
                })
                
            }
            else {
                dom = '<p>There is no team with similiar name</p>';
               
            }
            teamlist.html(dom);
        },
        error: function () {
            console.log("ne valja");
        }
    });
})