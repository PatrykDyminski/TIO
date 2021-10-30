using Genetic.Cross;
using Genetic.Mutation;
using Genetic.Selection;
using System.Numerics;
using Tio.Genetic;

namespace Genetic;

internal class TestRunnerBuilder
{
  private Vector2[] Cites { get; set; }

  private int PopSize { get; set; }
  private int Generations { get; set; }

  private float CrossProb { get; set; }
  private float MutProb { get; set; }

  private IMutation Mutation { get; set; }
  private ICross Cross { get; set; }
  private ISelection Selection { get; set; }

  public TestRunnerBuilder WithCities(Vector2[] cities)
  {
    Cites = cities;
    return this;
  }

  public TestRunnerBuilder WithPopulationSize(int pops)
  {
    PopSize = pops;
    return this;
  }

  public TestRunnerBuilder WithNumberOfGenerations(int geners)
  {
    Generations = geners;
    return this;
  }

  public TestRunnerBuilder WithCrossProb(float crossProb)
  {
    CrossProb = crossProb;
    return this;
  }

  public TestRunnerBuilder WithMutationProb(float mutProb)
  {
    MutProb = mutProb;
    return this;
  }

  public TestRunnerBuilder WithMutation(IMutation mut)
  {
    Mutation = mut;
    return this;
  }

  public TestRunnerBuilder WithCross(ICross cross)
  {
    Cross = cross;
    return this;
  }

  public TestRunnerBuilder WithSelection(ISelection selection)
  {
    Selection = selection;
    return this;
  }

  public (float best, float worst, float avg, float std) BuildAndRunTests(int cycles)
  {
    return Utils.RunTests(
      cycles, 
      Cites, 
      PopSize, 
      Generations, 
      CrossProb, 
      MutProb, 
      Selection, 
      Cross, 
      Mutation);
  }
}
