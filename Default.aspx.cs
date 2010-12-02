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

    public string LazyMansNews(String sqlString)
    {
        SqlCommand sqlCommand = new SqlCommand(sqlString, scripts.GetConnection());
        string returnStr = String.Empty;
        using (SqlDataReader reader = sqlCommand.ExecuteReader())
        {
            while (reader.Read())
            {
                returnStr += String.Format("{0} :<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{1}<br />", Convert.ToString(reader[1]), Convert.ToString(reader[0]));
                
            }
        }
        //returnStr += "</ul>";
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
