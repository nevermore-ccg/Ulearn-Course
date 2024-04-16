namespace TextAnalysis
{
    static class TextGeneratorTask
    {
        public static string ContinuePhrase(
            Dictionary<string, string> nextWords,
            string phraseBeginning,
            int wordsCount)
        {
            var splitStartPhrase = phraseBeginning.Split(' ');
            var listPhrases = new List<string>();
            listPhrases.AddRange(splitStartPhrase);
            if (listPhrases.Count > 0)
                for (int i = 0; i < wordsCount; i++)
                {
                    if (listPhrases.Count > 1 
                        && nextWords.ContainsKey($"{listPhrases[listPhrases.Count - 2]} {listPhrases[listPhrases.Count - 1]}"))
                        listPhrases.Add(nextWords[$"{listPhrases[listPhrases.Count - 2]} {listPhrases[listPhrases.Count - 1]}"]);
                    else if (listPhrases.Count > 1 && nextWords.ContainsKey(listPhrases[listPhrases.Count - 1]))
                        listPhrases.Add(nextWords[listPhrases[listPhrases.Count - 1]]);
                    else if (listPhrases.Count == 1 && nextWords.ContainsKey(listPhrases[0]))
                        listPhrases.Add(nextWords[listPhrases[0]]);
                }
            string result = string.Join(" ", listPhrases.ToArray());
            return result;
        }
    }
}