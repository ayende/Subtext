<%@ Control Language="C#" EnableTheming="false"  AutoEventWireup="false" Inherits="Subtext.Web.UI.Controls.GalleryThumbNailViewer" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<div id="gallery">
<div class="title"><asp:Literal id="GalleryTitle" runat="server" /></div>
<div class="description"><asp:Literal ID = "Description" Runat = "server" /></div>
<div class="thumbnails">
<asp:DataList id="ThumbNails" runat="server" OnItemCreated = "ImageCreated" RepeatColumns = "6" RepeatDirection = "Vertical">
	<ItemTemplate>
		<div class="thumbnail"><asp:HyperLink Runat="server" ID="ThumbNailImage"/></div>
	</ItemTemplate>
</asp:DataList>
</div>
</div>