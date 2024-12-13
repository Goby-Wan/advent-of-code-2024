using System;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
  class Day_11
  {
    private readonly string _input;
    private List<long> _originalStones;
    private long[][] _digits;
    private readonly int[][] _values = [[1], [1, 1, 2, 4], [1, 1, 2, 4], [1, 1, 2, 4], [1, 1, 2, 4], [1, 1, 1, 2, 4, 8], [1, 1, 1, 2, 4, 8], [1, 1, 1, 2, 4, 8], [1, 1, 1, 2, 4, 7], [1, 1, 1, 2, 4, 8]];

    public Day_11()
    {
      _input = File.ReadAllText("input/day_11/input-ex11.txt");
      _originalStones = ParseInput(_input);
      _digits = [];

      var sw = Stopwatch.StartNew();
      long resultOneBF = PartOneBF();
      sw.Stop();
      Console.WriteLine("Résultat de la partie 1 (bruteforce) : " + resultOneBF + " - Calculé en " + sw.ElapsedMilliseconds + "ms / " + sw.ElapsedTicks + " ticks");

      sw = Stopwatch.StartNew();
      long resultOne = PartOneOpti();
      sw.Stop();
      Console.WriteLine("Résultat de la partie 1 : " + resultOne + " - Calculé en " + sw.ElapsedMilliseconds + "ms / " + sw.ElapsedTicks + " ticks");

      sw = Stopwatch.StartNew();
      long resultTwo = PartTwo();
      sw.Stop();
      Console.WriteLine("Résultat de la partie 2 : " + resultTwo + " - Calculé en " + sw.ElapsedMilliseconds + "ms / " + sw.ElapsedTicks + " ticks");
    }

    private long PartOneBF()
    {
      List<long> stones = new List<long>(_originalStones);

      for (int i = 1; i <= 25; i++)
      {
        stones = BlinkBF(stones);
      }

      return stones.Count;
    }

    private long PartOneOpti()
    {
      List<long> stones = new List<long>(_originalStones);
      _digits = [[0], [0, 0, 0, 0], [0, 0, 0, 0], [0, 0, 0, 0], [0, 0, 0, 0], [0, 0, 0, 0, 0, 0], [0, 0, 0, 0, 0, 0], [0, 0, 0, 0, 0, 0], [0, 0, 0, 0, 0, 0], [0, 0, 0, 0, 0, 0]];

      for (int i = 1; i <= 25; i++)
      {
        stones = Blink(stones);
      }

      return HowManyStones(stones.Count);
    }

    private long PartTwo()
    {
      List<long> stones = new List<long>(_originalStones);
      _digits = [[0], [0, 0, 0, 0], [0, 0, 0, 0], [0, 0, 0, 0], [0, 0, 0, 0], [0, 0, 0, 0, 0, 0], [0, 0, 0, 0, 0, 0], [0, 0, 0, 0, 0, 0], [0, 0, 0, 0, 0, 0], [0, 0, 0, 0, 0, 0]];

      for (int i = 1; i <= 75; i++)
      {
        stones = Blink(stones);
      }

      return HowManyStones(stones.Count);
    }

    private List<long> Blink(List<long> input)
    {
      List<long> otherNumbers = new List<long>();

      // Avancer les nombres dans la matrice
      foreach (var (number, index) in _digits.Select((value, i) => (value, i)))
      {
        if (index == 0) continue;
        for (int i = number.Length - 2; i >= 0; i--)
        {
          if (number[i] != 0)
            number[i + 1] = number[i];
          number[i] = 0;
        }
      }

      // Gérer la dernière ligne de la matrice
      for (int i = 0; i < 10; i++)
      {
        int last = _digits[i].Length - 1;
        long occurences = _digits[i][last];
        if (occurences == 0) continue;
        switch (i)
        {
          case 0:
            _digits[1][0] += 1 * occurences;
            break;
          case 1:
            _digits[0][0] += 1 * occurences;
            _digits[2][0] += 2 * occurences;
            _digits[4][0] += 1 * occurences;
            break;
          case 2:
            _digits[0][0] += 1 * occurences;
            _digits[4][0] += 2 * occurences;
            _digits[8][0] += 1 * occurences;
            break;
          case 3:
            _digits[0][0] += 1 * occurences;
            _digits[2][0] += 1 * occurences;
            _digits[6][0] += 1 * occurences;
            _digits[7][0] += 1 * occurences;
            break;
          case 4:
            _digits[0][0] += 1 * occurences;
            _digits[6][0] += 1 * occurences;
            _digits[8][0] += 1 * occurences;
            _digits[9][0] += 1 * occurences;
            break;
          case 5:
            _digits[0][0] += 2 * occurences;
            _digits[2][0] += 2 * occurences;
            _digits[4][0] += 1 * occurences;
            _digits[8][0] += 3 * occurences;
            break;
          case 6:
            _digits[2][0] += 1 * occurences;
            _digits[4][0] += 2 * occurences;
            _digits[5][0] += 2 * occurences;
            _digits[6][0] += 1 * occurences;
            _digits[7][0] += 1 * occurences;
            _digits[9][0] += 1 * occurences;
            break;
          case 7:
            _digits[0][0] += 1 * occurences;
            _digits[2][0] += 2 * occurences;
            _digits[3][0] += 1 * occurences;
            _digits[6][0] += 2 * occurences;
            _digits[7][0] += 1 * occurences;
            _digits[8][0] += 1 * occurences;
            break;
          case 8:
            _digits[2][0] += 2 * occurences;
            _digits[3][0] += 1 * occurences;
            _digits[6][0] += 1 * occurences;
            _digits[7][0] += 2 * occurences;
            _digits[8][1] += 1 * occurences;
            break;
          case 9:
            _digits[1][0] += 1 * occurences;
            _digits[3][0] += 1 * occurences;
            _digits[4][0] += 1 * occurences;
            _digits[6][0] += 2 * occurences;
            _digits[8][0] += 2 * occurences;
            _digits[9][0] += 1 * occurences;
            break;
        }
        _digits[i][last] = 0;
      }

      // Gérer les autres nombres
      foreach (long stone in input)
      {
        if (stone == 0)
        {
          _digits[1][0] += 1;
        }
        else if (stone < 10)
        {
          _digits[stone][1] += 1;
        }
        else if (stone.ToString().Length % 2 == 0)
        {
          int halfLength = stone.ToString().Length / 2;
          string firstHalf = stone.ToString().Substring(0, halfLength);
          string secondHalf = stone.ToString().Substring(halfLength, halfLength);
          otherNumbers.Add(long.Parse(firstHalf));
          otherNumbers.Add(long.Parse(secondHalf));
        }
        else
        {
          otherNumbers.Add(stone * 2024);
        }
      }

      return otherNumbers;
    }

    private List<long> BlinkBF(List<long> input)
    {
      List<long> output = new List<long>();

      foreach (long stone in input)
      {
        if (stone == 0)
        {
          output.Add(1);
        }
        else if (stone.ToString().Length % 2 == 0)
        {
          int halfLength = stone.ToString().Length / 2;
          string firstHalf = stone.ToString().Substring(0, halfLength);
          string secondHalf = stone.ToString().Substring(halfLength, halfLength);
          output.Add(long.Parse(firstHalf));
          output.Add(long.Parse(secondHalf));
        }
        else
        {
          output.Add(stone * 2024);
        }
      }
      return output;
    }

    private long HowManyStones(long otherStones)
    {
      long result = otherStones;
      foreach (var (digit, i) in _digits.Select((digit, i) => (digit, i)))
      {
        foreach (var (value, j) in digit.Select((value, j) => (value, j)))
        {
          result += value * _values[i][j];
        }
      }

      return result;
    }

    private List<long> ParseInput(string input)
    {
      return Regex.Split(input, " ").ToList().ConvertAll(long.Parse);
    }

  }
}
