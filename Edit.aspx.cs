using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Data.SqlClient;


public partial class Edit : System.Web.UI.Page
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

        if (!IsPostBack)
        {
            HttpCookie cookie = Request.Cookies["trainCode"];
            if (cookie != null)
            {
                getTrainDetail(cookie["trainCode"]);
            }
        }
    }

    private void getTrainDetail(string trainCode)
    {
        string query = "SELECT * FROM TrainTable WHERE trainCode ='" + trainCode + "'";

        cmd.Connection = con;
        cmd.CommandText = query; // Set the query

        using (SqlDataReader reader = cmd.ExecuteReader())
        {
            while (reader.Read())
            {
                lblTrainCode.Text = trainCode.ToString();
                txttrainname.Text = reader["trainname"].ToString();
                txtseatcount.Text = reader["seatcount"].ToString();
                txtsource.Text = reader["source"].ToString();
                txtdestination.Text = reader["destination"].ToString();
                txtprice.Text = reader["ticketprice"].ToString();
            }
        }
    }
    protected void updatetrainBtn_Click(object sender, EventArgs e)
    {
        String trainCode = "";
        HttpCookie cookie = Request.Cookies["trainCode"];
        if (cookie != null)
        {
            trainCode = cookie["trainCode"];
        }
        string query = "UPDATE TrainTable set trainname = '" + txttrainname.Text.ToString() + "', seatcount = '" + txtseatcount.Text.ToString() + "', source = '" + txtsource.Text.ToString() + "', destination = '" + txtdestination.Text.ToString() + "', ticketprice = '" + txtprice.Text.ToString() + "' where trainCode = '" + trainCode.ToString() + "'";
        cmd.CommandText = query;
        cmd.ExecuteNonQuery();
        Response.Redirect("Dashboard.aspx");
    }
}