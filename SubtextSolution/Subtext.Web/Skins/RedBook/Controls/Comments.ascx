<%@ Control Language="C#" EnableTheming="false"  AutoEventWireup="false" Inherits="Subtext.Web.UI.Controls.Comments" %>
<hr/>
<a id="feedback" title="feedback anchor"></a>
<div id="moreinfo">
	<h2>Feedback</h2>

    <p><asp:Literal ID="NoCommentMessage" Runat ="server" /></p>
    <asp:Repeater id="CommentList" runat="server" OnItemCreated="CommentsCreated" OnItemCommand="RemoveComment_ItemCommand">
	    <ItemTemplate>
		    <a name="<%# Comment.Id %>"></a>
		    <div class="comment<%# AuthorCssClass %>">
			    <h4>
				    <asp:HyperLink Runat="server" ID="EditCommentImgLink" /><asp:Literal Runat = "server" ID = "Title" />
			    </h4>
			    <asp:Image runat="server" id="GravatarImg" visible="False" CssClass="avatar" AlternateText="Gravatar" />
			    <asp:Literal id="PostText" Runat="server" />
			    <span class="commentInfo"><asp:Literal id="PostDate" Runat="server" /> | <cite><asp:HyperLink Target="_blank" Runat="server" ID="NameLink" /></cite></span>
			    <asp:LinkButton Runat="server" ID="EditLink" CausesValidation="False" />
		    </div>
	    </ItemTemplate>
    </asp:Repeater>

</div>
<hr/>
