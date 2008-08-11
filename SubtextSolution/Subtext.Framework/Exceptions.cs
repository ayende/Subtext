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

namespace Subtext.Framework
{
	public class IllegalPostCharactersException : Exception
	{
		public IllegalPostCharactersException(String s) : base(s) {}
		public IllegalPostCharactersException(String s, Exception inner) : base(s, inner) {}
	}

	public class BlogSkinException : Exception
	{
		public BlogSkinException(String s) : base(s) {}
		public BlogSkinException(String s, Exception inner) : base(s, inner) {}
		
	}

	public class BlogFailedPostException : Exception 
	{
		public BlogFailedPostException(String s) : base(s) {}
		public BlogFailedPostException(String s, Exception inner) : base(s, inner) {}		
	}

	public class BlogAssemblyConfigException : Exception 
	{
		public BlogAssemblyConfigException(String s) : base(s) {}
		public BlogAssemblyConfigException(String s, Exception inner) : base(s, inner) {}		
	}

	public class ExtendedPropertiesOverFlowException : Exception 
	{
		public ExtendedPropertiesOverFlowException() : base("ExtendedProperties bytes overflow. The ExtendedProperties is limited to 7800 bytes")
		{
			
		}
		public ExtendedPropertiesOverFlowException(String s) : base(s) {}
		public ExtendedPropertiesOverFlowException(String s, Exception inner) : base(s, inner) {}		
	}
}
