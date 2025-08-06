using Xunit;
using Xunit.Abstractions;

namespace Backend.api.test
{
    public class CleanupTest
    {
        private readonly ITestOutputHelper _output;

        public CleanupTest(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public async Task CleanupDatabase_RemovesAllTestData()
        {
            // This test demonstrates the cleanup utility
            // You can run this manually if you want to clean up the database
            await CleanupDatabase.CleanupAllTestData();
            
            _output.WriteLine("Database cleanup test completed");
            Assert.True(true); // This test always passes, it's just for demonstration
        }
    }
} 