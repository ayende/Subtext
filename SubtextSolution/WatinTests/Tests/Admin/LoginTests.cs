using System;
using System.Threading;
using MbUnit.Framework;
using WatiN.Core;

namespace WatinTests.Tests.Admin
{
	[TestFixture(ApartmentState = ApartmentState.STA)]
	public class LoginTests
	{
		[Test]
		public void LoginRequiresCorrectUsernameAndPassword()
		{
			using (Browser browser = new Browser())
			{
				browser.GoToAdmin();
                if (!browser.IsOnLoginPage) {
                    browser.Logout();
                    browser.GoToAdmin();
                }
				Assert.IsTrue(browser.IsOnLoginPage);
				browser.Login("username", "not-password");
				Assert.IsTrue(browser.ContainsText("That�s not it"), "Expected an error message.");

				browser.Login("not-username", "password");
				Assert.IsTrue(browser.ContainsText("That�s not it"), "Expected an error message.");
			}
		}

		[Test]
		public void EmailPasswordFailsForUnknownUserName()
		{
			using(Browser browser = new Browser())
			{
				browser.GoToAdmin();
                if (!browser.IsOnLoginPage)
                {
                    browser.Logout();
                    browser.GoToAdmin();
                }
				browser.Login("not-username", "password");
                browser.Link(Find.ByText("Forgot Your Password?")).Click();
                Assert.IsTrue(browser.ContainsText("We cannot retrieve your password"));
			}
		}
	}
}
