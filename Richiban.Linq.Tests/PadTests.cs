using Xunit;

namespace Richiban.Linq.Tests
{
    public class PadTests : TestsBase
    {
        [Fact]
        void Pad()
        {
            AssertEqual("Pad",
                new[] { 1, 2, 3, 4 }.Pad(8, 1),
                new[] { 1, 2, 3, 4, 1, 1, 1, 1 });
        }
        [Fact]
        void Pad2()
        {
            AssertEqual("Pad",
                new[] { 1, 2, 3, 4 }.Pad(2, 1),
                new[] { 1, 2, 3, 4 });
        }
    }
}