using System;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
  class Day_02
  {
    private readonly string[] _input;
    private List<int[]> _listReports;
    public Day_02()
    {
      _input = File.ReadAllLines("input/day_02/input-ex2.txt");
      _listReports = new List<int[]>();
      ParseInput(_input);

      Console.WriteLine("Résultat de la partie 1 : " + PartOne());
    }

    private int PartOne()
    {
      int result = 0;

      foreach (int[] report in _listReports)
      {
        Boolean isValidIncr = true;
        Boolean isValidDecr = true;

        // Test montant
        for (int i = 1; i < report.Length; i++)
        {
          if (report[i] - report[i - 1] < 1 || report[i] - report[i - 1] > 3)
          {
            isValidIncr = false;
            break;
          }
        }
        // Test descendant
        for (int i = 1; i < report.Length; i++)
        {
          if (report[i] - report[i - 1] > -1 || report[i] - report[i - 1] < -3)
          {
            isValidDecr = false;
            break;
          }
        }

        result += isValidIncr || isValidDecr ? 1 : 0;
      }

      return result;
    }

    private void ParseInput(string[] input)
    {
      foreach (string line in input)
      {
        string[] lineParsed = Regex.Split(line, " ");
        _listReports.Add(lineParsed.Select(Int32.Parse).ToArray());
      }
    }
  }
}
