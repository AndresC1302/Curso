$(document).on('submit', '#Registrar', function (e) {
    e.preventDefault();
    $.ajax({
        beforeSend: function () {
            $('#Registrar button[type=submit]').prop('disabled', true);
        },
        type: this.method,
        url: this.action,
        data: $(this).serialize(),
        success: function (data) {
            alert('Usuario registrado con éxito.');
        },
        error: function (xhr, status) {
            alert(xhr.responseJSON.Message);
        },
        complete: function () {
            $('#Registrar button[type=submit]').prop('disabled', false);
        }
    });
});

$(document).on('submit', '#Login', function (e) {
    e.preventDefault();
    $.ajax({
        beforeSend: function () {
            $('#Login button[type=submit]').prop('disabled', true);
        },
        type: this.method,
        url: this.action,
        data: $(this).serialize(),
        success: function (data) {
    
            window.location = "/Home";
        },
        error: function (xhr, status) {
            alert(xhr.responseJSON.Message);
            
        },
        complete: function () {
            $('#Login button[type=submit]').prop('disabled', false);
        }
    });
});
