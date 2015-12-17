using System;
using System.Web.UI;
using SharedModels.Data.OracleContexts;
using SharedModels.Models;

namespace ICT4Events_Web
{
    public partial class Webform : Page
    {
        PersonOracleContext personContext = new PersonOracleContext();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                // stuff
            }
            else
            {
                // stuff
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            feedbackPanel.Visible = true;

            if (!IsValid)
            {
                Label1.Text = "Invalide registratie!";
                Label1.CssClass = "text-danger";
                return;
            }

            var firstname = first_name.Text;
            var surname = last_name.Text;
            var pAddress = address.Text;
            var pCity = city.Text;
            var ib = iban.Text;

            var person = new Person(0, firstname, surname, pAddress, pCity, ib);

            if (!personContext.Insert(person)) return;

            Label1.Visible = true;
            Label1.Text = 
                (IsValid ? "Succesvol!" : "Onsuccesvol") +
                "<br />Voornaam: " + firstname + 
                "<br />Achternaam: " + surname + 
                "<br />Adres: " + pAddress + 
                "<br />Woonplaats: " + pCity + 
                "<br />IBAN: " +ib;
        }
    }
}