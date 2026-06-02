using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Data.SqlClient;


public partial class Add : System.Web.UI.Page
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
    protected void addtrainBtn_Click(object sender, EventArgs e)
    {
        List<String> trainIDList = GetTrainCodeList();
        if (trainIDList.Contains(txttraincode.Text.ToString()))
        {
            lblMessage.Text = "Train code was already exist!";
            lblMessage.ForeColor = System.Drawing.ColorTranslator.FromHtml("#ff1100");
        }
        else
        {
            cmd.CommandText = "insert into TrainTable values('" + txttraincode.Text + "','" + txttrainname.Text + "','" + txtseatcount.Text + "','" + txtsource.Text + "','" + txtdestination.Text + "','" + txtprice.Text + "','" + 0 + "','" + 0 + "')";
            cmd.ExecuteNonQuery();
            lblMessage.Text = "Train was created";
            lblMessage.ForeColor = System.Drawing.ColorTranslator.FromHtml("#34eb83");
            lblMessage.Visible = true;
            Response.Redirect("Dashboard.aspx");
        }
    }

    public List<string> GetTrainCodeList()
    {
        List<string> trainCodeList = new List<string>();

        string query = "SELECT trainCode FROM TrainTable";

        using (SqlCommand cmd = new SqlCommand(query, con))
        {
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    trainCodeList.Add(reader["trainCode"].ToString());
                }
            }
        }
        return trainCodeList;
    }
}