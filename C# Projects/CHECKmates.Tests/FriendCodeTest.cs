using NUnit.Framework;
using System;
using System.Text.RegularExpressions;

namespace CHECKmates.Tests
{
    public class FriendCodeTest
    {
        [Test]
        [TestCase(1)]
        [TestCase(8)]
        [TestCase(15)]
        public void NewPrivateCode_Returns_Correct_String_Length(int length)
        {
            string code = FriendCode.NewPrivateCode(length);

            Assert.IsTrue(code.Length == length);
        }

        [Test]
        [TestCase(-1)]
        [TestCase(51)]
        public void NewPrivateCode_Throws_With_Invalid_Length(int length)
        {
            Assert.Throws<ArgumentOutOfRangeException>(
                delegate { FriendCode.NewPrivateCode(length); });
        }

        [Test]
        public void NewPrivateCode_Returns_Alphanumeric()
        {
            int length = 8;
            string code = FriendCode.NewPrivateCode(length);

            String pattern = "^([a-z]|[A-Z]|[0-9])*"; //only alphanumeric characters
            Regex regex = new Regex(pattern);

            Assert.True(regex.IsMatch(code));
        }

        [Test]
        [TestCase(1, 1)]
        [TestCase(8, 2)]
        [TestCase(15, 1)]
        public void NewPublicCode_Returns_Correct_String_Length(int length, int segmentSize)
        {
            string code = FriendCode.NewPublicCode(length, segmentSize);

            int segCount = (length / segmentSize) - 1;

            Assert.IsTrue(code.Length - segCount == length);
        }

        [Test]
        [TestCase(-1, 1)]
        [TestCase(51, 1)]
        public void NewPublicCode_Throws_With_Invalid_Length(int length, int segmentSize)
        {
            Assert.Throws<ArgumentOutOfRangeException>(
                delegate { FriendCode.NewPublicCode(length, segmentSize); });
        }

        [Test]
        [TestCase(8, 1)]
        [TestCase(9, 1)]
        [TestCase(8, 2)]
        [TestCase(4, 4)]
        [TestCase(8, 4)]
        [TestCase(15, 5)]
        [TestCase(12, 4)]
        [TestCase(32, 8)]
        public void NewPublicCode_Returns_Alphanumeric_With_Equal_Segments(int length, int segmentSize) {
            string code = FriendCode.NewPublicCode(length, segmentSize);

            String pattern = "^([a-z]|[A-Z]|[0-9]|[-])*$"; //only alphanumeric characters and '-'
            Regex regex = new Regex(pattern);

            Assert.True(regex.IsMatch(code));

            string[] segments = code.Split('-');

            foreach(string seg in segments)
            {
                Assert.True(seg.Length == segmentSize);
            }
        }

        [Test]
        [TestCase(5, 4)]
        [TestCase(4, 5)]
        [TestCase(6, 4)]
        public void NewPublicCode_Throws_With_Dissimilar_Segment_Sizes(int length, int segmentSize)
        {
            Assert.Throws<ArgumentException>(
                delegate { FriendCode.NewPublicCode(length, segmentSize); });
        }
    }
}