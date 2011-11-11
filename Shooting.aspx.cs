using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

public partial class Shooting : System.Web.UI.Page
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
        String sqlString = String.Format("select {0} from vPlayerSeasonShooting {1} {2} {3}", GenerateSelectColumns(), GenerateWhereClause(), GenerateGroupByClause(), GenerateHavingClause());
        SqlCommand gridCommand = new SqlCommand(sqlString, scripts.GetConnection());

        dgShooting.DataSource = gridCommand.ExecuteReader(CommandBehavior.CloseConnection);
        dgShooting.DataBind();


    }

    private string GenerateSelectColumns()
    {
        if (!String.IsNullOrEmpty(Request["sum"]))
        {
            return
                "0 as Rank, '<a class=\"'+ IsCurrent + '\" href=\"../Player.aspx?id=' + convert(varchar, playerId) + '\">' + PlayerName + '</a>' as Player, " +
                "Sum(GP) as [Games Played], " +
                "Sum(Goals) as Goals, " +
                "Sum(Shots) as SOG, " +
                "Case Sum(Shots) When 0 then 0 else Round(Sum(Goals)/Sum(Shots),4)*100 END as [S%], "+
                "Sum(AttemptsBlocked) as [A/B], " +
                "Sum(ShotsMissed) as [Miss], " +
                "Sum(TotalShotsTowardsNet) as [TS], " + 
                "Case Sum(TotalShotsTowardsNet) When 0 Then 0 else Round(Sum(Goals)/Sum(TotalShotsTowardsNet),4)*100 END as [TS%]"
                ;
        }
        
        return "Id as Rank, Description as Season, " +
               "'<a class=\"'+ IsCurrent + '\" href=\"../Player.aspx?id=' + convert(varchar, playerId) + '\">' + PlayerName + '</a>' as Player, " +
               "Age, " +
               "GP as [Games Played], " +
               "Goals, " +
               "Shots as [SOG], " +
               "ShootingPercent as [S%], " +
               "AttemptsBlocked as [A/B], " +
               "ShotsMissed as [Miss], " +
               "TotalShotsTowardsNet as [TS], " +
               "TotalShootingPercent as [TS%]";
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
        string ageLow = Request["age-low"];
        string ageHigh = Request["age-high"];
        string position = Request["position"];
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
        SqlCommand seasonsCommand = new SqlCommand("select orderNumber, Description from Season where orderNumber > 12 order by orderNumber ", scripts.GetConnection());
        seasonlow.DataSource = seasonsCommand.ExecuteReader(CommandBehavior.CloseConnection);
        seasonlow.DataTextField = "Description";
        seasonlow.DataValueField = "orderNumber";
        seasonlow.DataBind();
        seasonlow.SelectedValue = Request["ctl00$mainBody$seasonlow"];

        SqlCommand seasonsCommand2 = new SqlCommand("select orderNumber, Description from Season where orderNumber > 12 order by orderNumber", scripts.GetConnection());
        seasonhigh.DataSource = seasonsCommand2.ExecuteReader();
        seasonhigh.DataTextField = "Description";
        seasonhigh.DataValueField = "orderNumber";
        seasonhigh.DataBind();
        seasonhigh.SelectedValue = Request["ctl00$mainBody$seasonhigh"];

    }

}
