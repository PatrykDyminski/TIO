using Genetic.Cross;
using Genetic.Mutation;
using Genetic.Selection;
using System.Numerics;
using System.Text;

namespace Tio.Genetic;

internal class GeneticSolution
{
  public static int[][] RussianSelect(int[][] pop, int epsilon, Vector2[] cities)
  {
    //Console.WriteLine("inputPop");
    //Utils.PrintPopulation(pop);

    float[] fitnessy = new float[pop.Length];

    for (int i = 0; i < pop.Length; i++)
    {
      fitnessy[i] = Utils.SumDistance(pop[i], cities);
    }

    //fitnessy.ToList().ForEach(i => Console.WriteLine(i.ToString()));

    float maxDistance = fitnessy.ToList().Max();

    float[] helperArray = new float[pop.Length];

    helperArray[0] = maxDistance - fitnessy[0] + epsilon;

    for (int j = 1; j < pop.Length; j++)
    {
      helperArray[j] = helperArray[j - 1] + maxDistance - fitnessy[j] + epsilon;
    }


    Random rnd = new();

    int[][] newPop = new int[pop.Length][];

    for (int k = 0; k < pop.Length; k++)
    {
      var randomNum = rnd.Next(0, (int)helperArray.Last());

      if (randomNum < helperArray[0])
      {
        newPop[k] = pop[0];
      }
      else
      {
        for (int l = 1; l < pop.Length; l++)
        {
          if (helperArray[l] >= randomNum)
          {
            newPop[k] = pop[l];
          }
        }
      }

      //var indexOfGene = Array.IndexOf(helperArray, helperArray.First(x => x >= randomNum));

      //newPop[k] = pop[indexOfGene];

    }

    return newPop;
  }

  public static int[][] RussianSelect2(int[][] population, int epsilon, Vector2[] cities, int scale)
  {
    float[] distances = new float[population.Length];

    for (int i = 0; i < population.Length; i++)
    {
      distances[i] = Utils.SumDistance(population[i], cities);
    }

    int[][] result = new int[population.Length][];

    float[] ranges = new float[population.Length];
    float max = 0, sum = 0;

    for (int i = 0; i < population.Length; i++)
    {
      if (distances[i] > max)
      {
        max = distances[i];
      }
    }

    for (int i = 0; i < population.Length; i++)
    {
      ranges[i] = (max - distances[i]) * scale + epsilon;
      sum += ranges[i];
      if (i > 0) ranges[i] += ranges[i - 1];
    }

    for (int i = 0; i < population.Length; i++)
    {
      ranges[i] = ranges[i] / sum;
    }

    Random rnd = new();

    double chosen; bool found;
    for (int i = 0; i < population.Length; i++)
    {
      chosen = rnd.NextDouble(); found = false;
      for (int j = 0; j < population.Length && !found; j++)
      {
        if (chosen < ranges[j])
        {
          result[i] = new int[population[j].Length];
          //Array.Copy(population[j], result[i], population[j].Length);
          result[i] = (int[])population[j].Clone();
          found = true;
        }
      }
    }
    return result;
  }

  public static (int[] gene, float score) GeneticAlgorithm(Vector2[] cities, int popSize, int generations, float crossProb, float mutProb, int tourSize)
  {
    var csv = new StringBuilder();

    var prevPop = Utils.RandomPopulation(popSize, cities.Length);

    float bestScore = float.MaxValue;
    int[] bestGene = new int[cities.Length];

    Random rnd = new();

    ICross cross = new Cross();
    ISelection select = new Tour(tourSize);

    for (int i = 0; i < generations; i++)
    {
      var newPop = select.Select(prevPop, cities);
      //var newPop = RussianSelect2(prevPop, 1, cities, 1);
      int[][] tempPop = new int[popSize][];

      for (int j = 0; j < newPop.Length; j += 2)
      {
        if (rnd.NextDouble() < crossProb)
        {
          var dzieci = cross.Cross(newPop[j], newPop[j + 1]);
          tempPop[j] = dzieci.g1;
          tempPop[j + 1] = dzieci.g2;
        }
        else
        {
          tempPop[j] = newPop[j];
          tempPop[j + 1] = newPop[j + 1];
        }
      }

      var inversion = new Inversion();

      for (int k = 0; k < newPop.Length; k++)
      {
        if (rnd.NextDouble() < mutProb)
        {
          tempPop[k] = inversion.Mutate(tempPop[k]);
        }
      }

      //Utils.PrintPopulation(tempPop);
      prevPop = tempPop.Select(a => a.ToArray()).ToArray();

      var bestInPop = Utils.BestResultFromPopulation(prevPop, cities);
      var worstInPop = Utils.WorstResultFromPopulation(prevPop, cities);
      var avgInPop = Utils.AvgResultFromPopulation(prevPop, cities);

      var newLine = string.Format("{0};{1};{2};{3};;;", i, bestInPop.score, avgInPop, worstInPop.score);
      csv.AppendLine(newLine);

      if (bestInPop.score < bestScore)
      {
        bestScore = bestInPop.score;
        bestGene = bestInPop.gene;
      }
    }

    File.WriteAllText("results.csv", csv.ToString());

    return (bestGene, bestScore);
  }
}
