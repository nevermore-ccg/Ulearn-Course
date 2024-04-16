using NUnit.Framework;
using System.Text;

namespace TableParser
{
    [TestFixture]
    public class QuotedFieldTaskTests
    {
        [TestCase("''", 0, "", 2)]
        [TestCase("'a'", 0, "a", 3)]
        [TestCase("\"abc\"", 0, "abc", 5)]
        [TestCase("b \"a'\"", 2, "a'", 4)]
        [TestCase("'a'b", 0, "a", 3)]
        [TestCase("a'b'", 1, "b", 3)]
        [TestCase(@"'a\' b'", 0, "a' b", 7)]
        public void Test(string line, int startIndex, string expectedValue, int expectedLength)
        {
            var actualToken = QuotedFieldTask.ReadQuotedField(line, startIndex);
            Assert.AreEqual(new Token(expectedValue, startIndex, expectedLength), actualToken);
        }
    }

    class QuotedFieldTask
    {
        public static Token ReadQuotedField(string line, int startIndex)
        {
            var builder = new StringBuilder();
            int length = line.Length - startIndex;
            char firstSymbol = line[startIndex];
            int countSymbols = 1;
            for (int i = startIndex + 1; i < line.Length; i++)
            {
                if (line[i] == '\\')
                {
                    builder.Append(line[i + 1]);
                    i++;
                    countSymbols++;
                }
                else if (line[i] == firstSymbol)
                {
                    countSymbols++;
                    break;
                }
                else
                    builder.Append(line[i]);
            }
            return new Token(builder.ToString(), startIndex, builder.Length + countSymbols);
        }
    }
}