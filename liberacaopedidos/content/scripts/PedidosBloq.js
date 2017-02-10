
//metodo para o menu com botoes 
var openMenuButtons = function () {
    var pos = '+';
    if ($('.list-buttons').hasClass('list-buttons-openned')) {
        $('.list-buttons').removeClass('list-buttons-openned');
        pos = '-';
    }
    else
        $('.list-buttons').addClass('list-buttons-openned');
    for (var i = 0; i <= $('.list-buttons li').length; i++)
        $('.list-buttons li:eq(' + i + ')').animate({ 'bottom': pos + '=' + (i + 1) + '00px' }, 'fast');
};

$('.button-open').on('click', openMenuButtons);

var fillModal = function (type, fillItems) {
    var
        headers = listHeaders($('#selectFiltro').val())
        , html = ''
    ;
    if (headers != null && headers.length > 0) {
        html += '<dl>';
        for (var i = 1; i < headers.length; i++) {
            var
                field = ''
                , fieldClass = ''
            ;

            //a data de criação não deve ser mutável
            if (headers[i] != null && headers[i] != '' && headers[i].toUpperCase() != 'CRIAÇÃO') {
                switch (headers[i].toUpperCase()) {
                    case 'COD.':
                    
                }
                console.log(fieldClass);
                field = '<textarea class="full-width"></textarea>';               
                html += '<dt>' + headers[i] + '</dt><dd>' + field + '</dd>';
            }
        }
        html += '</dl>';
        $('#myModalBody').html(html);

        $('.tb-numero').mask('00000000000000');
        $('.tb-tempo').mask('00:00');
        $('.tb-data').mask('00/00/0000');

        if (fillItems === true)
            fillFieldsModal();

        $('#myModal').modal('show');
    }
};

var listHeaders = function (type) {
    var headers = ['Cod.'];
    
    if (type != 6)
        headers.push('Motivo');
    return headers;
};

$('#btnNao').on('click', function () {
    fillModal($('#selectFiltro').val(), false);
    $('#btnSalvar').off().on('click', addItem);
});

var listValuesModal = function () {
    var arr = [];
    $('#myModalBody').find('input, textarea').each(function () {
        if ($(this).attr('type') == 'checkbox')
            arr.push($(this).is(':checked') ? 'S' : 'N');
        else
            arr.push($(this).val());
    });
    return arr;
}


var addItem = function () {
    var
        Pedido = document.getElementById('lblNumPedido')        
        , motivo = listValuesModal()
        , Usuario = 1
    ;
    $.ajax({
        url: 'DadosPedidos.aspx/NaoLiberarPedido',
        type: 'post',
        contentType: 'application/json;charset=utf-8',
        dataType: 'json', 
        data: JSON.stringify({ 'NumPedido': Pedido, 'Motivo': motivo, 'codUsuario': Usuario }),
        success: function (response) {
            response = response.d;
            if (response != null && response > 0) {
                alert('Cadastrado com sucesso');
                $('#myModal').modal('hide');
                $('#buttonSearch').click();
            }
            else {
                alert('Não foi possível realizar o cadastro');
            }
        }
    }).fail(function (error) { console.log(error) });
};


