using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Test_Taste_Console_Application.Utilities
{
    // This is a static helper class designed to create formatted console output with consistent table layouts.
    // It provides methods for building text-based tables with proper alignment and borders.
    public static class ConsoleWriter
    {
        // This method creates a line of dashes and plus signs based on the provided column sizes.
        public static void CreateLine(int[] columnSizes)
        {
            /*
             This is an example of what the function can create:
             --------------------+--------------------+--------------------
            */
            var stringBuilder = new StringBuilder();

            for (var i = 0; i < columnSizes.Length; i++)
            {
                stringBuilder.Append(string.Concat(Enumerable.Repeat('-', columnSizes[i])));
                if (i != columnSizes.Length - 1)
                {
                    stringBuilder.Append('+');
                }
            }

            Console.WriteLine(stringBuilder.ToString());
            stringBuilder.Clear();
        }
        // This method creates a formatted text line based on the provided column labels and sizes.
        public static void CreateText(string[] columnLabels, int[] columnSizes)
        {
            /*
             This is an example of what the function can create:
             Moon's Number       |Moon's Id           |Moon's Average Temperature
            */
            if (!columnLabels.Length.Equals(columnSizes.Length))
            {
                return;
            }

            var result = string.Empty;
            var currentPosition = 0;
            for (var i = 0; i < columnLabels.Length; i++)
            {
                currentPosition += columnSizes[i];
                result += columnLabels[i];
                if (i != columnLabels.Length - 1)
                {
                    result = result.PadRight(currentPosition + i);
                    result += '|';
                }
            }

            Console.WriteLine(result);
        }

        // This method creates a header for the table using the provided column labels and sizes.
        public static void CreateHeader(string[] columnLabels, int[] columnSizes)
        {
            /*
             This is an example of what the function can create:
             --------------------+--------------------+------------------------------
             Moon's Number       |Moon's Id           |Moon's Average Temperature
             --------------------+--------------------+------------------------------
            */
            CreateLine(columnSizes);
            CreateText(columnLabels, columnSizes);
            CreateLine(columnSizes);
        }
        // This method creates empty lines in the console output.
        public static void CreateEmptyLines(int totalEmptyLines)
        {
            //The function can create empty spaces.
            for (var i = 0; i < totalEmptyLines; i++)
            {
                Console.WriteLine();
            }
        }
    }
}
