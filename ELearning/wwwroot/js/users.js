$(document).ready(function () {
    $('#user-type-select').change(function () {
        if (this.value == "1")
            $('#register-group-select').hide();
        else
            $('#register-group-select').show();
    });
});