<%@ Control Language="C#" EnableTheming="false"  Inherits="Subtext.Web.UI.Controls.MyLinks" %>
<ul id="nav">
	<li><asp:HyperLink Runat="server" CssClass="listitem" NavigateUrl="~/Default.aspx" Text="Home" ID="HomeLink" /></li>
	<li><asp:HyperLink AccessKey = "9" Runat="server" CssClass="listitem" NavigateUrl="~/Contact.aspx" Text="Contact" ID="ContactLink" /></li>
	<li><asp:HyperLink Runat="server"  CssClass="listitem" Text="Syndication" ID="Syndication" /> <asp:HyperLink Runat="server" ID="XMLLink" />
	<li><asp:HyperLink Runat="server" CssClass="listitem" Text="Admin" ID="Admin" /></li>
</ul>