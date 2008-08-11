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
using System.Globalization;
using System.Xml.Serialization;

namespace Subtext.Framework.UI.Skinning
{
	/// <summary>
	/// Summary description for SkinTemplate.
	/// </summary>
	[Serializable]
	public class SkinTemplate
	{
        public static SkinTemplate Empty
        {
            get
            {
                return empty;
            }
        }
        static SkinTemplate empty = new SkinTemplate();

		/// <summary>
		/// This is the folder that contains the template files (*.ascx) 
		/// for the current skin.
		/// </summary>
		[XmlAttribute]
		public string TemplateFolder
		{
			get {return this.templateFolder;}
			set {this.templateFolder = value;}
		}

		private string templateFolder;

        [XmlAttribute]
        public MobileSupport MobileSupport
        {
            get { return this.mobileSupport; }
            set { this.mobileSupport = value; }
        }

        private MobileSupport mobileSupport = MobileSupport.None;

		/// <summary>
		/// Gets or sets the stylesheet for this Skin.  Remember, 
		/// every skin template folder should include a "style.css" 
        /// file that is rendered by default, unless ExcludeDefaultStyle is set to true.
		/// </summary>
		/// <remarks>
		/// This property makes it possible to have multiple skins 
		/// use the same template folder.
		/// </remarks>
		/// <value>The secondary CSS.</value>
		[XmlAttribute]
		public string StyleSheet
		{
			get {return this.styleSheet;}
			set {this.styleSheet = value;}
		}

		private string styleSheet;

        /// <summary>
        /// Exclude the the default style.css from being rendered in the skin.
        /// </summary>
        /// <value>Whether to exclude the default style.css or not.</value>
        [XmlAttribute]
	    public bool ExcludeDefaultStyle
	    {
	        get { return excludeDefaultStyle; }
	        set { excludeDefaultStyle = value; }
	    }

        private bool excludeDefaultStyle;

        /// <summary>
        /// Specifies the order in which the styles are rendered inside the skin.
        /// </summary>
        /// <value>The styles merge mode.</value>
        [XmlAttribute]
	    public StyleMergeMode StyleMergeMode
	    {
	        get { return styleMergeMode; }
	        set { styleMergeMode = value; }
	    }
        
        private StyleMergeMode styleMergeMode;


        /// <summary>
        /// How to merge all scripts into one.
        /// </summary>
        /// <remarks>
        /// Even if set to None, if the list of scripts is unsafe (remote scripts or scripts with parameters)
        /// the scripts are not merged.
        /// </remarks>
        /// <value>The script merge mode.</value>
        [XmlAttribute]
        public ScriptMergeMode ScriptMergeMode
        {
            get { return scriptMergeMode; }
            set { scriptMergeMode = value; }
        }

        private ScriptMergeMode scriptMergeMode;

        /// <summary>
        /// Whether or not to merge all scripts into one.
        /// </summary>
        [XmlIgnore]
	    public bool MergeScripts
	    {
            get { return ScriptMergeMode != ScriptMergeMode.DontMerge; }
	    }


	    /// <summary>
		/// Whether or not this skin template has a secondary skin css file.
		/// </summary>
		[XmlIgnore]
		public bool HasSkinStylesheet
		{
			get{return (this.StyleSheet != null && this.StyleSheet.Trim().Length > 0);}
		}

		/// <summary>
		/// Gets the name of the skin as will be displayed in the 
		/// drop-down list in the admin section.
		/// </summary>
		[XmlAttribute]
		public string Name
		{
			get {return this.name;}
			set {this.name = value;}
		}

		private string name;
		
		/// <summary>
		/// A key representing this particular skin.  A Skin 
		/// is really a combination of the TemplateFolder and 
		/// the Stylesheet specified.
		/// </summary>
		[XmlIgnore]
		public string SkinKey
		{
			get
			{
				return (this.TemplateFolder + (this.StyleSheet != null && this.StyleSheet.Length > 0 ? "-" + this.StyleSheet : string.Empty)).ToUpper(CultureInfo.InvariantCulture);
			}
		}

		private Script[] _scripts;
		/// <summary>
		/// Collection of <code>script</code> elements, declared for the skin.
		/// </summary>
		[XmlArray("Scripts")]
		public Script[] Scripts
		{
			get
			{
				return _scripts;
			}
			set
			{
				_scripts = value;
			}
		}

		private Style[] _styles;
		/// <summary>
		/// Collection of stylesheet elements declared for the skin.
		/// </summary>
		[XmlArray("Styles")]
		public Style[] Styles
		{
			get
			{
				return _styles;
			}
			set
			{
				_styles = value;
			}
		}
		
	}

    public enum MobileSupport
    { 
        /// <summary>This skin does not work on mobile devices</summary>
        None = 0,
        /// <summary>This skin works for both browsers and mobile devices</summary>
        Supported = 1,
        /// <summary>This skin is only suitable for mobile devices</summary>
        MobileOnly = 2,
    }

    public enum ScriptMergeMode
    {
        /// <summary>
        /// No merging of JS files
        /// </summary>
        DontMerge = 0,
        /// <summary>
        /// Merge the js scripts
        /// </summary>
        Merge = 1
    }

    public enum StyleMergeMode
    {
        /// <summary>
        /// No merging of CSS files
        /// </summary>
        None = 0,
        /// <summary>
        /// The merged css will be rendered after the ones that cannot be merged
        /// </summary>
        /// <remarks>
        /// The order will be:
        /// <list type="ordered">
        /// <item>All not mergeable files (with title, media and condition)</item>
        /// <item>All mergeable styles inside the skin definition</item>
        /// <item>style.css (which is the main css file for the skin)</item>
        /// <item>secondary css for the skin</item>
        /// <item>custom css (the one defined in the admin)</item>
        /// </list>
        /// </remarks>
        MergedAfter = 1,
        /// <summary>
        /// The merged css will be rendered before the ones that cannot be merged
        /// </summary>
        /// <remarks>
        /// The order will be:
        /// <list type="ordered">
        /// <item>All mergeable styles inside the skin definition</item>
        /// <item>style.css (which is the main css file for the skin)</item>
        /// <item>secondary css for the skin</item>
        /// <item>All not mergeable files (with title, media and condition)</item>
        /// <item>custom css (the one defined in the admin)</item>
        /// </list>
        /// </remarks>
        MergedFirst = 2
    }
}

