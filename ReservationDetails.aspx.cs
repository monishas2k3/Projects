using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Data.SqlClient;

public partial class ReservationDetails : System.Web.UI.Page
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
            HttpCookie cookie = Request.Cookies["reservationCode"];
            if (cookie != null)
            {
                getReservationDetail(cookie["reservationCode"]);
            }
        }

    }

    private void getReservationDetail(string mReservationCode)
    {
        string query = "SELECT * FROM ReservationTable WHERE reservationCode ='" + mReservationCode + "'";

        using (SqlCommand cmd = new SqlCommand())
        {
            cmd.Connection = con;
            cmd.CommandText = query; // Set the query

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        reservationCode.Text = mReservationCode.ToString();
                        trainCode.Text = reader["trainCode"].ToString();
                        trainName.Text = reader["trainName"].ToString();
                        seats.Text = reader["seatCount"].ToString();
                        source.Text = reader["source"].ToString();
                        destination.Text = reader["destination"].ToString();
                        price.Text = reader["ticketPrice"].ToString();
                        passengerName.Text = reader["passengerName"].ToString();
                        passengerMobile.Text = reader["passengerMobile"].ToString();
                        passengerAge.Text = reader["passengerAge"].ToString();
                        passengerSource.Text = reader["passengerSource"].ToString();
                        passengerDestination.Text = reader["passengerDestination"].ToString();
                        reservationDate.Text = reader["reservationDate"].ToString();
                    }
                }
            }
        }
    }
}