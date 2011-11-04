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
   
   <div id="tabs">
       <ul>
		    <li><a id="aRegularSeason" href="#regularSeason">Regular Season</a></li>
		    <li><a id="aPostSeason" href="#postSeason">Playoffs</a></li>
		    <li><a href="#totals">Totals and Records</a></li>
		    <li><a href="#splits">Splits</a></li>
	    </ul>
       <div id="regularSeason">
            <asp:DataGrid ID="dgSeasons" runat="server" CssClass="dataGrid" CellPadding="2">
            </asp:DataGrid>
            <span id="seasonChart"></span>
        </div>
        <div id="postSeason">
            <asp:DataGrid ID="dgPlayoffs" runat="server" CssClass="dataGrid" CellPadding="2">
            </asp:DataGrid>
            <span id="playoffChart"></span>
        </div>
        <div id="totals">
            Regular Season Totals: 
            <asp:DataGrid ID="dgSeasonTotals" runat="server" CssClass="dataGrid" CellPadding="2">
            </asp:DataGrid>
        </div>
        <div id="splits"> 
            <asp:DataGrid ID="dgSplitsMonth" runat="server" CssClass="dataGrid" CellPadding="2">
            </asp:DataGrid>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="explainingColumn" runat="server">
    <b>Columns:</b>
        <ul>
            <li>
                Age:  Players age as of October 1st of the year the season started.
            </li>
        </ul>
</asp:Content>