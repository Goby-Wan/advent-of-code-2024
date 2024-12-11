using System;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
  class Day_11
  {
    private readonly string _input;
    private List<long> _originalStones;
    public Day_11()
    {
      _input = File.ReadAllText("input/day_11/input-ex11.txt");
      _originalStones = ParseInput(_input);

      Console.WriteLine("Résultat de la partie 1 : " + PartOne());
      Console.WriteLine("Résultat de la partie 2 : " + PartTwo());
    }

    private long PartOne()
    {
      List<long> stones = new List<long>(_originalStones);

      for (int i = 0; i < 25; i++)
      {
        stones = Blink(stones);
      }

      return stones.Count;
    }



    private long PartTwo()
    {
      long result = 0;

      return result;
    }

    private List<long> Blink(List<long> input)
    {
      List<long> output = new List<long>();
      foreach (long stone in input)
      {
        if (stone == 0)
        {
          output.Add(1);
        }
        else if (stone.ToString().Length % 2 == 0)
        {
          int halfLength = stone.ToString().Length / 2;
          string firstHalf = stone.ToString().Substring(0, halfLength);
          string secondHalf = stone.ToString().Substring(halfLength, halfLength);
          output.Add(long.Parse(firstHalf));
          output.Add(long.Parse(secondHalf));
        }
        else
        {
          output.Add(stone * 2024);
        }
      }
      return output;
    }

    private List<long> ParseInput(string input)
    {
      return Regex.Split(input, " ").ToList().ConvertAll(long.Parse);
    }

  }
}
