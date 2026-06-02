using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Data.SqlClient;

public partial class AllReservations : System.Web.UI.Page
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

    protected void viewBtn_Click(object sender, EventArgs e)
    {
        HttpCookie cookie = new HttpCookie("reservationCode");
        String reservationCode = Convert.ToString((sender as LinkButton).CommandArgument);
        cookie["reservationCode"] = reservationCode;
        // Cookie will be persisted for 30 days
        // cookie.Expires = DateTime.Now.AddDays(30);
        Response.Cookies.Add(cookie);
        Response.Redirect("ReservationDetails.aspx");
    }

    protected void deleteBtn_Click(object sender, EventArgs e)
    {
        string mReservationCode = Convert.ToString((sender as LinkButton).CommandArgument);
        string query = "Delete FROM ReservationTable WHERE reservationCode ='" + mReservationCode + "'";
        cmd.CommandText = query;
        cmd.ExecuteNonQuery();
        // con.Close();
        GridView1.DataBind();

    }
   
}