using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace LinkedU
{
    public partial class Admin : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if(Request.Cookies["UserName"]== null)
            {
                Response.Redirect("Default.aspx");
            }
            var UserLogin = Request.Cookies["UserName"].Value;
            if (isAdmin(UserLogin) == true)
            {
               
            }
            else
                Response.Redirect("Default.aspx");
        }
        public Boolean isAdmin(String UserLogin)
        {
           


            string connectionString = ConfigurationManager.ConnectionStrings["LinkedUConnectionString"].ConnectionString;
            using (SqlConnection dbConnection = new SqlConnection(connectionString))
            {

                dbConnection.Open();
                String invalidUni = "Error, Please enter the ID of a valid University";
                string adminquery = "SELECT UserLogin from admins WHERE UserLogin = @UserLogin";
                SqlCommand command = new SqlCommand(adminquery, dbConnection);
                SqlDataAdapter da = new SqlDataAdapter(adminquery, dbConnection);
                SqlDataReader dataReader;
                DataTable dt = new DataTable();
                da.SelectCommand.Parameters.AddWithValue("@UserLogin", UserLogin);
                da.Fill(dt);
                command.Parameters.AddWithValue("@UserLogin", UserLogin);
                dataReader = command.ExecuteReader();
                if (dt.Rows.Count == 0)
                {
                    return false;
                  
                }
                else
                {
                    while (dataReader.Read())
                    {
                        return true;

                    }
                    return true;
                }
            }

            
        }




        protected void btnRequest_Click(object sender, EventArgs e)
        {
            String UNITID = Request.Form["UniversityID"];
            int id = Int32.Parse(UNITID);
            //lblResult.Text = "Test";
            validUni(id);
            lblResult.Text = "University successfully marked for review";

        }
        protected void btnRequest0_Click(object sender, EventArgs e)
        {
            String UNITID = Request.Form["UniversityID"];
            int id = Int32.Parse(UNITID);
            //lblResult.Text = "Test";
            lblResult.Visible = true; 
            lblResult.Text = "University successfully unmarked for review";
            DeleteData(id);
         

        }



        public String validUni(int UNITID)
        {
            int UniversityID = 1;
            String UniversityName = "";
            string connectionString = ConfigurationManager.ConnectionStrings["LinkedUConnectionString"].ConnectionString;
            using (SqlConnection dbConnection = new SqlConnection(connectionString))
            {

                dbConnection.Open();          
                String invalidUni = "Error, Please enter the ID of a valid University";
                string univquery = "SELECT UNITID, INSTNM from universities WHERE UNITID = @UNITID";
                SqlCommand command = new SqlCommand(univquery, dbConnection);
                SqlDataAdapter da = new SqlDataAdapter(univquery, dbConnection);
                SqlDataReader dataReader;
                DataTable dt = new DataTable();
                da.SelectCommand.Parameters.AddWithValue("@UNITID", UNITID);
                da.Fill(dt);
                command.Parameters.AddWithValue("@UNITID", UNITID);
                dataReader = command.ExecuteReader();
                if (dt.Rows.Count == 0)
                {
                    return invalidUni;
                    lblResult.Text = UniversityName;
                }
                else
                {
                    while (dataReader.Read())
                    {
                        UniversityID = dataReader.GetInt32(0);
                        UniversityName = dataReader.GetString(1);
                        lblResult.Text = UniversityName;
                        InsertData(UniversityID, UniversityName);
                        // String stockPrice1 = dataReader.GetString(3);

                    }
                    return UniversityID + " " + UniversityName + " ";
                }
            }

        }
        public void InsertData(int UNITID, String UniversityName)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["LinkedUConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("INSERT INTO MarkedUni (UniversityID, UniversityName) VALUES (@UNITID, @UniversityName)"))
                {
                    cmd.Parameters.AddWithValue("@UNITID", UNITID);
                    cmd.Parameters.AddWithValue("@UniversityName", UniversityName);
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
           
        }
        public void DeleteData(int UNITID)
        {
            int UniversityID = UNITID;
            string connectionString = ConfigurationManager.ConnectionStrings["LinkedUConnectionString"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("DELETE FROM MarkedUni WHERE UniversityID = UniversityID")) 
                {
                    cmd.Parameters.AddWithValue("@UniversityID", UniversityID);
                   
                    cmd.Connection = con;
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }

        }

        protected void MultiView1_ActiveViewChanged(object sender, EventArgs e)
        {

        }
    }
    }

    

        
