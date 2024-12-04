using System;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
  class Day_03
  {
    private readonly string _input;
    public Day_03()
    {
      _input = File.ReadAllText("input/day_03/input-ex3.txt");

      Console.WriteLine("Résultat de la partie 1 : " + PartOne());
      Console.WriteLine("Résultat de la partie 2 : " + PartTwo());
    }

    private int PartOne()
    {
      int result = 0;

      MatchCollection lineParsed = Regex.Matches(_input, "mul\\((\\d+)\\,(\\d+)\\)");
      foreach (Match line in lineParsed)
      {
        result += Int32.Parse(line.Groups[1].Value) * Int32.Parse(line.Groups[2].Value);
      }

      return result;
    }

    private int PartTwo()
    {
      int result = 0;
      bool doOperation = true;

      MatchCollection lineParsed = Regex.Matches(_input, "mul\\((\\d+)\\,(\\d+)\\)|do\\(\\)|don\\'t\\(\\)");
      foreach (Match line in lineParsed)
      {
        if (line.Groups[0].Value == "do()")
        {
          doOperation = true;
        }
        else if (line.Groups[0].Value == "don't()")
        {
          doOperation = false;
        }
        else if (doOperation)
        {
          result += Int32.Parse(line.Groups[1].Value) * Int32.Parse(line.Groups[2].Value);
        }
      }

      return result;
    }


  }
}
