using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace TestWebAppNet2
{
    public class DBConnection
    {
        private static string CreateUsersTableQuery =
            " CREATE TABLE IF NOT EXISTS [users] ( " +
                " [user_id] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, " +
                " [username] VARCHAR(20) NOT NULL, " +
                " [firstname] VARCHAR(100) NOT NULL, " +
                " [surname] VARCHAR(100) NOT NULL, " +
                " [address_type] CHAR(1) NULL, " +
                " [street_address] VARCHAR(500) NULL, " +
                " [suburb] VARCHAR(50) NULL, " +
                " [city] VARCHAR(50) NULL, " +
                " [postal_code] VARCHAR(10) NULL " +
                " ) ";

        private static string CreateProductsTableQuery =
            " CREATE TABLE IF NOT EXISTS [products] ( " +
                " [product_id] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, " +
                " [title] VARCHAR(200) NOT NULL, " +
                " [description] VARCHAR(1000) NOT NULL, " +
                " [price] DECIMAL(5,2) NOT NULL, " +
                " [image_path] VARCHAR(500) NOT NULL " +
                " ) ";

        private static string CreateUserBasketTableQuery =
            "CREATE TABLE IF NOT EXISTS [user_basket] ( " +
                " [user_basket_id] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, " +
                " [user_id] INTEGER, " +
                " [product_id] INTEGER, " +
                " [quantity] INTEGER NOT NULL, " +
                " FOREIGN KEY (user_id) REFERENCES users(user_id), " +
                " FOREIGN KEY (product_id) REFERENCES products(product_id) " +
                " ) ";

        public static string InsertIntoUsersTableQuery = " INSERT INTO users (username, firstname, surname, address_type, street_address, suburb, city, postal_code) " +
                                                        " VALUES  ('gvntri005', 'Trinesan', 'Govender', 'R', '7 Cotswold Drive', 'Shallcross', 'Durban', '4093'), " +
                                                                " ('gvnsue001', 'Suendharan', 'Govender', 'R', '8 Cotswold Drive', 'Shallcross', 'Durban', '4093'), " +
                                                                " ('gvnpre002', 'Prem', 'Govender', 'B', '9 Cotswold Drive', 'Shallcross', 'Durban', '4093') ";

        public static string InsertIntoProductsTableQuery = " INSERT INTO products (title, description, price, image_path) " +
                                                        " VALUES  ('Pencil', 'aaa', 1.00, '/product_images/pencil.jpg'), " +
                                                                " ('Pen', 'qqq', 2.00, '/product_images/pen.jpg'), " +
                                                                " ('Erasor', 'www', 3.00, '/product_images/erasor.jpg'), " +
                                                                " ('Ruler', 'eee', 4.00, '/product_images/ruler.jpg'), " +
                                                                " ('Paper', 'sss', 5.00, '/product_images/paper.jpg'), " +
                                                                " ('Clipboard', 'ddd', 6.00, '/product_images/clipboard.jpg'), " +
                                                                " ('File', 'zzz', 7.00, '/product_images/file.jpg'), " +
                                                                " ('Book', 'xxx', 8.00, '/product_images/book.jpg'), " +
                                                                " ('Scissors', 'ccc', 9.00, '/product_images/scissors.jpg'), " +
                                                                " ('Tippex', 'vvv', 10.00, '/product_images/tippex.jpg') ";

        public static string InsertIntoUserBasketTableQuery = " INSERT INTO user_basket (user_id, product_id, quantity) " +
                                                            " VALUES  (1, 1, 5), " +
                                                                    " (1, 3, 7), " +
                                                                    " (1, 5, 9) ";


        private static string CreateTotalFunction = // SQLite cannot use Functions
            " CREATE FUNCTION GetTotalForUser(p_user_id INTEGER) RETURNS VARCHAR(20) " +
            " BEGIN " +
                " DECLARE total DECIMAL(10,2); " +
                " SELECT " +
                    " SUM(price * quantity) " +
                " INTO " +
                    " total " +
                " FROM " +
                            " user_basket " +
                        " INNER JOIN " +
                            " products " +
                        " USING (product_id) " +
                " WHERE " +
                        " user_id = p_user_id " +
                    " AND quantity > 0; " +
                " DECLARE CONTINUE HANDLER FOR NOT FOUND " +
                    " SET total = 0; " +
                " RETURN(total); " +
            " END ";

        private static string CreateDeleteProcedure = // SQLite cannot use Procedures
            " CREATE PROCEDURE DeleteAllForUser(IN p_user_id INTEGER) " +
            " BEGIN " +
                " DELETE FROM user_basket " +
                " WHERE " +
                        " user_id = p_user_id; " +
                " COMMIT; " +
            " END ";

        private static string DatabaseFile = Path.Combine(HttpContext.Current.Server.MapPath("~/App_Data"), "MSSQLServerDatabase.mdf");// "D:\\Projects\\TestWebApp\\TestWebAppNet2\\databaseFile.db";
        //private static string ConnectionString = "data source=" + DatabaseFile;
        private static string ConnectionString = //"Server=INSTANCE_NAME;Database=DATABASE_NAME;Trusted_Connection = True;""data source=" + DatabaseFile;
            "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=" + DatabaseFile + ";Integrated Security=True;Connect Timeout=30";
        //private static SqlConnection connection;
        private static SqlConnection connection;

        public static SqlConnection GetConnection()
        {
            /*if (connection != null)
            {
                try
                {
                    return connection.OpenAndReturn();
                }
                catch (Exception)
                {
                    return connection;
                }
            }
            else
            {*/
                // Connect to the database 
                connection = new SqlConnection(ConnectionString);
                connection.Open();
                return connection;
            //}
        }

        public static object GetNewParameter(string paramName, int paramValue)
        {
            return new SqlParameter(paramName, paramValue);
        }

        public static object GetNewParameter(string paramName, string paramValue)
        {
            return new SqlParameter(paramName, paramValue);
        }

        public static object GetNewReturnParameterDecimal(string paramName)
        {
            SqlParameter param = new SqlParameter(paramName, SqlDbType.Decimal);
            param.Direction = ParameterDirection.ReturnValue;
            return param;
        }

        public static void TestDB()
        {
            try
            {
                using (SqlConnection con = GetConnection())
                {
                    if (con == null)
                    {
                        throw new Exception("DB Connection failed");
                    }
                    con.Close();
                }

                /* For Local SQLite
                if (!File.Exists(DatabaseFile))
                {
                    // Create the file which will be hosting our database
                    SqlConnection.CreateFile(DatabaseFile);

                    // Connect to the database 
                    connection = new SqlConnection(ConnectionString);
                    connection.Open();

                    // Create a database command
                    SQLiteCommand command = new SQLiteCommand(connection);
                    command.Transaction = connection.BeginTransaction();


                    // Create the table
                    command.CommandText = CreateUsersTableQuery;
                    command.ExecuteNonQuery();

                    // Insert entries in database table
                    command.CommandText = InsertIntoUsersTableQuery;
                    command.ExecuteNonQuery();

                    // Create the table
                    command.CommandText = CreateProductsTableQuery;
                    command.ExecuteNonQuery();

                    // Insert entries in database table
                    command.CommandText = InsertIntoProductsTableQuery;
                    command.ExecuteNonQuery();

                    // Create the table
                    command.CommandText = CreateUserBasketTableQuery;
                    command.ExecuteNonQuery();

                    // Insert entries in database table
                    command.CommandText = InsertIntoUserBasketTableQuery;
                    command.ExecuteNonQuery();

                    //command.CommandText = " DELIMITER $$ ";
                    //command.ExecuteNonQuery();

                    // Create the function
                    //command.CommandText = CreateTotalFunction;
                    //command.ExecuteNonQuery();

                    // Create the procedure
                    //command.CommandText = CreateDeleteProcedure;
                    //command.ExecuteNonQuery();

                    //command.CommandText = " DELIMITER ; ";
                    //command.ExecuteNonQuery();

                    // Commit
                    command.Transaction.Commit();
                    connection.Close();
                }
                */
                //GetAllLocations(connection);
            }
            finally
            {
                //connection.Close(); // Close the connection to the database
            }
        }

    }
}
