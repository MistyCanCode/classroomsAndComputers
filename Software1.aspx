<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Software1.aspx.cs" Inherits="ASPFinal.Software1" %>


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

<%-- Modal for new/edit software --%>
<div class="modal fade" id="mdlNewEditSoftware">
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header">
                <h4 id="lblAddEditSoftware" class="modal-title" runat="server">Add New Software</h4>
            </div>
            <div class="modal-body">
                <div class="form-group">
                    <asp:TextBox CssClass="form-control" ID="txtSoftID" runat="server" placeholder="Auto Number"></asp:TextBox>
                </div> <%-- --%>
                <div class="form-group">
                    <asp:TextBox CssClass="form-control" ID="txtDeveloper" runat="server" placeholder="Software Developer"></asp:TextBox>
                </div>
                <div class="form-group">
                    <asp:TextBox CssClass="form-control" ID="txtProduct" runat="server" placeholder="Software Name"></asp:TextBox>
                </div>

                <div class="form-group">
                    <asp:TextBox CssClass="form-control" ID="txtVersion" runat="server" placeholder="Version Number" MaxLength="10"></asp:TextBox>
                </div>
                <div class="form-group">
                    <asp:TextBox CssClass="form-control" ID="txtLicenseKey" runat="server" placeholder="License Key:"></asp:TextBox>
                </div>

            </div>
            <div class="modal-footer">
                <asp:Button ID="btnNewSoftwareCancel" runat="server" CssClass="btn btn-outline-dark" Text="Cancel"/>
                <asp:Button ID="btnNewSoftwareSave" CssClass="btn btn-primary" runat="server" Text="Save"/>
            </div>
        </div>
    </div>
</div>
<%-- End of new/edit software modal --%>

<%-- Modal form for delete --%>
<div class="modal fade" id="mdlDeleteSoftware">
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header bg-danger">
                <h4 class="modal-title">Delete Software Warning</h4>
            </div>
            <div class="modal-body">
                <div class="form-group">
                    <p id="lblDeleteSoftMsg" runat="server"></p>
                </div>
                <div class="form-group">
                    <p id="lblDeleteSoftNote" runat="server"></p>
                </div>
            </div>
            <div class="modal-footer">
                <asp:Button ID="btnDeleteSoftCancel" runat="server" cssclass="btn btn-primary" Text="Cancel"/>
                <asp:Button ID="btnDeleteSoftSave" cssclass="btn btn-outline-dark" runat="server" Text="Delete"/>
            </div>
        </div>
    </div>
</div>
<!-- End of modal form delete -->

<%-- End Section for Modal Forms --%>

<!--Beginning of main form-->

<section id="about">
    <div class="container">
        <div class="row">
            <div class="col-md-12">
                <h1 class="pad-top30 pad-bot30 align-center">Manage Software</h1>
            </div>
        </div>
    </div>
</section>

<hr/>
<br/>
<section id="details">
    <div class="container-fluid">
        <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
        <div class="row pad-left20 pad-right20">
            <div class="col-sm-3">
                <h3 >Software Developer</h3>
            </div>
            <div class="col-md-3">
                <asp:DropdownList ID="dlSoftNum" CssClass="form-control" runat="server" AutoPostBack="True"></asp:DropdownList>
            </div>
        </div>
        <br/>
        <div class="row pad-left20 pad-right20">
            <div class="col-sm-3">
                <h3>Product Name</h3>
            </div>
            <div class="col-md-3">
                <asp:DropdownList ID="dlProduct" CssClass="form-control" runat="server" AutoPostBack="True"/>
            </div>
        </div>
        <br/>
        <div class="row pad-left20 pad-right20">
            <div class="col-sm-3">
                <h3>Version</h3>
            </div>
            <div class="col-md-3">
                <asp:DropdownList ID="dlVersion" CssClass="form-control" runat="server" AutoPostBack="True"/>
            </div>
        </div>
        <br/>
        <div class="row pad-left20 pad-right20">
            <div class="col-sm-3">
                <h3>License Key</h3>
            </div>
            <div class="col-md-3">
                <asp:Label ID="lblLicense" CssClass="form-control" runat="server"/>
            </div>
        </div>

        <br/>
        <hr/>
        <br/>

        <div class="container-fluid">
            <div class="row pad-left20">
                <div class="col-md-3">
                    <asp:Button ID="btnAdd" CssClass="btn btn-outline-dark" runat="server" Text="Add Software"/>
                </div>
                <div class="col-md-3">
                    <asp:Button ID="btnEdit" CssClass="btn btn-outline-dark" runat="server" Text="Edit Software"/>
                </div>
                <div class="col-md-3">
                    <asp:Button ID="btnDelete" CssClass="btn btn-outline-dark" runat="server" Text="Delete Software"/>
                </div>
            </div>
        </div>
    </div>
</section>
<br/>
<hr/>
<br/>

<section>
    <div class="container-fluid">
        <div class="row" style="margin-left: auto; margin-right: auto">
            <asp:GridView ID="gvSoftware" runat="server" AutoGenerateColumns="false" DataKeyNames="SoftwareID"
                          BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3">
                <%-- Theme Properties --%>
                <FooterStyle BackColor="White" ForeColor="#000066"/>
                <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White"/>
                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left"/>
                <RowStyle ForeColor="#000066"/>
                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White"/>
                <SortedAscendingCellStyle BackColor="#F1F1F1"/>
                <SortedAscendingHeaderStyle BackColor="#007DBB"/>
                <SortedDescendingCellStyle BackColor="#CAC9C9"/>
                <SortedDescendingHeaderStyle BackColor="#00547E"/>

                <Columns>
                    <asp:TemplateField HeaderText="Developer">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("Developer") %>' runat="server"/>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtDeveloper" Text='<%# Eval("Developer") %>' runat="server"/>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtDeveloperFooter" runat="server"/>
                        </FooterTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="ProductName">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("ProductName") %>' runat="server"/>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtProductName" Text='<%# Eval("ProductName") %>' runat="server"/>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtProductNameFooter" runat="server"/>
                        </FooterTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Version">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("Version") %>' runat="server"/>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtVersion" Text='<%# Eval("Version") %>' runat="server"/>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtVersionFooter" runat="server"/>
                        </FooterTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="LicenseKey">
                        <ItemTemplate>
                            <asp:Label Text='<%# Eval("LicenseKey") %>' runat="server"/>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtLicenseKey" Text='<%# Eval("LicenseKey") %>' runat="server"/>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtLicenseKeyFooter" runat="server"/>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <%-- 
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:ImageButton ImageUrl="~/Images/edit.png" runat="server" CommandName="Edit" ToolTip="Edit" width="20px" Height="20px"/>
                        <asp:ImageButton ImageUrl="~/Images/delete.png" runat="server" CommandName="Delete" ToolTip="Delete" width="20px" Height="20px"/>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:ImageButton ImageUrl="~/Images/save.png" runat="server" CommandName="Update" ToolTip="Update" width="20px" Height="20px"/>
                        <asp:ImageButton ImageUrl="~/Images/cancel.png" runat="server" CommandName="Cancel" ToolTip="Cancel" width="20px" Height="20px"/>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:ImageButton ImageUrl="~/Images/addnew.png" runat="server" CommandName="AddNew" ToolTip="Add New" width="20px" Height="20px"/>
                    </FooterTemplate>
                </asp:TemplateField>
                        --%>
                </Columns>
            </asp:GridView>
        </div>
    </div>
</section>

<%-- End Section for Software Page Forms --%>

<br/>
<br/>

</asp:Content>
