<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportViewer.aspx.cs" Inherits="DMeServicesInternal.Web.Reports.WebForm1" %>

<%@ register assembly="Microsoft.ReportViewer.WebForms" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:ScriptManager id="scriptManager1" runat="server"></asp:ScriptManager>
            <rsweb:reportviewer id="ReportWiewer1" runat="server"></rsweb:reportviewer>
        </div>
    </form>
</body>
</html>
