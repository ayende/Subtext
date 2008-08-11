using System;
using System.Globalization;
using System.Net;
using System.Web;

namespace Subtext.Akismet
{
	/// <summary>
	/// The client class used to communicate with the 
	/// <see href="http://akismet.com/">Akismet</see> service.
	/// </summary>
	[Serializable]
	public class AkismetClient
	{
        [NonSerialized]
		private HttpClient httpClient;


		static readonly string version = typeof(HttpClient).Assembly.GetName().Version.ToString();
		static readonly Uri verifyUrl = new Uri("http://rest.akismet.com/1.1/verify-key");
		const string checkUrlFormat = "http://{0}.rest.akismet.com/1.1/comment-check";
		const string submitSpamUrlFormat = "http://{0}.rest.akismet.com/1.1/submit-spam";
		const string submitHamUrlFormat = "http://{0}.rest.akismet.com/1.1/submit-ham";

		Uri submitSpamUrl;
		Uri submitHamUrl;
		Uri checkUrl;
		
		
		/// <summary>
		/// Initializes a new instance of the <see cref="AkismetClient"/> class.
		/// </summary>
		/// <remarks>
		/// This constructor takes in all the dependencies to allow for 
		/// dependency injection and unit testing. Seems like overkill, 
		/// but it's worth it.
		/// </remarks>
		/// <param name="apiKey">The Akismet API key.</param>
		/// <param name="blogUrl">The root url of the blog.</param>
		/// <param name="httpClient">Client class used to make the underlying requests.</param>
		public AkismetClient(string apiKey, Uri blogUrl, HttpClient httpClient)
		{
			if(apiKey == null)
				throw new ArgumentNullException("The akismet Api Key must be specified");

			if (blogUrl == null)
				throw new ArgumentNullException("The blog's url must be specified");
			
			if (httpClient == null)
				throw new ArgumentNullException("Must supply an http client");
			
			this.apiKey = apiKey;
			this.blogUrl = blogUrl;
			this.httpClient = httpClient;

			SetServiceUrls();
		}
		
		/// <summary>
		/// Initializes a new instance of the <see cref="AkismetClient"/> class.
		/// </summary>
		/// <param name="apiKey">The Akismet API key.</param>
		/// <param name="blogUrl">The root url of the blog.</param>
		public AkismetClient(string apiKey, Uri blogUrl) : this(apiKey, blogUrl, new HttpClient())
		{
			
		}
		
		void SetServiceUrls()
		{
			this.submitHamUrl = new Uri(String.Format(submitHamUrlFormat, this.apiKey));
			this.submitSpamUrl = new Uri(String.Format(submitSpamUrlFormat, this.apiKey));
			this.checkUrl = new Uri(String.Format(checkUrlFormat, this.apiKey));
		}

		/// <summary>
		/// Gets or sets the Akismet API key.
		/// </summary>
		/// <value>The API key.</value>
		public string ApiKey
		{
			get { return this.apiKey ?? string.Empty; }
			set 
			{ 
				this.apiKey = value ?? string.Empty;
				SetServiceUrls();
			}
		}

		string apiKey;

		/// <summary>
		/// Gets or sets the Usera Agent for the Akismet Client.  
		/// Do not confuse this with the user agent for the comment 
		/// being checked.
		/// </summary>
		/// <value>The API key.</value>
		public string UserAgent
		{
			get { return this.userAgent ?? BuildUserAgent("Subtext", version); }
			set { this.userAgent = value; }
		}

		string userAgent;

		/// <summary>
		/// Helper method for building a user agent string in the format 
		/// preferred by Akismet.
		/// </summary>
		/// <param name="applicationName">Name of the application.</param>
		/// <param name="appVersion">The version of the app.</param>
		/// <returns></returns>
		public static string BuildUserAgent(string applicationName, string appVersion)
		{
			return string.Format("{0}/{1} | Akismet/1.11", applicationName, version);
		}

		/// <summary>
		/// Gets or sets the timeout in milliseconds for the http request to Akismet. 
		/// By default 5000 (5 seconds).
		/// </summary>
		/// <value>The timeout.</value>
		public int Timeout
		{
			get { return this.timeout; }
			set { this.timeout = value; }
		}

		int timeout = 5000;
		
		/// <summary>
		/// Gets or sets the root URL to the blog.
		/// </summary>
		/// <value>The blog URL.</value>
		public Uri BlogUrl
		{
			get { return this.blogUrl; }
			set { this.blogUrl = value; }
		}

		Uri blogUrl;

		/// <summary>
		/// Gets or sets the proxy to use.
		/// </summary>
		/// <value>The proxy.</value>
		public IWebProxy Proxy
		{
			get
			{
				return this.proxy;
			}
			set
			{
				this.proxy = value;
			}
		}

		IWebProxy proxy;

		/// <summary>
		/// Verifies the API key.  You really only need to
		/// call this once, perhaps at startup.
		/// </summary>
		/// <returns></returns>
		/// <exception type="Sytsem.Web.WebException">If it cannot make a request of Akismet.</exception>
		public bool VerifyApiKey()
		{
			string parameters = "key=" + HttpUtility.UrlEncode(this.ApiKey) + "&blog=" + HttpUtility.UrlEncode(this.BlogUrl.ToString());
			string result = this.httpClient.PostRequest(verifyUrl, this.UserAgent, this.Timeout, parameters);

			if (String.IsNullOrEmpty(result))
				throw new InvalidResponseException("Akismet returned an empty response");
			
			return String.Equals("valid", result, StringComparison.InvariantCultureIgnoreCase);
		}
		
		/// <summary>
		/// Checks the comment and returns true if it is spam, otherwise false.
		/// </summary>
		/// <param name="comment"></param>
		/// <returns></returns>
		public bool CheckCommentForSpam(IComment comment)
		{
			string result = SubmitComment(comment, this.checkUrl);

			if (String.IsNullOrEmpty(result))
				throw new InvalidResponseException("Akismet returned an empty response");
			
			if (result != "true" && result != "false")
				throw new InvalidResponseException(string.Format("Received the response '{0}' from Akismet. Probably a bad API key.", result));
			
			return bool.Parse(result);
		}

		/// <summary>
		/// Submits a comment to Akismet that should have been 
		/// flagged as SPAM, but was not flagged by Akismet.
		/// </summary>
		/// <param name="comment"></param>
		/// <returns></returns>
		public void SubmitSpam(IComment comment)
		{
			SubmitComment(comment, this.submitSpamUrl);
		}

		/// <summary>
		/// Submits a comment to Akismet that should not have been 
		/// flagged as SPAM (a false positive).
		/// </summary>
		/// <param name="comment"></param>
		/// <returns></returns>
		public void SubmitHam(IComment comment)
		{
			SubmitComment(comment, this.submitHamUrl);
		}
		
		string SubmitComment(IComment comment, Uri url)
		{
			//Not too many concatenations.  Might not need a string builder.
			string parameters = "blog=" + HttpUtility.UrlEncode(this.blogUrl.ToString())
								+ "&user_ip=" + comment.IpAddress.ToString()
								+ "&user_agent=" + HttpUtility.UrlEncode(comment.UserAgent);

			if (!String.IsNullOrEmpty(comment.Referer))
				parameters += "&referer=" + HttpUtility.UrlEncode(comment.Referer);

			if (comment.Permalink != null)
				parameters += "&permalink=" + HttpUtility.UrlEncode(comment.Permalink.ToString());

			if (!String.IsNullOrEmpty(comment.CommentType))
				parameters += "&comment_type=" + HttpUtility.UrlEncode(comment.CommentType);

			if (!String.IsNullOrEmpty(comment.Author))
				parameters += "&comment_author=" + HttpUtility.UrlEncode(comment.Author);

			if (!String.IsNullOrEmpty(comment.AuthorEmail))
				parameters += "&comment_author_email=" + HttpUtility.UrlEncode(comment.AuthorEmail);

			if (comment.AuthorUrl != null)
				parameters += "&comment_author_url=" + HttpUtility.UrlEncode(comment.AuthorUrl.ToString());

			if (!String.IsNullOrEmpty(comment.Content))
				parameters += "&comment_content=" + HttpUtility.UrlEncode(comment.Content);

			if (comment.ServerEnvironmentVariables != null)
			{
				foreach (string key in comment.ServerEnvironmentVariables)
				{
					parameters += "&" + key + "=" + HttpUtility.UrlEncode(comment.ServerEnvironmentVariables[key]);
				}
			}

			return this.httpClient.PostRequest(url, this.UserAgent, this.Timeout, parameters).ToLower(CultureInfo.InvariantCulture);
		}
	}
}
