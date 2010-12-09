using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

public partial class Seasons : System.Web.UI.Page
{
    private CommonPage scripts;
    protected void Page_Load(object sender, EventArgs e)
    {
        scripts = new CommonPage();
        if(!IsPostBack)
            AppendDataToFilters();
        AppendDataToGrid();
    }

    private void AppendDataToGrid()
    {
        String sqlString = String.Format("select {0} from vSeasons {1} {2}",GenerateSelectColumns(),GenerateWhereClause(), " order by description");
        SqlCommand gridCommand = new SqlCommand(sqlString, scripts.GetConnection());
        
        dgSeasons.DataSource = gridCommand.ExecuteReader(CommandBehavior.CloseConnection);
        dgSeasons.DataBind();

      
    }

    private string GenerateSelectColumns()
    {
        if(!String.IsNullOrEmpty(Request["project"]) && String.IsNullOrEmpty(Request["show"]))
            return "Description as Season, " + 
                "ProjectedPoints as [Projected Points], " + 
                "ProjectedGoalsFor as [Projected Goals Scored], " + 
                "ProjectedGoalsAgainst as [Projected Goals Against], " +
                "GoalsToGoalsAgainst as [G:GA], " + 
                "GoalsPerPoint as [Goals Scored per Point]";

        if (String.IsNullOrEmpty(Request["project"]) && Request["show"] == "home")
            return "Description as Season, " +
                   "HomePoints as [Home Points], " +
                   "HomeWins as [Home Wins], " +
                   "HomeLosses as [Home Losses], " +
                   "HomeTies as [Home Ties], " +
                   "HomeOT as [Home OT], " +
                   "HomePPPercent as [Home PP%], " +
                   "HomePenaltyKill as [Home PK%]";

        if (String.IsNullOrEmpty(Request["project"]) && Request["show"] == "away")
            return "Description as Season, " +
                   "AwayPoints as [Away Points], " +
                   "AwayWins as [Away Wins], " +
                   "AwayLosses as [Away Losses], " +
                   "AwayTies as [Away Ties], " +
                   "AwayOT as [Away OT], " +
                   "AwayPPPercent as [Away PP%], " +
                   "AwayPenaltyKill as [Away PK%]";

        if (!String.IsNullOrEmpty(Request["project"]) && Request["show"] == "home")
            return "Description as Season, " +
                   "ProjectedHomePoints as [Projected Home Points]";

        if (!String.IsNullOrEmpty(Request["project"]) && Request["show"] == "away")
            return "Description as Season, " +
                   "ProjectedAwayPoints as [Projected Away Points]";

        return "Description as Season, " +
                "Points, " +
                "Wins, " +
                "Losses, " +
                "Ties, " +
                "OT, " +
                "GoalsFor as [G], " +
                "GoalsAgainst as [GA], " +
                "GoalsToGoalsAgainst as [G:GA], " +
                "PPPercent as [PP%], " +
                "PenaltyKill as [PK%], " +
                "GoalsPerPoint as [Goals Scored per Point], " +
                "AverageAge as [Avg. Player Age]";
    }

    private string GenerateWhereClause()
    {
        string pointsLow = Request["points-low"];
        string pointsHigh = Request["points-high"];
        string seasonLow = Request["ctl00$mainBody$seasonlow"];
        string seasonHigh = Request["ctl00$mainBody$seasonhigh"];
        List<String> whereClause = new List<string>();
        if (!String.IsNullOrEmpty(pointsLow))
            whereClause.Add(String.Format(" points >= {0}", pointsLow.Replace("'","''")));
        if (!String.IsNullOrEmpty(pointsHigh))
            whereClause.Add(String.Format(" points <= {0}",pointsHigh.Replace("'", "''")));
        if (!String.IsNullOrEmpty(seasonLow))
            whereClause.Add(String.Format(" orderNumber >= {0} ", seasonLow));
        if (!String.IsNullOrEmpty(seasonHigh))
            whereClause.Add(String.Format(" orderNumber <= {0} ", seasonHigh));

        string whereStrClause = " where 1=1 ";
        foreach(String str in whereClause)
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
