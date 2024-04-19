using ConfigurationReader.Services;
using Xunit;

namespace ConfigurationReader.Tests.ValidatorTests
{
    public class PathValidatorTests
    {
        [Fact]
        public void TestCorrectFilePath()
        {
            Assert.True(Validator.ValidateFilePath(@"c:\folder\file.txt"));
        }

        [Fact]
        public void TestCapitalLattersFilePath()
        {
            Assert.True(Validator.ValidateFilePath(@"C:\FOLDER\FILE.TXT"));
        }

        [Fact]
        public void TestCorrectFilePathWithoutExtension()
        {
            Assert.True(Validator.ValidateFilePath(@"c:\folder\fileWithoutExtension"));
        }

        [Fact]
        public void TestCorrectServerFilePath()
        {
            Assert.True(Validator.ValidateFilePath(@"\\123.123.123.123\share\folder\file.txt"));
        }

        [Fact]
        public void TestIncorrectFilePath()
        {
            Assert.False(Validator.ValidateFilePath(@"c:\folder\"));
        }

        [Fact]
        public void TestEmptyFilePath()
        {
            Assert.False(Validator.ValidateFilePath(""));
        }

        [Fact]
        public void TestNullFilePath()
        {
            Assert.False(Validator.ValidateFilePath(null));
        }
    }
}
