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
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace Subtext.Framework.Util
{
	/// <summary>
	/// Class with methods for saving and loading objects as 
	/// serialized instances.
	/// </summary>
	public static class SerializationHelper
	{
		/// <summary>
        /// Loads the specified type based on the specified stream.
        /// </summary>
        /// <param name="stream">stream containing the type.</param>
        /// <returns></returns>
        public static T Load<T>(Stream stream)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            return (T)serializer.Deserialize(stream);
        }
		
		/// <summary>
		/// Serializes an object to a base64 encoded string.
		/// </summary>
		/// <param name="o"></param>
		/// <returns></returns>
		public static string SerializeToBase64String(object o)
		{
			MemoryStream stream = new MemoryStream();
			BinaryFormatter formatter = new BinaryFormatter();
			formatter.Serialize(stream, o);
			byte[] serialized = stream.ToArray();
			return Convert.ToBase64String(serialized);
		}

		/// <summary>
		/// Deserializes from base64 string.
		/// </summary>
		/// <param name="base64SerializedObject">The base64 serialized object.</param>
		/// <returns></returns>
		public static T DeserializeFromBase64String<T>(string base64SerializedObject)
		{
			byte[] serialized = Convert.FromBase64String(base64SerializedObject);
			MemoryStream stream = new MemoryStream(serialized);
			stream.Position = 0;
			BinaryFormatter formatter = new BinaryFormatter();
			object o = formatter.Deserialize(stream);
			return (T)o;
		}
	}
}
