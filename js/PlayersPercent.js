$(document).ready(function() {
    var mytable = $("#ctl00_mainBody_dgPlayerSeasonsPercent");
    //Add the thead that datagrid omits:
    mytable.prepend(document.createElement('thead'));
    //Move the first row from tbody to thead:
    $("#ctl00_mainBody_dgPlayerSeasonsPercent thead").append($("#ctl00_mainBody_dgPlayerSeasonsPercent tbody tr:eq(0)"));
    $('thead tr td').each(function(i) { $(this).replaceWith("<th>" + $(this).html() + "<\/th>"); });

    var sortColumn;
    sortColumn = 7;

    $('#ctl00_mainBody_dgPlayerSeasonsPercent').dataTable({
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
            { "sType": "numeric" }
        ]
    });


    jQuery.each($(".IsCurrentPlayer"), function() {
        //alert($(this));
   
        $(this).parent().get(0).bgColor='#DDDDDD';
    });
});