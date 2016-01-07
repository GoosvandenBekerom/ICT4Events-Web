<%@ Page Title="Gebruikers aanpassen" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Rights.aspx.cs" Inherits="ICT4Events_Web.Views.AccountSystem.Rights" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h3><%: Title %></h3>
    
    <div class="row">
		<div class="col-md-12">
		    <div id="feedbackPanel">
		        
		    </div>
		    <h4>Selecteer een gebruiker waarvan je de rechten wilt wijzigen:</h4>
		    <asp:DropDownList ID="drpUsers" CssClass="form-control" runat="server"></asp:DropDownList><br/>
            <asp:Button ID="btnChange" CssClass="btn btn-default" runat="server" Text="Gebruikers rechten aanpassen" OnClick="btnChange_Click" />
		</div>
	</div>
    <script>
    $("select").on("change", function () {
        $("#MainContent_btnChange").removeProp("disabled");
        });

</script>
</asp:Content>
