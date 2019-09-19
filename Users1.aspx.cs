using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using ASPFinal.Classes;

namespace ASPFinal
{
    public partial class Users1 : System.Web.UI.Page
    {

        private static bool cEdit = false;
        private static string newUserId = "";
        private static string editUserId = "";

        protected void loadUser(SqlConnection cAW)
        {
            string strSQL = "SELECT Anumber, FullName, Password, SecurityLevel "
                            + "FROM Users ";

            SqlCommand command = new SqlCommand(strSQL, cAW);
            SqlDataReader SQLdr = command.ExecuteReader();

            if (SQLdr.HasRows)
            {
                while (SQLdr.Read())
                {
                    dlaNumber.Items.Insert(dlaNumber.Items.Count, new ListItem(SQLdr[0].ToString()));
                }

                dlaNumber.Items.Insert(0, new ListItem("Select A Number ", ""));
            }

            SQLdr.Close();
            SQLdr.Dispose();
            command.Dispose();
        }

        protected void loadDetail(SqlConnection cAW)
        {
            if (dlaNumber.SelectedItem.Value != "")
            {
                //A number has been selected
                string strSQL = "SELECT ANumber, FullName, Password, SecurityLevel "
                                + "FROM Users "
                                + "WHERE ANumber = '" + dlaNumber.SelectedItem.Value + "'";
                SqlCommand command = new SqlCommand(strSQL, cAW);
                SqlDataReader SQLdr = command.ExecuteReader();

                if (SQLdr.HasRows)
                {
                    while (SQLdr.Read())
                    {
                        //save the ID of the current User
                        editUserId = SQLdr["ANumber"].ToString();
                        //load the main page
                        lblUserName.Text = SQLdr["FullName"].ToString();
                        lblPassword.Text = SQLdr["Password"].ToString();
                        lblSecurity.Text = SQLdr["SecurityLevel"].ToString();

                        //load the modal in case of an edit
                        txtAnumber.Text = SQLdr["ANumber"].ToString();
                        txtUserName.Text = SQLdr["FullName"].ToString();
                        txtPassword.Text = SQLdr["Password"].ToString();
                        txtSecurity.Text = SQLdr["SecurityLevel"].ToString();
                    }
                }

                SQLdr.Close();
                SQLdr.Dispose();
                command.Dispose();
            }
            else
            {
                //No Customer has been selected. Clear the controls
                lblUserName.Text = "";
                lblPassword.Text = "";
                lblSecurity.Text = "";

                //clear the modal form
                txtAnumber.Text = "";
                txtUserName.Text = "";
                txtPassword.Text = "";
                txtSecurity.Text = "";
            }
        }

        protected void SaveNewUser(SqlConnection cAW)
        {
            //ANum selected
            string userString = "INSERT Users" +
                                " (ANumber, FullName, Password, SecurityLevel) VALUES ('"
                                + txtAnumber.Text + "', '"
                                + txtUserName.Text + "', '"
                                + txtPassword.Text + "', '"
                                + txtSecurity.Text + "')";

            SqlCommand command = new SqlCommand(userString, cAW);
            int recAffected = command.ExecuteNonQuery();
            if (recAffected == 1)
            {
                string strID = "Select ";
                SqlCommand commandID = new SqlCommand(strID, cAW);
                SqlDataReader SQLdr = commandID.ExecuteReader();

                if (SQLdr.HasRows)
                {
                    while (SQLdr.Read())
                    {
                        //load the main page
                        newUserId = SQLdr[0].ToString();
                    }
                }

                SQLdr.Close();
                SQLdr.Dispose();
                command.Dispose();
            }

            command.Dispose();
        }

        protected void SaveEditUser(SqlConnection cAW)
        {
            if (dlaNumber.SelectedItem.Value != "")
            {
                //ANum selected
                string saveEditString = "UPDATE Users SET " +
                                        "ANumber = '" + txtAnumber.Text + "'," +
                                        "FullName = '" + txtUserName.Text + "'," +
                                        "Password = '" + txtPassword.Text + "'," +
                                        "SecurityLevel = '" + txtSecurity.Text + "' " +
                                        "WHERE ANumber = '" + dlaNumber.SelectedItem.Value + "'";

                SqlCommand command = new SqlCommand(saveEditString, cAW);
                int recAffected = command.ExecuteNonQuery();

                command.Dispose();
            }
        }



        protected void DeleteUser(SqlConnection cAW)
        {
            string deleteString = "DELETE FROM Users WHERE ANumber = "
                                  + dlaNumber.SelectedItem.Value + ";";

            SqlCommand command = new SqlCommand(deleteString, cAW);
            int recAffected = command.ExecuteNonQuery();

            command.Dispose();
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
                    loadUser(conAW);
                }
                else
                {
                    //We need to find out what control caused the postback
                    getPostBackControl clsCtl = new getPostBackControl();
                    string ctl = clsCtl.getPostBackControlName(Page);
                    switch (ctl)
                    {
                        case "dlaNumber":
                            loadDetail(conAW);
                            break;
                        case "btnEdit":
                            //Call the js function to show the modal popup
                            lblAddEditUser.InnerHtml = "Edit User";
                            cEdit = true;
                            Page.ClientScript.RegisterStartupScript(this.GetType(),
                                "myScript", "initMyModal('mdlNewEditUser');", true);
                            break;
                        case "btnNewUserSave":
                            //We need to see if we are saving or editing
                            if (cEdit)
                            {
                                //saving an edited user
                                SaveEditUser(conAW);
                                cEdit = false;
                                loadUser(conAW);
                                //select the edited user record
                                for (int Loop = 0; Loop < dlaNumber.Items.Count; Loop++)
                                {
                                    if (dlaNumber.Items[Loop].Value == editUserId)
                                    {
                                        //we have found the edited user
                                        dlaNumber.SelectedIndex = Loop;
                                    }
                                }

                                loadDetail(conAW);
                            }
                            else
                            {
                                //we are saving a new user
                                SaveNewUser(conAW);
                                //reload the list
                                loadUser(conAW);
                                //now select the new user record
                                for (int Loop = 0; Loop < dlaNumber.Items.Count; Loop++)
                                {
                                    if (dlaNumber.Items[Loop].Value == newUserId)
                                    {
                                        //we found the new user
                                        dlaNumber.SelectedIndex = Loop;
                                    }
                                }

                                //reload the form
                                loadDetail(conAW);
                            }

                            break;
                        case "btnAdd":
                            //clear out modal form
                            txtAnumber.Text = "";
                            txtUserName.Text = "";
                            txtPassword.Text = "";
                            txtSecurity.Text = "";
                            lblAddEditUser.InnerHtml = "Add new User";
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "mysScript",
                                "initMyModal('mdlNewEditUser');", true);
                            break;
                        case "btnNewUserCancel":
                            cEdit = false;
                            break;
                        case "btnDelete":
                            lblDeleteUserMessage.InnerHtml = "Are you sure you want to delete the User: "
                                                             + dlaNumber.SelectedItem.Value + " ?";
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript",
                                "initMyModal('mdlDeleteUser');", true);
                            break;
                        case "btnDeleteUserSave":
                            DeleteUser(conAW);
                            //Reload user list
                            loadUser(conAW);
                            //Reload main form
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
                EL.SaveErrLog("Module: DefaultPage" + Environment.NewLine + "Function: Page_Load"
                              + Environment.NewLine + "ErrorMessage: " + ex.Message);
                Response.Write(@"<script>alert('Error Message: " + ex.Message.ToString()
                                                                 + "');</script>");
            }


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
    }
}