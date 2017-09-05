using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using SalesRepo.Model;

namespace SalesRepo
{
    public class OrderRepository
    {
        private SqlConnection _sqlConnection;

        public OrderRepository()
        {
            _sqlConnection = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Sales;Integrated Security=True;Pooling=False");
        }

        public Order Get(int orderId)
        {
            var order = new Order();

            try
            {
                _sqlConnection.Open();

                SqlCommand cmd = new SqlCommand("Select * from [Order] where id=@id", _sqlConnection);
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = orderId;

                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        order.Id = (int) rdr["id"];
                        order.CustomerId = (int) rdr["CustomerId"];
                        order.OrderDate = DateTime.Parse(rdr["OrderDate"].ToString());
                    }
                }
                
                cmd = new SqlCommand("Select * from OrderProduct where orderId=@orderId", _sqlConnection);
                cmd.Parameters.Add("@orderId", SqlDbType.Int).Value = orderId;

                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        var orderProduct = new OrderProduct();

                        orderProduct.Id = (int)rdr["id"];
                        orderProduct.OrderId = (int)rdr["OrderId"];
                        orderProduct.ProductId = (int)rdr["ProductId"];
                        orderProduct.Qty = int.Parse(rdr["Qty"].ToString());

                        order.OrderProducts.Add(orderProduct);
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

            return order;
        }

        public List<Order> GetAllByCustomerId(int customerId)
        {
            var orders = new List<Order>();
            SqlDataReader rdr = null;

            try
            {
                _sqlConnection.Open();
                SqlCommand cmd = new SqlCommand("Select * from [Order] where customerid=@customerid", _sqlConnection);

                cmd.Parameters.Add("@customerid", SqlDbType.Int).Value = customerId;

                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    var order = new Order();

                    order.Id = (int)rdr["id"];
                    order.CustomerId = (int)rdr["CustomerId"];
                    order.OrderDate = DateTime.Parse(rdr["OrderDate"].ToString());

                    orders.Add(order);
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

            return orders;
        }

        public List<Order> GetAll()
        {
            var orders = new List<Order>();
            SqlDataReader rdr = null;

            try
            {
                _sqlConnection.Open();
                SqlCommand cmd = new SqlCommand("Select * from [Order]", _sqlConnection);

                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    var order = new Order();

                    order.Id = (int)rdr["id"];
                    order.CustomerId = (int)rdr["CustomerId"];
                    order.OrderDate = DateTime.Parse(rdr["OrderDate"].ToString());

                    orders.Add(order);
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

            return orders;
        }

        public void UpdateOrderProduct(OrderProduct orderProduct)
        {
            try
            {
                _sqlConnection.Open();
                string commandString = @"update OrderProduct set productid=@productid, qty=@qty where id=@id";
                SqlCommand cmd = new SqlCommand(commandString, _sqlConnection);
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = orderProduct.Id;
                cmd.Parameters.Add("@productid", SqlDbType.Int).Value = orderProduct.ProductId;
                cmd.Parameters.Add("@qty", SqlDbType.Int).Value = orderProduct.Qty;

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

        public void Update(Order order)
        {
            try
            {
                _sqlConnection.Open();
                string commandString = @"update [order] set customerid=@customerid, orderdate=@orderdate where id=@id";
                SqlCommand cmd = new SqlCommand(commandString, _sqlConnection);
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = order.Id;
                cmd.Parameters.Add("@customerid", SqlDbType.Int).Value = order.CustomerId;
                cmd.Parameters.Add("@orderdate", SqlDbType.DateTime).Value = order.OrderDate;

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

        public void AddProduct(int orderId, int productid, int qty)
        {
            try
            {
                _sqlConnection.Open();
                string commandString = @"insert into OrderProduct VALUES (@orderId, @productid, @qty)";
                SqlCommand cmd = new SqlCommand(commandString, _sqlConnection);
                cmd.Parameters.Add("@orderId", SqlDbType.Int).Value = orderId;
                cmd.Parameters.Add("@productid", SqlDbType.Int).Value = productid;
                cmd.Parameters.Add("@qty", SqlDbType.Int).Value = qty;

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
