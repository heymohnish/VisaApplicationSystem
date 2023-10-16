$(document).ready(function () {
    var stateDropdown = $("#countryDropdown");
    $.ajax({
        url: "@Url.Action("GetCountry", "Form")", type: "GET",
        dataType: "json",
        success: function (data) {
            // Populate the dropdown with the fetched states
            $.each(data, function (index, item) {
                stateDropdown.append($('<option>', {
                    value: item,
                    text: item
                }));
            });
        }
    });
});
$(document).ready(function () {
    var stateDropdown = $("#stateDropdown");
    var countryDropdown = $("#countryDropdown");
    countryDropdown.change(function () {
        var selectedCountry = countryDropdown.val();
        $.ajax({
            url: "@Url.Action("GetStates", "Form")",
            type: "GET",
            dataType: "json",
            data: { country: selectedCountry },
            success: function (data) {
                stateDropdown.empty();
                stateDropdown.append($('<option>', {
                    value: "",
                    text: "Select State"
                }));
                $.each(data, function (index, item) {
                    stateDropdown.append($('<option>', {
                        value: item,
                        text: item
                    }));
                });
            }
        });
    });
});
$(document).ready(function () {
    var cityDropdown = $("#cityDropdown");
    var stateDropdown = $("#stateDropdown");
    stateDropdown.change(function () {
        var selectedState = stateDropdown.val();
        $.ajax({
            url: "@Url.Action("GetCities", "Form")",
            type: "GET",
            dataType: "json",
            data: { state: selectedState },
            success: function (data) {
                // Clear existing options in the city dropdown
                cityDropdown.empty();
                cityDropdown.append($('<option>', {
                    value: "",
                    text: "Select City"
                }));
                // Populate the dropdown with the fetched cities
                $.each(data, function (index, item) {
                    cityDropdown.append($('<option>', {
                        value: item,
                        text: item
                    }));
                });
            }
        });
    });
});