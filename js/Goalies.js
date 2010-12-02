$(document).ready(function() {
    var mytable = $("#ctl00_mainBody_dgGoalieSeasons");
    //Add the thead that datagrid omits:
    mytable.prepend(document.createElement('thead'));
    //Move the first row from tbody to thead:
    $("#ctl00_mainBody_dgGoalieSeasons thead").append($("#ctl00_mainBody_dgGoalieSeasons tbody tr:eq(0)"));
    $('thead tr td').each(function(i) { $(this).replaceWith("<th>" + $(this).html() + "<\/th>"); });

    if (!$('#sum')[0].checked)
        $('#ctl00_mainBody_dgGoalieSeasons').dataTable({
            "fnDrawCallback": function(oSettings) {
                /* Need to redo the counters if filtered or sorted */
                for (var i = 0, iLen = oSettings.aiDisplay.length; i < iLen; i++) {
                    $('td:eq(0)', oSettings.aoData[oSettings.aiDisplay[i]].nTr).html(i + 1);
                }

            },
            "bPaginate": false,
            "bProcessing": true,
            "aaSorting": [[16, 'desc']],
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
                    { "sType": "numeric" },
                    { "sType": "numeric" },
                    { "sType": "numeric" },
                    { "sType": "numeric" }
                ]
        });
    if ($('#sum')[0].checked)
            $('#ctl00_mainBody_dgGoalieSeasons').dataTable({
                "fnDrawCallback": function(oSettings) {
                    /* Need to redo the counters if filtered or sorted */
                    for (var i = 0, iLen = oSettings.aiDisplay.length; i < iLen; i++) {
                        $('td:eq(0)', oSettings.aoData[oSettings.aiDisplay[i]].nTr).html(i + 1);
                    }

                },
                "bPaginate": false,
                "bProcessing": true,
                "aaSorting": [[14, 'desc']],
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