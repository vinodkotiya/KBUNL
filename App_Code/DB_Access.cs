using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

/// <summary>
/// Summary description for DB_Access
/// </summary>
public class DB_Access:DB_Status
{
    public static string strConnectionString;
	public DB_Access()
	{
        strConnectionString = ConfigurationManager.ConnectionStrings["IntranetConnectionString"].ConnectionString;
	}
    //Open Connection
    public DB_Status OpenConnection(SqlConnection SQLCon, bool UseExistingConnection)
    {
        DB_Status openDBStatus = new DB_Status();
        try
        {
            if (SQLCon == null)
            {
                SQLCon = new SqlConnection();
            }
            SQLCon.ConnectionString = strConnectionString;
            SQLCon.Open();
            switch (SQLCon.State)
            {
                case ConnectionState.Broken: SQLCon.Close(); SQLCon.Open(); break;
                case ConnectionState.Closed: SQLCon.Open(); break;
                case ConnectionState.Open:
                    if (!UseExistingConnection)
                    {
                        SQLCon.Close(); SQLCon.Open();
                    }
                    break;
                default:
                    SQLCon.Open();
                    break;
            }
            openDBStatus.SetMessage(Status.Success, "Connection Established", "Connection has been established successfully.", "DBA->DBACCESS", "Established connection between Business Component and Database Server", "", "Asked operation completed", "");
        }
        catch (Exception ex)
        {
            openDBStatus.SetMessage(Status.Error, ex.Message, ex.InnerException.Message, "DBA->DBACCESS", "Established connection between Business Component and Database Server", "", "Operation failed", "");
        }
        return openDBStatus;
    }
    //Close Connection
    public DB_Status CloseConnection(SqlConnection SQLCon, bool Dispose)
    {
        DB_Status closeDBStatus = new DB_Status();
        try
        {
            if (SQLCon.State!= ConnectionState.Closed)
            {
                SQLCon.Close();
                if (Dispose == true)
                {
                    SQLCon.Dispose();
                }
            }
            closeDBStatus.SetMessage(Status.Success, "Connection closed", "Connection has been closed successfully.", "DBA->DBACCESS", "Close established connection between Business Component and Database Server", "", "Asked operation completed", "");
        }
        catch (Exception ex)
        {
            closeDBStatus.SetMessage(Status.Error, ex.Message, ex.Message, "DBA->DBACCESS", "Closing established connection between Business Component and Database Server", "", "Operation failed", "");
        }
        return closeDBStatus;
    }

    //Insert, Update, Delete Records
    public DB_Status sp_InsertUpdateDelete(string storeProcedure, int NoOfParameter, string[] parameter, string[] value)
    {
        DB_Status objDBStatus = new DB_Status();
        try
        {
            SqlConnection SQLCon = new SqlConnection();
            objDBStatus = OpenConnection(SQLCon, true);
            if (objDBStatus.OperationStatus == DB_Status.Status.Error)
                return objDBStatus;

            SqlCommand cmd = new SqlCommand(storeProcedure, SQLCon);
            cmd.CommandType = CommandType.StoredProcedure;
            for (int i = 0; i < NoOfParameter; i++)
            {
                cmd.Parameters.AddWithValue(parameter[i], value[i]);
            }
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            objDBStatus = CloseConnection(SQLCon, true);
            objDBStatus.SetMessage(Status.Success,"Execute Query", "Query has been executed successfully", "DBA->DBAccess", "Executing SQL Query", "", "Asked operation completed", "");
        }
        catch (Exception ex)
        {
            objDBStatus.SetMessage(Status.Error, ex.Message, ex.Message, "DBA->DBAccess", "Executing SQL Query", "", "Operation failed", "");
        }
        return objDBStatus;
    }
    //Read Single Value
    public DB_Status sp_readSingleData(string storeProcedure, int NoOfParameter, string[] parameter, string[] value)
    {
        DB_Status objDBStatus = new DB_Status();
        try
        {
            SqlConnection SQLCon = new SqlConnection();
            objDBStatus = OpenConnection(SQLCon, true);
            if (objDBStatus.OperationStatus == DB_Status.Status.Error)
                return objDBStatus;
            string data = "";
            SqlCommand cmd = new SqlCommand(storeProcedure, SQLCon);
            cmd.CommandType = CommandType.StoredProcedure;
            for (int i = 0; i < NoOfParameter; i++)
            {
                cmd.Parameters.AddWithValue(parameter[i], value[i]);
            }
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                data = dr[0].ToString();
            }
            objDBStatus.SingleResult = data;
            dr.Close();
            cmd.Dispose();
            objDBStatus = CloseConnection(SQLCon, true);
            objDBStatus.SetMessage(Status.Success, "Execute Query", "Query has been executed successfully", "DBA->DBAccess", "Executing SQL Query", "", "Asked operation completed", "", data);
            
        }
        catch (Exception ex)
        {
            objDBStatus.SetMessage(Status.Error, ex.Message, ex.Message, "DBA->DBAccess", "Executing SQL Query", "", "Operation failed", "");
        }
        return objDBStatus;
    }
    //Read Multiple/All Data (Reader)
    public DB_Status sp_populateDataSet(string storeProcedure, int NoOfParameter, string[] parameter, string[] value)
    {
        DB_Status objDBStatus = new DB_Status();
        try
        {
            SqlConnection SQLCon = new SqlConnection();
            objDBStatus = OpenConnection(SQLCon, true);
            if (objDBStatus.OperationStatus == DB_Status.Status.Error)
                return objDBStatus;

            
            SqlCommand cmd = new SqlCommand(storeProcedure, SQLCon);
            cmd.CommandType = CommandType.StoredProcedure;
            for (int i = 0; i < NoOfParameter; i++)
            {
                cmd.Parameters.AddWithValue(parameter[i], value[i]);
            }
            SqlDataAdapter SQLDA = new SqlDataAdapter(cmd);
            DataSet DS=new DataSet();
            SQLDA.Fill(DS);
            SQLDA.Dispose();
            cmd.Dispose();
            objDBStatus = CloseConnection(SQLCon, true);
            objDBStatus.SetMessage(Status.Success, "Execute Query", "Query has been executed successfully", "DBA->DBAccess", "Executing SQL Query", "", "Asked operation completed", "", DS);
        }
        catch (Exception ex)
        {
            objDBStatus.SetMessage(Status.Error, ex.Message, ex.Message, "DBA->DBAccess", "Executing SQL Query", "", "Operation failed", "");
        }
        return objDBStatus;
    }
}