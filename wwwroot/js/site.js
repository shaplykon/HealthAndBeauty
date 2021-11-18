function checkNotificationPromise() {
    try {
        Notification.requestPermission().then();
    } catch (e) {
        return false;
    }

    return true;
}


function showNotification(title, options) {
    var notification = new Notification(title, options);
    if (!("Notification" in window)) {
        alert('Ваш браузер не поддерживает HTML Notifications, его необходимо обновить.');
    }
    else if (Notification.permission === "granted") {
        var notification = new window.Notification(title, options);
        function clickFunc() {
            window.open("/");
        }
        notification.onclick = clickFunc;
    }

    else if (Notification.permission !== 'denied') {
        Notification.requestPermission.then((permission) => {
            if (permission === "granted") {
                var notification = new window.Notification(title, options);
            } else {
                alert('Вы запретили показывать уведомления');
            }
        });
    }
}


$(document).ready(function () {
    document.getElementById('notP').innerHTML = window.Notification.permission;

    const hubConnection = new signalR.HubConnectionBuilder()
        .withUrl("/notification")
        .build();

    hubConnection.on('Send', function (message) {
        showNotification("HealthAndBeauty notification", {
            body: message,
            icon: src = "https://kartinkin.net/uploads/posts/2021-07/thumbs/1627115474_22-kartinkin-com-p-ovoshchi-tekstura-krasivo-24.jpg",
            dir: 'auto'
        });
    });

    hubConnection.start();
});