using System;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
  class Day_02
  {
    private readonly string[] _input;
    private List<List<int>> _listReports;
    public Day_02()
    {
      _input = File.ReadAllLines("input/day_02/input-ex2.txt");
      _listReports = new List<List<int>>();
      ParseInput(_input);

      Console.WriteLine("Résultat de la partie 1 : " + PartOne());
      Console.WriteLine("Résultat de la partie 2 : " + PartTwo());
    }

    private int PartOne()
    {
      int result = 0;

      foreach (List<int> report in _listReports)
      {
        result += TestReport(report) ? 1 : 0;
      }

      return result;
    }

    private int PartTwo()
    {
      int result = 0;

      foreach (List<int> report in _listReports)
      {
        if (TestReport(report))
        {
          result += 1;
        }
        else
        {
          for (int i = 0; i < report.Count; i++)
          {
            List<int> reportCopy = new List<int>(report);
            reportCopy.RemoveAt(i);
            if (TestReport(reportCopy))
            {
              result += 1;
              break;
            }
          }
        }
      }

      return result;
    }


    private void ParseInput(string[] input)
    {
      foreach (string line in input)
      {
        string[] lineParsed = Regex.Split(line, " ");
        _listReports.Add(lineParsed.Select(Int32.Parse).ToList());
      }
    }

    private Boolean TestReport(List<int> report)
    {
      Boolean isValidIncr = true;
      Boolean isValidDecr = true;

      // Test montant
      for (int i = 1; i < report.Count; i++)
      {
        if (report[i] - report[i - 1] is < 1 or > 3)
        {
          isValidIncr = false;
          break;
        }
      }
      // Test descendant
      for (int i = 1; i < report.Count; i++)
      {
        if (report[i] - report[i - 1] is > -1 or < -3)
        {
          isValidDecr = false;
          break;
        }
      }

      return isValidIncr || isValidDecr;
    }
  }
}
