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
using System.Configuration;

namespace Subtext.Framework.Syndication.Compression
{
	internal class SyndicationCompressionSectionHandler :  IConfigurationSectionHandler	
	{
		object IConfigurationSectionHandler.Create(object parent, object configContext, System.Xml.XmlNode configurationSection) 
		{
			return new SyndicationCompressionSettings(configurationSection);
		}
	}
}
