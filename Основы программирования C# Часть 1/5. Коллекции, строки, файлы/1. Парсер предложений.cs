using System.Text;

namespace TextAnalysis
{
    static class SentencesParserTask
    {
        public static List<List<string>> ParseSentences(string text)
        {
            var sentencesList = new List<List<string>>();
            var separators = new char[] { '.', '!', '?', ';', ':', '(', ')' };
            var sentences = text.Split(separators);
            foreach (var sentence in sentences)
            {
                if (sentence.Length > 0)
                {
                    var wordsList = DivideSentences(sentence);
                    if (wordsList.Count > 0)
                        sentencesList.Add(wordsList);
                }
            }
            return sentencesList;
        }

        public static List<string> DivideSentences(string sentence)
        {
            var wordsList = new List<string>();
            var builder = new StringBuilder();
            for (int i = 0; i < sentence.Length; i++)
            {
                if (char.IsLetter(sentence[i]) || sentence[i] == '\'')
                {
                    builder.Append(sentence[i]);
                    if (i + 1 == sentence.Length)
                        wordsList.Add(builder.ToString().ToLower());
                }
                else
                {
                    if (builder.Length > 0)
                        wordsList.Add(builder.ToString().ToLower());
                    builder.Clear();
                }
            }
            return wordsList;
        }
    }
}