<%@ Page MasterPageFile="~/Master/Main.master" Language="C#" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ContentPlaceHolderID="mainBody" runat="server">
    <br />
    <span class="left"> 
        <div class="Updates ">
            <label class="UpdatesLabel">Latest Feature Additions</label>   
            <br />
            <%=LazyMansFeatureUpdates() %>
        </div>
        <div class="Updates ">
            <label class="UpdatesLabel">Latest Stats Updates</label>
            <br />
            <%=LazyMansStatUpdates() %>
        </div>
    </span>
    
    <span class="MainUpdate">
        <label>In the Works</label>
        <br />
        <br />
        <b>ADDED: <a href="../Goalies.aspx">Goalie Stats</a></b><br />
            &nbsp;&nbsp;&nbsp;&nbsp;Who doesn't like goalies?<br />
        <b>ADDED: <a href="../PlayerPlayoffs.aspx">Playoff Stats</a></b><br />
            &nbsp;&nbsp;&nbsp;&nbsp;Feels like its been a while...<br />
        <b>Home vs Away Stats</b><br />
            &nbsp;&nbsp;&nbsp;&nbsp;Is Stastny really that much better on home ice?<br />
        <b>Division Stats</b><br />
            &nbsp;&nbsp;&nbsp;&nbsp;Who comes up big in those important games?<br />
        <b>ADDED: <a href="../PlayersPercent.aspx">Team % Stats</a></b><br />
            &nbsp;&nbsp;&nbsp;&nbsp;Sure, Hejduk only got 59 points in 08-09, but he still lead the team.<br />
        <b>ADDED: <a href="../TimePer.aspx">TOI Per Stats</a></b><br />
            &nbsp;&nbsp;&nbsp;&nbsp;Just how much ice time would Forsberg log for each point scored?<br />
    </span>
    
   
    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="explainingColumn" runat="server">
    <b>About:</b>
    <ul>
        <li>
            Welcome to an Avalanche of Stats.  Here you <i>will</i> be able to find just about any stat about the Colorado Avalanche you can dream up.  
            Stats will be updated as regularly as humanly possible.  You can find the last time they were updated over to the left.  This is a work in progess,
            so please check back often.  You can see what updates have been added over to the left.  Message boards will be available soon, so if you have found a problem,
            or would like to request a stat be added, you can post there.
        </li>
        <li>
            Remember, as with all stats, they are just numbers.  It is impossible to truly measure how "good" a player is.  That said, they can go a long way assuming you know how to interpret them.
        </li>
    </ul>
</asp:Content>