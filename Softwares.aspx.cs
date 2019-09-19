using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using ASPFinal.Classes;
using System.Data.SqlClient;
using System.Configuration;

namespace ASPFinal
{
    public partial class Softwares : System.Web.UI.Page
    {

        string connectionString = "Data Source=nsccsqlinst16.nscc.edu;Database=CITC_BTEAM;UID=CITC_BTEAM;PWD=ITROCKS;";
        string conStr =  ConfigurationManager.ConnectionStrings["conAW"].ConnectionString;
        //SqlConnection conAW = new SqlConnection(conStr);

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
                    PopulateGridview(conAW);
                }
                else
                {
                    //We need to find out what control caused the postback
                    getPostBackControl clsCtl = new getPostBackControl();
                    string ctl = clsCtl.getPostBackControlName(Page);
                    PopulateGridview(conAW);
                    //switch (ctl)
                    //{
                    //    case "ddlEquipNum":
                    //        loadDetail(conAW);
                    //        LoadAllSoftwareList(conAW);
                    //        break;
                    //    case "btnEdit":
                    //        //Call the js function to show the modal popup
                    //        lblAddEditComputer.InnerHtml = "Edit Computer";
                    //        cEdit = true;
                    //        Page.ClientScript.RegisterStartupScript(this.GetType(),
                    //            "myScript", "initMyModal('mdlNewEditComputer');", true);
                    //        break;
                    //    case "btnNewCompSave":
                    //        //Will need more code to see if we are editing or saving a new computer
                    //        if (cEdit)
                    //        {
                    //            //We are saving an edited computer
                    //            SaveEditComputer(conAW);
                    //            cEdit = false;
                    //            loadComputer(conAW);
                    //            //Now Select the edited computer record
                    //            for (int Loop = 0; Loop < ddlEquipNum.Items.Count; Loop++)
                    //            {
                    //                if (ddlEquipNum.Items[Loop].Value == editComputerID)
                    //                {
                    //                    //We found the edited computer
                    //                    ddlEquipNum.SelectedIndex = Loop;
                    //                }
                    //            }

                    //            loadDetail(conAW);
                    //        }
                    //        else
                    //        {
                    //            //We are saving a new computer
                    //            SaveNewComputer(conAW);
                    //            //Reload the dropdown list
                    //            loadComputer(conAW);
                    //            //Now select the new computer record
                    //            for (int Loop = 0; Loop < ddlEquipNum.Items.Count; Loop++)
                    //            {
                    //                if (ddlEquipNum.Items[Loop].Value == newComputerID)
                    //                {
                    //                    //we found the new computer
                    //                    ddlEquipNum.SelectedIndex = Loop;
                    //                }
                    //            }

                    //            //Reload the main form
                    //            loadDetail(conAW);
                    //        }

                    //        //Also need code to reload the dropdownlist and reload the page detail
                    //        break;
                    //    case "btnAdd":
                    //        //Clear out the modal form
                    //        txtNSCCEquipmentNumber.Text = "";
                    //        txtMake.Text = "";
                    //        txtModel.Text = "";
                    //        txtSerialNumber.Text = "";
                    //        txtPurchaseOrderNumber.Text = "";
                    //        txtPurchaseDate.Text = "";
                    //        txtWarrantyStartDate.Text = "";
                    //        LoadEditLocationComboBox();
                    //        txtBuilding.Text = "";
                    //        txtRoom.Text = "";

                    //        lblAddEditComputer.InnerHtml = "Add New Computer";
                    //        Page.ClientScript.RegisterStartupScript(this.GetType(),
                    //            "myScript", "initMyModal('mdlNewEditComputer');", true);
                    //        break;
                    //    case "btnNewCompCancel":
                    //        cEdit = false;
                    //        break;
                    //    case "btnDelete":
                    //        lblDeleteCompMsg.InnerHtml =
                    //            "Are you sure you want to delete this computer: "
                    //            + ddlEquipNum.SelectedItem.Value + "?";
                    //        Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript",
                    //            "initMyModal('mdlDeleteComputer');", true);
                    //        break;
                    //    case "btnDeleteCompSave":
                    //        DeleteComputer(conAW);
                    //        //Reload the dropdown list
                    //        loadComputer(conAW);
                    //        //Reload the main form
                    //        loadDetail(conAW);
                    //        break;
                    //    case "btnAddSoftware":
                    //        LoadAllSoftwareList(conAW);
                    //        lblAddNewSoftware.InnerHtml = "Add New Software";
                    //        Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "initMyModal('mdlNewSoftwareInstall');", true);
                    //        break;
                    //    case "btnSoftwareInstallCancel":
                    //        loadSoftwareForComputer(conAW);
                    //        loadSoftwareForComputerList(conAW);
                    //        cEdit = false;
                    //        break;

                    //    case "btnSoftwareInstallSave":
                    //        {   //
                    //            SaveAddNewSoftware(conAW); //SaveAddNewSoftware
                    //            loadSoftwareForComputer(conAW);
                    //            loadSoftwareForComputerList(conAW);
                    //            //LoadAllSoftwareList(conAW);
                    //        }
                    //        //Also need code to reload the dropdownlist and reload the page detail
                    //        break;
                    //    case "btnDeleteSoftware":
                    //        lblDeleteComputerSWMsg.InnerHtml = "Are you sure you want to delete the Software from  this computer: " +
                    //                                            dlSoftwareInstalled.SelectedItem.Value + " ?";
                    //        Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "initMyModal('mdlDeleteSoftware');", true);
                    //        break;
                    //    case "btnDeleteSoftwareSave":
                    //        //DeleteComputer(conAW);
                    //        //Reload the dropdown list
                    //        DeleteSoftwareInstalled(conAW);
                    //        loadSoftwareForComputer(conAW);
                    //        loadSoftwareForComputerList(conAW);
                    //        //loadComputer(conAW);
                    //        //Reload the main form
                    //        //loadDetail(conAW);
                    //        break;
                    //    case "btnDeleteSoftwareCancel":
                    //        loadSoftwareForComputer(conAW);
                    //        loadSoftwareForComputerList(conAW);
                    //        cEdit = false;
                    //        break;


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




            //if (!IsPostBack)
            //{
            //    PopulateGridview(conAW);
            //}
            //To stop the user from entering this page if they are not logged in.
            //To stop the user from entering this page if they are not logged in.
            string strValidLogin = "";
            string userType = "";
            if (Session["VALIDLOGIN"] != null)
            {
                strValidLogin = Session["VALIDLOGIN"].ToString();
                userType = Session["USERTYPE"].ToString();
                if (strValidLogin == "false" || userType == "Tech")
                    Response.Redirect("Default.aspx");
            }
            else
                Response.Redirect("Default.aspx");

        }

        void PopulateGridview(SqlConnection cAW)
        {

            //string conStr = ConfigurationManager.ConnectionStrings["conAW"].ConnectionString;
            //SqlConnection conAW = new SqlConnection(conStr);


            DataTable dtbl = new DataTable();
            //SqlCommand command = new SqlCommand(strSQL, cAW);
            //SqlDataReader SQLdr = command.ExecuteReader();



            //using (SqlConnection sqlCon = new SqlConnection(cAW))
            //{
            //    sqlCon.Open();
            //    SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * FROM Software",sqlCon);
            //    sqlDa.Fill(dtbl);
            //}
            string strSQL1 = "Select * from Software";

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(strSQL1, cAW);
            DataTable softwaretbl = new DataTable();
            sqlDataAdapter.Fill(softwaretbl);
            //lvSoftwareInstalled.DataSource = softwareInstalled;
            //gvSoftwareInstalled.DataSource = softwareInstalled;
            sqlDataAdapter.Dispose();


            gvSoftware.DataSource = softwaretbl;
            gvSoftware.DataBind();

            gvSoftware.Rows[0].Cells.Clear();
            gvSoftware.Rows[0].Cells.Add(new TableCell());
            gvSoftware.Rows[0].Cells[0].ColumnSpan = dtbl.Columns.Count;
            gvSoftware.Rows[0].Cells[0].Text = "No Data Found ..!";
            gvSoftware.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;


        }


        protected void gvSoftware_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName.Equals("AddNew"))
                {
                    //using (SqlConnection con = new SqlConnection("Data Source=nsccsqlinst16.nscc.edu;Database=CITC_BTEAM;UID=CITC_BTEAM;PWD=ITROCKS;"))
                    using (SqlConnection sqlCon = new SqlConnection("Data Source=nsccsqlinst16.nscc.edu;Database=CITC_BTEAM;UID=CITC_BTEAM;PWD=ITROCKS;"))
                    {
                        sqlCon.Open();
                        string query = "INSERT INTO Software (Developer,ProductName,Version,LicenseKey) VALUES (@Developer,@ProductName,@Version,@LicenseKey)";
                        SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                        sqlCmd.Parameters.AddWithValue("@Developer", (gvSoftware.FooterRow.FindControl("txtDeveloperFooter") as TextBox).Text.Trim());
                        sqlCmd.Parameters.AddWithValue("@ProductName", (gvSoftware.FooterRow.FindControl("txtProductNameFooter") as TextBox).Text.Trim());
                        sqlCmd.Parameters.AddWithValue("@Version", (gvSoftware.FooterRow.FindControl("txtVersionFooter") as TextBox).Text.Trim());
                        sqlCmd.Parameters.AddWithValue("@LicenseKey", (gvSoftware.FooterRow.FindControl("txtLicenseKeyFooter") as TextBox).Text.Trim());
                        sqlCmd.ExecuteNonQuery();
                        PopulateGridview(sqlCon);
                        lblSuccessMessage.Text = "New Record Added";
                        lblErrorMessage.Text = "";
                    }
                }
            }
            catch (Exception ex)
            {
                lblSuccessMessage.Text = "";
                lblErrorMessage.Text = ex.Message;
            }
        }

        protected void gvSoftware_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvSoftware.EditIndex = e.NewEditIndex;
            SqlConnection conAW = new SqlConnection(conStr);
            PopulateGridview(conAW);
        }

        protected void gvSoftware_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvSoftware.EditIndex = -1;
            SqlConnection conAW = new SqlConnection(conStr);
            PopulateGridview(conAW);
        }

        protected void gvPhoneBook_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(connectionString))
                {
                    sqlCon.Open();
                    string query = "UPDATE Software SET Developer=@Developer,ProductName=@ProductName,Version=@Version,LicenseKey=@LicenseKey WHERE SoftwareID = @id";
                    SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                    sqlCmd.Parameters.AddWithValue("@Developer", (gvSoftware.Rows[e.RowIndex].FindControl("txtDeveloper") as TextBox).Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@ProductName", (gvSoftware.Rows[e.RowIndex].FindControl("txtProductName") as TextBox).Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@Version", (gvSoftware.Rows[e.RowIndex].FindControl("txtVersion") as TextBox).Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@LicenseKey", (gvSoftware.Rows[e.RowIndex].FindControl("LicenseKey") as TextBox).Text.Trim());
                    sqlCmd.Parameters.AddWithValue("@id", Convert.ToInt32(gvSoftware.DataKeys[e.RowIndex].Value.ToString()));
                    sqlCmd.ExecuteNonQuery();
                    gvSoftware.EditIndex = -1;
                    PopulateGridview(sqlCon);
                    lblSuccessMessage.Text = "Selected Record Updated";
                    lblErrorMessage.Text = "";
                }
            }
            catch (Exception ex)
            {
                lblSuccessMessage.Text = "";
                lblErrorMessage.Text = ex.Message;
            }
        }

        protected void gvPhoneBook_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                using (SqlConnection sqlCon = new SqlConnection(connectionString))
                {
                    sqlCon.Open();
                    string query = "DELETE FROM Software WHERE SoftwareID = @id";
                    SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                    sqlCmd.Parameters.AddWithValue("@id", Convert.ToInt32(gvSoftware.DataKeys[e.RowIndex].Value.ToString()));
                    sqlCmd.ExecuteNonQuery();
                    PopulateGridview(sqlCon);
                    lblSuccessMessage.Text = "Selected Record Deleted";
                    lblErrorMessage.Text = "";
                }
            }
            catch (Exception ex)
            {
                lblSuccessMessage.Text = "";
                lblErrorMessage.Text = ex.Message;
            }
        }



    }
}