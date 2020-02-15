# StringCalculator Kata

Instructions: [https://github.com/ardalis/kata-catalog/blob/master/katas/String Calculator.md](rl(https://github.com/ardalis/kata-catalog/blob/master/katas/String%20Calculator.md))

### Notes:
They way I chose to implement the unit tests may look a little strange.  Most of them except for the ones checking for the exceptions are all the same logic.  However the naming of the tests and the specific TestCase data I'm using for each one produces a detailed account of what may have failed and what exactly is being tested.  I could have had all the same tests with just 2 test methods and many, many TestCase attributes, but I felt multiple test methods more accurately reflected the different requirements of the exercise and produces a much more useful output, especially when one may fail.