<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCReceiptList.ascx.cs" Inherits="WebApplication2.UCReceiptList" %>

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
        <tbody>
            <asp:Repeater ID="receiptTable" runat="server">
                <ItemTemplate>
                     <tr>
                        <td><%# Eval("ID") %></td>
                        <td><%# Eval("number") %></td>
                        <td><%# Eval("date") %></td>
                        <td><%# Eval("money") %></td>
                        <td><%# Eval("type") %></td>
                        <td><%# Eval("tax") %></td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </tbody>