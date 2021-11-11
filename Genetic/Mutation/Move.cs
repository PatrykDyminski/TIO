namespace Genetic.Mutation;

internal class Move : IMutation
{
  public int[] Mutate(int[] genotype)
  {
    Random rnd = new();
    int i = rnd.Next(0, genotype.Length - 1);
    int j = rnd.Next(i + 1, genotype.Length);

    List<int> cut = genotype.ToList().GetRange(i, j - i);
    var newGene = genotype.Except(cut).ToList();
    newGene.InsertRange(rnd.Next(newGene.Count - 1), cut);
    return newGene.ToArray();
  }
}
