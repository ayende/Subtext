<%@ Control Language="C#" EnableTheming="false"  AutoEventWireup="false" Inherits="Subtext.Web.UI.Controls.RelatedLinks" %>
<a name = "links">
<div id="relatedlinks">
	<h3>Related Links</h3>
		<asp:Repeater id="Links" runat="server" OnItemCreated="MoreReadingCreated" OnItemCommand="RemovePTR_ItemCommand">
			<HeaderTemplate>
				<ul>
			</HeaderTemplate>
			<ItemTemplate>
				<li>
					<asp:HyperLink Target="_blank" Runat="server" ID="Link" />
					<asp:Literal Runat="server" ID="DisplayType" />
					<asp:LinkButton Runat="server" ID="EditReadingLink" CausesValidation="False" />								
			</li>	
		</ItemTemplate>
		<FooterTemplate>
			</ul>
		</FooterTemplate>
	</asp:Repeater>
</div>
