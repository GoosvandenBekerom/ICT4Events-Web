<%@ Page Title="Tijdlijn" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Timeline.aspx.cs" Inherits="ICT4Events_Web.Views.SocialMediaSystem.Timeline" %>
<%@ Reference Control="~/Views/SocialMediaSystem/Controls/PostControl.ascx" %>
<%@ Register Src="~/Views/SocialMediaSystem/Controls/CreatePost.ascx" TagPrefix="uc1" TagName="CreatePost" %>

<asp:Content ID="TimelineContent" ContentPlaceHolderID="MainContent" runat="server">
    <h1><%:Title%></h1>
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
                data: "{'postId':"+btn.attr('value')+"}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) {
                    console.log(result);
                    if (result.d === "succeeded") {
                        if (btn.hasClass('liked')) {
                            btn.removeClass('liked');
                            var count1 = parseInt(likeCount.text(), 10) - 1;
                            if (isNaN(count1)) { count1 = 1; }
                            likeCount.text(count1 === 0 ? '' : count1);
                        }
                        else
                        {
                            btn.addClass('liked');
                            var count2 = parseInt(likeCount.text(), 10) + 1;
                            if (isNaN(count2)) { count2 = 1; }
                            likeCount.text(count2);
                        }
                    }
                }
            });
        });

        $("body").on("click", ".reportButton", function () {
            var btn = $(this);
            $.ajax({
                type: "POST",
                url: "<%=VirtualPathUtility.ToAbsolute("~/Views/SocialMediaSystem/Timeline.aspx/ReportPost")%>",
                data: "{'postId':"+btn.attr('value')+"}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) {
                    if (result.d === "succeeded")
                        btn.addClass('reported');
                }
            });
        });

        $("body").on("click", ".postDeleteButton", function() {
            var btn = $(this);

            $.ajax({
                type: "POST",
                url: "<%=VirtualPathUtility.ToAbsolute("~/Views/SocialMediaSystem/Timeline.aspx/DeletePost")%>",
                data: "{'postId':" + btn.attr('value') + "}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function(result) {
                    if (result.d === "true") {
                        var target = btn.closest('.postContainer');
                        target.hide(500, function() { target.remove(); });
                        console.log('Removed post ' + btn.val());
                    }
                }
            });
        });

        $('.postReplyButton').on("click", function () {
            var btn = $(this);
            var text = btn.siblings('div').find('input').val();

            $.ajax({
                type: "POST",
                url: "<%=VirtualPathUtility.ToAbsolute("~/Views/SocialMediaSystem/Timeline.aspx/AddReply")%>",
                data: "{'postId':"+btn.attr('value')+", 'message':'"+text+"'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) {
                    if (result.d !== "false")
                        btn.parent().parent().parent().append(result.d);
                }
            });
        });

        $('.replyButton, .postContainer').one("click", function () {
            var btn;
            if ($(this).hasClass('replyButton')) {
                btn = $(this);
            } else {
                btn = $(this).find('li .post .PostFooter .replyButton');
            }

            var itemLocation = btn.parent().parent().parent();
            if (itemLocation.find(".reply").length > 0) return;

            console.log(btn.val());
            $.ajax({
                type: "POST",
                url: "<%=VirtualPathUtility.ToAbsolute("~/Views/SocialMediaSystem/Timeline.aspx/LoadReplies")%>",
                data: "{'postId':"+btn.val()+"}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (result) {
                    if (result.d !== "false")
                        itemLocation.append(result.d);
                }
            });
        });
    </script>
</asp:Content>
