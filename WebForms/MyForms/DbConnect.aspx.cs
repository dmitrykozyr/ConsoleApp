using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

namespace WebForms
{
    public partial class WebForm1 : Page
    {
        SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);

        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Write("<h1>Cyberpunk</h1>");
            connection.Open();
        }

        private void DbJobs(SqlCommand command, string labelText)
        {
            command.ExecuteNonQuery();
            connection.Close();
            Label1.Text = labelText;
            GridView1.DataBind();
            TextBox1.Text = "";
            TextBox2.Text = "";
        }

        protected void BtnInsert_Click(object sender, EventArgs e)
        {
            string request = "insert into TableWebForms values('" + TextBox1.Text + "','" + TextBox2.Text + "')";
            var command = new SqlCommand(request, connection);
            DbJobs(command, "Data was inserted");
        }

        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            string request = "update TableWebForms set name='" + TextBox2.Text + "' where id='" + TextBox1.Text + "'";
            var command = new SqlCommand(request, connection);
            DbJobs(command, "Data was updated");
        }

        protected void BtnDelete_Click(object sender, EventArgs e)
        {
            string request = "delete from TableWebForms where id='" + Convert.ToInt32(TextBox1.Text).ToString() + "'";
            var command = new SqlCommand(request, connection);
            DbJobs(command, "Data was deleted");
        }

        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            string request = "select * from TableWebForms where (Id like '%' +@Id+ '%')";
            var command = new SqlCommand(request, connection);
            command.Parameters.Add("@Id", SqlDbType.NVarChar).Value = TextBox3.Text;
            command.ExecuteNonQuery();
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = command;
            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet, "Id");

            GridView1.DataSourceID = null;
            GridView1.DataSource = dataSet;
            GridView1.DataBind();

            connection.Close();
            Label1.Text = "Data was selected";
        }
    }
}