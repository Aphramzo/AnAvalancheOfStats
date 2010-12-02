$(document).ready(function(){
     var mytable = $("#ctl00_mainBody_dgSeasons");
     //Add the thead that datagrid omits:
     mytable.prepend(document.createElement('thead'));
     //Move the first row from tbody to thead:
     $("#ctl00_mainBody_dgSeasons thead").append($("#ctl00_mainBody_dgSeasons tbody tr:eq(0)"));
      $('thead tr td').each(function(i) { $(this).replaceWith("<th>" + $(this).html() + "<\/th>"); });

    $('#ctl00_mainBody_dgSeasons').dataTable({
        "bPaginate": false,
        "bFilter":false
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
});