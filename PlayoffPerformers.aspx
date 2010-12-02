<%@ Page  MasterPageFile="~/Master/Main.master" Language="C#" AutoEventWireup="true" CodeFile="PlayoffPerformers.aspx.cs" Inherits="PlayoffPerformers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Playoff Performers</title>
    <script src="js/PlayoffPerformers.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainBody" runat="server">
    <fieldset>
        <legend>Filters</legend>
        <form id="Form1" runat="server" action="PlayoffPerformers.aspx">
            <ul>
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
                    <label id="Label2" runat="server">Age</label>
                    <input type="text" name="age-low" value="<%=Request["age-low"] %>" /> to <input type="text" name="age-high" value="<%=Request["age-high"] %>"  />        
                </li>
                <li>
                    <label>Position</label>
                    <select name="position">
                        <option value="">(all)</option>
                        <option <%if (Request["position"] == "SKATERS") Response.Write("selected"); %> value="SKATERS">-All Skaters</option>
                        <option <%if (Request["position"] == "FORWARD") Response.Write("selected"); %> value="FORWARD">-Forwards</option>
                        <option <%if (Request["position"] == "1") Response.Write("selected"); %> value="1">Goalie</option>
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
        <asp:DataGrid ID="dgPlayerSeasons" runat="server" CssClass="dataGrid" CellPadding="2">
        </asp:DataGrid>
    </div>
    <span style="padding:3px;background-color:#DDDDDD">Player is currently with the organization</span>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="explainingColumn" runat="server">
        <b>Columns:</b>
        <ul>
            <li>
                Age:  Players age as of October 1st of the year the season started.
            </li>
            <li>
                Goals per Game Difference:  The players regular season goals per game subtracted from their playoff goals per game.  All "per Game Difference" columns are calculated this way.  A negative number will represent a lower number in the playoffs than in the regular season.
            </li>
            <li>
                Goals per Game %: The players playoff goals per game divided by their regular season goals per game.  All "per Game %" columns are calculated this way.  I number higher than 100 will represent a better performance in the playoffs than the regular season.
            </li>
        </ul>
    
        <b>Filters:</b>
        <ul>
            <li>
                Seasons:  Also an inclusive filter.  If Player Totals is selected, it will sum all stats that fall within this filter.
            </li>
            <li>
                Age:  Age of the player. If Player Totals is selected, it will sum all stats from seasons that fall within this filter.
            </li>
            <li>
                Position:  Position the player... plays (duh).
            </li>
        </ul>
</asp:Content>