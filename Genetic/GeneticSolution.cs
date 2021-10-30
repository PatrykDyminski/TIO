using Genetic.Cross;
using Genetic.Mutation;
using Genetic.Selection;
using System.Numerics;
using System.Text;

namespace Tio.Genetic;

internal class GeneticSolution
{
  public static (int[] gene, float score) GeneticAlgorithm(
    Vector2[] cities, 
    int popSize, 
    int generations, 
    float crossProb, 
    float mutProb, 
    ISelection selection, 
    ICross cross, 
    IMutation mutation)
  {
    var csv = new StringBuilder();

    var prevPop = Utils.RandomPopulation(popSize, cities.Length);

    float bestScore = float.MaxValue;
    int[] bestGene = new int[cities.Length];

    Random rnd = new();

    for (int i = 0; i < generations; i++)
    {
      var newPop = selection.Select(prevPop, cities);
      int[][] tempPop = new int[popSize][];

      for (int j = 0; j < newPop.Length; j += 2)
      {
        if (rnd.NextDouble() < crossProb)
        {
          var children = cross.Cross(newPop[j], newPop[j + 1]);
          tempPop[j] = children.g1;
          tempPop[j + 1] = children.g2;
        }
        else
        {
          tempPop[j] = newPop[j];
          tempPop[j + 1] = newPop[j + 1];
        }
      }

      for (int k = 0; k < newPop.Length; k++)
      {
        if (rnd.NextDouble() < mutProb)
        {
          tempPop[k] = mutation.Mutate(tempPop[k]);
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
