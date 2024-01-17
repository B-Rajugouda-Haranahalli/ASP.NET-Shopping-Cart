using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

namespace My_Shopping_Portal
{
    public partial class Payments : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(WebConfigurationManager.ConnectionStrings["connectShop"].ToString());

        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (Session["loggedIn"] == null)
            {
                Response.Redirect("Home.aspx?source=Payments");
            }
            else
            {
                if (!IsPostBack)
                {
                    BindCart();
                    lbltotalamount.Text = GetTotalCartAmount().ToString();
                    SqlDataAdapter da = new SqlDataAdapter("Select * from PaymentMode", con);
                    DataTable dtpay = new DataTable();
                    da.Fill(dtpay);
                    RadioButtonList1.DataSource = dtpay;
                    RadioButtonList1.DataTextField = "Mode";
                    RadioButtonList1.DataValueField = "ModeId";
                    RadioButtonList1.DataBind();
                }
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

        protected void lnkHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("Home.aspx");
        }

        protected void lnkCart_Click(object sender, EventArgs e)
        {
            Response.Redirect("Cart.aspx");
        }

        protected void btnpayment_Click(object sender, EventArgs e)
        {
            bool paymentOptionSelect = false;
            int paymentMode = -1;
            foreach(ListItem li in RadioButtonList1.Items)
            {
                if (li.Selected)
                {
                    paymentOptionSelect = true;
                    paymentMode = Convert.ToInt32(li.Value);
                    break;

                }
            }
            if (!paymentOptionSelect)
            {
                lblMsg.Text = "Choose your payment option";

            }
            else
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                SqlTransaction trans = con.BeginTransaction();
                lblMsg.Text = "";
                try
                {
                    SqlCommand cmdbillID = new SqlCommand();
                    cmdbillID.Transaction = trans;
                    cmdbillID.Connection = con;
                    cmdbillID.CommandText = "select isnull((max(billid)+1),1) from billmaster";
                    int nextbill = Convert.ToInt32(cmdbillID.ExecuteScalar());

                    SqlCommand bill = new SqlCommand();
                    bill.Transaction = trans;
                    bill.Connection = con;
                    bill.CommandText = "insert into billmaster values (@billid, @cid, getdate(),@paymentMode)";
                    bill.Parameters.AddWithValue("@billid", nextbill);
                    bill.Parameters.AddWithValue("@cid", Convert.ToInt32(Session["customerId"]));
                    bill.Parameters.AddWithValue("@paymentMode", paymentMode);
                    bill.ExecuteNonQuery();

                    SqlCommand cmdSales = new SqlCommand();
                    cmdSales.Connection = con;
                    cmdSales.CommandText = "insert into sales values(@pid, @qty, @billId)";
                    SqlParameter paramBillId = new SqlParameter("@billId", SqlDbType.Int);
                    SqlParameter paramPid = new SqlParameter("@pid", SqlDbType.Int);
                    SqlParameter paramQty = new SqlParameter("@qty", SqlDbType.Int);
                    cmdSales.Parameters.Add(paramBillId);
                    cmdSales.Parameters.Add(paramPid);
                    cmdSales.Parameters.Add(paramQty);
                    cmdSales.Transaction = trans;

                    SqlCommand cmdUpdate = new SqlCommand();
                    cmdUpdate.Connection = con;
                    cmdUpdate.CommandText = "update productmaster set quantity=quantity-@qty where pid=@pid";

                    SqlParameter paramProductId = new SqlParameter("@pid", SqlDbType.Int);
                    SqlParameter paramQuantity = new SqlParameter("@qty", SqlDbType.Int);
                    cmdUpdate.Parameters.Add(paramProductId);
                    cmdUpdate.Parameters.Add(paramQuantity);
                    cmdUpdate.Transaction = trans;

                    foreach (DataRow r in ((DataTable)Session["cart"]).Rows)
                    {
                        paramBillId.Value = nextbill;
                        paramQty.Value = Convert.ToInt32(r[4]);
                        paramPid.Value = Convert.ToInt32(r[0]);
                        cmdSales.ExecuteNonQuery();

                        paramProductId.Value = Convert.ToInt32(r[0]);
                        paramQuantity.Value = Convert.ToInt32(r[4]);

                        cmdUpdate.ExecuteNonQuery();
                    }
                    trans.Commit();
                    Session["cart"] = null;
                    btnpayment.Enabled = false;
                    lblMsg.Text = "Thank you for placing the order";
                }
                catch (Exception ee)
                {
                    trans.Rollback();
                    lblMsg.Text = ee.Message;
                }
            
            }
        }
    }
}