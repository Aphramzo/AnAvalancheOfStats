using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Playoffs : System.Web.UI.Page
{
    private CommonPage scripts;
    protected void Page_Load(object sender, EventArgs e)
    {
        scripts = new CommonPage();
        if (!IsPostBack)
            AppendDataToFilters();
        AppendDataToGrid();
    }

    private void AppendDataToGrid()
    {
        String sqlString = String.Format("select {0} from vPlayoffs {1} {2}", GenerateSelectColumns(), GenerateWhereClause(), " order by description");
        SqlCommand gridCommand = new SqlCommand(sqlString, scripts.GetConnection());

        dgSeasons.DataSource = gridCommand.ExecuteReader(CommandBehavior.CloseConnection);
        dgSeasons.DataBind();


    }

    private string GenerateSelectColumns()
    {
        return "Description as Season, " +
                "GamesPlayed as [Games Played]," +
                "Wins, " +
                "Losses, " +
                "HomeWins as [Home Wins], " +
                "HomeLosses as [Home Losses], " +
                "AwayWins as [Away Wins], " +
                "AwayLosses as [Away Losses], " +
                "GoalsFor as [Goals Scored], " +
                "GoalsAgainst as [Goals Against], " +
                "GoalsToGoalsAgainst as [Goals to Goals Against Ratio]";
    }

    private string GenerateWhereClause()
    {
        string seasonLow = Request["ctl00$mainBody$seasonlow"];
        string seasonHigh = Request["ctl00$mainBody$seasonhigh"];
        List<String> whereClause = new List<string>();
        if (!String.IsNullOrEmpty(seasonLow))
            whereClause.Add(String.Format(" orderNumber >= {0} ", seasonLow));
        if (!String.IsNullOrEmpty(seasonHigh))
            whereClause.Add(String.Format(" orderNumber <= {0} ", seasonHigh));

        string whereStrClause = " where 1=1 ";
        foreach (String str in whereClause)
        {
            whereStrClause += " and " + str;
        }
        return whereStrClause;
    }

    private void AppendDataToFilters()
    {
        SqlCommand seasonsCommand = new SqlCommand("select orderNumber, Description from Season order by orderNumber", scripts.GetConnection());
        seasonlow.DataSource = seasonsCommand.ExecuteReader(CommandBehavior.CloseConnection);
        seasonlow.DataTextField = "Description";
        seasonlow.DataValueField = "orderNumber";
        seasonlow.DataBind();
        seasonlow.SelectedValue = Request["ctl00$mainBody$seasonlow"];

        SqlCommand seasonsCommand2 = new SqlCommand("select orderNumber, Description from Season order by orderNumber", scripts.GetConnection());
        seasonhigh.DataSource = seasonsCommand2.ExecuteReader();
        seasonhigh.DataTextField = "Description";
        seasonhigh.DataValueField = "orderNumber";
        seasonhigh.DataBind();
        seasonhigh.SelectedValue = Request["ctl00$mainBody$seasonhigh"];



    }
}
