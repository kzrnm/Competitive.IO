using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace Kzrnm.Competitive.IO
{
    public class SourceExpanderTest
    {
        [Fact]
        public async Task Embedded()
        {
            var embedded = await SourceExpander.EmbeddedData.LoadFromAssembly(typeof(ConsoleReader));
            embedded.AssemblyMetadatas.Should().NotContainKey("SourceExpander.EmbeddedAllowUnsafe");
            embedded.AssemblyMetadatas.Should().ContainKey("SourceExpander.EmbedderVersion");
            embedded.AssemblyMetadatas.Keys.Should().ContainSingle(key => key.StartsWith("SourceExpander.EmbeddedSourceCode"));
            embedded.EmbeddedNamespaces.Should().BeEquivalentTo("Kzrnm.Competitive.IO");
            embedded.SourceFiles.SelectMany(s => s.TypeNames)
                .Should().Contain(
                    "Kzrnm.Competitive.IO.ConsoleReader",
                    "Kzrnm.Competitive.IO.RepeatReader",
                    "Kzrnm.Competitive.IO.SplitReader",
                    "Kzrnm.Competitive.IO.ConsoleWriter",
                    "Kzrnm.Competitive.IO.ConsoleWriter",
                    "Kzrnm.Competitive.IO.PropertyConsoleReader",
                    "Kzrnm.Competitive.IO.PropertyRepeatReader",
                    "Kzrnm.Competitive.IO.PropertySplitReader"
                );

            embedded.SourceFiles.Select(s => s.CodeBody).Should()
                .NotContain(
                    "SuppressMessage",
                    "EditorBrowsable"
                );
        }
    }
}
