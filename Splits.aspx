<%@ Page  MasterPageFile="~/Master/Main.master" Language="C#" AutoEventWireup="true" CodeFile="Splits.aspx.cs" Inherits="Splits" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Season Splits</title>
    <script src="js/Splits.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainBody" runat="server">
    <fieldset>
        <legend>Filters</legend>
        <form id="Form1" runat="server" action="Splits.aspx">
            <ul>
                <li>
                    <label>Season</label>
                    <asp:DropDownList ID="season" AppendDataBoundItems="true" runat="server">
                    </asp:DropDownList> 
                </li>
                <li>
                    <input type="submit" value="Search" />
                </li>
            </ul>
        </form>
    </fieldset>
    <br />
    <div>
        <asp:DataGrid ID="dgSummary" runat="server" CssClass="dataGrid" CellPadding="2">
        </asp:DataGrid>
    </div>
    <br />
    <div>
        <asp:DataGrid ID="dgDivision" runat="server" CssClass="dataGrid" CellPadding="2">
        </asp:DataGrid>
    </div>
    <br />
    <div>
        <asp:DataGrid ID="dgTeam" runat="server" CssClass="dataGrid" CellPadding="2">
        </asp:DataGrid>
    </div>
    <br />
    <div>
        <asp:DataGrid ID="dgMonth" runat="server" CssClass="dataGrid" CellPadding="2">
        </asp:DataGrid>
    </div>
    <br />
    <div>
        <asp:DataGrid ID="dgDay" runat="server" CssClass="dataGrid" CellPadding="2">
        </asp:DataGrid>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="explainingColumn" runat="server">
       <b>About:</b>
        <ul>
            <li>
                Select a season to break down the team record by Home/Away, Conference, Team, Month, and Day of the Week.
            </li>
        </ul>
</asp:Content>