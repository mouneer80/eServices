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
using System.IO;

namespace DMeServicesInternal.Web.Reports
{
    public partial class Report : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                // Set the processing mode for the ReportViewer to Remote  
                //ReportViewer1.ProcessingMode = ProcessingMode.Remote;

                //ServerReport serverReport = ReportViewer1.ServerReport;

                // Set the report server URL and report path  
                //serverReport.ReportServerUrl =
                //    new Uri("https://<Server Name>/reportserver");
                //serverReport.ReportPath =
                //    "/AdventureWorks Sample Reports/Sales Order Detail";

                // Create the sales order number report parameter  
                //ReportParameter salesOrderNumber = new ReportParameter();
                //salesOrderNumber.Name = "SalesOrderNumber";
                //salesOrderNumber.Values.Add("SO43661");

                // Set the report parameters for the report  
                //ReportViewer1.ServerReport.SetParameters(
                    //new ReportParameter[] { salesOrderNumber });
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string id = Request.QueryString["id"];
                string civilid = Request.QueryString["civilid"];
                LoadReport(id, civilid);

            }
        }

        private void LoadReport(string id, string civilid)
        {
            InternalEngineeringDataSetTableAdapters.BldPermitsTableAdapter ta = new InternalEngineeringDataSetTableAdapters.BldPermitsTableAdapter();
            InternalEngineeringDataSet.BldPermitsDataTable dt = new InternalEngineeringDataSet.BldPermitsDataTable();
            ta.Fill(dt, Convert.ToInt32(id));
            ReportDataSource rds = new ReportDataSource();
            rds.Name = "Permits";
            rds.Value = dt;

            ReportViewer1.ProcessingMode = ProcessingMode.Local;
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/" + "Reports//rpt//PrintPermit.rdlc");
            //eServicesEntities entities = new eServicesEntities();
            //ReportDataSource datasource = new ReportDataSource("Permits", entities.BldPermits);
            
            ReportViewer1.LocalReport.DataSources.Clear();
            
            ReportParameter parameter = new ReportParameter("ID", id);
            ReportViewer1.LocalReport.SetParameters(parameter);
            ReportViewer1.LocalReport.DataSources.Add(rds);
            string deviceInfo = "<DeviceInfo>" +
                    "  <OutputFormat>PDF</OutputFormat>" +
                    "  <PageWidth>11.69in</PageWidth>" +
                    "  <PageHeight>8.27in</PageHeight>" +
                    "  <MarginTop>0in</MarginTop>" +
                    "  <MarginLeft>0in</MarginLeft>" +
                    "  <MarginRight>0in</MarginRight>" +
                    "  <MarginBottom>0in</MarginBottom>" +
                    "  <EmbedFonts>None</EmbedFonts>" +
                    "</DeviceInfo>";
            Warning[] warnings;
            string[] streamIds;
            string mimeType = string.Empty;
            string encoding = string.Empty;
            string extension = string.Empty;

            byte[] bytes = ReportViewer1.LocalReport.Render(
                "PDF",
                deviceInfo,
                out mimeType,
                out encoding,
                out extension,
                out streamIds,
                out warnings);
            var tempPath = System.Web.HttpContext.Current.Server.MapPath("~/Images/PrintedFiles/" + civilid);
            if (!Directory.Exists(tempPath))
            {
                Directory.CreateDirectory(tempPath);
            }
            //oFile.SaveAs(StrUploadPath);
            var saveAs = string.Format("{0}.pdf", Path.Combine(tempPath, civilid + "_" + id));

            var idx = 0;
            while (File.Exists(saveAs))
            {
                idx++;
                saveAs = string.Format("{0}_{1}.pdf", Path.Combine(tempPath, civilid + "_" + id), idx);
            }

            using (var stream = new FileStream(saveAs, FileMode.Create, FileAccess.Write))
            {
                stream.Write(bytes, 0, bytes.Length);
                stream.Close();
            }
            //Response.Buffer = true;
            //Response.Clear();
            //Response.ContentType = mimeType;
            //Response.AddHeader(
            //    "content-disposition",
            //    "attachment; filename= filename" + "." + extension);
            //Response.OutputStream.Write(bytes, 0, bytes.Length); // create the file  
            //Response.Flush(); // send it to the client to download  
            //Response.End();

            //ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/"+"Reports//rpt//PrintPermit.rdlc");
            ReportViewer1.LocalReport.Refresh();
            ReportViewer1.ZoomMode = ZoomMode.PageWidth;
        }
    }
}