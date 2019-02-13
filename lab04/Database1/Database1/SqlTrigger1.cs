using System;
using System.Data;
using System.Data.SqlClient;
using Microsoft.SqlServer.Server;

public partial class Triggers
{
    [Microsoft.SqlServer.Server.SqlTrigger(Name = "albumTrigger", Target = "dbo.AlbumDB", Event = "FOR UPDATE")]
    public static void SqlTrigger1 ()
    {

	    SqlContext.Pipe.Send("Trigger is working");
    }
}

