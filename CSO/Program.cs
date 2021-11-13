using CSO;
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

var CSO = new CsoSolution(cities);

var res = CSO.CSO(100, 1000);

Console.WriteLine(res.score);
//Utils.PrintGene(res.gene);

var cityA = new int[] { 1, 2, 3, 4, 5 };
var cityB = new int[] { 4, 3, 5, 1, 2 };

//CSO.Move(cityA, cityB);

