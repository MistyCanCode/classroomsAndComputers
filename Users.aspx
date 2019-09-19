<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Users.aspx.cs" Inherits="ASPFinal.Users" %>


<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">



<%--js for the modal popup--%>
<script type="text/javascript">
    //"backdrop": "static" causes the modal to stay on top
    //"show": true shows the modal
    var options = {
        "backdrop": "static",
        "show": true
    }

    function initMyModal(ModalName) {
        $('#' + ModalName).one().modal(options);
    }
</script>


<%-- Begin Section for Modal Forms --%>

<!--Modal for new/edit user-->
<div class="modal fade" id="mdlNewEditUser">
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header">
                <h4 id="lblAddEditUser" class="modal-title" runat="server">Add New User</h4>
            </div>
            <div class="modal-body">
                <div class="form-group">
                    <asp:TextBox CssClass="form-control" ID="txtAnumber"
                                 runat="server" placeholder="Enter A Number" MaxLength="9">
                    </asp:TextBox>
                </div>
                <div class="form-group">
                    <asp:TextBox CssClass="form-control" ID="txtUserName"
                                 runat="server" placeholder="Enter User Name">
                    </asp:TextBox>
                </div>
                <div class="form-group">
                    <asp:TextBox CssClass="form-control" ID="txtPassword"
                                 runat="server" placeholder="Enter Password">
                    </asp:TextBox>
                </div>
                <div class="form-group">
                    <%-- <asp:TextBox CssClass="form-control" ID="txtSecurity" runat="server" placeholder="Enter Security Level"></asp:TextBox> --%>
                    <asp:DropDownList ID="dlSecurity" CssClass="form-control" runat="server"></asp:DropDownList>
                </div>
            </div>
            <div class="modal-footer">
                <asp:Button ID="btnNewUserCancel" runat="server" CssClass="btn btn-outline-dark"
                            Text="Cancel"/>
                <asp:Button ID="btnNewUserSave" runat="server" CssClass="btn btn-primary"
                            Text="Save"/>
            </div>
        </div>
    </div>
</div>
<!--End of new/edit user modal-->

<!-- Modal form for delete -->
<div class="modal fade" id="mdlDeleteUser" aria-hidden="true" role="dialog">
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header bg-danger">
                <h4 class="modal-title">Delete User Warning</h4>
            </div>
            <div class="modal-body">
                <div class="form-group">
                    <p id="lblDeleteUserMessage" runat="server"></p>
                </div>
            </div>
            <div class="modal-footer">
                <asp:Button ID="btnDeleteUserCancel" runat="server" CssClass="btn btn-primary"
                            Text="Cancel"/>
                <asp:Button ID="btnDeleteUserSave" runat="server" CssClass="btn btn-outline-dark"
                            Text="Delete"/>
            </div>
        </div>
    </div>
</div>
<!--End of modal form delete-->

<%-- End Section for Modal Forms --%>

<!--Beginning of main form-->

<section id="about">
    <div class="container">
        <div class="row">
            <div class="col-md-12">
                 <h1 class="pad-top30 pad-bot30 align-center">Manage Users</h1>
            </div>
        </div>
    </div>
</section>

<hr/>
    <br/>
<section id="details">
    <div class="container-fluid">
        <div class="row pad-left20">
            <div class="col-sm-3">
                <h3 >A Number</h3>
            </div>
            <div class="col-md-3">
                <asp:DropdownList ID="dlaNumber" CssClass="form-control" runat="server" AutoPostBack="True" style="padding-left: 1em"></asp:DropdownList>
            </div>
        </div>
        <br/>
        <div class="row pad-left20">
            <div class="col-sm-3">
                <h3>User Name</h3>
            </div>
            <div class="col-md-3">
                <asp:Label ID="lblUserName" CssClass="form-control" runat="server" style="padding-right: 1em"></asp:Label>
            </div>
        </div>
        <br/>
        <div class="row pad-left20">
            <div class="col-sm-3">
                <h3>Password</h3>
            </div>
            <div class="col-md-3">
                <asp:TextBox ID="txtPasswordForm" CssClass="form-control" runat="server" hidden="true" style="padding-right: 1em"></asp:TextBox>
                <asp:Label ID="lblPassword" CssClass="form-control" runat="server" style="padding-right: 1em"></asp:Label>
            </div>
        </div>
        <br/>
        <div class="row pad-left20">
            <div class="col-sm-3">
                <h3>Security Level</h3>
            </div>
            <div class="col-md-3">
                <asp:Label ID="lblSecurity" CssClass="form-control" runat="server" style="padding-left: 1em"></asp:Label>
            </div>
        </div>

        <br/>
        <hr/>
        <br/>

        <div class="container-fluid">
            <div class="row pad-left20">
                <div class="col-md-3">
                    <asp:Button ID="btnAdd" CssClass="btn btn-outline-dark" runat="server" Text="Add User"/>
                </div>
                <div class="col-md-3">
                    <asp:Button ID="btnEdit" CssClass="btn btn-outline-dark" runat="server" Text="Edit User"/>
                </div>
                <div class="col-md-3">
                    <asp:Button ID="btnDelete" CssClass="btn btn-outline-dark" runat="server" Text="Delete User"/>
                </div>
            </div>
        </div>
    </div>
    
    <br/>
    <hr/>
    <br/>
    
</section>

</asp:Content>
