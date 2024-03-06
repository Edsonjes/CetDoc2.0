$(document).ready(function () {
    var totalpontos = 0;
    function limpa_formulario_cep() {
        // Limpa valores do formulário de cep.
        $("#rua").val("");
        $("#bairro").val("");
        $("#cidade").val("");
        $("#uf").val("");
        $("#ibge").val("");
    }

    //Quando o campo cep perde o foco.
    $("#cep").blur(function () {

        //Nova variável "cep" somente com dígitos.
        var cep = $(this).val().replace(/\D/g, '');

        //Verifica se campo cep possui valor informado.
        if (cep != "") {

            //Expressão regular para validar o CEP.
            var validacep = /^[0-9]{8}$/;

            //Valida o formato do CEP.
            if (validacep.test(cep)) {

                //Preenche os campos com "..." enquanto consulta webservice.
                $("#rua").val("...");
                $("#bairro").val("...");
                $("#cidade").val("...");
                $("#uf").val("...");
                $("#ibge").val("...");

                //Consulta o webservice viacep.com.br/
                $.getJSON("https://viacep.com.br/ws/" + cep + "/json/?callback=?", function (dados) {

                    if (!("erro" in dados)) {
                        //Atualiza os campos com os valores da consulta.
                        $("#rua").val(dados.logradouro);
                        $("#bairro").val(dados.bairro);
                        $("#cidade").val(dados.localidade);
                        $("#uf").val(dados.uf);
                        $("#ibge").val(dados.ibge);
                    } //end if.
                    else {
                        //CEP pesquisado não foi encontrado.
                        limpa_formulario_cep();
                        alert("CEP não encontrado.");
                    }
                });
            } //end if.
            else {
                //cep é inválido.
                limpa_formulario_cep();
                alert("Formato de CEP inválido.");
            }
        } //end if.
        else {
            //cep sem valor, limpa formulário.
            limpa_formulario_cep();
        }
    });

    function somaPontos() {
        $.ajax({
            url: 'http://localhost:5159/Home/ListaQuestoes',
            type: 'GET',
            dataType: 'json',
            success: function (data) {
                var somarPonto = 0;
               
                ['Idade', 'Sexo', 'Escolaridade', 'Raca', 'EstadoCivil', 'Moradia', 'Filhos', 'Trabalho', 'DeficienciaVisual', 'DeficienciaAuditiva', 'opcoesDeTrabalho', 'DeficienciaAuditiva', 'DeficienciaFisica', 'DeficienciaIntelectual', 'Autimos',
                        'passeLivre'].forEach(function (nome) {
                    var opcoesSelecionadas = document.querySelectorAll('input[name="' + nome + '"]:checked');

                    opcoesSelecionadas.forEach(function (opcao) {
                        var idQuestaoSelecionada = parseInt(opcao.id); // Supondo que o id do input seja o idQuestao
                        var questaoEncontrada = data.find(function (item) {
                            return item.idQuestao === idQuestaoSelecionada;
                        });

                        if (questaoEncontrada) {
                            somarPonto += parseInt(questaoEncontrada.pontuacao); // Converter para número antes de somar
                            totalpontos = somarPonto;
                        }
                    });
                });
                // Exemplo de uso dos pontos somados (você pode ajustar conforme necessário)
                console.log('Soma dos Pontos: ' + somarPonto);
            },
            error: function (error) {
                console.log(error);
            }
        });
    }

    // Adiciona eventos de clique aos elementos de checkbox e radio
    document.querySelectorAll('input[type="checkbox"], input[type="radio"]').forEach(function (input) {
        input.addEventListener('click', somaPontos);
    });

    function SalvarForm() {
        console.log("Clicou");

        // Serializa os dados do formulário em um objeto
        var formData = $("#formCadPessoal").serializeArray().reduce(function (obj, item) {
            // Verifica se o input é do tipo "text"
            if (item.type === "text") {
                obj[item.name] = {
                    value: item.value,
                };
            }
            return obj;
        }, {});

        // Agora, vamos lidar com os inputs do tipo "radio" e "checkbox"
        var radioCheckboxData = {};

        $("input[type='radio'], input[type='checkbox']").each(function () {
            var opcao = this;
            var opcaoName = opcao.name;
            var idQuestaoSelecionada = parseInt(opcao.id); // Supondo que o id do input seja o idQuestao

            if (!radioCheckboxData[opcaoName]) {
                radioCheckboxData[opcaoName] = [];
            }

            if (opcao.checked) {
                radioCheckboxData[opcaoName].push({
                    value: opcao.value,
                    id: idQuestaoSelecionada
                });
            }
        });

        // Mesclar os objetos formData e radioCheckboxData
        Object.assign(formData, radioCheckboxData);

        // Adiciona a pontuação ao objeto
        formData.pontuacao = totalpontos;

        console.log(formData);

        $.ajax({
            url: 'http://localhost:5159/Home/SalvarFormulario',
            type: 'POST',
            contentType: 'application/json', // Define o tipo de conteúdo como JSON
            data: JSON.stringify(formData), // Converte o objeto em uma string JSON
            success: function (response) {
                console.log("Sucesso", response);
            },
            error: function (error) {
                console.log("Erro", error);
            }
        });
    }
    document.getElementById('btnSalvar').addEventListener('click', SalvarForm);
});