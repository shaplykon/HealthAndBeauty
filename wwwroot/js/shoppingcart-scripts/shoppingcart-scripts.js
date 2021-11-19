var map;
var directionsService;
var directionsRenderer;

window.onload = () => {
    showDeliveryForm();
    document.querySelectorAll('input[type="radio"]').forEach((elem) => {
        elem.addEventListener("change", function (event) {
            if (this.name === "IsCash") {
                if (this.value === "True") {
                    document.getElementById("cardForm").classList.add('hidden')
                }
                else {
                    document.getElementById("cardForm").classList.remove('hidden')
                }
            }
            else {
                if (this.value === "True") {
                    showDeliveryForm();
                }
                else {
                    showPickupForm();
                }
            }

        });
    });

   directionsService = new google.maps.DirectionsService();
   directionsRenderer = new google.maps.DirectionsRenderer();

    var GoogleMapOptions = {
        center: new google.maps.LatLng(53.9024716, 27.5618225),
        zoom: 11,
        mapTypeId: google.maps.MapTypeId.ROADMAP
    };


    map = new google.maps.Map(document.getElementById("MapDiv"), GoogleMapOptions);

    directionsRenderer.setMap(map);


    document.getElementById("AddressId").addEventListener("change", () => { calculateAndDisplayRoute(directionsService, directionsRenderer) });
}

function showDeliveryForm() {
    document.getElementById("pickupAddressForm").classList.add('hidden')
    document.getElementById("MapDiv").classList.add('collapsed')
    document.getElementById("info-panel").classList.add('collapsed')
    document.getElementById("AddressId").classList.add('collapsed')

    document.getElementById("deliveryAddressForm").classList.remove('hidden')
    document.getElementById("addressInput").classList.remove('collapsed')
    document.getElementById("addressInputLabel").classList.remove('collapsed')

}

function showPickupForm() {
    document.getElementById("deliveryAddressForm").classList.add('hidden')
    document.getElementById("addressInput").classList.add('collapsed')
    document.getElementById("addressInputLabel").classList.add('collapsed')

    document.getElementById("pickupAddressForm").classList.remove('hidden')
    document.getElementById("MapDiv").classList.remove('collapsed')
    document.getElementById("info-panel").classList.remove('collapsed')

    document.getElementById("AddressId").classList.remove('collapsed')
}

function calculateAndDisplayRoute(directionsService, directionsRenderer) {
    navigator.geolocation.getCurrentPosition(function (position) {
        var lat = position.coords.latitude;
        var lng = position.coords.longitude;

        $.ajax({
            url: '/AboutUs/GetAddressByCoords/' + lat + '/' + lng,
            type: 'GET',
            success: function (data) {
                showRoute(data);
            }
        });
    });  
}

function showRoute(userAddress) {
    var selectDiv = document.getElementById("AddressId");
    var destinationAddress = selectDiv.options[selectDiv.selectedIndex].text;
    directionsService.
        route({
            origin: userAddress,
            destination: destinationAddress,
            travelMode: google.maps.TravelMode.DRIVING,
        })
        .then((response) => {
            directionsRenderer.setDirections(response);

            var legs = response.routes[0].legs;         

            var totalDistance = 0;
            var totalDuration = 0;

            for (var i = 0; i < legs.length; ++i) {
                totalDistance += legs[i].distance.value;
                totalDuration += legs[i].duration.value;
            }

            document.getElementById("duration").innerHTML = "Total distance: " + totalDistance + " m";
            document.getElementById("distance").innerHTML = "Total time: " + Math.round(totalDuration/60) + " minutes";
        })
        .catch((e) => window.alert("Directions request failed due to " + status));
}
