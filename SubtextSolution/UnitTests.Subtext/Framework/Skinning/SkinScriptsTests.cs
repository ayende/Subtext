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
using System.Web.Hosting;
using MbUnit.Framework;
using Rhino.Mocks;
using Subtext.Framework.UI.Skinning;

namespace UnitTests.Subtext.Framework.Skinning
{
    [TestFixture]
    public class SkinScriptsTests
    {
        [Test]
        public void CanGetScriptMergeModeAttribute()
        {
            MockRepository mocks = new MockRepository();

            VirtualPathProvider pathProvider = GetTemplatesPathProviderMock(mocks);
            mocks.ReplayAll();

            SkinTemplateCollection templates = new SkinTemplateCollection(pathProvider);

            SkinTemplate templateWithMergeScriptMergeMode = templates.GetTemplate("Piyo");
            Assert.IsTrue(templateWithMergeScriptMergeMode.MergeScripts, "ScriptMergeMode should be Merge.");

            SkinTemplate templateWithDontMergeScriptMergeMode = templates.GetTemplate("Semagogy");
            Assert.IsFalse(templateWithDontMergeScriptMergeMode.MergeScripts, "ScriptMergeMode should be DontMerge.");

            SkinTemplate templateWithoutScriptMergeMode = templates.GetTemplate("RedBook-Green.css");
            Assert.IsFalse(templateWithoutScriptMergeMode.MergeScripts, "ScriptMergeMode should be None.");
        }

        [Test]
        public void ScriptElementCollectionRendererRendersScriptElements()
        {
            UnitTestHelper.SetHttpContextWithBlogRequest("localhost", "blog", string.Empty);
            MockRepository mocks = new MockRepository();

            VirtualPathProvider pathProvider = GetTemplatesPathProviderMock(mocks);
            mocks.ReplayAll();

            SkinTemplateCollection templates = new SkinTemplateCollection(pathProvider);
            ScriptElementCollectionRenderer renderer = new ScriptElementCollectionRenderer(templates);
            string scriptElements = renderer.RenderScriptElementCollection("RedBook-Green.css");

            Console.WriteLine(scriptElements);

            string script = @"<script type=""text/javascript"" src=""/Skins/RedBook/blah.js""></script>";
            Assert.IsTrue(scriptElements.IndexOf(script) > -1, "Rendered the script improperly.");

            Console.WriteLine(scriptElements);

            scriptElements = renderer.RenderScriptElementCollection("Nature-Leafy.css");
            script = @"<script type=""text/javascript"" src=""/scripts/XFNHighlighter.js""></script>";
            Assert.IsTrue(scriptElements.IndexOf(script) > -1, "Rendered the script improperly. We got: " + scriptElements);
        }

        [Test]
        public void ScriptElementCollectionRendererRendersJSHandlerScript()
        {
            UnitTestHelper.SetHttpContextWithBlogRequest("localhost", "blog", string.Empty);
            MockRepository mocks = new MockRepository();

            VirtualPathProvider pathProvider = GetTemplatesPathProviderMock(mocks);
            mocks.ReplayAll();

            SkinTemplateCollection templates = new SkinTemplateCollection(pathProvider);
            ScriptElementCollectionRenderer renderer = new ScriptElementCollectionRenderer(templates);
            string scriptElements = renderer.RenderScriptElementCollection("RedBook-Blue.css");

            Console.WriteLine(scriptElements);

            string script = @"<script type=""text/javascript"" src=""/Skins/RedBook/js.axd?name=RedBook-Blue.css""></script>";
            Assert.IsTrue(scriptElements.IndexOf(script) > -1, "Rendered the script improperly.");
        }

        [Test]
        public void SkinsWithNoScriptsAreNotMerged()
        {
            MockRepository mocks = new MockRepository();

            VirtualPathProvider pathProvider = GetTemplatesPathProviderMock(mocks);
            mocks.ReplayAll();

            SkinTemplateCollection templates = new SkinTemplateCollection(pathProvider);
            SkinTemplate template = templates.GetTemplate("Gradient");
            bool canBeMerged = ScriptElementCollectionRenderer.CanScriptsBeMerged(template);

            Assert.IsFalse(canBeMerged, "Skins without scripts should not be mergeable.");
        }

        [Test]
        public void ScriptsWithRemoteSrcAreNotMerged()
        {
            MockRepository mocks = new MockRepository();

            VirtualPathProvider pathProvider = GetTemplatesPathProviderMock(mocks);
            mocks.ReplayAll();

            SkinTemplateCollection templates = new SkinTemplateCollection(pathProvider);
            SkinTemplate template = templates.GetTemplate("RedBook-Red.css");
            bool canBeMerged = ScriptElementCollectionRenderer.CanScriptsBeMerged(template);

            Assert.IsFalse(canBeMerged,"Skins with remote scripts should not be mergeable.");
        }

        [Test]
        public void ScriptsWithNoneScriptMergeModeAreNotMerged()
        {
            MockRepository mocks = new MockRepository();

            VirtualPathProvider pathProvider = GetTemplatesPathProviderMock(mocks);
            mocks.ReplayAll();

            SkinTemplateCollection templates = new SkinTemplateCollection(pathProvider);
            SkinTemplate template = templates.GetTemplate("Semagogy");
            bool canBeMerged = ScriptElementCollectionRenderer.CanScriptsBeMerged(template);

            Assert.IsFalse(canBeMerged, "Skins with ScriptMergeMode=\"DontMerge\" should not be mergeable.");
        }

        [Test]
        public void ScriptsWithParametricSrcAreNotMerged()
        {
            MockRepository mocks = new MockRepository();

            VirtualPathProvider pathProvider = GetTemplatesPathProviderMock(mocks);
            mocks.ReplayAll();

            SkinTemplateCollection templates = new SkinTemplateCollection(pathProvider);
            SkinTemplate template = templates.GetTemplate("Piyo");
            bool canBeMerged = ScriptElementCollectionRenderer.CanScriptsBeMerged(template);

            Assert.IsFalse(canBeMerged, "Skins with scripts that have query string parameters should not be mergeable.");
        }

        [RowTest]
        [Row("AnotherEon001", 0)]
        [Row("Colors-Blue.css", 0)]
        [Row("RedBook-Blue.css", 1)]
        [Row("Gradient", 0)]
        [Row("RedBook-Green.css", 0)]
        [Row("KeyWest", 0)]
        [Row("Nature-Leafy.css", 0)]
        [Row("Lightz", 0)]
        [Row("Naked", 0)]
        [Row("Colors", 0)]
        [Row("Origami", 7)]
        [Row("Piyo", 0)]
        [Row("Nature-rain.css", 2)]
        [Row("RedBook-Red.css", 0)]
        [Row("Semagogy", 0)]
        [Row("Submarine", 6)]
        [Row("WPSkin", 0)]
        [Row("Haacked", 0)]
        public void MergedScriptIsCorrect(string skinKey, int expectedStyles)
        {
            UnitTestHelper.SetHttpContextWithBlogRequest("localhost", "blog", string.Empty);
            MockRepository mocks = new MockRepository();

            VirtualPathProvider pathProvider = GetTemplatesPathProviderMock(mocks);
            mocks.ReplayAll();

            SkinTemplateCollection templates = new SkinTemplateCollection(pathProvider);
            ScriptElementCollectionRenderer renderer = new ScriptElementCollectionRenderer(templates);
            int mergedStyles = renderer.GetScriptsToBeMerged(skinKey).Count;

            Assert.AreEqual(expectedStyles, mergedStyles, String.Format("Skin {0} should have {1} merged scripts but found {2}", skinKey, expectedStyles, mergedStyles));
        }


        private static VirtualPathProvider GetTemplatesPathProviderMock(MockRepository mocks)
        {
            VirtualPathProvider pathProvider = (VirtualPathProvider)mocks.CreateMock(typeof(VirtualPathProvider));
            VirtualFile vfile = (VirtualFile)mocks.CreateMock(typeof(VirtualFile), "~/Admin/Skins.config");
            Expect.Call(pathProvider.GetFile("~/Admin/Skins.config")).Return(vfile);
            Expect.Call(pathProvider.FileExists("~/Admin/Skins.User.config")).Return(false);
            SetupResult.For(pathProvider.GetCacheDependency(null, null, DateTime.Now)).IgnoreArguments().Return(null);
            Stream stream = UnitTestHelper.UnpackEmbeddedResource("Skins.Skins.config");
            Expect.Call(vfile.Open()).Return(stream);
            return pathProvider;
        }
    }
}
