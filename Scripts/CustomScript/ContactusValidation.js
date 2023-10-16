function onBlurName() {
    // Get the input element and the error message paragraph
    var inputElement = document.getElementById('fname');
    var errorMessageElement = document.getElementById('userNameError');

    // Perform your validation here
    var inputValue = inputElement.value.trim();

    if (inputValue === '') {
        // Display an error message if the input is empty
        errorMessageElement.textContent = 'Name is required.';
        inputElement.classList.add('is-invalid'); // Optional: Add a CSS class for styling
    } else {
        // Clear the error message if the input is valid
        errorMessageElement.textContent = '';
        inputElement.classList.remove('is-invalid'); // Optional: Remove the CSS class
    }
}
function onBlurEmail() {
    var emailInput = document.getElementById("email");
    var errorEmail = document.getElementById("errorEmail");
    var emailValue = emailInput.value.trim();

    // Regular expression for email validation
    var emailPattern = /^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$/;

    if (emailValue === "") {
        errorEmail.innerHTML = "Email is required.";
    } else if (!emailPattern.test(emailValue)) {
        errorEmail.innerHTML = "Invalid email format.";
    } else {
        errorEmail.innerHTML = ""; // Clear any previous error message.
    }
}
function onBlurMessage(name) {
    var errorMessage = document.getElementById('errorMessage');
    if (name.value.length < 10) {
        errorMessage.innerHTML = "*minimum 10 letters";
    } else {
        if (name.value.length > 50) {
            errorMessage.innerHTML = "*maximum 100 letters";
        } else {
            errorMessage.innerHTML = "";
        }
    }
}