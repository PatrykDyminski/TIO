using Genetic.Cross;
using Genetic.Mutation;
using Genetic.Selection;
using System.Numerics;

namespace Tio.Genetic;

internal class Utils
{
  public static int[] RandomGene(int size)
  {
    Random rnd = new();
    return Enumerable
      .Range(0, size)
      .OrderBy(x => rnd.Next())
      .ToArray();
  }

  public static int[][] RandomPopulation(int popSize, int geneSize)
  {
    return Enumerable
      .Range(0, popSize)
      .Select(x => RandomGene(geneSize))
      .ToArray();
  }

  public static (int[] gene, float score) BestResultFromPopulation(int[][] pop, Vector2[] cities)
  {
    return pop
      .Select(x => (x, SumDistance(x, cities)))
      .MinBy(x => x.Item2);
  }

  public static (int[] gene, float score) WorstResultFromPopulation(int[][] pop, Vector2[] cities)
  {
    return pop
      .Select(x => (x, SumDistance(x, cities)))
      .MaxBy(x => x.Item2);
  }

  public static float AvgResultFromPopulation(int[][] pop, Vector2[] cities)
  {
    float cumulate = 0f;

    foreach (var gene in pop)
    {
      cumulate += SumDistance(gene, cities);
    }

    return cumulate / pop.Length;
  }

  public static float SumDistance(int[] gene, Vector2[] cities)
  {
    float sum = 0;

    for (int i = 0; i < gene.Length - 1; i++)
    {
      sum += Vector2.Distance(cities[gene[i]], cities[gene[i + 1]]);
    }

    return sum + Vector2.Distance(cities[gene[0]], cities[gene[cities.Length - 1]]);
  }

  public static void PrintGene(int[] gene)
  {
    foreach (var elem in gene)
    {
      Console.Write(elem + " ");
    }
    Console.WriteLine();
  }

  public static void PrintPopulation(int[][] pop)
  {
    Console.WriteLine("Population:");
    foreach (var gene in pop)
    {
      PrintGene(gene);
    }
    Console.WriteLine("End of Population");
  }

  public static (float best, float worst, float avg, float std) RunTests(
    int cycles, 
    Vector2[] cities, 
    int popSize, 
    int generations, 
    float crossProb, 
    float mutProb, 
    ISelection selection,
    ICross cross,
    IMutation mutation)
  {

    float[] results = new float[cycles];

    for (int i = 0; i < cycles; i++)
    {
      var (gene, score) = GeneticSolution.GeneticAlgorithm(
        cities, 
        popSize, 
        generations, 
        crossProb, 
        mutProb, 
        selection, 
        cross, 
        mutation);

      results[i] = score;

      Console.WriteLine(score);
      //Utils.PrintGene(gene);
    }

    float best = results.ToList().Min();
    float worst = results.ToList().Max();
    float avg = results.ToList().Average();
    float std = StdDev(results.ToList());

    return (best, worst, avg, std);
  }

  public static float StdDev(List<float> values)
  {
    float ret = 0.0f;
    int count = values.Count;
    if (count > 1)
    {
      //Compute the Average
      float avg = values.Average();

      //Perform the Sum of (value-avg)^2
      float sum = values.Sum(d => (d - avg) * (d - avg));

      //Put it all together
      ret = (float)Math.Sqrt(sum / count);
    }

    return ret;
  }
}
