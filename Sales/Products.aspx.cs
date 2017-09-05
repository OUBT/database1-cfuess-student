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
    public partial class Products : System.Web.UI.Page
    {
        private List<Product> _products = new List<Product>();
        private readonly ProductRepository _repo = new ProductRepository();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindData();
            }
        }

        private void BindData()
        {
            _products = _repo.GetAll();
            ProductGrid.DataSource = _products;
            ProductGrid.DataBind();
        }

        protected void ProductGrid_RowEditing(object sender, GridViewEditEventArgs e)
        {
            ProductGrid.EditIndex = e.NewEditIndex;
            BindData();
        }

        protected void ProductGrid_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = (GridViewRow)ProductGrid.Rows[e.RowIndex];

            string id = ((TextBox)row.Cells[1].Controls[0]).Text;
            string name = ((TextBox)row.Cells[2].Controls[0]).Text;
            string qty = ((TextBox)row.Cells[3].Controls[0]).Text;

            Product product = new Product();

            product.Id = int.Parse(id);
            product.Name = name;
            product.Qty = int.Parse(qty);

            _repo.Update(product);

            ProductGrid.EditIndex = -1;
            BindData();
        }

        protected void ProductGrid_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            ProductGrid.EditIndex = -1;
            BindData();
        }
    }
}