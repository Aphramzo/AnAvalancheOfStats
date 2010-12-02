<%@ Page MasterPageFile="~/Master/Main.master" Language="C#" AutoEventWireup="true" CodeFile="Seasons.aspx.cs" Inherits="Seasons" %>

<asp:Content ContentPlaceHolderID="head" runat="server">
    <title>Stats by Season</title>
    <script src="js/Season.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ContentPlaceHolderID="mainBody" runat="server">
    <fieldset>
        <legend>Filters</legend>
        <form runat="server">
            <ul>
                <li>
                    <label runat="server">Points</label>
                    <input type="text" name="points-low" value="<%=Request["points-low"] %>" /> to <input type="text" name="points-high" value="<%=Request["points-high"] %>"  />        
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
                    <label>Show Projected Stats</label>
                    <input type="checkbox" name="project" <%if (Request["project"] == "on"){Response.Write("checked");}%> />         
                </li>
                <li>
                    <input type="radio" name="show" value="" <%if(String.IsNullOrEmpty(Request["show"])) Response.Write("checked"); %> /> All Games <br />
                    <input type="radio" name="show" value="home" <%if(Request["show"] == "home") Response.Write("checked"); %> /> Home Games <br />
                    <input type="radio" name="show" value="away" <%if(Request["show"] == "away") Response.Write("checked"); %> /> Away Games
                </li>
                <li>
                    <input type="submit" value="Filter" />
                </li>
            </ul>
        </form>
    </fieldset>
    <br />
    <div>
        <asp:DataGrid ID="dgSeasons" runat="server" CssClass="dataGrid" CellPadding="2">
        </asp:DataGrid>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="explainingColumn" runat="server">
        <b>Filters:</b>
        <ul>
            <li>
                Projected Totals:  If a season is currently in progress, this will display what the team is on pace for in an 82 game season.
            </li>
        </ul>
         <b>Columns:</b>
        <ul>
            <li>
                Average Player Age:  The average age of the team.  This is based on the players age as of October 1st of the year the season started, and is weighted for how many games the player played in.  This only includes skaters, and not goalies.
            </li>
        </ul>
</asp:Content>