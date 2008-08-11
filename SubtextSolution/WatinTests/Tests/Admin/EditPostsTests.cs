using System;
using System.Threading;
using MbUnit.Framework;
using WatinTests.PageElements;
using WatiN.Core.DialogHandlers;

namespace WatinTests.Tests.Admin
{
	[TestFixture(ApartmentState = ApartmentState.STA)]
	public class EditPostsTests
	{
		[Test]
		public void CanCreateNewPost()
		{
			using(Browser browser = new Browser())
			{
				EditPostsPage page = browser.GoTo<EditPostsPage>();
                page.Browser.DialogWatcher.Add(new AlertAndConfirmDialogHandler());
                page.ClickNavLinkNoWait(PostsNavigationLink.New_Post);
				page.TitleField.Value = "Title of the post";
				page.RichTextEditorField.Value = "Body of the post";
				page.PostButton.Click();
				PostRow row = page.TableOfPosts.FindRowByDescription("Title of the post");
				Assert.IsTrue(row != null && row.Exists, "Could not find our post in the posts table.");
			}
		}

		[Test]
		public void CreateNewPostRequiresPostBody()
		{
			using (Browser browser = new Browser())
			{
				EditPostsPage page = browser.GoTo<EditPostsPage>();
                page.Browser.DialogWatcher.Add(new AlertAndConfirmDialogHandler());
				page.ClickNavLinkNoWait(PostsNavigationLink.New_Post);
                page.TitleField.Value = "Title of the post";
				page.PostButton.Click();

				Assert.IsTrue(browser.ContainsText("Your post must have a body"));
			}
		}
	}
}
