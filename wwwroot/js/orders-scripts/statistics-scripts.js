google.charts.load('current', { 'packages': ['corechart'] });
google.charts.setOnLoadCallback(drawChart);

function drawChart() {
    period = 15;
    displayChartsWithPeriod(period);
}

function displayChartsWithPeriod(period) {
    $.ajax({
        url: '/Orders/GetOrdersAmountStatistics/' + period,
        type: 'GET',
        success: function (jsonData) {
            var data = google.visualization.arrayToDataTable(jsonData, false);
            var option = {
                title: "Orders amount statistics",
                width: 600,
                height: 600
            };
            var chart = new google.visualization.LineChart(document.getElementById('ordersAmountChart'));
            chart.draw(data, option);
        }
    });

    $.ajax({
        url: '/Orders/GetOrdersStatistics/' + period,
        type: 'GET',
        success: function (jsonData) {
            var data = google.visualization.arrayToDataTable(jsonData, false);
            var option = {
                title: "Orders statistics",
                width: 600,
                height: 600,
                is3D: true
            };

            chart = new google.visualization.PieChart(document.getElementById('ordersChart'));
            chart.draw(data, option);
        }
    });
}