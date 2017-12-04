using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LinkedU
{

    public class HighlightedProgramData
    {
        public string Type { get; set; }
        public string Name { get; set; }
        public string URL { get; set; }
    }
    public partial class HighlightedProgram : System.Web.UI.UserControl
    {
        public HighlightedProgramData Data
        {
            get
            {
                return new HighlightedProgramData()
                {
                    Type = ProgramType.SelectedValue,
                    Name = ProgramName.Text,
                    URL = ProgramURL.Text
                };
            }
            set
            {
                ProgramType.SelectedValue = value.Type;
                ProgramName.Text = value.Name;
                ProgramURL.Text = value.URL;
            }
        }
    }
}