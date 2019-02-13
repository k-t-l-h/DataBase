using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;

[Serializable]
[Microsoft.SqlServer.Server.SqlUserDefinedAggregate(Format.Native)]
public struct SqlAggregateAvg
{
    int count;

    public void Init()
    {
        count = 0;
    }

    public void Accumulate(SqlString Value, SqlString @type)
    {
       if (Value == type)
        {
            
            count++;
        }
                
    }

    public void Merge (SqlAggregateAvg Group)
    {
        count += Group.count;
    }

    public SqlInt32 Terminate ()
    {
        return new SqlInt32 (count);
    }
    
}
