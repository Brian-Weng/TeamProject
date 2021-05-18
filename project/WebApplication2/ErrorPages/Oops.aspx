<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Oops.aspx.cs" Inherits="WebApplication2.ErrorPages.Oops" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        #content{
            margin-bottom:10px;
            margin-left:300px;
            width:1100px;
        }
    </style>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="jumbotron"><h1 class="display-4">發生未預期的錯誤</h1></div>
    <div id="content" class="jumbotron">
        <p>有一個未預期的錯誤發生在網站上，請聯繫開發人員</p>
        <ul>
            <li>
                <a href="~/ReceiptList.aspx">回到發票總覽</a>
            </li>
        </ul>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
