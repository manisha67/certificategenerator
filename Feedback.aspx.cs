
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Feedback : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        if (Session["email"] == null)
        {
            Response.Write("<script LANGUAGE='JavaScript' >alert('You should be logged in to give feedback')</script>");
        }
        else
        {
            String uname = Session["email"].ToString();

            String feedback = TextBox1.Text;
            MySqlConnection conn = new MySqlConnection("server=127.0.0.1;uid=root;pwd=root;database=egovernance;");
            string strSQL = "insert into feedback values('{0}','{1}')";
            String format = String.Format(strSQL, uname, feedback);

            conn.Open();
            MySqlCommand db_cmd = new MySqlCommand(format, conn);
            int i = db_cmd.ExecuteNonQuery();
            Response.Write("<script LANGUAGE='JavaScript' >alert('Your response has been submitted successfully')</script>");
        }
    }
}