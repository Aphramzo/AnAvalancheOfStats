$(document).ready(function() {
    var mytable = $("#ctl00_mainBody_dgSummary");
    //Add the thead that datagrid omits:
    mytable.prepend(document.createElement('thead'));
    //Move the first row from tbody to thead:
    $("#ctl00_mainBody_dgSummary thead").append($("#ctl00_mainBody_dgSummary tbody tr:eq(0)"));
    $('thead tr td').each(function(i) { $(this).replaceWith("<th>" + $(this).html() + "<\/th>"); });

    $('#ctl00_mainBody_dgSummary').dataTable({
        "bPaginate": false,
        "bFilter": false,
        "aoColumns": [
                    { "bVisible": false},
                   null,
                   null,
                   null,
                   null,
                   null,
                   null,
                   null,
                   null,
                   null,
                   null,
                   null
                ]
    });

    mytable = $("#ctl00_mainBody_dgDivision");
    //Add the thead that datagrid omits:
    mytable.prepend(document.createElement('thead'));
    //Move the first row from tbody to thead:
    $("#ctl00_mainBody_dgDivision thead").append($("#ctl00_mainBody_dgDivision tbody tr:eq(0)"));
    $('thead tr td').each(function(i) { $(this).replaceWith("<th>" + $(this).html() + "<\/th>"); });

    $('#ctl00_mainBody_dgDivision').dataTable({
    "bPaginate": false,
    "bFilter": false,
    "aoColumns": [
                    { "bVisible": false },
                   null,
                   null,
                   null,
                   null,
                   null,
                   null,
                   null,
                   null,
                   null,
                   null,
                   null
                ]
    });

    mytable = $("#ctl00_mainBody_dgMonth");
    //Add the thead that datagrid omits:
    mytable.prepend(document.createElement('thead'));
    //Move the first row from tbody to thead:
    $("#ctl00_mainBody_dgMonth thead").append($("#ctl00_mainBody_dgMonth tbody tr:eq(0)"));
    $('thead tr td').each(function(i) { $(this).replaceWith("<th>" + $(this).html() + "<\/th>"); });

    $('#ctl00_mainBody_dgMonth').dataTable({
    "bPaginate": false,
    "bFilter": false,
    "aoColumns": [
                    { "bVisible": false },
                   null,
                   null,
                   null,
                   null,
                   null,
                   null,
                   null,
                   null,
                   null,
                   null,
                   null
                ]
    });

    mytable = $("#ctl00_mainBody_dgDay");
    //Add the thead that datagrid omits:
    mytable.prepend(document.createElement('thead'));
    //Move the first row from tbody to thead:
    $("#ctl00_mainBody_dgDay thead").append($("#ctl00_mainBody_dgDay tbody tr:eq(0)"));
    $('thead tr td').each(function(i) { $(this).replaceWith("<th>" + $(this).html() + "<\/th>"); });

    $('#ctl00_mainBody_dgDay').dataTable({
    "bPaginate": false,
    "bFilter": false,
    "aoColumns": [
                    { "bVisible": false },
                   null,
                   null,
                   null,
                   null,
                   null,
                   null,
                   null,
                   null,
                   null,
                   null,
                   null
                ]
    });

    mytable = $("#ctl00_mainBody_dgTeam");
    //Add the thead that datagrid omits:
    mytable.prepend(document.createElement('thead'));
    //Move the first row from tbody to thead:
    $("#ctl00_mainBody_dgTeam thead").append($("#ctl00_mainBody_dgTeam tbody tr:eq(0)"));
    $('thead tr td').each(function(i) { $(this).replaceWith("<th>" + $(this).html() + "<\/th>"); });

    $('#ctl00_mainBody_dgTeam').dataTable({
    "bPaginate": false,
    "bFilter": false,
    "aoColumns": [
                    { "bVisible": false },
                   null,
                   null,
                   null,
                   null,
                   null,
                   null,
                   null,
                   null,
                   null,
                   null,
                   null
                ]
    });
});