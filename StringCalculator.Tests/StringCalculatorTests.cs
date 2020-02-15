using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace StringCalculator
{
  [TestFixture]
  public class StringCalculatorTests
  {
    //I typically create a private method in my test classes that does nothing but get the object.  It probably isn't necessary
    //in this case, but it offers a single place to deal with dependency injection into the object, and also ensures I'm testing against
    //the interface of the object if we have decided to implement one, and I like that because I expected all consumers of the object to be
    //written and called against the interface.
    private IStringCalculator GetObject()
    {
      return new StringCalculator();
    }

    //I choose to keep the separate unit tests even though they all are performing the same logic.
    //I did this because the name of the tests are giving me context to the data and logic that is being exercised, 
    //so this is actually giving me a lot more information than just running all test cases through one generically named test method.


    [TestCase("", 0)]
    [TestCase("52",52)]
    [TestCase("3,8", 11)]
    [TestCase("1,2,3", 6)]
    [TestCase("1,2,3,4", 10)]
    public void Add_VaryingNumberOfNumbers_ReturnsCorrectSum(string numbers, int expected)
    {
      var sut = GetObject();
      var actual = sut.Add(numbers);
      Assert.That(actual, Is.EqualTo(expected));
    }

    [TestCase("1\n2", 3)]
    [TestCase("1\n2,3", 6)]
    [TestCase("1\n", 1)]
    [TestCase("15,",15)]
    public void Add_NewLineAndCommaDelimiters_ReturnsCorrectSum(string numbers,int expected)
    {
      var sut = GetObject();
      var actual = sut.Add(numbers);
      Assert.That(actual, Is.EqualTo(expected));
    }

    [TestCase("//;\n3", 3)]
    [TestCase("//;\n1;2;3", 6)]
    [TestCase("//x\n1x2x3x4", 10)]
    public void Add_CustomDelimiter_ReturnsCorrectSum(string numbers,int expected)
    {
      var sut = GetObject();
      var actual = sut.Add(numbers);
      Assert.That(actual, Is.EqualTo(expected));
    }

    [TestCase("-1,2", "-1")]
    [TestCase("-1,2,-3", "-1,-3")]
    [TestCase("-2", "-2")]
    [TestCase("//;\n-3;2;-1", "-3,-1")]  
    public void Add_NegativeNumbers_ExceptionMessageContainsListOfAllNegativeNumbers(string numbers, string listOfNegativeNumbers)
    {
      var sut = GetObject();
      var ex = Assert.Throws<InvalidOperationException>(() => sut.Add(numbers));
      Assert.That(ex.Message.Split(':')[1], Is.EqualTo(listOfNegativeNumbers));
    }

    [TestCase("1000,1",1001)]
    [TestCase("1001,1",1)]
    [TestCase("1,2\n3,5000",6)]
    [TestCase("//;\n3000;2;1", 3)]  
    [TestCase("1000",1000)]
    [TestCase("3000",0)]
    public void Add_NumbersGreaterThan1000_IgnoreNumbersGreaterThan1000(string numbers, int expected)
    {
      var sut = GetObject();
      var actual = sut.Add(numbers);
      Assert.That(actual, Is.EqualTo(expected));      
    }

    [TestCase("//[|||]\n3", 3)]
    [TestCase("//[|||]\n1|||2|||3", 6)]
    public void Add_AnyLengthDelimiter_ReturnsCorrectSum(string numbers, int expected)
    {
      var sut = GetObject();
      var actual = sut.Add(numbers);
      Assert.That(actual, Is.EqualTo(expected));      
    }

    [TestCase("//[|][%]\n1|2%3", 6)]
    public void Add_MultipleDelimiters_ReturnsCorrectSum(string numbers, int expected)
    {
      var sut = GetObject();
      var actual = sut.Add(numbers);
      Assert.That(actual, Is.EqualTo(expected));      
    }
  }
}
