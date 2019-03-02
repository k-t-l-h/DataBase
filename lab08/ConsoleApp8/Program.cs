using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Lab08
{
    class Program
    {
        // Fields
        private static string connectionString = @"Data Source=LAPTOP-SUNC637N\SQLEXPRESS; Initial Catalog=Lab01; Integrated Security=True";

        // Main
        static void Main(string[] args)
        {
            Program program = new Program();

            try
            {
                bool flag = true;

                //string input;

                while (flag)
                {
                    Console.WriteLine("\nMenu:");
                    Console.WriteLine("0. Exit");
                    Console.WriteLine("Connected:");
                    Console.WriteLine("1. Connection Info");
                    Console.WriteLine("2. Data Reader");
                    Console.WriteLine("3. SqlCommand, Parameters, Select");
                    Console.WriteLine("4. SqlCommand, Parameters, Insert");
                    Console.WriteLine("5. StoredProcedure without parameters");
                    Console.WriteLine("6. StoredProcedure with parameters");
                    Console.WriteLine("Disconnected:");
                    Console.WriteLine("7. DataTableCollection");
                    Console.WriteLine("8. DataTableCollection with filter");
                    Console.WriteLine("9. Delete");
                    Console.WriteLine("10. Insert");
                    Console.WriteLine("11. XML");

                    var input = Console.ReadLine();
                    switch (input)
                    {
                        case "0":
                            flag = false;
                            break;
                        case "1":
                            program.createConnection();
                            break;
                        case "2":
                            program.FirstQuery();
                            break;
                        case "3":
                            program.SecondQuery();
                            break;
                        case "4":
                            program.ThirdQuery();
                            break;
                        case "5":
                            program.FourthQuery();
                            break;
                        case "6":
                            program.FifthQuery();
                            break;
                        case "7":
                            program.SixthQuery();
                            break;
                        case "8":
                            program.SeventhQuery();
                            break;
                        case "9":
                            program.EighthQuery();
                            break;
                        case "10":
                            program.NinthQuery();
                            break;
                        case "11":
                            program.TenQuery();
                            break;
                        default:
                            Console.WriteLine("Try again..");
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error. Message: " + ex.Message);
            }
        }

        // Methods for lab
        // Connected
        // 1.
        // Create connection, shows info, close connection
        void createConnection()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                Console.WriteLine("Connection has been opened.");
                Console.WriteLine("Connection properties:");
                Console.WriteLine("\tConnection string: {0}", connection.ConnectionString);
                Console.WriteLine("\tDatabase:          {0}", connection.Database);
                Console.WriteLine("\tData Source:       {0}", connection.DataSource);
                Console.WriteLine("\tServer version:    {0}", connection.ServerVersion);
                Console.WriteLine("\tConnection state:  {0}", connection.State);


                Console.WriteLine("\tWorkstation id:    {0}", connection.WorkstationId);
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Connection error! Message: " + ex.Message);
            }
            finally
            {
                connection.Close();
                Console.WriteLine("Connection has been closed.");
            }
            Console.ReadKey();
        }

        // 2.
        public void FirstQuery()
        {
            const string queryString = @"SELECT COUNT(*) FROM GroupDB";

            SqlConnection connection = new SqlConnection(connectionString);

            SqlCommand command = new SqlCommand(queryString, connection);
            try
            {
                connection.Open();
                Console.WriteLine("Number of Groups: " + command.ExecuteScalar());
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error! Message: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            Console.ReadKey();
        }

        // 3.
        public void SecondQuery()
        {
            const string queryString = @"SELECT GroupID, GroupName, GenreID, Country
                                        FROM GroupDB
                                        WHERE Country = 'Ireland'";

            SqlConnection connection = new SqlConnection(connectionString);

            
            SqlCommand command = new SqlCommand(queryString, connection);
            try
            {
                connection.Open();
                SqlDataReader dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    
                    Console.WriteLine("Group id: {0};  Group Name: {1};  GenreID: {2};  Country: {3};  ",
                                                                                                         dataReader.GetValue(0),
                                                                                                         dataReader.GetValue(1),
                                                                                                         dataReader.GetValue(2),
                                                                                                         dataReader.GetValue(3));
                }

                dataReader.Close();
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error! Message: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            Console.ReadKey();
        }

        // 4.
        public void ThirdQuery()
        {
            const string maxIDQuery = @"SELECT MAX(GroupID) FROM GroupDB";
            const string insertQuery = "INSERT INTO GroupDB(GroupID, GroupName, GenreID, Country) " + "VALUES(@GroupID, @GroupName, @GenreID, @Country)";

            SqlConnection connection = new SqlConnection(connectionString);

            SqlCommand maxIDCommand = new SqlCommand(maxIDQuery, connection);
            SqlCommand insertCommand = new SqlCommand(insertQuery, connection);
            insertCommand.Parameters.Add("@GroupID", SqlDbType.Int);
            insertCommand.Parameters.Add("@GroupName", SqlDbType.NVarChar, 255);
            insertCommand.Parameters.Add("@GenreID",SqlDbType.Int);
            insertCommand.Parameters.Add("@Country", SqlDbType.NVarChar, 255);
            try
            {
                connection.Open();
                int maxID = Convert.ToInt32(maxIDCommand.ExecuteScalar());

                Console.WriteLine("Enter Name: ");
                var GroupName = Console.ReadLine();

                Console.WriteLine("Enter GenreID: ");
                var GenreID = Console.ReadLine();

                Console.WriteLine("Enter Country: ");
                var Country = Console.ReadLine();

                maxID += 1;
                
                insertCommand.Parameters["@GroupID"].Value = maxID;
                insertCommand.Parameters["@GroupName"].Value = GroupName;
                insertCommand.Parameters["@GenreID"].Value = GenreID;
                insertCommand.Parameters["@Country"].Value = Country;

                insertCommand.ExecuteNonQuery();
                Console.WriteLine("Insert completed successfully.");
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error! Message: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            Console.ReadKey();
        }

        // 5.
        public void FourthQuery()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();

                SqlCommand command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GetTablesInfo";

                SqlDataReader dataReader = command.ExecuteReader();

                while (dataReader.Read())
                    Console.WriteLine(dataReader[0].ToString());

                dataReader.Close();
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error! Message: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            Console.ReadKey();
        }

        // 6.
        public void FifthQuery()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();

                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "GetGenreID";

                SqlParameter inParameter = command.Parameters.Add("@id", SqlDbType.Int);
                inParameter.Direction = ParameterDirection.Input;
                Console.WriteLine("Enter Group's id:");
                int id = Convert.ToInt32(Console.ReadLine());
                inParameter.Value = id;

                SqlParameter outParameter = command.Parameters.Add("@G_id", SqlDbType.Int);
                outParameter.Direction = ParameterDirection.Output;

                SqlDataReader dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    Console.WriteLine(dataReader[0].ToString());
                }

                dataReader.Close();

                Console.WriteLine("Group's id: " + id + "\nGroup genre id: " + command.Parameters["@G_id"].Value);
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error! Message: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            Console.ReadKey();
        }

        // Disconnected
        // 7.
        public void SixthQuery()
        {
            const string queryString = @"SELECT * FROM AlbumDB
                                        WHERE AlbumYear > '1990'";
            SqlConnection connection = new SqlConnection(connectionString);

            try
            {
                connection.Open();

                SqlDataAdapter dataAdapter = new SqlDataAdapter(queryString, connection);
                DataSet dataSet = new DataSet();
                dataAdapter.Fill(dataSet, "AlbumDB");

                DataTable dataTable = dataSet.Tables["AlbumDB"];

                foreach (DataRow row in dataTable.Rows)
                {

                    foreach (DataColumn column in dataTable.Columns)
                    {
                        Console.Write(row[column] + "   ");
                    }
                    Console.WriteLine();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error! Message: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            Console.ReadKey();
        }

        // 8.
        public void SeventhQuery()
        {
            const string queryString = @"SELECT * FROM GroupDB";
            SqlConnection connection = new SqlConnection(connectionString);

            try
            {
                connection.Open();
                SqlDataAdapter dataAdapter = new SqlDataAdapter();

                dataAdapter.SelectCommand = new SqlCommand(queryString, connection);

                DataSet dataSet = new DataSet();
                dataAdapter.Fill(dataSet, "GroupDB");
                DataTableCollection dataTableCollection = dataSet.Tables;

                string filter = "GenreID = 10";

                foreach (DataRow row in dataTableCollection["GroupDB"].Select(filter))
                {
                    Console.WriteLine(row["GroupName"] + "   " + (row["GenreID"].ToString()));
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error! Message: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            Console.ReadKey();
        }

        // 9.
        public void EighthQuery()
        {
            const string dataQuery = @"SELECT * FROM GroupDB";
            const string deleteQuery = @"DELETE FROM GroupDB WHERE GroupID = @id";
            SqlConnection connection = new SqlConnection(connectionString);

            try
            {
                connection.Open();
                Console.WriteLine("Enter group's id which you want to delete: ");
                int id = Convert.ToInt32(Console.ReadLine());

                SqlDataAdapter dataAdapter = new SqlDataAdapter(new SqlCommand(dataQuery, connection));
                DataSet dataSet = new DataSet();
                dataAdapter.Fill(dataSet, "GroupDB");
                DataTable table = dataSet.Tables["GroupDB"];

                string filter = "GroupID = " + id;

                foreach (DataRow row in table.Select(filter))
                {
                    row.Delete();
                }

                DataSet dataSet2 = new DataSet();
                dataAdapter.Fill(dataSet, "GroupDB");
                DataTable table2 = dataSet2.Tables["GroupDB"];

                dataSet.WriteXml("XMLdel.xml");

                SqlCommand deleteCommand = new SqlCommand(deleteQuery, connection);
                deleteCommand.Parameters.Add("@id", SqlDbType.Int, 4, "GroupID");
                dataAdapter.DeleteCommand = deleteCommand;
                dataAdapter.Update(dataSet, "GroupDB");

                Console.WriteLine("Deleted successfully.");
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error! Message: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            Console.ReadKey();
        }

        // 10.
        public void NinthQuery()
        {
            const string maxIDQuery = @"SELECT MAX(GroupID) FROM GroupDB";
            const string dataQuery = @"SELECT * FROM GroupDB";
            const string insertQuery = @"INSERT INTO GroupDB(GroupID, GroupName, GenreID, Country) 
                                                            VALUES (@GroupID, @GroupName, @GenreID, @Country)";

            SqlConnection connection = new SqlConnection(connectionString);

            try
            {
                connection.Open();
                SqlCommand maxIDCommand = new SqlCommand(maxIDQuery, connection);
                int maxID = Convert.ToInt32(maxIDCommand.ExecuteScalar());

                Console.WriteLine("Enter Name: ");
                var GroupName = Console.ReadLine();

                Console.WriteLine("Enter GenreID: ");
                var GenreID = Console.ReadLine();

                Console.WriteLine("Enter Country: ");
                var Country = Console.ReadLine();
                
                int GroupID = maxID + 1;

                SqlDataAdapter dataAdapter = new SqlDataAdapter(new SqlCommand(dataQuery, connection));
                DataSet dataSet = new DataSet();
                dataAdapter.Fill(dataSet, "GroupDB");
                DataTable table = dataSet.Tables["GroupDB"];
                
                DataRow insertingRow = table.NewRow();
                insertingRow["GroupID"] = GroupID;
                insertingRow["GroupName"] = GroupName;
                insertingRow["GenreID"] = GenreID;
                insertingRow["Country"] = Country;

                table.Rows.Add(insertingRow);

                SqlCommand insertQueryCommand = new SqlCommand(insertQuery, connection);
                insertQueryCommand.Parameters.Add("@GroupID", SqlDbType.Int, 4, "GroupID");
                insertQueryCommand.Parameters.Add("@GroupName", SqlDbType.VarChar, 255, "GroupName");
                insertQueryCommand.Parameters.Add("@GenreID", SqlDbType.Int, 4, "GenreID");
                insertQueryCommand.Parameters.Add("@Country", SqlDbType.VarChar, 255, "Country");

                dataAdapter.InsertCommand = insertQueryCommand;
                dataAdapter.Update(dataSet, "GroupDB");

                Console.WriteLine("Inserted successfully.");
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error! Message: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            Console.ReadKey();
        }

        // 11.
        public void TenQuery()
        {
            const string query = "SELECT * FROM GroupDB";
            SqlConnection connection = new SqlConnection(connectionString);

            try
            {
                connection.Open();

                SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connection);
                DataSet dataSet = new DataSet();
                dataAdapter.Fill(dataSet, "GroupDB");
                DataTable table = dataSet.Tables["GroupDB"];
                
                dataSet.WriteXml("XML.xml");
                Console.WriteLine("XML file created successfully.");
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error! Message: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            Console.ReadKey();
        }
    }
}
