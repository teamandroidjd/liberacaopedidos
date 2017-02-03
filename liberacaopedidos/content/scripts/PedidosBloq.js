var CarregarPedidosBloqueados = function () {    
    $.ajax({
        url: 'PedBlqService.aspx/CarregarPedidos',
        type: 'post',
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        success: function (response) {
            response = response.d;
            var html = '';
            if (response != null) {                
                html += '<tr style="background-color:#1976d2; color:#fff"><th>Número do Pedido</th></tr>';
                response.forEach(function (s) {
                    html += '<tr><td>' + s[0] + '</td><td>' + (s[1] != null && s[1] != '' ? s[1].toHHMMSS() : 'Aberto') +
                        '</td><td>' + s[2].toUpperCase() + '</td></tr>';
                });
            }            
            $('#tablePedidos').html(html);
            response = null;
        }
    });
};
$(function () { CarregarPedidosBloqueados(); });