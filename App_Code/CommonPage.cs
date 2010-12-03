using System;
using System.Data.SqlClient;

public class CommonPage
{
    public SqlConnection GetConnection()
    {
        SqlConnection myConn = new SqlConnection("Data Source=premsql2e.brinkster.com;Initial Catalog=aphramzo;user id=aphramzo;password=Ava1anch3;Transaction Binding=Explicit Unbind;MultipleActiveResultSets=true");
        myConn.Open();
        return myConn;
    }
}
