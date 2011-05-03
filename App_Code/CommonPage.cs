using System;
using System.Data.SqlClient;

public class CommonPage
{
    public SqlConnection GetConnection()
    {
        SqlConnection myConn = new SqlConnection("Data Source=localhost;Initial Catalog=AnAvalancheOfStats;user id=sa;password=ehrqbwtr;Transaction Binding=Explicit Unbind;MultipleActiveResultSets=true");
        myConn.Open();
        return myConn;
    }

    public string PlayerNameFirstLastFromLastFirst(string playerName)
    {
        var nameArr = playerName.Split(',');
        return String.Format("{0} {1}", nameArr[1].Trim(), nameArr[0].Trim());
    }
}
