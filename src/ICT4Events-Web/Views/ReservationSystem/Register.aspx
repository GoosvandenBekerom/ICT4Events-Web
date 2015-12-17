<%@ Page Title="Registreren" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" EnableEventValidation="true" Inherits="ICT4Events_Web.Webform" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %></h2>
        <fieldset>
            <!-- voornaam -->
            <div class="form-group">
              <asp:Label runat="server" CssClass="control-label" AssociatedControlID="first_name">Voornaam:</asp:Label>  
              <asp:TextBox runat="server" ID="first_name" CssClass="form-control input-md" placeholder="ex: Stefano"/>
              <asp:RequiredFieldValidator runat="server" CssClass="text-danger" ControlToValidate="first_name" Display="Dynamic" ErrorMessage="Voornaam is verplicht" ValidationGroup="RegistrationGroup" />
            </div>

            <!-- achternaam -->
            <div class="form-group">
              <asp:Label runat="server" CssClass="control-label" AssociatedControlID="last_name">Achternaam:</asp:Label>  
              <asp:TextBox runat="server" ID="last_name" CssClass="form-control input-md" placeholder="ex: Bovenkamp" />
              <asp:RequiredFieldValidator runat="server" CssClass="text-danger" ControlToValidate="last_name" Display="Dynamic" ErrorMessage="Achternaam is verplicht" ValidationGroup="RegistrationGroup" />
            </div>

            <!-- adres -->
            <div class="form-group">              
              <asp:Label runat="server" CssClass="control-label" AssociatedControlID="address">Adres:</asp:Label>
              <asp:TextBox runat="server" ID="address" CssClass="form-control input-md" placeholder="ex: 06123456748" />
              <asp:RequiredFieldValidator runat="server" CssClass="text-danger" ControlToValidate="address" Display="Dynamic" ErrorMessage="Adres is verplicht" ValidationGroup="RegistrationGroup"/>
            </div>
            
            <!-- woonplaats -->
            <div class="form-group">           
              <asp:Label runat="server" CssClass="control-label" AssociatedControlID="city">Woonplaats:</asp:Label>
              <asp:TextBox runat="server" ID="city" CssClass="form-control input-md" placeholder="ex: 06123456748" />
              <asp:RequiredFieldValidator runat="server" CssClass="text-danger" ControlToValidate="city" Display="Dynamic" ErrorMessage="Woonplaats is verplicht" ValidationGroup="RegistrationGroup"/>
            </div>

            <!-- iban -->
            <div class="form-group">
              <asp:Label runat="server" CssClass="control-label" AssociatedControlID="iban">Rekeningnummer:</asp:Label>  
              <asp:TextBox runat="server" ID="iban" CssClass="form-control input-md" placeholder="ex: NL99 BANK 2183 2384 12" />
              <asp:RequiredFieldValidator runat="server" CssClass="text-danger" ControlToValidate="iban" Display="Dynamic" ErrorMessage="IBAN is verplicht" ValidationGroup="RegistrationGroup" />
              <asp:CustomValidator ID="test" runat="server" ControlToValidate="iban" ClientValidationFunction="validateIban" CssClass="text-danger" Display="Dynamic" ErrorMessage="IBAN is niet geldig" ValidateEmptyText="False" ValidationGroup="RegistrationGroup"/>  
            </div>

            <div class="form-group">
                <asp:Button ID="btnSubmit" runat="server" Text="Registereren" CssClass="btn btn-primary" OnClick="btnSubmit_Click" CausesValidation="true" ValidationGroup="RegistrationGroup" />
            </div>
        </fieldset>

    <div id="feedbackPanel" class="alert alert-info alert-dismissible" role="alert" runat="server" Visible="False">
        <button type="button" class="close" data-dismiss="alert" aria-label="Close">
            <span aria-hidden="true">&times;</span>
        </button>
        <strong>Status registratie:</strong>
        <br/>
        <asp:Label ID="Label1" runat="server" Visible="False" Text="Label"></asp:Label>
    </div>

<script src="../../Scripts/iban.js"></script>
<script>
    function validateInput() {
        var firstname = $("#MainContent_first_name").val().length > 0;
        var lastname = $("#MainContent_first_name").val().length > 0;
        var ibanValid = validateIban();

        return firstname && lastname && ibanValid;
    }

    function validateIban(sender, args) {
        var valid = IBAN.isValid($("#MainContent_iban").val());
        args.IsValid = valid;
        return valid;
    }
</script>


</asp:Content>


