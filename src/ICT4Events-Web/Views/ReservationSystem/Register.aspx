<%@ Page Title="Registreren" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" EnableEventValidation="true" Inherits="ICT4Events_Web.Views.ReservationSystem.Webform" %>
<%@ Import Namespace="SharedModels.Logic" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %></h2>
    <div id="feedbackPanel" class="alert alert-info alert-dismissible" role="alert" runat="server" Visible="False">
        <button type="button" class="close" data-dismiss="alert" aria-label="Close">
            <span aria-hidden="true">&times;</span>
        </button>
        <strong>Status registratie:</strong>
        <br/>
        <asp:Label ID="lblError" runat="server" Text="Label"></asp:Label>
    </div>

    <section style="background: #efefe9;">
        <div class="container">
            <div class="row">
                <div class="board">
                    <div class="board-inner">
                        <ul class="nav nav-tabs" id="myTab">
                           <div class="liner"></div>
                            <li class="active">
                                <a href="#home" data-toggle="tab" title="welkom">
                                    <span class="round-tabs one">
                                        <i class="glyphicon glyphicon-user"></i>
                                    </span>
                                </a></li>

                            <li class="disabled"><a href="#profile" data-toggle="tab" title="profile">
                                <span class="round-tabs two">
                                    <i class="glyphicon glyphicon-home"></i>
                                </span>
                            </a>
                            </li>
                            <li class="disabled"><a href="#messages" data-toggle="tab" title="bootsnipp goodies">
                                <span class="round-tabs three">
                                    <i class="glyphicon glyphicon-gift"></i>
                                </span></a>
                            </li>

                            <li class="disabled"><a href="#settings" data-toggle="tab" title="blah blah">
                                <span class="round-tabs four">
                                    <i class="glyphicon glyphicon-comment"></i>
                                </span>
                            </a></li>

                            <li class="disabled"><a href="#doner" data-toggle="tab" title="completed">
                                <span class="round-tabs five">
                                    <i class="glyphicon glyphicon-ok"></i>
                                </span></a>
                            </li>

                        </ul>
                    </div>

                    <div class="tab-content">

                        <!-- first tab -->
                            <div class="tab-pane fade in active" id="home">
                         
                                <h3 class="head text-center clear-fix">Registratie voor <% Response.Write(curEvent.Name); %></h3>
                                
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
                                    <!-- <asp:CustomValidator ID="test" runat="server" ControlToValidate="leader_iban" ClientValidationFunction="validateIban" CssClass="text-danger" Display="Dynamic" ErrorMessage="IBAN is niet geldig" ValidateEmptyText="False" ValidationGroup="RegistrationGroup"/>-->
                                </div>

                                <!-- Emailadres leader -->
                                <div class="form-group">
                                    <asp:Label runat="server" CssClass="control-label" AssociatedControlID="leader_Email">Emailadres:</asp:Label>
                                    <asp:TextBox runat="server" ID="leader_Email" CssClass="form-control input-md" placeholder="ex: johndoe@gmail.com" />
                                    <asp:RequiredFieldValidator runat="server" CssClass="text-danger" ControlToValidate="leader_Email" Display="Dynamic" ErrorMessage="Emailadres is verplicht" ValidationGroup="RegistrationGroup" />
                                </div>

                                <div class="form-group">
                                    <asp:Label runat="server" CssClass="control-label" AssociatedControlID="leader_Password">Wachtwoord</asp:Label>
                                    <asp:TextBox runat="server" ID="leader_Password" TextMode="Password" CssClass="form-control" />
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="leader_Password" CssClass="text-danger" ErrorMessage="Wachtwoord is verplicht." Display="Dynamic" ValidationGroup="RegistrationGroup" />
                                </div>

                                    <fieldset>
                                       <button type="submit" href="#profile" name="home_form" class="btn-submit btn btn-success btn-outline-rounded green right">Next tab <span style="margin-left: 10px;" class="glyphicon glyphicon-send"></span></button>
                                    </fieldset>
                                </div>   
                        </div>

                        <!-- second -->
                        <div class="tab-pane fade" id="profile">
                            <h3 class="head text-center">Reserveerders aangeven:<sup>™</sup> Profile</h3>
    
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
                                <fieldset>
                                    <a href="#messages">Volgende stap</a>
                                    <!--<button type="submit" href="#messages" name="profile_form" class="btn-submit btn btn-success btn-outline-rounded green"> <span style="margin-left: 10px;" class="glyphicon glyphicon-send"></span></button>-->
                                </fieldset>

                        </div>
                        <!-- third -->
                        <div class="tab-pane fade" id="messages">
                            <h3 class="head text-center">Locatie kiezen:</h3>
                            <div class="col-md-6">
                                <h5>Evenementnaam: <strong><% Response.Write(curEvent.Name); %></strong></h5>
                                <h5>Startdatum: <% Response.Write(curEvent.StartDate.ToString("dd-M-yyyy")); %></h5>
                                <h5>Einddatum: <% Response.Write(curEvent.EndDate.ToString("dd-M-yyyy")); %></h5>
                                <h5>Capaciteit: <% Response.Write(curEvent.Capacity); %></h5>
                            </div>

                            <div class="col-md-6">
                                <h5>Locatie naam: <% Response.Write(LogicCollection.LocationLogic.GetById(curEvent.LocationID).Name); %></h5>
                                <h5>Locatie adres: <% Response.Write(LogicCollection.LocationLogic.GetById(curEvent.LocationID).Address + " " + LogicCollection.LocationLogic.GetById(curEvent.LocationID).Number); %></h5>
                                <h5>Locatie stad: <% Response.Write(LogicCollection.LocationLogic.GetById(curEvent.LocationID).City); %></h5>
                                <h5>Locatie adres: <% Response.Write(LogicCollection.LocationLogic.GetById(curEvent.LocationID).PostalCode); %></h5>
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
                            </div>

                            <div class="col-md-6">
                                <br />
                                <div id="informationPlace" class="alert" role="alert">
                                </div>
                            </div>
                                <fieldset>
                                    <button type="submit" href="#settings" name="messages_form" class="btn-submit btn btn-success btn-outline-rounded green">Next tab <span style="margin-left: 10px;" class="glyphicon glyphicon-send"></span></button>
                                </fieldset>
                        </div>
                        <!-- fourth -->
                        <div class="tab-pane fade" id="settings">
                            <h3 class="head text-center">Drop comments!</h3>

                            <form class="form-horizontal text-center" id="settings_form" name="settings_form" role="form">
                                <fieldset>
                                    <button type="submit" href="#doner" name="settings_form" class="btn-submit btn btn-success btn-outline-rounded green">Next tab <span style="margin-left: 10px;" class="glyphicon glyphicon-send"></span></button>
                                </fieldset>
                            </form>
                        </div>
                        <!-- fifth -->
                        <div class="tab-pane fade" id="doner">
                            <div class="text-center">
                                <i class="img-intro icon-checkmark-circle"></i>
                            </div>
                            <h3 class="head text-center">thanks for staying tuned! <span style="color: #f48260;">♥</span> Bootstrap</h3>
                            <p class="narrow text-center">
                                Lorem ipsum dolor sit amet, his ea mollis fabellas principes. Quo mazim facilis tincidunt ut, utinam saperet facilisi an vim.
                            </p>
                        </div>
                        <div class="clearfix"></div>
                    </div>

                </div>
            </div>
        </div>
    </section>
                   
    
    <div class="clearfix"></div>
    <hr />
    <div class="form-group">
        <asp:Button ID="btnSubmit" runat="server" Text="Registereren" CssClass="btn btn-primary" OnClick="btnSubmit_Click" CausesValidation="true" ValidationGroup="RegistrationGroup" />
    </div>

<script src="../../Scripts/iban.js"></script>
<script>
    
    $(function () {
        $('a[title]').tooltip();

        $('.btn-submit').on('click', function (e) {

            var formname = $(this).attr('name');
            var tabname = $(this).attr('href');

            if ($('#' + formname)[0].checkValidity()) { 
                e.preventDefault();
                $('ul.nav li a[href="' + tabname + '"]').parent().removeClass('disabled');
                $('ul.nav li a[href="' + tabname + '"]').trigger('click');
            }

        });

        $('ul.nav li').on('click', function (e) {
            if ($(this).hasClass('disabled')) {
                e.preventDefault();
                return false;
            }
        });
    });


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

<style>
@import url(http://fonts.googleapis.com/css?family=Roboto+Condensed:400,700);
.board{
width: 90%;
margin: 60px auto;
height: auto;
background: #fff;
}
.board .nav-tabs {
    position: relative;
    margin: 40px auto;
    margin-bottom: 0;
    box-sizing: border-box;

}

.board > div.board-inner{
    background: #fafafa url(http://subtlepatterns.com/patterns/geometry2.png);
    background-size: 30%;
}

p.narrow{
    width: 60%;
    margin: 10px auto;
}

.liner{
    height: 2px;
    background: #ddd;
    position: absolute;
    width: 80%;
    margin: 0 auto;
    left: 0;
    right: 0;
    top: 50%;
    z-index: 1;
}

.nav-tabs > li.active > a, .nav-tabs > li.active > a:hover, .nav-tabs > li.active > a:focus {
    color: #555555;
    cursor: default;
    border: 0;
    border-bottom-color: transparent;
}

span.round-tabs{
    width: 70px;
    height: 70px;
    line-height: 70px;
    display: inline-block;
    border-radius: 100px;
    background: white;
    z-index: 2;
    position: absolute;
    left: 0;
    text-align: center;
    font-size: 25px;
}

span.round-tabs.one{
    color: rgb(34, 194, 34);border: 2px solid rgb(34, 194, 34);
}

li.active span.round-tabs.one{
    background: #fff !important;
    border: 2px solid #ddd;
    color: rgb(34, 194, 34);
}

span.round-tabs.two{
    color: #febe29;border: 2px solid #febe29;
}

li.active span.round-tabs.two{
    background: #fff !important;
    border: 2px solid #ddd;
    color: #febe29;
}

span.round-tabs.three{
    color: #3e5e9a;border: 2px solid #3e5e9a;
}

li.active span.round-tabs.three{
    background: #fff !important;
    border: 2px solid #ddd;
    color: #3e5e9a;
}

span.round-tabs.four{
    color: #f1685e;border: 2px solid #f1685e;
}

li.active span.round-tabs.four{
    background: #fff !important;
    border: 2px solid #ddd;
    color: #f1685e;
}

span.round-tabs.five{
    color: #999;border: 2px solid #999;
}

li.active span.round-tabs.five{
    background: #fff !important;
    border: 2px solid #ddd;
    color: #999;
}

.nav-tabs > li.active > a span.round-tabs{
    background: #fafafa;
}
.nav-tabs > li {
    width: 20%;
}
li:after {
    content: " ";
    position: absolute;
    left: 45%;
   opacity:0;
    margin: 0 auto;
    bottom: 0px;
    border: 5px solid transparent;
    border-bottom-color: #ddd;
    transition:0.1s ease-in-out;
    
}
li.active:after {
    content: " ";
    position: absolute;
    left: 45%;
   opacity:1;
    margin: 0 auto;
    bottom: 0px;
    border: 10px solid transparent;
    border-bottom-color: #ddd;
    
}
.nav-tabs > li a{
   width: 70px;
   height: 70px;
   margin: 20px auto;
   border-radius: 100%;
   padding: 0;
}

.nav-tabs > li a:hover{
    background: transparent;
}

.tab-content{
}
.tab-pane{
   position: relative;
padding-top: 50px;
}
.tab-content .head{
    font-family: 'Roboto Condensed', sans-serif;
    font-size: 25px;
    text-transform: uppercase;
    padding-bottom: 10px;
}
.btn-outline-rounded{
    padding: 10px 40px;
    margin: 20px 0;
    border: 2px solid transparent;
    border-radius: 25px;
}

.btn.green{
    background-color:#5cb85c;
    color: #ffffff;
}


@media( max-width : 585px ){
    
    .board {
width: 90%;
height:auto !important;
}
    span.round-tabs {
        font-size:16px;
width: 50px;
height: 50px;
line-height: 50px;
    }
    .tab-content .head{
        font-size:20px;
        }
    .nav-tabs > li a {
width: 50px;
height: 50px;
line-height:50px;
}

li.active:after {
content: " ";
position: absolute;
left: 35%;
}

.btn-outline-rounded {
    padding:12px 20px;
    }
}

</style>

</asp:Content>


