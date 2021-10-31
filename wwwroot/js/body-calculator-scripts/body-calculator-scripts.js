var HEIGHT_UNITS;
var WEIGHT_UNITS;
var WEIGHT = 0;
var HEIGHT = 0;

window.onload += () => {

}

function onWeightUnitsChanged(value) { WEIGHT_UNITS = value; setBmiValue(); }

function onHeightUnitsChanged(value) { HEIGHT_UNITS = value; setBmiValue(); }

function setBmiValue() {
    var bmiInput = document.getElementById("bmi");

    var calculatedHeight;
    var calculatedWeight;

    if (HEIGHT_UNITS == "inches") 
        calculatedHeight = HEIGHT * 2.54;    
    else
        calculatedHeight = HEIGHT;
    
    if (WEIGHT_UNITS == "pounds") 
        calculatedWeight = WEIGHT * 0.45;
    else 
        calculatedWeight = WEIGHT;
   
    var bmiValue = calculatedWeight / ((calculatedHeight / 100) * (calculatedHeight / 100));

    if (!isNaN(bmiValue) && bmiValue != "Infinity")
        bmiInput.value = bmiValue.toFixed(2);
    else 
        bmiInput.value = "";

    setBmiInputColor(bmiValue);
}

function setBmiInputColor(bmiValue) {
    var bmiInput = document.getElementById("bmi");

    if (bmiValue < 18.5 || bmiValue >= 30) 
        bmiInput.style.color = "red";    
    else if (bmiValue >= 18.5 && bmiValue <= 25) 
        bmiInput.style.color = "green";    
    else 
        bmiInput.style.color = "orange";
}

function inputChanged() {
    var weightInput = document.getElementById("weightInput");
    var heightInput = document.getElementById("heightInput");

    WEIGHT = weightInput.value;
    HEIGHT = heightInput.value;

    setBmiValue();
}


function validate(evt) {
    var theEvent = evt || window.event;

    // Handle paste
    if (theEvent.type === 'paste') {
        key = event.clipboardData.getData('text/plain');
    } else {
        // Handle key press
        var key = theEvent.keyCode || theEvent.which;
        key = String.fromCharCode(key);
    }
    var regex = /[0-9]|\./;
    if (!regex.test(key)) {
        theEvent.returnValue = false;
        if (theEvent.preventDefault) theEvent.preventDefault();
    }
}