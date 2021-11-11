namespace Genetic.Mutation;

internal class Move : IMutation
{
  public int[] Mutate(int[] genotype)
  {
    Random rnd = new();
    int i = rnd.Next(0, genotype.Length);
    int j = rnd.Next(0, genotype.Length);

    if (i > j)
    {
      int temp = i;
      i = j;
      j = temp;
    }

    Array.Reverse(genotype, i, j - i + 1);

    return genotype;
  }
}
