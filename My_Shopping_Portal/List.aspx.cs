using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace My_Shopping_Portal
{
    public partial class List : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(WebConfigurationManager.ConnectionStrings["connectShop"].ToString());
        string vendorid = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["category"] != null)
            {

                if (!IsPostBack)
                {
                    lblCategory.Text = Request.QueryString["category"].ToString();

                    SqlDataAdapter da = new SqlDataAdapter("Select p.pid, p.Product, v.Vendor, p.Price, p.Quantity  from ProductMaster p inner join Vendors v on v.vendorid=p.vendorid where p.catid=" + Convert.ToInt32(Request.QueryString["catid"].ToString()), con);
                    DataTable dtproducts = new DataTable();
                    da.Fill(dtproducts);
                    dvProductes.DataSource = dtproducts;
                    dvProductes.DataBind();
                    CheckQuantity();

                    SqlDataAdapter davendors = new SqlDataAdapter("select Vendorid,vendor from Vendors where vendorid in(Select vendorid from Productmaster where catid=" + Convert.ToInt32(Request.QueryString["catid"].ToString()) + ")", con);
                    DataTable dtvendors = new DataTable();
                    davendors.Fill(dtvendors);
                    cblVendors.DataSource = dtvendors;
                    cblVendors.DataTextField = "Vendor";
                    cblVendors.DataValueField = "Vendorid";
                    cblVendors.DataBind();
                }
            }
            if (Session["cart"] == null)
            {
                DataTable dtcart = new DataTable();
                dtcart.Columns.Add("Pid", typeof(int));
                dtcart.Columns.Add("Product", typeof(string));
                dtcart.Columns.Add("Vendor", typeof(string));
                dtcart.Columns.Add("Price", typeof(double));
                dtcart.Columns.Add("Quantity", typeof(int));
                dtcart.Columns.Add("Amount", typeof(double));
                dtcart.Columns.Add("AvailableQuantity", typeof(int));
                Session["cart"] = dtcart;

            }
        }

        protected void lnkCart_Click(object sender, EventArgs e)
        {
            Response.Redirect("Cart.aspx");
        }

        protected void btnHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("Home.aspx");
        }

        protected void cblVendors_SelectedIndexChanged(object sender, EventArgs e)
        {
            string vendors = null;

            foreach (ListItem lis in cblVendors.Items)
            {
                if (lis.Selected)
                {
                    vendors = vendors + lis.Value + ",";
                }
            }
            if (!String.IsNullOrEmpty(vendors))
            {
                ListProducts(vendors.Remove(vendors.LastIndexOf(',')));
            }
            else
            {
                ListProducts();
            }
        }
        void ListProducts(string vendorid)
        {
            SqlDataAdapter da = new SqlDataAdapter("Select p.pid, p.Product, v.Vendor, p.Price, p.Quantity  from ProductMaster p inner join Vendors v on v.vendorid=p.vendorid where v.vendorid in (" + vendorid + ") and p.catid=" + Convert.ToInt32(Request.QueryString["catid"].ToString()), con);
            DataTable dtProducts = new DataTable();
            da.Fill(dtProducts);
            dvProductes.DataSource = dtProducts;
            dvProductes.DataBind();
            CheckQuantity();
        }
        void ListProducts()
        {
            SqlDataAdapter da = new SqlDataAdapter("Select p.pid, p.Product, v.Vendor, p.Price, p.Quantity  from ProductMaster p inner join Vendors v on v.vendorid=p.vendorid where  p.catid=" + Convert.ToInt32(Request.QueryString["catid"].ToString()), con);
            DataTable dtProducts = new DataTable();
            da.Fill(dtProducts);
            dvProductes.DataSource = dtProducts;
            dvProductes.DataBind();
            CheckQuantity();
        }
        void CheckQuantity()
        {
            DataTable dt = (DataTable)dvProductes.DataSource;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (Convert.ToInt32(dt.Rows[i][4]) <= 0)
                {
                    LinkButton btn = (LinkButton)dvProductes.Rows[i].FindControl("LinkButton1");
                    btn.Text = "Out Of Stock";
                    btn.Enabled = false;
                }
            }
        }
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            GridViewRow rowselected = (GridViewRow)btn.Parent.Parent;
            DataTable dt = (DataTable)Session["cart"];
            int productId = Convert.ToInt32(btn.CommandArgument);
            DataRow[] itemisincart = dt.Select("Pid=" + productId);
            if (itemisincart.Length == 0)
            {
                DataRow r = dt.NewRow();
                r[0] = Convert.ToInt32(btn.CommandArgument);
                r[1] = ((Label)rowselected.FindControl("Label6")).Text;
                r[2] = ((Label)rowselected.FindControl("Label3")).Text;
                r[3] = Convert.ToDouble(((Label)rowselected.FindControl("Label4")).Text);
                r[4] = 1;
                r[5] = Convert.ToDouble(((Label)rowselected.FindControl("Label4")).Text);
                r[6] = Convert.ToDouble(((Label)rowselected.FindControl("Label5")).Text);
                dt.Rows.Add(r);
            }
            else
            {
                itemisincart[0][4] = Convert.ToInt32(itemisincart[0][4]) + 1;
                itemisincart[0][5] = Convert.ToInt32(itemisincart[0][4])* Convert.ToDouble(((Label)rowselected.FindControl("Label4")).Text);
            }
            Session["cart"] = dt;
            GetCartItemCount();
        }
        void GetCartItemCount()
        {
            DataTable dt = (DataTable)Session["cart"];
            int totalQuantity = 0;
            foreach (DataRow r in dt.Rows)
            {
                totalQuantity = totalQuantity + Convert.ToInt32(r[4]);
            }
            lnkCart.Text = totalQuantity.ToString();
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("Cart.aspx");
        }
    }
}