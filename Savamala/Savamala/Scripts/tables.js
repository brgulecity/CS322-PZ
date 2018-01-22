function addMarker(id, x, y) {
    document.write('<a ' +
        'class="reel-annotation title" ' +
        'data-toggle="modal" ' +
        'href="#myModal" ' +
        'id="' + id +
        '" data-x="' + x +
        '" data-y="' + y +
        '" data-for="image">' +
        '<img src="/Images/reservation-icon-14.png" width="75px" />' +
        '</a>');

    $("#"+id).on("click",
        function () {
            var str = id;
            var result = str.substring(5);
            activeTable = result;

            $("#reservationTitle").text("Rezervisite sto br: " + result);
        });
}

