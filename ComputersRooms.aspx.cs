using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ASPFinal.Classes;
using System.Data.SqlClient;
using System.Configuration;
using System.Text;
using System.Data;


namespace ASPFinal
{
    public partial class ComputersRooms : System.Web.UI.Page
    {


        private static bool cEdit = false;
        private string newCustomerID = "";
        private static string saveNSCCEquipmentNumber = "";
        private static string editCampusName = "";
        private string campusName = "";
        StringBuilder table1 = new StringBuilder();
        StringBuilder table2 = new StringBuilder();

        protected void loadCampus(SqlConnection cAW)
        {
            dlCampusName.Items.Clear();
            string strSQL = "SELECT DISTINCT CampusName FROM Location ORDER BY CampusName;";
            SqlCommand command = new SqlCommand(strSQL, cAW);
            SqlDataReader SQLdr = command.ExecuteReader();

            if (SQLdr.HasRows)
            {
                while (SQLdr.Read())
                {
                    dlCampusName.Items.Insert(dlCampusName.Items.Count, new ListItem(SQLdr[0].ToString()));
                }
                dlCampusName.Items.Insert(0, new ListItem("Select a Campus", ""));
            }
            SQLdr.Close();
            SQLdr.Dispose();
            command.Dispose();
        }

        protected void loadDetail(SqlConnection cAW)
        {
            dlBuilding.Items.Clear();
            dlNSCCEquipmentNumber.Items.Clear();
            //dlBuilding.SelectedIndex = -1;
            dlRoom.Items.Clear();
            if (dlCampusName.SelectedItem.Value != "")
            {
                //A customer has been selected
                string strSQL = "SELECT DISTINCT Building FROM Computers WHERE " +
                                "LocationID = (Select LocationID From Location Where CampusName = '" + dlCampusName.SelectedItem.Value + "')"; 
                SqlCommand command = new SqlCommand(strSQL, cAW);
                SqlDataReader SQLdr = command.ExecuteReader();

                if (SQLdr.HasRows)
                {
                    while (SQLdr.Read())
                    {
                        if (SQLdr[0].ToString() != "")
                        {
                            string test = SQLdr[0].ToString();
                            dlBuilding.Items.Insert(dlBuilding.Items.Count, new ListItem(SQLdr[0].ToString()));
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
                }
                //dlBuilding.SelectedIndex = 0;
                if (dlBuilding.SelectedIndex == -1)
                {
                    dlBuilding.Items.Insert(0, new ListItem("No Building Number Found", ""));
                    dlBuilding.Enabled = false;
                    dlBuilding.AutoPostBack = false;                    
                    SQLdr.Close();
                    SQLdr.Dispose();
                    command.Dispose();
                    loadDetailRoom(cAW);
                }
                else
                {
                    campusName = dlCampusName.SelectedItem.Value;
                    dlBuilding.AutoPostBack = true;
                    dlBuilding.Enabled = true;
                    dlBuilding.Items.Insert(0, new ListItem("Select Building", ""));
                    dlBuilding.SelectedIndex = 0;
                    SQLdr.Close();
                    SQLdr.Dispose();
                    command.Dispose();
                }


            }
            else
            {
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


        protected void loadDetailRoom(SqlConnection cAW)
        {
            dlRoom.Items.Clear();
            dlNSCCEquipmentNumber.Items.Clear();
            if (dlBuilding.SelectedItem.Value != "")
            {
                //A customer has been selected
                string strSQL = "SELECT DISTINCT Room FROM Computers WHERE " +
                                "Building = '" + dlBuilding.SelectedItem.Value + "' AND " +
                                "LocationID = (Select LocationID From Location Where CampusName = '" + dlCampusName.SelectedItem.Value + "')";
                SqlCommand command = new SqlCommand(strSQL, cAW);
                SqlDataReader SQLdr = command.ExecuteReader();

                if (SQLdr.HasRows)
                {
                    while (SQLdr.Read())
                    {
                        dlRoom.Items.Insert(dlRoom.Items.Count, new ListItem(SQLdr[0].ToString()));
                        //Save the ID of the current customer
                        //editCampusName = SQLdr["CustomerID"].ToString();
                        //Load the main page
                        //lblFName.Text = SQLdr["FirstName"].ToString();

                        //Load the modal in case of an edit
                        //customerId = SQLdr["FirstName"].ToString(); //Must save the CustomerID for later use
                        //txtFName.Text = SQLdr["FirstName"].ToString();

                    }
                    dlRoom.Items.Insert(0, new ListItem("Select Room", ""));
                }
                    
                SQLdr.Close();
                SQLdr.Dispose();
                command.Dispose();
            }
            else
            {
                string strSQL = "SELECT DISTINCT Room FROM Computers WHERE " +
                "LocationID = (Select LocationID From Location Where CampusName = '" + dlCampusName.SelectedItem.Value + "')";
                SqlCommand command = new SqlCommand(strSQL, cAW);
                SqlDataReader SQLdr = command.ExecuteReader();

                if (SQLdr.HasRows)
                {
                    while (SQLdr.Read())
                    {
                        dlRoom.Items.Insert(dlRoom.Items.Count, new ListItem(SQLdr[0].ToString()));
                        //Save the ID of the current customer
                        //editCampusName = SQLdr["CustomerID"].ToString();
                        //Load the main page
                        //lblFName.Text = SQLdr["FirstName"].ToString();

                        //Load the modal in case of an edit
                        //customerId = SQLdr["FirstName"].ToString(); //Must save the CustomerID for later use
                        //txtFName.Text = SQLdr["FirstName"].ToString();
                    }
                    dlRoom.Items.Insert(0, new ListItem("Select Room", ""));
                }

                SQLdr.Close();
                SQLdr.Dispose();
                command.Dispose();

            }
        }


        protected void loadComputers(SqlConnection cAW)
        {
            dlNSCCEquipmentNumber.Items.Clear();
            if (dlRoom.SelectedItem.Value != "")
            {
                //A customer has been selected
                string strSQL = "SELECT NSCCEquipmentNumber, Make, Model, PurchaseOrderNumber, " +
                                "PurchaseDate, WarrantyStartDate FROM Computers WHERE " +
                                "Room = '" + dlRoom.SelectedItem.Value + "'";
                SqlCommand command = new SqlCommand(strSQL, cAW);
                SqlDataReader SQLdr = command.ExecuteReader();


                table1.Append("<table border='1'>");
                table1.Append("<tr><th>NSCC Computer ID</th><th>Make</th><th>Model</th><th>PO Number</th><th>PO Date</th><th>Warranty Start Date</th>");
                table1.Append("</tr>");

                if (SQLdr.HasRows)
                {
                    while (SQLdr.Read())
                    {
                        dlNSCCEquipmentNumber.Items.Insert(dlNSCCEquipmentNumber.Items.Count, new ListItem(SQLdr[0].ToString()));
                        table1.Append("<tr>");
                        table1.Append("<td>" + SQLdr[0] + "</td>");
                        table1.Append("<td>" + SQLdr[1] + "</td>");
                        table1.Append("<td>" + SQLdr[2] + "</td>");
                        table1.Append("<td>" + SQLdr[3] + "</td>");
                        table1.Append("<td>" + Convert.ToDateTime(SQLdr[4]).ToShortDateString() + "</td>"); 
                        table1.Append("<td>" + Convert.ToDateTime(SQLdr[5]).ToShortDateString() + "</td>");
                        table1.Append("</tr>");
                    }
                    dlNSCCEquipmentNumber.Items.Insert(0, new ListItem("Select Computer to Display the Software", ""));
                }
                table1.Append("</table");
                PlaceHolder1.Controls.Add(new Literal { Text = table1.ToString() });
                SQLdr.Close();
                SQLdr.Dispose();
                command.Dispose();

            }
            else
            {
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


        protected void loadSoftwareInstalled(SqlConnection cAW)
        {

            for (int Loop = 0; Loop < dlNSCCEquipmentNumber.Items.Count; Loop++)
            {
                if (dlNSCCEquipmentNumber.Items[Loop].Value == saveNSCCEquipmentNumber)
                {
                    //We found the new Customer
                    dlNSCCEquipmentNumber.SelectedIndex = Loop;
                }
            }        
            if (dlNSCCEquipmentNumber.SelectedItem.Value != "")
            {
                //A number has been selected  loadSoftwareInstalled
                cEdit = true;
                string campusName = "";
                //string strSQL = "SELECT DIstinct NSCCEquipmentNumber, Make, Model, SerialNumber, PurchaseOrderNumber, PurchaseDate, WarrantyStartDate," +
                //              " Location.CampusName as LocationK, Building, Room " +
                //              "From Computers join Location on Computers.LocationID = Location.LocationID " +
                //             "WHERE NSCCEquipmentNumber = '" + dlEquipNum.SelectedItem.Value + "'";

                string strSQL1 = "SELECT SoftwareID, ProductName + ' Version ' + Version AS SoftwareName " +
                 "FROM Software " +
                 "WHERE SoftwareID IN(SELECT SoftwareID " +
                                       "FROM SoftwareInstallations " +
                                       "WHERE NSCCEquipmentNumber = '" + dlNSCCEquipmentNumber.SelectedItem.Value + "')";

                //string strSQL = "SELECT DISTINCT NSCCEquipmentNumber, Make, Model, SerialNumber, PurchaseOrderNumber FROM Customer WHERE NSCCEquipmentNumber = '" + dlEquipNum.SelectedItem.Value + "'";
                //SqlCommand command = new SqlCommand(strSQL, cAW);
                //SqlDataReader SQLdr = command.ExecuteReader();

                //if (SQLdr.HasRows)
                //{
                //    while (SQLdr.Read())
                //    {
                //        editNSCCEquipmentNumber = SQLdr["NSCCEquipmentNumber"].ToString();
                //        lblMake.Text = SQLdr["Make"].ToString();
                //        txtModel.Text = SQLdr["Model"].ToString();
                //        txtSerialNum.Text = SQLdr["SerialNumber"].ToString();
                //        txtPoNum.Text = SQLdr["PurchaseOrderNumber"].ToString();
                //        txtDatePurch.Text = SQLdr["PurchaseDate"].ToString();
                //        txtWarrantyDate.Text = SQLdr["WarrantyStartDate"].ToString();
                //        txtCampus.Text = SQLdr["LocationK"].ToString();
                //        txtBuilding.Text = SQLdr["Building"].ToString();
                //        txtRoomNum.Text = SQLdr["Room"].ToString();
                //        // txtSoftName.Text = SQLdr["ProductName"].ToString();
                //        // txtSoftMan.Text = SQLdr["Developer"].ToString();
                //        // txtVersion.Text = SQLdr["Version"].ToString();

                //        // Load the Model if edit selected below:
                //        txtComputerNSCCeNumber.Text = SQLdr["NSCCEquipmentNumber"].ToString();
                //        txtComputerMake.Text = SQLdr["Make"].ToString();
                //        txtComputerModel.Text = SQLdr["Model"].ToString();
                //        txtComputerSerialNumber.Text = SQLdr["SerialNumber"].ToString();
                //        txtComputerPONumber.Text = SQLdr["PurchaseOrderNumber"].ToString();
                //        txtComputerDateOfPurchase.Text = SQLdr["PurchaseDate"].ToString();
                //        txtComputerWarrantyStartdate.Text = SQLdr["WarrantyStartDate"].ToString();
                //        campusName = SQLdr["LocationK"].ToString();
                //        txtComputerBuilding.Text = SQLdr["Building"].ToString();
                //        txtComputerRoom.Text = SQLdr["Room"].ToString();


                //    }
                //}
                //SQLdr.Close();
                //SQLdr.Dispose();
                //command.Dispose();
                //if (cEdit)
                //{
                //    dlComputerCampusName.Items.Clear();
                //    //update the model for the location CampusName
                //    string selectStatement = "SELECT CampusName "
                //           + "FROM Location "
                //           + "ORDER BY CampusName";
                //    SqlCommand selectCommand = new SqlCommand(selectStatement, cAW);
                //    SqlDataReader SQLdrLocation = selectCommand.ExecuteReader();

                //    if (SQLdrLocation.HasRows)
                //    {
                //        while (SQLdrLocation.Read())
                //        {
                //            if (campusName != SQLdrLocation[0].ToString())
                //                dlComputerCampusName.Items.Insert(dlComputerCampusName.Items.Count, new ListItem(SQLdrLocation[0].ToString()));
                //            //dlMake.Items.Insert(dlMake.Items.Count, new ListItem(SQLdr[3].ToString() + ", " + SQLdr[2].ToString(), SQLdr[1].ToString()));
                //            //dlEquipNum.Items.Insert(dlEquipNum.Items.Count, new ListItem(SQLdr[0].ToString() + ", " + SQLdr[1].ToString(), SQLdr[0].ToString()));
                //        }
                //        dlComputerCampusName.Items.Insert(0, new ListItem(campusName, ""));
                //        //dlComputerCampusName.SelectedItem.Value = campusName;
                //    }
                //    SQLdrLocation.Close();
                //    SQLdrLocation.Dispose();
                //    selectCommand.Dispose();
                //}

                //SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(strSQL1, cAW);
                //DataTable softwareInstalled = new DataTable();
                //sqlDataAdapter.Fill(softwareInstalled);
                //lvSoftwareInstalled.DataSource = softwareInstalled;
                //gvSoftwareInstalled.DataSource = softwareInstalled;
                //sqlDataAdapter.Dispose();


                SqlCommand command1 = new SqlCommand(strSQL1, cAW);
                SqlDataReader SQLdr1 = command1.ExecuteReader();

                table2.Append("<h3>Software installed on: " + saveNSCCEquipmentNumber + "</h3>");
                table2.Append("<br />");
                table2.Append("<table border='1'>");
                table2.Append("<tr><th>Software ID</th><th>Software Name</th>");
                table2.Append("</tr>");

                if (SQLdr1.HasRows)
                {
                    while (SQLdr1.Read())
                    {
                        table2.Append("<tr>");
                        table2.Append("<td>" + SQLdr1[0] + "</td>");
                        table2.Append("<td>" + SQLdr1[1] + "</td>");
                        table2.Append("</tr>");
                    }
                }
                table2.Append("</table");
                PlaceHolder2.Controls.Add(new Literal { Text = table2.ToString() });
                SQLdr1.Close();
                SQLdr1.Dispose();
                command1.Dispose();


            }
            else
            {
                //No Customer has been selected. Clear the controls
                //txtSerialNum.Text = "";
                //lblMake.Text = "";
                //txtModel.Text = "";
                //txtSerialNum.Text = "";
                //txtPoNum.Text = "";
                //txtDatePurch.Text = "";
                //txtWarrantyDate.Text = "";
                //txtCampus.Text = "";
                //txtBuilding.Text = "";
                //txtRoomNum.Text = "";
                //txtSoftName.Text = "";
                //txtSoftMan.Text = "";
                //txtVersion.Text = "";
            }
        }




        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {
                //Open a connection to the database
                string conStr = ConfigurationManager.ConnectionStrings["conAW"].ConnectionString;
                SqlConnection conAW = new SqlConnection(conStr);
                conAW.Open();

                if (!IsPostBack)
                {
                    //We are loading the page for the first time
                    loadCampus(conAW);
                }
                else
                {
                    //We need to find out what control caused the postback 
                    getPostBackControl clsCtl = new getPostBackControl();
                    string ctl = clsCtl.getPostBackControlName(Page);
                    switch (ctl)
                    {
                        case "dlCampusName":
                            loadDetail(conAW);
                            break;
                        case "dlBuilding":
                            loadDetailRoom(conAW);
                            break;
                        case "dlRoom":
                            loadComputers(conAW);
                            //Call the js function to show the modal popup
                            //lblAddEditCourse.InnerHtml = "Edit Customer";
                            //cEdit = true;
                            //Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "initMyModal('mdlNewEditEmployee');", true);
                            break;
                        case "dlNSCCEquipmentNumber":
                            //Will need more code to see if we are editing or saving a new employee
                            saveNSCCEquipmentNumber = dlNSCCEquipmentNumber.SelectedItem.Value;
                            loadComputers(conAW);
                            loadSoftwareInstalled(conAW);

                            //if (cEdit)
                            //{   //We are saving an edited customer
                            //    SaveEditCustomer(conAW);
                            //    cEdit = false;
                            //    loadCustomer(conAW);
                            //    //Now select the edited customer record
                            //    for (int Loop = 0; Loop < dlCustomer.Items.Count; Loop++)
                            //    {
                            //        if (dlCustomer.Items[Loop].Value == editCustomerID)
                            //        {
                            //            //We found the edited Customer
                            //            dlCustomer.SelectedIndex = Loop;
                            //        }
                            //    }
                            //    loadDetail(conAW);
                            //}
                            //else
                            //{   //We are saving a new customer
                            //    SaveNewCustomer(conAW);
                            //    //Reload the dropdown list
                            //    loadCustomer(conAW);
                            //    //Now select the new customer record
                            //    for (int Loop = 0; Loop < dlCustomer.Items.Count; Loop++)
                            //    {
                            //        if (dlCustomer.Items[Loop].Value == newCustomerID)
                            //        {
                            //            //We found the new Customer
                            //            dlCustomer.SelectedIndex = Loop;
                            //        }
                            //    }
                            //    //Reload the main form
                            //    loadDetail(conAW);
                            //}
                            //Also need code to reload the dropdownlist and reload the page detail
                            break;
                        //case "btnAdd":
                        //    //Clear out the modal form
                        //    txtFName.Text = "";
                        //    txtMI.Text = "";
                        //    txtLName.Text = "";
                        //    txtCompanyName.Text = "";
                        //    lblAddEditCourse.InnerHtml = "Add New Customer";
                        //    Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "initMyModal('mdlNewEditEmployee');", true);
                        //    break;
                        //case "btnNewEmpCancel":
                        //    cEdit = false;
                        //    break;
                        //case "btnDelete":
                        //    lblDeleteEmpMsg.InnerHtml = "Are you sure you want to delete the employee: " + lblFName.Text + " "
                        //                                                                                 + lblMI.Text + " "
                        //                                                                                 + lblLName.Text;
                        //    Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "initMyModal('mdlDeleteEmployee');", true);
                        //    break;
                        //case "btnDeleteEmpSave":
                        //    DeleteCustomer(conAW);
                        //    //Reload the dropdown list
                        //    loadCustomer(conAW);
                        //    //Reload the main form
                        //    loadDetail(conAW);
                        //    break;
                    }
                }

                //Close the connection to the DB
                conAW.Close();
                conAW.Dispose();
            }
            catch (Exception ex)
            {
                ErrLog EL = new ErrLog();
                EL.SaveErrLog("Module: DefaultPage" + Environment.NewLine + "Function: Page_Load" + Environment.NewLine + "Error Message: " + ex.Message);
                Response.Write(@"<script>alert('Error Message: " + ex.Message.ToString() + "');</script>");
            }





            //To stop the user from entering this page if they are not logged in.
            string strValidLogin = "";
            if (Session["VALIDLOGIN"] != null)
            {
                strValidLogin = Session["VALIDLOGIN"].ToString();
                if (strValidLogin == "false")
                    Response.Redirect("Default.aspx");
            }
            else
                Response.Redirect("Default.aspx");

        }
    }
}