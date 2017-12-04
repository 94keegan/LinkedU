using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LinkedU
{
    public partial class UniversityProfile : System.Web.UI.Page
    {

        string connectionString = ConfigurationManager.ConnectionStrings["LinkedUConnectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] == null)
                Response.Redirect("Sign-In.aspx");

            if (Session["AccountType"].ToString() == "Student")
                Response.Redirect("StudentProfile.aspx");

            if (!IsPostBack)
                GetUniversityProfile();

            if (CheckBoxNewsletter.Checked)
                SummaryNewsletter.Text = "Yes";
            else
                SummaryNewsletter.Text = "No";

            List<HighlightedProgramData> hps = GetHighlightedPrograms();
            if (hps.Count > 0)
            {
                PanelSummaryPrograms.Visible = true;
                foreach (HighlightedProgramData hp in hps)
                {
                    TableCell programName = new TableCell()
                    {
                        Text = hp.Name
                    };
                    TableCell programType = new TableCell()
                    {
                        Text = hp.Type
                    };
                    TableCell programUrl = new TableCell()
                    {
                        Text = hp.URL
                    };
                    TableRow hprow = new TableRow();
                    hprow.Cells.Add(programName);
                    hprow.Cells.Add(programType);
                    hprow.Cells.Add(programUrl);

                    SummaryPrograms.Rows.Add(hprow);
                }
            }


        }


        private void GetUniversityProfile()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    using (SqlCommand comm = conn.CreateCommand())
                    {

                        comm.Parameters.AddWithValue("@userID", Session["UserID"]);
                        comm.CommandText = "SELECT newsletter FROM university_profiles WHERE universityID = @userID";

                        using (SqlDataReader reader = comm.ExecuteReader())
                        {
                            if (reader != null && reader.HasRows && reader.Read())
                            {
                               
                                if (!reader.IsDBNull(0))
                                    CheckBoxNewsletter.Checked = reader.GetBoolean(0);

                            }
                        }
                        
                        comm.CommandText = "select program_name, [name], program_url from highlighted_programs " +
                            "INNER JOIN university_program_types ON highlighted_programs.program_type = university_program_types.id " +
                            "WHERE userID = @userID";
                        using (SqlDataReader reader = comm.ExecuteReader())
                        {
                            if (reader != null && reader.HasRows)
                            {
                                List<HighlightedProgramData> list = new List<HighlightedProgramData>();

                                while (reader.Read())
                                {
                                    HighlightedProgramData hp = new HighlightedProgramData()
                                    {
                                        Name = reader.GetString(0),
                                        Type = reader.GetString(1)
                                    };

                                    if (!reader.IsDBNull(2))
                                        hp.URL = reader.GetString(2);

                                    list.Add(hp);
                                }

                                RepeaterHighlightedPrograms.DataSource = list;
                                RepeaterHighlightedPrograms.DataBind();
                            }
                        }

                        comm.CommandText = "select university_files.ID, [file_name], [name]  from university_files " +
                            "inner join university_file_types ON university_files.file_type = university_file_types.id WHERE userID = @userID";
                        using (SqlDataReader reader = comm.ExecuteReader())
                        {
                            if (reader != null && reader.HasRows)
                            {
                                List<UploadMediaData> list = new List<UploadMediaData>();

                                while (reader.Read())
                                {
                                    UploadMediaData ec = new UploadMediaData()
                                    {
                                        ID = reader.GetInt32(0).ToString(),
                                        Name = reader.GetString(1),
                                        Type = reader.GetString(2)
                                    };
                                    list.Add(ec);
                                }

                                RepeaterUploadedMedia.DataSource = list;
                                RepeaterUploadedMedia.DataBind();
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {

            }
        }


        private List<UploadMediaData> GetUploadedMedia()
        {
            List<UploadMediaData> list = new List<UploadMediaData>();
            foreach (RepeaterItem item in RepeaterUploadedMedia.Items)
            {

                UploadMedia uc = (UploadMedia)item.FindControl("uploadedMedia");
                if (uc != null && uc.Data.Type.Length > 0)
                    list.Add(uc.Data);
            }
            return list;
        }


        private List<HighlightedProgramData> GetHighlightedPrograms()
        {
            List<HighlightedProgramData> list = new List<HighlightedProgramData>();
            foreach (RepeaterItem item in RepeaterHighlightedPrograms.Items)
            {

                HighlightedProgram uc = (HighlightedProgram)item.FindControl("highlightedProgram");
                if (uc != null && uc.Data.Type.Length > 0)
                    list.Add(uc.Data);
            }
            return list;
        }

        protected void ButtonAddProgram_Click(object sender, EventArgs e)
        {
            List<HighlightedProgramData> data = GetHighlightedPrograms();
            HighlightedProgramData newData = new HighlightedProgramData();

            data.Add(newData);
            RepeaterHighlightedPrograms.DataSource = data;
            RepeaterHighlightedPrograms.DataBind();
        }

        protected void RepeaterHighlightedPrograms_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Delete":

                    try
                    {

                        List<HighlightedProgramData> list = new List<HighlightedProgramData>();
                        foreach (RepeaterItem item in RepeaterHighlightedPrograms.Items)
                        {

                            HighlightedProgram uc = (HighlightedProgram)item.FindControl("extraCurricular");
                            if (uc != null && uc.Data.Name.Length > 0 && (uc.Data.Name != e.CommandArgument.ToString()))
                                list.Add(uc.Data);
                        }

                        RepeaterHighlightedPrograms.DataSource = list;
                        RepeaterHighlightedPrograms.DataBind();
                    }
                    catch
                    {

                    }
                    break;
            }
        }

        protected void RepeaterUploadedMedia_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Delete":

                    try
                    {
                        using (SqlConnection conn = new SqlConnection(connectionString))
                        {
                            conn.Open();

                            using (SqlCommand comm = conn.CreateCommand())
                            {
                                comm.Parameters.AddWithValue("@userID", Session["UserID"]);
                                comm.Parameters.AddWithValue("@fileID", int.Parse(e.CommandArgument.ToString()));

                                comm.CommandText = "DELETE FROM university_files WHERE userID = @userID AND ID = @fileID";
                                comm.ExecuteNonQuery();

                                List<UploadMediaData> list = new List<UploadMediaData>();
                                foreach (RepeaterItem item in RepeaterUploadedMedia.Items)
                                {

                                    UploadMedia uc = (UploadMedia)item.FindControl("uploadedMedia");
                                    if (uc != null && uc.Data.Type.Length > 0 && (uc.Data.ID != e.CommandArgument.ToString()))
                                        list.Add(uc.Data);
                                }

                                RepeaterUploadedMedia.DataSource = list;
                                RepeaterUploadedMedia.DataBind();

                            }
                        }
                    }
                    catch
                    {

                    }
                    break;
            }
        }

        protected void LinkButtonUploadMedia_Click(object sender, EventArgs e)
        {
            try
            {
                string newID;

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    using (SqlCommand comm = conn.CreateCommand())
                    {
                        comm.Parameters.AddWithValue("@userID", Session["UserID"]);

                        List<UploadMediaData> data = GetUploadedMedia();

                        if (DropDownListMediaType.Text == "Logo")
                        {
                            //Change any existing logo type files for this user to type Image

                            comm.CommandText = "UPDATE university_files " +
                                "SET file_type = (SELECT id FROM university_file_types WHERE name = 'Image') " +
                                "WHERE file_type = (SELECT id FROM university_file_Types WHERE name = 'Logo') " +
                                "AND userID = @userID";
                            comm.ExecuteNonQuery();

                            foreach (UploadMediaData m in data)
                            {
                                if (m.Type == "Logo")
                                    m.Type = "Image";
                            }
                        }

                        comm.CommandText = "INSERT INTO university_files (userID, file_name, file_type, [file]) " +
                            "OUTPUT Inserted.ID " +
                            "SELECT @userID, @file_name, id, @file FROM university_file_types WHERE name = @file_type";

                        comm.Parameters.AddWithValue("@file_name", FileUploadMedia.FileName);
                        comm.Parameters.AddWithValue("@file_type", DropDownListMediaType.Text);
                        comm.Parameters.Add("@file", System.Data.SqlDbType.VarBinary).Value = FileUploadMedia.FileBytes;

                        newID = comm.ExecuteScalar().ToString();

                        //add item to repeater

                        UploadMediaData newData = new UploadMediaData()
                        {
                            Name = FileUploadMedia.FileName,
                            Type = DropDownListMediaType.Text,
                            ID = newID
                        };

                        data.Add(newData);
                        RepeaterUploadedMedia.DataSource = data;
                        RepeaterUploadedMedia.DataBind();

                    }
                }



            }
            catch
            {

            }
        }

        protected void Wizard1_NextButtonClick(object sender, WizardNavigationEventArgs e)
        {

        }

        protected void Wizard1_FinishButtonClick(object sender, WizardNavigationEventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    SqlTransaction transaction = conn.BeginTransaction();

                    using (SqlCommand comm = conn.CreateCommand())
                    {
                        comm.Transaction = transaction;
                        try
                        {
                           
                            comm.Parameters.AddWithValue("@userID", Session["UserID"]);

                            comm.CommandText = "DELETE FROM university_profiles WHERE universityID = @userID";
                            comm.ExecuteNonQuery();

                            comm.Parameters.AddWithValue("@newsletter", CheckBoxNewsletter.Checked);
                            comm.CommandText = "INSERT INTO university_profiles (universityID, newsletter) VALUES (@userID, @newsletter)";

                            comm.CommandText = "DELETE FROM highlighted_programs WHERE userID = @userID";
                            comm.ExecuteNonQuery();

                            comm.Parameters.Add("@name", System.Data.SqlDbType.NVarChar, 100);
                            comm.Parameters.Add("@typename", System.Data.SqlDbType.NVarChar, 100);
                            comm.Parameters.Add("@url", System.Data.SqlDbType.NText);

                            comm.CommandText = "INSERT INTO highlighted_programs (userID, program_name, program_type, program_url) " +
                                "SELECT @userID, @name id, @url FROM university_program_types WHERE [name] = @typename";
                            List<HighlightedProgramData> hps = GetHighlightedPrograms();
                            if (hps.Count > 0)
                            {
                                foreach (HighlightedProgramData hp in hps)
                                {
                                    comm.Parameters["@name"].Value = hp.Name;
                                    comm.Parameters["@typename"].Value = hp.Type;
                                    comm.Parameters["@url"].Value = hp.URL;
                                    comm.ExecuteNonQuery();
                                }
                            }

                            transaction.Commit();

                            Response.Redirect("Default.aspx");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                            transaction.Rollback();
                        }
                    }
                }
                catch
                {

                }
            }
        }
    }
}