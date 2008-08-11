<%@ Control Language="C#" EnableTheming="false"  AutoEventWireup="false" Inherits="Subtext.Web.UI.Controls.Comments" %>
<div id="comments" class="section">
	<a name="feedback"></a>
	<h2 class="section-title">Your Comments.</h2>
	<p><asp:Literal ID = "NoCommentMessage" Runat ="server" /></p>
	<asp:Repeater ID="CommentList" runat="server" OnItemCreated="CommentsCreated" OnItemCommand="RemoveComment_ItemCommand">
	    <HeaderTemplate><ul class="comment-list" id="commentList"></HeaderTemplate>
	    <ItemTemplate>
	        <li id="<%# Comment.Id %>" class="comment normal-comment<%# AuthorCssClass %>">
	            <div class="comment-body">
		            <cite>
			            <strong>
			                <a href="<%# EditUrl(Comment) %>" runat="server" title="edit comment" class="edit-comment"
			                    style='<%# IsEditEnabled ? "" : "display:none;" %>' ></a><asp:Literal ID="Title" Runat="server" />
			            </strong>
		            </cite>
		            <p><asp:Image runat="server" id="GravatarImg" visible="False" CssClass="avatar" AlternateText="Gravatar" /><asp:Literal ID="PostText" Runat="server" /></p>
	            </div>
	            <div class="comment-date">Left by <asp:HyperLink Target="_blank" Runat="server" ID="NameLink" /> at <asp:Literal id = "PostDate" Runat = "server" /></div>
	            <asp:LinkButton Runat="server" cssclass="editlink" ID="EditLink" CausesValidation="False" />
            </li>
	    </ItemTemplate>
	    <AlternatingItemTemplate>
	        <li id="<%# Comment.Id %>" class="comment alternate-comment<%# AuthorCssClass %>">
	            <div class="comment-body">
		            <cite>
			            <strong>
				            <a href="<%# EditUrl(Comment) %>" runat="server" title="edit comment" class="edit-comment"
			                    style='<%# IsEditEnabled ? "" : "display:none;" %>' ></a><asp:Literal ID="Title" Runat="server" />
			            </strong>
		            </cite>
 		            <p class="comment-text"><asp:Image runat="server" id="GravatarImg" visible="False" CssClass="avatar" AlternateText="Gravatar" /><asp:Literal ID="PostText" Runat="server" /></p>
	            </div>
	            <div class="comment-date">Left by <asp:HyperLink Target="_blank" Runat="server" ID="NameLink" /> at <asp:Literal id = "PostDate" Runat = "server" /></div>
	            <asp:LinkButton Runat="server" cssclass="editlink" ID="EditLink" CausesValidation="False" />
            </li>
	    </AlternatingItemTemplate>
	    <FooterTemplate></ul></FooterTemplate>
	</asp:Repeater>    
</div>
