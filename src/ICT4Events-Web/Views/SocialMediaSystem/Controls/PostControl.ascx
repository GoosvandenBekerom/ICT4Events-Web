<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PostControl.ascx.cs" Inherits="ICT4Events_Web.Views.SocialMediaSystem.Controls.PostControl" %>
<ol class="postContainer">
    <li runat="server" ID="mainPost">
        <div ID="post" class="post well well-sm">
            <div class="PostHeader">
                <span ID="Username" class="Username" runat="server"></span>
                <span class="PostDate"><%:Post.Date.ToShortDateString()%></span>
                <p><strong><%:Post.Title%></strong></p>
            </div>
            <div class="PostContent">
                <p><%:Post.Content%></p>
            </div>
            <div class="PostFooter">
                <button type="button" id="reply" class="btn btn-sm btn-default" value="<%:Post.ID%>">
                    <span class="glyphicon glyphicon-share-alt" aria-hidden="true"></span> Reageren
                </button>
                <button type="button" id="report" class="btn btn-sm btn-default" value="<%:Post.ID%>">
                    <span class="glyphicon glyphicon-ban-circle" aria-hidden="true"></span> Rapporteren
                </button>
                <button type="button" id="like" class="btn btn-sm btn-default likeButton"  value="<%:Post.ID%>" runat="server">
                    <span class="glyphicon glyphicon-thumbs-up" aria-hidden="true"></span> 
                </button>
            </div>
        </div>
        <div class="replyForm hidden">
            <div class="form-group">
                <div class="col-md-9 col-xs-8"><asp:TextBox id="postReply" CssClass="form-control postReply" runat="server" placeholder="Reageren"></asp:TextBox></div>
                <button type="button" id="postReplyButton" class="btn btn-primary" value="<%:Post.ID%>">
                    <span class="glyphicon glyphicon-send" aria-hidden="true"></span> Reageer
                </button>
            </div>
        </div>
    </li>
</ol>