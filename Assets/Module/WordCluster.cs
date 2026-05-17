using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WordCluster
{
    public string Letterbank { get; private set; }
    public string[][] Words { get; private set; }

    public WordCluster(string letterBank, string[][] words)
    {
        Letterbank = letterBank;
        Words = words;
    }

    public static List<WordCluster> GenerateAllCompleteClusters(int banksize, int wordLength, string[] words)
    {
        Dictionary<string, WordCluster> wordClusters = new Dictionary<string, WordCluster>();

        string[] filteredWords = words.Where(x => !Enumerable.Range(0, x.Length).Any(y => x.Substring(0, y).Contains(x[y]))).ToArray();

        Stack<int> selectedLetters = new Stack<int>();
        int lastCounter = 0;
        while (true)
        {
            string currentBank = selectedLetters.Select(x => "ABCDEFGHIJKLMNOPQRSTUVWXYZ"[x]).Join("");
            WordCluster cluster = DivideClusters(currentBank, wordLength, filteredWords);

            if (selectedLetters.Count >= banksize || lastCounter >= 26 || cluster.Words.Any(x => x.Length == 0))
            {
                if (selectedLetters.Count == 0)
                    break;

                if (selectedLetters.Count >= banksize && cluster.Words.All(x => x.Length > 0))
                {
                    wordClusters.Add(currentBank, cluster);
                    Debug.Log(selectedLetters.Join(", "));
                    Debug.Log(cluster);
                }

                Debug.Log(currentBank);
                lastCounter = selectedLetters.Pop() + 1;
                continue;
            }

            selectedLetters.Push(lastCounter++);
        }


        /*
        Stack<int> selectedWords = new Stack<int>();
        int lastCounter = 0;
        while (true)
        {
            string currentBank = GenerateLetterbank(selectedWords.Select(x => filteredWords[x]));
            if (currentBank.Length > banksize || lastCounter >= filteredWords.Length || wordClusters.ContainsKey(currentBank))
            {
                if (selectedWords.Count == 0)
                    break;

                if (!wordClusters.ContainsKey(currentBank) && currentBank.Length == banksize)
                {
                    WordCluster cluster = DivideClusters(currentBank, wordLength, selectedWords.Select(x => filteredWords[x]));
                    wordClusters.Add(currentBank, cluster.Words.All(x => x.Length > 0) ? cluster : new WordCluster("", new string[0][]));
                    if (cluster.Words.All(x => x.Length > 0))
                        Debug.Log(currentBank + ":" + selectedWords.Join(", "));
                }

                lastCounter = selectedWords.Pop() + 1;
                continue;
            }

            selectedWords.Push(lastCounter++);
        }
        */

        return wordClusters.Values.ToList();
    }

    public static WordCluster DivideClusters(string bank, int wordLength, IEnumerable<string> words)
    {
        List<string[]> parts = new List<string[]>();
        Stack<int> selectedLetters = new Stack<int>();
        int lastCounter = 0;
        while (true)
        {
            if (lastCounter >= bank.Length || selectedLetters.Count >= wordLength)
            {
                if (selectedLetters.Count == 0)
                    return new WordCluster(bank, parts.ToArray());

                if (selectedLetters.Count == wordLength)
                    parts.Add(words.Where(x => x.All(y => selectedLetters.Select(z => bank[z]).Contains(y))).ToArray());

                lastCounter = selectedLetters.Pop() + 1;
                continue;
            }

            selectedLetters.Push(lastCounter++);
        }
    }

    public static string GenerateLetterbank(IEnumerable<string> words)
    {
        return "ABCDEFGHIJKLMNOPQRSTUVWXYZ".Where(x => words.Any(y => y.Contains(x))).Join("");
    }

    public override string ToString()
    {
        return Letterbank + ": [" + Words.Select(x => "[" + x.Join(", ") + "]").Join(", ") + "]";
    }
}
