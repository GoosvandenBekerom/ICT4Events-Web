<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Timeline.aspx.cs" Inherits="ICT4Events_Web.Views.SocialMediaSystem.Timeline" %>
<%@ Import Namespace="SharedModels.Models" %>
<%@ Reference Control="~/Views/SocialMediaSystem/Controls/PostControl.ascx" %>
<asp:Content ID="TimelineContent" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Timeline</h1>
    <asp:PlaceHolder ID="Posts" runat="server">
        
    </asp:PlaceHolder>
</asp:Content>
