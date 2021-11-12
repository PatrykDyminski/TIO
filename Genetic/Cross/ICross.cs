namespace Genetic.Cross;

public interface ICross
{
  (int[] g1, int[] g2) Cross(int[] parent1, int[] parent2);
}
