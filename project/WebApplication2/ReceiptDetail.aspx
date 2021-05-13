<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="ReceiptDetail.aspx.cs" Inherits="WebApplication2.ReceiptDetail" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        th{
            font-size:18px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
<div class="jumbotron"><h1 id="lbltitle" runat="server" class="display-4" ></h1></div>
<div class="jumbotron">
    <div class="container d-flex align-items-center justify-content-center">
    <table class="table" style="width:570px">
        <tr>
            <td colspan="2"><asp:Label runat="server" ID="lblMsg" ForeColor="Red"></asp:Label></td>
        </tr>
        <tr>
            <th>發票號碼：</th>
            <td><asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txtReceiptNumber" runat="server" AutoPostBack="true" OnTextChanged="txtReceiptNumber_TextChanged"></asp:TextBox>
                        <asp:Label ID="lbReceiptNumber" runat="server" Text="" ForeColor="Red"></asp:Label>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <th>日期：</th>
            <td>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:Label ID="lbDate" runat="server" Text="請選擇日期" Font-Bold="True" Font-Size="Large"></asp:Label><br /><hr />
                        <asp:Calendar ID="cldrDate" runat="server" ShowGridLines="True" OnSelectionChanged="cldrDate_SelectionChanged" CellPadding="1" Width="400px" DayNameFormat="Shortest"></asp:Calendar>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <th>開立公司：</th>
            <td>
                <asp:DropDownList ID="dpdCompany" runat="server">
                    <asp:ListItem Text="FamilyMart" Value="FamilyMart"></asp:ListItem>
                    <asp:ListItem Text="7-Eleven" Value="7-Eleven"></asp:ListItem>
                    <asp:ListItem Text="GlobalGas" Value="GlobalGas"></asp:ListItem>
                    <asp:ListItem Text="UBay" Value="UBay"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <th>金額：</th>
            <td>
                <asp:TextBox ID="txtAmount" runat="server" MaxLength="10" TextMode="Number" min="1" max="9999999"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <th>進/銷項</th>
            <td>
                <asp:DropDownList ID="dpdRE" runat="server">
                    <asp:ListItem Text="銷項" Value="1"></asp:ListItem>
                    <asp:ListItem Text="進項" Value="2"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td><asp:Button ID="btnSave" runat="server" Text="存檔" OnClick="btnSave_Click" CssClass="btn btn-success"/></td>
            <td><a class="btn btn-light" href="ReceiptList.aspx">回總覽頁</a></td>
        </tr>
        
    </table>
    
    </div>
</div>

</asp:Content>


