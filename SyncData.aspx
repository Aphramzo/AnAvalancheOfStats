<%@ Page MasterPageFile="~/Master/Main.master" Language="C#" AutoEventWireup="true" CodeFile="SyncData.aspx.cs" Inherits="SyncData" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Time Per Stats</title>
</asp:Content>
<asp:Content ContentPlaceHolderID="mainBody" runat="server">
    <asp:Label ID="WrongPassword" Visible="false" runat="server">
        <div style="padding: 0 .7em;" class="ui-state-error ui-corner-all">
            <p><span style="float: left; margin-right: .3em;" class="ui-icon ui-icon-alert"></span> 
					Sorry, but that is the wrong password.
        </div>
    </asp:Label>
    <asp:Label ID="InfoLabel" Visible="false" runat="server">
        
    </asp:Label>
    <form id="form1" runat="server">
    <div>
     Password: <input type="password" name="pwd" />
     <br />
     <input type="submit" value="Sync Data" />
    </div>
    </form>
</asp:Content>