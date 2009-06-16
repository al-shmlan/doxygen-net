<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="_Default" %>

<%@ Register Assembly="Ra" Namespace="Ra.Widgets" TagPrefix="ra" %>
<%@ Register Assembly="Ra.Extensions" Namespace="Ra.Extensions.Widgets" TagPrefix="ra" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <ra:Panel ID="typesPanel" runat="server" style="float:left;width:25%;" />
        <ra:Dynamic runat="server" ID="members" style="float:left;width:25%;" OnReload="members_Reload" />
        <ra:Panel runat="server" ID="parameters" style="float:left;width:25%;" />
        <br style="clear:left;" />
        <ra:Label runat="server" ID="descriptionLabel" Tag="p" />
    </div>
    </form>
</body>
</html>
