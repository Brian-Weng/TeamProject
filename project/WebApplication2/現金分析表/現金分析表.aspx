<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="現金分析表.aspx.cs" Inherits="WebApplication2.現金分析表.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
    <style>
        #content{
            margin-left:300px;
            width:75%;
        }
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="jumbotron"><h1 class="display-4"><asp:Label ID="title" runat="server" Text="Label"></asp:Label>年現金分析表</h1></div>
    

<div id="content">
    <asp:DropDownList ID="yearSelect" runat="server"  OnSelectedIndexChanged="yearSelect_SelectedIndexChanged" AutoPostBack="True">
        <asp:ListItem Value="2021">2021</asp:ListItem>
        <asp:ListItem Value="2022">2022</asp:ListItem>
        <asp:ListItem Value="2023">2023</asp:ListItem>
        <asp:ListItem Value="2024">2024</asp:ListItem>
    </asp:DropDownList><span>年</span>
    <br />
    <button type="button" class="btn btn-success" onclick="fnExcelReport()">匯出EXCEL</button>
    <table class="table table-bordered table-hover table-striped" id="MoneyList">
        <thead>
            <tr>
                <th>項目</th>
                <th>1月</th>
                <th>2月</th>
                <th>3月</th>
                <th>4月</th>
                <th>5月</th>
                <th>6月</th>
                <th>7月</th>
                <th>8月</th>
                <th>9月</th>
                <th>10月</th>
                <th>11月</th>
                <th>12月</th>
            </tr>
        </thead>
        <tbody>
            <tr>
                <td>月收入</td>
                <asp:Repeater ID="monthIncome" runat="server">
                    <ItemTemplate>
                        <td><%# Eval("monthInOut")  %></td>
                    </ItemTemplate>
                </asp:Repeater>
            </tr>
            <tr>
                <td>月支出</td>
                <asp:Repeater ID="monthExpenditure" runat="server">
                    <ItemTemplate>
                        <td><%# Eval("monthInOut")  %></td>
                    </ItemTemplate>
                </asp:Repeater>
            </tr>
            <tr>
                <td>月餘額</td>
                <asp:Repeater ID="monthBalance" runat="server">
                    <ItemTemplate>
                        <td><%# Eval("balance")  %></td>
                    </ItemTemplate>
                </asp:Repeater>
            </tr>

            <tr>
                <td colspan="13" align="center">收入</td>
            </tr>

            <asp:Repeater ID="MonthSubIncome" runat="server">
                <ItemTemplate>
                    <tr>
                        <td><%# Eval("subType")  %></td>
                        <td><%# Eval("Jan")  %></td>
                        <td><%# Eval("Feb")  %></td>
                        <td><%# Eval("Mar")  %></td>
                        <td><%# Eval("Apr")  %></td>
                        <td><%# Eval("May")  %></td>
                        <td><%# Eval("Jun")  %></td>
                        <td><%# Eval("Jul")  %></td>
                        <td><%# Eval("Aug")  %></td>
                        <td><%# Eval("Sep")  %></td>
                        <td><%# Eval("Oct")  %></td>
                        <td><%# Eval("Nov")  %></td>
                        <td><%# Eval("Dec")  %></td>
                    </tr>


                </ItemTemplate>
            </asp:Repeater>

            <tr>
                <td colspan="13" align="center">支出</td>
            </tr>

            <asp:Repeater ID="MonthSubEx" runat="server">
                <ItemTemplate>
                    <tr>
                        <td><%# Eval("subType")  %></td>
                        <td><%# Eval("Jan")  %></td>
                        <td><%# Eval("Feb")  %></td>
                        <td><%# Eval("Mar")  %></td>
                        <td><%# Eval("Apr")  %></td>
                        <td><%# Eval("May")  %></td>
                        <td><%# Eval("Jun")  %></td>
                        <td><%# Eval("Jul")  %></td>
                        <td><%# Eval("Aug")  %></td>
                        <td><%# Eval("Sep")  %></td>
                        <td><%# Eval("Oct")  %></td>
                        <td><%# Eval("Nov")  %></td>
                        <td><%# Eval("Dec")  %></td>
                    </tr>


                </ItemTemplate>
            </asp:Repeater>








            


        </tbody>
    </table>
</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
   <script>
       
           function fnExcelReport() {
               var tab_text = "<table border='2px'><tr bgcolor='#87AFC6'>";
               var textRange; var j = 0;
               tab = document.getElementById('MoneyList'); // id of table

               for (j = 0; j < tab.rows.length; j++) {
                   tab_text = tab_text + tab.rows[j].innerHTML + "</tr>";
                   //tab_text=tab_text+"</tr>";
               }

               tab_text = tab_text + "</table>";
               tab_text = tab_text.replace(/<A[^>]*>|<\/A>/g, "");//remove if u want links in your table
               tab_text = tab_text.replace(/<img[^>]*>/gi, ""); // remove if u want images in your table
               tab_text = tab_text.replace(/<input[^>]*>|<\/input>/gi, ""); // reomves input params

               var ua = window.navigator.userAgent; //檢測瀏覽器
               var msie = ua.indexOf("MSIE ");

               if (msie > 0 || !!navigator.userAgent.match(/Trident.*rv\:11\./))      // If Internet Explorer
               {
                   txtArea1.document.open("txt/html", "replace");
                   txtArea1.document.write(tab_text);
                   txtArea1.document.close();
                   txtArea1.focus();
                   sa = txtArea1.document.execCommand("SaveAs", true, "Say Thanks to Sumit.xls");
               }
               else                 //other browser not tested on IE 11
                   sa = window.open('data:application/vnd.ms-excel,' + encodeURIComponent(tab_text));

               return (sa);
           } 
       
   </script>
</asp:Content>
