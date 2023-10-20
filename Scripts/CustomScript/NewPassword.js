function validateField(field) {
    var value = field.value.trim();
    var errorSpan = field.parentElement.querySelector(".text-danger");
    if (value === '') {
        errorSpan.innerText = 'This cannot be empty.';
        errorSpan.style.color = 'red';
    } else {
        errorSpan.innerText = '';
    }
}
function validatePassword(field) {
    var passwordInput = field.value;
    var passwordError = field.parentElement.querySelector(".text-danger");

    // Define your password criteria here
    var minLength = 8;
    var hasUppercase = /[A-Z]/.test(passwordInput);
    var hasLowercase = /[a-z]/.test(passwordInput);
    var hasNumber = /\d/.test(passwordInput);
    var hasSpecialCharacter = /[!@#$%^&*()_+{}\[\]:;<>,.?~\\|]/.test(passwordInput);

    if (passwordInput.length < minLength ||
        !hasUppercase || !hasLowercase || !hasNumber || !hasSpecialCharacter) {
        passwordError.innerHTML = "Password must be at least 8 characters long and contain at least one uppercase letter, one lowercase letter, one number, and one special character.";
        passwordError.style.color = 'red';
    } else {
        passwordError.innerHTML = ""; // Clear any previous error message.
    }
}