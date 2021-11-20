using System;

namespace WebForms.UserControls
{
    public partial class WebFormUserControl_Contact : System.Web.UI.UserControl
    {
        public string Name { get; set; }
        public string Message { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                Name = txtName.Text;
                Message = txtMessage.Text;
            }
            else
            {
                txtName.Text = Name;
                txtMessage.Text = Message ;
            }
        }
    }
}