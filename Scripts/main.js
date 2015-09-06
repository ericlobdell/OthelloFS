$(function () {
    var uri = 'api/game/new';
    var gameboard = {};

    $.getJSON(uri)
        .done(function (data) {
            
            gameboard = data;
            console.log( "New Gameboard:", gameboard );

            var moveReq = {
                X: 1,
                Y: 1,
                Player: 2,
                Gameboard: gameboard
            };

            $.ajax({
                    url: "api/game/move",
                    type: "POST",
                    data: JSON.stringify( moveReq ),
                    contentType: "application/json"
                } )
                .then( function ( movedata ) {
                console.log( "Move Response: ", movedata );

                } )
                .fail( function ( response ) {
                console.log("FAIL: ", response);

            });
        });
});
