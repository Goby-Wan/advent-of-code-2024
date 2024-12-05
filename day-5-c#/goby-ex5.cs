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
    private List<int[]> _badUpdates;
    public Day_05()
    {
      _input = File.ReadAllLines("input/day_05/input-ex5.txt");
      _orders = new Dictionary<int, List<int>>();
      _updates = new List<int[]>();
      _badUpdates = new List<int[]>();

      ParseInput(_input);

      Console.WriteLine("Résultat de la partie 1 : " + PartOne());
      Console.WriteLine("Résultat de la partie 2 : " + PartTwo());
    }

    private int PartOne()
    {
      int result = 0;

      foreach (int[] update in _updates)
      {
        if (testUpdate(update))
        {
          result += update[(update.Count() - 1) / 2];
        }
      }

      return result;
    }

    private int PartTwo()
    {
      int result = 0;

      foreach (int[] badUpdate in _badUpdates)
      {
        List<int> currentUpdate = badUpdate.ToList();
        do
        {
          for (int i = 0; i < currentUpdate.Count; i++)
          {
            int value = currentUpdate[i];
            if (_orders.ContainsKey(value))
            {
              int[] badValues = _orders[value].Intersect(currentUpdate.Take(i)).ToArray();
              int firstValue = badValues.Count() > 0 ? badValues[0] : 0;
              if (firstValue != 0)
              {
                int newIndex = currentUpdate.IndexOf(firstValue);
                currentUpdate.RemoveAt(i);
                currentUpdate.Insert(newIndex, value);
                i--;
              }
            }
          }
        } while (testUpdate(currentUpdate.ToArray(), false) == false);


        result += currentUpdate[(currentUpdate.Count() - 1) / 2];
      }

      return result;
    }

    private Boolean testUpdate(int[] update, Boolean addToBadUpdates = true)
    {
      Boolean isValid = true;
      foreach (var (pageValue, index) in update.Select((value, i) => (value, i)))
      {
        if (_orders.ContainsKey(pageValue))
        {
          if (update.Take(index).Intersect(_orders[pageValue]).Any())
          {
            isValid = false;
            if (addToBadUpdates) _badUpdates.Add(update);
            break;
          }
        }
      }
      return isValid;
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
