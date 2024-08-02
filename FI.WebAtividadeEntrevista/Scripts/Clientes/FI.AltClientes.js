$(document).ready(function () {
    if (obj) {
        $('#formCadastro #Nome').val(obj.Nome);
        $('#formCadastro #CEP').val(obj.CEP);
        $('#formCadastro #Email').val(obj.Email);
        $('#formCadastro #Sobrenome').val(obj.Sobrenome);
        $('#formCadastro #Nacionalidade').val(obj.Nacionalidade);
        $('#formCadastro #Estado').val(obj.Estado);
        $('#formCadastro #Cidade').val(obj.Cidade);
        $('#formCadastro #Logradouro').val(obj.Logradouro);
        $('#formCadastro #Telefone').val(obj.Telefone);
        $('#formCadastro #CPF').val(obj.CPF);
        $('#beneficiariosForm #BeneficiarioNome').val(obj.beneficiarioNome);
        $('#beneficiariosForm #BeneficiarioCPF').val(obj.BeneficiarioCPF);
        $('#beneficiariosForm #BeneficiarioId').val(obj.BeneficiarioId);
    }


    $('#modalBeneficiario').on('shown.bs.modal', function () {
        carregarBeneficiarios(obj.Id);
    });

    $('#formCadastro').submit(function (e) {
        e.preventDefault();
        
        $.ajax({
            url: urlPost,
            method: "POST",
            data: {
                "NOME": $(this).find("#Nome").val(),
                "CEP": $(this).find("#CEP").val(),
                "Email": $(this).find("#Email").val(),
                "Sobrenome": $(this).find("#Sobrenome").val(),
                "Nacionalidade": $(this).find("#Nacionalidade").val(),
                "Estado": $(this).find("#Estado").val(),
                "Cidade": $(this).find("#Cidade").val(),
                "Logradouro": $(this).find("#Logradouro").val(),
                "Telefone": $(this).find("#Telefone").val(),
                "CPF": removerMascaraCpf($(this).find("#CPF").val())
            },
            error:
            function (r) {
                if (r.status == 400)
                    ModalDialog("Ocorreu um erro", r.responseJSON);
                else if (r.status == 500)
                    ModalDialog("Ocorreu um erro", "Ocorreu um erro interno no servidor.");
            },
            success:
            function (r) {
                ModalDialog("Sucesso!", r)
                $("#formCadastro")[0].reset();                                
            }
        });
    })
})


function carregarBeneficiarios(clienteId) {
    $.ajax({
        url: urlBuscarBeneficiarioPorCliente,
        type: 'GET',
        data: { idCliente: clienteId },
        success: function (response) {
            if (response.success) {
                var beneficiarios = response.data;
                adicionarBeneficiariosNaTabela(beneficiarios);
            } else {
                console.error('Erro: ' + response.message);
            }
        },
        error: function (xhr) {
            alert('Erro ao carregar beneficiários: ' + xhr.responseText);
        }
    });
}


var beneficiarioIdAtual = null;

function salvarBeneficiario() {
    beneficiarioIdAtual ? alteraBeneficiario() : adicionarBeneficiario();
}


function alteraBeneficiario() {
    var cpf = $("#BeneficiarioCPF").val();
    var nome = $("#BeneficiarioNome").val();
    var id = beneficiarioIdAtual;


    $.ajax({
        url: urlAtualizarBeneficiario,
        type: 'POST',
        data: {
            Id: id,
            BeneficiarioNome: nome,
            BeneficiarioCPF: removerMascaraCpf(cpf)
        },
        success: function (response) {
            beneficiarioIdAtual = null;
            limparFormBeneficiario();

            if (response.success) {
                atualizarTabelaBeneficiarios(id, nome, cpf);
                console.log(response.message);
            } else {
                console.error('Erro: ' + response.message);
            }
        },
        error: function (xhr) {
            beneficiarioIdAtual = null;
            alert('Erro ao salvar beneficiário: ' + xhr.responseText);
        }
    });
}


function limparFormBeneficiario() {
    $('#BeneficiarioCPF').val('');
    $('#BeneficiarioNome').val('');
}

function adicionarBeneficiario() {
    var clienteCPF = $("#CPF").val();
    var beneficiarioNome = $("#BeneficiarioNome").val();
    var beneficiarioCPF = $("#BeneficiarioCPF").val();

    $.ajax({
        url: urlPostBeneficiario,
        method: "POST",
        data: {
            ClienteCPF: removerMascaraCpf(clienteCPF),
            BeneficiarioNome: beneficiarioNome,
            BeneficiarioCPF: removerMascaraCpf(beneficiarioCPF)
        },
        success: function (response) {
            limparFormBeneficiario();
            if (response.success) {
                adicionarBeneficiariosNaTabela([response.beneficiario]);
            } else {
                console.error(response.message);
            }
        },
        error:
            function (r) {
                limparFormBeneficiario();
                $("#modalBeneficiario").modal('hide');

                if (r.status == 400)
                    ModalDialog("Ocorreu um erro", r.responseJSON.message);
                else if (r.status == 500)
                    ModalDialog("Ocorreu um erro", "Ocorreu um erro interno no servidor.");
            },
    });
}

function adicionarBeneficiariosNaTabela(beneficiarios) {
    var tableBody = $("#beneficiariosTableBody");

    beneficiarios.forEach(function (beneficiario) {

        var existente = tableBody.find(`tr[data-id='${beneficiario.Id}']`).length > 0;

        var cpfComMascara = aplicarMascaraCpf(beneficiario.CPF);

        if (!existente) {
            tableBody.append(
                `<tr data-id="${beneficiario.Id}">
                    <td>${beneficiario.Nome}</td>
                    <td>${cpfComMascara}</td>
                    <td>
                        <button class="btn btn-primary btn-sm" type="button"  onclick="editarBeneficiario(${beneficiario.Id})">Alterar</button>
                        <button class="btn btn-primary btn-sm" type="button"  onclick="excluirBeneficiario(${beneficiario.Id})">Excluir</button>
                    </td>
                </tr>`
            );
        }
    });
}


function atualizarTabelaBeneficiarios(beneficiarioId, beneficiarioName, beneficiarioCpf) {
    var row = $(`#beneficiariosTableBody tr[data-id='${beneficiarioId}']`);

    if (row.length) {
        row.find('td').eq(0).text(beneficiarioName);
        row.find('td').eq(1).text(beneficiarioCpf);
    }
}

function editarBeneficiario(id) {
    beneficiarioIdAtual = id;
    var row = $(`#beneficiariosTableBody tr[data-id='${id}']`);

    if (row.length) {
        var beneficiarioName = row.find('td').eq(0).text();
        var beneficiarioCpf = row.find('td').eq(1).text();
        $("#BeneficiarioNome").val(beneficiarioName)
        $("#BeneficiarioCPF").val(beneficiarioCpf);
    }
    $("#btnSalvar").text("Salvar");
}

function excluirBeneficiario(id) {
    if (confirm('Tem certeza que deseja excluir este beneficiário?')) {
        $.ajax({
            url: urlDeleteBeneficiario,
            type: 'POST',
            data: { id: id },
            success: function (response) {
                if (response.success) {
                    console.log(response.message);
                    $(`tr[data-id='${id}']`).remove();
                    
                } else {
                    console.log('Erro ao excluir beneficiário: ' + response.message);
                }
            },
            error: function (xhr) {
                console.log('Erro ao excluir beneficiário: ' + xhr.responseText);
            }
        });
    }
}

function ModalDialog(titulo, texto) {
    var random = Math.random().toString().replace('.', '');
    var texto = '<div id="' + random + '" class="modal fade">                                                               ' +
        '        <div class="modal-dialog">                                                                                 ' +
        '            <div class="modal-content">                                                                            ' +
        '                <div class="modal-header">                                                                         ' +
        '                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>         ' +
        '                    <h4 class="modal-title">' + titulo + '</h4>                                                    ' +
        '                </div>                                                                                             ' +
        '                <div class="modal-body">                                                                           ' +
        '                    <p>' + texto + '</p>                                                                           ' +
        '                </div>                                                                                             ' +
        '                <div class="modal-footer">                                                                         ' +
        '                    <button type="button" class="btn btn-default" data-dismiss="modal">Fechar</button>             ' +
        '                                                                                                                   ' +
        '                </div>                                                                                             ' +
        '            </div><!-- /.modal-content -->                                                                         ' +
        '  </div><!-- /.modal-dialog -->                                                                                    ' +
        '</div> <!-- /.modal -->                                                                                        ';

    $('body').append(texto);
    $('#' + random).modal('show');
}

function removerMascaraCpf(cpf) {
    return cpf.replace(/\D/g, '');
}

function aplicarMascaraCpf(cpf) {
    cpf = cpf.replace(/\D/g, '');

    if (cpf.length <= 11) {
        cpf = cpf.replace(/(\d{3})(\d{3})?(\d{3})?(\d{2})?/, function (match, p1, p2, p3, p4) {
            let result = p1;
            if (p2) result += '.' + p2;
            if (p3) result += '.' + p3;
            if (p4) result += '-' + p4;
            return result;
        });
    }

    return cpf;
}

