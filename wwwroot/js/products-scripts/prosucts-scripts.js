function addInput() {

    var ingredientIndex = document.getElementsByClassName("ingredient-input").length;
    var aspAttribureString = "Ingredients[" + ingredientIndex + "].";

    if (ingredientIndex <= 4) {
        var newBlock = document.createElement("div");

        newBlock.classList.add("form-group");
        newBlock.classList.add("row");
        newBlock.classList.add("ingredient-input");

        var hiddenInput = document.createElement("input");
        hiddenInput.id = "Ingredients_" + ingredientIndex + "__Id";
        hiddenInput.type = "hidden";
        hiddenInput.setAttribute("asp-for", aspAttribureString + "Id");


        var newLabel = document.createElement("label");
        newLabel.classList.add("col-sm-2");
        newLabel.classList.add("col-form-label");

        var inputDiv = document.createElement("div");
        inputDiv.classList.add("col-sm-6");

        var newInput = document.createElement("input");
        newInput.type = "text";
        newInput.classList.add("form-control");
        newInput.setAttribute("asp-for", aspAttribureString + "Name");
        newInput.setAttribute("name", aspAttribureString + "Name");

        inputDiv.appendChild(newInput);

        var newSpan = document.createElement("span");
        newSpan.setAttribute("asp-validation-for", aspAttribureString + "Name");
        newSpan.setAttribute("data-valmsg-for", aspAttribureString + "Name");
        newSpan.setAttribute("data-valmsg-replace","true");

        newBlock.appendChild(hiddenInput);
        newBlock.appendChild(newLabel);
        newBlock.appendChild(inputDiv);
        newBlock.appendChild(newSpan);

        var button = document.getElementById("submitButton");
        button.parentNode.insertBefore(newBlock, button)
    }
    {
        // TODO: add notification window about max length of ingredients list(4 elements)
    }

    
}