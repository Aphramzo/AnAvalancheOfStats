<%@ Page MasterPageFile="~/Master/Main.master" Language="C#" AutoEventWireup="true" CodeFile="Playoffs.aspx.cs" Inherits="Playoffs" %>

<asp:Content ContentPlaceHolderID="head" runat="server">
    <title>Team Playoff Stats</title>
    <script src="js/Season.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ContentPlaceHolderID="mainBody" runat="server">
    <fieldset>
        <legend>Filters</legend>
        <form id="Form1" runat="server">
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
                Seasons:  Inclusive filter.  
            </li>
        </ul>
</asp:Content>