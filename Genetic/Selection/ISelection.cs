using System.Numerics;

namespace Genetic.Selection;

internal interface ISelection
{
  int[][] Select(int[][] population, Vector2[] cities);
}
