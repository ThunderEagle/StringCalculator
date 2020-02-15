using System;
using System.Collections.Generic;
using System.Text;

namespace StringCalculator
{
  public class StringCalculator : IStringCalculator
  {
    private readonly char[] _delimiters = new[] {',', '\n'};

    public int Add(string numbers)
    {
      var total = 0;
      if (!string.IsNullOrEmpty(numbers))
      {
        var numbersToAdd = numbers.Split(_delimiters);
        foreach (var number in numbersToAdd)
        {
          if(int.TryParse(number, out var x))
          {
            total += x;
          }
        }
      }
      return total;
    }

  }
}
