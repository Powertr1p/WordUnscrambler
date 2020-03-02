using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WordUnscrambler.Workers;

namespace WordUnscrambler.Test.Unit
{
    [TestClass]
    public class WordMatcherTest
    {
        private readonly WordMatcher _wordMatcher = new WordMatcher();

        [TestMethod]
        public void ScrambledWordMatchesGivenWordInTheList()
        {
            string[] words = { "cat", "more", "chair" };
            string[] scrambledWord = { "rciah" };

            var matchedWords = _wordMatcher.Match(scrambledWord, words);

            Assert.IsTrue(matchedWords.Count == 1);
            Assert.IsTrue(matchedWords[0].ScrambleWord.Equals("rciah"));
            Assert.IsTrue(matchedWords[0].Word.Equals("chair")); 
        }

        [TestMethod]
        public void ScrambledWordMatchesGivenWordsInTheList()
        {
            string[] words = { "cat", "more", "chair" };
            string[] scrambledWords = { "rciah", "act" };

            var matchedWords = _wordMatcher.Match(scrambledWords, words);

            Assert.IsTrue(matchedWords.Count == 2);
            Assert.IsTrue(matchedWords[0].ScrambleWord.Equals("rciah"));
            Assert.IsTrue(matchedWords[0].Word.Equals("chair"));
            Assert.IsTrue(matchedWords[1].ScrambleWord.Equals("act"));
            Assert.IsTrue(matchedWords[1].Word.Equals("cat"));
        }
    }
}
