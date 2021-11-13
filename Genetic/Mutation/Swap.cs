namespace Genetic.Mutation;

public class Swap : IMutation
{
  public int[] Mutate(int[] genotype)
  {
    Random rnd = new();
    int i = rnd.Next(0, genotype.Length);
    int j = rnd.Next(0, genotype.Length);
    (genotype[j], genotype[i]) = (genotype[i], genotype[j]);
    return genotype;
  }
}
