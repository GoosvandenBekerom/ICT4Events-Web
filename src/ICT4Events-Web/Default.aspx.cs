using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SharedModels.Logic;
using SharedModels.Models;

namespace ICT4Events_Web
{
    public partial class Default : Page
    {
        public List<ReservationWristband> CurrentReservationsWristbands;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (SiteMaster.CurrentUser() == null) return;
            var current = SiteMaster.CurrentUser();
            CurrentReservationsWristbands = LogicCollection.ReservationWristbandLogic.GetReservationByUserIdAll(current.ID);

            foreach (var reservationWristband in CurrentReservationsWristbands)
            {
                var reservation = LogicCollection.ReservationLogic.GetByID(reservationWristband.ReservationId);

                var hdfReservationId = (HiddenField)lgnView.FindControl("hdf_reservation");
                hdfReservationId.Value = reservation.ID.ToString();
            }

            // Succes message
            var currentUrl = Request.Url.AbsoluteUri; // current url
            var paid = Convert.ToInt32(HttpUtility.ParseQueryString(new Uri(currentUrl).Query).Get("paid"));
            if (paid == 1) { lgnView.FindControl("feedbackPanelPaid").Visible = true; }
        }
        

        protected void btnPay_Click(object sender, EventArgs e)
        {
            var hdfReservationId = (HiddenField)lgnView.FindControl("hdf_reservation");
            var id = Convert.ToInt32(hdfReservationId.Value);
            var updateRes = LogicCollection.ReservationLogic.GetByID(id);
            updateRes.Paid = true;

            if (!LogicCollection.ReservationLogic.UpdateReservation(updateRes)) return;
            
            // Redirect with query
            var redirect = "http://" + Request.Url.Authority + "" + Request.Url.AbsolutePath + "?paid=1";
            Page.Response.Redirect(redirect);
        }
    }
}