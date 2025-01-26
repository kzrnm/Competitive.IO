using System.Diagnostics;
using System.Linq;
using Kzrnm.Competitive.IO;
namespace Shouldly
{
    [DebuggerStepThrough]
    [ShouldlyMethods]
    public static partial class ShouldBeAsciisTestExtensions
    {
        /// <summary>
        /// Perform a string comparison with sensitivity options
        /// </summary>
        public static void ShouldBe(
            this Asciis actual,
            string expected,
            string customMessage = null)
        {
            actual.ToString().ShouldBe(expected, customMessage, 0);
        }

        /// <summary>
        /// Perform a string comparison with sensitivity options
        /// </summary>
        public static void ShouldBe(
            this Asciis[] actual,
            string[] expected,
            string customMessage = null)
        {
            actual.Select(a => a.ToString()).ToArray()
                .ShouldBe(expected, customMessage);
        }
    }
}
