<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucNav.ascx.cs" Inherits="WebApplication2.ucNav" %>

 <div class="sidebar-header">

            
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

                    <%--<li class="list-group-item list-group-item-action list-group-item-primary">
                        <a href="#">稅務資料</a>
                    </li>--%>


                    <li class="nav-item dropdown list-group-item list-group-item-action list-group-item-primary">
                        <a class="dropdown-toggle" href="#" data-toggle="dropdown" aria-haspopup="false" aria-expanded="false" id ="myMenu">稅務資料</a>
                        <div class="dropdown-menu" aria-labelledby="myMenu">
                            <a class="dropdown-item" href="ReceiptList.aspx">發票總覽</a>
                            <a class="dropdown-item" href="PreSalesTaxList.aspx">銷項稅額</a>
                            <a class="dropdown-item" href="TaxPayableList.aspx">進項稅額</a>
                        </div>
                    </li>

                     <li class="list-group-item list-group-item-action list-group-item-primary">                 
                         <asp:PlaceHolder ID="LoginPlc" runat="server" Visible="false">
                         <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click">登出</asp:LinkButton>
                         </asp:PlaceHolder> 
                         <asp:PlaceHolder ID="LogoutPlc" runat="server" Visible="false">
                             <asp:LinkButton ID="LinkButton2" runat="server" OnClick="LinkButton2_Click">登入</asp:LinkButton>
                         </asp:PlaceHolder>
                    </li>

                </ul>