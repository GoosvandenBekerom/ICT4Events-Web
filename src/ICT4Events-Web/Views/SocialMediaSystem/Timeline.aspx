<%@ Page Title="Tijdlijn" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Timeline.aspx.cs" Inherits="ICT4Events_Web.Views.SocialMediaSystem.Timeline" %>
<%@ Reference Control="~/Views/SocialMediaSystem/Controls/PostControl.ascx" %>
<asp:Content ID="TimelineContent" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Tijdlijn</h1>
    <div class="row" id="SearchContainer">
        <div class="form-group" style="display: flex">
            <asp:TextBox runat="server" CssClass="form-control col-md-9" id="SearchBox" placeholder="Product zoeken"></asp:TextBox>
            <button type="submit" runat="server" OnServerClick="SearchButton_OnServerClick" ID="SearchButton" class="btn btn-primary"><span class="glyphicon glyphicon-search"></span></button>
        </div>
    </div>
    <asp:PlaceHolder ID="Posts" runat="server">
        
    </asp:PlaceHolder>
    <script>
        $(".likeButton").click(function () {
            $.ajax({
                type: "POST",
                url: "Timeline.aspx/LikePost",
                data: "{'postId':" + $(this).attr('value') + "}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) {
                    console.log(result);
                }
            });
        });
    </script>
</asp:Content>
