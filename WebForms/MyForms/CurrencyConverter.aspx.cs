using System;

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
    }
}