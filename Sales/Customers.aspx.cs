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
    public partial class Customers : System.Web.UI.Page
    {

        private List<Customer> _customers = new List<Customer>();
        private readonly CustomerRepository _repo = new CustomerRepository();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindData();
            }
        }

        private void BindData()
        {
            _customers = _repo.GetAll();
            CustomerGrid.DataSource = _customers;
            CustomerGrid.DataBind();
        }

        protected void CustomerGrid_RowEditing(object sender, GridViewEditEventArgs e)
        {
            CustomerGrid.EditIndex = e.NewEditIndex;
            BindData();
        }

        protected void CustomerGrid_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = (GridViewRow)CustomerGrid.Rows[e.RowIndex];

            string id = ((TextBox)row.Cells[1].Controls[0]).Text;
            string firstName = ((TextBox)row.Cells[2].Controls[0]).Text;
            string lastName = ((TextBox)row.Cells[3].Controls[0]).Text;
            string age = ((TextBox)row.Cells[4].Controls[0]).Text;
            string city = ((TextBox)row.Cells[5].Controls[0]).Text;

            Customer customer = new Customer();

            customer.Id = int.Parse(id);
            customer.FirstName = firstName;
            customer.LastName = lastName;
            customer.Age = int.Parse(age);
            customer.City = city;

            _repo.Update(customer);

            CustomerGrid.EditIndex = -1;
            BindData();
        }

        protected void CustomerGrid_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            CustomerGrid.EditIndex = -1;
            BindData();
        }
    }
}