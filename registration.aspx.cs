using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Data.SqlClient;

public partial class Default2 : System.Web.UI.Page
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
    protected void registerBtn_Click(object sender, EventArgs e)
    {
        List<String> emailList = GetEmailList();
        if (emailList.Contains(txtemail.Text))
        {
            lblMessage.Visible = true;
            lblMessage.Text = "This email id was already exist";
        }
        else
        {
            cmd.CommandText = "insert into AdminsTable values('" + txtname.Text + "','" + txtemail.Text + "','" + txtpassword.Text + "','" + txtmobile.Text + "','" + txtaddress.Text + "')";
            cmd.ExecuteNonQuery();
            lblMessage.Text = "User was registered";
            lblMessage.ForeColor = System.Drawing.ColorTranslator.FromHtml("#34eb83");
            lblMessage.Visible = true;
            Response.Redirect("Dashboard.aspx");
        }

    }
    public List<string> GetEmailList()
    {
        List<string> emailList = new List<string>();

        // Query to get email list - using parameterized query
        string query = "SELECT email FROM AdminsTable";

        using (SqlCommand cmd = new SqlCommand(query, con))
        {
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    emailList.Add(reader["email"].ToString());
                }
            }
        }

        return emailList;
    }
    protected void loginBtn_Click(object sender, EventArgs e)
    {
        Response.Redirect("loginpage.aspx");
    }
}