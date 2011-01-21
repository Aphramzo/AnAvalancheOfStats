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

        SqlCommand seasonsCommand2 = new SqlCommand("select Id, Name from avDBPlayer order by name", scripts.GetConnection());
        player2.DataSource = seasonsCommand2.ExecuteReader(CommandBehavior.CloseConnection);
        player2.DataTextField = "Name";
        player2.DataValueField = "Id";
        player2.DataBind();
    }
}
