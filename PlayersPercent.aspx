<%@ Page MasterPageFile="~/Master/Main.master" Language="C#" AutoEventWireup="true" CodeFile="PlayersPercent.aspx.cs" Inherits="PlayersPercent" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Percentages of Season by Player</title>
    <script src="js/PlayersPercent.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainBody" runat="server">
    <fieldset>
        <legend>Filters</legend>
        <form id="Form1" runat="server" action="PlayersPercent.aspx">
            <ul>
                <li>
                    <label id="Label1" runat="server">Games Played</label>
                    <input type="text" name="GP-low" value="<%=Request["GP-low"] %>" /> to <input type="text" name="GP-high" value="<%=Request["GP-high"] %>"  />        
                </li>
                <li>
                    <label>Seasons</label>
                    <asp:DropDownList ID="seasonlow" AppendDataBoundItems="true" runat="server">
                        <asp:ListItem Text="(dawn of time)" Value="" />
                    </asp:DropDownList> 
                    to 
                    <asp:DropDownList ID="seasonhigh" runat="server" AppendDataBoundItems="true" runat="server">
                        <asp:ListItem Text="(infinity and beyond)" Value="" />
                    </asp:DropDownList>
                </li>
                <li>
                    <label>Country</label>
                    <asp:DropDownList ID="Country" runat="server" AppendDataBoundItems="true">
                        <asp:ListItem Text="(all)" Value="" />
                    </asp:DropDownList>
                </li>
                <li>
                    <label id="Label2" runat="server">Age</label>
                    <input type="text" name="age-low" value="<%=Request["age-low"] %>" /> to <input type="text" name="age-high" value="<%=Request["age-high"] %>"  />        
                </li>
                <li>
                    <label>Position</label>
                    <select name="position">
                        <option value="">(all)</option>
                        <option <%if (Request["position"] == "FORWARD") Response.Write("selected"); %> value="FORWARD">Forwards</option>
                        <option <%if (Request["position"] == "2") Response.Write("selected"); %> value="2">Defence</option>
                        <option <%if (Request["position"] == "5") Response.Write("selected"); %> value="5">Center</option>
                        <option <%if (Request["position"] == "3") Response.Write("selected"); %> value="3">Left Wing</option>
                        <option <%if (Request["position"] == "4") Response.Write("selected"); %> value="4">Right Wing</option>
                    </select>
                </li>
                <li>
                    <input type="submit" value="Filter" />
                </li>
            </ul>
        </form>
    </fieldset>
    <br />
    <div>
        <asp:DataGrid ID="dgPlayerSeasonsPercent" runat="server" CssClass="dataGrid" CellPadding="2">
        </asp:DataGrid>
    </div>
    <span style="padding:3px;background-color:#DDDDDD">Player is currently with the organization</span>
</asp:Content>
<asp:Content ContentPlaceHolderID="explainingColumn" runat="server">
            <b>About Percentages:</b>
            <ul>
                <li>
                    All percentages are based of the entire season.  So if a player only plays half of the season, his %'s are that of all 82 games the team played.
                </li>
            </ul>
            <b>Columns:</b>
            <ul>
                <li>
                    Age:  Players age as of October 1st of the year the season started.
                </li>
                <li>
                    % of Team Goals:  The players goal total for the season divided by the total goals scored by the team that season.  All "% of Teams" columns are calculated this way.
                </li>
            </ul>
            <b>Filters:</b>
            <ul>
                <li>
                    Games Played:  Total number of games played by player in the season.  This is inclusive, so if you enter 20 as the minimum number, it will show players who played at least 20 or more games in a season.
                </li>
                <li>
                    Seasons:  Also an inclusive filter.
                </li>
                <li>
                    Country:  Country the player was born in.
                </li>
                <li>
                    Age:  Age of the player.
                </li>
                <li>
                    Position:  Position the player... plays (duh).
                </li>
            </ul>
</asp:Content>