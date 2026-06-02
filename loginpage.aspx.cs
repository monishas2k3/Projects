using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Data.SqlClient;

public partial class loginpage : System.Web.UI.Page
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
    protected void Button1_Click(object sender, EventArgs e)
    {
        // Get all email id list in admin table
        List<string> emailList = getEmailList();
        if (!emailList.Contains(txtemail.Text.ToString().Trim()))
            lblMessage.Text = "This email was not registered";
        else if (getAdminPassword(txtemail.Text.ToString().Trim()) != txtpassword.Text)
            lblMessage.Text = "Password was incorrect";
        else
            Response.Redirect("Dashboard.aspx");
    }
    public List<string> getEmailList()
    {
        List<string> emailList = new List<string>();
        string query = "SELECT email FROM AdminsTable";
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
                        emailList.Add(reader["email"].ToString());
                    }
                }
            }
        }
        return emailList;
    }

    public string getAdminPassword(string emailId)
    {
        string adminPassword = "";
        string query = "SELECT password FROM AdminsTable where email = '" + emailId + "'";
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
                        adminPassword = reader["password"].ToString();
                    }
                }
            }
        }
        return adminPassword;
    }
    protected void registerBtn_Click(object sender, EventArgs e)
    {
        Response.Redirect("registration.aspx");
    }
    
}