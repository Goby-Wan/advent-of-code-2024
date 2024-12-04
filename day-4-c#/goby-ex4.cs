using System;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
  class Day_04
  {
    private readonly string[] _input;
    private char[][] _crossword;
    private int maxX = 0;
    private int maxY = 0;

    public Day_04()
    {
      _input = File.ReadAllLines("input/day_04/input-ex4.txt");
      ParseInput(_input);

      Console.WriteLine("Résultat de la partie 1 : " + PartOne());
      Console.WriteLine("Résultat de la partie 2 : " + PartTwo());
    }

    private int PartOne()
    {
      int result = 0;

      for (int y = 0; y < _crossword.Length; y++)
      {
        for (int x = 0; x < _crossword[y].Length; x++)
        {
          if (_crossword[y][x] != 'X')
          {
            continue;
          }
          else
          {

            if (x > 2)
            {
              // Ouest
              if (_crossword[y][x - 1] == 'M' && _crossword[y][x - 2] == 'A' && _crossword[y][x - 3] == 'S') result++;
              // Sud-Ouest
              if (y < maxY && _crossword[y + 1][x - 1] == 'M' && _crossword[y + 2][x - 2] == 'A' && _crossword[y + 3][x - 3] == 'S') result++;
              // Nord-Ouest
              if (y > 2 && _crossword[y - 1][x - 1] == 'M' && _crossword[y - 2][x - 2] == 'A' && _crossword[y - 3][x - 3] == 'S') result++;
            }
            if (x < maxX)
            {
              // Est
              if (_crossword[y][x + 1] == 'M' && _crossword[y][x + 2] == 'A' && _crossword[y][x + 3] == 'S') result++;
              // Sud-Est
              if (y < maxY && _crossword[y + 1][x + 1] == 'M' && _crossword[y + 2][x + 2] == 'A' && _crossword[y + 3][x + 3] == 'S') result++;
              // Nord-Est
              if (y > 2 && _crossword[y - 1][x + 1] == 'M' && _crossword[y - 2][x + 2] == 'A' && _crossword[y - 3][x + 3] == 'S') result++;
            }
            // Sud
            if (y < maxY && _crossword[y + 1][x] == 'M' && _crossword[y + 2][x] == 'A' && _crossword[y + 3][x] == 'S') result++;
            // Nord
            if (y > 2 && _crossword[y - 1][x] == 'M' && _crossword[y - 2][x] == 'A' && _crossword[y - 3][x] == 'S') result++;
          }
        }
      }

      return result;
    }

    private int PartTwo()
    {
      int result = 0;

      for (int y = 1; y < _crossword.Length - 1; y++)
      {
        for (int x = 1; x < _crossword[y].Length - 1; x++)
        {
          if (_crossword[y][x] != 'A')
          {
            continue;
          }
          else
          {
            if (_crossword[y - 1][x - 1] == 'M')
            {
              // MM SS
              if (_crossword[y - 1][x + 1] == 'M' && _crossword[y + 1][x - 1] == 'S' && _crossword[y + 1][x + 1] == 'S')
              {
                result++;
                continue;
              }
              // MS MS
              if (_crossword[y - 1][x + 1] == 'S' && _crossword[y + 1][x - 1] == 'M' && _crossword[y + 1][x + 1] == 'S')
              {
                result++;
                continue;
              }
            }
            else if (_crossword[y - 1][x - 1] == 'S')
            {
              // SM SM
              if (_crossword[y - 1][x + 1] == 'M' && _crossword[y + 1][x - 1] == 'S' && _crossword[y + 1][x + 1] == 'M')
              {
                result++;
                continue;
              }
              // SS MM
              if (_crossword[y - 1][x + 1] == 'S' && _crossword[y + 1][x - 1] == 'M' && _crossword[y + 1][x + 1] == 'M')
              {
                result++;
                continue;
              }
            }
          }
        }
      }

      return result;
    }

    private void ParseInput(string[] input)
    {
      _crossword = new char[input.Length][];
      foreach (var (value, i) in input.Select((value, i) => (value, i)))
      {
        _crossword[i] = value.ToCharArray();
      }
      maxX = _crossword[0].Length - 3;
      maxY = _crossword.Length - 3;
    }

  }
}
