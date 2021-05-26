<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="ReceiptDetail.aspx.cs" Inherits="WebApplication2.ReceiptDetail" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        #content{
            margin-bottom:10px;
            margin-left:300px;
            width:1100px;
        }
        .jumbotron{
          padding-top:10px;
          padding-bottom:10px;
          margin-bottom:5px;
        }
        th{
            font-size:18px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
<div class="jumbotron"><h1 id="h1Title" runat="server" class="display-4" ></h1></div>
<div id="content" class="jumbotron">
    <div class="container d-flex align-items-center justify-content-center">
        <table class="table" style="width:570px">
            <tr>
                <td colspan="2"><asp:Label runat="server" ID="lblMsg" ForeColor="Red"></asp:Label></td>
            </tr>
            <tr>
                <th>發票號碼：</th>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="txtReceiptNumber" runat="server" MaxLength="11" AutoPostBack="true" OnTextChanged="txtReceiptNumber_TextChanged"></asp:TextBox>
                            <asp:Literal runat="server">(必填)</asp:Literal>
                            <asp:Label ID="lbReceiptNumber" runat="server" Text="" ForeColor="Red"></asp:Label>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <th>日期：</th>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <asp:Label ID="lbDate" runat="server" Text="請選擇日期" Font-Bold="True" Font-Size="Large"></asp:Label>
                            <asp:Literal runat="server">(必填)</asp:Literal>
                            <br />
                            <hr />
                            <asp:Calendar ID="cldrDate" runat="server" ShowGridLines="True" OnSelectionChanged="cldrDate_SelectionChanged" CellPadding="1" Width="400px" DayNameFormat="Shortest"></asp:Calendar>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <th>開立公司：</th>
                <td>
                    <asp:DropDownList ID="dplCompany" runat="server"></asp:DropDownList>
                    <asp:Literal runat="server">(必填)</asp:Literal>
                </td>
            </tr>
            <tr>
                <th>金額：</th>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="txtAmount" runat="server" TextMode="Number" min="1" max="99999999" AutoPostBack="true" OnTextChanged="txtAmount_TextChanged"></asp:TextBox>
                            <asp:Literal runat="server">(必填)</asp:Literal>
                            <asp:Label ID="lbAmount" runat="server" Text="" ForeColor="Red"></asp:Label>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <th>進/銷項：</th>
                <td>
                    <asp:DropDownList ID="dplRE" runat="server">
                        <asp:ListItem Text="銷項" Value="1"></asp:ListItem>
                        <asp:ListItem Text="進項" Value="2"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:Literal runat="server">(必填)</asp:Literal>
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


