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

      foreach (var region in _regions)
      {
        result += CalcSides(region.Key) * region.Value.Count;
      }

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

    private int CalcSides(int id)
    {
      int maxX = 0;
      int maxY = 0;
      List<(int x, int y)> corners = new List<(int x, int y)>();
      List<(int x, int y)> plots = new List<(int x, int y)>();

      foreach (var plot in _regions[id])
      {
        plots.Add((2 * plot.x + 1, 2 * plot.y + 1));
        if (2 * plot.x + 1 > maxX) maxX = 2 * plot.x + 1;
        if (2 * plot.y + 1 > maxY) maxY = 2 * plot.y + 1;
      };

      foreach (var plot in plots)
      {
        bool plotN = plot.y - 2 >= 0 && plots.Contains((plot.x, plot.y - 2));
        bool plotE = plot.x + 2 <= maxX && plots.Contains((plot.x + 2, plot.y));
        bool plotS = plot.y + 2 <= maxY && plots.Contains((plot.x, plot.y + 2));
        bool plotW = plot.x - 2 >= 0 && plots.Contains((plot.x - 2, plot.y));
        bool plotNW = plot.y - 2 >= 0 && plot.x - 2 >= 0 && plots.Contains((plot.x - 2, plot.y - 2));
        bool plotNE = plot.y - 2 >= 0 && plot.x + 2 <= maxX && plots.Contains((plot.x + 2, plot.y - 2));
        bool plotSE = plot.y + 2 <= maxY && plot.x + 2 <= maxX && plots.Contains((plot.x + 2, plot.y + 2));
        bool plotSW = plot.y + 2 <= maxY && plot.x - 2 >= 0 && plots.Contains((plot.x - 2, plot.y + 2));

        // Coin Nord-Ouest
        if (!corners.Contains((plot.x - 1, plot.y - 1)))
        {
          if (!plotN && !plotW || plotNW && !plotN || plotNW && !plotW || !plotNW && plotN && plotW) corners.Add((plot.x - 1, plot.y - 1));
          if (plotNW && !plotN && !plotW) corners.Add((plot.x - 1, plot.y - 1));
        }
        // Coin Nord-Est
        if (!corners.Contains((plot.x + 1, plot.y - 1)))
        {
          if (!plotN && !plotE || plotNE && !plotN || plotNE && !plotE || !plotNE && plotN && plotE) corners.Add((plot.x + 1, plot.y - 1));
          if (plotNE && !plotN && !plotE) corners.Add((plot.x + 1, plot.y - 1));
        }
        // Coin Sud-Est
        if (!corners.Contains((plot.x + 1, plot.y + 1)))
        {
          if (!plotS && !plotE || plotSE && !plotS || plotSE && !plotE || !plotSE && plotS && plotE) corners.Add((plot.x + 1, plot.y + 1));
          if (plotSE && !plotS && !plotE) corners.Add((plot.x + 1, plot.y + 1));
        }
        // Coin Sud-Ouest
        if (!corners.Contains((plot.x - 1, plot.y + 1)))
        {
          if (!plotS && !plotW || plotSW && !plotS || plotSW && !plotW || !plotSW && plotS && plotW) corners.Add((plot.x - 1, plot.y + 1));
          if (plotSW && !plotS && !plotW) corners.Add((plot.x - 1, plot.y + 1));
        }
      }

      return corners.Count;
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
