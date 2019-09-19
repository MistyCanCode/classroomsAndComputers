using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using ASPFinal.Classes;
using System.Text;

namespace ASPFinal
{
    public partial class Computers1 : System.Web.UI.Page
    {

        private static bool cEdit = false;
        private static string newComputerID = "";
        private static string editComputerID = "";
        StringBuilder table = new StringBuilder();

        protected void loadComputer(SqlConnection cAW)
        {
            ddlEquipNum.Items.Clear();
            string strSQL = "SELECT NSCCEquipmentNumber, Make, Model, SerialNumber, PurchaseOrderNumber," +
                            " PurchaseDate, WarrantyStartDate, Location.CampusName, Building, Room " +
                            "FROM Computers JOIN Location ON Computers.LocationID = Location.LocationID";

            SqlCommand command = new SqlCommand(strSQL, cAW);
            SqlDataReader SQLdr = command.ExecuteReader();

            if (SQLdr.HasRows)
            {
                while (SQLdr.Read())
                {
                    ddlEquipNum.Items.Insert(ddlEquipNum.Items.Count, new ListItem(SQLdr[0].ToString()));
                }

                ddlEquipNum.Items.Insert(0, new ListItem("Select a Computer Equipment Number", ""));
            }

            SQLdr.Close();
            SQLdr.Dispose();
            command.Dispose();

            // clear the form main this will need everytime when try edit, need to clear the form, so nothing selected.
            //txtSerialNum.Text = "";
            lblMake.Text = "";
            lblModel.Text = "";
            lblSerialNumber.Text = "";
            lblPurchaseOrderNumber.Text = "";
            lblPurchaseDate.Text = "";
            lblWarrantyStartDate.Text = "";
            lblCampus.Text = "";
            lblBuilding.Text = "";
            lblRoomNum.Text = "";


        }

        private void LoadEditLocationComboBox()
        {
            ddlLocationID.Items.Clear();
            DataTable campusTable = new DataTable();

            using (SqlConnection con = new SqlConnection("Data Source=nsccsqlinst16.nscc.edu;Database=CITC_BTEAM;UID=CITC_BTEAM;PWD=ITROCKS;"))
            {
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT LocationID, CampusName FROM Location", con);
                adapter.Fill(campusTable);
                ddlLocationID.DataSource = campusTable;
                ddlLocationID.DataTextField = "CampusName";
                ddlLocationID.DataValueField = "LocationID";
                ddlLocationID.DataBind();
                ddlLocationID.Items.Insert(0, new ListItem("Select Campus", "0"));
            }

            //https://stackoverflow.com/questions/7227510/what-is-the-right-way-to-populate-a-dropdownlist-from-a-database
        }

        protected void loadDetail(SqlConnection cAW)
        {
            if (ddlEquipNum.SelectedItem.Value != "")
            {
                //A number has been selected
                string strSQL =
                    "SELECT NSCCEquipmentNumber, Make, Model, SerialNumber, PurchaseOrderNumber, PurchaseDate, WarrantyStartDate, " +
                    "Location.CampusName, Computers.LocationID, Building, Room " +
                    "FROM Computers JOIN Location ON Computers.LocationID = Location.LocationID " +
                    "WHERE NSCCEquipmentNumber = '" + ddlEquipNum.SelectedItem.Value + "'";
                SqlCommand command = new SqlCommand(strSQL, cAW);
                SqlDataReader SQLdr = command.ExecuteReader();

                if (SQLdr.HasRows)
                {
                    while (SQLdr.Read())
                    {
                        //Save the ID of the current customer
                        editComputerID = SQLdr["NSCCEquipmentNumber"].ToString();

                        //Load the main page
                        lblMake.Text = SQLdr["Make"].ToString();
                        lblModel.Text = SQLdr["Model"].ToString();
                        lblSerialNumber.Text = SQLdr["SerialNumber"].ToString();
                        lblPurchaseOrderNumber.Text = SQLdr["PurchaseOrderNumber"].ToString();
                        lblPurchaseDate.Text = Convert.ToDateTime(SQLdr["PurchaseDate"]).ToShortDateString();
                        lblWarrantyStartDate.Text = Convert.ToDateTime(SQLdr["WarrantyStartDate"]).ToShortDateString();
                        lblCampus.Text = SQLdr["CampusName"].ToString();
                        lblBuilding.Text = SQLdr["Building"].ToString();
                        lblRoomNum.Text = SQLdr["Room"].ToString();

                        //Load the modal in case of an edit
                        txtNSCCEquipmentNumber.Text = SQLdr["NSCCEquipmentNumber"].ToString();
                        txtMake.Text = SQLdr["Make"].ToString();
                        txtModel.Text = SQLdr["Model"].ToString();
                        txtSerialNumber.Text = SQLdr["SerialNumber"].ToString();
                        txtPurchaseOrderNumber.Text = SQLdr["PurchaseOrderNumber"].ToString();
                        txtPurchaseDate.Text = Convert.ToDateTime(SQLdr["PurchaseDate"]).ToShortDateString();
                        txtWarrantyStartDate.Text = Convert.ToDateTime(SQLdr["WarrantyStartDate"]).ToShortDateString();
                        LoadEditLocationComboBox();
                        var locationID = SQLdr["LocationID"]; // this is to take the number and added to location ID
                        ddlLocationID.SelectedIndex = (int)locationID;
                        txtBuilding.Text = SQLdr["Building"].ToString();
                        txtRoom.Text = SQLdr["Room"].ToString();
                    }
                }

                SQLdr.Close();
                SQLdr.Dispose();
                command.Dispose();
                loadSoftwareForComputer(cAW);
                loadSoftwareForComputerList(cAW);




            }
            else
            {
                //No Computer has been selected. Clear the controls
                lblMake.Text = "";
                lblModel.Text = "";
                lblSerialNumber.Text = "";
                lblPurchaseOrderNumber.Text = "";
                lblPurchaseDate.Text = "";
                lblWarrantyStartDate.Text = "";
                lblCampus.Text = "";
                lblBuilding.Text = "";
                lblRoomNum.Text = "";

                //Clear out the modal form
                txtNSCCEquipmentNumber.Text = "";
                txtMake.Text = "";
                txtModel.Text = "";
                txtSerialNumber.Text = "";
                txtPurchaseOrderNumber.Text = "";
                txtPurchaseDate.Text = "";
                txtWarrantyStartDate.Text = "";
                ddlLocationID.Items.Clear();
                txtBuilding.Text = "";
                txtRoom.Text = "";
            }
        }

        protected void SaveNewComputer(SqlConnection cAW)
        {
            //A computer has been selected
            string strSQL =
                "INSERT Computers " +
                "(NSCCEquipmentNumber, Make, Model, SerialNumber, PurchaseOrderNumber, " +
                "PurchaseDate, WarrantyStartDate, LocationID, Building, Room) " +
                "VALUES ('"
                + txtNSCCEquipmentNumber.Text + "', '"
                + txtMake.Text + "', '"
                + txtModel.Text + "', '"
                + txtSerialNumber.Text + "', '"
                + txtPurchaseOrderNumber.Text + "', '"
                + Convert.ToDateTime(txtPurchaseDate.Text) + "', '"
                + Convert.ToDateTime(txtWarrantyStartDate.Text) + "', "
                + ddlLocationID.SelectedItem.Value + ", '"
                + txtBuilding.Text + "', '"
                + txtRoom.Text + "')";
            SqlCommand command = new SqlCommand(strSQL, cAW);
            int recAffected = command.ExecuteNonQuery();
            if (recAffected == 1)
            {
                string strID = "SELECT NSCCEquipmentNumber" +
                               " FROM Computers" +
                               " WHERE NSCCEquipmentNumber = '" + txtNSCCEquipmentNumber.Text + "'";
                SqlCommand commandID = new SqlCommand(strID, cAW);
                SqlDataReader SQLdr = commandID.ExecuteReader();

                if (SQLdr.HasRows)
                {
                    while (SQLdr.Read())
                    {
                        //Load the main page
                        newComputerID = SQLdr[0].ToString();
                    }
                }

                SQLdr.Close();
                SQLdr.Dispose();
                command.Dispose();
            }

            command.Dispose();
        }

        protected void SaveEditComputer(SqlConnection cAW)
        {
            if (ddlEquipNum.SelectedItem.Value != "")
            {
                //A computer hs been selected
                string strSQL =
                    "UPDATE Computers SET " +
                    "NSCCEquipmentNumber = '" + txtNSCCEquipmentNumber.Text + "', " +
                    "Make = '" + txtMake.Text + "', " +
                    "Model = '" + txtModel.Text + "', " +
                    "SerialNumber = '" + txtSerialNumber.Text + "', " +
                    "PurchaseOrderNumber = '" + txtPurchaseOrderNumber.Text + "', " +
                    "PurchaseDate = '" + Convert.ToDateTime(txtPurchaseDate.Text) + "', " +
                    "WarrantyStartDate = '" + Convert.ToDateTime(txtWarrantyStartDate.Text) + "', " +
                    "LocationID = " + ddlLocationID.SelectedItem.Value + ", " +
                    "Building = '" + txtBuilding.Text + "', " +
                    "Room = '" + txtRoom.Text + "' " +
                    "WHERE NSCCEquipmentNumber = '" + ddlEquipNum.SelectedItem.Value + "'";
                SqlCommand command = new SqlCommand(strSQL, cAW);
                var recAffected = command.ExecuteNonQuery();

                command.Dispose();
            }
        }

        protected void DeleteComputer(SqlConnection cAW)
        {
            //A customer has been selected
            string strSQL = "DELETE FROM Computers WHERE NSCCEquipmentNumber = '"
                            + ddlEquipNum.SelectedItem.Value + "';";
            SqlCommand command = new SqlCommand(strSQL, cAW);
            var recAffected = command.ExecuteNonQuery();

            command.Dispose();
        }

        protected void loadSoftwareForComputer(SqlConnection cAW)
        {
            // this Code to adding the SoftwareInstalled for that computer selected.
            string strSQL1 = "SELECT SoftwareID, ProductName + ' Version ' + Version AS SoftwareName " +
             "FROM Software " +
             "WHERE SoftwareID IN(SELECT SoftwareID " +
                                   "FROM SoftwareInstallations " +
                                   "WHERE NSCCEquipmentNumber = '" + ddlEquipNum.SelectedItem.Value + "')";

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

        protected void loadSoftwareForComputerList(SqlConnection cAW)
        {
            //Clear out the modal form
            dlSoftwareInstalled.Items.Clear();
            string selectStatement1 = "SELECT ProductName + ' #V' + Version AS SoftwareName " +
                                      "FROM Software " +
                                      "WHERE SoftwareID IN(SELECT SoftwareID " +
                                      "FROM SoftwareInstallations " +
                                      "WHERE NSCCEquipmentNumber = '" + ddlEquipNum.SelectedItem.Value + "')";
            SqlCommand selectCommand1 = new SqlCommand(selectStatement1, cAW);
            SqlDataReader SQLdr1 = selectCommand1.ExecuteReader();

            if (SQLdr1.HasRows)
            {
                while (SQLdr1.Read())
                {
                    dlSoftwareInstalled.Items.Insert(dlSoftwareInstalled.Items.Count, new ListItem(SQLdr1[0].ToString()));
                    //dlMake.Items.Insert(dlMake.Items.Count, new ListItem(SQLdr[3].ToString() + ", " + SQLdr[2].ToString(), SQLdr[1].ToString()));
                    //dlEquipNum.Items.Insert(dlEquipNum.Items.Count, new ListItem(SQLdr[0].ToString() + ", " + SQLdr[1].ToString(), SQLdr[0].ToString()));
                }
                dlSoftwareInstalled.Items.Insert(0, new ListItem("Select Software ", ""));
            }
            SQLdr1.Close();
            SQLdr1.Dispose();
            selectCommand1.Dispose();
        }



        protected void LoadAllSoftwareList(SqlConnection cAW)
        {
            //Clear out the modal form
            dlAddSoftwareInstalled.Items.Clear();
            string selectStatement1 = "SELECT Distinct ProductName + ' #V' + Version AS SoftwareName "
                                    + "FROM Software "
                                    + "ORDER BY SoftwareName";
            SqlCommand selectCommand1 = new SqlCommand(selectStatement1, cAW);
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
        }





        protected void SaveAddNewSoftware(SqlConnection cAW)
        {
            //A customer has been selected SaveAddNewSoftware dlEquipNum.SelectedItem.Value
            //editNSCCEquipmentNumber = dlEquipNum.SelectedItem.Value;
            editComputerID = ddlEquipNum.SelectedItem.Value;
            string softwareName = dlAddSoftwareInstalled.SelectedItem.Value;
            softwareName = softwareName.Trim();
            string[] software = softwareName.Split('#');
            string productName = software[0].Remove(software[0].Length - 1, 1);
            string version = software[1].Remove(0, 1);

            string insertStatement =
                    "INSERT SoftwareInstallations " +
                    "(NSCCEquipmentNumber, SoftwareID) " +
                    "VALUES ('"
                    + ddlEquipNum.SelectedItem.Value + "', "
                    + "(Select SoftwareID From Software Where ProductName = '" + productName + "' AND Version = '" + version + "'))";

            SqlCommand insertCommand = new SqlCommand(insertStatement, cAW);
            //int recAffected = insertCommand.ExecuteNonQuery();

            try
            {
                //connection.Open();
                int recAffected = insertCommand.ExecuteNonQuery();
                if (recAffected == 1)
                {
                    string strID = txtNSCCEquipmentNumber.Text;
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

        }

        protected void DeleteSoftwareInstalled(SqlConnection cAW)
        {
            //A customer has been selected





            editComputerID = ddlEquipNum.SelectedItem.Value;
            string softwareName = dlSoftwareInstalled.SelectedItem.Value;
            softwareName = softwareName.Trim();
            string[] software = softwareName.Split('#');
            string productName = software[0].Remove(software[0].Length - 1, 1);
            string version = software[1].Remove(0, 1);

            int locationID = 0;
            string strSQL = "Select SoftwareID From Software Where ProductName = '" + productName + "' AND Version = '" + version + "'";
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

            string deleteStatement =
                "DELETE FROM SoftwareInstallations " +
                "WHERE SoftwareID = " + locationID + " " +
                "AND NSCCEquipmentNumber = '" + ddlEquipNum.SelectedItem.Value + "'";

            //string deleteStatement =
            //    "DELETE FROM SoftwareInstallations " +
            //    "WHERE SoftwareID = (Select SoftwareID From Software Where ProductName = '" + productName + "' AND Version = '" + version + "') " +
            //    "AND NSCCEquipmentNumber = '" + ddlEquipNum.SelectedItem.Value + "'";

            SqlCommand command = new SqlCommand(deleteStatement, cAW);
            int recAffected = command.ExecuteNonQuery();

            command.Dispose();
        }

        protected void ClearFormModel()
        {
            // Clear the From because it is Add
            ddlEquipNum.SelectedIndex = 0;
            lblMake.Text = "";
            lblModel.Text = "";
            lblSerialNumber.Text = "";
            lblPurchaseOrderNumber.Text = "";
            lblPurchaseDate.Text = "";
            lblWarrantyStartDate.Text = "";
            lblCampus.Text = "";
            lblBuilding.Text = "";
            lblRoomNum.Text = "";


            // Clear the model for add
            txtNSCCEquipmentNumber.Text = "";
            txtMake.Text = "";
            txtModel.Text = "";
            txtSerialNumber.Text = "";
            txtPurchaseOrderNumber.Text = "";
            txtPurchaseDate.Text = "";
            txtWarrantyStartDate.Text = "";
            LoadEditLocationComboBox();
            txtBuilding.Text = "";
            txtRoom.Text = "";

        }



        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {
                //Open a connection to the database
                string conStr =
                    ConfigurationManager.ConnectionStrings["conAW"].ConnectionString;
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
                        case "ddlEquipNum":
                            loadDetail(conAW);
                            LoadAllSoftwareList(conAW);
                            break;
                        case "btnEdit":
                            //Call the js function to show the modal popup
                            lblAddEditComputer.InnerHtml = "Edit Computer";
                            cEdit = true;
                            Page.ClientScript.RegisterStartupScript(this.GetType(),
                                "myScript", "initMyModal('mdlNewEditComputer');", true);
                            break;
                        case "btnNewCompSave":
                            //Will need more code to see if we are editing or saving a new computer
                            if (cEdit)
                            {
                                //We are saving an edited computer
                                SaveEditComputer(conAW);
                                cEdit = false;
                                loadComputer(conAW);
                                //Now Select the edited computer record
                                for (int Loop = 0; Loop < ddlEquipNum.Items.Count; Loop++)
                                {
                                    if (ddlEquipNum.Items[Loop].Value == editComputerID)
                                    {
                                        //We found the edited computer
                                        ddlEquipNum.SelectedIndex = Loop;
                                    }
                                }

                                loadDetail(conAW);
                            }
                            else
                            {
                                //We are saving a new computer
                                SaveNewComputer(conAW);
                                //Reload the dropdown list
                                loadComputer(conAW);
                                //Now select the new computer record
                                for (int Loop = 0; Loop < ddlEquipNum.Items.Count; Loop++)
                                {
                                    if (ddlEquipNum.Items[Loop].Value == newComputerID)
                                    {
                                        //we found the new computer
                                        ddlEquipNum.SelectedIndex = Loop;
                                    }
                                }

                                //Reload the main form
                                loadDetail(conAW);
                            }

                            //Also need code to reload the dropdownlist and reload the page detail
                            break;
                        case "btnAdd":
                            //Clear out the modal form
                            ClearFormModel();
                            //txtNSCCEquipmentNumber.Text = "";
                            //txtMake.Text = "";
                            //txtModel.Text = "";
                            //txtSerialNumber.Text = "";
                            //txtPurchaseOrderNumber.Text = "";
                            //txtPurchaseDate.Text = "";
                            //txtWarrantyStartDate.Text = "";
                            //LoadEditLocationComboBox();
                            //txtBuilding.Text = "";
                            //txtRoom.Text = "";

                            lblAddEditComputer.InnerHtml = "Add New Computer";
                            Page.ClientScript.RegisterStartupScript(this.GetType(),
                                "myScript", "initMyModal('mdlNewEditComputer');", true);
                            break;
                        case "btnNewCompCancel":
                            cEdit = false;
                            break;
                        case "btnDelete":
                            lblDeleteCompMsg.InnerHtml =
                                "Are you sure you want to delete this computer: "
                                + ddlEquipNum.SelectedItem.Value + "?";
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript",
                                "initMyModal('mdlDeleteComputer');", true);
                            break;
                        case "btnDeleteCompSave":
                            DeleteComputer(conAW);
                            //Reload the dropdown list
                            loadComputer(conAW);
                            //Reload the main form
                            loadDetail(conAW);
                            break;
                        case "btnAddSoftware":
                            LoadAllSoftwareList(conAW);
                            lblAddNewSoftware.InnerHtml = "Add New Software";
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "initMyModal('mdlNewSoftwareInstall');", true);
                            break;
                        case "btnSoftwareInstallCancel":
                            loadSoftwareForComputer(conAW);
                            loadSoftwareForComputerList(conAW);
                            cEdit = false;
                            break;

                        case "btnSoftwareInstallSave":
                            {   //
                                SaveAddNewSoftware(conAW); //SaveAddNewSoftware
                                loadSoftwareForComputer(conAW);
                                loadSoftwareForComputerList(conAW);
                                //LoadAllSoftwareList(conAW);
                            }
                            //Also need code to reload the dropdownlist and reload the page detail
                            break;
                        case "btnDeleteSoftware":
                            lblDeleteComputerSWMsg.InnerHtml = "Are you sure you want to delete the Software from  this computer: " +
                                                                dlSoftwareInstalled.SelectedItem.Value + " ?";
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "initMyModal('mdlDeleteSoftware');", true);
                            break;
                        case "btnDeleteSoftwareSave":
                            //DeleteComputer(conAW);
                            //Reload the dropdown list
                            DeleteSoftwareInstalled(conAW);
                            loadSoftwareForComputer(conAW);
                            loadSoftwareForComputerList(conAW);
                            //loadComputer(conAW);
                            //Reload the main form
                            //loadDetail(conAW);
                            break;
                        case "btnDeleteSoftwareCancel":
                            loadSoftwareForComputer(conAW);
                            loadSoftwareForComputerList(conAW);
                            cEdit = false;
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
                EL.SaveErrLog("Module: DefaultPage" + Environment.NewLine
                                                    + "Function: Page_Load" + Environment.NewLine
                                                    + "Error Message: " + ex.Message);
                Response.Write(@"<script>alert('Error Message: "
                               + ex.Message.ToString() + "';</script>");
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