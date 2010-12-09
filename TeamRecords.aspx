<%@ Page MasterPageFile="~/Master/Main.master" Language="C#" AutoEventWireup="true" CodeFile="TeamRecords.aspx.cs" Inherits="TeamRecords" %>
<%@ Import Namespace="System.Data.SqlClient"%>


<asp:Content ContentPlaceHolderID="head" runat="server">
    <title>Team Records</title>
</asp:Content>
<asp:Content ContentPlaceHolderID="mainBody" runat="server">
    <%
        SqlDataReader records = GetsRecords();
        String description = String.Empty;
    %>
    <ul>
        <%while(records.Read()){
            if(description != Convert.ToString(records["description"]))
            {
                if(!String.IsNullOrEmpty(description))
                    %></ul><%
                description = Convert.ToString(records["description"]);
                %>
                <li class="bold"><%=Convert.ToString(records["description"]) %></li>
                    <ul>
                <%
            }
            %>
            <li class="showBullet"><%=Convert.ToString(records["maxStat"]) %> - <%=Convert.ToDateTime(records["date"]).ToShortDateString()%></li>
            <%      
        }%>
        </ul>
    </ul>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="explainingColumn" runat="server">
    <ul>
        <li>
            Regular season individual game records.
        </li>
    </ul>
</asp:Content>