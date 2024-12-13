using System;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
  class Day_13
  {
    private readonly string _input;
    private List<double[,]> _equations;
    public Day_13()
    {
      _input = File.ReadAllText("input/day_13/input-ex13.txt");
      _equations = new List<double[,]>();
      ParseInput(_input);

      Console.WriteLine("Résultat de la partie 1 : " + PartOne());
      Console.WriteLine("Résultat de la partie 2 : " + PartTwo());
    }

    private long PartOne(int maxTries = 100)
    {
      long result = 0;
      foreach (double[,] equation in _equations)
      {
        (long x, long y) = SolveEquation(equation);
        bool moreThanMax = maxTries != -1 ? x <= maxTries && y <= maxTries : true;
        if (x >= 0 && y >= 0 && moreThanMax)
        {
          result += 3 * x + y;
        }
      }

      return result;
    }

    private long PartTwo()
    {
      foreach (double[,] equation in _equations)
      {
        equation[2, 0] += 10000000000000;
        equation[2, 1] += 10000000000000;
      }


      return PartOne(-1);
    }

    private (long x, long y) SolveEquation(double[,] equation)
    {
      double delta = equation[0, 0] * equation[1, 1] - equation[0, 1] * equation[1, 0];
      if (delta == 0)
      {
        return (-1, -1);
      }
      else
      {
        double x = (equation[2, 0] * equation[1, 1] - equation[2, 1] * equation[1, 0]) / delta;
        double y = (equation[0, 0] * equation[2, 1] - equation[0, 1] * equation[2, 0]) / delta;

        return Double.IsInteger(x) && Double.IsInteger(y) ? ((long)x, (long)y) : (-1, -1);
      }
    }

    private void ParseInput(string input)
    {
      string[] lineParsed = Regex.Split(input, "\\r\\n\\r\\n");
      foreach (string line in lineParsed)
      {
        MatchCollection equationA = Regex.Matches(line, "Button A: X\\+(\\d+), Y\\+(\\d+)\\r\\n");
        MatchCollection equationB = Regex.Matches(line, "Button B: X\\+(\\d+), Y\\+(\\d+)\\r\\n");
        MatchCollection equationP = Regex.Matches(line, "Prize: X=(\\d+), Y=(\\d+)");

        double[,] matrix = new double[3, 2];
        matrix[0, 0] = double.Parse(equationA[0].Groups[1].Value);
        matrix[0, 1] = double.Parse(equationA[0].Groups[2].Value);
        matrix[1, 0] = double.Parse(equationB[0].Groups[1].Value);
        matrix[1, 1] = double.Parse(equationB[0].Groups[2].Value);
        matrix[2, 0] = double.Parse(equationP[0].Groups[1].Value);
        matrix[2, 1] = double.Parse(equationP[0].Groups[2].Value);
        _equations.Add(matrix);
      }
    }

  }
}
