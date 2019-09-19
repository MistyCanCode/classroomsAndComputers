<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="ComputersRooms.aspx.cs" Inherits="ASPFinal.ComputersRooms" %>


<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">




<%-- Each section will have its own theme --%>
<section id="section-about">
    <div class="container">
        <div class="row">
            <div class="col-md-12">
                <h1 class="pad-top30 pad-bot30 align-center">Filter computers by Campus, Building, and Room:</h1>
            </div>
        </div>
    </div>
</section>

<section id="section-details">
    <div class="container-fluid">
        <div class="card">
            <div class="card-body">
                <div class="row pad-left20 pad-top20 pad-right20 pad-bot20">
                    <div class="col-lg-4">
                        <asp:DropDownList ID="dlCampusName" CssClass="form-control" runat="server" AutoPostBack="True" style="width: 50%"></asp:DropDownList>
                    </div>
                    <div class="col-lg-4">
                        <asp:DropDownList ID="dlBuilding" CssClass="form-control" runat="server" AutoPostBack="True" style="width: 50%"></asp:DropDownList>
                    </div>
                    <div class="col-lg-4">
                        <asp:DropDownList ID="dlRoom" CssClass="form-control" runat="server" AutoPostBack="True" style="width: 50%"></asp:DropDownList>
                    </div>
                </div>
            </div>
        </div>
        <br/>
        <hr/>
        <br/>
        <div class="container-fluid">
            <div class="row pad-left20">
                <div class="col-sm-2">
                    <h3 >Computers in Selected Area:</h3>
                    <br/>
                    <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
                </div>
                <div class="col-sm-10"></div>
            </div>
        </div>

        <br/>
        <hr/>
        <div class="container-fluid">
            <div class="row pad-left20">
                <div class="col-sm-8">
                    <h3>Select a computer to display its installed software:</h3>
                    <br/>
                    <asp:DropDownList ID="dlNSCCEquipmentNumber" CssClass="form-control" runat="server" AutoPostBack="True" style="width: 50%"></asp:DropDownList>
                    <br/>
                    <hr/>
                    <br/>
                    <asp:PlaceHolder ID="PlaceHolder2" runat="server"></asp:PlaceHolder>
                </div>
                <div class="col-sm-4"></div>
                <br/>
                <hr/>
                <br/>
            </div>
        </div>
    </div>
</section>

</asp:Content>
