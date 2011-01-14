﻿$(document).ready(function() {
    var mytable = $("#ctl00_mainBody_dgPlayerSeasons");
    //Add the thead that datagrid omits:
    mytable.prepend(document.createElement('thead'));
    //Move the first row from tbody to thead:
    $("#ctl00_mainBody_dgPlayerSeasons thead").append($("#ctl00_mainBody_dgPlayerSeasons tbody tr:eq(0)"));
    $('thead tr td').each(function(i) { $(this).replaceWith("<th>" + $(this).html() + "<\/th>"); });

    var sortColumn;
    if ($('#sum')[0].checked)
        sortColumn = 6;
    else
        sortColumn = 8;

    if ($('#sum')[0].checked && $('#perGame')[0].checked)
        $('#ctl00_mainBody_dgPlayerSeasons').dataTable({
            "fnDrawCallback": function(oSettings) {
                /* Need to redo the counters if filtered or sorted */
                for (var i = 0, iLen = oSettings.aiDisplay.length; i < iLen; i++) {
                    $('td:eq(0)', oSettings.aoData[oSettings.aiDisplay[i]].nTr).html(i + 1);
                }

            },
            "bPaginate": false,
            "bProcessing": true,
            "aaSorting": [[sortColumn, 'desc']],
            "aoColumns": [
                    null,
                    { "sType": "html" },
                    null,
                    { "sType": "numeric" },
                    { "sType": "numeric" },
                    { "sType": "numeric" },
                    { "sType": "numeric" },
                    { "sType": "numeric" },
                    { "sType": "numeric" },
                    { "sType": "numeric" },
                    { "sType": "numeric" },
                    { "sType": "numeric" }
                ]
        });

    if (!$('#sum')[0].checked && $('#perGame')[0].checked)
        $('#ctl00_mainBody_dgPlayerSeasons').dataTable({
            "fnDrawCallback": function(oSettings) {
                /* Need to redo the counters if filtered or sorted */
                for (var i = 0, iLen = oSettings.aiDisplay.length; i < iLen; i++) {
                    $('td:eq(0)', oSettings.aoData[oSettings.aiDisplay[i]].nTr).html(i + 1);
                }

            },
            "bPaginate": false,
            "bProcessing": true,
            "aaSorting": [[sortColumn, 'desc']],
            "aoColumns": [
                null,
                null,
                { "sType": "html" },
                null,
                { "sType": "numeric" },
                { "sType": "numeric" },
                { "sType": "numeric" },
                { "sType": "numeric" },
                { "sType": "numeric" },
                { "sType": "numeric" },
                { "sType": "numeric" },
                { "sType": "numeric" },
                { "sType": "numeric" },
                { "sType": "numeric" }
            ]
        });

    if ($('#sum')[0].checked && !$('#perGame')[0].checked)
        $('#ctl00_mainBody_dgPlayerSeasons').dataTable({
            "fnDrawCallback": function(oSettings) {
                /* Need to redo the counters if filtered or sorted */
                for (var i = 0, iLen = oSettings.aiDisplay.length; i < iLen; i++) {
                    $('td:eq(0)', oSettings.aoData[oSettings.aiDisplay[i]].nTr).html(i + 1);
                }

            },
            "bPaginate": false,
            "bProcessing": true,
            "aaSorting": [[sortColumn, 'desc']],
            "aoColumns": [
                null,
                { "sType": "html" },
                null,
                { "sType": "numeric" },
                { "sType": "numeric" },
                { "sType": "numeric" },
                { "sType": "numeric" },
                { "sType": "numeric" },
                { "sType": "numeric" },
                { "sType": "numeric" },
                { "sType": "numeric" },
                { "sType": "numeric" },
                { "sType": "numeric" }
            ]
        });

    if (!$('#sum')[0].checked && !$('#perGame')[0].checked)
        $('#ctl00_mainBody_dgPlayerSeasons').dataTable({
            "fnDrawCallback": function(oSettings) {
                /* Need to redo the counters if filtered or sorted */
                    for (var i = 0, iLen = oSettings.aiDisplay.length; i < iLen; i++) {
                        $('td:eq(0)', oSettings.aoData[oSettings.aiDisplay[i]].nTr).html(i + 1);
                    }

            },
            "bPaginate": false,
            "bProcessing": true,
            "aaSorting": [[sortColumn, 'desc']],
            "aoColumns": [
                null,
                null,
                { "sType": "html" },
                null,
                { "sType": "numeric" },
                { "sType": "numeric" },
                { "sType": "numeric" },
                { "sType": "numeric" },
                { "sType": "numeric" },
                { "sType": "numeric" },
                { "sType": "numeric" },
                { "sType": "numeric" },
                { "sType": "numeric" },
                { "sType": "numeric" },
                { "sType": "numeric" },
                null
            ]
        });


    jQuery.each($(".IsCurrentPlayer"), function() {
        //alert($(this));
   
        $(this).parent().get(0).bgColor='#DDDDDD';
    });
});