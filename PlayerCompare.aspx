<%@ Page MasterPageFile="~/Master/Main.master" Language="C#" AutoEventWireup="true" CodeFile="PlayerCompare.aspx.cs" Inherits="PlayerCompare" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Player Compare - An Avalanche Of Stats</title>
    <script src="/js/PlayerCompare.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainBody" runat="server">
    <fieldset>
        <legend>Filters</legend>
        <form id="Form1" runat="server" action="Players.aspx">
            <ul>
                <li>
                    <label>Players</label>
                    <asp:DropDownList ID="player1" AppendDataBoundItems="true" runat="server">
                        <asp:ListItem Text="(select player)" Value="" />
                    </asp:DropDownList> 
                    and
                    <asp:DropDownList ID="player2" runat="server" AppendDataBoundItems="true" runat="server">
                        <asp:ListItem Text="(select player)" Value="" />
                    </asp:DropDownList>
                </li>
                <li>
                    <label>Stat</label>
                    <select name="stat" id="stat" runat="server">
                        <option value="G">Goals</option>
                        <option value="A">Assists</option>
                        <option value="Points">Points</option>
                        <option value="PIM">PIM</option>
                        <option value="GW">GW Goals</option>
                        <option value="PP">PP Goals</option>
                        <option value=SH>SH Goals</option>
                    </select>
                </li>
                <li>
                    <label>Show Trend Lines</label>
                    <input type="checkbox" id="showTrends" <%
                                                               if(Request["showTrends"] == "true"){%>checked<%
                                                               }
%> />
                </li>
                <li>
                    <button id="showChart">Compare</button>&nbsp;&nbsp;
                    <a href="#" id="createLink">Link to this comparison</a>&nbsp;&nbsp;&nbsp;&nbsp;
                    <span id="linkSpan"></span>
                </li>
            </ul>
        </form>
    </fieldset>
    <span id="compareChart"></span>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="explainingColumn" runat="server">
    <b>About:</b>
        <ul>
            <li>
                Select two players to compare their stats through the same number of games played with the Avalanche.
            </li>
            <li>
                Show Trend Lines: Will calculate and display the average pace the players earn the stat.
            </li>
        </ul>
</asp:Content>