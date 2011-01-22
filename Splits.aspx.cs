using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Splits : System.Web.UI.Page
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
        String sqlString = String.Format("select {0} from avDBvSplitsTotals {1} {2}", GenerateSelectColumns(), GenerateWhereClause(), GenerateOrderClause());
        SqlCommand gridCommand = new SqlCommand(sqlString, scripts.GetConnection());

        dgSummary.DataSource = gridCommand.ExecuteReader(CommandBehavior.CloseConnection);
        dgSummary.DataBind();

        String sqlString2 = String.Format("select {0} from avDBvSplitsDivision {1} {2}", GenerateSelectColumns(), GenerateWhereClause(), GenerateOrderClause());
        SqlCommand gridCommand2 = new SqlCommand(sqlString2, scripts.GetConnection());

        dgDivision.DataSource = gridCommand2.ExecuteReader(CommandBehavior.CloseConnection);
        dgDivision.DataBind();

        String sqlString3 = String.Format("select {0} from avDBvSplitsMonth {1} {2}", GenerateSelectColumns(), GenerateWhereClause(), GenerateOrderClause());
        SqlCommand gridCommand3 = new SqlCommand(sqlString3, scripts.GetConnection());

        dgMonth.DataSource = gridCommand3.ExecuteReader(CommandBehavior.CloseConnection);
        dgMonth.DataBind();

        String sqlString4 = String.Format("select {0} from avDBvSplitsDay {1} {2}", GenerateSelectColumns(), GenerateWhereClause(), GenerateOrderClause());
        SqlCommand gridCommand4 = new SqlCommand(sqlString4, scripts.GetConnection());

        dgDay.DataSource = gridCommand4.ExecuteReader(CommandBehavior.CloseConnection);
        dgDay.DataBind();

        String sqlString5 = String.Format("select {0} from avDBvSplitsTeam {1} {2}", GenerateSelectColumns(), GenerateWhereClause(), GenerateOrderClause());
        SqlCommand gridCommand5 = new SqlCommand(sqlString5, scripts.GetConnection());

        dgTeam.DataSource = gridCommand5.ExecuteReader(CommandBehavior.CloseConnection);
        dgTeam.DataBind();


    }

    private string GenerateOrderClause()
    {
        return " order by orderby";
    }

    private string GenerateWhereClause()
    {
        return String.Format(" where SeasonId = {0}", Request["ctl00$mainBody$season"]);
    }

    private string GenerateSelectColumns()
    {
        return
            "orderby, " + 
            "Description, " +
            "GP, " +
            "W, " +
            "L, " +
            "T," +
            "Points, " +
            "PointPercent as [Point %]," +
            "GF, " +
            "GA,  " +
            "PPPercent as [PP%]," +
            "PKPercent as [PK%]";
    }

    private void AppendDataToFilters()
    {
        SqlCommand seasonsCommand = new SqlCommand("select id as orderNumber, year as Description from avDBSeason order by ID", scripts.GetConnection());
        season.DataSource = seasonsCommand.ExecuteReader(CommandBehavior.CloseConnection);
        season.DataTextField = "Description";
        season.DataValueField = "orderNumber";
        season.DataBind();
        season.SelectedValue = Request["ctl00$mainBody$season"];

    }
    
}
