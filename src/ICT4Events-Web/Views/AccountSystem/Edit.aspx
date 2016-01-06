<%@ Page Title="Gegevens wijzigen" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Edit.aspx.cs" Inherits="ICT4Events_Web.Views.AccountSystem.WebForm1" %>
<%@ Import Namespace="ICT4Events_Web" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<%
    // Succes message
    var currentUrl = Request.Url.AbsoluteUri; // current url
    var succes = Convert.ToInt32(HttpUtility.ParseQueryString(new Uri(currentUrl).Query).Get("succes")); // gets brand id
    if (succes == 1) { feedbackPanelSucces.Visible = true; }

    var currentUser = ((SiteMaster) Master)?.CurrentUser();
    if (currentUser != null)
    {
        txtEmailadress.Text = currentUser.Email;
        txtUsername.Text = currentUser.Username;
    }
    else
    {
        Response.Redirect("~/Views/AccountSystem/Login.aspx");
    }
%>
    <!-- Form Name -->
    <h2><%: Title %></h2>
    <br />
    <div class="row">
        <div id="feedbackPanel" runat="server" class="alert alert-danger" role="alert" Visible="False"></div>   
        <div id="feedbackPanelSucces" runat="server" class="alert alert-success" role="alert" Visible="False">Gegevens succesvol gewijzigd.</div>     
        <fieldset>
            <!-- Text input-->
              <label class="control-label" for="txtEmailadress">Emailadres: </label>  
              <asp:TextBox ID="txtEmailadress" runat="server" CssClass="form-control input-md" ReadOnly="True"></asp:TextBox>


            <!-- Text input-->
              <label class="control-label" for="txtUsername">Gebruikersnaam: </label> 
              <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control input-md"></asp:TextBox>

            <!-- Text input-->
              <label class="control-label" for="txtwpass1">Wachtwoord: </label>  
              <asp:TextBox ID="txtwpass1" runat="server" CssClass="form-control input-md" TextMode="Password"></asp:TextBox>
    
             <!-- Text input-->
              <label class="control-label" for="txtwpass2">Verificatie wachtwoord: </label>  
              <asp:TextBox ID="txtwpass2" runat="server" CssClass="form-control input-md" TextMode="Password"></asp:TextBox>
            <br />
        <!-- Button (Double) -->
          <div class="col-md-8">
              <asp:Button ID="btnSave" runat="server" CssClass="btn btn-success" Text="Wijzigen aanbrengen" OnClick="btnSave_Click" />
          </div>

        </fieldset>
</div>
</asp:Content>
