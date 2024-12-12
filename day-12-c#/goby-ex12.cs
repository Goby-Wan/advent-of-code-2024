using System;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
  class Day_12
  {
    private readonly string[] _input;
    private char[][] _map;
    private Dictionary<(int x, int y), int> _plots;
    private Dictionary<int, List<(int x, int y)>> _regions;

    public Day_12()
    {
      _input = File.ReadAllLines("input/day_12/input-ex12.txt");
      _map = new char[_input.Length][];
      _plots = new Dictionary<(int x, int y), int>();
      _regions = new Dictionary<int, List<(int x, int y)>>();
      ParseInput(_input);
      FindRegions();

      Console.WriteLine("Résultat de la partie 1 : " + PartOne());
      Console.WriteLine("Résultat de la partie 2 : " + PartTwo());
    }

    private int PartOne()
    {
      int result = 0;

      foreach (var region in _regions)
      {
        result += CalcPerimeter(region.Key) * region.Value.Count;
      }

      return result;
    }

    private int PartTwo()
    {
      int result = 0;

      return result;
    }

    private void FindRegions()
    {
      int id = 0;
      for (int y = 0; y < _map.Length; y++)
      {
        for (int x = 0; x < _map[y].Length; x++)
        {
          if (_plots.ContainsKey((x, y))) continue;
          FindAdjacentPlots(x, y, id, _map[y][x]);
          id++;
        }
      }

      foreach (var plot in _plots)
      {
        if (!_regions.ContainsKey(plot.Value))
        {
          _regions.Add(plot.Value, new List<(int x, int y)>());
        }
        _regions[plot.Value].Add(plot.Key);
      }
    }

    private void FindAdjacentPlots(int x, int y, int id, char plant)
    {
      if (x < 0 || y < 0 || y >= _map.Length || x >= _map[y].Length) return;
      if (_plots.ContainsKey((x, y))) return;
      if (_map[y][x] != plant) return;

      _plots.Add((x, y), id);

      FindAdjacentPlots(x + 1, y, id, plant);
      FindAdjacentPlots(x - 1, y, id, plant);
      FindAdjacentPlots(x, y + 1, id, plant);
      FindAdjacentPlots(x, y - 1, id, plant);
    }

    private int CalcPerimeter(int id)
    {
      int result = 0;
      foreach (var plot in _regions[id])
      {
        int numberOfAdjacentPlots = 0;
        if (plot.y - 1 >= 0 && _plots[(plot.x, plot.y - 1)] == id) numberOfAdjacentPlots++;
        if (plot.x + 1 < _map.Length && _plots[(plot.x + 1, plot.y)] == id) numberOfAdjacentPlots++;
        if (plot.y + 1 < _map.Length && _plots[(plot.x, plot.y + 1)] == id) numberOfAdjacentPlots++;
        if (plot.x - 1 >= 0 && _plots[(plot.x - 1, plot.y)] == id) numberOfAdjacentPlots++;
        result += 4 - numberOfAdjacentPlots;
      }
      return result;
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
