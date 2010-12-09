﻿<%@ Page  MasterPageFile="~/Master/Main.master" Language="C#" AutoEventWireup="true" CodeFile="Player.aspx.cs" Inherits="Player" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title><%=PlayerName()%> - An Avalanche Of Stats</title>
    <script src="js/Player.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainBody" runat="server">
    <input type="hidden" id="playerId" value="<%=Request["Id"] %>" />
   <ul>
    <li class="playerName">
        <%=PlayerName() %>
    </li>
    <li>
        <label>Born</label>
        <%=DOB() %>
    </li>
    <li>
        <label>Country of Birth</label>
        <%=Country() %>
    </li>
    <li>
        <label>Position</label>
        <%=Position() %>
    </li>
   </ul>
   
   <div>
        Regular Season: <a href="#" id="showSeasonChart">(show chart)</a>
        <asp:DataGrid ID="dgSeasons" runat="server" CssClass="dataGrid" CellPadding="2">
        </asp:DataGrid>
    </div>
    <span id="seasonChart"></span>
    <br />
    <div>
        Playoffs: <a href="#" id="showPlayoffChart">(show chart)</a>
        <asp:DataGrid ID="dgPlayoffs" runat="server" CssClass="dataGrid" CellPadding="2">
        </asp:DataGrid>
    </div>
    <span id="playoffChart"></span>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="explainingColumn" runat="server">
    <b>Columns:</b>
        <ul>
            <li>
                Age:  Players age as of October 1st of the year the season started.
            </li>
        </ul>
</asp:Content>