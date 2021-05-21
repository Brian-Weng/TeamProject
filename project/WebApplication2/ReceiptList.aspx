<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="ReceiptList.aspx.cs" Inherits="WebApplication2.ReceiptList" %>
<%@ Import Namespace="WebApplication2.Models" %>
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
    <div class="jumbotron"><h1 class="display-4">發票總覽</h1></div>
    <div id="content" class="jumbotron">
        <div id="insert" class="d-flex">
            <div class="mr-auto">
                <a href="ReceiptDetail.aspx"><button type="button" data-target="#myModal" data-toggle="modal" id="btn" class="btn btn-primary">新增發票</button></a>
            </div>
            <div>
                開立公司:
                <asp:DropDownList runat="server" ID="ddlCompany">
                    <asp:ListItem Text="全部" Value=""></asp:ListItem>
                    <asp:ListItem Text="UBay" Value="1"></asp:ListItem>
                    <asp:ListItem Text="7-Eleven" Value="2"></asp:ListItem>
                    <asp:ListItem Text="FamilyMart" Value="3"></asp:ListItem>
                    <asp:ListItem Text="GlobalGas" Value="4"></asp:ListItem>
                </asp:DropDownList>
                金額:
                <asp:TextBox runat="server" ID="txtMinAmount" placeHolder="min" TextMode="Number" Width="120"></asp:TextBox>&nbsp~
                <asp:TextBox runat="server" ID="txtMaxAmount" placeHolder="max" TextMode="Number" Width="120"></asp:TextBox>
                進/銷項:
                 <asp:DropDownList runat="server" ID="ddlR_E">
                    <asp:ListItem Text="全部" Value=""></asp:ListItem>
                    <asp:ListItem Text="進項" Value="1"></asp:ListItem>
                    <asp:ListItem Text="銷項" Value="2"></asp:ListItem>
                </asp:DropDownList>
                <asp:Button runat="server" ID="btnSearch" Text="搜尋" OnClick="btnSearch_Click"/>
            </div>
        </div>
        <table id="InvoiceTable"  class="table table-striped table-hover">
                <tr class="table-primary">
                    <th>發票號碼</th>
                    <th>日期</th>
                    <th>開立公司</th>
                    <th>金額</th>
                    <th>進/銷項</th>
                    <th>刪除/修改</th>
                </tr>
            <asp:Repeater ID="repInvoice" runat="server" OnItemCommand="repInvoice_ItemCommand">
                <ItemTemplate>
                    <tr>
                        <td><%# Eval("ReceiptNumber") %></td>
                        <td><%# Eval("Date", "{0:yyyy-MM-dd}") %></td>
                        <td><%# Eval("Company") %></td>
                        <td><%# Eval("Amount") %></td>
                        <td><%# (Revenue_Expense)Eval("Revenue_Expense") %></td>
                        <td>
                            <asp:Button ID="btnDelete" runat="server" Text="刪除" CommandName="DeleteItem" CommandArgument='<%# Eval("ReceiptNumber") %>' OnClientClick='<%# Eval("ReceiptNumber", "return confirm(\"確定要刪除發票{0}?\");") %>'/>
                            <asp:Button ID="btnUpdate" runat="server" Text="修改" CommandName="UpdateItem" CommandArgument='<%# Eval("ReceiptNumber") %>'/>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>
        <div class="d-flex justify-content-between">
            <div>
                <nav aria-label="Page navigation example">
                    <ul class="pagination">
                        <li class="page-item"><a class="page-link" runat="server" id="firstPage" href="#" title="前往第1頁">First</a></li>
                        <asp:Repeater runat="server" ID="repPaging">
                            <ItemTemplate>
                                <li class="page-item"><a class="page-link" href="<%# Eval("Link") %>" title="<%# Eval("Title") %>"><%# Eval("Name") %></a></li>
                            </ItemTemplate>
                        </asp:Repeater>
                        <li class="page-item"><a class="page-link" runat="server" id="lastPage" href="#" title="前往第1頁">Last</a></li>
                    </ul>
                </nav>
            </div>
            <div>
                <asp:Label runat="server" ID="lblCurrentPage"></asp:Label> /
                <asp:Label runat="server" ID="lblTotalPage"></asp:Label>頁
            </div>
        </div>
    </div>
</asp:Content>

