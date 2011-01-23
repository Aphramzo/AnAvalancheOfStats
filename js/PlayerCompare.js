$(document).ready(function() {
    $('#showChart').click(function(event) {
        event.preventDefault();
        if ($('#ctl00_mainBody_player1').val() == '' || $('#ctl00_mainBody_player1').val() == '') {
            alert('You must select two players to compare.');
            return;
        }

        $('#compareChart').insertFusionCharts({
            swfPath: "../AnAvalancheOfStats/charts/",
            type: "MSLine2D",
            data: "PlayerCompareChart.aspx?Player1Id=" + $('#ctl00_mainBody_player1').val() + "&Player2Id=" + $('#ctl00_mainBody_player2').val() + "&stat=" + $('#ctl00_mainBody_stat').val() + '&showTrends=' + $('#showTrends').is(':checked'),
            dataFormat: "URIData",
            width: "700",
            height: "400"
        });
        $('.AccordionNavigation').height($(document).height());
    });

    if ($('#ctl00_mainBody_player1').val() != '' || $('#ctl00_mainBody_player1').val() != '') {
        $('#compareChart').insertFusionCharts({
            swfPath: "../AnAvalancheOfStats/charts/",
            type: "MSLine2D",
            data: "PlayerCompareChart.aspx?Player1Id=" + $('#ctl00_mainBody_player1').val() + "&Player2Id=" + $('#ctl00_mainBody_player2').val() + "&stat=" + $('#ctl00_mainBody_stat').val() + '&showTrends=' + $('#showTrends').is(':checked'),
            dataFormat: "URIData",
            width: "700",
            height: "400"
        });
        $('.AccordionNavigation').height($(document).height());
    }

    $('#createLink').click(function(event) {
        event.preventDefault();
        var link = 'http:/www.AnAvalancheOfStats.com/PlayerCompare.aspx?player1Id=' + $('#ctl00_mainBody_player1').val() + '&player2Id=' + $('#ctl00_mainBody_player2').val() + '&stat=' + $('#ctl00_mainBody_stat').val() + '&showTrends=' + $('#showTrends').is(':checked');
        $('#linkSpan').text(link);
    });
});