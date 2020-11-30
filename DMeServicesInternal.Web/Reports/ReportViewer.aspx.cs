using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DMeServicesInternal.Web.Reports
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadReport();
            }
        }

        private void LoadReport()
        {
            ReportWiewer1.LocalReport.ReportPath = Server.MapPath("~/"+"Reports//rpt//CalculateFees.rdlc");
            ReportWiewer1.LocalReport.Refresh();
        }
    }
}