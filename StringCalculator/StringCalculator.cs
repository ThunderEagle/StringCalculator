using System;
using System.Collections.Generic;
using System.Text;

namespace StringCalculator
{
  public class StringCalculator : IStringCalculator
  {
    public int Add(string numbers)
    {
      var total = 0;
      if (!string.IsNullOrEmpty(numbers))
      {
        var numbersToAdd = numbers.Split(',');
        if(numbersToAdd.Length > 2)
        {
          throw new InvalidOperationException("Only provide 0, 1 or 2 numbers.");
        }
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
