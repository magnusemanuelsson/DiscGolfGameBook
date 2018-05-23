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
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Model1"].ConnectionString);
            conn.Open();
            string checkuser = "select count(*) from Player where Username= '" + TextBoxAnvändarnamn.Text + "' ";
            SqlCommand com = new SqlCommand(checkuser, conn);

            int temp = Convert.ToInt32(com.ExecuteScalar().ToString());
            conn.Close();
            if (temp == 1)
            {
                conn.Open();
                string checkPasswordQuery = "select password from Player where Username= '" + TextBoxAnvändarnamn.Text + "' ";
                string idOnUser = "select ID from Player where Username= '" + TextBoxAnvändarnamn.Text + "' ";

                SqlCommand passComm = new SqlCommand(checkPasswordQuery, conn);
                SqlCommand Iduser = new SqlCommand(idOnUser, conn);
               
                string UserId = Iduser.ExecuteScalar().ToString();


                // Gets real username, from Database, to be saved in session
                SqlCommand actualUserSQL = new SqlCommand("select Username from Player where ID= '" + UserId + "' ", conn);
                string actualUsername = actualUserSQL.ExecuteScalar().ToString();


                string activeGame = "select ID from Game where Active = 1 and Player = '" + Int32.Parse(UserId) +"' ";
                SqlCommand gameActive = new SqlCommand(activeGame, conn);
                string GameID = null;
                string GameRoundID = null;
                if (gameActive.ExecuteScalar() != null)
                {
                    GameID = gameActive.ExecuteScalar().ToString();
                    string activeGameround = "select ID from GameRound where Game = '" + Int32.Parse(GameID) + "' ";
                    SqlCommand gameRoundActive = new SqlCommand(activeGameround, conn);
                    GameRoundID = gameRoundActive.ExecuteScalar().ToString();
                }

                string password = passComm.ExecuteScalar().ToString().Replace(" ","");
                if(password == TextBoxLösen.Text)
                {
                    Session["new"] = TextBoxLösen.Text;
                    Session["användare"] = actualUsername;
                    Session["användarID"] = UserId;
                    Response.Write("Password is correct");
                    if(gameActive.ExecuteScalar() != null)
                    {
                        Response.Redirect("~/Home/PlayRound/"+ Int32.Parse(GameRoundID) + "");
                    }
                    if(actualUsername == "Admin")
                    {
                        Response.Redirect("~/Home/Admin/");
                    }
                    Response.Redirect("~/Home/Weather");

                }
                else if (password != TextBoxLösen.Text)
                {
                    Response.Write("Password is not correct");

                }
            }
            

        }

        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        protected void ButtonLoggain_Click(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Model1"].ConnectionString);

            conn.Open();
            string checkuser = "select count(*) from Player where Username= '" + TextBoxAnvändarnamn.Text + "' ";
            SqlCommand com = new SqlCommand(checkuser, conn);

            int temp = Convert.ToInt32(com.ExecuteScalar().ToString());
            conn.Close();
            if(temp != 1)
            {
                Response.Write("Username is not correct");

            }

        }
    }
}