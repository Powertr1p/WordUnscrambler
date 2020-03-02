using System;
using System.Collections.Generic;
using WordUnscrambler.Workers;
using WordUnscrambler.Data;

namespace WordUnscrambler
{
    class Program
    {
        private static readonly FileReader _fileReader = new FileReader();
        private static readonly WordMatcher _wordMatcher = new WordMatcher();

        static void Main(string[] args)
        {
            try
            {
                bool continueWordUnscrambler = true;
                do
                {
                    Console.WriteLine(Constants.OptionOnHowToEnterScrambleWords);
                    var usetInput = Console.ReadLine() ?? string.Empty;
                    switch (usetInput.ToUpper())
                    {
                        case Constants.File:
                            LoadWordsFromFile();
                            Console.Write(Constants.EnterScrambledWordsViaFile);
                            break;
                        case Constants.Manual:
                            StartGetWordsFromUser();
                            break;
                        default:
                            Console.WriteLine(Constants.EnterScrambledWordsOptionNotRecognized);
                            break;
                    }

                    string continueDecision = string.Empty;
                    do
                    {
                        Console.WriteLine(Constants.OptionOnContinuingTheProgram);
                        continueDecision = Console.ReadLine() ?? string.Empty;
                    } while (!continueDecision.Equals(Constants.Yes, StringComparison.OrdinalIgnoreCase) && !continueDecision.Equals(Constants.No, StringComparison.OrdinalIgnoreCase));

                    continueWordUnscrambler = continueDecision.Equals(Constants.Yes, StringComparison.OrdinalIgnoreCase);

                } while (continueWordUnscrambler);
            }
            catch(Exception ex)
            {
                Console.WriteLine(Constants.ErrorProgramWillBeTerminated + ex.Message);
            }
        }

        private static void LoadWordsFromFile()
        {
            try
            {
                Console.WriteLine(Constants.EnterScrambledWordsViaFile);
                string fileName = Console.ReadLine() ?? string.Empty;
                string[] scrambledWords = _fileReader.Read(fileName);
                DisplayMatchedUnscrambledWords(scrambledWords);
            }
            catch(Exception ex)
            {
                Console.WriteLine(Constants.ErrorScrambledWordsCannotBeLoadded + ex.Message);
            }
        }

        private static void DisplayMatchedUnscrambledWords(string[] scrambledWords)
        {
            string[] wordList = _fileReader.Read(Constants.WordListFileName);

            List<MatchedWord> matchedWords = _wordMatcher.Match(scrambledWords, wordList);

            if (matchedWords.Count > 0)
                foreach (var word in matchedWords)
                    Console.WriteLine(Constants.MatchFound, word.ScrambleWord, word.Word);
            else
                Console.WriteLine(Constants.MatchNotFound);
        }

        private static void StartGetWordsFromUser()
        {
            Console.WriteLine(Constants.EnterScrambledWordsManually);
            string manualInput = Console.ReadLine() ?? string.Empty;
            string[] scrambledWords = manualInput.Split(',');
            DisplayMatchedUnscrambledWords(scrambledWords);
        }
    }
}
