function onBlurName() {
    var firstNameInput = document.getElementById("firstNameInput");
    var errorName = document.getElementById("errorName");
    var firstNameValue = firstNameInput.value.trim();

    // Regular expressions for numbers and special characters
    var hasNumber = /\d/.test(firstNameValue);
    var hasSpecialCharacter = /[!#@$%^&*()_+{}\[\]:;<>,.?~\\|]/.test(firstNameValue);

    if (firstNameValue === "") {
        errorName.innerHTML = "First name is required.";
    } else if (hasNumber) {
        errorName.innerHTML = "First name should not contain number.";
    } else if (hasSpecialCharacter) {
        errorName.innerHTML = "First name should not contain special character.";
    } else {
        errorName.innerHTML = ""; // Clear any previous error message.
    }
}
function onBlurlName() {
    var firstNameInput = document.getElementById("lastNameInput");
    var errorName = document.getElementById("errorlName");
    var firstNameValue = firstNameInput.value.trim();

    // Regular expressions for numbers and special characters
    var hasNumber = /\d/.test(firstNameValue);
    var hasSpecialCharacter = /[!#@$%^&*()_+{}\[\]:;<>,.?~\\|]/.test(firstNameValue);

    if (firstNameValue === "") {
        errorlName.innerHTML = "Last name is required.";
    } else if (hasNumber) {
        errorlName.innerHTML = "Last name should not contain number.";
    } else if (hasSpecialCharacter) {
        errorlName.innerHTML = "Last name should not contain special character.";
    } else {
        errorlName.innerHTML = ""; // Clear any previous error message.
    }
}
function onBlurEmail() {
    var emailInput = document.getElementById("emailInput");
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
function validatePassword(input) {
    var passwordInput = input.value;
    var passwordError = document.getElementById("passwordError");

    // Define your password criteria here
    var minLength = 8;
    var hasUppercase = /[A-Z]/.test(passwordInput);
    var hasLowercase = /[a-z]/.test(passwordInput);
    var hasNumber = /\d/.test(passwordInput);
    var hasSpecialCharacter = /[!@@#$%^&*()_+{}\[\]:;<>,.?~\\|]/.test(passwordInput);

    if (passwordInput.length < minLength ||
        !hasUppercase || !hasLowercase || !hasNumber || !hasSpecialCharacter) {
        passwordError.innerHTML = "Password must be at least 8 characters long and contain at least one uppercase letter, one lowercase letter, one number, and one special character.";
    } else {
        passwordError.innerHTML = ""; // Clear any previous error message.
    }
}
function validatecPassword(input) {
    var cpasswordInput = input.value;
    var cpasswordError = document.getElementById("cpasswordError");
    var passwordInput = document.getElementById("passwordInput").value;

    if (cpasswordInput === "") {
        cpasswordError.innerHTML = "Password should not be empty";
    } else if (cpasswordInput !== passwordInput) {
        cpasswordError.innerHTML = "Password mismatch";
    } else {
        cpasswordError.innerHTML = "";
    }
}
function validateUsername() {
    var usernameInput = document.getElementById("usernameInput").value;
    var emailInput = document.getElementById("emailInput").value;
    var usernameError = document.getElementById("usernameError");

    if (usernameInput === "") {
        usernameError.innerHTML = "Username should not be empty";
    } else if (usernameInput !== emailInput) {
        usernameError.innerHTML = "Username should match the provided email";
    } else {
        usernameError.innerHTML = "";
    }
}
function validateUsernamer() {
    var usernameInput = document.getElementById("usernameInput").value;
    var usernameError = document.getElementById("usernameError");

    // Define your validation rules here
    const minLength = 4; // Minimum length of the username
    const maxLength = 20; // Maximum length of the username
    const validCharacters = /^[a-zA-Z0-9._-]+$/; // Regular expression for valid characters

    // Remove leading and trailing spaces from the username
    var trimmedUsername = usernameInput.trim();

    if (trimmedUsername.length < minLength || trimmedUsername.length > maxLength) {
        usernameError.innerHTML = 'Username must be between ' + minLength + ' and ' + maxLength + ' characters.';
        document.getElementById("usernameInput").style.borderColor = "red";
    } else if (!validCharacters.test(trimmedUsername)) {
        usernameError.innerHTML = 'Username can only contain letters, numbers, periods, underscores, and hyphens.';
        document.getElementById("usernameInput").style.borderColor = "red";
    } else {
        // Clear any previous error message and set the border color to green
        usernameError.innerHTML = "";
        document.getElementById("usernameInput").style.borderColor = "green";
    }
}

function validateUsernamed(username) {
    // Define your validation rules here
    const minLength = 4; // Minimum length of the username
    const maxLength = 20; // Maximum length of the username
    const validCharacters = /^[a-zA-Z0-9._-]+$/; // Regular expression for valid characters

    if (username.length < minLength || username.length > maxLength) {
        return 'Username must be between ' + minLength + ' and ' + maxLength + ' characters.';
    }

    if (!validCharacters.test(username)) {
        return 'Username can only contain letters, numbers, periods, underscores, and hyphens.';
    }

    return null; // Return null if the username is valid
}

function onBlurUser1() {
    var usernameInput = document.getElementById("usernameInput").value;
    var inputFieldUsername = document.getElementById("usernameInput");
    var usernameError = document.getElementById("usernameError");

    // Remove leading and trailing spaces from the username
    var trimmedUsername = usernameInput.trim();

    // Validate the username
    var validationMessage = validateUsernamed(trimmedUsername);

    if (validationMessage) {
        // Display the validation error
        usernameError.innerHTML = validationMessage;
        inputFieldUsername.style.borderColor = "red";
    } else {
        // Clear any previous error message
        usernameError.innerHTML = "";
        inputFieldUsername.style.borderColor = "green";
    }

    inputFieldUsername.style.borderWidth = "3px";
}

function setMinDate() {
    var date = new Date();
    const year = date.getFullYear();
    const month = String(date.getMonth() + 1).padStart(2, '0');
    const day = String(date.getDate()).padStart(2, '0');
    const final = year + "-" + month + "-" + day;
    var inputField = document.getElementById("DOB");
    inputField.max = final;

}
function calculateAge() {
    // Get the selected date from the date input field
    var selectedDate = document.getElementById("DOB").value;

    // Calculate the age based on the selected date
    var today = new Date();
    var birthDate = new Date(selectedDate);
    var age = today.getFullYear() - birthDate.getFullYear();

    // Update the value of the age input field
    document.getElementById("age").value = age;
    if (age < 18) {
        document.getElementById("age").style.borderColor = "red";
        errorAge.innerHTML = "You must be above 18";
    } else {
        document.getElementById("age").style.borderColor = "";
        errorAge.innerHTML = ""
    }
    document.getElementById("age").style.borderWidth = "3px";
    document.getElementById("DOB").style.borderWidth = "3px";

}
function validateField(field) {
    var value = field.value.trim();
    var errorSpan = field.parentElement.querySelector(".text-danger");
    if (value === '') {
        errorSpan.innerText = 'This cannot be empty.';
    } else {
        errorSpan.innerText = '';
    }
}
