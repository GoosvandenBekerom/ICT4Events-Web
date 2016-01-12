<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ICT4Events_Web.Default" %>
<%@ Import Namespace="ICT4Events_Web" %>
<%@ Import Namespace="SharedModels.Logic" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    
 <asp:LoginView runat="server" ID="lgnView">
   <AnonymousTemplate>
        <h2>Welkom bij ICT4Events!</h2>
        <h3>Huidige evenementen:</h3>
            <% //TODO: LOAD EVENT %>
        <p>
            <a class="btn btn-info" href="Views/ReservationSystem/Register.aspx">Registreer vandaag nog</a>
        </p>
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
