<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="現金日記帳.aspx.cs" Inherits="WebApplication2.現金日記帳" %>

<%@ Register Src="~/UC新增日記帳.ascx" TagPrefix="uc1" TagName="UC新增日記帳" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    

    <style>
        #content{
            margin-bottom:10px;
            margin-left:300px;
            width:1100px;
        }
        #insert{
            margin-bottom:20px;
        }
        #confirm{
            margin-left:500px;
        }
        #deleteCheck{
            margin:auto;
        }
       #plusSub{
           margin-left:3px;
       }
       #addSub{
           margin-left:550px;
       }
       #confirmSub{
            margin-left:550px;
       }
      .jumbotron{
          padding-top:10px;
          padding-bottom:10px;
          margin-bottom:5px;
      }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="jumbotron"><h1 class="display-4">現金日記帳</h1></div>
    <uc1:UC新增日記帳 runat="server" id="UC新增日記帳" /> <%--使用者控制項--%>
    <div id="content" class="jumbotron">
        <div id="insert"><button type="button" data-target="#myModal" data-toggle="modal" id="btn" class="btn btn-primary">新增資料</button></div>
        <table id="myDataTalbe"  class="table table-striped table-hover">
            <thead>
                <tr>
                    <th>No</th>
                    <th>日期</th>
                    <th>科目</th>
                    <th>摘要</th>
                    <th>金額</th>
                    <th>收/支</th>
                    <th>餘額</th>
                    <th>刪除/修改</th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="showMoneyList" runat="server" OnItemCommand="Repeater1_ItemCommand">
                    <ItemTemplate>
                        <tr>
                            <td><%#Eval("ID") %></td>
                            <td><%#Eval("Date") %></td>
                            <td><%#Eval("Sub") %></td>
                            <td><%#Eval("Usefor") %></td>
                            <td><%#Eval("Money") %></td>
                            <td><%#Eval("IncomeAndExpenditure") %></td>
                            <td><%#Eval("Total") %></td>
                            <td>
                                <asp:Button ID="Button1" runat="server" Text="修改" commandName="EditItem" CommandArgument='<%# Eval("ID") %>' class="btn btn-warning"/>               
                                <asp:Button　data-target="#myModal" data-toggle="modal" ID="Button2" runat="server" Text="刪除" class="btn btn-danger" commandName="deleteItem" CommandArgument='<%# Eval("ID") %>' OnClientClick="return confirm('是否刪除項目？')"/>
                                
                            </td>
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
        </table>


        
        
        



    </div>
    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
   
     
    <script type="text/javascript">
        $(function () {
            $("#myDataTalbe").DataTable({
  
                "order": [[0, "desc"]]
            });
        });

        
        document.getElementById("btn").onclick = function () {
            $("#myModel").modal('show');


        }
            

        $("#deleteCheck").modal('show');

        

        
        document.getElementById("confirmBtn").onclick = function () {
            $("#confirm").modal('show');
        }
        $(function () {
            $('#Text1').datepicker();
        });

       
        
    </script>
</asp:Content>
