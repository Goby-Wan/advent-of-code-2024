using System;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
  class Day_06
  {
    private readonly string[] _input;
    private char[][] _originLaby;
    private char[][] _laby;

    // Direction : 0 = Haut, 1 = Droite, 2 = Bas, 3 = Gauche
    private (int x, int y, int dir) _pos;

    public Day_06()
    {
      _input = File.ReadAllLines("input/day_06/input-ex6.txt");
      ParseInput(_input);
      _originLaby = _laby;

      Console.WriteLine("Résultat de la partie 1 : " + PartOne());
      Console.WriteLine("Résultat de la partie 2 : " + PartTwo());
    }

    private int PartOne()
    {
      int result = 0;
      int moves = 0;
      while (Move()) { moves++; }
      _laby[_pos.y][_pos.x] = 'X';

      for (int y = 0; y < _laby.Length; y++)
      {
        for (int x = 0; x < _laby[y].Length; x++)
        {
          if (_laby[y][x] == 'X') result++;
        }
      }

      return result;
    }

    private int PartTwo()
    {
      int result = 0;

      return result;
    }

    private Boolean Move()
    {
      switch (_pos.dir)
      {
        case 0:
          return MoveUp();
        case 1:
          return MoveRight();
        case 2:
          return MoveDown();
        case 3:
          return MoveLeft();
        default:
          return false;
      }
    }

    private Boolean MoveUp()
    {
      if (_pos.y == 0) return false;
      if (_laby[_pos.y - 1][_pos.x] == '#')
      {
        TurnRight();
      }
      else
      {
        _laby[_pos.y][_pos.x] = 'X';
        _pos.y--;
      }
      return true;
    }

    private Boolean MoveRight()
    {
      if (_pos.x == _laby[0].Length - 1) return false;
      if (_laby[_pos.y][_pos.x + 1] == '#')
      {
        TurnRight();
      }
      else
      {
        _laby[_pos.y][_pos.x] = 'X';
        _pos.x++;
      }
      return true;
    }

    private Boolean MoveDown()
    {
      if (_pos.y == _laby.Length - 1) return false;
      if (_laby[_pos.y + 1][_pos.x] == '#')
      {
        TurnRight();
      }
      else
      {
        _laby[_pos.y][_pos.x] = 'X';
        _pos.y++;
      }
      return true;
    }

    private Boolean MoveLeft()
    {
      if (_pos.x == 0) return false;
      if (_laby[_pos.y][_pos.x - 1] == '#')
      {
        TurnRight();
      }
      else
      {
        _laby[_pos.y][_pos.x] = 'X';
        _pos.x--;
      }
      return true;
    }

    private void TurnRight()
    {
      _pos.dir = (_pos.dir + 1) % 4;
    }

    private void ParseInput(string[] input)
    {
      _laby = new char[input.Length][];
      foreach (var (value, y) in input.Select((value, i) => (value, i)))
      {
        _laby[y] = value.ToCharArray();
        int x = Array.FindIndex(_laby[y], x => x == '^');
        if (x != -1)
        {
          _pos = (x, y, 0);
        }
      }
    }

  }
}
