using System;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
  class Day_08
  {
    private readonly string[] _input;
    private char[][] _map;
    private readonly int _maxX, _maxY;
    public Day_08()
    {
      _input = File.ReadAllLines("input/day_08/input-ex8.txt");
      _map = new char[_input.Length][];
      ParseInput(_input);
      _maxX = _map[0].Length;
      _maxY = _map.Length;

      Console.WriteLine("Résultat de la partie 1 : " + PartOne());
      Console.WriteLine("Résultat de la partie 2 : " + PartTwo());
    }

    private int PartOne()
    {
      HashSet<(int, int)> antinodes = new HashSet<(int x, int y)>();

      Dictionary<char, List<(int, int)>> antennas = new Dictionary<char, List<(int x, int y)>>();

      for (int y = 0; y < _map.Length; y++)
      {
        for (int x = 0; x < _map[y].Length; x++)
        {
          if (_map[y][x] != '.')
          {
            if (!antennas.ContainsKey(_map[y][x]))
            {
              antennas[_map[y][x]] = new List<(int x, int y)>();
            }
            antennas[_map[y][x]].Add((x, y));
          }
        }
      }

      foreach (var (key, value) in antennas)
      {
        GetPermutations(2, value).ToList().ForEach(e =>
        {
          int x1 = e.ElementAt(0).Item1; int y1 = e.ElementAt(0).Item2;
          int x2 = e.ElementAt(1).Item1; int y2 = e.ElementAt(1).Item2;

          int xDiff = x2 - x1;
          int yDiff = y2 - y1;

          int xA = x1 - xDiff; int yA = y1 - yDiff;
          int xB = x2 + xDiff; int yB = y2 + yDiff;

          if (xA >= 0 && xA < _maxX && yA >= 0 && yA < _maxY)
          {
            antinodes.Add((xA, yA));
          }
          if (xB >= 0 && xB < _maxX && yB >= 0 && yB < _maxY)
          {
            antinodes.Add((xB, yB));
          }

        });
      }

      return antinodes.Count;
    }

    private int PartTwo()
    {
      int result = 0;

      return result;
    }

    private IEnumerable<IEnumerable<T>> GetPermutations<T>(long length, IEnumerable<T> list)
    {
      if (length == 1) return list.Select(t => new T[] { t });
      return GetPermutations(length - 1, list)
          .SelectMany(t => list.Where(o => !t.Contains(o)),
              (t1, t2) => t1.Concat(new T[] { t2 }));
    }

    private void ParseInput(string[] input)
    {
      foreach (var (value, i) in input.Select((value, i) => (value, i)))
      {
        _map[i] = value.ToCharArray();
      }
    }

  }
}
