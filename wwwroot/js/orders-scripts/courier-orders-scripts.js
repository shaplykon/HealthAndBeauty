function takeOrder(sender, orderId) {
    $.ajax({
        url: '/Orders/TakeOrder/' + orderId,
        type: 'POST',
        success: function(data) {
            sender.parentNode.parentNode.getElementsByClassName("status")[0].innerHTML = data.status;
        }
    });
}


window.onload = () => {
    var buttons = document.getElementsByName("takeOrder");

    for (i = 0; i < buttons.length; i++) {
        buttons[i].onclick = function (event) {
            orderId = event.currentTarget.parentNode.parentNode.getElementsByClassName("id")[0].innerHTML;
            takeOrder(event.currentTarget, orderId);
        }
    }
}