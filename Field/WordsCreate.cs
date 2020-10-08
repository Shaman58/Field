using System;
using System.Collections.Generic;
using System.Text;

namespace Field
{
    class WordsCreate
    {
        private WordsAndColors[] _words = new WordsAndColors[]
            {
                new WordsAndColors("соска", ConsoleColor.Red),
                new WordsAndColors("нянька", ConsoleColor.Green),
                new WordsAndColors("глушитель", ConsoleColor.Blue),
                new WordsAndColors("затычка", ConsoleColor.DarkYellow),
                new WordsAndColors("херзнаетчоуже", ConsoleColor.Cyan)
            };

        public WordsAndColors[] Words => _words;
    }
}
