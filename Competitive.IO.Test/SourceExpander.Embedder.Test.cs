using System.Linq;
using System.Reflection;
using FluentAssertions;
using Xunit;

namespace Kzrnm.Competitive.IO
{
    public class SourceExpanderTest
    {
        [Fact]
        public void Embedded()
        {
            var metadataDic = typeof(ConsoleReader).Assembly.GetCustomAttributes<AssemblyMetadataAttribute>()
                .ToDictionary(attr => attr.Key, attr => attr.Value);

            metadataDic.Should().NotContainKey("SourceExpander.EmbeddedAllowUnsafe");
            metadataDic.Should().ContainKey("SourceExpander.EmbedderVersion");
            metadataDic.Keys.Should().ContainSingle(key => key.StartsWith("SourceExpander.EmbeddedSourceCode"));

        }
    }
}
