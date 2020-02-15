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


    [TestCase("", 0)]
    [TestCase("52",52)]
    [TestCase("3,8", 11)]
    [TestCase("1,2,3", 6)]
    [TestCase("1,2,3,4", 10)]
    public void Add_VaryingNumberOfNumbers_ReturnsCorrectSum(string numbers, int expected)
    {
      var sut = GetObject();
      var actual = sut.Add(numbers);
      Assert.AreEqual(expected,actual);
    }


    //At step 3, I chose to do this as a separate test cases mainly to call out the different delimiters,
    //this may refactor into the above test since it is currently the same code.
    [TestCase("1\n2", 3)]
    [TestCase("1\n2,3", 6)]
    [TestCase("1\n", 1)]
    [TestCase("15,",15)]
    public void Add_NewLineAndCommaDelimiters_ReturnsCorrectSum(string numbers,int expected)
    {
      var sut = GetObject();
      var actual = sut.Add(numbers);
      Assert.AreEqual(expected,actual);
    }


  }
}
