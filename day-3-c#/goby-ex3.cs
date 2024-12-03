using System;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
  class Day_03
  {
    private readonly string _input;
    private List<(int, int)> _listOperations;
    public Day_03()
    {
      _input = File.ReadAllText("input/day_03/input-ex3.txt");
      _listOperations = new List<(int, int)>();
      ParseInput(_input);

      Console.WriteLine("Résultat de la partie 1 : " + PartOne());
      Console.WriteLine("Résultat de la partie 2 : " + PartTwo());
    }

    private int PartOne()
    {
      int result = 0;

      foreach ((int, int) operation in _listOperations)
      {
        result += operation.Item1 * operation.Item2;
      }

      return result;
    }

    private int PartTwo()
    {
      int result = 0;

      return result;
    }


    private void ParseInput(string input)
    {
      MatchCollection lineParsed = Regex.Matches(input, "mul\\((\\d+)\\,(\\d+)\\)");
      foreach (Match line in lineParsed)
      {
        _listOperations.Add((Int32.Parse(line.Groups[1].Value), Int32.Parse(line.Groups[2].Value)));
      }
    }

  }
}
