using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Login1_Authenticate(object sender, AuthenticateEventArgs e)
    {
        String value = null;
        MySqlConnection conn = new MySqlConnection("server=127.0.0.1;uid=root;pwd=root;database=egovernance;");
        String uname = Login1.UserName;
        String pwd = Login1.Password;
        string strSQL = "select email,password from register where email='{0}' and password='{1}'";
        String format = String.Format(strSQL, uname, pwd);

        conn.Open();
        MySqlCommand db_cmd = new MySqlCommand(format, conn);
        MySqlDataReader dbread = db_cmd.ExecuteReader();
        String type = Request.QueryString["cerType"];

        
        if(uname.Equals("admin")&&pwd.Equals("admin"))
        {
            Response.Redirect("Details.aspx");
        }
         else{
             if (Session["CerType"] == null)
             {
                 Session["CerType"] = type;

             }
             else
             {
                 Session["CerType"] = type;
             }

             value = Session["CerType"].ToString();
        if (dbread.Read())
        {
            if (dbread["email"].ToString().Equals(uname) && dbread["password"].ToString().Equals(pwd))
            {
                if (Session["email"] == null)
                {
                    Session["email"] = uname;

                }
                if (value.Equals("birth"))
                {
                    
                    Session["Page"] = type;
                    Response.Redirect("BirthInfo.aspx");
                }
                else if (value.Equals("death"))
                {
                   

                    Session["Page"] = type;
                    Response.Redirect("DeathCertificateInfo.aspx");
                }
                else if(value.Equals("marriage"))
                {
                   
                    Session["Page"] = type;
                    Response.Redirect("MarriageInfo.aspx");
                }
               
            }
        }
        else
        {


            Response.Redirect("SignIn.aspx");
        }
    }}
}