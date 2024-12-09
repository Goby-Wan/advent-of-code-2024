using System;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
  class Day_09
  {
    private readonly char[] _input;
    private int[] _fs;
    public Day_09()
    {
      _input = File.ReadAllText("input/day_09/input-ex9.txt").ToCharArray();
      _fs = ParseInput(_input);

      Console.WriteLine("Résultat de la partie 1 : " + PartOne());
      Console.WriteLine("Résultat de la partie 2 : " + PartTwo());
    }

    private long PartOne()
    {
      long result = 0;

      int[] compressedValues = Compress(_fs);

      for (int i = 0; i < compressedValues.Length; i++)
      {
        result += i * compressedValues[i];
      }

      return result;
    }

    private int PartTwo()
    {
      int result = 0;

      return result;
    }
    private int[] ParseInput(char[] input)
    {
      List<int> fs = new List<int>();
      int id = 0;
      for (int i = 0; i < input.Length; i++)
      {
        int value = Int32.Parse(input[i].ToString());
        for (int j = 0; j < value; j++)
        {
          if (i % 2 == 0) // Nouveau fichier
          {
            fs.Add(id);
          }
          else // Espace libre
          {
            fs.Add(-1);
          }
        }
        if (i % 2 == 0) id++;
      }

      return fs.ToArray();
    }

    private int[] Compress(int[] fs)
    {
      int lastValueIndex = fs.Count() - 1;
      for (int i = 0; i != lastValueIndex; i++)
      {
        if (fs[i] == -1)
        {
          while (fs[lastValueIndex] == -1)
          {
            lastValueIndex--;
          }
          (fs[i], fs[lastValueIndex]) = (fs[lastValueIndex], fs[i]);
        }
      }

      int[] lastValues = fs.TakeLast(fs.Length - lastValueIndex).ToArray();
      if (lastValues.All(x => x == -1))
      {
        return fs.Take(lastValueIndex).ToArray();
      }
      else
      {
        Console.WriteLine("Erreur : impossible de compresser le fichier");
        return new int[0];
      }


    }


  }
}
