using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;
using TestWebAppNet2.Data;

namespace TestWebAppNet2.Data
{
    public class UserController
    {
        public static Users GetByKey(int userId)
        {
            Users user = new Users();
            using (DbConnection con = DBConnection.GetConnection())
            {
                using (DbCommand command = con.CreateCommand())
                {
                    command.CommandType = CommandType.Text;
                    command.CommandText = " SELECT * FROM users WHERE user_id = @user_id ";
                    command.Parameters.Add(DBConnection.GetNewParameter("@user_id", userId));

                    DbDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        ReadObject(user, reader);
                        break;
                    }
                }
            }
            return user;
        }

        public static Users GetByUserName(string userName)
        {
            Users user = new Users();
            using (DbConnection con = DBConnection.GetConnection())
            {
                using (DbCommand command = con.CreateCommand())
                {
                    command.CommandType = CommandType.Text;
                    command.CommandText = " SELECT * FROM users WHERE username = @username ";
                    command.Parameters.Add(DBConnection.GetNewParameter("@username", userName));

                    DbDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        ReadObject(user, reader);
                        break;
                    }
                }
            }
            return user;
        }

        public static void SaveNewUser(Users user)
        {
            using (DbConnection con = DBConnection.GetConnection())
            {
                using (DbCommand command = con.CreateCommand())
                {
                    command.Transaction = con.BeginTransaction(IsolationLevel.ReadCommitted);
                    command.CommandType = CommandType.Text;
                    command.CommandText = " INSERT INTO users (username, firstname, surname, address_type, street_address, suburb, city, postal_code) " +
                                                    " VALUES  (@username, @firstname, @surname, @address_type, @street_address, @suburb, @city, @postal_code) ";
                    command.Parameters.Add(DBConnection.GetNewParameter("@username", user.UserName));
                    command.Parameters.Add(DBConnection.GetNewParameter("@firstname", user.FirstName));
                    command.Parameters.Add(DBConnection.GetNewParameter("@surname", user.SurName));
                    command.Parameters.Add(DBConnection.GetNewParameter("@address_type", user.AddressType));
                    command.Parameters.Add(DBConnection.GetNewParameter("@street_address", user.StreetAddress));
                    command.Parameters.Add(DBConnection.GetNewParameter("@suburb", user.Suburb));
                    command.Parameters.Add(DBConnection.GetNewParameter("@city", user.City));
                    command.Parameters.Add(DBConnection.GetNewParameter("@postal_code", user.PostalCode));

                    command.ExecuteNonQuery();
                    command.Transaction.Commit();
                }
            }
        }

        public static void UpdateUser(Users user)
        {
            using (DbConnection con = DBConnection.GetConnection())
            {
                using (DbCommand command = con.CreateCommand())
                {
                    command.Transaction = con.BeginTransaction(IsolationLevel.ReadCommitted);
                    command.CommandType = CommandType.Text;
                    command.CommandText =
                        " UPDATE users SET " +
                            //" username = @username, " +
                            " firstname = @firstname, " +
                            " surname = @surname, " +
                            " address_type = @address_type, " +
                            " street_address = @street_address, " +
                            " suburb = @suburb, " +
                            " city = @city, " +
                            " postal_code = @postal_code " +
                        " WHERE user_id = @user_id ";
                    command.Parameters.Add(DBConnection.GetNewParameter("@user_id", user.UserId));
                    //command.Parameters.Add(DBConnection.GetNewParameter("@username", user.UserName));
                    command.Parameters.Add(DBConnection.GetNewParameter("@firstname", user.FirstName));
                    command.Parameters.Add(DBConnection.GetNewParameter("@surname", user.SurName));
                    command.Parameters.Add(DBConnection.GetNewParameter("@address_type", user.AddressType.ToString()));
                    command.Parameters.Add(DBConnection.GetNewParameter("@street_address", user.StreetAddress));
                    command.Parameters.Add(DBConnection.GetNewParameter("@suburb", user.Suburb));
                    command.Parameters.Add(DBConnection.GetNewParameter("@city", user.City));
                    command.Parameters.Add(DBConnection.GetNewParameter("@postal_code", user.PostalCode));

                    command.ExecuteNonQuery();
                    command.Transaction.Commit();
                }
            }
        }

        private static void ReadObject(Users user, DbDataReader reader)
        {
            user.UserId = int.Parse(reader["user_id"].ToString());
            user.UserName = reader["username"].ToString();
            user.FirstName = reader["firstname"].ToString();
            user.SurName = reader["surname"].ToString();
            user.AddressType = reader["address_type"].ToString().ElementAt(0);
            user.StreetAddress = reader["street_address"].ToString();
            user.Suburb = reader["suburb"].ToString();
            user.City = reader["city"].ToString();
            user.PostalCode = reader["postal_code"].ToString();
        }

        public static int GetNewUserId()
        {
            int newUserId = -1;

            using (DbConnection con = DBConnection.GetConnection())
            {
                using (DbCommand command = con.CreateCommand())
                {
                    command.CommandType = CommandType.Text;
                    command.CommandText = " SELECT max(user_id) FROM users";

                    newUserId = int.Parse(command.ExecuteScalar().ToString()) + 1;
                }
            }
            return newUserId;
        }
    }
}
