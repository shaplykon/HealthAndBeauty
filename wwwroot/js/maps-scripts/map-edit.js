window.onload = () => {

    var marker = null;
    var map = null;

    function initializeMap() {
        var GoogleMapOptions = {
            center: new google.maps.LatLng(53.9024716, 27.5618225),
            zoom: 11,
            mapTypeId: google.maps.MapTypeId.ROADMAP
        };

        map = new google.maps.Map(document.getElementById("MapDiv"), GoogleMapOptions);

        map.addListener('click', function (e) {
            placeMarker(e.latLng);
        });

        function placeMarker(location) {
            if (marker)
                marker.setPosition(location);
            else {
                marker = new google.maps.Marker({
                    position: location,
                    flat: false,
                    map: map,
                    draggable: true
                });
            }
            document.getElementById("lat").value = location.lat();
            document.getElementById("lon").value = location.lng();
            getAddressByCoords(location.lat(), location.lng());
        }
    }

    function setAddressAsync(addr) {
        document.getElementById("addr").value = addr;
    }

    function getAddressByCoords(lat, lon) {
        $.ajax({
            url: '/AboutUs/GetAddressByCoords/' + lat + '/' + lon,
            type: 'GET',
            success: function (data) {
                setAddressAsync(data);
            }
        });
    }

    initializeMap();
}

