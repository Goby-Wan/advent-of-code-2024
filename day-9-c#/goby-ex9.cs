using System;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
  class Day_09
  {
    private readonly char[] _input;
    public Day_09()
    {
      _input = File.ReadAllText("input/day_09/input-ex9.txt").ToCharArray();
      Console.WriteLine("Résultat de la partie 1 : " + PartOne());
      Console.WriteLine("Résultat de la partie 2 : " + PartTwo());
    }

    private long PartOne()
    {
      int[] fs = ParseInputArray(_input);
      int[] compressedValues = CompressByte(fs);
      return Checksum(compressedValues);
    }

    private long PartTwo()
    {
      List<(int val, int size)> fs = ParseInputList(_input);
      ListToString(fs);
      int[] compressedValues = CompressFile(fs);
      return Checksum(compressedValues);
    }
    private int[] ParseInputArray(char[] input)
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

    private List<(int val, int size)> ParseInputList(char[] input)
    {
      List<(int val, int size)> fs = new List<(int, int)>();
      int id = 0;
      for (int i = 0; i < input.Length; i++)
      {
        int size = Int32.Parse(input[i].ToString());
        int value = -1;
        if (i % 2 == 0)
        {
          value = id;
          id++;
        }
        fs.Add((value, size));
      }
      return fs;
    }

    private long Checksum(int[] values)
    {
      long result = 0;
      for (int i = 0; i < values.Length; i++)
      {
        result += values[i] != -1 ? i * values[i] : 0;
      }
      return result;
    }

    private int[] CompressByte(int[] fs)
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

    private int[] CompressFile(List<(int val, int size)> fs)
    {
      for (int i = fs.Count - 1; i > 0; i--)
      {
        if (fs[i].val == -1) continue;
        int index = fs.GetRange(0, i).FindIndex(x => x.val == -1 && x.size >= fs[i].size);
        if (index != -1)
        {
          int space = fs[index].size - fs[i].size;
          if (space > 0) fs[index] = (-1, fs[i].size);
          (fs[i], fs[index]) = (fs[index], fs[i]);
          if (space > 0) fs.Insert(index + 1, (-1, space));
        }
      }

      List<int> result = new List<int>();
      foreach ((int val, int size) f in fs)
      {
        result.AddRange(Enumerable.Repeat(f.val, f.size));
      }
      return result.ToArray();
    }

    public string ArrayToString(int[] array)
    {
      return string.Join("", array.Select(x => x == -1 ? "." : x.ToString()));
    }

    public string ListToString(List<(int val, int size)> fs)
    {
      List<int> intList = new List<int>();
      foreach ((int val, int size) f in fs)
      {
        intList.AddRange(Enumerable.Repeat(f.val, f.size));
      }
      return ArrayToString(intList.ToArray());
    }

  }
}
