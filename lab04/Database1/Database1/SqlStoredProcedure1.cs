using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;

public partial class StoredProcedures
{
    [Microsoft.SqlServer.Server.SqlProcedure]
    public static void SqlStoredProcedure1 (int id)
    {
        using (SqlConnection contextConnection = new SqlConnection("context connection = true"))
        {
            SqlCommand contextCommand =
            new SqlCommand(
            "Select MIN(AlbumYear) from AlbumDB " +
            "where GroupID = @id", contextConnection);

            contextCommand.Parameters.AddWithValue("@id", id);
            contextConnection.Open();

            SqlContext.Pipe.ExecuteAndSend(contextCommand);
        }
    }
}
