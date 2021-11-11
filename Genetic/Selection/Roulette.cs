using System.Numerics;
using Tio.Genetic;

namespace Genetic.Selection;

internal class Roulette : ISelection
{
  public int[][] Select(int[][] population, Vector2[] cities)
  {
    var rand = new Random();

    int currentSize = population.Length;
    int[][] newPopulation = new int[currentSize][];
    float[] summedDistances = new float[currentSize];

    double fitnessSum = 0;
    for(int i = 0; i < currentSize; i++)
    {
      summedDistances[i] = (float)Math.Pow(1 / Utils.SumDistance(population[i], cities), 10f);
      //summedDistances[i] = (float)((1 / Utils.SumDistance(population[i], cities)) * 1000000 + 5000);
      fitnessSum += summedDistances[i];
    }

    double[] rangeMax = new double[currentSize];
    double s = 0;

    for (int i = 0; i < currentSize; i++)
    {
      s += summedDistances[i] / fitnessSum;
      rangeMax[i] = s;
    }

    for (int j = 0; j < currentSize; j++)
    {
      double wheelValue = rand.NextDouble();
      for (int i = 0; i < currentSize; i++)
      {
        if (wheelValue <= rangeMax[i])
        {
          newPopulation[j] = (int[])population[i].Clone();
          break;
        }
      }
    }

    return newPopulation;
  }
}
