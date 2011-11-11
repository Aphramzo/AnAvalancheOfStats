$(document).ready(function() {
    var mytable = $("#ctl00_mainBody_dgShooting");
    //Add the thead that datagrid omits:
    mytable.prepend(document.createElement('thead'));
    //Move the first row from tbody to thead:
    $("#ctl00_mainBody_dgShooting thead").append($("#ctl00_mainBody_dgShooting tbody tr:eq(0)"));
    $('thead tr td').each(function(i) { $(this).replaceWith("<th>" + $(this).html() + "<\/th>"); });

    var sortColumn;
    if ($('#sum')[0].checked)
        sortColumn = 9;
    else
        sortColumn = 11;

    if ($('#sum')[0].checked)
        $('#ctl00_mainBody_dgShooting').dataTable({
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
    else
        $('#ctl00_mainBody_dgShooting').dataTable({
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


    jQuery.each($(".IsCurrentPlayer"), function() {
        //alert($(this));

        $(this).parent().get(0).bgColor = '#DDDDDD';
    });
});