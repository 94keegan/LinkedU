using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LinkedU
{

    public class ExtraCurricularData
    {
        public int Type { get; set; }
        public string Name { get; set; }
        public string TypeName { get; set; }

    }
    public partial class ExtraCurricular : System.Web.UI.UserControl
    {
        public ExtraCurricularData Data
        {
            get
            {
                return new ExtraCurricularData()
                {
                    Type = int.Parse(ectype.SelectedValue),
                    Name = ecname.Text,
                    TypeName = ectype.SelectedItem.Text
                };
            }
            set
            {
                if (value.Type >= 1)
                    ectype.SelectedValue = value.Type.ToString();
                ecname.Text = value.Name;
            }
        }

    }
}