<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PostControl.ascx.cs" Inherits="ICT4Events_Web.Views.SocialMediaSystem.Controls.PostControl" %>
<ol class="postContainer">
    <li runat="server" ID="mainPost">
        <div ID="post" class="post well well-sm">
            <div class="PostHeader">
                <span ID="Username" class="Username" runat="server"></span>
                <span class="PostDate"><%:Post.Date.ToShortDateString()%></span>
                <p><strong><%:Post.Title%></strong></p>
                <button type="button" ID="delete" class="close postDeleteButton" data-toggle="tooltip" title="Bericht verwijderen" aria-label="Close" runat="server" Visible="False"><span aria-hidden="true">&times;</span></button>
            </div>
            <hr/>
            <div class="PostContent">
                <p <% Response.Write(File != null ? "class=\"col-sm-10\"" : "");%>><%:Post.Content%></p>
            <% 
                if (File != null && File.IsImage)
                {
            %>
            <div class="postMedia col-sm-2">
                <a class="thumbnail" 
                    data-toggle="modal" data-target=".modal_<%:File.ID%>">
                    <asp:Image ID="postThumbnail" runat="server" CssClass="img img-thumbnail" />
                </a>
                <!-- Modal of image -->
                <div class="modal fade modal_<%:File.ID%>" tabindex="-1" role="dialog" aria-labelledby="mySmallModalLabel">
                    <div class="modal-dialog modal-sm">
                        <div class="modal-content">
                            <a href="<%:Page.ResolveClientUrl(File.Filepath)%>" target="_blank"><img src="<% Response.Write(File.Filepath); %>" alt="" /></a>
                        </div>
                    </div>
                </div>
            </div>
            <% 
                }
                else if (File != null)
                {
            %>
            <div class="postMedia col-sm-2">
                <a class="btn btn-primary" href="<%:File.Filepath%>">
                    Download Bestand
                    <span class="glyphicon glyphicon-download"></span>
                </a>
            </div>
            <%
               }

            %>
            </div>
            <div class="clear-fix clearfix"></div>
            <hr/>
            <div class="PostFooter">
                <button type="button" class="btn btn-sm btn-default replyButton" onclick="$(this).parent().parent().parent().find('.hidden').toggleClass('hidden');" value="<%:Post.ID%>">
                    <span class="glyphicon glyphicon-share-alt" aria-hidden="true"></span> Reageren
                </button>
                <button type="button" ID="report" class="btn btn-sm btn-default reportButton" runat="server">
                    <span class="glyphicon glyphicon-ban-circle" aria-hidden="true"></span> 
                </button>
                <button type="button" id="like" class="btn btn-sm btn-default likeButton" runat="server">
                    <span class="glyphicon glyphicon-thumbs-up" aria-hidden="true"></span>
                </button>
            </div>
        </div>
        <div class="replyForm hidden">
            <div class="form-group">
                <div class="col-md-9 col-xs-8">
                    <asp:TextBox id="postReply" CssClass="form-control postReply" runat="server" placeholder="Reageren"></asp:TextBox>
                </div>
                <button type="button" class="btn btn-primary postReplyButton" value="<%:Post.ID%>">
                    <span class="glyphicon glyphicon-send" aria-hidden="true"></span> Reageer
                </button>
            </div>
        </div>
    </li>
</ol>