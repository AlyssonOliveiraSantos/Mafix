﻿

$('.close-alert').click(function () {
    $('.alert').hide();
});

$(document).ready(function () {
    $('.select2').select2({
        placeholder: "Selecione uma opção",
        allowClear: true,
        width: '100%'
    });
});



$(document).ready(function () {
    getDataTable('#table-maquinas');
    getDataTable('#table-operadores');
    getDataTable('#table-produtos'); 
    getDataTable('#table-usuarios');
    getDataTable('#table-paradaMaquina');

});

function getDataTable(id) {

    $(id).DataTable({
        "ordering": true,
        "paging": true,
        "searching": true,
        "oLanguage": {
            "sEmptyTable": "Nenhum registro encontrado na tabela",
            "sInfo": "Mostrar _START_ até _END_ de _TOTAL_ registros",
            "sInfoEmpty": "Mostrar 0 até 0 de 0 Registros",
            "sInfoFiltered": "(Filtrar de _MAX_ total registros)",
            "sInfoPostFix": "",
            "sInfoThousands": ".",
            "sLengthMenu": "Mostrar _MENU_ registros por pagina",
            "sLoadingRecords": "Carregando...",
            "sProcessing": "Processando...",
            "sZeroRecords": "Nenhum registro encontrado",
            "sSearch": "Pesquisar",
            "oPaginate": {
                "sNext": "Proximo",
                "sPrevious": "Anterior",
                "sFirst": "Primeiro",
                "sLast": "Ultimo"
            },
            "oAria": {
                "sSortAscending": ": Ordenar colunas de forma ascendente",
                "sSortDescending": ": Ordenar colunas de forma descendente"
            }
        }
    });

}