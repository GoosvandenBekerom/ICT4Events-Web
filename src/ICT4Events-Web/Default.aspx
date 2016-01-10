<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ICT4Events_Web.Default" %>
<%@ Import Namespace="ICT4Events_Web" %>
<%@ Import Namespace="SharedModels.Logic" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    
 <asp:LoginView runat="server" ID="lgnView">
   <AnonymousTemplate>
    <div class="jumbotron">
        <h1>ASP.NET</h1>
        <p class="lead">ASP.NET is a free web framework for building great Web sites and Web applications using HTML, CSS, and JavaScript.</p>
        <p><a href="http://www.asp.net" class="btn btn-primary btn-lg">Learn more &raquo;</a></p>
    </div>

    <div class="row">
        <div class="col-md-4">
            <h2>Getting started</h2>
            <p>
                ASP.NET Web Forms lets you build dynamic websites using a familiar drag-and-drop, event-driven model.
            A design surface and hundreds of controls and components let you rapidly build sophisticated, powerful UI-driven sites with data access.
            </p>
            <p>
                <a class="btn btn-default" href="http://go.microsoft.com/fwlink/?LinkId=301948">Learn more &raquo;</a>
            </p>
        </div>
        <div class="col-md-4">
            <h2>Get more libraries</h2>
            <p>
                NuGet is a free Visual Studio extension that makes it easy to add, remove, and update libraries and tools in Visual Studio projects.
            </p>
            <p>
                <a class="btn btn-default" href="http://go.microsoft.com/fwlink/?LinkId=301949">Learn more &raquo;</a>
            </p>
        </div>                                
        <div class="col-md-4">
            <h2>Web Hosting</h2>
            <p>
                You can easily find a web hosting company that offers the right mix of features and price for your applications.
            </p>
            <p>
                <a class="btn btn-default" href="http://go.microsoft.com/fwlink/?LinkId=301950">Learn more &raquo;</a>
            </p>
        </div>
    </div>
       </AnonymousTemplate>
       <LoggedInTemplate>
            <h3> Jouw huidige reserveringen: </h3>   
           <div id="feedbackPanelPaid" runat="server" class="alert alert-success" Visible="False">Je hebt succesvol betaald.</div>
          <%
           if (CurrentReservationsWristbands.Count > 0)
           {
               foreach (var reservationWristband in CurrentReservationsWristbands)
               {
                   var reservation = LogicCollection.ReservationLogic.GetByID(reservationWristband.ReservationId);
                   var person = LogicCollection.PersonLogic.GetByID(reservation.PersonId);

                   %>
                 <div class="col-md-12 well">
                	<div class="artist-data pull-left">
                        <div class="artst-prfle">
                        	<div class="art-title">
                            	<h3>ICT4Events</h3>
                                <span class="artst-sub">Gereserveerd door: <span class="byname"><%: person.Name %></span>
                            </div>
                            <p><%: $@"Reservatienummer #{reservation.ID}" %></p>
                            <div class="counter-tab">
                            	<div class="counter_comnt"><i class="glyphicon glyphicon-log-in"></i> <%: reservation.DateStart.ToShortDateString() %></div>
                            	<div class="counter_ply"><i class="glyphicon glyphicon-log-out"></i> <%: reservation.DateEnd.ToShortDateString() %></div>
                            	<div class="counter_like"><i class="glyphicon glyphicon-euro"></i> <%: (reservation.Paid ? "Betaald" : "Niet-betaald") %></div>
                            </div>
                                <% if (!reservation.Paid)
                                   {
                                %>
                                <asp:HiddenField ID="hdf_reservation" runat="server" />
                                <asp:Button ID="btnPay" runat="server" CssClass="btn btn-info" Text="Nu betalen" OnClick="btnPay_Click" />
                                <%
                                   } 
                                %>
                        </div>
                    </div>
                     <div class="clear-fix clearfix"></div>
                </div>      
           <div class="clear-fix clearfix"></div>
           <%
               }
           }
           else
           {
          %>
           <asp:Label ID="lblError" CssClass="alert alert-warning" runat="server" Text="Je heb nog geen reserveringen geplaatst."></asp:Label>    
       <%
           }
       %>
       </LoggedInTemplate>
 </asp:LoginView>
</asp:Content>
