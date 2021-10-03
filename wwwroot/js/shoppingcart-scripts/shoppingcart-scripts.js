window.onload = () => {

    document.querySelectorAll('input[type="radio"]').forEach((elem) => {
        elem.addEventListener("change", function (event) {
            if (this.name === "IsCash") {
                if (this.value === "True") {
                    document.getElementById("cardForm").classList.add('hidden')
                }
                else {
                    document.getElementById("cardForm").classList.remove('hidden')
                }
            }
            else {
                if (this.value === "True") {
                    document.getElementById("pickupAddressForm").classList.add('hidden')
                    document.getElementById("deliveryAddressForm").classList.remove('hidden')
                }
                else {
                    document.getElementById("deliveryAddressForm").classList.add('hidden')
                    document.getElementById("pickupAddressForm").classList.remove('hidden')

                }
            }
            
        });
    });




}

