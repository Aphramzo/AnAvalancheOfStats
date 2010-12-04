using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PlayoffPerformers : System.Web.UI.Page
{
    private CommonPage scripts;
    protected void Page_Load(object sender, EventArgs e)
    {
        scripts = new CommonPage();
        if (!IsPostBack)
            AppendDataToFilters();
        else
            AppendDataToGrid();
    }

    private void AppendDataToGrid()
    {
        String sqlString = String.Format("select {0} from vPlayoffPerformers {1} {2} {3}", GenerateSelectColumns(), GenerateWhereClause(), GenerateGroupByClause(), GenerateHavingClause());
        SqlCommand gridCommand = new SqlCommand(sqlString, scripts.GetConnection());

        dgPlayerSeasons.DataSource = gridCommand.ExecuteReader(CommandBehavior.CloseConnection);
        dgPlayerSeasons.DataBind();


    }

    private string GenerateSelectColumns()
    {
        return "0 as Rank, Description as Season, " +
               "'<a class=\"'+ IsCurrent + '\" href=\"../Player.aspx?id=' + convert(varchar, playerId) + '\">' + PlayerName + '</a>' as Player, " +
               "Position as POS, " +
               "Age, " +
               "GoalsPerGameDifference as [Goals per Game Difference], " +
               "GoalsPerGamePercent as [Goals per Game %], " +
               "AssistsPerGameDifference as [Assists per Game Difference], " +
               "AssistsPerGamePercent as [Assists per Game %], " +
               "PointsPerGameDifference as [Points per Game Difference], " +
               "PointsPerGamePercent as [Points per Game %], " +
               "PIMPerGameDifference as [PIM per Game Difference], " +
               "PIMPerGamePercent as [PIM per Game %], " +
               "ShotsPerGameDifference as [Shots per GameDifference], " +
               "ShotsPerGamePercent as [Shots per %]";
    }

    private string GenerateHavingClause()
    {
        if (!String.IsNullOrEmpty(Request["sum"]))
        {
            string GPLow = Request["GP-low"];
            string GPHigh = Request["GP-high"];
            List<String> havingClause = new List<string>();
            if (!String.IsNullOrEmpty(GPLow))
                havingClause.Add(String.Format(" Sum(GP) >= {0}", GPLow.Replace("'", "''")));
            if (!String.IsNullOrEmpty(GPHigh))
                havingClause.Add(String.Format(" Sum(GP) <= {0}", GPHigh.Replace("'", "''")));

            if (havingClause.Count > 0)
            {
                string havingStrClause = " having 1=1 ";
                foreach (String str in havingClause)
                {
                    havingStrClause += " and " + str;
                }
                return havingStrClause;
            }
            else
                return String.Empty;

        }
        else
            return String.Empty;
    }

    private string GenerateWhereClause()
    {
        string seasonLow = Request["ctl00$mainBody$seasonlow"];
        string seasonHigh = Request["ctl00$mainBody$seasonhigh"];
        string ageLow = Request["age-low"];
        string ageHigh = Request["age-high"];
        string position = Request["position"];
        List<String> whereClause = new List<string>();
        

        if (!String.IsNullOrEmpty(seasonLow))
            whereClause.Add(String.Format(" orderNumber >= {0} ", seasonLow));
        if (!String.IsNullOrEmpty(seasonHigh))
            whereClause.Add(String.Format(" orderNumber <= {0} ", seasonHigh));
        if (!String.IsNullOrEmpty(ageLow))
            whereClause.Add(String.Format(" age >= {0}", ageLow.Replace("'", "''")));
        if (!String.IsNullOrEmpty(ageHigh))
            whereClause.Add(String.Format(" age <= {0}", ageHigh.Replace("'", "''")));
        
        switch (position)
        {
            case "SKATERS":
                whereClause.Add(" positionId <> 1 ");
                break;
            case "FORWARD":
                whereClause.Add(" positionId in (3,4,5) ");
                break;
            case "1":
                whereClause.Add(" positionId = 1 ");
                break;
            case "2":
                whereClause.Add(" positionId = 2 ");
                break;
            case "3":
                whereClause.Add(" positionId = 3 ");
                break;
            case "4":
                whereClause.Add(" positionId = 4 ");
                break;
            case "5":
                whereClause.Add(" positionId = 5 ");
                break;
            default:
                break;
        }

        string whereStrClause = " where 1=1 ";
        foreach (String str in whereClause)
        {
            whereStrClause += " and " + str;
        }
        return whereStrClause;
    }

    private string GenerateGroupByClause()
    {
        if (!String.IsNullOrEmpty(Request["sum"]))
        {
            return " group by PlayerName, PlayerId, Position, PositionId, IsCurrent";
        }
        return string.Empty;
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
