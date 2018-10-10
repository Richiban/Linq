using Xunit;

namespace Richiban.Linq.Tests
{
    public class PartitionTests : TestsBase
    {
        [Fact]
        void Partition()
        {
            AssertEqual("Partition",
                new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 }.Partition(x => x % 2 == 0),
                (new[] { 2, 4, 6, 8, 10 }, new[] { 1, 3, 5, 7, 9 }));
        }
    }
}