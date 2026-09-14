using System.Linq;
using System.Threading.Tasks;

namespace Kzrnm.Competitive.IO
{
    public class SourceExpanderTest
    {
        [Test]
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
#elif NET10_0
                "14.0"
#endif
                ;

            var embedded = await SourceExpander.EmbeddedData.LoadFromAssembly(typeof(ConsoleReader));
            await Assert.That(embedded.EmbeddedLanguageVersion).IsEqualTo(expectedEmbeddedLanguageVersion);
            await Assert.That(embedded.AssemblyMetadatas).DoesNotContainKey("SourceExpander.EmbeddedAllowUnsafe");
            await Assert.That(embedded.AssemblyMetadatas).ContainsKey("SourceExpander.EmbedderVersion");
            await Assert.That(embedded.AssemblyMetadatas.Keys.Where(key => key.StartsWith("SourceExpander.EmbeddedSourceCode"))).HasSingleItem();
            await Assert.That(embedded.EmbeddedNamespaces).IsNotStrictlyEqualTo(["Kzrnm.Competitive.IO"]);
            await Assert.That(embedded.SourceFiles.SelectMany(s => s.TypeNames))
                .Contains("Kzrnm.Competitive.IO.ConsoleReader")
                .And.Contains("Kzrnm.Competitive.IO.RepeatReader")
                .And.Contains("Kzrnm.Competitive.IO.ConsoleWriter")
                .And.Contains("Kzrnm.Competitive.IO.ConsoleWriter")
                .And.Contains("Kzrnm.Competitive.IO.PropertyConsoleReader")
                .And.Contains("Kzrnm.Competitive.IO.PropertyRepeatReader");

            await Assert.That(embedded.SourceFiles.Select(s => s.CodeBody))
                .DoesNotContain(b => b.Contains("SuppressMessage"))
                .And.DoesNotContain(b => b.Contains("EditorBrowsable"));
        }
    }
}
