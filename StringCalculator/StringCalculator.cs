using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
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
        var parsedNumbers = ParseNumbers(numbersToAdd);
        foreach (var number in parsedNumbers)
        {
          total += number;
        }
      }
      return total;
    }


    private IEnumerable<int> ParseNumbers(string[] numbers)
    {
      var parsedNumbers = new List<int>();
      var negativeNumbers = new List<int>();
      foreach(var number in numbers)
      {
        if(int.TryParse(number, out var x))
        {
          if(x < 0)
          {
            negativeNumbers.Add(x);
          }
          parsedNumbers.Add(x);
        }
      }
      if(negativeNumbers.Any())
      {
        throw new InvalidOperationException($"Negatives not allowed:{string.Join(',',negativeNumbers)}");
      }
      return parsedNumbers;
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
