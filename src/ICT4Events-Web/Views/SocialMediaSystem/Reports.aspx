<%@ Page Title="Reports Admin Section" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Reports.aspx.cs" Inherits="ICT4Events_Web.Views.SocialMediaSystem.Reports" %>

<%@ Register Src="~/Views/SocialMediaSystem/Controls/PostControl.ascx" TagPrefix="uc1" TagName="PostControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1><%:Page.Title%></h1>
    <asp:Label ID="Label1" runat="server" Text="Selecteer een bericht" AssociatedControlID="lbReportedPosts"></asp:Label>
    <asp:ListBox ID="lbReportedPosts" CssClass="form-control" AutoPostBack="True"
        OnSelectedIndexChanged="lbReportedPosts_OnSelectedIndexChanged" runat="server">
        
    </asp:ListBox>
    <asp:PlaceHolder ID="phPost" runat="server">
        
    </asp:PlaceHolder>
</asp:Content>
