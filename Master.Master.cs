using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ASPFinal.Classes;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace ASPFinal
{
    public partial class Master : System.Web.UI.MasterPage
    {

        private static bool showAdmin = false;
        private static bool showTech = false;
        private static string userName = "";

        protected void NavbarSetup(bool showAdminLinks, bool showTechLinks)
        {
            if (showAdminLinks && Session["USERTYPE"].ToString() == "Admin")
            {
                //Add a class to hide the dropdown login. Applied to the <a
                navLogin.Attributes["class"] = "invisible";
                //Remove the class attribute invisible from the logout asp linkbutton
                lnkLogout.CssClass = "nav-link";
                //mnuComputers.Attributes["class"] = "nav-link";
                computer.Attributes["class"] = "nav-link menu";
                mnuComputersRooms.Attributes["class"] = "nav-link menu";
                mnuUsers.Attributes["class"] = "nav-link menu";
                //mnuSoftwares.Attributes["class"] = "nav-link";
                //users.Attributes["class"] = "nav-link";
                software.Attributes["class"] = "nav-link menu";
                mlblUser.Text = "Welcome " + Session["EMPNAME"].ToString();
            }
            else if (showTechLinks && Session["USERTYPE"].ToString() == "Tech")
            {
                //Add a class to hide the dropdown login. Applied to the <a
                navLogin.Attributes["class"] = "invisible";
                //Remove the class attribute invisible from the logout asp linkbutton
                lnkLogout.CssClass = "nav-link";
                //mnuComputers.Attributes["class"] = "nav-link";
                computer.Attributes["class"] = "nav-link menu";
                mnuComputersRooms.Attributes["class"] = "nav-link menu";
                //mnuUsers.Attributes["class"] = "nav-link";
                //mnuSoftwares.Attributes["class"] = "nav-link";
                //users.Attributes["class"] = "nav-link";
                software.Attributes["class"] = "nav-link menu";
                mlblUser.Text = "Welcome " + Session["EMPNAME"].ToString();
            }
            else
            {
                navLogin.Attributes["class"] = "dropdown-toggle";
                lnkLogout.CssClass = "invisible";
                //mnuComputers.Attributes["class"] = "invisible";
                computer.Attributes["class"] = "invisible";
                mnuComputersRooms.Attributes["class"] = "invisible";
                mnuUsers.Attributes["class"] = "invisible";
                //mnuSoftwares.Attributes["class"] = "invisible";
                //users.Attributes["class"] = "invisible";
                software.Attributes["class"] = "invisible";
                mlblUser.Text = "";
            }
        }

        protected void LoginSetup()
        {
            //Need a test to see if we have a valid username and password
            //Typically this is stored in a DB
            bool bValidLogin = false;
            string conStr = ConfigurationManager.ConnectionStrings["conAW"].ConnectionString;
            SqlConnection conAW = new SqlConnection(conStr);
            conAW.Open();

            string strSQL = "SELECT Anumber, FullName, Password, SecurityLevel " +
                            "FROM Users ";

            SqlCommand command = new SqlCommand(strSQL, conAW);
            SqlDataReader SQLdr = command.ExecuteReader();

            if (SQLdr.HasRows)
            {
                while (SQLdr.Read())
                {
                    if (txtUserID.Text.ToString().Trim().ToUpper() == SQLdr[0].ToString().Trim().ToUpper() && txtPassword.Text.ToString() == SQLdr[2].ToString())
                    {
                        bValidLogin = true;
                        userName = SQLdr[1].ToString();
                        if (SQLdr[3].ToString().Trim().ToUpper() == "ADMIN")
                        {
                            showAdmin = true;
                            showTech = false;
                        }
                        else
                        {
                            showTech = true;
                            showAdmin = false;
                        }
                    }
                    //ddlEquipNum.Items.Insert(ddlEquipNum.Items.Count, new ListItem(SQLdr[0].ToString()));
                }
                //ddlEquipNum.Items.Insert(0, new ListItem("Select a Computer Equipment Number", ""));
            }

            SQLdr.Close();
            SQLdr.Dispose();
            command.Dispose();

            conAW.Close();
            conAW.Dispose();

            //if (txtUserID.Text.ToString().Trim().ToUpper() == "ADMIN"
            //    && txtPassword.Text.ToString().Trim() == "ITRocks")
            //    bValidLogin = true;

            if (bValidLogin)
            {
                if (showAdmin)
                {
                    Session["USERTYPE"] = "Admin"; //Used if you have differt types
                    Session["VALIDLOGIN"] = "true";
                    mlblUser.Text = "Welcome " + userName.ToString();
                    Session["EMPNAME"] = userName.ToString();

                    NavbarSetup(showAdmin, showTech);
                }
                else if(showTech)
                {
                    Session["USERTYPE"] = "Tech"; //Used if you have differt types
                    Session["VALIDLOGIN"] = "true";
                    mlblUser.Text = "Welcome " + userName.ToString();
                    Session["EMPNAME"] = userName.ToString();

                    NavbarSetup(showAdmin, showTech);
                }

            }
            else
            {
                Session["USERTYPE"] = "";
                Session["VALIDLOGIN"] = "false";
                mlblUser.Text = "";
                Session["EMPNAME"] = "";
                NavbarSetup(false, false);
                //To make sure we move to the main web page on a logout
                Response.Redirect("Default.aspx");
            }
            //Cleanup the login textboxes
            txtUserID.Text = "";
            txtPassword.Text = "";
        }

        protected void LogoutSetup()
        {
            Session["USERTYPE"] = "";
            Session["VALIDLOGIN"] = "false";
            mlblUser.Text = "";
            Session["EMPNAME"] = "";
            NavbarSetup(false, false);

            //To make sure we move to the main web page on a logout
            Response.Redirect("Default.aspx");
        }


        protected void Page_Load(object sender, EventArgs e)
        {

            //IsPostback will not work if you are opening a new child page
            //To stop the user from clicking the back button after they leave the page
            //This is not a great solution but it gets the job done
            if (!IsPostBack)
            {
                Response.Cache.SetNoStore();
                Response.Cache.AppendCacheExtension("no-cache");
                Response.Expires = 0;
            }

            string strValidLogin = "";

            //Will need to test to see if a session variable VALIDLOGIN to know if you are posting back
            if (Session["VALIDLOGIN"] != null) //A user has already possibly logedin
                strValidLogin = Session["VALIDLOGIN"].ToString();
            if (strValidLogin == "true")
                NavbarSetup(showAdmin, showTech);
            else
                NavbarSetup(false, false);

            getPostBackControl FindCtrl = new getPostBackControl();
            string CtrlName = FindCtrl.getPostBackControlName(Page);

            switch (CtrlName)
            {
                case "btnLogin":
                    LoginSetup();
                    break;
                case "lnkLogout":
                    LogoutSetup();
                    break;
            }

        }
    }
}