var HEIGHT_UNITS;
var WEIGHT_UNITS;


window.onload += () => {

}

function onWeightUnitsChanged(value) { WEIGHT_UNITS = value; }

function onHeighttUnitsChanged(value) { HEIGHT_UNITS = value; }

function weightInputChanged() {
    var input = document.getElementById("weightInput");
    var value = input.innerHTML;
    if (!(/^[0-9]*[.,]?[0-9]+$/.test(value))) {
        alert(1);
    }
}

function heightInputChanged() {

}