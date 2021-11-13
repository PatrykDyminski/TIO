using Genetic.Mutation;
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

    //Console.WriteLine(bestScore);
    //Utils.PrintGene(bestGene);

    for (int i = 0; i < iterations; i++)
    {
      //Console.WriteLine("before");
      //Utils.PrintPopulation(initialPopulation);

      var iterationsBestGene = bestGene;
      var iterationBestScore = bestScore;

      //crawl to best food
      for (int j = 0; j < popSize; j++)
      {
        //Utils.PrintGene(bestGene);

        int[] toMove = new int[Cities.Length];
        bestGene.CopyTo(toMove, 0);

        var (gene, score) = Move(initialPopulation[j], toMove, bestScore);
        if (score < iterationBestScore)
        {
          iterationBestScore = score;
          gene.CopyTo(iterationsBestGene, 0);
          //Console.WriteLine("iteration best" + " " + score);
        }
      }

      //if better food found on the way
      if (iterationBestScore < bestScore)
      {
        bestScore = iterationBestScore;
        iterationsBestGene.CopyTo(bestGene, 0);
        //Console.WriteLine("best" + " " + bestScore);
        //Utils.PrintGene(bestGene);
      }

      //Console.WriteLine("after");
      //Utils.PrintPopulation(initialPopulation);

      //search for better food
      var mutation = new Inversion();

      for (int k = 0; k < popSize; k++)
      {
        var toMutate = new int[Cities.Length];
        initialPopulation[k].CopyTo(toMutate, 0);
        var mutated = mutation.Mutate(toMutate);

        var (gene, score) = Move(initialPopulation[k], mutated, bestScore);
        if (score < bestScore)
        {
          bestScore = score;
          gene.CopyTo(bestGene, 0);
          //Console.WriteLine("iteration best" + " " + score);
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

    for (int i = 0; i < citiesNumber; i++)
    {
      if(nestA[i] == nestB[i]) { continue; }

      var SwapIndex = Array.IndexOf(nestA, nestB[i]);
      var (gene, score) = Step(nestA, i, SwapIndex);

      if(score < bestScore)
      {
        bestScore = score; 
        gene.CopyTo(bestNest, 0);
        //Console.WriteLine("move best" + " " + bestScore);
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
