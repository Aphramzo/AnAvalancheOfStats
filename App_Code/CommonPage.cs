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
}
