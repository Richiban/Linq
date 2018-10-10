using Xunit;

namespace Richiban.Linq.Tests
{
    public class SplitTests : TestsBase
    {

        [Fact]
        void Split()
        {
            AssertEqual2("Split",
                new[] { 1, 2, 3, 0, 4, 0, 5, 6, 0, 7, 8, 9, 0 }.Split(0),
                new[] { new[] { 1, 2, 3 }, new[] { 4 }, new[] { 5, 6 }, new[] { 7, 8, 9 } });
        }

        [Fact]
        void SplitWhen()
        {
            AssertEqual2("SplitWhen",
                new[] { 1, 2, 3, 0, 4, 0, 5, 6, 0, 7, 8, 9, 0 }.SplitWhen((x, y) => x > y),
                new[] { new[] { 1, 2, 3 }, new[] { 0, 4 }, new[] { 0, 5, 6 }, new[] { 0, 7, 8, 9 } });
        }

    }
}