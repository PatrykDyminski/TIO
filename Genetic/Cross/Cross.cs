namespace Genetic.Cross;

internal class Cross : ICross
{
  (int[] g1, int[] g2) ICross.Cross(int[] parent1, int[] parent2)
  {
    int length = parent1.Length;

    Random rnd = new Random();
    int i = rnd.Next(0, length);
    int j = rnd.Next(i, length);

    List<int> cut = parent1.ToList().GetRange(i, j - i + 1);
    List<int> rest = parent2.ToList();
    rest = rest.Except(cut).ToList();
    rest.InsertRange(i, cut);
    int[] g1 = rest.ToArray();

    List<int> cut2 = parent2.ToList().GetRange(i, j - i + 1);
    List<int> rest2 = parent1.ToList();
    rest2 = rest2.Except(cut2).ToList();
    rest2.InsertRange(i, cut2);
    int[] g2 = rest2.ToArray();

    return (g1, g2);
  }
}
