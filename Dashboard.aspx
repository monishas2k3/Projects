<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Dashboard.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    <div>
    
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    
        Railway Booking
    
        System<br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Dashboard<br />
        <br />
        <br />
        <br />
    
    </div>
    
    </div>
        <asp:GridView ID="GridView1" runat="server" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" Height="153px" Width="777px" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" BackColor="White" BorderColor="White" BorderStyle="Ridge" BorderWidth="2px" CellPadding="3" CellSpacing="1" GridLines="None" ClientIDMode="Predictable">
            <Columns>
                <asp:BoundField DataField="trainCode" HeaderText="trainCode" SortExpression="trainCode" />
                <asp:BoundField DataField="trainName" HeaderText="trainName" SortExpression="trainName" />
                <asp:BoundField DataField="seatCount" HeaderText="seatCount" SortExpression="seatCount" />
                <asp:BoundField DataField="source" HeaderText="source" SortExpression="source" />
                <asp:BoundField DataField="destination" HeaderText="destination" SortExpression="destination" />
                <asp:BoundField DataField="ticketPrice" HeaderText="ticketPrice" SortExpression="ticketPrice" />

                <%-- Need to add this asp:TemplateField part to show action buttons in gridview added by Pari at 24.12.2024--%>

                <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="editBtn" OnClick="editBtn_Click" Text="Edit" ForeColor="Blue" runat="server" CommandArgument='<%# Eval("trainCode") %>' />
                                <asp:LinkButton ID="deleteBtn" OnClick="deleteBtn_Click" OnClientClick="return confirm('Are you sure want to delete?');" Text="Delete" ForeColor="Red" runat="server" CommandArgument='<%# Eval("trainCode") %>' />
                                <asp:LinkButton ID="reserveNowBtn" OnClick="reserveNowBtn_Click" Text="Reserve Now" ForeColor="Green" runat="server" CommandArgument='<%# Eval("trainCode") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
            </Columns>
            <FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
            <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#E7E7FF" />
            <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
            <RowStyle BackColor="#DEDFDE" ForeColor="Black" />
            <SelectedRowStyle BackColor="#9471DE" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F1F1F1" />
            <SortedAscendingHeaderStyle BackColor="#594B9C" />
            <SortedDescendingCellStyle BackColor="#CAC9C9" />
            <SortedDescendingHeaderStyle BackColor="#33276A" />
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT [trainCode], [trainName], [seatCount], [source], [destination], [ticketPrice] FROM [TrainTable]"></asp:SqlDataSource>
        <p>
            &nbsp;</p>
        <p>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="addtrainBtn" runat="server" Text="Add Train" OnClick="addtrainBtn_Click" />
&nbsp;&nbsp;
            <asp:Button ID="allreservationsBtn" runat="server" Text="All Reservations" OnClick="allreservationsBtn_Click" />
&nbsp;&nbsp;
            <asp:Button ID="reportBtn" runat="server" Text="Report" OnClick="reportBtn_Click" />
&nbsp;&nbsp;
            <asp:Button ID="logoutBtn" runat="server" Text="Logout" OnClick="logoutBtn_Click" />
        </p>
    </form>
</body>
</html>
