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

    [Test]
    public void Add_EmptyString_ReturnsZero()
    {
      var sut = GetObject();
      var actual = sut.Add(string.Empty);

      Assert.AreEqual(0, actual);

    }
  }
}
