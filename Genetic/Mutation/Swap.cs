namespace Genetic.Mutation;

internal class Swap : IMutation
{
  public int[] Mutate(int[] genotype)
  {
    Random rnd = new Random();
    int i = rnd.Next(0, genotype.Length);
    int j = rnd.Next(0, genotype.Length);
    int v = genotype[i];
    genotype[i] = genotype[j];
    genotype[j] = v;

    return genotype;
  }
}
