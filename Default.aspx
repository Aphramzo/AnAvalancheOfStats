<%@ Page MasterPageFile="~/Master/Main.master" Language="C#" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="_Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>An Avalanche Of Stats</title>
    <script src="js/LandingPage.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ContentPlaceHolderID="mainBody" runat="server">
    <br />
    
    <span class="MainUpdate">
        <label>Upcoming Milestones</label>
        <br />
        <br />
        <div id="tabs">
               <ul>
                    <li><a href="#GP">Games Played</a></li>
		            <li><a href="#Goals">Goals</a></li>
		            <li><a href="#Points">Points</a></li>
	            </ul>
	            <div id="Goals">
	                <%=LazyMansMilestones() %>
                </div>
                <div id="Points">
	                <%=LazyMansPointMilestones() %>
                </div>
                <div id="GP">
	                <%=LazyMansGPMilestones() %>
                </div>
        </div>
        
    </span>
    
   
    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="explainingColumn" runat="server">
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
</asp:Content>