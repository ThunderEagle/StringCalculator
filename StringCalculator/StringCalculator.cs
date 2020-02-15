using System;
using System.Collections.Generic;
using System.Text;

namespace StringCalculator
{
  public class StringCalculator : IStringCalculator
  {
    private readonly char[] _defaultDelimiters = new[] { ',', '\n' };

    public int Add(string numbers)
    {
      var total = 0;
      if (!string.IsNullOrEmpty(numbers))
      {
        var delimiters = (numbers.StartsWith("//")) ? GetDelimiter(numbers, out numbers) : _defaultDelimiters;
        var numbersToAdd = numbers.Split(delimiters);
        foreach (var number in numbersToAdd)
        {
          if (int.TryParse(number, out var x))
          {
            total += x;
          }
        }
      }
      return total;
    }

    private char[] GetDelimiter(string command, out string numbers)
    {
      var lines = command.Split('\n');
      if (lines.Length == 2)
      {
        numbers = lines[1];
        return new[] { lines[0][2] };
      }
      throw new InvalidOperationException("Improperly formatted input");
    }

  }
}
