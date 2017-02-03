var
    user = document.getElementById('lblUsuario'),
    psw = document.getElementById('lblSenha')
;
function login() {
    $.ajax({
        url: 'login.aspx/Logar',
        type: 'post',
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        data: JSON.stringify({ 'user': $(user).val(), 'psw': $(psw).val() }),
        success: function (response) {
            response = $.parseJSON(response.d);
            if (response > 0) {
                sessionStorage.setItem('id', response);
                sessionStorage.removeItem('name');
                sessionStorage.setItem('name', $('#lblUsuario').val().toUpperCase());
                location.replace('login.aspx');
            }
        }
    }).fail(function (error) { console.log(error); });
}
$('#buttonLogon').on('click', login);

$('.section-container').on('keypress', function (e) {
    var keycode = (window.Event) ? e.which : e.keyCode;
    var str = $(this).val();
    if (keycode == 13)
        login();
});

$('#textLoginUsername').focus();