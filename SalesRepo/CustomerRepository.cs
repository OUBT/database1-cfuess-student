using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

using SalesRepo.Model;

namespace SalesRepo
{
    public class CustomerRepository
    {
        private SqlConnection _sqlConnection;

        public CustomerRepository()
        {
            _sqlConnection = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Sales;Integrated Security=True;Pooling=False");
        }

        public List<Customer> GetAll()
        {
            var customers = new List<Customer>();
            SqlDataReader rdr = null;

            try
            {
                _sqlConnection.Open();
                SqlCommand cmd = new SqlCommand("Select * from Customer", _sqlConnection);

                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    var customer = new Customer();

                    customer.Id = (int)rdr["id"];
                    customer.FirstName = rdr["FirstName"].ToString();
                    customer.LastName = rdr["LastName"].ToString();
                    customer.Age = (int)rdr["age"];
                    customer.City = rdr["City"].ToString();

                    customers.Add(customer);
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

            return customers;
        }



        public void Update(Customer customer)
        {
            try
            {
                _sqlConnection.Open();
                string commandString = @"update customer set firstname=@first, lastname=@last, age=@age, city=@city where id=@id";
                SqlCommand cmd = new SqlCommand(commandString, _sqlConnection);
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = customer.Id;
                cmd.Parameters.Add("@first", SqlDbType.NVarChar).Value = customer.FirstName;
                cmd.Parameters.Add("@last", SqlDbType.NVarChar).Value = customer.LastName;
                cmd.Parameters.Add("@age", SqlDbType.Int).Value = customer.Age;
                cmd.Parameters.Add("@city", SqlDbType.NVarChar).Value = customer.City;

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

        public Customer Get(int customerId)
        {
            var customer = new Customer();

            try
            {
                _sqlConnection.Open();
                SqlCommand cmd = new SqlCommand("Select * from Customer where id=@id", _sqlConnection);
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = customerId;

                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        customer = new Customer();

                        customer.Id = (int) rdr["id"];
                        customer.FirstName = rdr["FirstName"].ToString();
                        customer.LastName = rdr["LastName"].ToString();
                        customer.Age = (int) rdr["age"];
                        customer.City = rdr["City"].ToString();
                    }
                }
            }
            finally
            {
                if (_sqlConnection != null)
                {
                    _sqlConnection.Close();
                }
            }

            return customer;
        }
    }
}
