using System.Numerics;
using Tio.Genetic;

namespace Genetic.Selection;

internal class Russian : ISelection
{
  private readonly int Epsilon;

  public Russian(int epsilon)
  {
    Epsilon = epsilon;
  }

  public int[][] Select(int[][] population, Vector2[] cities)
  {
    float[] fitnessy = new float[population.Length];

    for (int i = 0; i < population.Length; i++)
    {
      fitnessy[i] = Utils.SumDistance(population[i], cities);
    }

    float maxDistance = fitnessy.ToList().Max();

    float[] helperArray = new float[population.Length];

    helperArray[0] = maxDistance - fitnessy[0] + Epsilon;

    for (int j = 1; j < population.Length; j++)
    {
      helperArray[j] = helperArray[j - 1] + maxDistance - fitnessy[j] + Epsilon;
    }

    Random rnd = new();

    int[][] newPop = new int[population.Length][];

    for (int k = 0; k < population.Length; k++)
    {
      var randomNum = rnd.Next(0, (int)helperArray.Last());

      if (randomNum < helperArray[0])
      {
        newPop[k] = population[0];
      }
      else
      {
        for (int l = 1; l < population.Length; l++)
        {
          if (helperArray[l] >= randomNum)
          {
            newPop[k] = population[l];
          }
        }
      }
      //var indexOfGene = Array.IndexOf(helperArray, helperArray.First(x => x >= randomNum));

      //newPop[k] = pop[indexOfGene];
    }

    return newPop;
  }
}

internal class Russian2 : ISelection
{
  private readonly int Scale;
  private readonly int Epsilon;

  public Russian2(int scale, int epsilon)
  {
    Scale = scale;
    Epsilon = epsilon;
  }

  public int[][] Select(int[][] population, Vector2[] cities)
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
      ranges[i] = (max - distances[i]) * Scale + Epsilon;
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
}
