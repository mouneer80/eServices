<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Report.aspx.cs" Inherits="DMeServicesInternal.Web.Reports.Report" %>

<%@ register assembly="Microsoft.ReportViewer.WebForms" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div style="width:58%; text-align:center;">
            <asp:ScriptManager id="scriptManager1" runat="server"></asp:ScriptManager>
            <rsweb:reportviewer id="ReportViewer1" runat="server" AsyncRendering="false" ProcessingMode="Remote" SizeToReportContent="true"></rsweb:reportviewer>
        </div>
    </form>
</body>
</html>
