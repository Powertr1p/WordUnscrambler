using System;
using System.Collections.Generic;
using WordUnscrambler.Data;

namespace WordUnscrambler.Workers
{
    class WordMatcher
    {
        internal List<MatchedWord> Match(string[] scrambledWords, string[] wordList)
        {
            List<MatchedWord> matchedWords = new List<MatchedWord>();

            foreach (var scrambledWord in scrambledWords)
            { 
                foreach (var word in wordList)
                { 
                    if (scrambledWord.Equals(word, StringComparison.OrdinalIgnoreCase))
                    { 
                        matchedWords.Add(new MatchedWord { ScrambleWord = scrambledWord, Word = word });
                    }
                    else
                    {
                        char[] scrambledWordArray = scrambledWord.ToCharArray();
                        char[] wordArray = word.ToCharArray();

                        Array.Sort(scrambledWordArray);
                        Array.Sort(wordArray);

                        string sortedScrambleWord = new string(scrambledWordArray);
                        string sortedWord = new string(wordArray);

                        if (sortedScrambleWord.Equals(sortedWord, StringComparison.OrdinalIgnoreCase))
                            matchedWords.Add(new MatchedWord { ScrambleWord = sortedScrambleWord, Word = sortedWord });
                    }
                }
            }
            return matchedWords;
        }
    }
}
