using System;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.SqlClient;

public partial class _Default : System.Web.UI.Page 
{
    private CommonPage scripts;
    protected void Page_Load(object sender, EventArgs e)
    {
        scripts = new CommonPage();
    }

    public string LazyMansMilestones()
    {
        var sqlString = "select * from avDBvGoalMileStones";
        return LazyMansGenericMilestones(sqlString);
    }

    public string LazyMansPointMilestones()
    {
        var sqlString = "select * from avDBvPointMileStones";
        return LazyMansGenericMilestones(sqlString);
    }

    public string LazyMansGPMilestones()
    {
        var sqlString = "select * from avDBvGPMileStones";
        return LazyMansGenericMilestones(sqlString);
    }

    public string LazyMansGenericMilestones(String sqlString)
    {
        var returnStr = "<ul>";
        SqlCommand command = new SqlCommand(sqlString, scripts.GetConnection());
        using (SqlDataReader reader = command.ExecuteReader())
        {
            while (reader.Read())
            {
                returnStr += String.Format("<li>{0}</li>", Convert.ToString(reader[0]));

            }
        }
        returnStr += "</ul>";
        return returnStr;
    }

    public string LazyMansNews(String sqlString)
    {
        SqlCommand sqlCommand = new SqlCommand(sqlString, scripts.GetConnection());
        string returnStr = "<ul>";
        using (SqlDataReader reader = sqlCommand.ExecuteReader())
        {
            while (reader.Read())
            {
                returnStr += String.Format("<li>{0}: {1}</li>", Convert.ToString(reader[1]), Convert.ToString(reader[0]));
                
            }
        }
        returnStr += "</ul>";
        return returnStr;
    }
    public string LazyMansStatUpdates()
    {
        String sqlString = "select top 5 description, date from news where type = 1 order by date desc ";
        return LazyMansNews(sqlString);
    }

    public string LazyMansFeatureUpdates()
    {
        String sqlString = "select top 5 description, date from news where type = 0 order by date desc ";
        return LazyMansNews(sqlString);
    }

}
