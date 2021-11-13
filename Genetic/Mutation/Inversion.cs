namespace Genetic.Mutation;

public class Inversion : IMutation
{
  public int[] Mutate(int[] genotype)
  {
    int length = genotype.Length;

    Random rnd = new();
    int i = rnd.Next(0, length - 1);
    int j = rnd.Next(i + 1, length);

    Array.Reverse(genotype, i, j - i + 1);

    return genotype;
  }
}
