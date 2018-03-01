using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;

namespace WebApplication1
{
    public partial class Registration : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Model1"].ConnectionString);
                conn.Open();
                string checkuser = "select count(*) from Player where Username= '"+ TextBoxAnvändarnamn.Text + "' ";
                SqlCommand com = new SqlCommand(checkuser, conn);

                int temp = Convert.ToInt32(com.ExecuteScalar().ToString());
                if(temp == 1)
                {
                    Response.Write("Användarnamn finns redan");
                }

                conn.Close();
            }
        }

        protected void TextBox3_TextChanged(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try { 
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Model1"].ConnectionString);
            conn.Open();
            string insertQuery = "insert into Player (Name,Username,Password) values (@name, @Username,@Password)";
            SqlCommand com = new SqlCommand(insertQuery, conn);
                com.Parameters.AddWithValue("@name", TextBoxNamn.Text);
                com.Parameters.AddWithValue("@Username", TextBoxAnvändarnamn.Text);
                com.Parameters.AddWithValue("@Password", TextBoxLösen.Text);
                com.ExecuteNonQuery();
                Response.Redirect("Management.aspx");
                Response.Write("Din användare är skapad");

                conn.Close();
            }
            catch(Exception ex)
            {
                Response.Write("Error:" + ex.ToString());
            }
        }
    }
}