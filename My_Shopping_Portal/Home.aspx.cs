using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Drawing;

namespace My_Shopping_Portal
{
    public partial class Home : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(WebConfigurationManager.ConnectionStrings["connectShop"].ToString());

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SqlDataAdapter da = new SqlDataAdapter("select Catid,Category, '~/Images/'+category+'.png'[Image] from CategoryList", con);
                DataTable dtcat = new DataTable();
                da.Fill(dtcat);
                DataList1.DataSource = dtcat;
                DataList1.DataBind();
            }
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            Response.Redirect("List.aspx?catid="+btn.CommandArgument+"&category="+btn.Text);
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (con.State == ConnectionState.Closed)
                con.Open();
            SqlCommand cmdlog = new SqlCommand("Select cid,name,address,password from customers where email='" + txtEmail.Text.Trim() + "'", con);
            SqlDataReader dr = cmdlog.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();
                if (dr[3].ToString().Equals(txtPwd.Text.Trim()))
                {
                    lblUser.Text= dr[1].ToString();
                    Session["loggedIn"] = true;
                    Session["name"] = dr[1].ToString();
                    Session["CustomerId"] = dr[0].ToString();
                    Session["address"] = dr[2].ToString();
                    if (Request.QueryString["source"] != null)
                    {
                        Response.Redirect("Payments.aspx");
                    }
                }
                else
                {
                    lblMsg.Text = "Authentication Failed";
                    lblMsg.ForeColor = Color.Red;
                }                
            }
            else
            {
                lblMsg.Text = "Authentication Failed";
                lblMsg.ForeColor = Color.Red;
            }
        }
    }
}