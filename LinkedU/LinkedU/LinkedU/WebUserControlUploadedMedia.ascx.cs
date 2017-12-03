using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LinkedU
{

    public class UploadMediaData
    {
        public string Type { get; set; }
        public string Name { get; set; }
        public string ID { get; set; }

    }

    public partial class UploadMedia : System.Web.UI.UserControl
    {

        public UploadMediaData Data
        {
            get
            {
                return new UploadMediaData()
                {
                    Type = LabelMediaType.Text,
                    Name = LabelMediaName.Text,
                    ID = MediaID.Value
                };
            }

            set
            {
                LabelMediaType.Text = value.Type;
                LabelMediaName.Text = value.Name;
                MediaID.Value = value.ID;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

    }
}