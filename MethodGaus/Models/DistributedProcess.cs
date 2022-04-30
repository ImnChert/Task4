using MethodGaus.Interfaces;
using MethodGaus.Services;
using System.Diagnostics;

namespace MethodGaus.Models
{
	internal class DistributedProcess : IGaussianProcess
	{
		/// <summary>
		/// Process in distributed version.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="matrix"></param>
		public void Process(object sender, Slae matrix)
		{
			var stopwatch = new Stopwatch();
			stopwatch.Start();
			double[,] a = matrix.CopySquare();
			double[] b = matrix.CopyColumn();
			ForwardElimination(a, b, matrix.Size.Item1);
			double[] results = BackSubstitution(a, b, matrix.Size.Item1);
			stopwatch.Stop();
			ProcessingResults.SetResults(ToString(), results, stopwatch.ElapsedMilliseconds);
		}

		/// <summary>
		/// Forward elimination.
		/// </summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <param name="n"></param>
		private void ForwardElimination(double[,] a, double[] b, int n)
		{
			for (int k = 0; k < n; k++)
			{
				Parallel.For(k + 1, n, j =>
				{
					double d = a[j, k] / a[k, k];

					for (int i = k; i < n; i++)
					{
						a[j, i] = a[j, i] - d * a[k, i];
					}

					b[j] = b[j] - d * b[k];
				});
			}
		}

		/// <summary>
		/// Back substitution.
		/// </summary>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <param name="n"></param>
		/// <returns></returns>
		private double[] BackSubstitution(double[,] a, double[] b, int n)
		{
			var x = new double[n];

			for (int k = n - 1; k >= 0; k--)
			{
				double d = 0;

				for (int j = k + 1; j < n; j++)
				{
					double s = a[k, j] * x[j];
					d += s;
				}

				x[k] = (b[k] - d) / a[k, k];
			}

			return x;
		}

		public override string ToString()
			=> "Distributed";

	}
}
