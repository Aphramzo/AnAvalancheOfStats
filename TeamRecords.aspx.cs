using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class TeamRecords : System.Web.UI.Page
{
    private CommonPage scripts;
    protected void Page_Load(object sender, EventArgs e)
    {
        scripts = new CommonPage();
    }

    protected SqlDataReader GetsRecords()
    {
        String sqlString = GetDataString();
        SqlCommand sqlCommand = new SqlCommand(sqlString, scripts.GetConnection());
        return sqlCommand.ExecuteReader();
    }

    private string GetDataString()
    {
        return "select * from vSeasonGameRecords where seasonTypeId = 2 order by description, date";
    }
}
