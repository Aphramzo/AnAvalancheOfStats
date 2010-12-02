using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class Goalies : System.Web.UI.Page
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
        String sqlString = String.Format("select {0} from vGoalieSeason {1} {2} {3}", GenerateSelectColumns(), GenerateWhereClause(), GenerateGroupByClause(), GenerateHavingClause());
        SqlCommand gridCommand = new SqlCommand(sqlString, scripts.GetConnection());

        dgGoalieSeasons.DataSource = gridCommand.ExecuteReader(CommandBehavior.CloseConnection);
        dgGoalieSeasons.DataBind();


    }

    private string GenerateSelectColumns()
    {
        if (!String.IsNullOrEmpty(Request["sum"]))
        {
            return
                "0 as Rank, '<a class=\"'+ IsCurrent + '\" href=\"../Player.aspx?id=' + convert(varchar, playerId) + '\">' + PlayerName + '</a>' as Player, " +
                "Sum(GP) as [Games Played], " +
                "Sum(Goals) as Goals, " +
                "Sum(Assists) as Assists, " +
                "Sum(Points) as Points, " +
                "Sum(PIM) as PIM, " +
                "Sum(Wins) as [W], " +
                "Sum(Losses) as [L], " +
                "Sum(Ties) as [T], " +
                "Sum(OTLoss) as [OT],  " +
                "Sum(ShotAgainst) as SA, " +
                "Sum(GoalsAgainst) as GA, " +
                "Sum(Saves) as Saves, " +
                "Round(Sum(Saves)/(Convert(float,Sum(ShotAgainst))),3) as [Sv%], " +
                "Round(Sum(GoalsAgainst)/(convert(float,Sum(TOI))/60),3) as [GAA], " +
                "Sum(TOI) as TOI";
        }
        return "Id as Rank, Description as Season, " +
               "'<a class=\"'+ IsCurrent + '\" href=\"../Player.aspx?id=' + convert(varchar, playerId) + '\">' + PlayerName + '</a>' as Player, " +
               "Age, " +
               "GP as [Games Played], " +
               "Goals, " +
               "Assists, " +
               "Points, " +
               "PIM, " +
               "Wins as [W], " +
               "Losses as [L], " +
               "Ties as [T], " +
               "OTLoss as [OT],  " +
               "ShotAgainst as [SA], " +
               "GoalsAgainst as [GA], " +
               "Saves, " +
               "SavePercentage as [Sv%], " +
               "GAA, " +
               "TOI ";
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
        string GPLow = Request["GP-low"];
        string GPHigh = Request["GP-high"];
        string seasonLow = Request["ctl00$mainBody$seasonlow"];
        string seasonHigh = Request["ctl00$mainBody$seasonhigh"];
        string country = Request["ctl00$mainBody$Country"];
        string ageLow = Request["age-low"];
        string ageHigh = Request["age-high"];

        List<String> whereClause = new List<string>();
        if (String.IsNullOrEmpty(Request["sum"]))
        {
            if (!String.IsNullOrEmpty(GPLow))
                whereClause.Add(String.Format(" GP >= {0}", GPLow.Replace("'", "''")));
            if (!String.IsNullOrEmpty(GPHigh))
                whereClause.Add(String.Format(" GP <= {0}", GPHigh.Replace("'", "''")));
        }

        if (!String.IsNullOrEmpty(seasonLow))
            whereClause.Add(String.Format(" orderNumber >= {0} ", seasonLow));
        if (!String.IsNullOrEmpty(seasonHigh))
            whereClause.Add(String.Format(" orderNumber <= {0} ", seasonHigh));
        if (!String.IsNullOrEmpty(ageLow))
            whereClause.Add(String.Format(" age >= {0}", ageLow.Replace("'", "''")));
        if (!String.IsNullOrEmpty(ageHigh))
            whereClause.Add(String.Format(" age <= {0}", ageHigh.Replace("'", "''")));
        if (!String.IsNullOrEmpty(country))
            whereClause.Add(String.Format(" Country = {0}", country));

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

        SqlCommand seasonsCommand3 = new SqlCommand("select Id, Description from Country order by Description", scripts.GetConnection());
        Country.DataSource = seasonsCommand3.ExecuteReader();
        Country.DataTextField = "Description";
        Country.DataValueField = "Id";
        Country.DataBind();
        Country.SelectedValue = Request["ctl00$mainBody$Country"];

    }

}
