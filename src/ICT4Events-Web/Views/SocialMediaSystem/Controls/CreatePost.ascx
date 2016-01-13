<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CreatePost.ascx.cs" Inherits="ICT4Events_Web.Views.SocialMediaSystem.Controls.CreatePost" %>

<div class="container">
	<div class="row">
		<div class="col-md-12 well createPostContainer">
            <asp:ValidationSummary ID="ValidationSummary" ValidationGroup="PostCreationGroup" runat="server" />
            <asp:TextBox ID="txtTitle" CssClass="form-control createPostFormControl" placeholder="Titel" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="TitleValidator" runat="server" ErrorMessage="Titel is verplicht" ControlToValidate="txtTitle" CssClass="text-danger" ValidationGroup="PostCreationGroup" Display="None"></asp:RequiredFieldValidator>
            <asp:TextBox ID="txtMessage" CssClass="form-control createPostFormControl" TextMode="multiline" Rows="3"
                placeholder="Wat ben je aan het doen?" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="MessageValidator" runat="server" ErrorMessage="Bericht is verplicht" ControlToValidate="txtMessage" CssClass="text-danger" ValidationGroup="PostCreationGroup" Display="None"></asp:RequiredFieldValidator>
            <div class="col-md-10 left">
                <asp:FileUpload ID="FileUpload" runat="server" AllowMultiple="False"
                    CssClass="form-control createPostFormControl pull-right" />
            </div>
            <div class="col-md-2 right">
                <asp:Button ID="btnPost" CssClass="btn btn-info pull-right" runat="server" Text="Plaats Bericht" OnClick="btnPost_Click" data-loading-text="Uploaden..." OnClientClick="if(Page_ClientValidate('PostCreationGroup')) { $(this).button('loading'); } else { return false; }" />
            </div>
        </div>
	</div>
</div>