$(document).ready(function() {
    $("input:submit, button").button();
    $('#glbPlayerSearch').autocomplete({
        source: 'PlayerSearch.aspx',
        select: function(event, ui) {
        $("#glbPlayerSearch").val(ui.item.label);
            document.location.href = "Player.aspx?id=" + ui.item.value;
        },
        focus: function(event, ui) {
            $("#glbPlayerSearch").val(ui.item.label);
        },
        change: function(event, ui) {
            $("#glbPlayerSearch").val(ui.item.label);
        }
    });
});