namespace tests;

[TestClass]
public class Processor2Test : code.Processor2
{
//   [TestMethod]
//   public void ExampleTest1()
//   {

//     string input = @"seeds: 70 31 15 1

//   seed-to-soil map:
//   79 70 14
//   1 15 1

// soil-to-fertilizer map:
// 0 15 37";

//     var expected = 1;
//     var actual = code.Processor2.Process(input);
//     Assert.AreEqual(expected, actual);
//   }
  [TestMethod]
  public void ExampleTest2()
  {
    string input = File.ReadAllText("input5test.txt");

    var expected = 46;
    var actual = code.Processor2.Process(input);
    Assert.AreEqual(expected, actual);
  }

  [TestMethod]
  [Timeout(6000000)] // 100 minutes
  public void GetQuestion2AnswerTest()
  {
    string input = File.ReadAllText("input5.txt");
    var expected = 0;
    var actual = code.Processor2.Process(input);
    Assert.AreEqual(expected, actual);
  }
}
