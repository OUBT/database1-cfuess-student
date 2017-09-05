using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SalesRepo;
using SalesRepo.Model;

namespace Sales
{
    public partial class Orders : System.Web.UI.Page
    {
        List<Order> _orders = new List<Order>();
        CustomerRepository _custRepo = new CustomerRepository();
        OrderRepository _orderRepo = new OrderRepository();
        int _customerId;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Get the customerid from the url, passed from the Customer grid on the last screen
            _customerId = int.Parse(Request.QueryString["customerId"]);
            Customer customer = _custRepo.Get(_customerId);
            CustomerTitle.Text = $"Orders for {customer.FirstName} {customer.LastName} "; 

            // Only bind the data on post back.  If we are submitting the form then we want to keep the forms existing values to pass to the db
            if (!IsPostBack)
            {
                BindData();
            }
        }

        private void BindData()
        {
            //_orders = _orderRepo.GetAll();
            _orders = _orderRepo.GetAllByCustomerId(_customerId);
            OrderGrid.DataSource = _orders;
            OrderGrid.DataBind();
        }

        protected void OrderGrid_RowEditing(object sender, GridViewEditEventArgs e)
        {
            OrderGrid.EditIndex = e.NewEditIndex;
            BindData();
        }

        protected void OrderGrid_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = (GridViewRow)OrderGrid.Rows[e.RowIndex];

            string id = ((TextBox)row.Cells[1].Controls[0]).Text;
            string customerId = ((TextBox)row.Cells[2].Controls[0]).Text;
            string orderDate = ((TextBox)row.Cells[3].Controls[0]).Text;

            Order order = new Order();

            order.Id = int.Parse(id);
            order.CustomerId = int.Parse(customerId);
            order.OrderDate = DateTime.Parse(orderDate);

            _orderRepo.Update(order);

            OrderGrid.EditIndex = -1;
            BindData();
        }

        protected void OrderGrid_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            OrderGrid.EditIndex = -1;
            BindData();
        }
    }
}