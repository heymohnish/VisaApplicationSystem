function onFocusName() {
    var inputFieldName = document.getElementById("name");
    inputFieldName.style.borderColor = "rgb(255, 213, 0)";
}
function onBlurName() {
    var name = document.getElementById("name").value;
    var inputFieldName = document.getElementById("name");
    var errorName = document.getElementById("errorName");
    var trimmedStr = name.trim();
    var numbers = name.match(/\d+/);
    var bool = true;
    console.log(inputFieldName.length)
    if (trimmedStr != name) {
        errorName.innerHTML = "Name contain space ";
        bool = false;
    } else {
        errorName.innerHTML = "";
    }
    if (name === '') {
        errorName.innerHTML = "Name should be filled ";
        inputFieldName.style.borderColor = "red";
    } else {
        if (bool == true) {
            checkNumber(numbers);
        }
        else {
            inputFieldName.style.borderColor = "green";
        }
    }
}
function checkNumber(numbers) {
    var errorName = document.getElementById("errorName");
    var inputFieldName = document.getElementById("name");
    if (numbers !== null) {
        errorName.innerHTML = "Name contains number";
        inputFieldName.style.borderColor = "red";
    } else {
        errorName.innerHTML = "";
    }
}
function onBlurEmail() {
    var emailInput = document.getElementById("email").value;
    var inputFieldEmail = document.getElementById("email");
    var trimmedStr = emailInput.trim();
    console.log(emailInput + " " + trimmedStr);
    var bool = true;
    var emailPattern = /^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$/;
    if (trimmedStr != emailInput) {
        errorMail.innerHTML = "email contain space ";
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
            inputFieldName.style.borderColor = "red";
        }
    }
}
function onfocusMail() {
    var inputFieldName = document.getElementById("name");
    inputFieldName.style.borderColor = "rgb(255, 213, 0)";
}
function checkValidMail(emailPattern, emailInput) {
    var inputFieldEmail = document.getElementById("email");
    if (!emailPattern.test(emailInput)) {
        errorMail.innerHTML = "Enter valid email";
        inputFieldEmail.style.borderColor = "red";
    } else {
        errorMail.innerHTML = "";
    }
}
function onfocusMail() {

}
function onKeyMessage(name) {
    // console.log(name.value);

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
function onBlurMessage(name) {
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
function focusFunction() {
    var name = document.getElementById("name").value;
    var email = document.getElementById("email").value
    var message = document.getElementById("message").value;
    if (name === "") {
        document.getElementById("name").style.background = "red";
    } else {
        document.getElementById("name").style.background = "";
    }
    if (email === "") {
        document.getElementById("email").style.background = "red";
    } else {
        document.getElementById("email").style.background = "";
    }
    if (message === "") {
        document.getElementById("message").style.background = "red";
    } else {
        document.getElementById("message").style.background = "";
    }

}




