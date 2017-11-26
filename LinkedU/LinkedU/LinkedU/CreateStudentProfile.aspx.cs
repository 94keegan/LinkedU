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
    public partial class CreateStudentProfile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
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
            string connectionString = ConfigurationManager.ConnectionStrings["LinkedUConnectionString"].ConnectionString;
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

                            comm.CommandText = "INSERT INTO student_profiles (userID, age, gender, race, address1, address2, city, state, zipcode, formatted_address, latitude, longitude, highschool, gpa) VALUES (@userID, @age, @gender, @race, @address1, @address2, @city, @state, @zipcode, @formatted_address, @latitude, @longitude, @highschool, @gpa)";
                            comm.Parameters.AddWithValue("@userID", Session["UserID"]);
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
                            comm.Parameters.AddWithValue("@gpa", float.Parse(TextBoxGpa.Text));
                            comm.ExecuteNonQuery();


                            comm.Parameters.Clear();
                            comm.Parameters.AddWithValue("@userID", Session["UserID"]);
                            comm.Parameters.Add("@score", System.Data.SqlDbType.Float);
                            comm.Parameters.Add("@type", System.Data.SqlDbType.Int);
                            comm.CommandText = "INSERT INTO student_test_scores (userID, test_score, test_type) VALUES (@userID, @score, @type)";
                            if (TextBoxActScore.Text.Length > 0)
                            {
                                comm.Parameters["@score"].Value = TextBoxActScore.Text;
                                comm.Parameters["@type"].Value = 9;
                                comm.ExecuteNonQuery();   
                            }
                            if (TextBoxSatScore.Text.Length > 0)
                            {
                                comm.Parameters["@score"].Value = TextBoxSatScore.Text;
                                comm.Parameters["@type"].Value = 8;
                                comm.ExecuteNonQuery();
                            }
                            if (TextBoxPsat.Text.Length > 0)
                            {
                                comm.Parameters["@score"].Value = TextBoxPsat.Text;
                                comm.Parameters["@type"].Value = 13;
                                comm.ExecuteNonQuery();
                            }
                            if (TextBoxPsatNmsqt.Text.Length > 0)
                            {
                                comm.Parameters["@score"].Value = TextBoxPsatNmsqt.Text;
                                comm.Parameters["@type"].Value = 14;
                                comm.ExecuteNonQuery();
                            }
                            if (TextBoxGreVerbal.Text.Length > 0)
                            {
                                comm.Parameters["@score"].Value = TextBoxGreVerbal.Text;
                                comm.Parameters["@type"].Value = 10;
                                comm.ExecuteNonQuery();
                            }
                            if (TextBoxGreQuantitative.Text.Length > 0)
                            {
                                comm.Parameters["@score"].Value = TextBoxGreQuantitative.Text;
                                comm.Parameters["@type"].Value = 11;
                                comm.ExecuteNonQuery();
                            }
                            if (TextBoxGreWritten.Text.Length > 0)
                            {
                                comm.Parameters["@score"].Value = TextBoxGreWritten.Text;
                                comm.Parameters["@type"].Value = 12;
                                comm.ExecuteNonQuery();
                            }
                            if (TextBoxLsat.Text.Length > 0)
                            {
                                comm.Parameters["@score"].Value = TextBoxLsat.Text;
                                comm.Parameters["@type"].Value = 15;
                                comm.ExecuteNonQuery();
                            }
                            if (TextBoxMcat.Text.Length > 0)
                            {
                                comm.Parameters["@score"].Value = TextBoxMcat.Text;
                                comm.Parameters["@type"].Value = 16;
                                comm.ExecuteNonQuery();
                            }


                            comm.Parameters.Clear();
                            comm.Parameters.AddWithValue("@userID", Session["UserID"]);
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
                        catch
                        {
                            transaction.Rollback();
                        }
                    }
                }
                catch
                {
                    
                }
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

        protected void TextBoxAddress1_TextChanged(object sender, EventArgs e)
        {
            HiddenFieldAddressLatitude.Value = null;
            HiddenFieldAddressLongitude.Value = null;
            HiddenFieldAddressFormatted.Value = null;
        }
    }
}