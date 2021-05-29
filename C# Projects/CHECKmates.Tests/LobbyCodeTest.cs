using NUnit.Framework;
using System;
using System.Text.RegularExpressions;

namespace CHECKmates.Tests
{
    public class LobbyCodeTest
    {
        [Test]
        [TestCase(1)]
        [TestCase(8)]
        [TestCase(15)]
        public void New_Returns_Correct_String_Length(int codeLength)
        {
            string code = LobbyCode.New(codeLength);

            Assert.IsTrue(code.Length == codeLength);
        }

        [Test]
        [TestCase(-1)]
        [TestCase(51)]
        public void New_Throws_With_Invalid_Length(int codeLength)
        {
            Assert.Throws<ArgumentOutOfRangeException>(
                delegate { LobbyCode.New(codeLength); });
        }

        [Test]
        public void New_Returns_Alphanumeric()
        {
            int codeLength = 8;
            string code = LobbyCode.New(codeLength);

            String pattern = "^([a-z]|[A-Z]|[0-9])*$"; //only alphanumeric characters
            Regex regex = new Regex(pattern);

            Assert.True(regex.IsMatch(code));
        }
    }
}