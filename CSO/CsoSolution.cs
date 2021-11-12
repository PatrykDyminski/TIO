using System.Numerics;
using System.Text;
using Tio.Genetic;

namespace CSO;

internal class CsoSolution
{
  private Vector2[] Cities { get; set; }

  public CsoSolution(Vector2[] cities)
  {
    Cities = cities;
  }

  public (int[] gene, float score) CSO(
    int popSize,
    int iterations)
  {
    //var csv = new StringBuilder();

    var initialPopulation = Utils.RandomPopulation(popSize, Cities.Length);

    var (bestGene, bestScore) = Utils.BestResultFromPopulation(initialPopulation, Cities);

    for(int i = 0; i < iterations; i++)
    {

      for(int j = 0; j < popSize; j++)
      {
        var (gene, score) = Move(initialPopulation[j], bestGene, bestScore);
        if(score < bestScore)
        {
          bestScore = score;
          bestGene = gene;
        }
      }

    }

    return (bestGene, bestScore);
  }

  public (int[] gene, float score) Move(int[] nestA, int[] nestB, float nestBscore)
  {
    int citiesNumber = nestA.Length;

    var bestScore = nestBscore;
    var bestNest = nestB;

    for(int i = 0; i < citiesNumber; i++)
    {
      if(nestA[i] == nestB[i]) { continue; }

      var SwapIndex = Array.IndexOf(nestA, nestB[i]);
      var (gene, score) = Step(nestA, i, SwapIndex);

      //Utils.PrintGene(gene);
      //Console.WriteLine(score);

      if(score < bestScore)
      {
        bestScore = score; 
        bestNest = gene;
      }
    }

    return (bestNest, bestScore);
  }

  private (int[] gene, float score) Step(int[] nest, int cityA, int cityB)
  {
    (nest[cityA], nest[cityB]) = (nest[cityB], nest[cityA]);
    var distance = Utils.SumDistance(nest, Cities);
    return (nest, distance);
  }

}
