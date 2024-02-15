using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Esercizio
{
    public partial class HomePage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CaricaAuto();
                CaricaOptionals();
            }
        }

        private void CaricaAuto()
        {
            string connString = ConfigurationManager.ConnectionStrings["ConnessioneDBLocale"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand("SELECT IDAuto, Nome, PrezzoDiPartenza, UrlImmagine FROM AUTO", conn);

                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                ddlAuto.DataSource = reader;
                ddlAuto.DataTextField = "Nome";
                ddlAuto.DataValueField = "IDAuto";
                ddlAuto.DataBind();
                conn.Close();
            }
        }

        private void CaricaOptionals()
        {
            string connString = ConfigurationManager.ConnectionStrings["ConnessioneDBLocale"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand("SELECT IDOptional, Descrizione, Prezzo FROM Optionals", conn);

                conn.Open();

                cblOptional.DataSource = cmd.ExecuteReader();
                cblOptional.DataTextField = "Descrizione";
                cblOptional.DataValueField = "IDOptional";
                cblOptional.DataBind();
                conn.Close();
            }
        }

        protected void ddlAuto_SelectedIndexChanged(object sender, EventArgs e)
        {
            string connString = ConfigurationManager.ConnectionStrings["ConnessioneDBLocale"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand("SELECT PrezzoDiPartenza, UrlImmagine FROM Auto WHERE IDAuto = @IDAuto", conn);
                cmd.Parameters.AddWithValue("@IDAuto", ddlAuto.SelectedValue);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    lblPrezzoDiPartenza.Text = "Prezzo di partenza: " + reader["PrezzoDiPartenza"].ToString() + " EUR";
                    imgAuto.ImageUrl = reader["UrlImmagine"].ToString();
                }
                conn.Close();
            }
        }

      

        protected void btnCalcola_Click(object sender, EventArgs e)
        {
            decimal prezzoDiPartenza = 0;
            decimal prezzoOptionals = 0;
            int anniGaranzia = 0;
            decimal prezzoGaranzia = 120.00m; // Prezzo per anno di garanzia

            string connString = ConfigurationManager.ConnectionStrings["ConnessioneDBLocale"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlCommand cmd = new SqlCommand("SELECT PrezzoDiPartenza FROM Auto WHERE IDAuto = @IDAuto", conn);
                cmd.Parameters.AddWithValue("@IDAuto", ddlAuto.SelectedValue);
                conn.Open();
                prezzoDiPartenza = (decimal)cmd.ExecuteScalar();
                conn.Close();
            }

            // Calcolo prezzo optionals

            foreach (ListItem item in cblOptional.Items)
            {
                if (item.Selected)
                {
                    using (SqlConnection conn = new SqlConnection(connString))
                    {
                        SqlCommand cmd = new SqlCommand("SELECT Prezzo FROM Optionals WHERE IDOptional = @IDOptional", conn);
                        cmd.Parameters.AddWithValue("@IDOptional", item.Value);
                        conn.Open();
                        prezzoOptionals += (decimal)cmd.ExecuteScalar();
                        conn.Close();
                    }
                }
            }

            // Calcolo prezzo garanzia

            if (int.TryParse(txtAnniGaranzia.Text, out anniGaranzia))
            {
                lblTotaleGaranzia.Text = "Totale Garanzia: " + (prezzoGaranzia * anniGaranzia).ToString("C");

            }
            else
            {
                lblTotaleGaranzia.Text = "Anni di garanzia non validi.";
            }

            lblTotalePrezzoDiPartenza.Text = "Prezzo di base: " + prezzoDiPartenza.ToString("C");
            lblTotaleOptionals.Text = "Totale optionals: " + prezzoOptionals.ToString("C");
            lblTotalePrezzo.Text = "Prezzo Totale " + (prezzoDiPartenza + prezzoOptionals + (prezzoGaranzia * anniGaranzia)).ToString("C");
        }
    }
}