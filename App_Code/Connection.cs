using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Used to establish connection to db
/// </summary>
public class Connection
{

    public static SqlConnection GetDBConnection()
    {

        string connectionString = ConfigurationManager.ConnectionStrings["Local"].ConnectionString;

        SqlConnection connection = new SqlConnection(connectionString);
        connection.Open();
        return connection;
    }

}