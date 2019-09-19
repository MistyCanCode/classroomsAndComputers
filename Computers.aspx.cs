using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ASPFinal;
using ASPFinal.Classes;
using System.Data.SqlClient;
using System.Configuration;
using System.Text;
using System.Data;

namespace ASPFinal
{
    public partial class Computers : System.Web.UI.Page
    {

        StringBuilder table = new StringBuilder();
        private static bool cEdit = false;
        private static string newNSCCEquipmentNumber = "";
        private static string editNSCCEquipmentNumber = "";


        protected void loadComputer(SqlConnection cAW)
        {
            dlEquipNum.Items.Clear();
            string strSQL = "SELECT NSCCEquipmentNumber, Make, Model, SerialNumber, PurchaseOrderNumber," +
            " PurchaseDate, WarrantyStartDate, Location.CampusName, Building, Room " +
            "From Computers join Location on Computers.LocationID = Location.LocationID";

            //string strSQL = "SELECT SELECT DIstinct NSCCEquipmentNumber, Make, Model, SerialNumber, PurchaseOrderNumber," +
            //" PurchaseDate, WarrantyStartDate, Location.CampusName, Building, Room " +
            //"From Computers join Location on Computers.LocationID = Location.LocationID";

            SqlCommand command = new SqlCommand(strSQL, cAW);
            SqlDataReader SQLdr = command.ExecuteReader();

            if (SQLdr.HasRows)
            {
                while (SQLdr.Read())
                {
                    dlEquipNum.Items.Insert(dlEquipNum.Items.Count, new ListItem(SQLdr[0].ToString()));
                    //dlMake.Items.Insert(dlMake.Items.Count, new ListItem(SQLdr[3].ToString() + ", " + SQLdr[2].ToString(), SQLdr[1].ToString()));
                    //dlEquipNum.Items.Insert(dlEquipNum.Items.Count, new ListItem(SQLdr[0].ToString() + ", " + SQLdr[1].ToString(), SQLdr[0].ToString()));
                }
                dlEquipNum.Items.Insert(0, new ListItem("Select a Computer Equipment Number ", ""));
            }
            SQLdr.Close();
            SQLdr.Dispose();
            command.Dispose();

            // clear the form main
            txtSerialNum.Text = "";
            lblMake.Text = "";
            txtModel.Text = "";
            txtSerialNum.Text = "";
            txtPoNum.Text = "";
            txtDatePurch.Text = "";
            txtWarrantyDate.Text = "";
            txtCampus.Text = "";
            txtBuilding.Text = "";
            txtRoomNum.Text = "";


        }

        protected void loadDetail(SqlConnection cAW)
        {
            if (dlEquipNum.SelectedItem.Value != "")
            {
                //A number has been selected  loadSoftwareInstalled
                cEdit = true;
                string campusName = "";
                string strSQL = "SELECT DIstinct NSCCEquipmentNumber, Make, Model, SerialNumber, PurchaseOrderNumber, PurchaseDate, WarrantyStartDate," +
                              " Location.CampusName as LocationK, Building, Room " +
                              "From Computers join Location on Computers.LocationID = Location.LocationID " +
                             "WHERE NSCCEquipmentNumber = '" + dlEquipNum.SelectedItem.Value + "'";


                //string strSQL = "SELECT DISTINCT NSCCEquipmentNumber, Make, Model, SerialNumber, PurchaseOrderNumber FROM Customer WHERE NSCCEquipmentNumber = '" + dlEquipNum.SelectedItem.Value + "'";
                SqlCommand command = new SqlCommand(strSQL, cAW);
                SqlDataReader SQLdr = command.ExecuteReader();

                if (SQLdr.HasRows)
                {
                    while (SQLdr.Read())
                    {
                        editNSCCEquipmentNumber = SQLdr["NSCCEquipmentNumber"].ToString();
                        lblMake.Text = SQLdr["Make"].ToString();
                        txtModel.Text = SQLdr["Model"].ToString();
                        txtSerialNum.Text = SQLdr["SerialNumber"].ToString();
                        txtPoNum.Text = SQLdr["PurchaseOrderNumber"].ToString();
                        txtDatePurch.Text = SQLdr["PurchaseDate"].ToString();
                        txtWarrantyDate.Text = SQLdr["WarrantyStartDate"].ToString();
                        txtCampus.Text = SQLdr["LocationK"].ToString();
                        txtBuilding.Text = SQLdr["Building"].ToString();
                        txtRoomNum.Text = SQLdr["Room"].ToString();
                        // txtSoftName.Text = SQLdr["ProductName"].ToString();
                        // txtSoftMan.Text = SQLdr["Developer"].ToString();
                        // txtVersion.Text = SQLdr["Version"].ToString();

                        // Load the Model if edit selected below:
                        txtComputerNSCCeNumber.Text = SQLdr["NSCCEquipmentNumber"].ToString();
                        txtComputerMake.Text = SQLdr["Make"].ToString();
                        txtComputerModel.Text = SQLdr["Model"].ToString();
                        txtComputerSerialNumber.Text = SQLdr["SerialNumber"].ToString();
                        txtComputerPONumber.Text = SQLdr["PurchaseOrderNumber"].ToString();
                        txtComputerDateOfPurchase.Text = SQLdr["PurchaseDate"].ToString();
                        txtComputerWarrantyStartdate.Text = SQLdr["WarrantyStartDate"].ToString();
                        campusName = SQLdr["LocationK"].ToString();
                        txtComputerBuilding.Text = SQLdr["Building"].ToString();
                        txtComputerRoom.Text = SQLdr["Room"].ToString();


                    }
                }
                SQLdr.Close();
                SQLdr.Dispose();
                command.Dispose();
                if (cEdit)
                {
                    dlComputerCampusName.Items.Clear();
                //update the model for the location CampusName
                string selectStatement = "SELECT CampusName "
                       + "FROM Location "
                       + "ORDER BY CampusName";
                SqlCommand selectCommand = new SqlCommand(selectStatement, cAW);
                SqlDataReader SQLdrLocation = selectCommand.ExecuteReader();

                if (SQLdrLocation.HasRows)
                {
                    while (SQLdrLocation.Read())
                    {
                        if (campusName != SQLdrLocation[0].ToString())
                        dlComputerCampusName.Items.Insert(dlComputerCampusName.Items.Count, new ListItem(SQLdrLocation[0].ToString()));
                        //dlMake.Items.Insert(dlMake.Items.Count, new ListItem(SQLdr[3].ToString() + ", " + SQLdr[2].ToString(), SQLdr[1].ToString()));
                        //dlEquipNum.Items.Insert(dlEquipNum.Items.Count, new ListItem(SQLdr[0].ToString() + ", " + SQLdr[1].ToString(), SQLdr[0].ToString()));
                    }
                    dlComputerCampusName.Items.Insert(0, new ListItem(campusName, ""));
                    //dlComputerCampusName.SelectedItem.Value = campusName;
                }
                SQLdrLocation.Close();
                SQLdrLocation.Dispose();                    
                selectCommand.Dispose();
                }

                //SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(strSQL1, cAW);
                //DataTable softwareInstalled = new DataTable();
                //sqlDataAdapter.Fill(softwareInstalled);
                //lvSoftwareInstalled.DataSource = softwareInstalled;
                //gvSoftwareInstalled.DataSource = softwareInstalled;
                //sqlDataAdapter.Dispose();

                string strSQL1 = "SELECT SoftwareID, ProductName + ' Version ' + Version AS SoftwareName " +
                 "FROM Software " +
                 "WHERE SoftwareID IN(SELECT SoftwareID " +
                                       "FROM SoftwareInstallations " +
                                       "WHERE NSCCEquipmentNumber = '" + dlEquipNum.SelectedItem.Value + "')";

                SqlCommand command1 = new SqlCommand(strSQL1, cAW);
                SqlDataReader SQLdr1 = command1.ExecuteReader();
                
                table.Append("<table border='1'>");
                table.Append("<tr><th>Software ID</th><th>Software Name</th>");
                table.Append("</tr>");

                if (SQLdr1.HasRows)
                {
                    while (SQLdr1.Read())
                    {
                        table.Append("<tr>");
                        table.Append("<td>" + SQLdr1[0] + "</td>");
                        table.Append("<td>" + SQLdr1[1] + "</td>");
                        table.Append("</tr>");
                    }
                }
                table.Append("</table");
                PlaceHolder1.Controls.Add(new Literal { Text = table.ToString() });
                SQLdr1.Close();
                SQLdr1.Dispose();
                command1.Dispose();


            }
            else
            {
                //No Customer has been selected. Clear the controls
                txtSerialNum.Text = "";
                lblMake.Text = "";
                txtModel.Text = "";
                txtSerialNum.Text = "";
                txtPoNum.Text = "";
                txtDatePurch.Text = "";
                txtWarrantyDate.Text = "";
                txtCampus.Text = "";
                txtBuilding.Text = "";
                txtRoomNum.Text = "";
                txtSoftName.Text = "";
                txtSoftMan.Text = "";
                txtVersion.Text = "";
            }
        }

        protected void SaveEditComputer(SqlConnection cAW)
        {
            if (dlEquipNum.SelectedItem.Value != "")
            {
                string locationCampus = "";
                if (dlComputerCampusName.SelectedItem.Value == "")
                    locationCampus = txtCampus.Text;
                else
                    locationCampus = dlComputerCampusName.SelectedItem.Value;
                string updateStatement =
                    "UPDATE Computers SET " +
                    "Make = '" + txtComputerMake.Text + "', " +
                    "Model = '" + txtComputerModel.Text + "', " +
                    "SerialNumber = '" + txtComputerSerialNumber.Text + "', " +
                    "PurchaseOrderNumber = '" + txtComputerPONumber.Text + "', " +
                    "PurchaseDate = '" + txtComputerDateOfPurchase.Text + "', " +
                    "WarrantyStartDate = '" + txtComputerWarrantyStartdate.Text + "', " +
                    "LocationID = (Select LocationID From Location Where CampusName = '" + locationCampus + "'), "+ 
                    "Building = '" + txtComputerBuilding.Text + "', " +
                    "Room = '" + txtComputerRoom.Text + "' " +
                    "WHERE NSCCEquipmentNumber = '" + dlEquipNum.SelectedItem.Value + "'";
                SqlCommand updateCommand = new SqlCommand(updateStatement, cAW);
                //SqlCommand updateCommand = new SqlCommand(updateStatement, connection);
                try
                {
                    //connection.Open();
                    int count = updateCommand.ExecuteNonQuery();
                    //if (count > 0)
                    //    return true;
                    //else
                    //    return false;
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
                finally
                {
                    updateCommand.Dispose();
                }
                //A customer has been selected
                //string strSQL = "UPDATE Customer SET FirstName = '" + txtFName.Text + "', MiddleName = '" + txtMI.Text +
                //    "', LastName = '" + txtLName.Text + "', CompanyName = '" + txtCompanyName.Text +
                //    "' WHERE CustomerID = " + dlCustomer.SelectedItem.Value + ";";
                //SqlCommand command = new SqlCommand(strSQL, cAW);
                //int recAffected = command.ExecuteNonQuery();

                //command.Dispose();
            }
        }


        protected void SaveNewComputer(SqlConnection cAW)
        {
            //A customer has been selected SaveAddNewSoftware
            string test = dlComputerCampusName.SelectedItem.Value;
            newNSCCEquipmentNumber = txtComputerNSCCeNumber.Text;
            int locationID = 0;
            string strSQL = "SELECT LocationID FROM Location WHERE CampusName = '" + dlComputerCampusName.SelectedItem.Value + "'";
            SqlCommand selectCommand = new SqlCommand(strSQL, cAW);
            SqlDataReader SQLdrLocation = selectCommand.ExecuteReader();
            if (SQLdrLocation.HasRows)
            {
                while (SQLdrLocation.Read())
                {                    
                    locationID = Convert.ToInt32(SQLdrLocation[0]);                                
                }                                
            }
            SQLdrLocation.Close();
            SQLdrLocation.Dispose();
            selectCommand.Dispose();
        

            string insertStatement = "INSERT Computers " +
                                     "(NSCCEquipmentNumber, Make, Model, SerialNumber, PurchaseOrderNumber, " +
                                     "PurchaseDate, WarrantyStartDate, LocationID, Building, Room) " +
                                     "VALUES ('"
                                     + txtComputerNSCCeNumber.Text + "', '"
                                     + txtComputerMake.Text + "', '"
                                     + txtComputerModel.Text + "', '"
                                     + txtComputerSerialNumber.Text + "', '"
                                     + txtComputerPONumber.Text + "', '"
                                     + txtComputerDateOfPurchase.Text + "', '"
                                     + txtComputerWarrantyStartdate.Text + "', "
                                     + locationID + ", '" 
                                     + txtComputerBuilding.Text + "', '"
                                     + txtComputerRoom.Text + "')";
            //"(Select LocationID From Location Where CampusName = '" + dlComputerCampusName.SelectedItem.Value + "'), '"
            SqlCommand insertCommand = new SqlCommand(insertStatement, cAW);
            //int recAffected = insertCommand.ExecuteNonQuery();

            try
            {
                //connection.Open();
                int recAffected = insertCommand.ExecuteNonQuery();
                if (recAffected == 1)
                {
                    string strID = txtComputerNSCCeNumber.Text;
                    //SQLdr.Close();
                    //SQLdr.Dispose();
                    //command.Dispose();
                }
                //command.Dispose();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {                
                insertCommand.Dispose();
            }
            insertCommand.Dispose();


            //string strSQL = "INSERT INTO Customer (FirstName, MiddleName, LastName, CompanyName, PasswordHash, PasswordSalt, rowguid, ModifiedDate) VALUES ('"
            //    + txtFName.Text + "', '" + txtMI.Text + "',  '" + txtLName.Text + "', '"
            //    + txtCompanyName.Text + "', 'PH1', 'PS1', '" + Guid.NewGuid() + "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "');";
            //SqlCommand command = new SqlCommand(strSQL, cAW);
            //int recAffected = command.ExecuteNonQuery();
            //if (recAffected == 1)
            //{
            //    string strID = "Select @@Identity";
            //    SqlCommand commandID = new SqlCommand(strID, cAW);
            //    SqlDataReader SQLdr = commandID.ExecuteReader();

            //    if (SQLdr.HasRows)
            //    {
            //        while (SQLdr.Read())
            //        {
            //            //Load the main page
            //            newCustomerID = SQLdr[0].ToString();
            //        }
            //    }
            //    SQLdr.Close();
            //    SQLdr.Dispose();
            //    command.Dispose();
            //}
            //command.Dispose();
        }


        protected void DeleteComputer(SqlConnection cAW)
        {
            //A customer has been selected
            string strSQL = "DELETE FROM Computers WHERE NSCCEquipmentNumber = '" + dlEquipNum.SelectedItem.Value + "';";
            SqlCommand command = new SqlCommand(strSQL, cAW);
            int recAffected = command.ExecuteNonQuery();

            command.Dispose();
        }


        protected void SaveAddNewSoftware(SqlConnection cAW)
        {
            //A customer has been selected SaveAddNewSoftware dlEquipNum.SelectedItem.Value
            editNSCCEquipmentNumber = dlEquipNum.SelectedItem.Value;
            string softwareName = dlAddSoftwareInstalled.SelectedItem.Value;
            softwareName = softwareName.Trim();
            string[] software = softwareName.Split('#');
            string productName = software[0].Remove(software[0].Length - 1, 1);
            string version = software[1].Remove(0, 1);

            string insertStatement =
                    "INSERT SoftwareInstallations " +
                    "(NSCCEquipmentNumber, SoftwareID) " +
                    "VALUES ('"
                    + dlEquipNum.SelectedItem.Value + "', "
                    + "(Select SoftwareID From Software Where ProductName = '" + productName + "' AND Version = '" + version + "'))";

            SqlCommand insertCommand = new SqlCommand(insertStatement, cAW);
            //int recAffected = insertCommand.ExecuteNonQuery();

            try
            {
                //connection.Open();
                int recAffected = insertCommand.ExecuteNonQuery();
                if (recAffected == 1)
                {
                    string strID = txtComputerNSCCeNumber.Text;
                    //SQLdr.Close();
                    //SQLdr.Dispose();
                    //command.Dispose();
                }
                //command.Dispose();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                insertCommand.Dispose();
            }



            //string strSQL = "INSERT INTO Customer (FirstName, MiddleName, LastName, CompanyName, PasswordHash, PasswordSalt, rowguid, ModifiedDate) VALUES ('"
            //    + txtFName.Text + "', '" + txtMI.Text + "',  '" + txtLName.Text + "', '"
            //    + txtCompanyName.Text + "', 'PH1', 'PS1', '" + Guid.NewGuid() + "', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "');";
            //SqlCommand command = new SqlCommand(strSQL, cAW);
            //int recAffected = command.ExecuteNonQuery();
            //if (recAffected == 1)
            //{
            //    string strID = "Select @@Identity";
            //    SqlCommand commandID = new SqlCommand(strID, cAW);
            //    SqlDataReader SQLdr = commandID.ExecuteReader();

            //    if (SQLdr.HasRows)
            //    {
            //        while (SQLdr.Read())
            //        {
            //            //Load the main page
            //            newCustomerID = SQLdr[0].ToString();
            //        }
            //    }
            //    SQLdr.Close();
            //    SQLdr.Dispose();
            //    command.Dispose();
            //}
            //command.Dispose();
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
                    loadComputer(conAW);
                }
                else
                {
                    
                    //We need to find out what control caused the postback
                    getPostBackControl clsCtl = new getPostBackControl();
                    string ctl = clsCtl.getPostBackControlName(Page);
                    switch (ctl)
                    {
                        case "dlEquipNum":
                            loadDetail(conAW);
                            break;
                        case "btnEdit":
                            //Call the js function to show the modal popup
                            lblAddEditCourse.InnerHtml = "Edit Computer";
                            cEdit = true;
                            txtComputerNSCCeNumber.Enabled = false;
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "initMyModal('mdlNewEditComputer');", true);
                            break;
                        case "btnNewComputerSave":
                            //Will need more code to see if we are editing or saving a new employee btnSoftwareInstallSave
                            if (cEdit)
                            {   //We are saving an edited customer
                                SaveEditComputer(conAW);
                                cEdit = false;
                                loadComputer(conAW);
                                //Now select the edited customer record
                                for (int Loop = 0; Loop < dlEquipNum.Items.Count; Loop++)
                                {
                                    if (dlEquipNum.Items[Loop].Value == editNSCCEquipmentNumber)
                                    {
                                        //We found the edited Customer
                                        dlEquipNum.SelectedIndex = Loop;
                                    }
                                }
                                loadDetail(conAW);
                            }
                            else
                            {   //We are saving a new customer
                                SaveNewComputer(conAW);
                                //Reload the dropdown list
                                loadComputer(conAW);
                                //Now select the new customer record
                                for (int Loop = 0; Loop < dlEquipNum.Items.Count; Loop++)
                                {
                                    if (dlEquipNum.Items[Loop].Value == newNSCCEquipmentNumber)
                                    {
                                        //We found the new Customer
                                        dlEquipNum.SelectedIndex = Loop;
                                    }
                                }
                                //Reload the main form
                                loadDetail(conAW);
                            }
                            //Also need code to reload the dropdownlist and reload the page detail
                            break;
                        case "btnAdd":
                            cEdit = false;
                            //dlEquipNum.SelectedItem.Value = "";
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
                            //Clear out the modal form
                            dlComputerCampusName.Items.Clear();
                            string selectStatement = "SELECT CampusName "
                                                   + "FROM Location "
                                                   + "ORDER BY CampusName";
                            SqlCommand selectCommand = new SqlCommand(selectStatement, conAW);
                            SqlDataReader SQLdr = selectCommand.ExecuteReader();

                            if (SQLdr.HasRows)
                            {
                                while (SQLdr.Read())
                                {
                                    dlComputerCampusName.Items.Insert(dlComputerCampusName.Items.Count, new ListItem(SQLdr[0].ToString()));
                                    //dlMake.Items.Insert(dlMake.Items.Count, new ListItem(SQLdr[3].ToString() + ", " + SQLdr[2].ToString(), SQLdr[1].ToString()));
                                    //dlEquipNum.Items.Insert(dlEquipNum.Items.Count, new ListItem(SQLdr[0].ToString() + ", " + SQLdr[1].ToString(), SQLdr[0].ToString()));
                                }
                                dlComputerCampusName.Items.Insert(0, new ListItem("Select Campus Location ", ""));
                            }
                            SQLdr.Close();
                            SQLdr.Dispose();
                            selectCommand.Dispose();



                            txtComputerNSCCeNumber.Enabled = true;
                            txtComputerNSCCeNumber.Text = "";                            
                            txtComputerMake.Text = "";
                            txtComputerModel.Text = "";
                            txtComputerSerialNumber.Text = "";
                            txtComputerPONumber.Text = "";
                            txtComputerDateOfPurchase.Text = "";
                            txtComputerWarrantyStartdate.Text = "";
                            //txtComputerLocationID.Text = "";
                            txtComputerBuilding.Text = "";
                            txtComputerRoom.Text = "";
                            lblAddEditCourse.InnerHtml = "Add New Computer";
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "initMyModal('mdlNewEditComputer');", true);
                            break;
                        case "btnNewComputerCancel":
                            cEdit = false;
                            loadComputer(conAW);
                            break;
                        case "btnDelete":
                            lblDeleteComputerMsg.InnerHtml = "Are you sure you want to delete the Computer: " + lblMake.Text + " "
                                                                                                         + txtModel.Text + " "
                                                                                                         + txtSerialNum.Text;
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "initMyModal('mdlDeleteComputer');", true);
                            break;
                        case "btnDeleteComputerSave":
                            DeleteComputer(conAW);
                            //Reload the dropdown list
                            loadComputer(conAW);
                            //Reload the main form
                            loadDetail(conAW);
                            break;
                        case "btnAddSoftware":
                            //Clear out the modal form
                            dlAddSoftwareInstalled.Items.Clear();
                            string selectStatement1 = "SELECT Distinct ProductName + ' #V' + Version AS SoftwareName "
                                                    + "FROM Software "
                                                    + "ORDER BY SoftwareName";
                            SqlCommand selectCommand1 = new SqlCommand(selectStatement1, conAW);
                            SqlDataReader SQLdr1 = selectCommand1.ExecuteReader();

                            if (SQLdr1.HasRows)
                            {
                                while (SQLdr1.Read())
                                {
                                    dlAddSoftwareInstalled.Items.Insert(dlAddSoftwareInstalled.Items.Count, new ListItem(SQLdr1[0].ToString()));
                                    //dlMake.Items.Insert(dlMake.Items.Count, new ListItem(SQLdr[3].ToString() + ", " + SQLdr[2].ToString(), SQLdr[1].ToString()));
                                    //dlEquipNum.Items.Insert(dlEquipNum.Items.Count, new ListItem(SQLdr[0].ToString() + ", " + SQLdr[1].ToString(), SQLdr[0].ToString()));
                                }
                                dlAddSoftwareInstalled.Items.Insert(0, new ListItem("Select Software ", ""));
                            }
                            SQLdr1.Close();
                            SQLdr1.Dispose();
                            selectCommand1.Dispose();
                            lblAddNewSoftware.InnerHtml = "Add New Software";
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "initMyModal('mdlNewSoftwareInstall');", true);
                            break;

                         case "btnSoftwareInstallSave":
                            //Will need more code to see if we are editing or saving a new employee 
                            //if (cEdit)
                            //{   //We are saving an edited customer
                            //    SaveEditComputer(conAW);
                            //    cEdit = false;
                            //    loadComputer(conAW);
                            //    //Now select the edited customer record
                            //    for (int Loop = 0; Loop < dlEquipNum.Items.Count; Loop++)
                            //    {
                            //        if (dlEquipNum.Items[Loop].Value == editNSCCEquipmentNumber)
                            //        {
                            //            //We found the edited Customer
                            //            dlEquipNum.SelectedIndex = Loop;
                            //        }
                            //    }
                            //    loadDetail(conAW);
                            //}
                            //else
                            {   //We are saving a new customer
                                SaveAddNewSoftware(conAW); //SaveAddNewSoftware
                                //Reload the dropdown list
                                loadComputer(conAW);
                                //Now select the new customer record
                                for (int Loop = 0; Loop < dlEquipNum.Items.Count; Loop++)
                                {
                                    if (dlEquipNum.Items[Loop].Value == editNSCCEquipmentNumber)
                                    {
                                        //We found the new Customer
                                        dlEquipNum.SelectedIndex = Loop;
                                    }
                                }
                                //Reload the main form
                                loadDetail(conAW);
                            }
                            //Also need code to reload the dropdownlist and reload the page detail
                            break;

                        case "btnSoftwareInstallCancel":
                            cEdit = false;
                            break;
                        case "btnDeleteSoftware":
                            lblDeleteComputerSWMsg.InnerHtml = "Are you sure you want to delete the Software from  this computer: " +
                                                                dlComputerCampusName.SelectedItem.Value + " ?";
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "initMyModal('mdlDeleteSoftware');", true);
                            break;
                        case "btnDeleteSoftwareSave":
                            //DeleteComputer(conAW);
                            //Reload the dropdown list
                            loadComputer(conAW);
                            //Reload the main form
                            loadDetail(conAW);
                            break;

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


            // this is was before add the above


            ////Open a connection to the database
            //string conStr = ConfigurationManager.ConnectionStrings["conAW"].ConnectionString;
            //SqlConnection conAW = new SqlConnection(conStr);
            //conAW.Open();

            //if (!IsPostBack)
            //{
            //    //We are loading the page for the first time
            //    loadComputer(conAW);
            //}
            //else
            //{
            //    //We need to find out what control caused the postback
            //    getPostBackControl clsCtl = new getPostBackControl();
            //    string ctl = clsCtl.getPostBackControlName(Page);
            //    switch (ctl)
            //    {
            //        case "dlEquipNum":
            //            loadDetail(conAW);
            //            break;
            //    }
            //}

            ////Close the connection to the DB
            //conAW.Close();
            //conAW.Dispose();


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


        protected void addAnother_Click(object sender, EventArgs e)
        {

        }



    }
}