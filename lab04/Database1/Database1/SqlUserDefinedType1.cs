using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;


[Serializable]
[Microsoft.SqlServer.Server.SqlUserDefinedType(Format.Native)]
public struct SqlUserDefinedType1: INullable
{
    `
    public override string ToString()
    {
        return string.Empty;
    }
    
    public bool IsNull
    {
        get
        {
            return _null;
        }
    }
    
    public static SqlUserDefinedType1 Null
    {
        get
        {
            SqlUserDefinedType1 h = new SqlUserDefinedType1();
            h._null = true;
            return h;
        }
    }
    
    public static SqlUserDefinedType1 Parse(SqlString s)
    {
        if (s.IsNull)
            return Null;
        SqlUserDefinedType1 u = new SqlUserDefinedType1();
        return u;
    }
    
    // Это метод-заполнитель
    public string Method1()
    {
        // Введите здесь код
        return string.Empty;
    }
    
    // Это статический метод заполнителя
    public static SqlString Method2()
    {
        // Введите здесь код
        return new SqlString("");
    }
    
    // Это поле элемента-заполнителя
    public int _var1;
 
    // Закрытый член
    private bool _null;
}