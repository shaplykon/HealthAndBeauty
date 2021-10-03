var orderId;


function bindCourier() {
    var courierId = document.getElementById("CourierId").value;


    $.ajax({
        url: '/Orders/BindCourier/' + orderId + '/' + courierId,
        type: 'POST'
    });
}


window.onload = () => {

    // Get the modal
    var modal = document.getElementById("myModal");
    modal.display = "none";
    // Get the button that opens the modal
    var btn = document.getElementById("setCourier");

    // Get the <span> element that closes the modal
    var span = document.getElementsByClassName("close")[0];

    // When the user clicks on the button, open the modal
    btn.onclick = function (event) {
        modal.style.display = "block";
        orderId = event.currentTarget.parentNode.parentNode.getElementsByClassName("id")[0].innerHTML;
    }

    // When the user clicks on <span> (x), close the modal
    span.onclick = function () {
        modal.style.display = "none";
    }

    // When the user clicks anywhere outside of the modal, close it
    window.onclick = function (event) {
        if (event.target == modal) {
            modal.style.display = "none";
        }
    }



    //timer();

    /*function timer() {
        var timeRowsList = document.getElementsByClassName('time-row');
        var timerRowsList = document.getElementsByClassName('timer-row');
        var dateRowsList = document.getElementsByClassName('date-row');
        var msRowsList = document.getElementsByClassName('ms');

        for (var i = 0; i < timerRowsList.length; i++) {
            var ms = parseInt(msRowsList[i].innerHTML, 10);
            var fromTime = new Date(ms);
            var toTime = new Date().getMilliseconds();


            //var differenceTravel = toTime.getTime() - fromTime.getTime();

            timerRowsList[i].innerHTML = toTime - ms;
        }

        var time = setInterval(timer, 1000);

    }*/
}

