var
    arrayResources = ['prioridade', 'setor', 'produto', 'tipoatendimento', 'tipoproblema', 'status'],
    arraySysUsers = arraySysUsers == null || arraySysUsers == undefined || arraySysUsers == [] ? [] : arraySysUsers,
    arrayPrioridades = arrayPrioridades == null || arrayPrioridades == undefined || arrayPrioridades == [] ? [] : arrayPrioridades,
    arraySetores = arraySetores == null || arraySetores == undefined || arraySetores == [] ? [] : arraySetores,
    arrayProdutos = arrayProdutos == null || arrayProdutos == undefined || arrayProdutos == [] ? [] : arrayProdutos,
    arrayEventos = arrayEventos == null || arrayEventos == undefined || arrayEventos == [] ? [] : arrayEventos,
    arrayProblemas = arrayProblemas == null || arrayProblemas == undefined || arrayProblemas == [] ? [] : arrayProblemas,
    arrayStatus = arrayStatus == null || arrayStatus == undefined || arrayStatus == [] ? [] : arrayStatus,
    arrayTipoAtend = arrayTipoAtend == null || arrayTipoAtend == undefined || arrayTipoAtend == [] ? [] : arrayTipoAtend
;

String.prototype.toHHMMSS = function () {
    var sec_num = parseInt(this, 10); // don't forget the second param
    var hours = Math.floor(sec_num / 3600);
    var minutes = Math.floor((sec_num - (hours * 3600)) / 60);
    var seconds = sec_num - (hours * 3600) - (minutes * 60);

    if (hours < 10) { hours = "0" + hours; }
    if (minutes < 10) { minutes = "0" + minutes; }
    if (seconds < 10) { seconds = "0" + seconds; }
    var time = hours + ':' + minutes + ':' + seconds;
    return time;
}

$('[data-toggle="tooltip"]').tooltip();

//retorna o array de parametros da url
var urlDecoder = function () {
    var params = window.location.href.split('?')[1];
    return params;
};

var toggleMainMenu = function (button) {
    var
        body = $('body'),
        menu = $('.aside-menu'),
        pos = menu[0] == null ? 0 : menu[0].getClientRects()[0].left,
        difPos = '320px'
    ;
    difPos = pos < 0 ? '0' : '-' + difPos;
    $(button).attr('disabled', true);
    $(menu).animate({ "left": difPos }, 'fast', function () { $(button).removeAttr('disabled'); });
};

//limpa os campos de texto e os campos com a classe cleanable  de determinado container
var clearContainer = function (container) {
    $(container).find('input[type="text"], textarea, .cleanable').val('');
};

//inverte a visibilidade de dois containers, ou seja, caso queira que um apareca no lugar do outro
var toggleChangeContainers = function (container1, container2) {
    $(container1).fadeOut('slow', function () {
        $(this).addClass('hidden');
        $(container2).fadeIn('slow', function () { $(this).removeClass('hidden') });
    });
}

var fillOffices = function (field) {
    $.ajax({
        url: 'controllerfull.aspx/listItemsDrop',
        type: 'POST',
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify({ "type": 'CARGO' }),
        success: function (response) {
            response = response.d;
            var html = '';
            response.forEach(function (x) { html += '<option value="' + x['id'] + '">' + x['descricao'] + '</option>'; });
            $(field).html(html);
            response = null;
        }
    }).fail(function (error) { console.log(error) });
}

var formatDate = function (data) {
    if (data.indexOf('/') > 0) {
        data = data.split('/');
        data = data[2] + '-' + data[1] + '-' + data[0];
    }
    else if (data.indexOf('-') > 0) {
        data = data.split('-');
        data = data[2] + '/' + data[1] + '/' + data[0];
    }
    return data;
};

var fillArrayResource = function (cont) {
    var type = arrayResources[cont];
    $.ajax({
        url: 'controllerfull.aspx/listItemsDrop',
        type: 'POST',
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify({ "type": type }),
        success: function (response) {
            response = response.d;
            response.forEach(function (x) {
                switch (type) {
                    case 'prioridade':
                        arrayPrioridades.push(x);
                        break;
                    case 'produto':
                        arrayProdutos.push(x);
                        break;
                    case 'tipoatendimento':
                        arrayTipoAtend.push(x);
                        break;
                    case 'tipoproblema':
                        arrayProblemas.push(x);
                        break;
                    case 'setor':
                        arraySetores.push(x);
                        break;
                    case 'status':
                        arrayStatus.push(x);
                        break;
                    default:
                        stop();
                        break;
                }
            });
            response = null;
        }
    }).fail(function (error) { console.log(error) })
      .done(function () { if ((cont + 1) < arrayResources.length) fillArrayResource(cont + 1); else fillArrayEvents(); });
};

var fillArrayEvents = function () {
    $.ajax({
        url: 'controllerfull.aspx/listEventsDrop',
        type: 'POST',
        dataType: 'json',
        contentType: 'application/json; charset=utf-8',
        success: function (response) {
            response = response.d;
            response.forEach(function (x) {
                arrayEventos.push(x);
            });
            response = null;
        }
    }).fail(function (error) { console.log(error) })
      .done(function () { retrieveSysUsers() });
};

//retornar todos os usuarios do sistema com seus codigos (serve para preenchimento de comboboxes)
var retrieveSysUsers = function () {
    $.ajax({
        url: 'controllerfull.aspx/listUsers',
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        success: function (response) {
            response = response.d;
            response.forEach(function (user) {
                arraySysUsers.push(user);
            });
            response = null;
        }
    }).fail(function (error) { console.log(error) })
      .done(function () { fillDropsResources() });
};

//preenche os dropdowns de determinadas classes relacionando arrayResources com os arrays ja existentes
var fillDropsResources = function () {
    var arrays = [
          arrayPrioridades
        , arraySetores
        , arrayProdutos
        , arrayTipoAtend
        , arrayProblemas
        , arrayStatus
        , arrayEventos
        , arraySysUsers
    ];

    //acrescenta evento para os itens do array
    arrayResources.push('evento');
    arrayResources.push('users');

    for (var x in arrayResources) {
        var html = '';
        //if (arrays[x] != null && arrays[x].length > 0) {
        for(var y of arrays[x])
        {
            html += '<option value="' + y['id'] + '"';
            if (y['habproblem'] != null && y['habproblem'] == 'S')
                html += ' class="habprob"';
            html += '>';
            if (y['name'] != null && y['name'] != undefined)//em caso de usuario
                html += y['name'];
            else
                html += y['descricao'];
            html += '</option>';
        }
        var
            classIdentifier = '.select-' + arrayResources[x]
            , articleOfClassIdent = $(classIdentifier).parents('article');

        if ($(articleOfClassIdent).hasClass('container-consulta')) $(classIdentifier).html('<option value="0">TODOS</option>' + html);
        else $(classIdentifier).html(html);

        $('#selectUserAssent, .container-dml').find('option[value=0]').remove();
    }
    //remover ultimo item da lista
    arrayResources.pop()
    arrayResources.pop();

    var urlParams = urlDecoder();
    if (urlParams != null && urlParams.length > 0) {
        urlParams = urlParams.split('&');

        if (urlParams[0].indexOf('exist') >= 0) {
            var codUsu = sessionStorage.getItem('id');
            $('#selectUsuarios').val(codUsu);
            $('#selectSituacao').val('1');
            $('input:radio[name="typeUser"]:checked').removeAttr('checked');
            $('input:radio[name="typeUser"]').each(function () {
                if ($(this).val() == 2)
                    $(this).attr('checked', true)
            });
            $('#buttonConsultar').click();
        }
    }

    //para a base do conhecimento
    $('#selectEditProduto').find('option[value=0]').remove();
    $('#selectAddAssentDireto').val(sessionStorage.getItem('id'));
}

//preenche o select(campo) com a  id e nome de contatos a partir do codigo do cliente informado
var retrieveContacts = function (codClient, campo) {
    $(campo).html('');
    $.ajax({
        url: 'controllerfull.aspx/listContacts',
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        data: JSON.stringify({ "codClient": codClient }),
        success: function (response) {
            response = response.d;
            var html = '';
            if (response != null)
                response.forEach(function (item) {
                    html += '<option value="' + item[0] + '">' + item[1] + '</option>';
                });
            $(campo).html(html);
            response = null;
        }
    }).fail(function (error) { console.log(error) });
}

//adiciona a classe de linha selecionada para uma tabela que pode conter esse evento
var tableSelectable = function (table) {
    $(table + ' tr:not(.tr-oculta) td').on('click', function () {
        $(this).parents('table').find('.tr-selecionada').removeClass('tr-selecionada');
        $(this).parents('tr').addClass('tr-selecionada');
    });
};

var tableAccordion = function (table) {
    var lines = $(table).find('tr:not(.tr-oculta)');
    $(lines).on('click', function () {
        var ref = $(this).attr('ref');
        $(table).find('.tr-oculta[ref-hidden=' + ref + ']')
            .toggleClass('hidden')//mostrar linha oculta referente a clicada
            .toggleClass('tr-hidden-clicada').toggleClass('tr-selecionada');
    });
};

var toggleAllAccordion = function (button, table) {
    if ($(button).val().indexOf('Abrir') >= 0) {
        $(button).val('Fechar Todos');
        $(table).find('.tr-oculta').removeClass('hidden');
    }
    else {
        $(button).val('Abrir Todos');
        $(table).find('.tr-oculta').addClass('hidden');
    }
};

//verifica se os campos que estao no array foram preenchidos
var verifFields = function (arrayFields) {
    $('.field-obrigatorio').removeClass('field-obrigatorio');
    for (var i = 0; i < arrayFields.length; i++) {
        if ($(arrayFields[i]).val() == '') {
            $(arrayFields[i]).addClass('field-obrigatorio');
            alert('Por favor preencha os campos obrigatórios');
            return false;
        }
    }
    return true;
};

//verifica se existem ocorrencias em aberto para o usuario logado
var verifOccursOpen = function () {
    if (location.pathname.toUpperCase().indexOf('login') >= 0)
        $('#footerMain').remove();
    else
        $.ajax({
            url: 'controllerfull.aspx/existOccurrencesToUser',
            type: 'POST',
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            success: function (response) {
                var css = 'expand';//variavel referente a animacao
                response = response.d;
                if (response > 0) {
                    $('#footerMain').removeClass('hidden');
                    //modifica o css de animacao para destacar que existem assentamentos hj
                    $('#buttonAlertOccursOpen').removeClass('button-alert-haveSettlements');
                    if (response == 2)
                        $('#buttonAlertOccursOpen').addClass('button-alert-haveSettlements');
                }
                else
                    $('#footerMain').remove();
                response = null;
            }
        }).fail(function (error) { console.log(error) });
};

$('.tb-numero').mask('00000000000000');
$('.tb-cep').mask('00000000');
$('.tb-tempo').mask('00:00');
$('.tb-data').mask('00/00/0000');
$('.tb-tel').mask('(00)0000-0000');

//metodo para ao clicar em um botao do main-menu o usuario ser redirecionado para outra tela
$('.aside-menu input').on('click', function () {
    var url = $(this).attr('ref');
    if (url.indexOf('login') >= 0)
        $('#footerMain').remove();
    else {
        $('#footerMain').addClass('hidden');
        verifOccursOpen();
    }
    if (url.indexOf('base') >= 0)
        window.open(url, '_blank');
    else
        location.replace(url);
});

$('#buttonMainMenu, .button-close-menu').off().on('click', function () { toggleMainMenu(this); });

$('body').children().not('.aside-menu').on('click', function () { if ($('.aside-menu')[0].getClientRects() != null && $('.aside-menu')[0].getClientRects()[0] != null && $('.aside-menu')[0].getClientRects()[0].left >= 0) toggleMainMenu(this); });

$('.button-toggle-accordion').on('click', function () { toggleAllAccordion($(this), '#' + $(this).attr('ref')) });

function autoTamTextArea() {
    var textLine = Math.round($(this).val().length / 150) + $(this).val().split('\n').length;
    $(this).css({ 'height': textLine * 2 + 'em' });
}

$('textarea').on('keyup', autoTamTextArea).on('focus', autoTamTextArea);


$('#buttonNotifications').on('click', function () {
    $('#containerNotificacoes').toggleClass('hidden');
});

/* --- images --- */

//metodo para os botoes abrirem as imagens em nova aba
var openArchieveNewTab = function (button, knowledgeB) {
    var cod = $(button).attr('ref');
    $.ajax({
        url: 'controllerfull.aspx/retrieveAnArchive',
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        data: JSON.stringify({ 'cod': cod, 'knowledgeB': knowledgeB }),
        success: function (response) {
            response = response.d;
            if (response != null)
                window.open(response['stringB64'], '_blank');
        }
    }).fail(function (error) { console.log(error) });
};

//cod = cod da imagem
var delImage = function (cod) {
    $.ajax({
        url: 'controllerfull.aspx/delImage',
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        data: JSON.stringify({ 'cod': cod }),
        success: function (response) {
            response = response.d;
            if (response != null && response.length > 0) {

            }
            response = null;
        }
    }).fail(function (error) { console.log(error); });
}

//cod = codigo da ocorrencia; container = container a ser inserido o conteudo
var listImages = function (cod, container) {
    $(container).html('');
    $.ajax({
        url: 'controllerfull.aspx/listImages',
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        data: JSON.stringify({ 'cod': cod }),
        success: function (response) {
            response = response.d;
            if (response != null && response.length > 0) {
                var html = '';
                response.forEach(function (file) {
                    html += '<li><input type="button" class="btn btn-link white" value="' + file['name'] + '" ref="' + file['cod'] + '" '
                        + ' ref-settlement="' + file['codSettlement'] + '" /></li>';
                });
                $(container).html('').html(html);
                $(container).find('.btn-link').on('click', function () {
                    openArchieveNewTab($(this), false);
                });
            }
            response = null;
        }
    }).fail(function (error) { console.log(error); })
      .done(function () { if (container == '#listImagesSavedSettlements') relImagesWithSettlement('#listImagesSavedSettlements', '#tableAssentamentos'); });
}

//salvar imagem no banco
//cod = codigo da ocorrencia; name = nome do arquivo; bin = array binario; arrayFiles = lista de arquivos; pos = posicao na lista
var saveImage = function (cod, name, bin, arrayFiles, pos, codSettlement) {
    $.ajax({
        url: 'controllerfull.aspx/uploadImage'
        , type: 'POST'
        , datatType: 'json'
        , contentType: 'application/json'
        , data: JSON.stringify({
            'cod': cod
            , 'bin': bin
            , 'name': name
            , 'codSettlement': codSettlement
        })
        , success: function (response) {
            response = response.d;
            if (response <= 0)
                alert('Não foi possível adicionar as imagens.');
            response = null;
        }
    }).fail(function (error) { console.log(error); })
      .done(function () {
          if ((pos++) <= arrayFiles.length)
              uploadFile(cod, arrayFiles, pos++, codSettlement);
          else {
              alert('Salvo com sucesso.');
              if (codSettlement == null || codSettlement <= 0 || codSettlement == '') {
                  $('#textareaDescricao').val('');
                  findSettlements($('.tr-selecionada').attr('ref'));
              }
              //location.replace("atendimentos.aspx?protocol=" + cod);
          }
      });
};

//files = imagens; quantidade de imagens
var uploadFile = function (cod, files, pos, codSettlement) {
    $('.button-execute').button('loading');
    if (pos <= files.length) {
        var file = files[pos];
        var fr = new FileReader();
        try {
            fr.onload = function () {
                var b64Data = fr.result.split(',');
                var contentType = file.type;
                var byteCharacters = atob(b64Data[1]);
                var bin = Array.prototype.map.call(byteCharacters, charCodeFromCharacter);
                if (pos > files.length)
                    event.stopPropagation;
                //salvar imagem no banco                         
                saveImage(cod, file.name, bin, files, pos, codSettlement);
            };
            fr.onloadend = function () { fr = null; }
            fr.onprogress = function () { }
            fr.readAsDataURL(file);
        } catch (error) {
            if (pos++ <= files.length)
                uploadFile(cod, files, pos++);
        }
    }
    else {
        fr = null;
        alert('Salvo com sucesso');
        location.replace('atendimentos.aspx?protocol=' + cod);
    }

};

var charCodeFromCharacter = function (c) { return c.charCodeAt(0); }

//retorna um array de imagens 
var verifImages = function (container) {
    var
          array = []
        , inputs = $(container).find('li input:file')
    ;
    for (var i = 0; i < inputs.length; i++) {
        if (inputs[i].value == '') $(inputs[i]).parents('li').remove();
        if (inputs[i].files[0] != null && inputs[i].files[0] != undefined)
            array.push(inputs[i].files[0]);
    }
    return array;
}

$('.btn-anexos-add').on('click', function () {
    var
        lista = $(this).parents('article:not(#containerEdit):not(#containerAssent)').find("ul")[0]
        , htmlAddAnexo = "<li><p><input type='button' class='button button-blue btn-anexos-show' value='Abrir'><input type='button' class='button button-red btn-anexos-remove' value='Remover'><input class='file' type='file'></p></li>"
    ;
    $(lista).append(htmlAddAnexo);
    $(".btn-anexos-remove").on("click", function () {
        var
          parentLI = $(this).parents('li'),
          parentUL = $(parentLI).parent()
        ;
        $(parentUL).find($(parentLI)).remove();
    });
    $(".btn-anexos-show").on("click", function () {
        var
            parentLI = $(this).parents('li')
            , image = $(parentLI).find("input:file")[0].files[0]
            , url = window.URL.createObjectURL(image)
        ;
        window.open(url, "_blank");
    });
});

$('.button-execute, .button-retornar').on('click', function () { window.scrollTo(0, 0) });

/*------------*/

(function () {
    if (window.navigator.userAgent.toUpperCase().indexOf("CHROME") < 0)
        $('.tb-date').mask('00/00/0000');
    fillArrayResource(0);
    verifOccursOpen();
})();

$(function () {
    if (sessionStorage.getItem('id') != null && parseInt(sessionStorage.getItem('id')) > 0
            && sessionStorage.getItem('name') != null) {
        $('#spanUsuarioMenu').text(sessionStorage.getItem('name'));
        switch (sessionStorage.getItem('name')) {
            case 'FLAVIO':
                $('#spanUsuarioMenu').css('color', 'rgb(241, 108, 224)');
                $('#smallTituloUsu').text('(Rihanna)');
                break;
            case 'JORGE':
                $('#smallTituloUsu').text('(Supremo Sr. Stalker)');
                break;
            case 'FELIPE':
                $('#smallTituloUsu').text('(Supremo Sr. KAYO)');
                break;
            case 'MATHEUS':
                $('#smallTituloUsu').text('(FRUIT NINJA)');
                break;
            case 'JUNIOR':
                $('#smallTituloUsu').text('(SUPREMO MESTRE OOMPA LOOMPA)');
                break;
            case 'GUILHERME':
                $('#smallTituloUsu').text('(??????)');
                break;
            default:
        }
    }
})