<%@ Control Language="C#" EnableTheming="false"  Inherits="Subtext.Web.UI.Controls.MonthList" %>
<%@ Register TagPrefix="uc1" TagName="EntryList" Src="EntryList.ascx" %>
<%@ Import Namespace = "Subtext.Framework" %>
<div id="entries">
<uc1:EntryList id="MonthListings" runat="server"></uc1:EntryList>
</div>