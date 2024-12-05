using System;
using System.Reflection.Metadata;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
  class Day_05
  {
    private readonly string[] _input;
    private Dictionary<int, List<int>> _orders;
    private List<int[]> _updates;
    public Day_05()
    {
      _input = File.ReadAllLines("input/day_05/input-ex5.txt");
      _orders = new Dictionary<int, List<int>>();
      _updates = new List<int[]>();
      ParseInput(_input);

      Console.WriteLine("Résultat de la partie 1 : " + PartOne());
      Console.WriteLine("Résultat de la partie 2 : " + PartTwo());
    }

    private int PartOne()
    {
      int result = 0;

      foreach (int[] update in _updates)
      {
        Boolean isValid = true;
        foreach (var (pageValue, index) in update.Select((value, i) => (value, i)))
        {
          if (_orders.ContainsKey(pageValue))
          {
            if (update.Take(index).Intersect(_orders[pageValue]).Any())
            {
              isValid = false;
              break;
            }
          }
        }
        if (isValid)
        {
          result += update[(update.Count() - 1) / 2];
        }
      }

      return result;
    }

    private int PartTwo()
    {
      int result = 0;

      return result;
    }

    private void ParseInput(string[] input)
    {
      foreach (string line in input)
      {
        if (line.Contains("|"))
        {
          string[] parts = line.Split("|");
          int key = int.Parse(parts[0]);
          int value = int.Parse(parts[1]);
          if (_orders.ContainsKey(key) == false)
          {
            _orders.Add(key, new List<int>());
          }
          _orders[key].Add(value);

        }
        else if (line.Contains(","))
        {
          string[] parts = line.Split(",");
          _updates.Add(parts.Select(Int32.Parse).ToArray());
        }
      }
    }

  }
}
