<%@ Page Title="Registreren klant" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Webform.aspx.cs" EnableEventValidation="true" Inherits="ICT4Events_Web.Webform" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %></h2>
    <h3>Registratieformulier</h3>
    <p>Op deze pagina staat een registratieformulier die het concept van het registreren van een nieuwe gebruiker met validatie uitbeeldt.
        <br/><small>Let op: Er wordt voor deze demo naast de validatie niets gedaan met de gegevens.</small>
    </p>
    
        <fieldset>
            <!-- Form Name -->
            <legend>Form Name</legend>

            <!-- Text input-->
            <div class="form-group">
              <asp:Label runat="server" CssClass="control-label" AssociatedControlID="first_name">Voornaam:</asp:Label>  
              <asp:TextBox runat="server" ID="first_name" CssClass="form-control input-md" placeholder="ex: Stefano"/>
              <asp:RequiredFieldValidator runat="server" CssClass="text-danger" ControlToValidate="first_name" Display="Dynamic" ErrorMessage="Voornaam is verplicht" ValidationGroup="RegistrationGroup" />
            </div>

            <!-- Text input-->
            <div class="form-group">
              <asp:Label runat="server" CssClass="control-label" AssociatedControlID="last_name">Achternaam:</asp:Label>  
              <asp:TextBox runat="server" ID="last_name" CssClass="form-control input-md" placeholder="ex: Bovenkamp" />
              <asp:RequiredFieldValidator runat="server" CssClass="text-danger" ControlToValidate="last_name" Display="Dynamic" ErrorMessage="Achternaam is verplicht" ValidationGroup="RegistrationGroup" />
            </div>

            <!-- Text input-->
            <div class="form-group">              
              <asp:Label runat="server" CssClass="control-label" AssociatedControlID="phone_number">Telefoonnummer:</asp:Label>  
              <asp:TextBox runat="server" ID="phone_number" CssClass="form-control input-md" placeholder="ex: 0655854878" />
              <asp:RequiredFieldValidator runat="server" CssClass="text-danger" ControlToValidate="phone_number" Display="Dynamic" ErrorMessage="Telefoonnummer is verplicht" ValidationGroup="RegistrationGroup"/>
              <asp:CustomValidator Enabled="true" runat="server" ControlToValidate="phone_number" ClientValidationFunction="validatePhoneNumber" CssClass="text-danger" Display="Dynamic" ErrorMessage="Telefoonnummer is niet geldig" ValidateEmptyText="False" ValidationGroup="RegistrationGroup" /> 
            </div>

            <!-- Text input-->
            <div class="form-group">
              <asp:Label runat="server" CssClass="control-label" AssociatedControlID="iban">Rekeningnummer:</asp:Label>  
              <asp:TextBox runat="server" ID="iban" CssClass="form-control input-md" placeholder="ex: NL99 BANK 2183 2384 12" />
              <asp:RequiredFieldValidator runat="server" CssClass="text-danger" ControlToValidate="iban" Display="Dynamic" ErrorMessage="IBAN is verplicht" ValidationGroup="RegistrationGroup" />
              <asp:CustomValidator ID="test" runat="server" ControlToValidate="iban" ClientValidationFunction="validateIban" CssClass="text-danger" Display="Dynamic" ErrorMessage="IBAN is niet geldig" ValidateEmptyText="False" ValidationGroup="RegistrationGroup"/>  
            </div>

            <!-- Button -->
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

<script src="scripts/iban.js"></script>
<script>
    function validateInput() {
        var firstname = $("#MainContent_first_name").val().length > 0;
        var lastname = $("#MainContent_first_name").val().length > 0;
        var phoneNumberValid = validatePhoneNumber();
        var ibanValid = validateIban();

        return firstname && lastname && ibanValid && phoneNumberValid;
    }

    // Functie voor het valideren van een telefoonnummer
    function validatePhoneNumber(sender, args) {
        // Haal het ingevulde telefoonnummer op
        var phone_number = $("#MainContent_phone_number").val();
        var valid = false;

        // Een telefoonnummer is geldig als het met een 0 begint, en 10 tekens bevat
        if (phone_number.substr(0, 1) == "0" && phone_number.length == 10) {
            valid = true;
        }
        args.IsValid = valid;
        return valid;
    }

    function validateIban(sender, args) {
        var valid = IBAN.isValid($("#MainContent_iban").val());
        args.IsValid = valid;
        return valid;
    }
</script>


</asp:Content>


