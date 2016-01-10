<%@ Page Title="Registreren" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" EnableEventValidation="true" Inherits="ICT4Events_Web.Views.ReservationSystem.Webform" %>
<%@ Import Namespace="SharedModels.Logic" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <asp:Label ID="lblLoggedIn" CssClass="alert alert-success" runat="server" Text="" Visible="False"></asp:Label>
    <div id="feedbackPanel" class="alert alert-warning" role="alert" runat="server" Visible="False">
        <asp:Label ID="lblError" runat="server" Text="Label"></asp:Label>
    </div>
    <div id="feedbackPanelSucces" class="alert alert-info alert-dismissible" role="alert" runat="server" Visible="False">
        <button type="button" class="close" data-dismiss="alert" aria-label="Close">
            <span aria-hidden="true">&times;</span>
        </button>
        <strong>Status registratie:</strong>
        <br/>
        <asp:Label ID="lblSucces" runat="server" Text="Label"></asp:Label>
    </div>
    <div class="container" id="fromRegister" runat="server">
        <div class="row">
            <div class="board">
                <div class="tab-content">
                    <h3 class="head clear-fix">Registratie voor <% Response.Write(CurEvent.Name); %></h3>

                    <div class="col-md-6">
                        <!-- voornaam leader -->
                        <div class="form-group">
                            <asp:Label runat="server" CssClass="control-label" AssociatedControlID="leader_first_name">Voornaam:</asp:Label>
                            <asp:TextBox runat="server" ID="leader_first_name" CssClass="form-control input-md" placeholder="ex: Stefano" />
                            <asp:RequiredFieldValidator runat="server" CssClass="text-danger" ControlToValidate="leader_first_name" Display="Dynamic" ErrorMessage="Voornaam is verplicht" ValidationGroup="RegistrationGroup" />
                        </div>

                        <!-- achternaam leader -->
                        <div class="form-group">
                            <asp:Label runat="server" CssClass="control-label" AssociatedControlID="leader_last_name">Achternaam:</asp:Label>
                            <asp:TextBox runat="server" ID="leader_last_name" CssClass="form-control input-md" placeholder="ex: Bovenkamp" />
                            <asp:RequiredFieldValidator runat="server" CssClass="text-danger" ControlToValidate="leader_last_name" Display="Dynamic" ErrorMessage="Achternaam is verplicht" ValidationGroup="RegistrationGroup" />
                        </div>

                        <!-- adres leader-->
                        <div class="form-group">
                            <asp:Label runat="server" CssClass="control-label" AssociatedControlID="leader_address">Adres:</asp:Label>
                            <asp:TextBox runat="server" ID="leader_address" CssClass="form-control input-md" placeholder="ex: Kennedylaan" />
                            <asp:RequiredFieldValidator runat="server" CssClass="text-danger" ControlToValidate="leader_address" Display="Dynamic" ErrorMessage="Adres is verplicht" ValidationGroup="RegistrationGroup" />
                        </div>

                        <!-- gebruikersnaam leader -->
                        <div class="form-group">
                            <asp:Label runat="server" CssClass="control-label" AssociatedControlID="leader_Username">Gebruikersnaam:</asp:Label>
                            <asp:TextBox runat="server" ID="leader_Username" CssClass="form-control input-md" placeholder="ex: Stefano" />
                            <asp:RequiredFieldValidator runat="server" CssClass="text-danger" ControlToValidate="leader_Username" Display="Dynamic" ErrorMessage="Gebruikersnaam is verplicht" ValidationGroup="RegistrationGroup" />
                        </div>
                    </div>
                    <div class="col-md-6">
                        <!-- woonplaats leader -->
                        <div class="form-group">
                            <asp:Label runat="server" CssClass="control-label" AssociatedControlID="leader_city">Woonplaats:</asp:Label>
                            <asp:TextBox runat="server" ID="leader_city" CssClass="form-control input-md" placeholder="ex: Eindhoven" />
                            <asp:RequiredFieldValidator runat="server" CssClass="text-danger" ControlToValidate="leader_city" Display="Dynamic" ErrorMessage="Woonplaats is verplicht" ValidationGroup="RegistrationGroup" />
                        </div>

                        <!-- iban leader -->
                        <div class="form-group">
                            <asp:Label runat="server" CssClass="control-label" AssociatedControlID="leader_iban">Rekeningnummer:</asp:Label>
                            <asp:TextBox runat="server" ID="leader_iban" CssClass="form-control input-md" placeholder="ex: NL99 BANK 2183 2384 12" />
                            <asp:RequiredFieldValidator runat="server" CssClass="text-danger" ControlToValidate="leader_iban" Display="Dynamic" ErrorMessage="IBAN is verplicht" ValidationGroup="RegistrationGroup" />
                            <asp:CustomValidator ID="test" runat="server" ControlToValidate="leader_iban" ClientValidationFunction="validateIban" CssClass="text-danger" Display="Dynamic" ErrorMessage="IBAN is niet geldig" ValidateEmptyText="False" ValidationGroup="RegistrationGroup"/>
                        </div>

                        <!-- Emailadres leader -->
                        <div class="form-group">
                            <asp:Label runat="server" CssClass="control-label" AssociatedControlID="leader_Email">Emailadres:</asp:Label>
                            <asp:TextBox runat="server" ID="leader_Email" CssClass="form-control input-md" placeholder="ex: johndoe@gmail.com" />
                            <asp:RequiredFieldValidator runat="server" CssClass="text-danger" ControlToValidate="leader_Email" Display="Dynamic" ErrorMessage="Emailadres is verplicht" ValidationGroup="RegistrationGroup" />
                        </div>
                        
                        <!-- Password leader -->
                        <div class="form-group">
                            <asp:Label runat="server" CssClass="control-label" AssociatedControlID="leader_Password">Wachtwoord</asp:Label>
                            <asp:TextBox runat="server" ID="leader_Password" TextMode="Password" CssClass="form-control" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="leader_Password" CssClass="text-danger" ErrorMessage="Wachtwoord is verplicht." Display="Dynamic" ValidationGroup="RegistrationGroup" />
                        </div>
                    </div>
                    
                    <hr style="clear: both;" />
                    <!-- second -->
                    <h3 class="head">Reserveerders aangeven:</h3>
                    <div class="col-md-12">
                        <!-- Emailadres 1 -->
                        <div class="form-group">
                            <asp:Label runat="server" CssClass="control-label" AssociatedControlID="Email1">Emailadres reserveerder 1 (optioneel):</asp:Label>
                            <asp:TextBox runat="server" ID="Email1" CssClass="form-control input-md" placeholder="ex: johndoe@gmail.com" />
                        </div>

                        <!-- Emailadres 2 -->
                        <div class="form-group">
                            <asp:Label runat="server" CssClass="control-label" AssociatedControlID="Email2">Emailadres reserveerder 2 (optioneel):</asp:Label>
                            <asp:TextBox runat="server" ID="Email2" CssClass="form-control input-md" placeholder="ex: johndoe@gmail.com" />
                        </div>

                        <!-- Emailadres 3 -->
                        <div class="form-group">
                            <asp:Label runat="server" CssClass="control-label" AssociatedControlID="Email3">Emailadres reserveerder 3 (optioneel):</asp:Label>
                            <asp:TextBox runat="server" ID="Email3" CssClass="form-control input-md" placeholder="ex: johndoe@gmail.com" />
                        </div>

                        <!-- Emailadres 4 -->
                        <div class="form-group">
                            <asp:Label runat="server" CssClass="control-label" AssociatedControlID="Email4">Emailadres reserveerder 4 (optioneel):</asp:Label>
                            <asp:TextBox runat="server" ID="Email4" CssClass="form-control input-md" placeholder="ex: johndoe@gmail.com" />
                        </div>

                        <!-- Emailadres 5 -->
                        <div class="form-group">
                            <asp:Label runat="server" CssClass="control-label" AssociatedControlID="Email5">Emailadres reserveerder 5 (optioneel):</asp:Label>
                            <asp:TextBox runat="server" ID="Email5" CssClass="form-control input-md" placeholder="ex: johndoe@gmail.com" />
                        </div>

                    </div>

                    <hr />
                    <!-- third -->
                    <h3 class="head">Locatie kiezen:</h3>
                    <div class="col-md-6">
                        <h5>Evenementnaam: <strong><% Response.Write(CurEvent.Name); %></strong></h5>
                        <h5>Startdatum: <% Response.Write(CurEvent.StartDate.ToString("dd-M-yyyy")); %></h5>
                        <h5>Einddatum: <% Response.Write(CurEvent.EndDate.ToString("dd-M-yyyy")); %></h5>
                        <h5>Capaciteit: <% Response.Write(CurEvent.Capacity); %></h5>
                    </div>

                    <div class="col-md-6">
                        <h5>Locatie naam: <% Response.Write(LogicCollection.LocationLogic.GetById(CurEvent.LocationID).Name); %></h5>
                        <h5>Locatie adres: <% Response.Write(LogicCollection.LocationLogic.GetById(CurEvent.LocationID).Address + " " + LogicCollection.LocationLogic.GetById(CurEvent.LocationID).Number); %></h5>
                        <h5>Locatie stad: <% Response.Write(LogicCollection.LocationLogic.GetById(CurEvent.LocationID).City); %></h5>
                        <h5>Locatie adres: <% Response.Write(LogicCollection.LocationLogic.GetById(CurEvent.LocationID).PostalCode); %></h5>
                    </div>
                    <hr />

                    <asp:UpdatePanel runat="server">
                        <ContentTemplate>

                            <div class="col-md-6">
                                Startdatum:
                                        <br />
                                <asp:Calendar ID="StartDate" runat="server" OnDayRender="StartDate_DayRender" OnSelectionChanged="StartDate_SelectionChanged"></asp:Calendar>
                            </div>

                            <div class="col-md-6">
                                Einddatum:
                                        <br />
                                <asp:Calendar ID="EndDate" runat="server" OnDayRender="EndDate_DayRender" OnSelectionChanged="EndDate_SelectionChanged"></asp:Calendar>
                            </div>

                        </ContentTemplate>
                    </asp:UpdatePanel>

                    <br />
                    <div class="col-md-6">
                        <br />
                        <h5>Plek selecteren: </h5>
                        <asp:DropDownList ID="drpListOfPlaces" runat="server" CssClass="form-control"></asp:DropDownList>
                        <br />
                        <!-- Map modal -->
                        <button type="button" class="btn btn-primary" data-toggle="modal" data-target=".bs-example-modal-sm">Map bekijken</button>

                        <div class="modal fade bs-example-modal-sm" tabindex="-1" role="dialog" aria-labelledby="mySmallModalLabel">
                          <div class="modal-dialog modal-sm">
                            <div class="modal-content">
                              <img src="http://i64.tinypic.com/281vio0.jpg"/>
                            </div>
                          </div>
                        </div>
                    </div>

                    <div class="col-md-6">
                        <br />
                        <div id="informationPlace" class="alert" role="alert">
                        </div>
                    </div>
                    <div class="clearfix"></div>
                </div>
            </div>
            <br />
<div id="checkout-button" class="panel panel-primary">
    <div class="panel-heading"><h3 class="panel-title">Registratie voltooien</h3></div>
	<div class="panel-body">    
		<div class="alert alert-info">
		<div class="checkbox">
			<label>
                <asp:CheckBox ID="chbAgreement" runat="server" Checked="False" /><strong>Ik heb de algemene voorwaarden gelezen en ik ga hiermee akkoord</strong>
            </label>
		</div>
	</div>
	</div>    		
</div>
	<div id="checkout-review-submit">
			<div class="pull-right" id="review-buttons-container">
				   <asp:Button ID="btnSubmit" runat="server" Text="Registereren" CssClass="btn btn-primary" OnClick="btnSubmit_Click" CausesValidation="true" ValidationGroup="RegistrationGroup" disabled="disbaled"/>
			</div>
	</div>    

        </div>
    </div>

<script src="../../Scripts/iban.js"></script>
<script>
    function validateInput() {
        var firstname = $("#MainContent_leader_first_name").val().length > 0;
        var lastname = $("#MainContent_leader_first_name").val().length > 0;
        var ibanValid = validateIban();
        return firstname && lastname && ibanValid;
    }

    function validateIban(sender, args) {
        var valid = IBAN.isValid($("#MainContent_leader_iban").val());
        args.IsValid = valid;
        return valid;
    }

    $("input[type='checkbox']").on("change", function() {
        $("#MainContent_btnSubmit").toggleClass("disabled", !$(this).prop('checked'));
        if ($("#MainContent_btnSubmit").prop('disabled'))
        {
            $("#MainContent_btnSubmit").removeProp("disabled");
        }
    });

    $("select").on("change", function () {
        var select = $(this);
        console.log(select);
        console.log(select.val());
        $.ajax({
            type: "POST",
            url: "<%=VirtualPathUtility.ToAbsolute("~/Views/ReservationSystem/Register.aspx/LoadPlace")%>",
            data: "{'id':"+select.val()+"}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result.d !== "false") {
                    $("#informationPlace").empty();
                    $("#informationPlace").append(result.d);
                }
            }
        });
    });

</script>

</asp:Content>


