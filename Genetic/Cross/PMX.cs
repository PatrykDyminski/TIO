using Tio.Genetic;

namespace Genetic.Cross;

internal class PMX : ICross
{
  public (int[] g1, int[] g2) Cross(int[] parent1, int[] parent2)
  {

    //Console.WriteLine("Start");

    int length = parent1.Length;

    Random rnd = new();
    int i = rnd.Next(0, length-1);
    int j = rnd.Next(i+1, length);

    //Console.WriteLine(i + " " + j);

    List<int> cut1 = parent1.ToList().GetRange(i, j - i);
    List<int> cut2 = parent2.ToList().GetRange(i, j - i);

    //Utils.PrintGene(cut1.ToArray());
    //Utils.PrintGene(cut2.ToArray());

    int[] g1 = new int[length];
    int[] g2 = new int[length];

    var p1l = parent1.Except(cut1).ToList();
    var p2l = parent2.Except(cut2).ToList();

    for (int ii = 0; ii < length; ii++)
    {
      if (ii >= i && ii < j)
      {
        g1[ii] = parent1[ii];
      }
      else
      {
        g1[ii] = p1l[0];
      }
      p1l.Remove(g1[ii]);

      if (ii >= i && ii < j)
      {
        g2[ii] = parent2[ii];
      }
      else
      {
        g2[ii] = p2l[0];
      }
      p2l.Remove(g2[ii]);
    }

    //Utils.PrintGene(g1);
    //Utils.PrintGene(g2);

    return (g1, g2);
  }
}
