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
    public class ProductController
    {
        public static Products GetByKey(int productId)
        {
            Products product = new Products();
            using (DbConnection con = DBConnection.GetConnection())
            {
                using (DbCommand command = con.CreateCommand())
                {
                    command.CommandType = CommandType.Text;
                    command.CommandText = " SELECT * FROM products WHERE product_id = @product_id ";
                    command.Parameters.Add(DBConnection.GetNewParameter("@product_id", productId));

                    DbDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        ReadObject(product, reader);
                        break;
                    }
                }
            }
            return product;
        }

        public static Products GetByTitle(string title)
        {
            Products product = new Products();
            using (DbConnection con = DBConnection.GetConnection())
            {
                using (DbCommand command = con.CreateCommand())
                {
                    command.CommandType = CommandType.Text;
                    command.CommandText = " SELECT * FROM products WHERE title = @title ";
                    command.Parameters.Add(DBConnection.GetNewParameter("@title", title));

                    DbDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        ReadObject(product, reader);
                        break;
                    }
                }
            }
            return product;
        }

        public static List<Products> GetAllProducts()
        {
            List<Products> productList = new List<Products>();
            Products product = new Products();
            using (DbConnection con = DBConnection.GetConnection())
            {
                using (DbCommand command = con.CreateCommand())
                {
                    command.CommandType = CommandType.Text;
                    command.CommandText = " SELECT * FROM products ";

                    DbDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        ReadObject(product, reader);
                        productList.Add(product);
                    }
                }
            }
            return productList;
        }

        public static DataTable GetAllProductsAsDataTable()
        {
            DataTable dt = new DataTable();
            using (DbConnection con = DBConnection.GetConnection())
            {
                using (DbCommand command = con.CreateCommand())
                {
                    command.CommandType = CommandType.Text;
                    command.CommandText = " SELECT * FROM products ";

                    DbDataReader reader = command.ExecuteReader();
                    dt.Load(reader);
                }
            }
            return dt;
        }

        private static void ReadObject(Products product, DbDataReader reader)
        {
            product.ProductId = int.Parse(reader["product_id"].ToString());
            product.Title = reader["title"].ToString();
            product.Description = reader["description"].ToString();
            product.Price = float.Parse(reader["price"].ToString());
            product.ImagePath = reader["image_path"].ToString();
        }
    }
}
