using System;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
  class Day_10
  {
    private readonly string[] _input;
    private byte[][] _map;
    private List<List<Byte>> _paths;
    private List<(int x, int y, List<Byte> path)> _hikingPaths;
    public Day_10()
    {
      _input = File.ReadAllLines("input/day_10/input-ex10.txt");
      _map = new byte[_input.Length][];
      ParseInput(_input);
      _paths = GetAllPaths();
      _hikingPaths = new List<(int x, int y, List<Byte> path)>();

      Console.WriteLine("Résultat de la partie 1 : " + PartOne());
      Console.WriteLine("Résultat de la partie 2 : " + PartTwo());
    }

    private int PartOne()
    {
      int result = 0;


      for (int startY = 0; startY < _map.Length; startY++)
      {
        for (int startX = 0; startX < _map[startY].Length; startX++)
        {
          if (_map[startY][startX] == 0)
          {

            HashSet<(int, int)> visited = new HashSet<(int, int)>();
            foreach (List<Byte> pathToTest in _paths)
            {
              int x = startX;
              int y = startY;
              int value = 0;
              foreach (byte direction in pathToTest)
              {
                switch (direction)
                {
                  case 0:
                    y--;
                    break;
                  case 1:
                    x++;
                    break;
                  case 2:
                    y++;
                    break;
                  case 3:
                    x--;
                    break;
                }
                if (x < 0 || x >= _map[startY].Length || y < 0 || y >= _map.Length) break; // Sortie
                if (_map[y][x] != value + 1) break; // Pas la bonne valeur
                value++;
                if (value == 9)
                {
                  visited.Add((x, y));
                  _hikingPaths.Add((startX, startY, pathToTest));
                }
              }
            }
            result += visited.Count;
          }
        }
      }

      return result;
    }

    private int PartTwo()
    {
      int result = 0;

      return result;
    }

    private List<List<Byte>> GetAllPaths()
    {
      List<List<Byte>> paths = new List<List<Byte>>();
      for (byte a = 0; a < 4; a++)
      {
        for (byte b = 0; b < 4; b++)
        {
          if (b % 2 == a % 2 && b != a) continue;
          for (byte c = 0; c < 4; c++)
          {
            if (c % 2 == b % 2 && c != b) continue;
            for (byte d = 0; d < 4; d++)
            {
              if (d % 2 == c % 2 && d != c) continue;
              for (byte e = 0; e < 4; e++)
              {
                if (e % 2 == d % 2 && e != d) continue;
                for (byte f = 0; f < 4; f++)
                {
                  if (f % 2 == e % 2 && f != e) continue;
                  for (byte g = 0; g < 4; g++)
                  {
                    if (g % 2 == f % 2 && g != f) continue;
                    for (byte h = 0; h < 4; h++)
                    {
                      if (h % 2 == g % 2 && h != g) continue;
                      for (byte i = 0; i < 4; i++)
                      {
                        if (i % 2 == h % 2 && i != h) continue;
                        paths.Add([a, b, c, d, e, f, g, h, i]);
                      }
                    }
                  }
                }
              }
            }
          }
        }
      }
      return paths;
    }


    private void ParseInput(string[] input)
    {
      for (int y = 0; y < input.Length; y++)
      {
        _map[y] = new byte[input[y].Length];
        for (int x = 0; x < input[y].Length; x++)
        {
          _map[y][x] = Byte.Parse(input[y][x].ToString());
        }
      }
    }

  }
}
