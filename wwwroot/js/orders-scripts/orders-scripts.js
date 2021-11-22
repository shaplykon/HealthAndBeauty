var orderId;


function bindCourier() {
    $.ajax({
        url: '/Orders/BindCourier/' + orderId + '/' + document.getElementById("CourierId").value,
        type: 'POST'
    });
}


window.onload = () => {
    var modal = document.getElementById("myModal");
    modal.display = "none";
    var buttons = document.getElementsByName("setCourier");

    var span = document.getElementsByClassName("close")[0];

    for (i = 0; i < buttons.length; i++) {
        buttons[i].onclick = function (event) {
            modal.style.display = "block";
            orderId = event.currentTarget.parentNode.parentNode.getElementsByClassName("id")[0].innerHTML;
        }
    }

    // When the user clicks on the button, open the modal


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

}

