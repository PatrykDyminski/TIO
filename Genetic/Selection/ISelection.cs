using System.Numerics;

namespace Genetic.Selection;

public interface ISelection
{
  int[][] Select(int[][] population, Vector2[] cities);
}
