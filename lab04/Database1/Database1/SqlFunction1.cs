using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;

public partial class UserDefinedFunctions
{
    [Microsoft.SqlServer.Server.SqlFunction]
    public static SqlInt32 CountLines()
    {
        using (SqlConnection conn
            = new SqlConnection("context connection=true"))
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand( "SELECT COUNT(*) FROM GroupDB", conn);
            return (SqlInt32)cmd.ExecuteScalar();
        }
    }
}
