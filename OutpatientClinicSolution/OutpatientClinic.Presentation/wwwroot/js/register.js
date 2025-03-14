document.addEventListener("DOMContentLoaded", function () {
    const registerBtn = document.getElementById("registerBtn");
    const inputs = document.querySelectorAll("input");

    function validateForm() {
        let isValid = true;
        inputs.forEach(input => {
            if (input.value.trim() === "") {
                isValid = false;
            }
        });
        registerBtn.disabled = !isValid;
    }

    inputs.forEach(input => {
        input.addEventListener("input", validateForm);
    });
});
