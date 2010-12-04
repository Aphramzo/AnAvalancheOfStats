using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PlayerPlayoffs : System.Web.UI.Page
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
        String sqlString = String.Format("select {0} from vPlayerPlayoff {1} {2} {3}", GenerateSelectColumns(), GenerateWhereClause(), GenerateGroupByClause(), GenerateHavingClause());
        SqlCommand gridCommand = new SqlCommand(sqlString, scripts.GetConnection());

        dgPlayerSeasons.DataSource = gridCommand.ExecuteReader(CommandBehavior.CloseConnection);
        dgPlayerSeasons.DataBind();


    }

    private string GenerateSelectColumns()
    {
        if (!String.IsNullOrEmpty(Request["sum"]) && String.IsNullOrEmpty(Request["perGame"]))
        {
            return
                "0 as Rank, '<a class=\"'+ IsCurrent + '\" href=\"../Player.aspx?id=' + convert(varchar, playerId) + '\">' + PlayerName + '</a>' as Player, " +
                "Position as POS, " +
                "Sum(GP) as [Games Played], " +
                "Sum(Goals) as Goals, " +
                "Sum(Assists) as Assists, " +
                "Sum(Points) as Points, " +
                "Sum(PIM) as PIM, " +
                "Sum(PlusMinus) as [+/-], " +
                "Sum(PP) as PP, " +
                "Sum(SH) as SH, " +
                "Sum(GW) as GW, " +
                "Sum(Shots) as Shots";
        }
        else if (String.IsNullOrEmpty(Request["sum"]) && !String.IsNullOrEmpty(Request["perGame"]))
        {
            return
            "Id as Rank, Description as Season, " +
               "'<a class=\"'+ IsCurrent + '\" href=\"../Player.aspx?id=' + convert(varchar, playerId) + '\">' + PlayerName + '</a>' as Player, " +
               "Position as POS, " +
               "Age, " +
               "GP as [Games Played], " +
               "GoalsPerGame as [Goals Per Game], " +
               "AssistsPerGame as [Assists Per Game], " +
               "PointsPerGame as [Points Per Game], " +
               "PIMPerGame as [PIM Per Game], " +
               "PPPerGame as [PP Per Game], " +
               "SHPerGame as [SH Per Game], " +
               "GWPerGame as [GW Per Game], " +
               "ShotsPerGame as [Shots Per Game], " +
               "ShiftsPerGame";
        }
        else if (!String.IsNullOrEmpty(Request["sum"]) && !String.IsNullOrEmpty(Request["perGame"]))
        {
            return
               "0 as Rank, '<a class=\"'+ IsCurrent + '\" href=\"../Player.aspx?id=' + convert(varchar, playerId) + '\">' + PlayerName + '</a>' as Player, " +
               "Position as POS, " +
               "Sum(GP) as [Games Played], " +
               "Round((sum(Goals)/Convert(float,sum(GP))),2) as [Goals Per Game], " +
               "Round((sum(assists)/Convert(float,sum(GP))),2) as [Assists Per Game], " +
               "Round((sum(points)/Convert(float,sum(GP))),2) as [Points Per Game], " +
               "Round((sum(PIM)/Convert(float,sum(GP))),2) as [PIM Per Game], " +
               "Round((sum(PP)/Convert(float,sum(GP))),3) as [PP Per Game], " +
               "Round((sum(SH)/Convert(float,sum(GP))),3) as [SH Per Game], " +
               "Round((sum(GW)/Convert(float,sum(GP))),3) as [GW Per Game], " +
               "Round((sum(Shots)/Convert(float,sum(GP))),2) as [Shots Per Game]";
        }
        return "Id as Rank, Description as Season, " +
               "'<a class=\"'+ IsCurrent + '\" href=\"../Player.aspx?id=' + convert(varchar, playerId) + '\">' + PlayerName + '</a>' as Player, " +
               "Position as POS, " +
               "Age, " +
               "GP as [Games Played], " +
               "Goals, " +
               "Assists, " +
               "Points, " +
               "PIM, " +
               "PlusMinus as [+/-], " +
               "PP, " +
               "SH, " +
               "GW, " +
               "Shots, " +
               "ATOI ";
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
        string position = Request["position"];
        string perGame = Request["perGame"];
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

        SqlCommand seasonsCommand3 = new SqlCommand("select Id, Description from Country order by Description", scripts.GetConnection());
        Country.DataSource = seasonsCommand3.ExecuteReader();
        Country.DataTextField = "Description";
        Country.DataValueField = "Id";
        Country.DataBind();
        Country.SelectedValue = Request["ctl00$mainBody$Country"];

    }
}
