<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WebApplication2.WebForm3" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css" integrity="sha384-Gn5384xqQ1aoWXA+058RXPxPg6fy4IWvTNh0E263XmFcJlSAwiGgFAW/dAiS6JXm"
        crossorigin="anonymous">
    <style>
        body{
            background-image:url("photo/photo.png");
        }

        #center{
            
            background-color:lightgrey;
            height:300px;
            width:300px;
            padding:50px;
            margin:auto;
            margin-top:300px !important;
            
        }
        h1{
            margin-top:150px;
            margin-bottom:-200px;
        }
        #Label1{
            color:red;
        }
        
    </style>
</head>
<body>
    <form id="form1" runat="server" method="post">
        <div>
           <center> <h1 class="display-4">財務管理系統登入</h1></center>
        </div>
        <div id="center">
            
        帳號:<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
            <br/>
        密碼:<asp:TextBox ID="TextBox2" runat="server" TextMode="password"></asp:TextBox>
            <asp:Button ID="Button1" runat="server" Text="登入" OnClick="Button1_Click" />
            <br />
            <br />
            <asp:Label ID="Label1" runat="server" Text="帳號或密碼錯誤" Visible="False"></asp:Label>
       </div>
        
    </form>
    
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.12.9/umd/popper.min.js" integrity="sha384-ApNbgh9B+Y1QKtv3Rn7W3mgPxhU9K/ScQsAP7hUibX39j7fakFPskvXusvfa0b4Q"
        crossorigin="anonymous"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/js/bootstrap.min.js" integrity="sha384-JZR6Spejh4U02d8jOt6vLEHfe/JQGiRRSQQxSfFWpi1MquVdAyjUar5+76PVCmYl"
        crossorigin="anonymous"></script>
</body>
</html>
