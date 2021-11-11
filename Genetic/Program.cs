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

string filename = berlin52;
Vector2[] cities = DataReader.ReadFile(filename);

var (best, worst, avg, std) = new TestRunnerBuilder()
  .WithPopulationSize(100)
  .WithNumberOfGenerations(500)
  .WithSelection(new Tour(40))
  .WithCross(new PMX())
  .WithCrossProb(0.8f)
  .WithMutation(new Move())
  .WithMutationProb(0.4f)
  .WithCities(cities)
  .BuildAndRunTests(5);

Console.WriteLine("Best: " + best);
Console.WriteLine("Worst: " + worst);
Console.WriteLine("Avg: " + avg);
Console.WriteLine("STD: " + std);

//int[] p1 = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11,12,13,14,15,16,17,18,19 };
//int[] p2 = new int[] { 4, 3, 1, 2, 5 };
//
//var cross = new PMX();
//var move = new Move();
//var mutated = move.Mutate(p1);
//Utils.PrintGene(mutated);

//var res = cross.Cross(p1, p2);