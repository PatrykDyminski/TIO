using System.Numerics;
using Tio.Genetic;

namespace Genetic.Selection;

internal class Tour : ISelection
{
  private readonly int TourSize;

  public Tour(int tourSize)
  {
    TourSize = tourSize;
  }

  public int[][] Select(int[][] pop, Vector2[] cities)
  {
    //Console.WriteLine("inputPop");
    //Utils.PrintPopulation(pop);

    var popSize = pop.Length;

    float[] sums = new float[popSize];

    //liczenie wszytkich sum
    for (int i = 0; i < popSize; i++)
    {
      sums[i] = Utils.SumDistance(pop[i], cities);
    }

    //sums.ToList().ForEach(i => Console.WriteLine(i.ToString()));

    int[][] selectedPop = new int[popSize][];

    //wybór tylu osobników ile jest w populacji
    for (int i = 0; i < popSize; i++)
    {
      Random rnd = new();

      int[] randomSelected = new int[TourSize];

      //losowanie osobników do selekcji
      for (int j = 0; j < TourSize; j++)
      {
        int k = rnd.Next(0, popSize);
        randomSelected[j] = k;
      }

      //wybór najlepszego z losowo wybranych
      int bestIndex = 0;
      float bestScore = sums[randomSelected[0]];
      for (int l = 1; l < TourSize; l++)
      {
        float tempScore = sums[randomSelected[l]];
        if (tempScore < bestScore)
        {
          bestScore = tempScore;
          //TU COS ZLE MOZE BYC
          bestIndex = randomSelected[l];
        }
      }
      selectedPop[i] = pop[bestIndex];
    }

    //Console.WriteLine("selectedPop:");
    //Utils.PrintPopulation(selectedPop);

    return selectedPop;
  }
}
