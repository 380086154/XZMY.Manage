<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="plan.aspx.cs" Inherits="XZMY.Manage.Web.plan" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>规划测试</title>
</head>
<body>
    <form id="form1" runat="server">
    <div><asp:Button ID="Button31" runat="server"  Text="测试规划" OnClick="Button31_Click"  />
    
    &nbsp;<asp:Button ID="Button32" runat="server" OnClick="Button32_Click" Text="规划年级" />
        <asp:Button ID="Button34" runat="server" OnClick="Button34_Click" Text="导出WORD" />
        <br />
        <asp:Button ID="Button33" runat="server" OnClick="Button33_Click" Text="规划活动" />
        <br />
        <br />
        <br />
        <br />
        <br />
        当前年级： <asp:DropDownList ID="ddlGrade" runat="server">
            <asp:ListItem Value="7" >小学一年级</asp:ListItem>
            <asp:ListItem Value="8" >小学二年级</asp:ListItem>
            <asp:ListItem Value="9" >小学三年级</asp:ListItem>
            <asp:ListItem Value="10">小学四年级</asp:ListItem>
            <asp:ListItem Value="11">小学五年级</asp:ListItem>
            <asp:ListItem Value="12">小学六年级</asp:ListItem>
            <asp:ListItem Value="13" Selected="True">初中一年级</asp:ListItem>
            <asp:ListItem Value="14">初中二年级</asp:ListItem>
            <asp:ListItem Value="15">初中三年级</asp:ListItem>
            <asp:ListItem Value="16">高中一年级</asp:ListItem>
            <asp:ListItem Value="17">高中二年级</asp:ListItem>
            <asp:ListItem Value="18">高中三年级</asp:ListItem>
        </asp:DropDownList>
         当前学校类型：
        <asp:DropDownList ID="ddlSchoolType" runat="server">
            <asp:ListItem Value="1" Selected="True">普通学校</asp:ListItem>
            <asp:ListItem Value="2">重点学校</asp:ListItem>
            <asp:ListItem Value="3">国内国际学校</asp:ListItem>
        </asp:DropDownList>
    &nbsp;年预算：
        <asp:TextBox ID="txtGeneralBudget" runat="server">40000</asp:TextBox>
        <br />
        何时出国：
        <asp:DropDownList ID="ddlAbroadGrade" runat="server">
            <asp:ListItem Value="13">初中一年级</asp:ListItem>
            <asp:ListItem Value="14">初中二年级</asp:ListItem>
            <asp:ListItem Value="15">初中三年级</asp:ListItem>
            <asp:ListItem Value="16">高中一年级</asp:ListItem>
            <asp:ListItem Value="17">高中二年级</asp:ListItem>
            <asp:ListItem Value="18">高中三年级</asp:ListItem>
            <asp:ListItem Value="19" Selected="True">高中毕业后</asp:ListItem>
        </asp:DropDownList>
        <br />
        英语得分:<asp:TextBox ID="txtEnglishScore" runat="server">20</asp:TextBox>
        学科得分:<asp:TextBox ID="txtLearnScore" runat="server">20</asp:TextBox>
        素质得分:<asp:TextBox ID="txtQualityScore" runat="server">20</asp:TextBox>
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="生成规划" />
        <br />
        <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
        <br/>---------------------------
        <br />
        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
        <br/>-----------------下一步选着路线--------------------<br/>
        <asp:DropDownList ID="DropDownList1" runat="server">
            <asp:ListItem Value="1" Selected="True">学霸路线</asp:ListItem>
            <asp:ListItem Value="2">稳妥路线</asp:ListItem>
            <asp:ListItem Value="3">经济路线</asp:ListItem>
        </asp:DropDownList>      
        <asp:Button ID="Button2" runat="server"  Text="生成规划活动" OnClick="Button2_Click" />
    </div>
    </form>
</body>
</html>
