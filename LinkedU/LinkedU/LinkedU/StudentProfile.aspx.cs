using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LinkedU.DistanceFinder;
using System.Configuration;
using System.Data.SqlClient;

namespace LinkedU
{
    public partial class StudentProfile : System.Web.UI.Page
    {

        string connectionString = ConfigurationManager.ConnectionStrings["LinkedUConnectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["UserID"] == null)
                Response.Redirect("Sign-In.aspx");

            if (Session["AccountType"].ToString() == "University")
                Response.Redirect("UniversityProfile.aspx");

            if (!IsPostBack)
                GetStudentProfile();

            SummaryAddress.Text = HiddenFieldAddressFormatted.Value;
            SummaryAge.Text = TextBoxAge.Text;

            if (RadioButtonGender.SelectedItem != null)
                SummaryGender.Text = RadioButtonGender.SelectedItem.Text;

            if (RadioButtonRace.SelectedItem != null)
                SummaryRace.Text = RadioButtonRace.SelectedItem.Text;

            if (TextBoxGpa.Text.Length > 0)
            {
                PanelSummaryGpa.Visible = true;
                SummaryGpa.Text = TextBoxGpa.Text;
            }
            if (TextBoxHighSchool.Text.Length > 0)
            {
                PanelSummaryHighSchool.Visible = true;
                SummaryHighSchool.Text = TextBoxHighSchool.Text;
            }
            if (TextBoxGraduationYear.Text.Length > 0)
            {
                PanelSummaryGraduationYear.Visible = true;
                SummaryGraduationYear.Text = TextBoxGraduationYear.Text;
            }

            if (TextBoxActScore.Text.Length > 0)
            {
                PanelSummaryAct.Visible = true;
                SummaryAct.Text = TextBoxActScore.Text;
            }
            if (TextBoxSatScore.Text.Length > 0)
            {
                PanelSummarySat.Visible = true;
                SummarySat.Text = TextBoxSatScore.Text;
            }
            if (TextBoxPsat.Text.Length > 0)
            {
                PanelSummaryPsat.Visible = true;
                SummaryPsat.Text = TextBoxPsat.Text;
            }
            if (TextBoxPsatNmsqt.Text.Length > 0)
            {
                PanelSummaryPsatnsmqt.Visible = true;
                SummaryPsatnsmqt.Text = TextBoxPsatNmsqt.Text;
            }
            if (TextBoxGreVerbal.Text.Length > 0)
            {
                PanelSummaryGreV.Visible = true;
                SummaryGreV.Text = TextBoxGreVerbal.Text;
            }
            if (TextBoxGreQuantitative.Text.Length > 0)
            {
                PanelSummaryGreQ.Visible = true;
                SummaryGreQ.Text = TextBoxGreQuantitative.Text;
            }
            if (TextBoxGreWritten.Text.Length > 0)
            {
                PanelSummaryGreW.Visible = true;
                SummaryGreW.Text = TextBoxGreWritten.Text;
            }
            if (TextBoxLsat.Text.Length > 0)
            {
                PanelSummaryLsat.Visible = true;
                SummaryLsat.Text = TextBoxLsat.Text;
            }
            if (TextBoxMcat.Text.Length > 0)
            {
                PanelSummaryMcat.Visible = true;
                SummaryMcat.Text = TextBoxMcat.Text;
            }

            if (CheckBoxNewsletter.Checked)
                SummaryNewsletter.Text = "Yes";
            else
                SummaryNewsletter.Text = "No";

            List<ExtraCurricularData> ecs = GetExtraCurriculars();
            if (ecs.Count > 0)
            {
                PanelSummaryExtraCurriculars.Visible = true;
                foreach (ExtraCurricularData ec in ecs)
                {
                    TableCell ecname = new TableCell()
                    {
                        Text = ec.Name
                    };
                    TableCell ectype = new TableCell()
                    {
                        Text = ec.TypeName
                    };
                    TableRow ecrow = new TableRow();
                    ecrow.Cells.Add(ecname);
                    ecrow.Cells.Add(ectype);

                    SummaryExtraCurriculars.Rows.Add(ecrow);
                }
            }

            SummaryPersonalStatement.Text = TextBoxPersonalStatement.Text;
        }

        protected void Wizard1_NextButtonClick(object sender, WizardNavigationEventArgs e)
        {
            Page.Validate();

            if (!Page.IsValid)
            {
                e.Cancel = true;
                return;
            }

            if ((e.CurrentStepIndex == 1) && (HiddenFieldAddressFormatted.Value.Length == 0))
            {
                using (DistanceFinderSoapClient distance = new DistanceFinderSoapClient())
                {
                    try
                    {
                        Geolocation geo = distance.GetLocation(TextBoxAddress1.Text, TextBoxCity.Text, TextBoxState.Text, TextBoxZipCode.Text);

                        HiddenFieldAddressLatitude.Value = geo.Latitude.ToString();
                        HiddenFieldAddressLongitude.Value = geo.Longitude.ToString();
                        HiddenFieldAddressFormatted.Value = geo.FormattedAddress;
                    }
                    catch
                    {
                        e.Cancel = true;
                    }
                }

            }
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
                            comm.CommandText = "DELETE FROM student_profiles WHERE userID = @userID";
                            comm.ExecuteNonQuery();

                            comm.CommandText = "INSERT INTO student_profiles (userID, age, gender, race, address1, address2, city, state, zipcode, formatted_address, latitude, longitude, highschool, gpa, graduationyear, newsletter, personalStatement) VALUES (@userID, @age, @gender, @race, @address1, @address2, @city, @state, @zipcode, @formatted_address, @latitude, @longitude, @highschool, @gpa, @graduationyear, @newsletter, @personalStatement)";
                            comm.Parameters.AddWithValue("@age", int.Parse(TextBoxAge.Text));
                            comm.Parameters.AddWithValue("@gender", RadioButtonGender.SelectedValue);
                            comm.Parameters.AddWithValue("@race", RadioButtonRace.SelectedValue);
                            comm.Parameters.AddWithValue("@address1", TextBoxAddress1.Text);
                            comm.Parameters.AddWithValue("@address2", TextBoxAddress2.Text);
                            comm.Parameters.AddWithValue("@city", TextBoxCity.Text);
                            comm.Parameters.AddWithValue("@state", TextBoxState.Text);
                            comm.Parameters.AddWithValue("@zipcode", TextBoxZipCode.Text);
                            comm.Parameters.AddWithValue("@formatted_address", HiddenFieldAddressFormatted.Value);
                            comm.Parameters.AddWithValue("@latitude", float.Parse(HiddenFieldAddressLatitude.Value));
                            comm.Parameters.AddWithValue("@longitude", float.Parse(HiddenFieldAddressLongitude.Value));
                            comm.Parameters.AddWithValue("@highschool", TextBoxHighSchool.Text);
                            comm.Parameters.AddWithValue("@graduationyear", TextBoxGraduationYear.Text);
                            comm.Parameters.AddWithValue("@newsletter", CheckBoxNewsletter.Checked);
                            comm.Parameters.AddWithValue("@personalStatement", TextBoxPersonalStatement.Text);
                            comm.Parameters.Add("@gpa", System.Data.SqlDbType.Float);

                            if (TextBoxGpa.Text.Length > 0)
                                comm.Parameters["@gpa"].Value = float.Parse(TextBoxGpa.Text);

                            comm.ExecuteNonQuery();


                            comm.Parameters.Clear();
                            comm.Parameters.AddWithValue("@userID", Session["UserID"]);
                            comm.CommandText = "DELETE FROM student_test_scores WHERE userID = @userID";
                            comm.ExecuteNonQuery();

                            comm.Parameters.Add("@score", System.Data.SqlDbType.Float);
                            comm.Parameters.Add("@type", System.Data.SqlDbType.NVarChar, 100);
                            comm.CommandText = "INSERT INTO student_test_scores (userID, test_score, test_type) " +
                                "SELECT @userID, @score, [id] FROM student_test_types WHERE [name] = @type";
                            if (TextBoxActScore.Text.Length > 0)
                            {
                                comm.Parameters["@score"].Value = TextBoxActScore.Text;
                                comm.Parameters["@type"].Value = "ACT";
                                comm.ExecuteNonQuery();
                            }
                            if (TextBoxSatScore.Text.Length > 0)
                            {
                                comm.Parameters["@score"].Value = TextBoxSatScore.Text;
                                comm.Parameters["@type"].Value = "SAT";
                                comm.ExecuteNonQuery();
                            }
                            if (TextBoxPsat.Text.Length > 0)
                            {
                                comm.Parameters["@score"].Value = TextBoxPsat.Text;
                                comm.Parameters["@type"].Value = "PSAT";
                                comm.ExecuteNonQuery();
                            }
                            if (TextBoxPsatNmsqt.Text.Length > 0)
                            {
                                comm.Parameters["@score"].Value = TextBoxPsatNmsqt.Text;
                                comm.Parameters["@type"].Value = "PSAT/NMSQT";
                                comm.ExecuteNonQuery();
                            }
                            if (TextBoxGreVerbal.Text.Length > 0)
                            {
                                comm.Parameters["@score"].Value = TextBoxGreVerbal.Text;
                                comm.Parameters["@type"].Value = "GRE-V";
                                comm.ExecuteNonQuery();
                            }
                            if (TextBoxGreQuantitative.Text.Length > 0)
                            {
                                comm.Parameters["@score"].Value = TextBoxGreQuantitative.Text;
                                comm.Parameters["@type"].Value = "GRE-Q";
                                comm.ExecuteNonQuery();
                            }
                            if (TextBoxGreWritten.Text.Length > 0)
                            {
                                comm.Parameters["@score"].Value = TextBoxGreWritten.Text;
                                comm.Parameters["@type"].Value = "GRE-W";
                                comm.ExecuteNonQuery();
                            }
                            if (TextBoxLsat.Text.Length > 0)
                            {
                                comm.Parameters["@score"].Value = TextBoxLsat.Text;
                                comm.Parameters["@type"].Value = "LSAT";
                                comm.ExecuteNonQuery();
                            }
                            if (TextBoxMcat.Text.Length > 0)
                            {
                                comm.Parameters["@score"].Value = TextBoxMcat.Text;
                                comm.Parameters["@type"].Value = "MCAT";
                                comm.ExecuteNonQuery();
                            }


                            comm.Parameters.Clear();
                            comm.Parameters.AddWithValue("@userID", Session["UserID"]);
                            comm.CommandText = "DELETE FROM student_extracurriculars WHERE userID = @userID";
                            comm.ExecuteNonQuery();

                            comm.Parameters.Add("@ecname", System.Data.SqlDbType.NVarChar, 100);
                            comm.Parameters.Add("@ectype", System.Data.SqlDbType.Int);
                            comm.CommandText = "INSERT INTO student_extracurriculars (userID, ec_name, ec_type) VALUES (@userID, @ecname, @ectype)";
                            List<ExtraCurricularData> ecs = GetExtraCurriculars();
                            if (ecs.Count > 0)
                            {
                                foreach (ExtraCurricularData ec in ecs)
                                {
                                    comm.Parameters["@ecname"].Value = ec.Name;
                                    comm.Parameters["@ectype"].Value = ec.Type;
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

        private void GetStudentProfile()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    using (SqlCommand comm = conn.CreateCommand())
                    {

                        comm.Parameters.AddWithValue("@userID", Session["UserID"]);
                        comm.CommandText = "SELECT age, gender, race, address1, address2, city, state, zipcode, formatted_address, latitude, longitude, highschool, gpa, graduationyear, newsletter, personalStatement FROM student_profiles WHERE userID = @userID";

                        using (SqlDataReader reader = comm.ExecuteReader())
                        {
                            if (reader != null && reader.HasRows && reader.Read())
                            {
                                if (!reader.IsDBNull(0))
                                    TextBoxAge.Text = reader.GetInt32(0).ToString();

                                if (!reader.IsDBNull(1))
                                    RadioButtonGender.SelectedValue = reader.GetString(1);

                                if (!reader.IsDBNull(2))
                                    RadioButtonRace.SelectedValue = reader.GetString(2);

                                if (!reader.IsDBNull(3))
                                    TextBoxAddress1.Text = reader.GetString(3);
                                if (!reader.IsDBNull(4))
                                    TextBoxAddress2.Text = reader.GetString(4);
                                if (!reader.IsDBNull(5))
                                    TextBoxCity.Text = reader.GetString(5);
                                if (!reader.IsDBNull(6))
                                    TextBoxState.Text = reader.GetString(6);
                                if (!reader.IsDBNull(7))
                                    TextBoxZipCode.Text = reader.GetString(7);
                                if (!reader.IsDBNull(8))
                                    HiddenFieldAddressFormatted.Value = reader.GetString(8);

                                if (!reader.IsDBNull(9))
                                    HiddenFieldAddressLatitude.Value = reader.GetDouble(9).ToString();
                                if (!reader.IsDBNull(10))
                                    HiddenFieldAddressLongitude.Value = reader.GetDouble(10).ToString();

                                if (!reader.IsDBNull(11))
                                    TextBoxHighSchool.Text = reader.GetString(11);

                                if (!reader.IsDBNull(12))
                                    TextBoxGpa.Text = Math.Round(reader.GetDouble(12), 2).ToString();

                                if (!reader.IsDBNull(13))
                                    TextBoxGraduationYear.Text = reader.GetInt32(13).ToString();

                                if (!reader.IsDBNull(14))
                                    CheckBoxNewsletter.Checked = reader.GetBoolean(14);

                                if (!reader.IsDBNull(15))
                                    TextBoxPersonalStatement.Text = reader.GetString(15);
                            }
                        }

                        comm.CommandText = "SELECT test_score, name FROM student_test_scores INNER JOIN student_test_types ON student_test_scores.test_type = student_test_types.id WHERE userID = @userID";
                        using (SqlDataReader reader = comm.ExecuteReader())
                        {
                            if (reader != null && reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    switch (reader.GetString(1))
                                    {
                                        case "ACT":
                                            TextBoxActScore.Text = reader.GetDouble(0).ToString();
                                            break;
                                        case "SAT":
                                            TextBoxSatScore.Text = reader.GetDouble(0).ToString();
                                            break;
                                        case "GRE-V":
                                            TextBoxGreVerbal.Text = reader.GetDouble(0).ToString();
                                            break;
                                        case "GRE-Q":
                                            TextBoxGreQuantitative.Text = reader.GetDouble(0).ToString();
                                            break;
                                        case "GRE-W":
                                            TextBoxGreWritten.Text = reader.GetDouble(0).ToString();
                                            break;
                                        case "PSAT":
                                            TextBoxPsat.Text = reader.GetDouble(0).ToString();
                                            break;
                                        case "PSAT/NMSQT":
                                            TextBoxPsatNmsqt.Text = reader.GetDouble(0).ToString();
                                            break;
                                        case "LSAT":
                                            TextBoxLsat.Text = reader.GetDouble(0).ToString();
                                            break;
                                        case "MCAT":
                                            TextBoxMcat.Text = reader.GetDouble(0).ToString();
                                            break;
                                    }
                                }
                            }
                        }

                        comm.CommandText = "select ec_name, ec_type from student_extracurriculars WHERE userID = @userID";
                        using (SqlDataReader reader = comm.ExecuteReader())
                        {
                            if (reader != null && reader.HasRows)
                            {
                                List<ExtraCurricularData> list = new List<ExtraCurricularData>();

                                while (reader.Read())
                                {
                                    ExtraCurricularData ec = new ExtraCurricularData()
                                    {
                                        Name = reader.GetString(0),
                                        Type = reader.GetInt32(1)
                                    };
                                    list.Add(ec);
                                }

                                ExtraCurriculars.DataSource = list;
                                ExtraCurriculars.DataBind();
                            }
                        }

                        comm.CommandText = "select student_files.ID, [file_name], [name]  from student_files " +
                            "inner join student_file_types ON student_files.file_type = student_file_types.id WHERE userID = @userID";
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
            catch
            {

            }
        }

        protected void ButtonAddExtraCurricular_Click(object sender, EventArgs e)
        {
            List<ExtraCurricularData> data = GetExtraCurriculars();
            ExtraCurricularData newData = new ExtraCurricularData();

            data.Add(newData);
            ExtraCurriculars.DataSource = data;
            ExtraCurriculars.DataBind();
        }


        private List<ExtraCurricularData> GetExtraCurriculars()
        {
            List<ExtraCurricularData> list = new List<ExtraCurricularData>();
            foreach (RepeaterItem item in ExtraCurriculars.Items)
            {

                ExtraCurricular uc = (ExtraCurricular)item.FindControl("extraCurricular");
                if (uc != null && uc.Data.Type != 0)
                    list.Add(uc.Data);
            }
            return list;
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

        protected void TextBoxAddress1_TextChanged(object sender, EventArgs e)
        {
            HiddenFieldAddressLatitude.Value = null;
            HiddenFieldAddressLongitude.Value = null;
            HiddenFieldAddressFormatted.Value = null;
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

                                comm.CommandText = "DELETE FROM student_files WHERE userID = @userID AND ID = @fileID";
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

                        if (DropDownListMediaType.Text == "Profile Picture")
                        {
                            //Change any existing profile picture type files for this user to type Image

                            comm.CommandText = "UPDATE student_files " +
                                "SET file_type = (SELECT id FROM student_file_types WHERE name = 'Image') " +
                                "WHERE file_type = (SELECT id FROM student_file_Types WHERE name = 'Profile Picture') " +
                                "AND userID = @userID";
                            comm.ExecuteNonQuery();

                            foreach(UploadMediaData m in data)
                            {
                                if (m.Type == "Profile Picture")
                                    m.Type = "Image";
                            }
                        }

                        comm.CommandText = "INSERT INTO student_files (userID, file_name, file_type, [file]) " +
                            "OUTPUT Inserted.ID " +
                            "SELECT @userID, @file_name, id, @file FROM student_file_types WHERE name = @file_type";

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



            } catch
            {

            }
        }
    }
}