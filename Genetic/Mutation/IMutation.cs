namespace Genetic.Mutation;

internal interface IMutation
{
  int[] Mutate(int[] genotype);
}
