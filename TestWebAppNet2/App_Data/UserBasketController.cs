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
    public class UserBasketController
    {
        public static List<UserBasket> GetAllByUserId(int userId)
        {
            List<UserBasket> userBasketList = new List<UserBasket>();
            UserBasket userBasket = new UserBasket();
            using (DbConnection con = DBConnection.GetConnection())
            {
                using (DbCommand command = con.CreateCommand())
                {
                    command.CommandType = CommandType.Text;
                    command.CommandText = " SELECT * FROM user_basket WHERE user_id = @user_id AND quantity > 0 ";
                    command.Parameters.Add(DBConnection.GetNewParameter("@user_id", userId));

                    DbDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        ReadObject(userBasket, reader);
                        userBasketList.Add(userBasket);
                    }
                }
            }
            return userBasketList;
        }

        public static DataTable GetAllDetailsByUserIdAsDataTable(int userId)
        {
            DataTable dt = new DataTable();
            using (DbConnection con = DBConnection.GetConnection())
            {
                using (DbCommand command = con.CreateCommand())
                {
                    command.CommandType = CommandType.Text;
                    command.CommandText =
                            " SELECT " +
                                " user_basket_id, " +
                                " product_id, " +
                                " title, " +
                                " description, " +
                                " image_path," +
                                " price, " +
                                " quantity, " +
                                " total " +
                            " FROM " +
                                " v_user_basket " +
                            " WHERE " +
                                    " user_id = @user_id ";
                    command.Parameters.Add(DBConnection.GetNewParameter("@user_id", userId));

                    DbDataReader reader = command.ExecuteReader();
                    dt.Load(reader);
                }
            }
            return dt;
        }

        public static float GetTotalForUser(int userId)
        {
            float total = 0f;
            using (DbConnection con = DBConnection.GetConnection())
            {
                using (DbCommand command = con.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "GetTotalForUser";
                    /*" SELECT " +
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
                        " AND quantity > 0; ";*/
                    command.Parameters.Add(DBConnection.GetNewReturnParameterDecimal("@RETURN_VALUE"));
                    command.Parameters.Add(DBConnection.GetNewParameter("@p_user_id", userId));
                    command.ExecuteNonQuery();

                    //try
                    //{
                    total = float.Parse(command.Parameters["@RETURN_VALUE"].Value.ToString());//command.ExecuteScalar().ToString()
                    //}
                    //catch (Exception)
                    //{

                    //}
                }
            }
            return total;
        }

        public static bool ProductExistsForUser(int userId, int productId)
        {
            int count = 0;
            using (DbConnection con = DBConnection.GetConnection())
            {
                using (DbCommand command = con.CreateCommand())
                {
                    command.CommandType = CommandType.Text;
                    command.CommandText = " SELECT COUNT(*) FROM user_basket WHERE user_id = @user_id AND product_id = @product_id ";
                    command.Parameters.Add(DBConnection.GetNewParameter("@user_id", userId));
                    command.Parameters.Add(DBConnection.GetNewParameter("@product_id", productId));

                    count = int.Parse(command.ExecuteScalar().ToString());
                }
            }

            return count > 0;
        }

        public static void AddNewProductForUser(int userId, int productId, int quantity)
        {
            using (DbConnection con = DBConnection.GetConnection())
            {
                using (DbCommand command = con.CreateCommand())
                {
                    command.Transaction = con.BeginTransaction(IsolationLevel.ReadCommitted);
                    command.CommandType = CommandType.Text;
                    command.CommandText = " INSERT INTO user_basket (user_id, product_id, quantity) " +
                                                        " VALUES    (@user_id, @product_id, @quantity) ";
                    command.Parameters.Add(DBConnection.GetNewParameter("@user_id", userId));
                    command.Parameters.Add(DBConnection.GetNewParameter("@product_id", productId));
                    command.Parameters.Add(DBConnection.GetNewParameter("@quantity", quantity));

                    command.ExecuteNonQuery();
                    command.Transaction.Commit();
                }
            }
        }

        public static void DeleteAllForUser(int userId)
        {
            using (DbConnection con = DBConnection.GetConnection())
            {
                using (DbCommand command = con.CreateCommand())
                {
                    command.Transaction = con.BeginTransaction(IsolationLevel.ReadCommitted);
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "DeleteAllForUser";
                        //" DELETE FROM user_basket WHERE user_id = p_user_id ";
                    command.Parameters.Add(DBConnection.GetNewParameter("@p_user_id", userId));

                    command.ExecuteNonQuery();
                    command.Transaction.Commit();
                }
            }
        }

        public static void UpdateBasket(int userId, int productId, int change)
        {
            using (DbConnection con = DBConnection.GetConnection())
            {
                using (DbCommand command = con.CreateCommand())
                {
                    command.Transaction = con.BeginTransaction(IsolationLevel.ReadCommitted);
                    command.CommandType = CommandType.Text;
                    command.CommandText =
                        " UPDATE user_basket SET " +
                            " quantity = quantity " + (change > 0 ? "+" : "") + change.ToString() + " " + //@change " +
                        " WHERE user_id = @user_id " +
                        " AND product_id = @product_id ";
                    command.Parameters.Add(DBConnection.GetNewParameter("@user_id", userId));
                    command.Parameters.Add(DBConnection.GetNewParameter("@product_id", productId));
                    //command.Parameters.Add(DBConnection.GetNewParameter("@change", (change > 0 ? "+" : "") + change.ToString()));

                    command.ExecuteNonQuery();
                    command.Transaction.Commit();
                }
            }
        }

        private static void ReadObject(UserBasket user, DbDataReader reader)
        {
            user.UserBasketId = int.Parse(reader["user_basket_id"].ToString());
            user.UserId = int.Parse(reader["user_id"].ToString());
            user.ProductId = int.Parse(reader["product_id"].ToString());
            user.Quantity = int.Parse(reader["quantity"].ToString());
        }
    }
}
