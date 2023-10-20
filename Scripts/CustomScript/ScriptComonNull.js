function validateField(field) {
    var value = field.value.trim();
    var errorSpan = field.parentElement.querySelector(".text-danger");
    if (value === '') {
        errorSpan.innerText = 'This cannot be empty.';
    } else {
        errorSpan.innerText = '';
    }
}