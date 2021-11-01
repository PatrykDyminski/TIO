using System.Numerics;
using Tio.Genetic;

namespace Genetic.Selection;

internal class Tour2 : ISelection
{
  private readonly int TourSize;

  public Tour2(int tourSize)
  {
    TourSize = tourSize;
  }

  public int[][] Select(int[][] population, Vector2[] cities)
  {
    Random rnd = new();
    var popSize = population.Length;

    (float, int[])[] sumAndInd = new (float, int[])[population.Length];

    for (int i = 0; i < popSize; i++)
    {
      var sum = Utils.SumDistance(population[i], cities);
      sumAndInd[i] = (sum, population[i]);
    }

    int[][] selectedPop = new int[popSize][];

    for (int i = 0; i < popSize; i++)
    {
      selectedPop[i] = sumAndInd
        .OrderBy(x => rnd.Next())
        .Take(TourSize)
        .MinBy(x => x.Item1)
        .Item2;
    }

    return selectedPop;
  }
}
