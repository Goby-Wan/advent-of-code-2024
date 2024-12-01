using System;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    class Day_01
    {
        private readonly string[] _input;
        private List<int> _listOne,
            _listTwo;

        public Day_01()
        {
            _input = File.ReadAllLines("input/day_01/input-ex1.txt");
            _listOne = new List<int>();
            _listTwo = new List<int>();

            ParseInput(_input);
            _listOne.Sort();
            _listTwo.Sort();

            Console.WriteLine("Résultat de la partie 1 : " + PartOne());
            Console.WriteLine("Résultat de la partie 2 : " + PartTwo());
        }

        private int PartOne()
        {
            int result = 0;

            for (int i = 0; i < _listOne.Count(); i++)
            {
                result += Int32.Abs(_listOne[i] - _listTwo[i]);
            }

            return result;
        }

        private int PartTwo()
        {
            int result = 0;

            Dictionary<int, int> groupedValues = _listTwo
                .GroupBy(i => i)
                .ToDictionary(x => x.Key, x => x.Count());
            foreach (int i in _listOne)
            {
                result += i * groupedValues.GetValueOrDefault(i, 0);
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
