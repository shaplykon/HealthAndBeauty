function setInDeliveryStatus(orderId) {
    var button = document.getElementById('set-' + orderId);
    button.classList.remove('btn-success');
    button.classList.add('btn-danger');
    button.innerHTML = 'Delivered';
    button.setAttribute("name", "deliverOrder");
    button.setAttribute("id", "delivery-"  + orderId);
    setListeners();
}


function takeOrder(sender, orderId) {
    $.ajax({
        url: '/Orders/TakeOrder/' + orderId,
        type: 'POST',
        success: function(data) {
            sender.parentNode.parentNode.getElementsByClassName("status")[0].innerHTML = data.status;
            setInDeliveryStatus(orderId);
        }
    });
}

function setDeliveredStatus(orderId) {
    document.getElementById('delivery-' + orderId).style.visibility = 'hidden';
}

function deliverOrder(sender, orderId) {
    $.ajax({
        url: '/Orders/DeliverOrder/' + orderId,
        type: 'POST',
        success: function (data) {
            sender.parentNode.parentNode.getElementsByClassName("status")[0].innerHTML = data.status;
            setDeliveredStatus(orderId);
        }
    });
}


function setListeners() {
    var takeOrderButtons = document.getElementsByName("takeOrder");

    for (i = 0; i < takeOrderButtons.length; i++) {
        takeOrderButtons[i].onclick = function (event) {
            orderId = event.currentTarget.parentNode.parentNode.getElementsByClassName("id")[0].innerHTML;
            takeOrder(event.currentTarget, orderId);
        }
    }

    var deliverButtons = document.getElementsByName("deliverOrder");
    for (i = 0; i < deliverButtons.length; i++) {
        deliverButtons[i].onclick = function (event) {
            orderId = event.currentTarget.parentNode.parentNode.getElementsByClassName("id")[0].innerHTML;
            deliverOrder(event.currentTarget, orderId);
        }
    }
}

window.onload = () => {
    setListeners();
}