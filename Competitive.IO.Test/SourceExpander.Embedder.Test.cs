using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Kzrnm.Competitive.IO
{
    public class SourceExpanderTest
    {
        [Fact]
        public async Task Embedded()
        {

            const string expectedEmbeddedLanguageVersion =
#if NETFRAMEWORK
                "7.0"
#elif NETCOREAPP3_1
                "7.3"
#elif NET6_0
                "10.0"
#elif NET8_0
                "12.0"
#endif
                ;

            var embedded = await SourceExpander.EmbeddedData.LoadFromAssembly(typeof(ConsoleReader));
            embedded.EmbeddedLanguageVersion.ShouldBe(expectedEmbeddedLanguageVersion);
            embedded.AssemblyMetadatas.ShouldNotContainKey("SourceExpander.EmbeddedAllowUnsafe");
            embedded.AssemblyMetadatas.ShouldContainKey("SourceExpander.EmbedderVersion");
            embedded.AssemblyMetadatas.Keys.Where(key => key.StartsWith("SourceExpander.EmbeddedSourceCode")).ShouldHaveSingleItem();
            embedded.EmbeddedNamespaces.ShouldBe(["Kzrnm.Competitive.IO"]);
            embedded.SourceFiles.SelectMany(s => s.TypeNames).ShouldSatisfyAllConditions([
                t => t.ShouldContain("Kzrnm.Competitive.IO.ConsoleReader"),
                t => t.ShouldContain("Kzrnm.Competitive.IO.RepeatReader"),
                t => t.ShouldContain("Kzrnm.Competitive.IO.ConsoleWriter"),
                t => t.ShouldContain("Kzrnm.Competitive.IO.ConsoleWriter"),
                t => t.ShouldContain("Kzrnm.Competitive.IO.PropertyConsoleReader"),
                t => t.ShouldContain("Kzrnm.Competitive.IO.PropertyRepeatReader"),
            ]);

            embedded.SourceFiles.Select(s => s.CodeBody).ShouldSatisfyAllConditions([
                t => t.ShouldAllBe(s => !s.Contains("SuppressMessage")),
                t => t.ShouldAllBe(s => !s.Contains("EditorBrowsable")),
            ]);
        }
    }
}
