<%@ Control Language="C#" EnableTheming="false"  Inherits="Subtext.Web.UI.Controls.BlogStats" %>
<div class="BlogStats">posts - <asp:Literal ID = "PostCount" Runat = "server" />, comments - <asp:Literal ID = "CommentCount" Runat = "server" />, trackbacks - <asp:Literal ID = "PingTrackCount" Runat = "server" /><asp:Literal ID = "StoryCount" Runat = "server" Visible="False" /></div>