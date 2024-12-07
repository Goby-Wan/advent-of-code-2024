using System;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
  class Day_07
  {
    private readonly string[] _input;
    private List<(long, List<long>)> _list;
    private List<String> _operandes;
    public Day_07()
    {
      _input = File.ReadAllLines("input/day_07/input-ex7.txt");
      _list = new List<(long, List<long>)>();
      ParseInput(_input);

      Console.WriteLine("Résultat de la partie 1 : " + PartOne());
      Console.WriteLine("Résultat de la partie 2 : " + PartTwo());
    }

    private long PartOne()
    {
      long result = 0;
      _operandes = new List<string> { "+", "*" };

      foreach (var (key, value) in _list)
      {
        List<List<String>> listOperations = GetPermutationsWithRept(value.Count - 1, _operandes).Select(e => e.ToList()).ToList();
        foreach (List<String> operations in listOperations)
        {
          long currentResult = value[0];
          for (int i = 0; i < value.Count - 1; i++)
          {
            if (operations[i] == "+")
            {
              currentResult += value[i + 1];
            }
            else if (operations[i] == "*")
            {
              currentResult *= value[i + 1];
            }
          }
          if (currentResult == key)
          {
            result += key;
            break;
          }
        }
      }

      return result;
    }

    private long PartTwo()
    {
      long result = 0;
      _operandes = new List<string> { "+", "*", "|" };

      foreach (var (key, value) in _list)
      {
        List<List<String>> listOperations = GetPermutationsWithRept(value.Count - 1, _operandes).Select(e => e.ToList()).ToList();
        foreach (List<String> operations in listOperations)
        {
          long currentResult = value[0];
          for (int i = 0; i < value.Count - 1; i++)
          {
            if (operations[i] == "+")
            {
              currentResult += value[i + 1];
            }
            else if (operations[i] == "*")
            {
              currentResult *= value[i + 1];
            }
            else if (operations[i] == "|")
            {
              currentResult = Int64.Parse(String.Concat(currentResult.ToString(), value[i + 1].ToString()));
            }
            if (currentResult >= key) break;
          }
          if (currentResult == key)
          {
            result += key;
            break;
          }
        }
      }

      return result;
    }

    private IEnumerable<IEnumerable<T>> GetPermutationsWithRept<T>(long length, IEnumerable<T> list)
    {
      if (length == 1) return list.Select(t => new T[] { t });
      return GetPermutationsWithRept(length - 1, list)
          .SelectMany(t => list,
              (t1, t2) => t1.Concat(new T[] { t2 }));
    }

    private void ParseInput(string[] input)
    {
      foreach (string line in input)
      {
        string[] lineParsed = Regex.Split(line, ": ");
        long result = Int64.Parse(lineParsed[0]);
        _list.Add((result, Regex.Split(lineParsed[1], " ").Select(Int64.Parse).ToList()));
      }
    }

  }
}
