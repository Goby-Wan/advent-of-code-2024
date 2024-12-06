using System;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
  class Day_06
  {
    private readonly string[] _input;
    private char[][] _originLaby;
    private char[][] _laby;
    private (int x, int y, int dir) _originalPos;
    private HashSet<(int x, int y)> _obstructions;

    public Day_06()
    {
      _input = File.ReadAllLines("input/day_06/input-ex6.txt");
      _laby = new char[_input.Length][];
      ParseInput(_input);
      _originLaby = CloneLaby(_laby);
      _obstructions = new HashSet<(int x, int y)>();

      Console.WriteLine("Résultat de la partie 1 : " + PartOne());
      Console.WriteLine("Résultat de la partie 2 : " + PartTwo());
    }

    private int PartOne()
    {
      int result = 0;
      int moves = 0;
      (int x, int y, int dir) currentPos = _originalPos;
      while (Move(ref currentPos)) { moves++; }
      _laby[currentPos.y][currentPos.x] = 'X';

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
      for (int y = 0; y < _laby.Length; y++)
      {
        for (int x = 0; x < _laby[y].Length; x++)
        {
          SimulateObs((x, y));
        }
      }

      return _obstructions.Count();
    }
    private Boolean SimulateObs((int x, int y) pos)
    {
      (int x, int y, int dir) currentPos = (_originalPos.x, _originalPos.y, _originalPos.dir);
      _laby = CloneLaby(_originLaby);
      _laby[pos.y][pos.x] = '#';

      List<(int x, int y, int dir)> visited = new List<(int x, int y, int dir)>();
      while (Move(ref currentPos))
      {
        if (visited.Contains(currentPos))
        {
          _obstructions.Add((pos.x, pos.y));
          return true;
        }
        visited.Add(currentPos);
      }
      return false;
    }

    private Boolean Move(ref (int x, int y, int dir) _pos)
    {
      switch (_pos.dir)
      {
        case 0:
          return MoveUp(ref _pos);
        case 1:
          return MoveRight(ref _pos);
        case 2:
          return MoveDown(ref _pos);
        case 3:
          return MoveLeft(ref _pos);
        default:
          return false;
      }
    }

    private Boolean MoveUp(ref (int x, int y, int dir) _pos)
    {
      if (_pos.y == 0) return false;
      if (_laby[_pos.y - 1][_pos.x] == '#')
      {
        TurnRight(ref _pos);
      }
      else
      {
        _laby[_pos.y][_pos.x] = 'X';
        _pos.y--;
      }
      return true;
    }

    private Boolean MoveRight(ref (int x, int y, int dir) _pos)
    {
      if (_pos.x == _laby[0].Length - 1) return false;
      if (_laby[_pos.y][_pos.x + 1] == '#')
      {
        TurnRight(ref _pos);
      }
      else
      {
        _laby[_pos.y][_pos.x] = 'X';
        _pos.x++;
      }
      return true;
    }

    private Boolean MoveDown(ref (int x, int y, int dir) _pos)
    {
      if (_pos.y == _laby.Length - 1) return false;
      if (_laby[_pos.y + 1][_pos.x] == '#')
      {
        TurnRight(ref _pos);
      }
      else
      {
        _laby[_pos.y][_pos.x] = 'X';
        _pos.y++;
      }
      return true;
    }

    private Boolean MoveLeft(ref (int x, int y, int dir) _pos)
    {
      if (_pos.x == 0) return false;
      if (_laby[_pos.y][_pos.x - 1] == '#')
      {
        TurnRight(ref _pos);
      }
      else
      {
        _laby[_pos.y][_pos.x] = 'X';
        _pos.x--;
      }
      return true;
    }

    private void TurnRight(ref (int x, int y, int dir) _pos)
    {
      _pos.dir = (_pos.dir + 1) % 4;
    }

    private void ParseInput(string[] input)
    {
      foreach (var (value, y) in input.Select((value, i) => (value, i)))
      {
        _laby[y] = value.ToCharArray();
        int x = Array.FindIndex(_laby[y], x => x == '^');
        if (x != -1)
        {
          _originalPos = (x, y, 0);
        }
      }
    }

    private char[][] CloneLaby(char[][] laby)
    {
      char[][] clone = new char[laby.Length][];
      for (int y = 0; y < laby.Length; y++)
      {
        clone[y] = new char[laby[y].Length];
        for (int x = 0; x < laby[y].Length; x++)
        {
          clone[y][x] = laby[y][x];
        }
      }
      return clone;
    }
  }

}
