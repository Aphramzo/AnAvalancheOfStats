using System;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;

public class CommonPage
{
    public SqlConnection GetConnection()
    {
        SqlConnection myConn = new SqlConnection("Data Source=premsql2e.brinkster.com;Initial Catalog=Aphramzo;user id=aphramzo;password=Ava1anch3;Transaction Binding=Explicit Unbind;MultipleActiveResultSets=true");
        myConn.Open();
        return myConn;
    }

    public MySqlConnection GetavDBConnection()
    {
        string MyConString = "SERVER=box294.bluehost.com;" +
                "DATABASE=avaland2_stats;" +
                "UID=avaland2_db;" +
                "PASSWORD=drury1837;";
        MySqlConnection connection = new MySqlConnection(MyConString);
        connection.Open();
        return connection;
    }

    public string PlayerNameFirstLastFromLastFirst(string playerName)
    {
        var nameArr = playerName.Split(',');
        return String.Format("{0} {1}", nameArr[1].Trim(), nameArr[0].Trim());
    }

    public void ExecuteMySQLNonQuery(string sqlToExecute)
    {
        var connection = GetavDBConnection();
        var command = connection.CreateCommand();
        command.CommandText = sqlToExecute;
        command.ExecuteNonQuery();
        connection.Close();
    }

    public void ExecuteMSSQLNonQuery(string sqlToExecute)
    {
        var connection = GetConnection();
        var command = connection.CreateCommand();
        command.CommandText = sqlToExecute;
        command.ExecuteNonQuery();
        connection.Close();
    }

    public String DateToMySQLDate(DateTime date)
    {
        return date.ToString("s").Replace('T', ' ');
    }

    public String CleanUglyHTMLString(String someString)
    {
        return someString.Replace("\t", "").Replace("\n", "").Replace("&nbsp;", "").Trim();
    }

    public int ConvertHeightToInches(String heightStr)
    {
        var heightArr = heightStr.Split('\'');
        return Convert.ToInt32(heightArr[0]) * 12 + Convert.ToInt32(heightArr[1].Replace("\"",""));
    }

    public String RemoveUnclosedHTMLTag(String HTML, string tagToRemove)
    {
        var tmpHTML = HTML;
        while (tmpHTML.IndexOf("<" + tagToRemove) > 0)
        {
            var tmpStr = tmpHTML.Substring(tmpHTML.IndexOf("<"+tagToRemove));
            tmpHTML = tmpHTML.Remove(tmpHTML.IndexOf("<" + tagToRemove), tmpStr.IndexOf(">")+1);
        }
        return tmpHTML;
    }

    public String RemoveClosedHTMLTag(String HTML, string tagToRemove)
    {
        var tmpHTML = HTML;
        while (tmpHTML.IndexOf("<" + tagToRemove) > 0)
        {
            var tmpStr = tmpHTML.Substring(tmpHTML.IndexOf("<" + tagToRemove));
            tmpHTML = tmpHTML.Remove(tmpHTML.IndexOf("<" + tagToRemove), tmpStr.IndexOf("</" + tagToRemove + ">") + tagToRemove.Length+3);
        }
        return tmpHTML;
    }

    public Boolean ToSafeBoolean(object value)
    {
        if(value == null)
            return false;

        if (value is DBNull)
            return false;

        return Convert.ToBoolean(value);
    }

    public Int32 ToSafeInt(object value)
    {
        if (value == null)
            return 0;

        if (value is DBNull)
            return 0;

        return Convert.ToInt32(value);
    }

    public bool GoodPassWord(String pwd)
    {
        if (pwd == "#EDCvfrtgb^YHN")
            return true;

        return false;
    }

}
