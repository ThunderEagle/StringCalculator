using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;

namespace StringCalculator
{
  public class StringCalculator : IStringCalculator
  {
    private readonly string[] _defaultDelimiters = new[] { ",", "\n" };

    public int Add(string numbers)
    {
      var total = 0;
      if (!string.IsNullOrEmpty(numbers))
      {
        var delimiters = (numbers.StartsWith("//")) ? GetDelimiters(numbers, out numbers) : _defaultDelimiters;
        var numbersToAdd = numbers.Split(delimiters, StringSplitOptions.None);
        var parsedNumbers = ParseNumbers(numbersToAdd);
        total = parsedNumbers.Sum();
      }
      return total;
    }


    private IEnumerable<int> ParseNumbers(string[] numbers)
    {
      var parsedNumbers = new List<int>();
      var negativeNumbers = new List<int>();
      foreach (var number in numbers)
      {
        if (int.TryParse(number, out var x))
        {
          if (x < 0)
          {
            negativeNumbers.Add(x);
          }
          if (x <= 1000)
          {
            parsedNumbers.Add(x);
          }
        }
      }
      if (negativeNumbers.Any())
      {
        throw new InvalidOperationException($"Negatives not allowed:{string.Join(',', negativeNumbers)}");
      }
      return parsedNumbers;
    }

    private string[] GetDelimiters(string command, out string numbers)
    {
      var lines = command.Split('\n');
      if (lines.Length == 2)
      {
        numbers = lines[1];
        var delimiterLine = lines[0].Substring(2);
        var matchMultiDelimiters = Regex.Matches(delimiterLine, "\\[(.+?)\\]");
        if (matchMultiDelimiters.Count > 0)
        {
          return matchMultiDelimiters.Select(m => m.Groups[1].Value).ToArray();
        }
        return new[] { delimiterLine };
      }
      throw new InvalidOperationException("Improperly formatted input");
    }

  }
}
