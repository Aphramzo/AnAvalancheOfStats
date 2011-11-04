using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class PlayerSearch : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!String.IsNullOrEmpty(Request["term"]))
            Search(Request["term"]);
    }

    private void Search(String searchTerm)
    {
        var first = true;
        var comma = "";
        var scripts = new CommonPage();
        var sqlString = String.Format("select Id, Name from avDBPlayer where name like '%{0}%' order by name", searchTerm.Replace("'", "''").Replace(";", "").Replace(":", "").Replace("drop", "").Replace("select","").Replace("truncate",""));
        SqlCommand command = new SqlCommand(sqlString, scripts.GetConnection());
        var reader = command.ExecuteReader();
        if (!reader.HasRows)
        {
            Response.Write("[\"(no players found)\"]");
        }
        else
        {
            Response.Write("[");
            while (reader.Read())
            {
                if (first)
                    comma = "";
                else
                    comma = ", ";

                Response.Write(String.Format("{1}{{\"label\":\"{0}\",\"value\":\"{2}\"}}", reader[1].ToString(), comma, reader[0].ToString()));

                first = false;
            }
            Response.Write("]");
        }
        reader.Close();
    }
}
