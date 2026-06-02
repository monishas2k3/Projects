using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Data.SqlClient;


public partial class _Default : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\monis\OneDrive\Documents\Miniproject\App_Data\RailwayDB.mdf;Integrated Security=True");
    SqlCommand cmd;
    protected void Page_Load(object sender, EventArgs e)
    {
        // Here We check the sql connection was already open or not
        // if already run any previous connection once close then open new connection
        if (con.State == ConnectionState.Open)
            con.Close();
        con.Open();
        cmd = con.CreateCommand();
        cmd.CommandType = CommandType.Text;

    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void addtrainBtn_Click(object sender, EventArgs e)
    {
        Response.Redirect("Add.aspx");
    }
    protected void allreservationsBtn_Click(object sender, EventArgs e)
    {
        Response.Redirect("AllReservations.aspx");
    }
    protected void reportBtn_Click(object sender, EventArgs e)
    {
        Response.Redirect("Report.aspx");
    }
    protected void logoutBtn_Click(object sender, EventArgs e)
    {
        Response.Redirect("loginpage.aspx");
    }

    protected void editBtn_Click(object sender, EventArgs e)
    {
        HttpCookie cookie = new HttpCookie("trainCode");
        String trainCode = Convert.ToString((sender as LinkButton).CommandArgument);
        cookie["trainCode"] = trainCode;
        // Cookie will be persisted for 30 days
        // cookie.Expires = DateTime.Now.AddDays(30);
        Response.Cookies.Add(cookie);
        Response.Redirect("Edit.aspx");
    }

    protected void deleteBtn_Click(object sender, EventArgs e)
    {
        String trainCode = Convert.ToString((sender as LinkButton).CommandArgument);
        string query = "Delete FROM TrainTable WHERE trainCode ='" + trainCode + "'";
        cmd.CommandText = query;
        cmd.ExecuteNonQuery();
        con.Close();
        GridView1.DataBind();
    }

    protected void reserveNowBtn_Click(object sender, EventArgs e)
    {
        HttpCookie cookie = new HttpCookie("trainCode");
        String trainCode = Convert.ToString((sender as LinkButton).CommandArgument);
        cookie["trainCode"] = trainCode;
        // Cookie will be persisted for 30 days
        // cookie.Expires = DateTime.Now.AddDays(30);
        Response.Cookies.Add(cookie);
        Response.Redirect("CreateReservation.aspx");
    }
}