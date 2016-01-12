<%@ Page Title="Log in" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="ICT4Events_Web.Account.Login" Async="true" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <h2><%: Title %></h2>
    <br />
    <asp:Label ID="lblNotLoggedIn" CssClass="alert alert-success clearfix" runat="server" Text="" Visible="False"></asp:Label>
    <div class="row">
        <div class="col-md-8">
            <section id="loginForm" runat="server">
              <asp:Label ID="errorLabel" Runat="server" CssClass="alert alert-danger show" role="alert" Visible="false"></asp:Label>
                <div class="form-horizontal">
                    <div class="form-group">
                        <asp:Label runat="server" AssociatedControlID="Email" CssClass="col-md-2 control-label">Emailadres</asp:Label>
                        <div class="col-md-10">
                            <asp:TextBox runat="server" ID="Email" CssClass="form-control" placeholder="Ex: johndoe@gmail.com" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="Email"
                                CssClass="text-danger" ErrorMessage="Emailadres is verplicht." Display="Dynamic" />
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" AssociatedControlID="Password" CssClass="col-md-2 control-label">Wachtwoord</asp:Label>
                        <div class="col-md-10">
                            <asp:TextBox runat="server" ID="Password" TextMode="Password" CssClass="form-control" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="Password" CssClass="text-danger" ErrorMessage="Wachtwoord is verplicht." Display="Dynamic" />
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-md-offset-2 col-md-10">
                            <div class="checkbox">
                                <asp:CheckBox runat="server" ID="RememberMe" />
                                <asp:Label runat="server" AssociatedControlID="RememberMe">Onthouden?</asp:Label><br/>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-md-offset-2 col-md-10">
                            <asp:Button runat="server" OnClick="LogIn" Text="Inloggen" CssClass="btn btn-primary"/><br/>
                        </div>
                    </div>
                </div>
                <p>
                    <a href="../ReservationSystem/Register.aspx">Registreren</a>
                </p>
                <p>
                    <%-- Enable this once you have account confirmation enabled for<a href="../ReservationSystem/Register.aspx">../ReservationSystem/Register.aspx</a> password reset functionality
                    <asp:HyperLink runat="server" ID="ForgotPasswordHyperLink" ViewStateMode="Disabled">Forgot your password?</asp:HyperLink>
                    --%>
                </p>
            </section>
        </div>
    </div>
</asp:Content>