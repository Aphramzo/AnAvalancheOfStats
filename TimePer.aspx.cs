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

public partial class TimePer : System.Web.UI.Page
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
        String sqlString = String.Format("select {0} from {2} {1}", GenerateSelectColumns(), GenerateWhereClause(), GenerateSelectTable());
      
        SqlCommand gridCommand = new SqlCommand(sqlString, scripts.GetConnection());

        dgTimePer.DataSource = gridCommand.ExecuteReader(CommandBehavior.CloseConnection);
        dgTimePer.DataBind();


    }

    private string GenerateSelectTable()
    {
        if (!String.IsNullOrEmpty(Request["sum"]))
            return "vItHappensEveryTotal";
        return "vItHappensEvery";
    }

    private string GenerateSelectColumns()
    {
        if (!String.IsNullOrEmpty(Request["sum"]))
        {
            return
                "0 as Rank," +
                   "'<a class=\"'+ IsCurrent + '\" href=\"../Player.aspx?id=' + convert(varchar, playerId) + '\">' + PlayerName + '</a>' as Player, " +
                   "Position as POS, " +
                   "GP as [Games Played], " +
                   "TimePerGoal as [TOI Per Goal], " +
                   "TimePerAssist as [TOI Per Assist], " +
                   "TimePerPoint as [TOI Per Point], " +
                   "TimePerMinor as [TOI Per Minor], " +
                   "TOI";
        }
        return "Id as Rank, Description as Season, " +
               "'<a class=\"'+ IsCurrent + '\" href=\"../Player.aspx?id=' + convert(varchar, playerId) + '\">' + PlayerName + '</a>' as Player, " +
               "Position as POS, " +
               "Age, " +
               "GP as [Games Played], " +
               "TimePerGoal as [TOI Per Goal], " +
               "TimePerAssist as [TOI Per Assist], " +
               "TimePerPoint as [TOI Per Point], " +
               "TimePerMinor as [TOI Per Minor], " +
               "TOI";
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
        List<String> whereClause = new List<string>();
        if (!String.IsNullOrEmpty(GPLow))
            whereClause.Add(String.Format(" GP >= {0}", GPLow.Replace("'", "''")));
        if (!String.IsNullOrEmpty(GPHigh))
            whereClause.Add(String.Format(" GP <= {0}", GPHigh.Replace("'", "''")));
    
        if(String.IsNullOrEmpty(Request["sum"]))
        {
            if (!String.IsNullOrEmpty(seasonLow))
                whereClause.Add(String.Format(" orderNumber >= {0} ", seasonLow));
            if (!String.IsNullOrEmpty(seasonHigh))
                whereClause.Add(String.Format(" orderNumber <= {0} ", seasonHigh));
            if (!String.IsNullOrEmpty(ageLow))
                whereClause.Add(String.Format(" age >= {0}", ageLow.Replace("'", "''")));
            if (!String.IsNullOrEmpty(ageHigh))
                whereClause.Add(String.Format(" age <= {0}", ageHigh.Replace("'", "''")));
        }
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
