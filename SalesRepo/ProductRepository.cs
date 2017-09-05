using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

using SalesRepo.Model;

namespace SalesRepo
{
    public class ProductRepository
    {
        private SqlConnection _sqlConnection;

        public ProductRepository()
        {
            _sqlConnection = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Sales;Integrated Security=True;Pooling=False");
        }

        public List<Product> GetAll()
        {
            var products = new List<Product>();
            SqlDataReader rdr = null;

            try
            {
                _sqlConnection.Open();
                SqlCommand cmd = new SqlCommand("Select * from Product", _sqlConnection);

                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    var product = new Product();

                    product.Id = (int)rdr["id"];
                    product.Name = rdr["Name"].ToString();
                    product.Qty = (int)rdr["qty"];

                    products.Add(product);
                }
            }
            finally
            {
                if (rdr != null)
                {
                    rdr.Close();
                }
                if (_sqlConnection != null)
                {
                    _sqlConnection.Close();
                }
            }

            return products;
        }



        public void Update(Product product)
        {
            try
            {
                _sqlConnection.Open();
                string commandString = @"update product set name=@name, qty=@qty where id=@id";
                SqlCommand cmd = new SqlCommand(commandString, _sqlConnection);
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = product.Id;
                cmd.Parameters.Add("@name", SqlDbType.NVarChar).Value = product.Name;
                cmd.Parameters.Add("@qty", SqlDbType.Int).Value = product.Qty;

                cmd.ExecuteNonQuery();
            }
            finally
            {

                if (_sqlConnection != null)
                {
                    _sqlConnection.Close();
                }
            }

        }
    }
}
