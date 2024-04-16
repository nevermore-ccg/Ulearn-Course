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
