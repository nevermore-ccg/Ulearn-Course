using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace TableParser
{
    [TestFixture]
    public class FieldParserTaskTests
    {
        public static void Test(string input, string[] expectedResult)
        {
            var actualResult = FieldsParserTask.ParseLine(input);
            Assert.AreEqual(expectedResult.Length, actualResult.Count);
            for (int i = 0; i < expectedResult.Length; ++i)
            {
                Assert.AreEqual(expectedResult[i], actualResult[i].Value);
            }
        }

        [TestCase("text", new[] { "text" })]
        [TestCase("hello world", new[] { "hello", "world" })]
        [TestCase("\"a b\"", new[] { "a b" })]
        [TestCase("\"a 'b'\"", new[] { "a 'b'" })]
        [TestCase("'a \"b\"'", new[] { "a \"b\"" })]
        [TestCase("'ab''cd'", new[] { "ab", "cd" })]
        [TestCase("''", new[] { "" })]
        [TestCase("'dsd", new[] { "dsd" })]
        [TestCase(@"'abc\''", new[] { "abc'" })]
        [TestCase(@"""abc\""""", new[] { "abc\"" })]
        [TestCase(" 'a' ", new[] { "a" })]
        [TestCase("ab'ab'", new[] { "ab", "ab" })]
        [TestCase("'av'c", new[] { "av", "c" })]
        [TestCase("hello   world", new[] { "hello", "world" })]
        [TestCase(@"'\\s'", new[] { "\\s" })]
        [TestCase(@"'\\'", new[] { "\\" })]
        [TestCase("\"s d ", new[] { "s d " })]
        [TestCase("", new string[0])]
        public static void RunTests(string input, string[] expectedOutput)
        {
            Test(input, expectedOutput);
        }
    }

    public class FieldsParserTask
    {
        public static List<Token> ParseLine(string line)
        {
            var tokens = new List<Token>();
            for (int i = 0; i < line.Length;)
            {
                i = FillList(line, tokens, i);
            }
            return tokens;
        }

        public static int FillList(string line, List<Token> tokens, int startIndex)
        {
            int result = startIndex + 1;
            if (line[startIndex] == '\'' || line[startIndex] == '\"')
            {
                tokens.Add(ReadQuotedField(line, startIndex));
                result = tokens.Last().Length + startIndex;
            }
            else if (!char.IsWhiteSpace(line[startIndex]))
            {
                tokens.Add(ReadField(line, startIndex));
                result = tokens.Last().Length + startIndex;
            }
            return result;
        }

        private static Token ReadField(string line, int startIndex)
        {
            var builder = new StringBuilder();
            for (int i = startIndex; i < line.Length; i++)
            {
                if (char.IsWhiteSpace(line[i]) || line[i] == '\'' || line[i] == '\"')
                    break;
                else
                    builder.Append(line[i]);
            }
            return new Token(builder.ToString(), startIndex, builder.Length);
        }

        public static Token ReadQuotedField(string line, int startIndex)
        {
            return QuotedFieldTask.ReadQuotedField(line, startIndex);
        }
    }
}
