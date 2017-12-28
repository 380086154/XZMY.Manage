<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestDataDictionary.aspx.cs" Inherits="XZMY.Manage.Web.TestDataDictionary" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <title></title>
    <meta http-equiv="Content-Type" content="text/html;charset=utf-8"/>
    <script type="text/javascript" charset="utf-8" src="/Content/Custom/UEditor/ueditor.config.js"></script>
                                                                         
    <script type="text/javascript" charset="utf-8" src="/Content/Custom/UEditor/ueditor.all.min.js"> </script>
    <script type="text/javascript" charset="utf-8" src="/Content/Custom/UEditor/lang/zh-cn/zh-cn.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <input type="checkbox" id="1" checked=""/>
    </div>
        <asp:Button Id="Button1" runat="server" OnClick="Button1_Click" Text="Button" />
        <div>
   
                                                        111111111111111111111
    
            <asp:Button Id="btnPlan" runat="server"  Text="创建一个规划" OnClick="btnPlan_Click" />
    
            </div>

        <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="重新规划活动" />
    </form>
</body>
</html>








