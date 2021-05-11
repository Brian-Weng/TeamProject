<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="修改項目.aspx.cs" Inherits="WebApplication2.WebForm4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        
        #content{
            padding-top:10px;
            padding-bottom:10px;
            margin-left:450px;
            margin-top:150px;
            background-color:#BDC0BA;
            border:1px solid ;
            border-radius:10px;
            width:500px;
            text-align:center;
        }
        #OutIn{
            margin-left:-135px;
        }
        p{
            margin-top:10px;
        }
        #pDate{
            margin-left:-30px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="content">
        <p id="pDate">日期: <asp:TextBox ID="date" runat="server" type="date"></asp:TextBox></p>   <br />
       <p> 科目: <asp:TextBox ID="sub" runat="server"></asp:TextBox> </p>   <br />
       <p> 摘要: <asp:TextBox ID="usefor" runat="server"></asp:TextBox></p> <br />
        <p>金額: <asp:TextBox ID="money" runat="server"></asp:TextBox></p>  <br />
        <p id="OutIn">收/支: <asp:DropDownList ID="DropDownList1" runat="server">
                <asp:ListItem>收入</asp:ListItem>
                <asp:ListItem>支出</asp:ListItem>
             </asp:DropDownList>  </p><br />
        <button id="btn" data-target="#editCheck" data-toggle="modal" type="button">確認</button>
        <asp:Button ID="Button2" runat="server" Text="取消" OnClick="Button2_Click" />



        <div class="modal fade" id ="editCheck" tabindex="-1" role="dialog">
            <div class="modal-dialog modal-sm">
                <div class="modal-content">
                    <div class="modal-header">
                        <div class="modal-title"><h6>修改確認</h6></div>
                        <button type="button" class="close" data-dismiss="modal"><span>&times;</span></button>
                    </div>
                    <div class="modal-body">
                        是否要修改項目
                    </div>
                    <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">否</button>
                    <asp:Button ID="checkBtn" runat="server" Text="是" class="btn btn-primary" OnClick="checkBtn_Click"/></div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">

    


    <script type="text/javascript">

        document.getElementById("btn").onclick = function () {
            $("#editCheck").modal("show");
        }
    </script>
</asp:Content>
