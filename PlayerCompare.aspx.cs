using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

public partial class PlayerCompare : System.Web.UI.Page
{
    private CommonPage scripts;
    protected void Page_Load(object sender, EventArgs e)
    {
        scripts = new CommonPage();
        if (!IsPostBack)
            AppendDataToFilters();
    }

    private void AppendDataToFilters()
    {
        SqlCommand seasonsCommand = new SqlCommand("select Id, Name from avDBPlayer order by name", scripts.GetConnection());
        player1.DataSource = seasonsCommand.ExecuteReader(CommandBehavior.CloseConnection);
        player1.DataTextField = "Name";
        player1.DataValueField = "Id";
        player1.DataBind();
        player1.SelectedValue = Request["player1Id"];

        SqlCommand seasonsCommand2 = new SqlCommand("select Id, Name from avDBPlayer order by name", scripts.GetConnection());
        player2.DataSource = seasonsCommand2.ExecuteReader(CommandBehavior.CloseConnection);
        player2.DataTextField = "Name";
        player2.DataValueField = "Id";
        player2.DataBind();
        player2.SelectedValue = Request["player2Id"];

        if(Request["stat"] == "A")
            stat.SelectedIndex = 1;
        if (Request["stat"] == "Points")
            stat.SelectedIndex = 2;
        if (Request["stat"] == "PIM")
            stat.SelectedIndex = 3;
        if (Request["stat"] == "GW")
            stat.SelectedIndex = 4;
        if (Request["stat"] == "PP")
            stat.SelectedIndex = 5;
        if (Request["stat"] == "SH")
            stat.SelectedIndex = 6;
    }
}
