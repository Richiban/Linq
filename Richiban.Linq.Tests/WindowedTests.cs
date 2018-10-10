using Xunit;

namespace Richiban.Linq.Tests
{
    public class WindowedTests : TestsBase
    {
        [Fact]
        void Pairwise()
        {
            AssertEqual("Pairwise",
                new[] { 1, 2, 3, 4, 5, 6, 7, 8 }.Pairwise(),
                new[] { (1, 2), (2, 3), (3, 4), (4, 5), (5, 6), (6, 7), (7, 8) });
        }

        [Fact]
        void Tripwise()
        {
            AssertEqual("Tripwise",
                new[] { 1, 2, 3, 4, 5 }.Tripwise(),
                new[] { (1, 2, 3), (2, 3, 4), (3, 4, 5) });
        }

        [Fact]
        void Windowed()
        {
            AssertEqual2("Windowed",
                new[] { 1, 2, 3, 4, 5, 6 }.Windowed(4),
                new[] { new[] { 1, 2, 3, 4 }, new[] { 2, 3, 4, 5 }, new[] { 3, 4, 5, 6 } });
        }
    }
}