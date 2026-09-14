using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using TUnit.Assertions.Core;
using TUnit.Assertions.Sources;

namespace Kzrnm.Competitive.IO;

[DebuggerStepThrough]
public static partial class TUnitExtensions
{
    extension(Assert)
    {
        /// <summary>
        /// Creates an assertion for an immediate value.
        /// Example: await Assert.That(42).IsEqualTo(42);
        /// </summary>
        public static ValueAssertion<Asciis> That(
            Asciis value,
            [CallerArgumentExpression(nameof(value))] string expression = null)
        {
            return new ValueAssertion<Asciis>(value, expression);
        }
    }

    public static TUnit.Assertions.Conditions.IsEquivalentToAssertion<TCollection, T> IsStrictlyEquivalentTo<TCollection, T>(this IAssertionSource<TCollection> source, IEnumerable<T> expected, [CallerArgumentExpression(nameof(expected))] string expression = null)
        where TCollection : IEnumerable<T>
         => source.IsEquivalentTo(expected, TUnit.Assertions.Enums.CollectionOrdering.Matching,
#pragma warning disable TUnitAssertions0003 // Manual CallerArgumentExpression parameter provided
             expression
#pragma warning restore TUnitAssertions0003 // Manual CallerArgumentExpression parameter provided
             );
    public static TUnit.Assertions.Conditions.IsEquivalentToAssertion<TCollection, Ascii> IsStrictlyEquivalentTo<TCollection>(this IAssertionSource<TCollection> source, string expected, [CallerArgumentExpression(nameof(expected))] string expression = null)
        where TCollection : IEnumerable<Ascii>
         => source.IsEquivalentTo(FromString(expected), TUnit.Assertions.Enums.CollectionOrdering.Matching,
#pragma warning disable TUnitAssertions0003 // Manual CallerArgumentExpression parameter provided
             expression
#pragma warning restore TUnitAssertions0003 // Manual CallerArgumentExpression parameter provided
             );

    public static TUnit.Assertions.Conditions.IsEquivalentToAssertion<TCollection, Asciis> IsStrictlyEquivalentTo<TCollection>(this IAssertionSource<TCollection> source, IEnumerable<string> expected, [CallerArgumentExpression(nameof(expected))] string expression = null)
        where TCollection : IEnumerable<Asciis>
         => source.IsEquivalentTo(expected.Select(FromString), TUnit.Assertions.Enums.CollectionOrdering.Matching,
#pragma warning disable TUnitAssertions0003 // Manual CallerArgumentExpression parameter provided
             expression
#pragma warning restore TUnitAssertions0003 // Manual CallerArgumentExpression parameter provided
             );

    static Asciis FromString(string s) => new([.. s.Select(c => (byte)c)]);
}
