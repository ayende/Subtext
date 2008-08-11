using System;
using System.Web.UI;

namespace Subtext.Web.Controls
{
	/// <summary>
	/// Summary description for CoComment.
	/// </summary>
	public class CoComment : Control
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="CoComment"/> class.
		/// </summary>
		public CoComment()
		{
		}

		/// <summary>
		/// Gets or sets the blog tool.
		/// </summary>
		/// <value>The blog tool.</value>
		public string BlogTool
		{
			get { return ViewState["BlogTool"] as string; }
			set { ViewState["BlogTool"] = value; }
		}

		/// <summary>
		/// Gets or sets the blog URL.
		/// </summary>
		/// <value>The blog URL.</value>
		public string BlogUrl
		{
			get { return ViewState["BlogUrl"] as string; }
			set { ViewState["BlogUrl"] = value; }
		}

		/// <summary>
		/// Gets or sets the blog title.
		/// </summary>
		/// <value>The blog title.</value>
		public string BlogTitle
		{
			get { return ViewState["BlogTitle"] as string; }
			set { ViewState["BlogTitle"] = value; }
		}

		/// <summary>
		/// Gets or sets the post title.
		/// </summary>
		/// <value>The post title.</value>
		public string PostTitle
		{
			get { return ViewState["PostTitle"] as string; }
			set { ViewState["PostTitle"] = value; }
		}

		/// <summary>
		/// Gets or sets the post URL.
		/// </summary>
		/// <value>The post URL.</value>
		public string PostUrl
		{
			get { return ViewState["PostUrl"] as string; }
			set { ViewState["PostUrl"] = value; }
		}

		/// <summary>
		/// Gets or sets the name of the comment author field.
		/// </summary>
		/// <value>The name of the comment author field.</value>
		public string CommentAuthorFieldName
		{
			get { return ViewState["CommentAuthorFieldName"] as string; }
			set { ViewState["CommentAuthorFieldName"] = value; }
		}

		/// <summary>
		/// Gets or sets the name of the comment text field.
		/// </summary>
		/// <value>The name of the comment text field.</value>
		public string CommentTextFieldName
		{
			get { return ViewState["CommentTextFieldName"] as string; }
			set { ViewState["CommentTextFieldName"] = value; }
		}

		/// <summary>
		/// Gets or sets the comment button id.
		/// </summary>
		/// <value>The comment button id.</value>
		public string CommentButtonId
		{
			get { return ViewState["CommentButtonId"] as string; }
			set { ViewState["CommentButtonId"] = value; }
		}

		public string CommentFormId
		{
			get { return ViewState["CommentFormId"] as string; }
			set { ViewState["CommentFormId"] = value; }
		}

		/// <summary>
		/// Sends server control content to a provided <see cref="T:System.Web.UI.HtmlTextWriter"/> object, which writes the content to
		/// be rendered on
		/// the client.
		/// </summary>
		/// <param name="writer">The <see langword="HtmlTextWriter"/> object that receives the server control content.</param>
		protected override void Render(HtmlTextWriter writer)
		{
			writer.Write(string.Format(ScriptHelper.UnpackScript("CoCommentScript.js"), this.BlogTool, this.BlogUrl, this.BlogTitle, this.PostTitle, this.PostUrl, this.CommentAuthorFieldName, this.CommentTextFieldName, this.CommentButtonId, this.CommentFormId));
		}
	}
}
