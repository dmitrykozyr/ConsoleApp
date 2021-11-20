using System;
using System.Configuration;
using System.Data.SqlClient;

namespace WebForms.MyForms
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            double txtbx = Convert.ToDouble(TextBox1.Text);

            //throw new ApplicationException("My application exception");
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Label3.Text = cfMessage.Name;

            var connectionString = ConfigurationManager.ConnectionStrings["MessageDB"].ConnectionString;
            var insertStatement = "INSERT into Messages (Name, Message) values (@Name, @Message)";
            using (var sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                using(var sqlCommand = new SqlCommand(insertStatement, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("Name", cfMessage.Name);
                    sqlCommand.Parameters.AddWithValue("Message", cfMessage.Message);
                    sqlCommand.ExecuteNonQuery();
                }
            }
        }
    }
}