<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Computers.aspx.cs" Inherits="ASPFinal.Computers" %>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <link href="CSS/Sections.css" rel="stylesheet" />
    <link href="Content/animate.css" rel="stylesheet" />
    <%-- https://github.com/michalsnik/aos --%>
    <%-- http://michalsnik.github.io/aos/ --%>
    <link href="Content/aos.css" rel="stylesheet" />
    <script src="Scripts/aos.js"></script>

    <script>
        AOS.init();
    </script>

    <%-- Each section will have its own theme --%>
	<section id="section-about" class="section about-theme1"> 
	    <div class="container" >  
			<div class="row" > 
				<div class="col-md-offset-2 col-md-8">
					<div class="section-header" data-aos="fade-down" data-aos-easing="ease-in-out-sine" data-aos-duration="600">
						<h1 class="align-center">Section About Text</h1>
						<h3 class="align-center">By Your Company</h3>
					</div>
				</div>
			</div>
        </div>

        
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
    }</script>


        <%-- This section is for the modal forms --%>
        <%-- Modal for new/edit employee --%>
        <div class="modal fade" id="mdlNewEditComputer">
          <div class="modal-dialog">
            <div class="modal-content">
              <div class="modal-header">
                <h4 id="lblAddEditCourse" class="modal-title" runat="server">Add New Computer</h4>
              </div>
              <div class="modal-body">

                <div class="form-group">
                    <asp:TextBox cssclass="form-control" ID="txtComputerNSCCeNumber" runat="server" placeholder="Enter NSCC Equipment Number" Enabled="False"></asp:TextBox>
                </div>
                <div class="form-group">
                    <asp:TextBox cssclass="form-control" ID="txtComputerMake" runat="server" placeholder="Enter Computer Make"></asp:TextBox>
                </div>
                <div class="form-group">
                    <asp:TextBox cssclass="form-control" ID="txtComputerModel" runat="server" placeholder="Enter Computer Model"></asp:TextBox>
                </div>
                <div class="form-group">
                    <asp:TextBox cssclass="form-control" ID="txtComputerSerialNumber" runat="server" placeholder="Enter Serial Number"></asp:TextBox>
                </div>

                <div class="form-group">
                    <asp:TextBox cssclass="form-control" ID="txtComputerPONumber" runat="server" placeholder="Enter PO Number"></asp:TextBox>
                </div>
                <div class="form-group">
                    <asp:TextBox cssclass="form-control" ID="txtComputerDateOfPurchase" runat="server" placeholder="Enter Date of Purchase"></asp:TextBox>
                </div>
                <div class="form-group">
                    <asp:TextBox cssclass="form-control" ID="txtComputerWarrantyStartdate" runat="server" placeholder="Enter Warranty Start Date"></asp:TextBox>
                </div>
                <div class="form-group">
                    <asp:DropDownList AppendDataBoundItems="true" ID="dlComputerCampusName" CssClass="form-control" runat="server" AutoPostBack="False"></asp:DropDownList>
                    <%-- <asp:TextBox cssclass="form-control" ID="txtComputerLocationID" runat="server" placeholder="Enter LocationID"></asp:TextBox>--%>
                </div>
                <div class="form-group">
                    <asp:TextBox cssclass="form-control" ID="txtComputerBuilding" runat="server" placeholder="Enter Building"></asp:TextBox>
                </div>
                <div class="form-group">
                    <asp:TextBox cssclass="form-control" ID="txtComputerRoom" runat="server" placeholder="Enter Room Number"></asp:TextBox>
                </div>

              </div>
              <div class="modal-footer">
                  <asp:Button ID="btnNewComputerCancel" runat="server" cssclass="btn btn-outline-dark" Text="Cancel" />
                  <asp:Button ID="btnNewComputerSave" cssclass="btn btn-primary" runat="server" Text="Save" />
              </div>
            </div>
          </div>
        </div>
        <%-- End of Modal for new/edit course --%>
        <%-- Modal form for delete --%>
        <div class="modal fade" id="mdlDeleteComputer">
          <div class="modal-dialog">
            <div class="modal-content">
              <div class="modal-header bg-danger">
                <h4 class="modal-title">Delete Computer Warning</h4>
              </div>
              <div class="modal-body">
                <div class="form-group">
                     <p id="lblDeleteComputerMsg" runat="server"></p>
                </div>
                <div class="form-group">
                     <p id="lblDeleteComputerNote" runat="server"></p>
                </div>
              </div>
              <div class="modal-footer">
                  <asp:Button ID="btnDeleteComputerCancel" runat="server" cssclass="btn btn-primary" Text="Cancel" />
                  <asp:Button ID="btnDeleteComputerSave" cssclass="btn btn-outline-dark" runat="server" Text="Delete" />
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
                  <asp:Button ID="btnDeleteSoftwareCancel" runat="server" cssclass="btn btn-primary" Text="Cancel" />
                  <asp:Button ID="btnDeleteSoftwareSave" cssclass="btn btn-outline-dark" runat="server" Text="Delete Software" />
              </div>
            </div>
          </div>
        </div>
        <%-- End of modal form delete Software from the computer--%>

        <%-- Modal for new Software Installed --%>
        <div class="modal fade" id="mdlNewSoftwareInstall">
          <div class="modal-dialog">
            <div class="modal-content">
              <div class="modal-header">
                <h4 id="lblAddNewSoftware" class="modal-title" runat="server">Add New Software Installed</h4>
              </div>
              <div class="modal-body">

                <div class="form-group">
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
                  <asp:Button ID="btnSoftwareInstallCancel" runat="server" cssclass="btn btn-outline-dark" Text="Cancel" />
                  <asp:Button ID="btnSoftwareInstallSave" cssclass="btn btn-primary" runat="server" Text="Save" />
              </div>
            </div>
          </div>
        </div>
        <%-- End of Modal for new Software Installed --%>





   
    <div class="container-fluid">
        <div class="pad-top80 align-center">
            <h3>Computer and Software Information.</h3>
        </div>
    </div>
    <div class="container-fluid">
        <div class="row">
            <h1>Computer</h1>
        </div>
        <br />
        <div class="row">
            <div class="col-sm-2">
                <h3>NSCC Equipment Number</h3>
            </div>
            <div class="col-sm-3">
                <asp:DropDownList ID="dlEquipNum" CssClass="form-control" runat="server" AutoPostBack="True"></asp:DropDownList>
            </div>
            <div class="col-sm-1">
                <h3>Make</h3>
            </div>
                <div class="col-sm-2">
                    <%-- <asp:DropDownList ID="dlMake" CssClass="form-control" runat="server" AutoPostBack="True"></asp:DropDownList>
                     <asp:TextBox ID="txtMake" CssClass="form-control" runat="server"></asp:TextBox>--%>
                     <asp:Label ID="lblMake" CssClass="form-control" runat="server"></asp:Label>
                </div>
            <div class="col-sm-1"></div>
                <h3>Model</h3>
                   <div class="col-sm-2">
                     <asp:TextBox ID="txtModel" CssClass="form-control" runat="server"></asp:TextBox>
                   </div>
        </div>
        <br />
        <div class="row">
             <div class="col-sm-1">
                <h3>Serial Number</h3>
            </div>
            <div class="col-sm-2">
                <asp:TextBox ID="txtSerialNum" CssClass="form-control" runat="server"></asp:TextBox>
            </div>
            <div class="col-sm-1">
                <h3>Purchase Order Number</h3>
            </div>
                <div class="col-sm-2">
                     <asp:TextBox ID="txtPoNum" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
            <div class="col-sm-1"></div>
                <h3>Date of Purchase</h3>
                   <div class="col-sm-2">
                     <asp:TextBox ID="txtDatePurch" CssClass="form-control" runat="server"></asp:TextBox>
                   </div>
            <div class="col-sm-1"></div>
                <h3>Warranty Start Date</h3>
                    <div class="col-sm-2">
                        <asp:TextBox ID="txtWarrantyDate" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
        </div>
        <br />
        <hr />
        <br />
        <div class="row">
            <h1>Location</h1>
        </div>
        <br />
        <div class="row">
            <div class="col-sm-1">
                <h3>Campus</h3>
            </div>
            <div class="col-sm-2">
                <asp:TextBox ID="txtCampus" CssClass="form-control" runat="server"></asp:TextBox>
            </div>
            <div class="col-sm-1">
                <h3>Building</h3>
            </div>
            <div class="col-sm-2">
                <asp:TextBox ID="txtBuilding" CssClass="form-control" runat="server"></asp:TextBox>
            </div>
            <div class="col-sm-1">
                <h3>Room Number</h3>
            </div>
            <div class="col-sm-2">
                <asp:TextBox ID="txtRoomNum" CssClass="form-control" runat="server"></asp:TextBox>
            </div>
            <div class="col-sm-3"></div>
        </div>
       
        </div>
        <br />
        <hr />
        <br />
        <div class="row">
            <h1>Software Installed</h1>
        </div>
        <br />
        
        <div class="row">
            <div class="col-sm-1">
                <h3>Software Name</h3>
            </div>
            <div class="col-sm-2">
                <asp:TextBox ID="txtSoftName" CssClass="form-control" runat="server"></asp:TextBox>
            </div>
            <div class="col-sm-1">
                <h3>Software Manufacturer</h3>
                
            </div>
            <div class="col-sm-2">
                <asp:TextBox ID="txtSoftMan" CssClass="form-control" runat="server"></asp:TextBox>
            </div>
            <div class="col-sm-1">
                <h3>Version Number</h3>
            </div>
            <div class="col-sm-2">
                <asp:TextBox ID="txtVersion" CssClass="form-control" runat="server"></asp:TextBox>
            </div>
            <div class="col-sm-1"></div>
        </div>
        <br />
        <div class="row">
            <div class="col-sm-3">
                <asp:Button ID="addAnother" Text="Add More Software?"  OnClick="addAnother_Click" CssClass="form-check" runat="server" />
                <asp:Button ID="btnDeleteSoftware" CssClass="btn btn-outline-dark" runat="server" Text="Delete Software" />
                <asp:Button ID="btnAddSoftware" CssClass="btn btn-outline-dark" runat="server" Text="Add New Software" />
                 </div>
            <div class="col-sm-9"></div>
        </div>
        <br />
        <hr />
        <br />
         <br />
            <div class="row">
                <div class="col-sm-1"></div>
                <div class="col-sm-2">
                    <asp:Button ID="btnAdd" CssClass="btn btn-outline-dark" runat="server" Text="Add" />
                    <asp:Button ID="btnEdit" CssClass="btn btn-outline-dark" runat="server" Text="Edit" />
                    <asp:Button ID="btnDelete" CssClass="btn btn-outline-dark" runat="server" Text="Delete" />
                </div>
                <div class="col-sm-9"></div>
            </div>

        <!-- Modal -->
        <div class = "modal fade" id = "myModal" tabindex = "-1" role = "dialog" 
             aria-labelledby = "myModalLabel" aria-hidden = "true">
   
        <div class = "modal-dialog">
        <div class = "modal-content">
        <div class = "modal-header">
           <button type = "button" class = "close" data-dismiss = "modal" aria-hidden = "true">&times;</button>
            
           <h4 class = "modal-title" id = "myModalLabel">Add Software</h4>
        </div>
         
         <div class = "modal-body">
             <div class="col-sm-1">
                <h3>Software Name</h3>
            </div>
            <div class="col-sm-2">
                <asp:TextBox ID="txtAddSoftName" CssClass="form-control" runat="server"></asp:TextBox>
            </div>
            <div class="col-sm-1">
                <h3>Software Manufacturer</h3>
            </div>
            <div class="col-sm-2">
                <asp:TextBox ID="txtAddSoftMan" CssClass="form-control" runat="server"></asp:TextBox>
            </div>
            <div class="col-sm-1">
                <h3>Version Number</h3>
            </div>
            <div class="col-sm-2">
                <asp:TextBox ID="txtAddVersion" CssClass="form-control" runat="server"></asp:TextBox>
            </div>
            <div class="col-sm-1"></div>
         </div>
         
         <div class = "modal-footer">
            <button type = "button" class = "btn btn-default" data-dismiss = "modal">Cancel</button>
            
            <button type = "button" class = "btn btn-primary">Submit</button>
         </div>
         
         </div><!-- /.modal-content -->
         </div><!-- /.modal-dialog -->
  
         </div><!-- /.modal -->


            <div class="row">
        <br />
        <hr />
    </div>
        <div class="container">
            <div class="row">
                <div class="col-2"></div>        
                    <div class="col-md-4">
                        <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
                    </div>
                <br />
                <hr />
                <br />
            </div>            
        </div>

        <div class="container">
            <div class="row">
                <div class="col-2"></div>
                <div class="col-md-6">
                    <asp:ListView ID="lvSoftwareInstalled" runat="server"></asp:ListView>
                    <asp:GridView ID="gvSoftwareInstalled" runat="server"></asp:GridView>



                </div>
            </div>
        </div>




    </section>



	<section id="section-groups" class="section color-theme-1"> 
	    <div class="container"> 
			<div class="row align-center"> 
				<div class="col-md-3">
					<div class="team-member" data-aos="zoom-in-left" data-aos-easing="ease-in-out-sine" data-aos-duration="600" >
						<figure class="member-photo"><img src="Images/01.jpg" alt="" /></figure>
						<div class="team-detail">
							<h4><a href="#CEOSection">CEO</a></h4>
						</div>
					</div>
				</div>
				<div class="col-md-3">
					<div class="team-member" data-aos="zoom-in-left" data-aos-easing="ease-in-out-sine" >
						<figure class="member-photo"><img src="Images/02.jpg" alt="" /></figure>
						<div class="team-detail">
							<h4><a href="#CIOSection">CIO</a></h4>
						</div>
					</div>
				</div>
				<div class="col-md-3">
					<div class="team-member" data-aos="zoom-in-left" data-aos-easing="ease-in-out-sine" >
						<figure class="member-photo"><img src="Images/Kam.jpg" alt="" /></figure>
						<div class="team-detail">
							<h4><a href="#CIOSection">KAMIRAN</a></h4>
						</div>
					</div>
				</div>
				<div class="col-md-3">
					<div class="team-member" data-aos="zoom-in-left">
						<figure class="member-photo"><img src="Images/03.jpg" alt="" /></figure>
						<div class="team-detail">
    						<h4><a href="#COOSection">COO</a></h4>
						</div>
					</div>
				</div>
			</div>
		</div>
	</section>






</asp:Content>
