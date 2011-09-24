<%@ Page MasterPageFile="~/Master/Main.master" Language="C#" AutoEventWireup="true" CodeFile="LoadData.aspx.cs" Inherits="LoadData" %>
<asp:Content ContentPlaceHolderID="mainBody" runat="server">
    <asp:Label ID="WrongPassword" Visible="false" runat="server">
        <div style="padding: 0 .7em;" class="ui-state-error ui-corner-all">
            <p><span style="float: left; margin-right: .3em;" class="ui-icon ui-icon-alert"></span> 
					Sorry, but that is the wrong password.
        </div>
    </asp:Label>
    <form id="form1" runat="server">
    <div>
        Password: <input type="password" name="pwd" />
        <br />
        Season: 
        <select name="season">
            <option value="20072008">07-08</option>
            <option value="20082009">08-09</option>
            <option value="20092010">09-10</option>
            <option value="20102011">10-11</option>
        </select>
        <br />
        From Game Number: <input name="from" />
        <br />
        To Game Number: <input name="to" />
        <input type="submit" value="Load Games" />
    </div>
    </form>
</asp:Content>