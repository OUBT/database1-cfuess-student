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
    public partial class OrderDetail : System.Web.UI.Page
    {
        OrderRepository _orderRepo = new OrderRepository();
        ProductRepository _productRepo = new ProductRepository();
        int _orderId;

        protected void Page_Load(object sender, EventArgs e)
        {
            _orderId = int.Parse(Request.QueryString["orderId"]);

            if (!IsPostBack)
            {
                BindData();
            }
        }

        private void BindData()
        {
            var order = _orderRepo.Get(_orderId);
            OrderTitle.Text = "Order Details";
            OrderTitle2.Text = $"Order: {order.Id} - Placed: {order.OrderDate.ToShortDateString()} ";

            OrderDetailGrid.DataSource = order.OrderProducts;
            OrderDetailGrid.DataBind();

            lnkBack.NavigateUrl = "Orders.aspx?customerId=" + order.CustomerId;
        }

        protected void OrderDetailGrid_RowEditing(object sender, GridViewEditEventArgs e)
        {
            OrderDetailGrid.EditIndex = e.NewEditIndex;
            BindData();
        }

        protected void OrderDetailGrid_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = (GridViewRow)OrderDetailGrid.Rows[e.RowIndex];

            string id = ((TextBox)row.Cells[1].Controls[0]).Text;
            string orderId = ((TextBox)row.Cells[2].Controls[0]).Text;
            string productId = ((TextBox)row.Cells[3].Controls[0]).Text;
            string qty = ((TextBox)row.Cells[4].Controls[0]).Text;

            OrderProduct orderProduct = new OrderProduct();

            orderProduct.Id = int.Parse(id);
            orderProduct.OrderId = int.Parse(orderId);
            orderProduct.ProductId = int.Parse(productId);
            orderProduct.Qty = int.Parse(qty);

            _orderRepo.UpdateOrderProduct(orderProduct);

            OrderDetailGrid.EditIndex = -1;
            BindData();
        }

        protected void OrderDetailGrid_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            OrderDetailGrid.EditIndex = -1;
            BindData();
        }

        protected void OrderDetailGrid_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            // this locks the id and orderId fields
            e.Row.Cells[1].Enabled = false;
            e.Row.Cells[2].Enabled = false;
        }

        protected void btnShowAdd_Click(object sender, EventArgs e)
        {
            var products = _productRepo.GetAll();
            ddlProduct.DataSource = products;
            ddlProduct.DataTextField = "Name";
            ddlProduct.DataValueField = "Id";
            ddlProduct.DataBind();
            pnlAddProduct.Visible = true;
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            _orderRepo.AddProduct(_orderId, int.Parse(ddlProduct.SelectedValue), int.Parse(txtQty.Text));
            pnlAddProduct.Visible = false;
            BindData();
        }

        protected void btnAddCancel_Click(object sender, EventArgs e)
        {
            pnlAddProduct.Visible = false;
        }
    }
}