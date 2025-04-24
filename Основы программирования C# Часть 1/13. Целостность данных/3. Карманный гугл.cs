using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PocketGoogle;

public class Indexer : IIndexer
{
    private Dictionary<string, Dictionary<int, List<int>>> _library;
    private char[] _wordsSeparators = new char[] { ' ', '.', ',', '!', '?', ':', '-', '\r', '\n' };

    public Indexer()
    {
        _library = new Dictionary<string, Dictionary<int, List<int>>>();
    }

    public void Add(int id, string documentText)
    {
        var wordBuilder = new StringBuilder();
        var startIndex = 0;
        for (int i = 0; i < documentText.Length; i++)
        {
            if (!_wordsSeparators.Contains(documentText[i]))
            {
                wordBuilder.Append(documentText[i]);
                if (wordBuilder.Length == 1)
                    startIndex = i;
            }
            else if (wordBuilder.Length > 0)
                UpdateLibrary(id, wordBuilder, ref startIndex);
            if (documentText.Length == i + 1)
                UpdateLibrary(id, wordBuilder, ref startIndex);
        }
    }

    private void UpdateLibrary(int id, StringBuilder wordBuilder, ref int startIndex)
    {
        var word = wordBuilder.ToString();
        if (!_library.ContainsKey(word))
            _library.Add(word, new Dictionary<int, List<int>>());
        if (!_library[word].ContainsKey(id))
            _library[word].Add(id, new List<int>());
        _library[word][id].Add(startIndex);
        wordBuilder.Clear();
        startIndex = 0;
    }

    public List<int> GetIds(string word)
    {
        var result = new List<int>();
        if (_library.ContainsKey(word))
        {
            result = _library[word].Keys.ToList();
        }
        return result;
    }

    public List<int> GetPositions(int id, string word)
    {
        var result = new List<int>();
        if (_library.ContainsKey(word) && _library[word].ContainsKey(id))
        {
            result = _library[word][id];
        }
        return result;
    }

    public void Remove(int id)
    {
        foreach (var word in _library.Keys)
        {
            if (_library[word].ContainsKey(id))
                _library[word].Remove(id);
        }
    }
}