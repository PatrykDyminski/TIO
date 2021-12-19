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
    var csv = new StringBuilder();

    var population = Utils.RandomPopulation(popSize, Cities.Length);

    var (bestNest, bestScore) = Utils.BestResultFromPopulation(population, Cities);

    var allTimeBestNest = bestNest;
    var allTimeBestScore = bestScore;

    var sameResultFor = 0;

    //iterations
    for (int i = 0; i < iterations; i++)
    {
      Console.WriteLine(bestScore);

      var iterationsBestNest = bestNest;
      var iterationBestScore = bestScore;

      //crawl to best food for each population member
      for (int j = 0; j < popSize; j++)
      {
        var (nest, score) = Move(population[j], bestNest, bestScore);
        if (score < iterationBestScore)
        {
          iterationBestScore = score;
          nest.CopyTo(iterationsBestNest, 0);
        }
      }

      //if better food found on the way
      if (iterationBestScore < bestScore)
      {
        bestScore = iterationBestScore;
        iterationsBestNest.CopyTo(bestNest, 0);
      }

      //search for better food
      for (int k = 0; k < popSize; k++)
      {
        //get new direction for corkroach
        var newDirection = GetNewDirection(population[k]);

        //go there
        var (nest, score) = Move(population[k], newDirection, bestScore);
        if (score < bestScore)
        {
          iterationBestScore = score;
          nest.CopyTo(iterationsBestNest, 0);
        }
      }

      //if better food found on the way
      if (iterationBestScore < bestScore)
      {
        bestScore = iterationBestScore;
        iterationsBestNest.CopyTo(bestNest, 0);
      }
      else
      {
        sameResultFor++;
      }

      //save best score
      if(bestScore < allTimeBestScore)
      {
        allTimeBestNest = bestNest;
        allTimeBestScore = bestScore;
      }

      //find new food when best has been exhausted
      if(sameResultFor >= 10)
      {
        sameResultFor = 0;
        Console.WriteLine("Ding");

        allTimeBestNest = bestNest;
        allTimeBestScore = bestScore;

        population = Utils.RandomPopulation(popSize, Cities.Length);
        (bestNest, bestScore) = Utils.BestResultFromPopulation(population, Cities);
      }

      var worstInPop = Utils.WorstResultFromPopulation(population, Cities);
      var avgInPop = Utils.AvgResultFromPopulation(population, Cities);
      var newLine = string.Format("{0};{1};{2};{3};;;", i, bestScore, avgInPop, worstInPop.score);
      csv.AppendLine(newLine);
    }

    File.WriteAllText("results.csv", csv.ToString());

    return (allTimeBestNest, allTimeBestScore);
  }

  private int[] GetNewDirection(int[] nest)
  {
    var random = new Random();

    if(random.NextDouble() < 0.05)
    {
      return Utils.RandomGene(Cities.Length);
    }

    var mutation = new Inversion();
    var toMutate = new int[Cities.Length];
    nest.CopyTo(toMutate, 0);
    return mutation.Mutate(toMutate);
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
    //Console.WriteLine("step: " + cityA + " " + cityB);

    (nest[cityA], nest[cityB]) = (nest[cityB], nest[cityA]);
    var distance = Utils.SumDistance(nest, Cities);
    return (nest, distance);
  }
}
