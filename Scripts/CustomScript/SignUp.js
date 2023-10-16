
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
        document.getElementById("DOB").style.borderColor = "red";
        errorAge.innerHTML = "You must be above 18"
    } else {
        document.getElementById("age").style.borderColor = "green";
        document.getElementById("DOB").style.borderColor = "green";
        errorAge.innerHTML = ""
    }
    document.getElementById("age").style.borderWidth = "3px";
    document.getElementById("DOB").style.borderWidth = "3px";

}
// EMAIL

//
function onBlurEmail() {
    var emailInput = document.getElementById("emailInput");
    var inputFieldEmail = document.getElementById("emailInput");
    var trimmedStr = emailInput.trim();
    console.log(emailInput + " " + trimmedStr);
    var bool = true;
    var emailPattern = /^[a-zA-Z0-9._-]+[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$/;
    if (trimmedStr != emailInput) {
        errorMail.innerHTML = "Email contain space ";
        bool = false;
    } else {
        errorMail.innerHTML = "";
    }
    if (emailInput === '') {
        errorMail.innerHTML = "Email should be filled ";
        inputFieldEmail.style.borderColor = "red";
    } else {
        if (bool == true) {
            checkValidMail(emailPattern, emailInput);
        }
        else {
            if (bool == false) {
                inputFieldEmail.style.borderColor = "red";
            } else {
                inputFieldEmail.style.borderColor = "green";
            }
        }
    }
    inputFieldEmail.style.borderWidth = "3px";
}
function checkValidMail(emailPattern, emailInput) {
    var inputFieldEmail = document.getElementById("email");
    if (!emailPattern.test(emailInput)) {
        errorMail.innerHTML = "Enter valid email";
        inputFieldEmail.style.borderColor = "red";
    } else {
        inputFieldEmail.style.borderColor = "green";
        errorMail.innerHTML = "";
    }
}

//FNAME
function onBlurName() {
    var name = document.getElementById("fname").value;
    var inputFieldName = document.getElementById("fname");
    var errorName = document.getElementById("errorName");
    var trimmedStr = name.trim();

    // Define regular expressions to check for special characters and numbers
    var specialCharacterPattern = /[!@#$%^&*()_+{}\[\]:;<>,.?~\\|]/;
    var numberPattern = /\d/;

    // Check if the name contains special characters or numbers
    if (specialCharacterPattern.test(name)) {
        errorName.innerHTML = "Name contains special characters.";
        inputFieldName.style.borderColor = "red";
    } else if (numberPattern.test(name)) {
        errorName.innerHTML = "Name contains numbers.";
        inputFieldName.style.borderColor = "red";
    } else if (trimmedStr === '') {
        errorName.innerHTML = "Name should be filled.";
        inputFieldName.style.borderColor = "red";
    } else {
        // If none of the conditions are met, the input is valid.
        inputFieldName.style.borderColor = "green";
        errorName.innerHTML = "";
    }
    inputFieldName.style.borderWidth = "3px";
}



function checkNumber(numbers) {
    var errorName = document.getElementById("errorName");
    var inputFieldName = document.getElementById("fname");
    if (numbers !== null) {
        errorName.innerHTML = "Name contains number";
        inputFieldName.style.borderColor = "red";
    } else {
        inputFieldName.style.borderColor = "green";
        errorName.innerHTML = "";
    }
}
// lname
function onBlurNameLast() {
    var name = document.getElementById("lname").value;
    var inputFieldName = document.getElementById("lname");
    var errorName = document.getElementById("errorLname");
    var trimmedStr = name.trim();
    var numbers = name.match(/\d+/);
    var bool = true;
    console.log(inputFieldName.length)
    if (trimmedStr != name) {
        errorName.innerHTML = "Name contain space ";
        bool = false;
    } else {
        // inputFieldEmail.style.borderColor = "green";
        errorName.innerHTML = "";
    }
    if (name === '') {
        errorName.innerHTML = "Name should be filled ";
        inputFieldName.style.borderColor = "red";
    } else {
        if (bool == true) {
            checkNumberLast(numbers);
        }
        else {
            if (bool == false) {
                inputFieldName.style.borderColor = "red";
            } else {
                inputFieldName.style.borderColor = "green";
            }
        }
    }
    inputFieldName.style.borderWidth = "3px";
}
function checkNumberLast(numbers) {
    var errorName = document.getElementById("errorName");
    var inputFieldName = document.getElementById("lname");
    if (numbers !== null) {
        errorName.innerHTML = "Name contains number";
        inputFieldName.style.borderColor = "red";
    } else {
        inputFieldName.style.borderColor = "green";
        errorName.innerHTML = "";
    }
}
//address
function onKeyMessage(name) {
    var inputFieldName = document.getElementById("address");
    if (name.value.length < 10) {
        errorMessage.innerHTML = "minimum 10 letters";
        inputFieldName.style.borderColor = "red";
    } else {
        if (name.value.length > 50) {
            inputFieldName.style.borderColor = "red";
            errorMessage.innerHTML = "maximum 100 letters";
        } else {
            inputFieldName.style.borderColor = "green";
            errorMessage.innerHTML = "";
        }
    }
}
function onBlurMessage(name) {
    var inputFieldName = document.getElementById("address");
    if (name.value.length < 10) {
        errorMessage.innerHTML = "minimum 10 letters";
    } else {
        if (name.value.length > 50) {
            errorMessage.innerHTML = "maximum 100 letters";
            inputFieldName.style.borderColor = "red";
        } else {
            inputFieldName.style.borderColor = "green";
            errorMessage.innerHTML = "";
        }
    }
}
//phone
function validPhone(inputtxt) {
    var phoneno = /^\d{10}$/;
    var trim = inputtxt.value.trim();
    var inputFieldName = document.getElementById("phone");
    if ((inputtxt.value.match(phoneno))) {

        errorPhone.innerHTML = "";
        inputFieldName.style.borderColor = "green";
    }

    else {
        errorPhone.innerHTML = "must be valid number";
        inputFieldName.style.borderColor = "red";
    }
    inputFieldName.style.borderWidth = "3px";
}

function onfocusMail() {
    var inputFieldName = document.getElementById("name");
    inputFieldName.style.borderColor = "rgb(255, 213, 0)";
}
function validateEmail() {
    var fname = document.getElementById("fname").value;
    var lname = document.getElementById("lname").value;
    var dob = document.getElementById("DOB").value;
    var age = document.getElementById("age").value;
    var phone = document.getElementById("phone").value;
    var emailInput = document.getElementById("email").value;
    var address = document.getElementById("address").value;
    var state = document.getElementById("state").value;
    var username = document.getElementById("username").value;
    var pass = document.getElementById("password").value;
    var rePass = document.getElementById("repassword").value;
    var emailPattern = /^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$/;
    if (emailInput == "" || rePass == "" || pass == "" || username == "" || state == "" ||
        address == "" || passWord == "" || phone == "" || age == "" || dob == "" || lname == "" || fname == "") {
        alert("Fill All The Field");
    }
    if (emailPattern.test(emailInput) === false) {
        alert("In-Valid email address");
    }

}
function focusFunction() {
    var email = document.getElementById("email").value;
    var password = document.getElementById("password").value;
    if (email === "") {
        document.getElementById("email").style.background = "red";
    } else {
        document.getElementById("email").style.background = "";
    }
    if (password === "") {
        document.getElementById("password").style.background = "red";
    } else {
        document.getElementById("password").style.background = "";
    }
}
function onBlurPass() {
    document.getElementById("password").style.background = "";
}
function cityFunction() {
    var state = document.getElementById("state").value;
    var stringList;
    console.log(state);
    if (state === "Andaman and Nicobar Islands") {
        var stringList = ["--city select--", "Option 1", "Option 2", "Option 3", "Option 4"];
    }
    else if (state == "Tamil Nadu") {
        var stringList = ["--city select--", "Chennai", "Coimbatore", "Madurai", "Salem"]
    }
    else if (state == "Telangana") {
        var stringList = ["--city select--", "Hyderabad", "Warangal", "Karimnagar", "Nizamabad"]
    }
    else if (state == "Andhra Pradesh") {
        var stringList = ["--city select--", "Hyderabad", "Visakhapatnam", "Vijayawada"]
    }
    else {
        var stringList = [v, "Option 2", "Option 3", "Option 4"];
    }
    console.log(stringList);
    const city = document.getElementById("city");
    city.innerHTML = "";
    for (let i = 0; i < stringList.length; i++) {
        const option = document.createElement("option");
        option.text = stringList[i];
        city.add(option);
    }

}
//user
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
        inputFieldUsername.style.borderColor = "red";
    } else {
        // Clear any previous error message
        errorUserName.innerHTML = "";
        inputFieldUsername.style.borderColor = "green";
    }

    inputFieldUsername.style.borderWidth = "3px";
}

function checkValidUser(emailPattern, emailInput) {
    var inputFieldEmail = document.getElementById("username");
    if (!emailPattern.test(emailInput)) {
        errorUserName.innerHTML = "Enter valid Username";
        inputFieldEmail.style.borderColor = "red";
    } else {
        inputFieldEmail.style.borderColor = "green";
        errorUserName.innerHTML = "";
    }
}
// password
function isStrongPassword(password) {
    console.log(password);
    if (password.length < 8) {
        errorPassword.innerHTML = "Password should have at least 8 characters";
        return false;
    }

    if (!/[A-Z]/.test(password)) {
        errorPassword.innerHTML = "Password should have at least one uppercase letter";
        return false;
    }

    if (!/[a-z]/.test(password)) {
        errorPassword.innerHTML = "Password should have at least one lowercase letter";
        return false;
    }

    if (!/[0-9]/.test(password)) {
        errorPassword.innerHTML = "Password should have at least one digit";
        return false;
    }

    if (!/[!@#$%^&*()_+{}\[\]:;<>,.?~\\-]/.test(password)) {
        errorPassword.innerHTML = "Password should have at least one special character (e.g., !, @, #, $, etc.)";
        return false;
    } else {
        errorPassword.innerHTML = "";
    }

    return true;
}

function validatePassword() {
    var pass = document.getElementById("password");
    var passin = document.getElementById("password").value;
    var outString = isStrongPassword(passin);
    if (outString) {
        errorPassword.innerHTML = ""
        pass.style.borderColor = "green";

    } else {
        pass.style.borderColor = "red";
        console.log("Password is not strong.");
    }
    pass.style.borderWidth = "3px";
}
function checkRepass() {
    var passin = document.getElementById("password").value;
    var repassin = document.getElementById("repassword").value;
    var repassinhtml = document.getElementById("repassword");
    console.log(repassin);
    console.log(passin);
    if (!repassin.length < 8) {
        errorrePassword.innerHTML = "invalid password";
        repassinhtml.style.borderColor = "red";
    }
    if (passin === repassin) {
        errorrePassword.innerHTML = "";
        repassinhtml.style.borderColor = "green";
    }
    else {
        errorrePassword.innerHTML = "invalid password";
        repassinhtml.style.borderColor = "red";
    }

    repassinhtml.style.borderWidth = "3px";

}


// Example usage
//   var password = "MyPassword123!";
//   var isValid = validatePassword(password);
//   console.log(isValid); // Output: true
