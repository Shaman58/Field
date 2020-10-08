using System;

namespace Field
{
    class WordsAndColors
    {
        private string _word;
        private ConsoleColor _consoleColor;

        public WordsAndColors(string word, ConsoleColor consoleColor)
        {
            Word = word;
            ConsoleColor = consoleColor;
        }

        public string Word { get => _word; set => _word = value; }
        public ConsoleColor ConsoleColor { get => _consoleColor; set => _consoleColor = value; }
    }
}