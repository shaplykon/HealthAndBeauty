


$(function () {
    $("#product").autocomplete({
        source: function (request, response) {
            $.ajax({
                type: "POST",
                url: "/CalorificCalculator/GetAutocompleteSuggestion?product=" + document.getElementById("product").value,
                dataType: "json",
                error: function (xhr, textStatus, errorThrown) {
                    alert('Error: ' + xhr.responseText);
                },
                success: function (data) {
                    response(data);
                }
            });
        },
        select: getProductInfo(),
        minLength: 2

    });
});

function getProductInfo() {
    if (document.getElementById("product").value != "") {
        $.ajax({
            type: "POST",
            url: "/CalorificCalculator/GetProductCalorific?product=" + document.getElementById("product").value,
            dataType: "json",
            error: function (xhr, textStatus, errorThrown) {
                alert('Error: ' + xhr.responseText);
            },
            success: function (data) {
                document.getElementById("nutrition-label").style.visibility = 'visible'
                document.getElementById("table-responsive").style.visibility = 'visible'

                document.getElementById("calories").innerHTML = data.calorific;
                document.getElementById("total-fat").innerHTML = data.totalFat;
                document.getElementById("saturated-fat").innerHTML = data.saturatedFat;
                document.getElementById("cholesterol").innerHTML = data.cholesterol;
                document.getElementById("sodium").innerHTML = data.sodium;
                document.getElementById("protein").innerHTML = data.protein;
                document.getElementById("carbohydrate").innerHTML = data.carbohydrate;
                document.getElementById("potassium").innerHTML = data.potassium;

                document.getElementById("product-weight").innerHTML = data.weight;
                document.getElementById("product-unit").innerHTML = data.unit;
                document.getElementById("product-name").innerHTML = document.getElementById("product").value;
                document.getElementById("product-calorific").innerHTML = data.calorific;
                document.getElementById("product-image").src = data.photo.photoUrl;

                document.getElementById("fat-norm").innerHTML = getPercantage(data.totalFat, 65);
                document.getElementById("saturated-fat-norm").innerHTML = getPercantage(data.saturatedFat, 20);
                document.getElementById("cholesterol-norm").innerHTML = getPercantage(data.cholesterol, 300);
                document.getElementById("sodium-norm").innerHTML = getPercantage(data.sodium, 2300);
                document.getElementById("potassium-norm").innerHTML = getPercantage(data.potassium, 3450);

 
            }
        });
    }
}

function getPercantage(value, norm) {
    return Number((value / norm) * 100).toFixed(2);
}