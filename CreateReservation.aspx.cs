using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Data.SqlClient;


public partial class CreateReservation : System.Web.UI.Page
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

    private void getTrainDetail(string mTrainCode)
    {
        string query = "SELECT * FROM TrainTable WHERE trainCode ='" + mTrainCode + "'";

        cmd.Connection = con;
        cmd.CommandText = query; // Set the query

        using (SqlDataReader reader = cmd.ExecuteReader())
        {
            while (reader.Read())
            {
                trainCode.Text = mTrainCode.ToString();
                trainName.Text = reader["trainname"].ToString();
                seats.Text = reader["seatcount"].ToString();
                source.Text = reader["source"].ToString();
                destination.Text = reader["destination"].ToString();
                price.Text = reader["ticketprice"].ToString();
            }
        }
    }
    protected void createreservationBtn_Click1(object sender, EventArgs e)
    {
        String reservationCode = GenerateReservationCode();
        cmd.CommandText = "insert into ReservationTable values('" + reservationCode + "', '" + trainCode.Text + "','" + trainName.Text + "','" + seats.Text + "','" + source.Text + "','" + destination.Text + "','" + price.Text + "','" + txtpassengername.Text + "','" + txtpassengermobile.Text + "','" + txtpassengerage.Text + "','" + txtpassengersource.Text + "','" + txtpassengerdestination.Text + "','" + txtreservationdate.Text + "')";
        cmd.ExecuteNonQuery();
        lblMessage.Text = "Reservation was applied";
        lblMessage.ForeColor = System.Drawing.ColorTranslator.FromHtml("#34eb83");
        lblMessage.Visible = true;
        updateReservationCountAndRevenue(Convert.ToInt32(price.Text.ToString()));
        Response.Redirect("Dashboard.aspx");
    }

    public string GenerateReservationCode()
    {
        string prefix = "RRB";
        Random random = new Random();
        string newReservationCode;

        while (true)
        {
            // Generate a random 4-digit number
            int randomNumber = random.Next(1000, 10000); // Random number between 1000 and 9999
            newReservationCode = prefix + randomNumber;

            // Ensure the generated code is unique in the database
            if (!IsReservationCodeExists(newReservationCode))
            {
                break; // Exit the loop if the code is unique
            }
        }

        return newReservationCode;
    }

    private bool IsReservationCodeExists(string reservationCode)
    {
        // Check if the booking code exists in the database
        string query = "SELECT COUNT(1) FROM ReservationTable WHERE reservationCode ='" + reservationCode + "' ";
        SqlCommand cmd = new SqlCommand(query, con);
        int count = Convert.ToInt32(cmd.ExecuteScalar());
        return count > 0; // True if the booking code exists
    }

    private void updateReservationCountAndRevenue(int mTicketPrice)
    {
        int totalReservations = getReservationCount();
        int lastRevenue = getTotalRevenue();
        string query = "UPDATE TrainTable  SET totalReservations ='" + (totalReservations + 1) + "', totalRevenue ='" + (lastRevenue + mTicketPrice) + "' WHERE trainCode = '" + trainCode.Text + "' ";
        cmd.CommandText = query;
        cmd.ExecuteNonQuery();
        con.Close();
    }

    private int getReservationCount()
    {
        int reservationCount = -1;
        string query1 = "SELECT totalReservations FROM TrainTable WHERE trainCode = '" + trainCode.Text + "'";
        cmd.CommandText = query1;
        object result = cmd.ExecuteScalar();
        if (result != null)
        {
            reservationCount = Convert.ToInt32(result);
        }
        return reservationCount;
    }

    private int getTotalRevenue()
    {
        int totalRevenue = -1;
        string query1 = "SELECT totalRevenue FROM TrainTable  WHERE trainCode = '" + trainCode.Text + "'";
        cmd.CommandText = query1;
        object result = cmd.ExecuteScalar();
        if (result != null)
        {
            totalRevenue = Convert.ToInt32(result);
        }
        return totalRevenue;
    }
    
}