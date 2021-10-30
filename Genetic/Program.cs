using Genetic;
using Genetic.Cross;
using Genetic.Mutation;
using Genetic.Selection;
using System.Numerics;
using Tio.Genetic;

string berlin11 = "berlin11_modified.tsp";
string fl = "fl417.tsp";
string kroA150 = "kroA150.tsp";
string kroA100 = "kroA100.tsp";
string kroA200 = "kroA200.tsp";
string berlin52 = "berlin52.tsp";

string filename = berlin11;
Vector2[] cities = DataReader.ReadFile(filename);

var (best, worst, avg, std) = new TestRunnerBuilder()
  .WithPopulationSize(300)
  .WithNumberOfGenerations(400)
  .WithSelection(new Tour(10))
  .WithCross(new Cross())
  .WithCrossProb(0.8f)
  .WithMutation(new Inversion())
  .WithMutationProb(0.35f)
  //.WithCities(cities)
  .BuildAndRunTests(5);

Console.WriteLine("Best: " + best);
Console.WriteLine("Worst: " + worst);
Console.WriteLine("Avg: " + avg);
Console.WriteLine("STD: " + std);