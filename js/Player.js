$(document).ready(function() {
    var mytable = $("#ctl00_mainBody_dgSeasons");
    //Add the thead that datagrid omits:
    mytable.prepend(document.createElement('thead'));
    //Move the first row from tbody to thead:
    $("#ctl00_mainBody_dgSeasons thead").append($("#ctl00_mainBody_dgSeasons tbody tr:eq(0)"));
    $('thead tr td').each(function(i) { $(this).replaceWith("<th>" + $(this).html() + "<\/th>"); });

    $('#ctl00_mainBody_dgSeasons').dataTable({
        "bPaginate": false,
        "bFilter": false
    });

    mytable = $("#ctl00_mainBody_dgPlayoffs");
    //Add the thead that datagrid omits:
    mytable.prepend(document.createElement('thead'));
    //Move the first row from tbody to thead:
    $("#ctl00_mainBody_dgPlayoffs thead").append($("#ctl00_mainBody_dgPlayoffs tbody tr:eq(0)"));
    $('thead tr td').each(function(i) { $(this).replaceWith("<th>" + $(this).html() + "<\/th>"); });

    $('#ctl00_mainBody_dgPlayoffs').dataTable({
        "bPaginate": false,
        "bFilter": false
    });

    $('#showSeasonChart').click(function(event) {
        event.preventDefault();
        $('#seasonChart').insertFusionCharts({
            swfPath: "../AnAvalancheOfStats/charts/",
            type: "MSLine2D",
            data: "PlayerChart.aspx?Id=" + $('#playerId').val(),
            dataFormat: "URIData",
            width: "700",
            height: "400"
        });
        $('.AccordionNavigation').height($(document).height());
    });

    $('#showPlayoffChart').click(function(event) {
        event.preventDefault();
        $('#playoffChart').insertFusionCharts({
            swfPath: "../AnAvalancheOfStats/charts/",
            type: "MSLine2D",
            data: "PlayerChart.aspx?Playoffs=1&Id=" + $('#playerId').val(),
            dataFormat: "URIData",
            width: "700",
            height: "400"
        });
        $('.AccordionNavigation').height($(document).height());
    });
});