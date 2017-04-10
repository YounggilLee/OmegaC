﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OmegaC.dsOmegaCTableAdapters;
using System.Drawing;

namespace OmegaC.SecurePages
{
   
    using TableCars = dsOmegaC.carDataTable;

    public partial class Vehicle : System.Web.UI.Page
    {       
        carTableAdapter adpCars = new carTableAdapter();
        TableCars tblcars = new TableCars();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                RefreshGridView();
            }
        }

        private void RefreshGridView()
        {
            adpCars.Fill(tblcars);  // Fill the data table
            grdCars.DataSource = tblcars;
            grdCars.DataBind();

            lblMessage.Text = "Data loaded";
            lblMessage.ForeColor = Color.Green;
        }

        protected void btnLoad_Click(object sender, EventArgs e)
        {
            string carSerial = txtCarSerial.Text;

            // Fill the data table with a row based on the carSerial
            adpCars.FillBy(tblcars, carSerial);

            // if something was fetched
            if (tblcars.Rows.Count > 0)
            {
                // assign the row to the Row object
                var row = tblcars[0];

                // assign the values from the row to the textboxes
                txtCarSerial.Text = InputData.dataInput(row.serial);
                txtCarMake.Text = InputData.dataInput(row.make);
                txtCarModel.Text = InputData.dataInput(row.model);
                txtCarYear.Text = InputData.dataInput(row.cyear);
                txtCarPrice.Text = InputData.dataInput(row.price);               

                lblMessage.Text = "Record found";
                lblMessage.ForeColor = Color.Green;
            }
            else
            {
                lblMessage.Text = "Record not found";
                lblMessage.ForeColor = Color.Red;
            }
        }

        protected void btnInsert_Click(object sender, EventArgs e)
        {
         
            int result = adpCars.Insert(txtCarSerial.Text, txtCarMake.Text, txtCarModel.Text, Convert.ToDecimal(txtCarYear.Text), Convert.ToDecimal(txtCarPrice.Text));


            if (result == 1)
            {
                RefreshGridView();
                lblMessage.Text = "New Car Added";
                lblMessage.ForeColor = Color.Green;
            }
            else
            {
                lblMessage.Text = "New Car not Added";
                lblMessage.ForeColor = Color.Red;
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            string carSerial = txtCarSerial.Text;

            // Fill the data table with a row based on the carSerial
            adpCars.FillBy(tblcars, carSerial);

            // if something was fetched
            if (tblcars.Rows.Count > 0)
            {

                var row = tblcars[0];
                row.serial = txtCarSerial.Text;
                row.make = txtCarMake.Text;
                row.model = txtCarModel.Text;
                row.cyear = Convert.ToDecimal(txtCarYear.Text);
                row.price = Convert.ToDecimal(txtCarPrice.Text);

                int result = adpCars.Update(tblcars);

                if (result == 1)
                {
                    RefreshGridView();
                    lblMessage.Text = "Car updated";
                    lblMessage.ForeColor = Color.Green;
                }
                else
                {
                    lblMessage.Text = "Car not updated";
                    lblMessage.ForeColor = Color.Red;
                }
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string carSerial = txtCarSerial.Text;

            int result = adpCars.Delete(carSerial);

            if (result == 1)
            {
                RefreshGridView();
                lblMessage.Text = "Car deleted";
                lblMessage.ForeColor = Color.Green;
            }
            else
            {
                lblMessage.Text = "Car not deleted";
                lblMessage.ForeColor = Color.Red;
            }
        }
    }
}