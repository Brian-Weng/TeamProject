<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="homepage.aspx.cs" Inherits="WebApplication2.WebForm2" %>

<%@ Register Src="~/ucNav.ascx" TagPrefix="uc1" TagName="ucNav" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css" integrity="sha384-Gn5384xqQ1aoWXA+058RXPxPg6fy4IWvTNh0E263XmFcJlSAwiGgFAW/dAiS6JXm"
        crossorigin="anonymous">
    <link href="CSS/CSS.css" rel="stylesheet" />
    <style>
       body {
    background-image: url("photo/歡迎主管.png");
}
    </style>
</head>
<body>
    <form id="form1" runat="server">
        
        <div class="wrapper">

            <nav id="sidebar">

                <uc1:ucNav runat="server" ID="ucNav" />
            </nav>
           <div>
               <asp:Literal ID="Literal1" runat="server"></asp:Literal>
               <h1 class="display-4"> 歡迎進入本系統</h1>
           </div>
        



    </form>

    

<script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.12.9/umd/popper.min.js" integrity="sha384-ApNbgh9B+Y1QKtv3Rn7W3mgPxhU9K/ScQsAP7hUibX39j7fakFPskvXusvfa0b4Q"
        crossorigin="anonymous"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/js/bootstrap.min.js" integrity="sha384-JZR6Spejh4U02d8jOt6vLEHfe/JQGiRRSQQxSfFWpi1MquVdAyjUar5+76PVCmYl"
        crossorigin="anonymous"></script>


    <script type="text/javascript">


       


    </script>

</body>
</html>
