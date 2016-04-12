using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Sockets;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SignUp : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        String uname = txtName.Text;
        String email = txtEmail.Text;

        String password = txtPassword.Text;
        double adhaar = Convert.ToDouble(txtAdhaar.Text);
        TcpClient tClient = new TcpClient("gmail-smtp-in.l.google.com", 25);
        string CRLF = "\r\n";
        byte[] dataBuffer;
        string ResponseString;
        NetworkStream netStream = tClient.GetStream();
        StreamReader reader = new StreamReader(netStream);
        ResponseString = reader.ReadLine();
        /* Perform HELLO to SMTP Server and get Response */
        dataBuffer = BytesFromString("HELO " + CRLF);
        netStream.Write(dataBuffer, 0, dataBuffer.Length);
        ResponseString = reader.ReadLine();
        dataBuffer = BytesFromString("MAIL FROM:<hosseinhagh66@gmail.com>" + CRLF);
        netStream.Write(dataBuffer, 0, dataBuffer.Length);
        ResponseString = reader.ReadLine();
        /* Read Response of the RCPT TO Message to know from google if it exist or not */
        dataBuffer = BytesFromString("RCPT TO:<" + email.Trim() + ">" + CRLF);
        netStream.Write(dataBuffer, 0, dataBuffer.Length);
        ResponseString = reader.ReadLine();

        if (GetResponseCode(ResponseString) == 550)
        {
            Response.Write("<script LANGUAGE='JavaScript' >alert('This email id does not exists....')</script>");

        }
        else
        {


            dataBuffer = BytesFromString("QUITE" + CRLF);
            netStream.Write(dataBuffer, 0, dataBuffer.Length);
            tClient.Close();
            MySqlConnection conn = new MySqlConnection("server=127.0.0.1;uid=root;pwd=root;database=egovernance;");
            string strSQL = "insert into register values('{0}','{1}','{2}',{3})";
            String format = String.Format(strSQL, uname, email, password, adhaar);

            conn.Open();
            MySqlCommand db_cmd = new MySqlCommand(format, conn);
            int i = db_cmd.ExecuteNonQuery();
            SendMail();
            Response.Redirect("Services.aspx");
        }
    }
    private byte[] BytesFromString(string str)
    {
        return Encoding.ASCII.GetBytes(str);
    }

    private int GetResponseCode(string ResponseString)
    {
        return int.Parse(ResponseString.Substring(0, 3));
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        txtName.Text = "";
        txtEmail.Text = "";
        txtPassword.Text = "";
        txtCPassword.Text = "";
    }
    protected void SendMail()
    {
        try
            {
                System.Net.Mail.MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress("geetharde@gmail.com");
                mail.To.Add(txtEmail.Text);
                mail.Subject = "E-Governance";
                mail.Body = String.Format("Thanx For Registration. To access services plz login to ur account");

                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("geetharde@gmail.com", "computerengineering");
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

    }
    protected void txtName_TextChanged(object sender, EventArgs e)
    {

    }
    protected void txtEmail_TextChanged(object sender, EventArgs e)
    {

    }
}