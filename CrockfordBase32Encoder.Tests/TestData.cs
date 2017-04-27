using System.Collections.Generic;

namespace CrockfordBaseEncoder.Tests
{
    internal static class TestData
    {
        internal static IEnumerable<object[]> TestRows()
        {
            yield return new object[] { 0, "0", "0"  };
            yield return new object[] { 1, "1", "1"  };
            yield return new object[] { 2, "2", "2"  };
            yield return new object[] { 3, "3", "3"  };
            yield return new object[] { 4, "4", "4"  };
            yield return new object[] { 5, "5", "5"  };
            yield return new object[] { 6, "6", "6"  };
            yield return new object[] { 7, "7", "7"  };
            yield return new object[] { 8, "8", "8"  };
            yield return new object[] { 9, "9", "9" };
            yield return new object[] { 10, "A", "A"  };
            yield return new object[] { 11, "B", "B"  };
            yield return new object[] { 12, "C", "C"  };
            yield return new object[] { 13, "D", "D"  };
            yield return new object[] { 14, "E", "E"  };
            yield return new object[] { 15, "F", "F"  };
            yield return new object[] { 16, "G", "G"  };
            yield return new object[] { 17, "H", "H"  };
            yield return new object[] { 18, "J", "J"  };
            yield return new object[] { 19, "K", "K"  };
            yield return new object[] { 20, "M", "M"  };
            yield return new object[] { 21, "N", "N"  };
            yield return new object[] { 22, "P", "P"  };
            yield return new object[] { 23, "Q", "Q"  };
            yield return new object[] { 24, "R", "R"  };
            yield return new object[] { 25, "S", "S"  };
            yield return new object[] { 26, "T", "T"  };
            yield return new object[] { 27, "V", "V"  };
            yield return new object[] { 28, "W", "W"  };
            yield return new object[] { 29, "X", "X"  };
            yield return new object[] { 30, "Y", "Y"  };
            yield return new object[] { 31, "Z", "Z" };
            yield return new object[] { 32, "10", "*"  };
            yield return new object[] { 33, "11", "~"  };
            yield return new object[] { 34, "12", "$"  };
            yield return new object[] { 35, "13", "="  };
            yield return new object[] { 36, "14", "U"  };
            yield return new object[] { 37, "15", "0"  };
            yield return new object[] { 64, "20", "V" };
            yield return new object[] { 468, "EM", "R" };
            yield return new object[] { 3783, "3P7", "9" };
            yield return new object[] { 4546, "4E2", "*" };
            yield return new object[] { 65535, "1ZZZ", "8" };
            yield return new object[] { 18446744073709551615, "FZZZZZZZZZZZZ", "B" };
        }
    }
}
