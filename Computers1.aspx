<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Computers1.aspx.cs" Inherits="ASPFinal.Computers1" %>


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

<%-- Modal for new/edit computer --%>
<div class="modal fade" id="mdlNewEditComputer">
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header">
                <h4 id="lblAddEditComputer" class="modal-title" runat="server">Add New Computer</h4>
            </div>
            <div class="modal-body">
                <div class="form-group">
                    <asp:TextBox CssClass="form-control" ID="txtNSCCEquipmentNumber"
                                 runat="server" placeholder="Enter NSCC Equipment #">
                    </asp:TextBox>
                </div>
                <div class="form-group">
                    <asp:TextBox CssClass="form-control" ID="txtMake"
                                 runat="server" placeholder="Enter Make">
                    </asp:TextBox>
                </div>
                <div class="form-group">
                    <asp:TextBox CssClass="form-control" ID="txtModel"
                                 runat="server" placeholder="Enter Model">
                    </asp:TextBox>
                </div>
                <div class="form-group">
                    <asp:TextBox CssClass="form-control" ID="txtSerialNumber"
                                 runat="server" placeholder="Enter Serial #">
                    </asp:TextBox>
                </div>
                <div class="form-group">
                    <asp:TextBox CssClass="form-control" ID="txtPurchaseOrderNumber"
                                 runat="server" placeholder="Enter Purchase Order #">
                    </asp:TextBox>
                </div>
                <div class="form-group">
                    <asp:TextBox CssClass="form-control" ID="txtPurchaseDate"
                                 runat="server" placeholder="Enter Purchase Date">
                    </asp:TextBox>
                </div>
                <div class="form-group">
                    <asp:TextBox CssClass="form-control" ID="txtWarrantyStartDate"
                                 runat="server" placeholder="Enter Warranty Start Date">
                    </asp:TextBox>
                </div>
                <div class="form-group">
                    <asp:DropDownList CssClass="form-control" ID="ddlLocationID"
                                      runat="server">
                    </asp:DropDownList>
                </div>
                <div class="form-group">
                    <asp:TextBox CssClass="form-control" ID="txtBuilding"
                                 runat="server" placeholder="Enter Building">
                    </asp:TextBox>
                </div>
                <div class="form-group">
                    <asp:TextBox CssClass="form-control" ID="txtRoom"
                                 runat="server" placeholder="Enter Room #">
                    </asp:TextBox>
                </div>
            </div>
            <div class="modal-footer">
                <asp:Button ID="btnNewCompCancel" runat="server" CssClass="btn btn-outline-dark" Text="Cancel"/>
                <asp:Button ID="btnNewCompSave" CssClass="btn btn-primary" runat="server" Text="Save"/>
            </div>
        </div>
    </div>
</div>
<%-- End of Modal for new/edit computer --%>

<%-- Modal form for delete --%>
<div class="modal fade" id="mdlDeleteComputer">
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header bg-danger">
                <h4 class="modal-title">Delete Employee Warning</h4>
            </div>
            <div class="modal-body">
                <div class="form-group">
                    <p id="lblDeleteCompMsg" runat="server"></p>
                </div>
                <div class="form-group">
                    <p id="lblDeleteEmpNote" runat="server"></p>
                </div>
            </div>
            <div class="modal-footer">
                <asp:Button ID="btnDeleteCompCancel" runat="server" cssclass="btn btn-primary" Text="Cancel"/>
                <asp:Button ID="btnDeleteCompSave" cssclass="btn btn-outline-dark" runat="server" Text="Delete"/>
            </div>
        </div>
    </div>
</div>
<%-- End of modal form delete --%>

<%-- Modal form for delete Software from the computer--%>
<div class="modal fade" id="mdlDeleteSoftware">
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header bg-danger">
                <h4 class="modal-title">Delete Software Warning</h4>
            </div>
            <div class="modal-body">
                <div class="form-group">
                    <p id="lblDeleteComputerSWMsg" runat="server"></p>
                </div>
                <div class="form-group">
                    <p id="lblDeleteComputerSWNote" runat="server"></p>
                </div>
            </div>
            <div class="modal-footer">
                <asp:Button ID="btnDeleteSoftwareCancel" runat="server" cssclass="btn btn-primary" Text="Cancel"/>
                <asp:Button ID="btnDeleteSoftwareSave" cssclass="btn btn-outline-dark" runat="server" Text="Delete Software"/>
            </div>
        </div>
    </div>
</div>
<%-- End of modal form delete Software from the computer--%>

<%-- Modal for new Software Installed --%>
<div class="modal fade" id="mdlNewSoftwareInstall">
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header" style="padding-left: 3em">
                <h4 id="lblAddNewSoftware" class="modal-title" runat="server">Add New Software Installed</h4>
            </div>
            <div class="modal-body">

                <div class="form-group" style="padding-left: 3em">
                    <asp:DropDownList AppendDataBoundItems="true" ID="dlAddSoftwareInstalled" CssClass="form-control" runat="server" AutoPostBack="False"></asp:DropDownList>
                    <%-- <asp:TextBox cssclass="form-control" ID="txtComputerLocationID" runat="server" placeholder="Enter LocationID"></asp:TextBox>--%>
                </div>
                <%-- <div class="form-group">
                    <asp:TextBox cssclass="form-control" ID="TextBox8" runat="server" placeholder="Enter Building"></asp:TextBox>
                </div>
                <div class="form-group">
                    <asp:TextBox cssclass="form-control" ID="TextBox9" runat="server" placeholder="Enter Room Number"></asp:TextBox>
                </div>
                    --%>
            </div>
            <div class="modal-footer">
                <asp:Button ID="btnSoftwareInstallCancel" runat="server" cssclass="btn btn-outline-dark" Text="Cancel"/>
                <asp:Button ID="btnSoftwareInstallSave" cssclass="btn btn-primary" runat="server" Text="Save"/>
            </div>
        </div>
    </div>
</div>
<%-- End of Modal for new Software Installed --%>

<%-- End Section for Modal Forms --%>


<%-- Begin Section for Computer Page Forms --%>

<section id="about">
    <div class="container">
        <div class="row">
            <div class="col-md-12">
                <h1 class="pad-top30 pad-bot30 align-center">Manage Computers</h1>
            </div>
        </div>
    </div>
</section>

<hr/>
<br/>
<section id="details">
    <div class="container-fluid">


        <section id="ComputerInfo">
            <div class="row pad-left20 pad-right20">
                <div class="col-sm-3">
                    <h3>NSCC Equipment Number</h3>
                </div>
                <div class="col-md-3">
                    <asp:DropDownList ID="ddlEquipNum" CssClass="form-control" runat="server" AutoPostBack="True"></asp:DropDownList>
                </div>
            </div>
            <br/>
            <div class="row pad-left20 pad-right20">
                <div class="col-lg-3">
                    <h3>Make</h3>
                </div>
                <div class="col-md-3">
                    <asp:Label ID="lblMake" CssClass="form-control" runat="server"></asp:Label>
                </div>
            
                <div class="col-lg-3">
                    <h3>Model</h3>
                </div>
                <div class="col-md-3">
                    <asp:Label ID="lblModel" CssClass="form-control" runat="server"></asp:Label>
                </div>
            </div>

            <div class="row pad-left20 pad-right20">
                <div class="col-lg-3">
                    <h3>Serial Number</h3>
                </div>
                <div class="col-lg-3">
                    <asp:Label ID="lblSerialNumber" CssClass="form-control" runat="server"></asp:Label>
                </div>
            
                <div class="col-lg-3">
                    <h3>Purchase Order Number</h3>
                </div>
                <div class="col-lg-3">
                    <asp:Label ID="lblPurchaseOrderNumber" CssClass="form-control" runat="server"></asp:Label>
                </div>
            </div>

            <div class="row pad-left20 pad-right20">
                <div class="col-lg-3">
                    <h3>Date of Purchase</h3>
                </div>
                <div class="col-lg-3">
                    <asp:Label ID="lblPurchaseDate" CssClass="form-control" runat="server"></asp:Label>
                </div>

                <div class="col-lg-3">
                    <h3>Warranty Start Date</h3>
                </div>
                <div class="col-lg-3">
                    <asp:Label ID="lblWarrantyStartDate" CssClass="form-control" runat="server"></asp:Label>
                </div>
            </div>
        </section>

        <br/>
        <hr/>

        <section id="LocationInfo">
            <div class="row pad-left20 pad-right20">
                <div class="col-sm-3">
                    <h3>Campus</h3>
                </div>
                <div class="col-sm-3">
                    <asp:Label ID="lblCampus" CssClass="form-control" runat="server"></asp:Label>
                </div>
                <div class="col-sm-3">
                    <h3>Building</h3>
                </div>
                <div class="col-sm-3">
                    <asp:Label ID="lblBuilding" CssClass="form-control" runat="server"></asp:Label>
                </div>
                <div class="col-sm-3 clearfix">
                    <h3>Room Number</h3>
                </div>
                <div class="col-sm-3">
                    <asp:Label ID="lblRoomNum" CssClass="form-control" runat="server"></asp:Label>
                </div>
            </div>
        </section>
        
        <br/>
        <hr/>
        <br/>
        
        <div class="container-fluid">
            <div class="row pad-left20">
                <div class="col-md-3">
                    <asp:Button ID="btnAdd" CssClass="btn btn-outline-dark" runat="server" Text="Add Computer"/>
                </div>
                <div class="col-md-3">
                    <asp:Button ID="btnEdit" CssClass="btn btn-outline-dark" runat="server" Text="Edit Computer"/>
                </div>
                <div class="col-md-3">
                    <asp:Button ID="btnDelete" CssClass="btn btn-outline-dark" runat="server" Text="Delete Computer"/>
                </div>
            </div>
        </div>

        <br/>
    </div>
    <hr/>
    <%-- End Section for Computer Page Forms --%>

    <%-- Section start adding the table for the Software Instalation for that computer selected, also have model and buttons to add or remove a software --%>

    <div class="container-fluid">
        <section id="LoadSoftwareInstalled">
            <div class="row">
                <div class="col-sm-1"></div>
                <div class="col-sm-10">
                    <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
                </div>
                <div class="col-sm-1"></div>
            </div>
        </section>

        <br/>

        <div class="row pad-left20">
            <div class="col-lg-1">
                <asp:Button ID="btnAddSoftware" CssClass="btn btn-outline-dark" runat="server" Text="Add New Software"/>
            </div>
        </div>
        <br/>
        <hr/>
        <br/>
        <div class="row pad-left20">
            <div class="col-lg-2">
                <h3>Select installed software to be deleted:</h3>
            </div>
            <div class="col-lg-3">
                <asp:DropDownList ID="dlSoftwareInstalled" CssClass="form-control" runat="server" AutoPostBack="false"></asp:DropDownList><br/>
            </div>
        </div>

       <br/>
        <div class="row pad-left20">
            <div class="col-lg-1">
                <asp:Button ID="btnDeleteSoftware" CssClass="btn btn-outline-dark" runat="server" Text="Delete Software"/>
            </div>
        </div>
    </div>
    <br/>
    <hr/>
    <br/>

    <%--
        <div class="row">
            <div class="col-sm-3">
             <asp:Button ID="addAnother" Text="Add More Software?"  OnClick="addAnother_Click" CssClass="form-check" runat="server" />              
                <asp:Button ID="bt" CssClass="btn btn-outline-dark" runat="server" Text="Add New Software" />
                <asp:Button ID="btde" CssClass="btn btn-outline-dark" runat="server" Text="Delete Software" />
                 </div>
            <div class="col-sm-6">
                
            </div>
        --%>
    <%-- <div class="col-sm-6"></div>
        </div>--%>
</section>

<br/>
<br/>

</asp:Content>
