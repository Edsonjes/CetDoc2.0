$(document).ready(function () {

    function limpa_formulario_cep() {
        // Limpa valores do formul�rio de cep.
        $("#rua").val("");
        $("#bairro").val("");
        $("#cidade").val("");
        $("#uf").val("");
        $("#ibge").val("");
    }

    //Quando o campo cep perde o foco.
    $("#cep").blur(function () {

        //Nova vari�vel "cep" somente com d�gitos.
        var cep = $(this).val().replace(/\D/g, '');

        //Verifica se campo cep possui valor informado.
        if (cep != "") {

            //Express�o regular para validar o CEP.
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
                        //CEP pesquisado n�o foi encontrado.
                        limpa_formulario_cep();
                        alert("CEP n�o encontrado.");
                    }
                });
            } //end if.
            else {
                //cep � inv�lido.
                limpa_formulario_cep();
                alert("Formato de CEP inv�lido.");
            }
        } //end if.
        else {
            //cep sem valor, limpa formul�rio.
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

                ['Idade', 'Sexo', 'Escolaridade', 'Raca','EstadoCivil'].forEach(function (nome) {
                    var opcoesSelecionadas = document.querySelectorAll('input[name="' + nome + '"]:checked');

                    opcoesSelecionadas.forEach(function (opcao) {
                        var idQuestaoSelecionada = parseInt(opcao.id); // Supondo que o id do input seja o idQuestao
                        var questaoEncontrada = data.find(function (item) {
                            return item.idQuestao === idQuestaoSelecionada;
                        });

                        if (questaoEncontrada) {
                            somarPonto += parseInt(questaoEncontrada.pontuacao); // Converter para n�mero antes de somar
                        }
                    });
                });
                // Exemplo de uso dos pontos somados (voc� pode ajustar conforme necess�rio)
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

        $.ajax({

            
        })
    }
});