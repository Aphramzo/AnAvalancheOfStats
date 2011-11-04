$(document).ready(function() {

    $("#tabs").tabs();
    $("#TabsTheSecond").tabs();

    var mytable = $("#ctl00_mainBody_dgSeasons");
    //Add the thead that datagrid omits:
    mytable.prepend(document.createElement('thead'));
    //Move the first row from tbody to thead:
    $("#ctl00_mainBody_dgSeasons thead").append($("#ctl00_mainBody_dgSeasons tbody tr:eq(0)"));
    $('thead tr td').each(function(i) { $(this).replaceWith("<th>" + $(this).html() + "<\/th>"); });

    $('#ctl00_mainBody_dgSeasons').dataTable({
        "bPaginate": false,
        "bFilter": false,
        "bInfo": false
    });

    mytable = $("#ctl00_mainBody_dgPlayoffs");
    //Add the thead that datagrid omits:
    mytable.prepend(document.createElement('thead'));
    //Move the first row from tbody to thead:
    $("#ctl00_mainBody_dgPlayoffs thead").append($("#ctl00_mainBody_dgPlayoffs tbody tr:eq(0)"));
    $('thead tr td').each(function(i) { $(this).replaceWith("<th>" + $(this).html() + "<\/th>"); });

    $('#ctl00_mainBody_dgPlayoffs').dataTable({
        "bPaginate": false,
        "bFilter": false,
        "bInfo": false
    });

    mytable = $("#ctl00_mainBody_dgSeasonTotals");
    //Add the thead that datagrid omits:
    mytable.prepend(document.createElement('thead'));
    //Move the first row from tbody to thead:
    $("#ctl00_mainBody_dgSeasonTotals thead").append($("#ctl00_mainBody_dgSeasonTotals tbody tr:eq(0)"));
    $('thead tr td').each(function(i) { $(this).replaceWith("<th>" + $(this).html() + "<\/th>"); });

    $('#ctl00_mainBody_dgSeasonTotals').dataTable({
        "bPaginate": false,
        "bFilter": false,
        "bInfo": false,
        "bSort": false
    });

    mytable = $("#ctl00_mainBody_dgSplitsMonth");
    //Add the thead that datagrid omits:
    mytable.prepend(document.createElement('thead'));
    //Move the first row from tbody to thead:
    $("#ctl00_mainBody_dgSplitsMonth thead").append($("#ctl00_mainBody_dgSplitsMonth tbody tr:eq(0)"));
    $('thead tr td').each(function(i) { $(this).replaceWith("<th>" + $(this).html() + "<\/th>"); });

    $('#ctl00_mainBody_dgSplitsMonth').dataTable({
        "bPaginate": false,
        "bInfo": false
    });

    mytable = $("#ctl00_mainBody_dgSplitsOpponent");
    //Add the thead that datagrid omits:
    mytable.prepend(document.createElement('thead'));
    //Move the first row from tbody to thead:
    $("#ctl00_mainBody_dgSplitsOpponent thead").append($("#ctl00_mainBody_dgSplitsOpponent tbody tr:eq(0)"));
    $('thead tr td').each(function(i) { $(this).replaceWith("<th>" + $(this).html() + "<\/th>"); });

    $('#ctl00_mainBody_dgSplitsOpponent').dataTable({
        "bPaginate": false,
        "bInfo": false
    });

    mytable = $("#ctl00_mainBody_dgSplitsHomeAway");
    //Add the thead that datagrid omits:
    mytable.prepend(document.createElement('thead'));
    //Move the first row from tbody to thead:
    $("#ctl00_mainBody_dgSplitsHomeAway thead").append($("#ctl00_mainBody_dgSplitsHomeAway tbody tr:eq(0)"));
    $('thead tr td').each(function(i) { $(this).replaceWith("<th>" + $(this).html() + "<\/th>"); });

    $('#ctl00_mainBody_dgSplitsHomeAway').dataTable({
        "bPaginate": false,
        "bInfo": false,
        "bFilter": false
    });


    $('#aPostSeason').click(function(event) {
        event.preventDefault();
        $('#playoffChart').insertFusionCharts({
            swfPath: "../charts/",
            type: "MSLine2D",
            data: "PlayerChart.aspx?Playoffs=1&Id=" + $('#playerId').val(),
            dataFormat: "URIData",
            width: "700",
            height: "400"
        });
        $('.AccordionNavigation').height($(document).height());
    });

    $('#seasonChart').insertFusionCharts({
        swfPath: "../charts/",
        type: "MSLine2D",
        data: "PlayerChart.aspx?Id=" + $('#playerId').val(),
        dataFormat: "URIData",
        width: "700",
        height: "400"
    });
    $('.AccordionNavigation').height($(document).height());
});