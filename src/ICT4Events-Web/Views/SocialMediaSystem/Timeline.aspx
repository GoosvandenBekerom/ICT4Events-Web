<%@ Page Title="Tijdlijn" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Timeline.aspx.cs" Inherits="ICT4Events_Web.Views.SocialMediaSystem.Timeline" %>
<%@ Reference Control="~/Views/SocialMediaSystem/Controls/PostControl.ascx" %>
<%@ Register Src="~/Views/SocialMediaSystem/Controls/CreatePost.ascx" TagPrefix="uc1" TagName="CreatePost" %>

<asp:Content ID="TimelineContent" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Tijdlijn</h1>
    <div class="row" id="SearchContainer">
        <div class="form-group" style="display: flex">
            <asp:TextBox runat="server" CssClass="form-control col-md-9" id="SearchBox" placeholder="Zoeken op hashtag"></asp:TextBox>
            <button type="submit" runat="server" OnServerClick="SearchButton_OnServerClick" ID="SearchButton" class="btn btn-primary"><span class="glyphicon glyphicon-search"></span></button>
        </div>
    </div>

    <uc1:CreatePost runat="server" id="CreatePost" />

    <asp:PlaceHolder ID="Posts" runat="server">
        
    </asp:PlaceHolder>
    <script>
        $("body").on("click", '.likeButton', function () {
            var btn = $(this);
            var likeCount = $(this).find('span').last();
            $.ajax({
                type: "POST",
                url: "<%=VirtualPathUtility.ToAbsolute("~/Views/SocialMediaSystem/Timeline.aspx/LikePost")%>",
                data: "{'postId':"+$(this).attr('value')+"}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) {
                    console.log(result);
                    if (result.d === "succeeded") {
                        btn.addClass('liked');
                        var count = parseInt(likeCount.text(), 10) + 1;
                        if (isNaN(count)) { count = 1; }

                        likeCount.text(count);
                    }
                }
            });
        });

        $(".reportButton").on("click", function () {
            var btn = $(this);
            $.ajax({
                type: "POST",
                url: "<%=VirtualPathUtility.ToAbsolute("~/Views/SocialMediaSystem/Timeline.aspx/ReportPost")%>",
                data: "{'postId':"+$(this).attr('value')+"}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) {
                    if (result.d === "succeeded")
                        btn.addClass('reported');
                }
            });
        });

        $('.postReplyButton').on("click", function () {
            var btn = $(this);
            var text = btn.siblings('div').find('input').val();

            $.ajax({
                type: "POST",
                url: "<%=VirtualPathUtility.ToAbsolute("~/Views/SocialMediaSystem/Timeline.aspx/AddReply")%>",
                data: "{'postId':"+$(this).attr('value')+", 'message':'"+text+"'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) {
                    if (result.d !== "false")
                        btn.parent().parent().parent().append(result.d);
                }
            });
        });
    </script>
</asp:Content>
