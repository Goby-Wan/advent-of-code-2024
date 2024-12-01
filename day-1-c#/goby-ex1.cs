using System;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    class Day_01
    {
        private readonly string[] _inputOne,
            _inputTwo;
        private List<int> _listOne,
            _listTwo;

        public Day_01()
        {
            _inputOne = File.ReadAllLines("input/day_01/input-ex1.txt");
            _listOne = new List<int>();
            _listTwo = new List<int>();

            Console.WriteLine("Résultat de l'exercice 1 : " + PartOne(_inputOne));
        }

        private int PartOne(string[] input)
        {
            int result = 0;

            ParseInput(input);
            _listOne.Sort();
            _listTwo.Sort();

            for (int i = 0; i < _listOne.Count(); i++)
            {
                result += Int32.Abs(_listOne[i] - _listTwo[i]);
            }

            return result;
        }

        private void ParseInput(string[] input)
        {
            foreach (string line in input)
            {
                string[] lineParsed = Regex.Split(line, "   ");
                _listOne.Add(Int32.Parse(lineParsed[0]));
                _listTwo.Add(Int32.Parse(lineParsed[1]));
            }
        }
    }
}
