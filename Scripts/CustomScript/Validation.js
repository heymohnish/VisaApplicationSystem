function validateUsername(username) {
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
    var usernameInput = document.getElementById("username").value;
    var inputFieldUsername = document.getElementById("username");

    // Remove leading and trailing spaces from the username
    var trimmedUsername = usernameInput.trim();

    // Validate the username
    var validationMessage = validateUsername(trimmedUsername);

    if (validationMessage) {
        // Display the validation error
        errorUserName.innerHTML = validationMessage;
        //inputFieldUsername.style.borderColor = "red";
    } else {
        // Clear any previous error message
        errorUserName.innerHTML = "";
        //inputFieldUsername.style.borderColor = "green";
    }

    inputFieldUsername.style.borderWidth = "3px";
}