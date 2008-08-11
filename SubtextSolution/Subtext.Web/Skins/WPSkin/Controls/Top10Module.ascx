<%@ Control Language="C#" EnableTheming="false"  AutoEventWireup="false" Inherits="Subtext.Web.UI.Controls.Top10Module" %>
<div class="popposts">
	<div class="poptitle">Popular Posts</div>
	<asp:Repeater id="Top10Entries" runat="server">
		<ItemTemplate>
			<div class="popitem">
					<a href="<%# DataBinder.Eval(Container.DataItem, "url") %>"> 
						<%# DataBinder.Eval(Container.DataItem, "Title") %> 
					</a> 
			</div>
		</ItemTemplate>
	</asp:Repeater>
</div>