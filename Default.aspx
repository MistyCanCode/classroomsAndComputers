<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ASPFinal.Default" %>


<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <%-- ContentPlaceHolder1 indicates the name of the placeholder on the Master page where your HTML will be placed at run time --%>

    <div class="container-fluid">
        <div class="pad-top30 pad-bot30 align-center">
            <h3>Welcome to NSCC Final Project 2312 - Team B</h3>
        </div>
    </div>

    <div class="container-fluid">
        <div class="align-center">

            <!-- Carousel Images -->

            <div id="myCarousel" class="carousel slide" data-ride="carousel" data-interval="7000">

                <!-- Indicators -->
                <ol class="carousel-indicators">
                    <li data-target="#myCarousel" data-slide-to="0" class="active"></li>
                    <li data-target="#myCarousel" data-slide-to="1"></li>
                    <li data-target="#myCarousel" data-slide-to="2"></li>
                    <li data-target="#myCarousel" data-slide-to="3"></li>
                    <li data-target="#myCarousel" data-slide-to="4"></li>
                    <li data-target="#myCarousel" data-slide-to="5"></li>
                </ol>

                <!-- Wrapper for slides -->
                <div class="carousel-inner text-center justify-content-center" role="listbox">
                    <div class="carousel-item active text-center">
                        <img class="img-fluid text-center justify-content-center" src="Images/MainCampus.jpg" alt="main campus" width="80%" height="80%"/>
                        <div class="carousel-caption d-none d-md-block">
                            <h5>Main Campus</h5>
                            <p>120 White Bridge Pike, Nashville, TN 37209</p>
                        </div>
                    </div>

                    <div class="carousel-item text-center">
                        <img class="img-fluid text-center justify-content-center" src="Images/EastDavidson.jpg" alt="east davidson campus" width="80%" height="80%"/>
                        <div class="carousel-caption d-none d-md-block">
                            <h5>East Davidson Campus</h5>
                            <p>2845 Elm Hill Pike, Nashville, TN 37214</p>
                        </div>
                    </div>

                    <div class="carousel-item text-center">
                        <img class="img-fluid text-center justify-content-center" src="Images/Southeast.jpg" alt="southeast campus" width="80%" height="80%"/>
                        <div class="carousel-caption d-none d-md-block">
                            <h5>Southeast Campus</h5>
                            <p>5248 Hickory Hollow Pkwy, Antioch, TN 37013</p>
                        </div>
                    </div>

                    <div class="carousel-item text-center">
                        <img class="img-fluid text-center justify-content-center" src="Images/Humphreys.jpg" alt="humphreys county campus" width="80%" height="80%"/>
                        <div class="carousel-caption d-none d-md-block">
                            <h5>Humphreys County Campus</h5>
                            <p>695 Holly Lane, Waverly, TN 37185</p>
                        </div>
                    </div>

                    <div class="carousel-item text-center">
                        <img class="img-fluid text-center justify-content-center" src="Images/Dickson.jpg" alt="dickson campus" width="80%" height="80%"/>
                        <div class="carousel-caption d-none d-md-block">
                            <h5>Nashville State at Dickson Renaissance Center</h5>
                            <p>855 Highway 46 S, Dickson, TN 37055</p>
                        </div>
                    </div>

                    <div class="carousel-item text-center">
                        <img class="img-fluid text-center justify-content-center" src="Images/Clarksville.jpg" alt="clarksville campus" width="80%" height="80%"/>
                        <div class="carousel-caption d-none d-md-block">
                            <h5>Clarksville Campus</h5>
                            <p>1760 Wilma Rudolph Blvd, Clarksville, TN 37040</p>
                        </div>
                    </div>
                </div>

                <!-- Left and right controls -->
                <a class="carousel-control-prev" href="#myCarousel" role="button" data-slide="prev">
                    <span class="carousel-control-prev-icon" aria-hidden="true"></span>
                    <span class="sr-only">Previous</span>
                </a>
                <a class="carousel-control-next" href="#myCarousel" role="button" data-slide="next">
                    <span class="carousel-control-next-icon" aria-hidden="true"></span>
                    <span class="sr-only">Next</span>
                </a>
            </div>

        </div>
    </div>


</asp:Content>
