using System.Globalization;
using System.Numerics;

namespace Tio.Genetic;

public class DataReader
{
  public static Vector2[] ReadFile(string filename)
  {
    string f = filename;
    var lines = File.ReadAllLines(f);

    char[] delimiterChars = { ' ', '\t' };

    Vector2[] cities = new Vector2[lines.Length];

    foreach (var line in lines)
    {
      var nums = line.Split(delimiterChars, StringSplitOptions.RemoveEmptyEntries);

      var v1 = float.Parse(nums[1], CultureInfo.InvariantCulture.NumberFormat);
      var v2 = float.Parse(nums[2], CultureInfo.InvariantCulture.NumberFormat);

      cities[int.Parse(nums[0]) - 1] = new Vector2(v1, v2);
    }

    return cities;
  }
}

