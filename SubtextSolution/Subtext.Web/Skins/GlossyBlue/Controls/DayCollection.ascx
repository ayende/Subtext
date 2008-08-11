<%@ Import Namespace = "Subtext.Framework.Components" %>
<%@ Control Language="c#" Inherits="Subtext.Web.UI.Controls.DayCollection" %>
<%@ Register TagPrefix="uc1" TagName="Day" Src="Day.ascx" %>
<asp:Repeater id="DaysList" runat="server">
	<ItemTemplate>
		<uc1:Day id="DayItem" CurrentDay = '<%# Container.DataItem %>' runat="server" />
	</ItemTemplate>
	<SeparatorTemplate><p /></SeparatorTemplate>
</asp:Repeater>
