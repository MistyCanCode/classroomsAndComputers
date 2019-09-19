using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using ASPFinal.Classes;
using System.Text;
using System.Data;

namespace ASPFinal
{
    public partial class Software1 : System.Web.UI.Page
    {

        private static bool cEdit = false;
        private static string newSoftId = "";
        private static string editSoftwareId = "";
        private static bool noDelete = false;
        private static string nscc = "";
        int hasData = 0;
        StringBuilder table = new StringBuilder();

        protected void loadSoftware(SqlConnection cAW)
        {
            lblLicense.Text = "";
            dlSoftNum.Items.Clear();
            //dlProduct.Items.Clear();
            //dlVersion.Items.Clear();
            string strSQL = "SELECT DISTINCT Developer "
                            + "FROM Software ";

            SqlCommand command = new SqlCommand(strSQL, cAW);
            SqlDataReader SQLdr = command.ExecuteReader();

            if (SQLdr.HasRows)
            {
                while (SQLdr.Read())
                {
                    dlSoftNum.Items.Insert(dlSoftNum.Items.Count, new ListItem(SQLdr[0].ToString()));
                }
                dlSoftNum.Items.Insert(0, new ListItem("Select Software Developer", ""));
            }
            SQLdr.Close();
            SQLdr.Dispose();
            command.Dispose();
        }


        protected void loadDetail(SqlConnection cAW)
        {
            if ((dlSoftNum.SelectedItem.Value != "") && (dlProduct.SelectedItem.Value != "") && (dlVersion.SelectedItem.Value != ""))
            {
                //A number has been selected
                string strSQL = "SELECT SoftwareID, Developer, ProductName, Version, LicenseKey "
                + "FROM Software "
                + "WHERE Developer = '" + dlSoftNum.SelectedItem.Value + "' "
                + "AND ProductName = '" + dlProduct.SelectedItem.Value + "' "
                + "AND Version = '" + dlVersion.SelectedItem.Value + "'";
                SqlCommand command = new SqlCommand(strSQL, cAW);
                SqlDataReader SQLdr = command.ExecuteReader();

                if (SQLdr.HasRows)
                {
                    while (SQLdr.Read())
                    {
                        //save the ID of the current User
                        editSoftwareId = SQLdr["SoftwareID"].ToString();
                        //load the main page
                        //lblDeveloper.Text = SQLdr["Developer"].ToString();
                        //lblProduct.Text = SQLdr["ProductName"].ToString();
                        //lblVersion.Text = SQLdr["Version"].ToString();
                        lblLicense.Text = SQLdr["LicenseKey"].ToString();

                        //load the modal in case of an edit
                        txtSoftID.Text = SQLdr["SoftwareID"].ToString();
                        txtDeveloper.Text = SQLdr["Developer"].ToString();
                        txtProduct.Text = SQLdr["ProductName"].ToString();
                        txtVersion.Text = SQLdr["Version"].ToString();
                        txtLicenseKey.Text = SQLdr["LicenseKey"].ToString();
                    }
                    
                }
                SQLdr.Close();
                SQLdr.Dispose();
                command.Dispose();
            }
            else
            {
                dlProduct.Items.Clear();
                dlVersion.Items.Clear();
                //No Customer has been selected. Clear the controls
                //lblDeveloper.Text = "";
                //lblProduct.Text = "";
                //lblVersion.Text = "";
                lblLicense.Text = "";

                //clear the modal form
                txtSoftID.Text = "";
                txtDeveloper.Text = "";
                txtProduct.Text = "";
                txtVersion.Text = "";
                txtLicenseKey.Text = "";
            }
        }


        protected void loadProductName(SqlConnection cAW)
        {
            ////use and active the code below:
            //dlProduct.Items.Clear();
            //if (dlSoftNum.SelectedItem.Value != "")
            //{
            //    //A number has been selected
            //    string strSQL = "SELECT DISTINCT ProductName "
            //    + "FROM Software "
            //    + "WHERE Developer = '" + dlSoftNum.SelectedItem.Value + "'";
            //    SqlCommand command = new SqlCommand(strSQL, cAW);
            //    SqlDataReader SQLdr = command.ExecuteReader();

            //    if (SQLdr.HasRows)
            //    {
            //        while (SQLdr.Read())
            //        {
            //            dlSoftNum.Items.Insert(dlSoftNum.Items.Count, new ListItem(SQLdr[0].ToString()));
            //        }
            //        dlSoftNum.Items.Insert(0, new ListItem("Select Software Developer", ""));
            //    }
            //    SQLdr.Close();
            //    SQLdr.Dispose();
            //    command.Dispose();
            //}
            //else
            //{
            //    dlProduct.Items.Clear();
            //    //No Customer has been selected. Clear the controls
            //}


            //or avtive and use the code below
            lblLicense.Text = "";
            dlProduct.Items.Clear();
            //dlBuilding.SelectedIndex = -1;
            dlVersion.Items.Clear();
            if (dlSoftNum.SelectedItem.Value != "")
            {
                //A customer has been selected
                string strSQL = "SELECT DISTINCT ProductName FROM Software WHERE " +
                                "Developer = '" + dlSoftNum.SelectedItem.Value + "'";
                SqlCommand command = new SqlCommand(strSQL, cAW);
                SqlDataReader SQLdr = command.ExecuteReader();

                if (SQLdr.HasRows)
                {
                    while (SQLdr.Read())
                    {
                        if (SQLdr[0].ToString() != "")
                        {
                            string test = SQLdr[0].ToString();
                            dlProduct.Items.Insert(dlProduct.Items.Count, new ListItem(SQLdr[0].ToString()));
                        }                       
                        //Save the ID of the current customer
                        //editCampusName = SQLdr["CustomerID"].ToString();
                        //Load the main page
                        //lblFName.Text = SQLdr["FirstName"].ToString();
                        //lblMI.Text = SQLdr["MiddleName"].ToString();
                        //lblLName.Text = SQLdr["LastName"].ToString();
                        //lblCompanyName.Text = SQLdr["CompanyName"].ToString();

                        //Load the modal in case of an edit
                        //customerId = SQLdr["FirstName"].ToString(); //Must save the CustomerID for later use
                        //txtFName.Text = SQLdr["FirstName"].ToString();
                        //txtMI.Text = SQLdr["MiddleName"].ToString();
                        //txtLName.Text = SQLdr["LastName"].ToString();
                        //txtCompanyName.Text = SQLdr["CompanyName"].ToString();

                    }
                    dlProduct.Items.Insert(0, new ListItem("Select Software", ""));
                }
                //dlBuilding.SelectedIndex = 0;
                if (dlProduct.SelectedIndex == -1)
                {
                    dlProduct.Items.Insert(0, new ListItem("No Building Number Found", ""));
                    dlProduct.Enabled = false;
                    dlProduct.AutoPostBack = false;
                    SQLdr.Close();
                    SQLdr.Dispose();
                    command.Dispose();
                    //loadDetailRoom(cAW);
                }
                else
                {
                    //campusName = dlCampusName.SelectedItem.Value;
                    dlProduct.AutoPostBack = true;
                    dlProduct.Enabled = true;
                    //dlProduct.Items.Insert(0, new ListItem("Select Software", ""));
                    SQLdr.Close();
                    SQLdr.Dispose();
                    command.Dispose();
                }


            }
            else
            {
                dlProduct.Items.Clear();
                //No Customer has been selected. Clear the controls
                //lblFName.Text = "";
                //lblMI.Text = "";
                //lblLName.Text = "";
                //lblCompanyName.Text = "";

                ////Clear out the modal form
                //txtFName.Text = "";
                //txtMI.Text = "";
                //txtLName.Text = "";
                //txtCompanyName.Text = "";
            }
        }

        protected void loadVersion(SqlConnection cAW)
        {
            ////use and active the code below:
            //dlProduct.Items.Clear();
            //if (dlSoftNum.SelectedItem.Value != "")
            //{
            //    //A number has been selected
            //    string strSQL = "SELECT DISTINCT ProductName "
            //    + "FROM Software "
            //    + "WHERE Developer = '" + dlSoftNum.SelectedItem.Value + "'";
            //    SqlCommand command = new SqlCommand(strSQL, cAW);
            //    SqlDataReader SQLdr = command.ExecuteReader();

            //    if (SQLdr.HasRows)
            //    {
            //        while (SQLdr.Read())
            //        {
            //            dlSoftNum.Items.Insert(dlSoftNum.Items.Count, new ListItem(SQLdr[0].ToString()));
            //        }
            //        dlSoftNum.Items.Insert(0, new ListItem("Select Software Developer", ""));
            //    }
            //    SQLdr.Close();
            //    SQLdr.Dispose();
            //    command.Dispose();
            //}
            //else
            //{
            //    dlProduct.Items.Clear();
            //    //No Customer has been selected. Clear the controls
            //}


            //or avtive and use the code below

            //dlBuilding.SelectedIndex = -1;
            lblLicense.Text = "";
            dlVersion.Items.Clear();
            if ((dlSoftNum.SelectedItem.Value != "") && (dlProduct.SelectedItem.Value != ""))
            {
                //A customer has been selected
                string strSQL = "SELECT DISTINCT Version, LicenseKey "
                + "FROM Software "
                + "WHERE Developer = '" + dlSoftNum.SelectedItem.Value + "' "
                + "AND ProductName = '" + dlProduct.SelectedItem.Value + "'";

                SqlCommand command = new SqlCommand(strSQL, cAW);
                SqlDataReader SQLdr = command.ExecuteReader();

                if (SQLdr.HasRows)
                {
                    while (SQLdr.Read())
                    {
                        if (SQLdr[0].ToString() != "")
                        {
                            string test = SQLdr[0].ToString();
                            dlVersion.Items.Insert(dlVersion.Items.Count, new ListItem(SQLdr[0].ToString()));
                        }                        
                        //Save the ID of the current customer
                        //editCampusName = SQLdr["CustomerID"].ToString();
                        //Load the main page
                        //lblFName.Text = SQLdr["FirstName"].ToString();
                        //lblMI.Text = SQLdr["MiddleName"].ToString();
                        //lblLName.Text = SQLdr["LastName"].ToString();
                        //lblCompanyName.Text = SQLdr["CompanyName"].ToString();

                        //Load the modal in case of an edit
                        //customerId = SQLdr["FirstName"].ToString(); //Must save the CustomerID for later use
                        //txtFName.Text = SQLdr["FirstName"].ToString();
                        //txtMI.Text = SQLdr["MiddleName"].ToString();
                        //txtLName.Text = SQLdr["LastName"].ToString();
                        //txtCompanyName.Text = SQLdr["CompanyName"].ToString();

                    }
                    dlVersion.Items.Insert(0, new ListItem("Select Software Version", ""));
                }
                //dlBuilding.SelectedIndex = 0;
                if (dlVersion.SelectedIndex == -1)
                {
                    dlVersion.Items.Insert(0, new ListItem("No Version Found", ""));
                    dlVersion.Enabled = false;
                    dlVersion.AutoPostBack = false;
                    SQLdr.Close();
                    SQLdr.Dispose();
                    command.Dispose();
                    //loadDetailRoom(cAW);
                }
                else
                {
                    //campusName = dlCampusName.SelectedItem.Value;
                    dlVersion.AutoPostBack = true;
                    dlVersion.Enabled = true;
                    //dlVersion.Items.Insert(0, new ListItem("Select Software Version", ""));
                    SQLdr.Close();
                    SQLdr.Dispose();
                    command.Dispose();
                }


            }
            else
            {
                dlVersion.Items.Clear();
                //No Customer has been selected. Clear the controls
                //lblFName.Text = "";
                //lblMI.Text = "";
                //lblLName.Text = "";
                //lblCompanyName.Text = "";

                ////Clear out the modal form
                //txtFName.Text = "";
                //txtMI.Text = "";
                //txtLName.Text = "";
                //txtCompanyName.Text = "";
            }
        }


        protected void SaveEditUser(SqlConnection cAW)
        {
            if (dlSoftNum.SelectedItem.Value != "")
            {
                //ANum selected
                string saveEditString = "UPDATE Software SET " +
                    "Developer = '" + txtDeveloper.Text + "', " +
                    "ProductName = '" + txtProduct.Text + "', " +
                    "Version = '" + txtVersion.Text + "', " +
                    "LicenseKey = '" + txtLicenseKey.Text + "' " +
                    "WHERE SoftwareID = " + Convert.ToInt32(txtSoftID.Text) + " " +
                    "AND Developer = '" + dlSoftNum.SelectedItem.Value + "' " +
                    "AND ProductName = '" + dlProduct.SelectedItem.Value + "' " +
                    "AND Version = '" + dlVersion.SelectedItem.Value + "' " +
                    "AND LicenseKey = '" + lblLicense.Text + "' ";
                SqlCommand command = new SqlCommand(saveEditString, cAW);
                int recAffected = command.ExecuteNonQuery();

                command.Dispose();
            }
        }
        protected void SaveNewSoftware(SqlConnection cAW)
        {
            //ANum selected
            string strSQL = "INSERT Software" +
                " (Developer, ProductName, Version, LicenseKey) VALUES ('"                
                + txtDeveloper.Text + "', '"
                + txtProduct.Text + "', '"
                + txtVersion.Text + "', '"
                + txtLicenseKey.Text + "')";

            //string strSQL =
            //    "INSERT Computers " +
            //    "(NSCCEquipmentNumber, Make, Model, SerialNumber, PurchaseOrderNumber, " +
            //    "PurchaseDate, WarrantyStartDate, LocationID, Building, Room) " +
            //    "VALUES ('"
            //    + txtNSCCEquipmentNumber.Text + "', '"
            //    + txtMake.Text + "', '"
            //    + txtModel.Text + "', '"
            //    + txtSerialNumber.Text + "', '"
            //    + txtPurchaseOrderNumber.Text + "', '"
            //    + Convert.ToDateTime(txtPurchaseDate.Text) + "', '"
            //    + Convert.ToDateTime(txtWarrantyStartDate.Text) + "', "
            //    + ddlLocationID.SelectedItem.Value + ", '"
            //    + txtBuilding.Text + "', '"
            //    + txtRoom.Text + "')";
            SqlCommand command = new SqlCommand(strSQL, cAW);
            int recAffected = command.ExecuteNonQuery();
            if (recAffected == 1)
            {
                string strID = "SELECT @@Identity";

                //dlSoftNum.Text = txtDeveloper.Text;
                //dlProduct.Text = txtProduct.Text;
                //dlVersion.Text = txtVersion.Text;
                //lblLicense.Text = txtLicenseKey.Text;
                               
                               
                SqlCommand commandID = new SqlCommand(strID, cAW);
                SqlDataReader SQLdr = commandID.ExecuteReader();

                if (SQLdr.HasRows)
                {
                    while (SQLdr.Read())
                    {
                        //Load the main page
                        //newComputerID = SQLdr[0].ToString();
                    }
                }

                SQLdr.Close();
                SQLdr.Dispose();
                command.Dispose();
            }

            command.Dispose();



            //SqlCommand command = new SqlCommand(userString, cAW);
            //int recAffected = command.ExecuteNonQuery();
            //if (recAffected == 1)
            //{
            //    string strID = "Select ";
            //    SqlCommand commandID = new SqlCommand(strID, cAW);
            //    SqlDataReader SQLdr = commandID.ExecuteReader();

            //    if (SQLdr.HasRows)
            //    {
            //        while (SQLdr.Read())
            //        {
            //            //load the main page
            //            newSoftId = SQLdr[0].ToString();
            //        }
            //    }
            //    SQLdr.Close();
            //    SQLdr.Dispose();
            //    command.Dispose();
            //}
            //command.Dispose();
        }
        protected void DeleteSoftware(SqlConnection cAW)
        {

            nscc = "";
            hasData = 0;
            string strSQL = "SELECT SoftwareID, NSCCEquipmentNumber "
                          + "From SoftwareInstallations " +
                            "WHERE SoftwareID = " + Convert.ToInt32(txtSoftID.Text);
                 
            SqlCommand command1 = new SqlCommand(strSQL, cAW);
            SqlDataReader SQLdr = command1.ExecuteReader();

            if (SQLdr.HasRows)
            {
                hasData++;
                while (SQLdr.Read())
                {
                    hasData++;
                    if (nscc == "")
                        nscc = SQLdr[1].ToString();
                    else
                        nscc += ", " + SQLdr[1].ToString();
                }
                //dlSoftNum.Items.Insert(0, new ListItem("Select Software Developer", ""));
            }
            SQLdr.Close();
            SQLdr.Dispose();
            command1.Dispose();

            if (hasData == 0)
            {
                string deleteString = "DELETE FROM Software WHERE SoftwareID = " + Convert.ToInt32(txtSoftID.Text) + ";";

                SqlCommand command = new SqlCommand(deleteString, cAW);
                int recAffected = command.ExecuteNonQuery();

                command.Dispose();
            }
            else
            {
                noDelete = true;
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage",
                              "alert('Sorry Can't delete that software! The Software is used by the Computers: " + nscc + " ')", true);
            }



        }

        protected void ClearFormModel ()
        {
            dlSoftNum.SelectedIndex = 0;
            dlProduct.Items.Clear();
            dlVersion.Items.Clear();
            //No Customer has been selected. Clear the controls
            //lblDeveloper.Text = "";
            //lblProduct.Text = "";
            //lblVersion.Text = "";
            lblLicense.Text = "";

            //clear the modal form

            txtSoftID.Text = "This number is automatically generated";
            txtSoftID.Enabled = false;

            txtSoftID.Text = "";
            txtDeveloper.Text = "";
            txtProduct.Text = "";
            txtVersion.Text = "";
            txtLicenseKey.Text = "";

        }


        void PopulateGridview(SqlConnection cAW)
        {

            DataTable dtbl = new DataTable();

            string strSQL1 = "Select * from Software Order by Developer";

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(strSQL1, cAW);
            DataTable softwaretbl = new DataTable();
            sqlDataAdapter.Fill(softwaretbl);

            sqlDataAdapter.Dispose();

            gvSoftware.DataSource = softwaretbl;
            gvSoftware.DataBind();
            //gvSoftware.Rows[0].Cells.Clear();
            //gvSoftware.Rows[0].Cells.Add(new TableCell());
            //gvSoftware.Rows[0].Cells[0].ColumnSpan = dtbl.Columns.Count;
            ////gvSoftware.Rows[0].Cells[0].Text = "No Data Found ..!";
            //gvSoftware.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;


        }



        protected void Page_Load(object sender, EventArgs e)
        {

            //To stop the user from entering this page if they are not logged in.
            string strValidLogin = "";
            string userType = "";
            if (Session["VALIDLOGIN"] != null)
            {
                strValidLogin = Session["VALIDLOGIN"].ToString();
                userType = Session["USERTYPE"].ToString();
                if (strValidLogin == "false")
                    Response.Redirect("Default.aspx");
            }
            else
                Response.Redirect("Default.aspx");

            if (dlProduct.SelectedIndex == -1 || dlSoftNum.SelectedIndex == 0)
            {
                dlProduct.Enabled = false;
            }
            else
            {
                dlProduct.Enabled = true;
            }
            if (dlVersion.SelectedIndex == -1 || dlProduct.SelectedIndex == 0 || dlSoftNum.SelectedIndex == 0)
            {
                dlVersion.Enabled = false;
            }
            else
            {
                dlVersion.Enabled = true;
            }


            try
            {
                //Open a connection to the database
                string conStr = ConfigurationManager.ConnectionStrings["conAW"].ConnectionString;
                SqlConnection conAW = new SqlConnection(conStr);
                conAW.Open();

                if (!IsPostBack)
                {
                    //We are loading the page for the first time
                    loadSoftware(conAW);
                    PopulateGridview(conAW);
                }
                else
                {
                    //We need to find out what control caused the postback
                    getPostBackControl clsCtl = new getPostBackControl();
                    string ctl = clsCtl.getPostBackControlName(Page);
                    PopulateGridview(conAW);
                    switch (ctl)
                    {
                        case "dlSoftNum":
                            loadProductName(conAW);
                            //loadDetail(conAW);
                            break;
                        case "dlProduct":                            
                            loadVersion(conAW);
                            //loadDetail(conAW);
                            break;
                        case "dlVersion":
                            //loadVersion(conAW);
                            loadDetail(conAW);
                            break;
                        case "btnEdit":
                            //Call the js function to show the modal popup
                            lblAddEditSoftware.InnerHtml = "Edit Software";
                            txtSoftID.Enabled = false;
                            cEdit = true;
                            Page.ClientScript.RegisterStartupScript(this.GetType(),
                                "myScript", "initMyModal('mdlNewEditSoftware');", true);
                            break;
                        case "btnNewSoftwareSave":
                            //We need to see if we are saving or editing
                            if (cEdit)
                            {
                                //saving an edited user
                                SaveEditUser(conAW);
                                cEdit = false;
                                loadSoftware(conAW);
                                //select the edited user record
                                for (int Loop = 0; Loop < dlSoftNum.Items.Count; Loop++)
                                {
                                    if (dlSoftNum.Items[Loop].Value == txtDeveloper.Text)
                                    {
                                        //we have found the edited user
                                        dlSoftNum.SelectedIndex = Loop;
                                    }
                                }
                                loadProductName(conAW);
                                for (int Loop = 0; Loop < dlProduct.Items.Count; Loop++)
                                {
                                    if (dlProduct.Items[Loop].Value == txtProduct.Text)
                                    {
                                        //we found the new user
                                        dlProduct.SelectedIndex = Loop;
                                    }
                                }
                                loadVersion(conAW);
                                for (int Loop = 0; Loop < dlVersion.Items.Count; Loop++)
                                {
                                    if (dlVersion.Items[Loop].Value == txtVersion.Text)
                                    {
                                        //we found the new user
                                        dlVersion.SelectedIndex = Loop;
                                    }
                                }


                                loadDetail(conAW);
                            }
                            else
                            {
                                //we are saving a new user
                                SaveNewSoftware(conAW);
                                //reload the list
                                loadSoftware(conAW);
                                //now select the new user record
                                for (int Loop = 0; Loop < dlSoftNum.Items.Count; Loop++)
                                {
                                    if (dlSoftNum.Items[Loop].Value == txtDeveloper.Text)
                                    {
                                        //we found the new user
                                        dlSoftNum.SelectedIndex = Loop;
                                    }
                                }
                                loadProductName(conAW);
                                for (int Loop = 0; Loop < dlProduct.Items.Count; Loop++)
                                {
                                    if (dlProduct.Items[Loop].Value == txtProduct.Text)
                                    {
                                        //we found the new user
                                        dlProduct.SelectedIndex = Loop;
                                    }
                                }
                                loadVersion(conAW);
                                for (int Loop = 0; Loop < dlVersion.Items.Count; Loop++)
                                {
                                    if (dlVersion.Items[Loop].Value == txtVersion.Text)
                                    {
                                        //we found the new user
                                        dlVersion.SelectedIndex = Loop;
                                    }
                                }
                                //reload the form
                                loadDetail(conAW);
                            }
                            break;
                        case "btnAdd":
                            ClearFormModel();
                            //clear out modal form
                            //txtSoftID.Text = "This is Auto Generate Number";
                            //txtSoftID.Enabled = false;
                            //txtProduct.Text = "";
                            //txtDeveloper.Text = "";
                            //txtLicenseKey.Text = "";
                            //txtVersion.Text = "";
                            lblAddEditSoftware.InnerHtml = "Add new Software";
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "mysScript",
                                "initMyModal('mdlNewEditSoftware');", true);
                            break;
                        case "btnNewSoftwareCancel":
                            cEdit = false;
                            break;
                        case "btnDelete":
                            //string alert = "";
                            //BootstrapAlert(lblMsg, "Congrats! You've won a dismissable booty message.",
                            //        BootstrapAlertType.Success, True);
                            //ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                            if (dlSoftNum.SelectedIndex == 0 || dlProduct.SelectedIndex == 0 || dlVersion.SelectedIndex == 0 )
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", 
                                    "alert('Please select Developer, Product Name, and Version')", true);
                            else
                            {
                                lblDeleteSoftMsg.InnerHtml = "Are you sure you want to delete the Software: "
                                    + dlProduct.SelectedItem.Value + " " + dlVersion.SelectedItem.Value + " ?";
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "initMyModal('mdlDeleteSoftware');", true);
                            }
                            break;
                        case "btnDeleteSoftSave":
                            DeleteSoftware(conAW);
                            if (noDelete) // if true don't need to clear the table, but if been used, will clear the table
                            {// this is not popup when it is true.
                                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage",
                                //"alert('Sorry Can't delete that software! The Software is used by the Computers: " + nscc + " ')", true);
                                hasData = hasData - 1;
                                table.Append("<table border='1'>");
                                table.Append("<tr><th>QTY Used</th><th>The Software been used by the Computers:</th>");
                                table.Append("</tr>");
                                table.Append("<tr>");
                                table.Append("<td>" + hasData + "</td>");
                                table.Append("<td>" + nscc + "</td>");
                                table.Append("</tr>");
                                table.Append("</table");
                                PlaceHolder1.Controls.Add(new Literal { Text = table.ToString() });
                            }
                            else
                            {
                                //Reload user list
                                loadSoftware(conAW);
                                dlProduct.Items.Clear();
                                dlProduct.Enabled = false;
                                dlVersion.Items.Clear();
                                dlVersion.Enabled = false;
                                lblLicense.Enabled = false;
                                lblLicense.Text = "";
                                //Reload main form
                                //loadDetail(conAW);
                            }
                            break;

                    }

                    //Close the connection to the DB
                    PopulateGridview(conAW);
                    conAW.Close();
                    conAW.Dispose();
                    
                }
            }
            catch (Exception ex)
            {
                ErrLog EL = new ErrLog();
                EL.SaveErrLog("Module: DefaultPage" + Environment.NewLine + "Function: Page_Load"
                    + Environment.NewLine + "ErrorMessage: " + ex.Message);
                Response.Write(@"<script>alert('Error Message: " + ex.Message.ToString()
                    + "');</script>");
            }



        }
    }
}