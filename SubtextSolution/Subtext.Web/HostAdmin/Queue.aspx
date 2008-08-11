<%@ Page Language="C#" EnableTheming="false"  Title="Subtext - Host Admin - Queue" MasterPageFile="~/HostAdmin/HostAdminTemplate.Master"  Codebehind="Queue.aspx.cs" AutoEventWireup="True" Inherits="Subtext.Web.HostAdmin.Queue" EnableViewState="false" %>

<asp:Content id="sectionTitle" ContentPlaceHolderID="MPSectionTitle" runat="server">Subtext - Host Admin - Queue</asp:Content>
<asp:Content id="sidebar" ContentPlaceHolderID="MPSideBar" runat="server"></asp:Content>
<asp:Content id="queueStats" ContentPlaceHolderID="MPContent" runat="server">
	<p>
		<label>Active Threads:</label>
		<asp:Literal id="ltlActiveThreads" runat="server"></asp:Literal>
	</p>
	<p>
		<label>Waiting Callbacks:</label>
		<asp:Literal id="ltlWaitingCallbacks" runat="server"></asp:Literal>
	</p>

</asp:Content>