<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="發票總攬.aspx.cs" Inherits="WebApplication2.發票總攬" %>

<%--<%@ Register Src="~/UCReceiptList.ascx" TagPrefix="uc1" TagName="UCReceiptList" %>--%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        #content{
            width:1100px;
            margin-left:300px;
            margin-top:85px;
        }

    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="content">
    <table id="receiptList">
       <%-- <uc1:UCReceiptList runat="server" id="UCReceiptList" />--%>

        <thead>
            <tr>
                <th>No</th>
                <th>發票號碼</th>
                <th>日期</th>
                <th>價錢</th>
                <th>稅額種類</th>
                <th>稅額</th>
            </tr>
        </thead>


    </table>
</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
  
<script>

    

    //$(document).ready( function () {
    //    $('#receiptList').DataTable({
    //        "order": [[0, "desc"]]
    //    });
    //});

    $(document).ready(function () {
        $('#receiptList').DataTable({
            "ajax": {
                "url": "API/Handler1.ashx",
                "type": "GET",
                "datatype" : "json"
            },
            "colums": [
                { "data": "No" },
                { "data": "發票號碼" },
                { "data": "日期" },
                { "data": "價錢" },
                { "data": "稅額種類" },
                { "data": "稅額" },
            ]
        });
    });

</script>
</asp:Content>
