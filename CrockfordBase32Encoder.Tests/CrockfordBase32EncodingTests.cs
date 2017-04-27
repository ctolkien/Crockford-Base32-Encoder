using System;
using System.Linq;
using Xunit;

namespace CrockfordBaseEncoder.Tests
{
    public class CrockfordBase32EncodingTests
    {
        [Fact]
        public void CrockfordBase32Encoding_SplitInto5BitChunks_ShouldReturnASingleChunkFor0()
        {
            const int input = 0;
            var expected = new byte[] { 0 };
            var actual = CrockfordBase32Encoding.SplitInto5BitChunks(input);
            Assert.Equal(expected, actual.ToArray());
        }

        [Fact]
        public void CrockfordBase32Encoding_SplitInto5BitChunks_ShouldNotChunkANumberThatFitsIn5Bits()
        {
            const int input = 31;
            var expected = new byte[] { 31 };
            var actual = CrockfordBase32Encoding.SplitInto5BitChunks(input);
            Assert.Equal(expected, actual.ToArray());
        }

        [Fact]
        public void CrockfordBase32Encoding_SplitInto5BitChunks_ShouldChunkANumberThatFitsIn6Bits()
        {
            const int input = 32;
            var expected = new byte[] { 1, 0 };
            var actual = CrockfordBase32Encoding.SplitInto5BitChunks(input);
            Assert.Equal(expected, actual.ToArray());
        }

        [Fact]
        public void CrockfordBase32Encoding_SplitInto5BitChunks_ShouldChunkANumberThatFitsIn13Bits()
        {
            const int input = 4546;
            var expected = new byte[] { 4, 14, 2 };
            var actual = CrockfordBase32Encoding.SplitInto5BitChunks(input);
            Assert.Equal(expected, actual.ToArray());
        }

        [Theory]
        [MemberData("TestRows", MemberType = typeof(TestData))]
        public void CrockfordBase32Encoding_Encode_ShouldReturnExpectedResult(ulong number, string encodedString, string checkDigit)
        {
            var actual = new CrockfordBase32Encoding().Encode(number, false);

            Assert.Equal(encodedString, actual);
        }

        [Theory]
        [MemberData("TestRows", MemberType = typeof(TestData))]
        public void CrockfordBase32Encoding_Encode_ShouldReturnExpectedResultWithCheckDigit(ulong number, string encodedString, string checkDigit)
        {
            var actual = new CrockfordBase32Encoding().Encode(number, true);

            Assert.Equal(encodedString + checkDigit, actual);
        }

        [Fact]
        public void CrockfordBase32Encoding_Decode_ShouldThrowArgumentNullExceptionForNullInput()
        {
            Assert.Throws<ArgumentNullException>(() => new CrockfordBase32Encoding().Decode(null, false));
        }

        [Fact]
        public void CrockfordBase32Encoding_Decode_ShouldReturnNullForBadCharacter()
        {
            Assert.Null(new CrockfordBase32Encoding().Decode("/", false));
        }

        [Fact]
        public void CrockfordBase32Encoding_Decode_ShouldReturnNullForBadCharacterWithinValidOtherwiseInput()
        {
            Assert.Null(new CrockfordBase32Encoding().Decode("a/b", false));
        }

        [Fact]
        public void CrockfordBase32Encoding_Decode_ShouldReturnNullForEmptyString()
        {
            Assert.Null(new CrockfordBase32Encoding().Decode(string.Empty, false));
        }

        [Theory]
        [MemberData("TestRows", MemberType = typeof(TestData))]
        public void CrockfordBase32Encoding_Decode_ShouldReturnExpectedResult(ulong number, string encodedString, string checkDigit)
        {
            var actual = new CrockfordBase32Encoding().Decode(encodedString, false);

            Assert.Equal(number, actual);
        }

        [Theory]
        [MemberData("TestRows", MemberType = typeof(TestData))]
        public void CrockfordBase32Encoding_Decode_ShouldReturnExpectedResultWithValidCheckDigit(ulong number, string encodedString, string checkDigit)
        {
            var actual = new CrockfordBase32Encoding().Decode(encodedString + checkDigit, true);

            Assert.Equal(number, actual);
        }

        [Theory]
        [MemberData("TestRows", MemberType = typeof(TestData))]
        public void CrockfordBase32Encoding_Decode_ShouldReturnNullForInvalidCheckDigit(ulong number, string encodedString, string checkDigit)
        {
            var actual = new CrockfordBase32Encoding().Decode(encodedString + '#', true);

            Assert.Null(actual);
        }

        [Theory]
        [MemberData("TestRows", MemberType = typeof(TestData))]
        public void CrockfordBase32Encoding_Decode_ShouldReturnNullForIncorrectCheckDigit(ulong number, string encodedString, string checkDigit)
        {
            checkDigit = checkDigit.Equals("a", StringComparison.OrdinalIgnoreCase) ? "b" : "a";

            var actual = new CrockfordBase32Encoding().Decode(encodedString + checkDigit, true);

            Assert.Null(actual);
        }
    }
}