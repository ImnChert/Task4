using MethodGaus.Interfaces;
using MethodGaus.Services;
using System.Diagnostics;

namespace MethodGaus.Models
{
	public class LinearProcess : IGaussianProcess
	{
		/// <summary>
		/// Process matrix in linear version.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="matrix"></param>
		public void Process(object sender, Slae matrix)
		{
			var stopwatch = new Stopwatch();
			stopwatch.Start();
			double[,] a = matrix.Copy();
			ForwardElimination(a, matrix.Size.Item1);
			double[] results = BackSubstitution(a, matrix.Size.Item1);
			stopwatch.Stop();
			ProcessingResults.SetResults(ToString(), results, stopwatch.ElapsedMilliseconds);
		}

		/// <summary>
		/// Forward elimination.
		/// </summary>
		/// <param name="a"></param>
		/// <param name="orderOfTheMatrix"></param>
		private void ForwardElimination(double[,] a, int orderOfTheMatrix)
		{
			for (int i = 0; i < orderOfTheMatrix; i++)
			{
				for (int j = i + 1; j < orderOfTheMatrix; j++)
				{
					double ratio = a[j, i] / a[i, i];

					for (int k = 0; k <= orderOfTheMatrix; k++)
					{
						a[j, k] -= ratio * a[i, k];
					}
				}
			}
		}

		/// <summary>
		/// Back substition.
		/// </summary>
		/// <param name="a"></param>
		/// <param name="ordeOfTheMatrix"></param>
		/// <returns></returns>
		private static double[] BackSubstitution(double[,] a, int ordeOfTheMatrix)
		{
			var x = new double[ordeOfTheMatrix];

			x[ordeOfTheMatrix - 1] = a[ordeOfTheMatrix - 1, ordeOfTheMatrix] / a[ordeOfTheMatrix - 1, ordeOfTheMatrix - 1];

			for (int k = ordeOfTheMatrix - 2; k >= 0; k--)
			{
				double sum = 0;

				for (int j = k + 1; j < ordeOfTheMatrix; j++)
				{
					sum += a[k, j] * x[j];
				}

				x[k] = (a[k, ordeOfTheMatrix] - sum) / a[k, k];
			}

			return x;
		}

		public override string ToString()
			=> "Linear";

	}
}
