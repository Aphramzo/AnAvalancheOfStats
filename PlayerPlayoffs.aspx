<%@ Page  MasterPageFile="~/Master/Main.master" Language="C#" AutoEventWireup="true" CodeFile="PlayerPlayoffs.aspx.cs" Inherits="PlayerPlayoffs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Player Playoff Stats</title>
    <script src="js/Players.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainBody" runat="server">
    <fieldset>
        <legend>Filters</legend>
        <form id="Form1" runat="server" action="PlayerPlayoffs.aspx">
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
                    <label>Show Per Game Stats</label>
                    <input type="checkbox" id="perGame" name="perGame" <%if (Request["perGame"] == "on"){Response.Write("checked");}%> />         
                </li>
                <li>
                    <label>Show Player Totals</label>
                    <input type="checkbox" id="sum" name="sum" <%if (Request["sum"] == "on"){Response.Write("checked");}%> />         
                </li>
                <li>
                    <input type="submit" value="Search" />
                </li>
            </ul>
        </form>
    </fieldset>
    <br />
    <div>
        <asp:DataGrid ID="dgPlayerSeasons" runat="server" CssClass="dataGrid" CellPadding="2">
        </asp:DataGrid>
    </div>
    <%if(IsPostBack) 
        Response.Write("<span style='padding:3px;background-color:#DDDDDD'>Player is currently with the organization</span>");
    %>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="explainingColumn" runat="server">
        <b>Columns:</b>
        <ul>
            <li>
                Age:  Players age as of October 1st of the year the season started.
            </li>
            <li>
                Goals per Game:  The players goal total for the playoffs divided by the number of games the player played in.  All "per Game" columns are calculated this way.  If Player Totals is selected, it is the total goals they scored in playoff games divided by the total number of games they played in.
            </li>
        </ul>
    
        <b>Filters:</b>
        <ul>
            <li>
                Games Played:  Total number of games played by player in the post season.  This is inclusive, so if you enter 20 as the minimum number, it will show players who played at least 20 or more games in a post season.  If Player Totals is selected, this will filter based on the total number of games they played.
            </li>
            <li>
                Seasons:  Also an inclusive filter.  If Player Totals is selected, it will sum all stats that fall within this filter.
            </li>
            <li>
                Country:  Country the player was born in.
            </li>
            <li>
                Age:  Age of the player. If Player Totals is selected, it will sum all stats from post seasons that fall within this filter.
            </li>
            <li>
                Position:  Position the player... plays (duh).
            </li>
            <li>
                Show per Game Stats:  Instead of showing total stats, it will display per game number (see above).
            </li>
            <li>
                Player Totals:  Show all time stats.
            </li>
        </ul>
</asp:Content>