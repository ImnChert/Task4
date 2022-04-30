using MethodGaus.Models;
using MethodGaus.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
	[TestClass]
	public class LinearProcessTests
	{
		[TestMethod]
		public void SolutionOfLinearSlae_SuccessfulSolution()
		{

			string actual = SolutionOfLinearSlae("{{ 3,5,5,19},{ 4,7,9,10},{ 2,5,6,7}}");
			string expected = "4,111111111111093,9,222222222222234,-7,88888888888889";

			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void SolutionOfLinearSlae_NotSuccessfulSolution()
		{
			string actual = SolutionOfLinearSlae("{{ 3,2,5,59},{ 4,7,9,10},{ 2,7,6,7}}");
			string expected = "4,11111111,9,22222,-7";

			Assert.AreNotEqual(expected, actual);
		}

		private string SolutionOfLinearSlae(string matrix)
		{
			ProcessingResults.Reset();
			var gaussian = new GaussianHandler();
			var process = new LinearProcess();

			gaussian.ConnectProcess(process);

			gaussian.GaussSolve(matrix);

			return ProcessingResults.Solution;
		}
	}
}
