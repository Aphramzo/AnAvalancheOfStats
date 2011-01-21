<%@ Page MasterPageFile="~/Master/Main.master" Language="C#" AutoEventWireup="true" CodeFile="PlayerCompare.aspx.cs" Inherits="PlayerCompare" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Player Compare - An Avalanche Of Stats</title>
    <script src="/AnAvalancheOFStats/js/PlayerCompare.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainBody" runat="server">
    <fieldset>
        <legend>Filters</legend>
        <form id="Form1" runat="server" action="Players.aspx">
            <ul>
                <li>
                    <label>Players</label>
                    <asp:DropDownList ID="player1" AppendDataBoundItems="true" runat="server">
                    </asp:DropDownList> 
                    and
                    <asp:DropDownList ID="player2" runat="server" AppendDataBoundItems="true" runat="server">
                    </asp:DropDownList>
                </li>
                <li>
                    <label>Stat</label>
                    <select name="stat" id="stat">
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
                    <button id="showChart">Compare</button>
                </li>
            </ul>
        </form>
    </fieldset>
    <span id="compareChart"></span>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="explainingColumn" runat="server">
    <b>Columns:</b>
        <ul>
            <li>
                Age:  Players age as of October 1st of the year the season started.
            </li>
        </ul>
</asp:Content>