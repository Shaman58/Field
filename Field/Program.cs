using Field;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;


namespace algoritm
{
    class Program
    {
        static void Main()
        {
            int xField = 7;
            int yField = 7;
            FieldItem[,] field = new FieldItem[xField, yField];

            WordsCreate wordsCreate = new WordsCreate();
            WordsAndColors[] words = wordsCreate.Words;


            FeelRandomCharsArrayInField(field);
            FeelingArrayWordsInField(field, words);
            PrintField(field);
            Console.WriteLine("DONE!");
        }


        /// <summary>
        /// try feel field 
        /// </summary>
        /// <param name="field"></param>
        /// <param name="words"></param>
        private static void FeelingArrayWordsInField(FieldItem[,] field, WordsAndColors[] words)
        {
            while (!FeelingWordsInField(field, words))
            {
                FeelRandomCharsArrayInField(field);
                Console.WriteLine("one more try");
            }
        }

        /// <summary>
        /// feel random chars to field
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        static FieldItem[,] FeelRandomCharsArrayInField(FieldItem[,] field)
        {
            Random random = new Random();
            char[] letters;
            letters = ReturnLocalAlphabet();

            for (int i = 0; i < field.GetLength(0); i++)
            {
                for (int j = 0; j < field.GetLength(1); j++)
                {
                    field[i, j] = new FieldItem
                    {
                        Letter = letters[random.Next(letters.Length)],
                        ConsoleColor = ConsoleColor.White,
                        XCord = i,
                        YCord = j
                    };
                }
            }
            return field;
        }

        /// <summary>
        /// Localiztion
        /// </summary>
        /// <returns></returns>
        static char[] ReturnLocalAlphabet()
        {
            CultureInfo cultureInfo = CultureInfo.CurrentCulture;
            char[] letters;

            switch (cultureInfo.TwoLetterISOLanguageName)
            {
                case "ru":
                    letters = Enumerable.Range('а', 'я' - 'а' + 1).Select(a => (char)a).ToArray();
                    break;
                case "en":
                    letters = Enumerable.Range('a', 'z' - 'a' + 1).Select(a => (char)a).ToArray();
                    break;
                default:
                    letters = Enumerable.Range('a', 'z' - 'a' + 1).Select(a => (char)a).ToArray();
                    break;
            }
            return letters;
        }

        /// <summary>
        /// print any field
        /// </summary>
        /// <param name="field"></param>
        private static void PrintField(FieldItem[,] field)
        {
            for (int i = 0; i < field.GetLength(0); i++)
            {
                for (int j = 0; j < field.GetLength(1); j++)
                {
                    Console.ForegroundColor = field[i, j].ConsoleColor;
                    Console.Write("{0}  ", field[i, j].Letter);
                }
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine();
            }
        }

        private static bool FeelingWordsInField(FieldItem[,] field, WordsAndColors[] words)
        {
            FieldItem[,] saveField = new FieldItem[field.GetLength(0), field.GetLength(1)];

            for (int o = 0; o < words.Length; o++)
            {                
                ArrayCopy(field, saveField);
                for (int i = 0; i < 10; i++)
                {
                    Console.WriteLine("попытка засунуть слово {0}    N {1}", words[o].Word, i);
                    if (TryFeelOneWordInFieldUp(field, words[o]))                    
                        break;

                    Console.WriteLine("попытка засунуть слово {0} не удалась", words[o].Word);

                    ArrayCopy(saveField, field);                    
                }
            }            
            return false;
        }


        /// <summary>
        /// copy DATA(not references) from source array to target array
        /// </summary>        
        private static FieldItem[,] ArrayCopy(FieldItem[,] sourceArray, FieldItem[,] targetArray)
        {
            for (int i = 0; i < sourceArray.GetLength(0); i++)
            {
                for (int j = 0; j < sourceArray.GetLength(1); j++)
                {
                    targetArray[i, j] = new FieldItem
                    {
                        Letter = sourceArray[i, j].Letter,
                        ConsoleColor = sourceArray[i, j].ConsoleColor,
                        XCord = sourceArray[i, j].XCord,
                        YCord = sourceArray[i, j].YCord

                    };
                }
            }
            return targetArray;
        }

        private static bool TryFeelOneWordInFieldUp(FieldItem[,] field, WordsAndColors word)
        {
            FieldItem nowFieldItem = new FieldItem();

            //ArrayCopy(field, saveField);

            for (int i = 0; i < word.Word.Length; i++)
            {
                Console.WriteLine("попытка засунуть {0} букву", i);
                nowFieldItem = SearchFreeFieldOrReturnNullIfEmpty(field, nowFieldItem, i == 0);
                if (nowFieldItem == null)
                    return false;

                nowFieldItem.Letter = word.Word.ToCharArray()[i];
                nowFieldItem.ConsoleColor = word.ConsoleColor;
                PrintField(field);
                Console.WriteLine();
            }
            return true;
        }

        private static FieldItem SearchFreeFieldOrReturnNullIfEmpty(FieldItem[,] field, FieldItem fieldItem, bool isFirstChar)
        {

            Random random = new Random();
            List<FieldItem> listOfFieldsFreeNeighbours = new List<FieldItem>();
            FieldItem targetFreeField;

            if (!isFirstChar)
            {                
                listOfFieldsFreeNeighbours = GenerateListOfFreeNaighbours(field, fieldItem);

                if (listOfFieldsFreeNeighbours.Count != 0)
                    targetFreeField = RandomFieldItem(listOfFieldsFreeNeighbours);
                else targetFreeField = null; // говнокод. Налл возвращать нельзя.                
            }
            else
                targetFreeField = RandomFieldItem(field);           

            return targetFreeField;
        }


        /// <summary>
        /// returns List of FieldItems what are white neighbours
        /// </summary>
        /// <param name="field"></param>
        /// <param name="fieldItem"></param>
        /// <returns></returns>
        private static List<FieldItem> GenerateListOfFreeNaighbours(FieldItem[,] field, FieldItem fieldItem)
        {
            List<FieldItem> listOfFieldsFreeNeighbours = new List<FieldItem>();
            if ((fieldItem.XCord - 1) >= 0 &&
                    field[fieldItem.XCord - 1, fieldItem.YCord].ConsoleColor == ConsoleColor.White)
                listOfFieldsFreeNeighbours.Add(field[fieldItem.XCord - 1, fieldItem.YCord]);
            if ((fieldItem.XCord + 1) < field.GetLength(0) &&
                field[fieldItem.XCord + 1, fieldItem.YCord].ConsoleColor == ConsoleColor.White)
                listOfFieldsFreeNeighbours.Add(field[fieldItem.XCord + 1, fieldItem.YCord]);
            if ((fieldItem.YCord - 1) >= 0 &&
                field[fieldItem.XCord, fieldItem.YCord - 1].ConsoleColor == ConsoleColor.White)
                listOfFieldsFreeNeighbours.Add(field[fieldItem.XCord, fieldItem.YCord - 1]);
            if ((fieldItem.YCord + 1) < field.GetLength(1) &&
                field[fieldItem.XCord, fieldItem.YCord + 1].ConsoleColor == ConsoleColor.White)
                listOfFieldsFreeNeighbours.Add(field[fieldItem.XCord, fieldItem.YCord + 1]);
            return listOfFieldsFreeNeighbours;
        }



        /// <summary>
        /// return random FieldItem in income array of FieldItems or list of FieldItems
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>

        private static FieldItem RandomFieldItem(FieldItem[,] field)
        {
            List<FieldItem> freeFieldItems = new List<FieldItem>();
            Random random = new Random();            
            foreach (var item in field)
            {
                if (item.ConsoleColor == ConsoleColor.White)
                {
                    freeFieldItems.Add(item);
                }
            }
            return freeFieldItems[random.Next(freeFieldItems.Count)];
        }
        private static FieldItem RandomFieldItem(List<FieldItem> field)
        {
            Random random = new Random();            
            return field[random.Next(field.Count)];
        }

    }

}
