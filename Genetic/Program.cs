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

var popSize = 300;
var generations = 400;
var crossProb = 0.8f;
var mutProb = 0.35f;
var tourSize = 10;

int cycles = 5;

var (best, worst, avg, std) = Utils.RunTests(cycles, cities, popSize, generations, crossProb, mutProb, tourSize);
//var testResult = RandomSolution.RandomAlgorithm2(cities, 1000000);
//var testResult = GreedySolution.GreedyAlgorithmAll(cities);

Console.WriteLine("Best: " + best);
Console.WriteLine("Worst: " + worst);
Console.WriteLine("Avg: " + avg);
Console.WriteLine("STD: " + std);