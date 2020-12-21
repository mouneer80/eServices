using Microsoft.Reporting.WebForms;
using DMeServicesInternal;
using DMeServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DMeServices.Models.BuildingServices;
using DMeServices.Models.ViewModels.Internal.Permits;
using DMeServices.DAL;

namespace DMeServicesInternal.Web.Reports
{
    public partial class Report : System.Web.UI.Page
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
            ReportViewer1.ProcessingMode = ProcessingMode.Local;
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/" + "Reports//rpt//PrintPermit.rdlc");
            eServicesEntities entities = new eServicesEntities();
            ReportDataSource datasource = new ReportDataSource("Permits", entities.BldPermits);
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(datasource);
            ReportParameter parameter = new ReportParameter("ID", "10121");
            ReportViewer1.LocalReport.SetParameters(parameter);

            //ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/"+"Reports//rpt//PrintPermit.rdlc");
            ReportViewer1.LocalReport.Refresh();
            ReportViewer1.ZoomMode = ZoomMode.FullPage;
        }
    }
}