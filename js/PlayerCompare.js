$(document).ready(function() {
    $('#showChart').click(function(event) {
        event.preventDefault();
        $('#compareChart').insertFusionCharts({
            swfPath: "../AnAvalancheOfStats/charts/",
            type: "MSLine2D",
            data: "PlayerCompareChart.aspx?Player1Id=" + $('#ctl00_mainBody_player1').val() + "&Player2Id=" + $('#ctl00_mainBody_player2').val() + "&stat=" + $('#stat').val(),
            dataFormat: "URIData",
            width: "700",
            height: "400"
        });
        $('.AccordionNavigation').height($(document).height());
    });
});