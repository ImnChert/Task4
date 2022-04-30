using System.Text.RegularExpressions;

namespace MethodGaus.Models
{
	public class Slae
	{
		private (int, int) _size;

		public (int, int) Size => _size;
		public double[][] Matrix { get; private set; }

		public Slae(string matrix)
		{
			Matrix = TransformIntoMatrix(matrix);
			_size = FindSize();
		}

		/// <summary>
		/// Copy current matrix and return it.
		/// </summary>
		/// <returns></returns>
		public double[,] Copy()
		{
			var array = new double[_size.Item1, _size.Item2];

			for (int i = 0; i < _size.Item1; i++)
			{
				for (int j = 0; j < _size.Item2; j++)
				{
					array[i, j] = Matrix[i][j];
				}
			}

			return array;
		}

		/// <summary>
		/// Copy square of the current matrix and return it.
		/// </summary>
		/// <returns></returns>
		public double[,] CopySquare()
		{
			var array = new double[_size.Item1, _size.Item1];

			for (int i = 0; i < _size.Item1; i++)
			{
				for (int j = 0; j < _size.Item1; j++)
				{
					array[i, j] = Matrix[i][j];
				}
			}

			return array;
		}

		/// <summary>
		/// Copy last column of the current matrix and return it.
		/// </summary>
		/// <returns></returns>
		public double[] CopyColumn()
		{
			var array = new double[_size.Item1];

			for (int i = 0; i < _size.Item1; i++)
			{
				array[i] = Matrix[i][_size.Item2 - 1];
			}

			return array;
		}

		/// <summary>
		/// Find size of the current matrix.
		/// </summary>
		private (int, int) FindSize()
			=> (Matrix.GetLength(0), Matrix[0].Length);

		/// <summary>
		/// Transform matrix from string to double.
		/// </summary>
		/// <param name="matrix"></param>
		private double[][] TransformIntoMatrix(string matrix)
		{
			matrix = matrix.Replace(" ", string.Empty);

			return Regex.Matches(matrix, @"{(\d+,?)+},?").Cast<Match>()
					  .Select(m => Regex.Matches(m.Groups[0].Value, @"\d+(?=,?)")
					  .Cast<Match>()
					  .Select(ma => double.Parse(ma.Groups[0].Value)).ToArray())
					  .ToArray();
		}
	}
}
