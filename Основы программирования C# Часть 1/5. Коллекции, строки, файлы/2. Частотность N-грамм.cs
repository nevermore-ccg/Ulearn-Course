namespace TextAnalysis
{
    static class FrequencyAnalysisTask
    {
        public static Dictionary<string, string> GetMostFrequentNextWords(List<List<string>> text)
        {
            var result = new Dictionary<string, string>();
            if (text != null && text[0].Count > 1)
            {
                var countDictionary = CreateCountDictionary(text);
                result = AnalyseDictionary(countDictionary);
            }
            return result;
        }

        public static Dictionary<string, Dictionary<string, int>> CreateCountDictionary(List<List<string>> text)
        {
            var countDictionary = new Dictionary<string, Dictionary<string, int>>();
            foreach (var wordsList in text)
            {
                for (int i = 0; i < wordsList.Count; i++)
                {
                    CreateBigram(countDictionary, wordsList, i);
                    CreateTrigram(countDictionary, wordsList, i);
                }
            }
            return countDictionary;
        }

        private static void CreateBigram(Dictionary<string, Dictionary<string, int>> countDictionary,
            List<string> wordsList, int i)
        {
            if (i + 1 < wordsList.Count)
            {
                if (!countDictionary.ContainsKey(wordsList[i]))
                {
                    countDictionary[wordsList[i]] = new Dictionary<string, int>
                            {
                                { wordsList[i + 1], 1 }
                            };
                }
                else if (!countDictionary[wordsList[i]].ContainsKey(wordsList[i + 1]))
                {
                    countDictionary[wordsList[i]][wordsList[i + 1]] = 1;
                }
                else
                    countDictionary[wordsList[i]][wordsList[i + 1]]++;
            }
        }

        private static void CreateTrigram(Dictionary<string, Dictionary<string, int>> countDictionary,
            List<string> wordsList, int i)
        {
            if (i + 2 < wordsList.Count)
            {
                if (!countDictionary.ContainsKey($"{wordsList[i]} {wordsList[i + 1]}"))
                {
                    countDictionary[$"{wordsList[i]} {wordsList[i + 1]}"] = new Dictionary<string, int>
                            {
                                { wordsList[i + 2], 1 }
                            };
                }
                else if (!countDictionary[$"{wordsList[i]} {wordsList[i + 1]}"].ContainsKey(wordsList[i + 2]))
                {
                    countDictionary[$"{wordsList[i]} {wordsList[i + 1]}"][wordsList[i + 2]] = 1;
                }
                else
                    countDictionary[$"{wordsList[i]} {wordsList[i + 1]}"][wordsList[i + 2]]++;
            }
        }

        public static Dictionary<string, string> AnalyseDictionary(Dictionary<string, Dictionary<string, int>> countDictionary)
        {
            var result = new Dictionary<string, string>();
            foreach (var pair in countDictionary)
            {
                int max = 0;
                string key = null;
                foreach (var item in pair.Value)
                {
                    if (item.Value == max)
                    {
                        int i = string.CompareOrdinal(item.Key, key);
                        if (i < 0)
                            key = item.Key;
                    }
                    if (item.Value > max)
                    {
                        max = item.Value;
                        key = item.Key;
                    }
                }
                result.Add(pair.Key, key);
            }
            return result;
        }
    }
}