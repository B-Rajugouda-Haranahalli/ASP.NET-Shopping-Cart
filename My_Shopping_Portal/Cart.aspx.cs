using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;


namespace My_Shopping_Portal
{
    public partial class Cart : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["cart"] == null)
            {
                Response.Redirect("Home.aspx");
            }
            if (!IsPostBack)
            {
                BindCart();
                lbltotalamount.Text = GetTotalCartAmount().ToString();
            }
        }

        void BindCart()
        {
            DataTable dtcart = (DataTable)Session["cart"];
            GridView1.DataSource = dtcart;
            GridView1.DataBind();
        }

        double GetTotalCartAmount()
        {
            double total = 0.0D;
            DataTable dtcart = (DataTable)Session["cart"];
            foreach (DataRow r in dtcart.Rows)
            {
                total = total + Convert.ToDouble(r[5]);
            }
            return total;
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            DataTable dtcart = (DataTable)Session["cart"];
            ImageButton btnadd = (ImageButton)sender;
            int productid = Convert.ToInt32(btnadd.CommandArgument);
            DataRow[] itemincart = dtcart.Select("Pid=" + productid);
            if (Convert.ToInt32(itemincart[0][4]) < Convert.ToInt32(itemincart[0][6]))
            {
                itemincart[0][4] = Convert.ToInt32(itemincart[0][4]) + 1;
                itemincart[0][5] = Convert.ToInt32(itemincart[0][4]) * Convert.ToDouble(itemincart[0][3]);
                Session["cart"] = dtcart;
                BindCart();
                lbltotalamount.Text = GetTotalCartAmount().ToString();

            }
            else
            {
                ClientScript.RegisterClientScriptBlock(typeof(string), "add", "alert('This Product Is Out Of Stock')", true);

            }

        }

        protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
        {
            DataTable dtcart = (DataTable)Session["cart"];
            ImageButton btnMinus = (ImageButton)sender;

            int productid = Convert.ToInt32(btnMinus.CommandArgument);
            DataRow[] itemincart = dtcart.Select("Pid=" + productid);
            if (Convert.ToInt32(itemincart[0][4]) > 1)
            {
                itemincart[0][4] = Convert.ToInt32(itemincart[0][4]) - 1;
                itemincart[0][5] = Convert.ToInt32(itemincart[0][4]) * Convert.ToDouble(itemincart[0][3]);
            }
            else
            {
                itemincart[0].Delete();
            }
            Session["cart"] = dtcart;
            BindCart();
            lbltotalamount.Text = GetTotalCartAmount().ToString();

        }

        protected void lnkHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("Home.aspx");
        }

        protected void btnpayment_Click(object sender, EventArgs e)
        {
            Response.Redirect("Payments.aspx");
        }
    }
}