<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MediaPanelDataSubmit.aspx.cs" Inherits="tnm.net.services.MediaPanelDataSubmit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <title>Download Manager powered by Synaptic Digital</title>
</head>
<body>
    <form id="DataForm" method="post" action="<%=dlm %>">
        <input type="hidden" name="ASSET_GUID" value="<%=asset_guid %>" />
        <input type="hidden" name="DLMURL" value="<%=dlmurl %>" />
        <input type="hidden" name="FILE_TYPE_ID" value="<%=file_type_id %>" />
        <input type="hidden" name="FILE_ID" value="<%=file_id %>" />
        <input type="hidden" name="SITE_ID" value="<%=site_id %>" />
        <input type="hidden" name="HOSTNAME" value="<%=hostname %>" />        
    </form>
    <script type="text/javascript">
        document.getElementById("DataForm").submit();
    </script>
</body>
</html>