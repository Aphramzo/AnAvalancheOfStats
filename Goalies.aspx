<%@ Page  MasterPageFile="~/Master/Main.master" Language="C#" AutoEventWireup="true" CodeFile="Goalies.aspx.cs" Inherits="Goalies" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Goalie Stats by Season</title>
    <script src="js/Goalies.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainBody" runat="server">
    <fieldset>
        <legend>Filters</legend>
        <form id="Form1" runat="server" action="Goalies.aspx">
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
                    <label>Show Player Totals</label>
                    <input type="checkbox" id="sum" name="sum" <%if (Request["sum"] == "on"){Response.Write("checked");}%> />         
                </li>
                <li>
                    <input type="submit" value="Filter" />
                </li>
            </ul>
        </form>
    </fieldset>
    <br />
    <div>
        <asp:DataGrid ID="dgGoalieSeasons" runat="server" CssClass="dataGrid" CellPadding="2">
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
        </ul>
    
        <b>Filters:</b>
        <ul>
            <li>
                Games Played:  Total number of games played by player in the season.  This is inclusive, so if you enter 20 as the minimum number, it will show players who played at least 20 or more games in a season.  If Player Totals is selected, this will filter based on the total number of games they played.
            </li>
            <li>
                Seasons:  Also an inclusive filter.  If Player Totals is selected, it will sum all stats that fall within this filter.
            </li>
            <li>
                Country:  Country the player was born in.
            </li>
            <li>
                Age:  Age of the player. If Player Totals is selected, it will sum all stats from seasons that fall within this filter.
            </li>
            <li>
                Player Totals:  Show all time stats.
            </li>
        </ul>
</asp:Content>