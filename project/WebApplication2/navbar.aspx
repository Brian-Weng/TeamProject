<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="navbar.aspx.cs" Inherits="WebApplication2.navbar" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css" integrity="sha384-Gn5384xqQ1aoWXA+058RXPxPg6fy4IWvTNh0E263XmFcJlSAwiGgFAW/dAiS6JXm"
        crossorigin="anonymous">
    <title></title>
    <style>
       .wrapper{
            display: flex;
            width:100%;
        }

        #sidebar{
             width: 250px;
    position: fixed;
    top: 0;
    left: 0;
    height: 100vh;
    z-index: 999;
    background: rgb(184,218,255);
    color: white;
        }
.active {
    background-color: rgb(132,194,255) !important;
}
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="wrapper">

            <nav id="sidebar">

                <div class="sidebar-header">

                    <h3>財務管理系統</h3>
                </div>

                <ul class="list-unstyled components">

                    <p>功能表</p>
                    
                    <li class="list-group-item list-group-item-action list-group-item-primary">
                        <a href="#">現金日記帳</a>
                    </li>

                    <li class="list-group-item list-group-item-action list-group-item-primary">
                        <a href="#">現金分析表</a>
                    </li>

                    <li class="list-group-item list-group-item-action list-group-item-primary">
                        <a href="#">零用金管理</a>
                    </li>

                    <li class="list-group-item list-group-item-action list-group-item-primary">
                        <a href="#">稅務資料</a>
                    </li>

                    <li class="nav-item dropdown">
                        <a class="dropdown-toggle" href="#" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">稅務資料</a>
                        <div class="dropdown-menu">
                            <a class="dropdown-item" href="#">銷項稅額</a>
                            <a class="dropdown-item" href="#">進項稅額</a>
                        </div>
                    </li>

                </ul>
            </nav>
        </div>
    </form>


    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.12.9/umd/popper.min.js" integrity="sha384-ApNbgh9B+Y1QKtv3Rn7W3mgPxhU9K/ScQsAP7hUibX39j7fakFPskvXusvfa0b4Q"
        crossorigin="anonymous"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/js/bootstrap.min.js" integrity="sha384-JZR6Spejh4U02d8jOt6vLEHfe/JQGiRRSQQxSfFWpi1MquVdAyjUar5+76PVCmYl"
        crossorigin="anonymous"></script>

</body>
</html>
