using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;

public partial class UserDefinedFunctions
{
    private class CountResult
    {
        public SqlInt32 Id;
        public SqlString Name;

        public CountResult(SqlInt32 tId, SqlString tName)
        {
            Id = tId;
            Name = tName;
        }
    }
    [SqlFunction(DataAccess = DataAccessKind.Read, FillRowMethodName = "FillRow",
        TableDefinition = "Id int, Name nchar(40)")]

    public static IEnumerable SqlTableFunction1(SqlInt32 id)
    {
        ArrayList result = new ArrayList();

        using (SqlConnection connection = new SqlConnection("context connection=true"))
        {
            connection.Open();
            using (SqlCommand selectRows = new SqlCommand(
            "SELECT [GroupID], [GroupName] FROM [GroupDB]" +
            "WHERE [GroupID] > @id",
            connection))
            {
                SqlParameter idParam = selectRows.Parameters.Add(
                "@id",
                SqlDbType.Int);
                idParam.Value = id;

                using (SqlDataReader countReader = selectRows.ExecuteReader())
                {
                    while (countReader.Read())
                    {
                        result.Add(new CountResult(
                        countReader.GetSqlInt32(0),
                        countReader.GetSqlString(1)));
                    }
                }
            }
        }
        return result;
    }

    public static void FillRow(object countResultObj, out SqlInt32 Id, out SqlString Name)
    {
        CountResult tmp = (CountResult)countResultObj;

        Id = tmp.Id;
        Name = tmp.Name;
    }
}
