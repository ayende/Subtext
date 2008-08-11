#region Disclaimer/Info
///////////////////////////////////////////////////////////////////////////////////////////////////
// Subtext WebLog
// 
// Subtext is an open source weblog system that is a fork of the .TEXT
// weblog system.
//
// For updated news and information please visit http://subtextproject.com/
// Subtext is hosted at SourceForge at http://sourceforge.net/projects/subtext
// The development mailing list is at subtext-devs@lists.sourceforge.net 
//
// This project is licensed under the BSD license.  See the License.txt file for more information.
///////////////////////////////////////////////////////////////////////////////////////////////////
#endregion

using System;
using System.IO;
using blowery.Web.HttpCompress;

namespace Subtext.Framework.Syndication.Compression
{
	public static class SyndicationCompressionHelper
	{
		#region -- GetFilterForScheme(schemes, Stream) Method --
		public static SyndicationCompressionFilter GetFilterForScheme(string schemes, Stream contextFilter) 
		{
			SyndicationCompressionSettings settings;
			SyndicationCompressionFilter filter = null;
			bool foundDeflate = false;
			bool foundGZip = false;

			schemes = schemes.ToLower(System.Globalization.CultureInfo.InvariantCulture);
			settings = SyndicationCompressionSettings.GetSettings();

			if(schemes.IndexOf("deflate") >= 0)
			{
				foundDeflate = true;
				
			}
			if(schemes.IndexOf("gzip") >= 0)
			{
				foundGZip = true;
			}

			if(settings.CompressionType == Algorithms.Deflate && foundDeflate)
			{
				filter = new SyndicationCompressionFilter(new DeflateFilter(contextFilter, settings.CompressionLevel), "deflate");
			}
			else if(settings.CompressionType == Algorithms.GZip && foundGZip)
			{
				filter = new SyndicationCompressionFilter(new GZipFilter(contextFilter), "gzip");
			}
			else if(foundDeflate) //-- If Use Accepts Other Than Configured
			{
				filter = new SyndicationCompressionFilter(new DeflateFilter(contextFilter, settings.CompressionLevel), "deflate");
			}
			else if(foundGZip) //-- If Use Accepts Other Than Configured
			{
				filter = new SyndicationCompressionFilter(new GZipFilter(contextFilter), "gzip");
			}
      
			return filter;
		}
		#endregion
	}
}
