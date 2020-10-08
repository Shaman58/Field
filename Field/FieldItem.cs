using System;
using System.Collections.Generic;
using System.Text;

namespace Field
{
    class FieldItem
    {
        private char _letter;
        private ConsoleColor _consoleColor = ConsoleColor.White;
        private int _xCord;
        private int _yCord;

        public char Letter { get => _letter; set => _letter = value; }
        public ConsoleColor ConsoleColor { get => _consoleColor; set => _consoleColor = value; }
        public int XCord { get => _xCord; set => _xCord = value; }
        public int YCord { get => _yCord; set => _yCord = value; }
    }
}
