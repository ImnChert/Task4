namespace MethodGaus.Services
{
	public class ProcessingResults
	{
		static private string _solution = null!;
		static private string _timeUsage = null!;

		static public string Solution => _solution;
		static public string TimeUsage => _timeUsage;

		/// <summary>
		/// Set result of the given process.
		/// </summary>
		/// <param name="sender">Name of the process.</param>
		/// <param name="res">result.</param>
		/// <param name="time">used time to process.</param>
		static public void SetResults(string sender, double[] res, long time)
		{
			if (_solution == null)
				_solution = String.Join(",", res);

			if (_timeUsage == null)
				_timeUsage += sender + ":" + $"{Convert.ToString(time)}";
			else
				_timeUsage += ", " + sender + ":" + $"{Convert.ToString(time)}";
		}

		/// <summary>
		/// Get current results.
		/// </summary>
		/// <returns>Result</returns>
		static public string GetResult() => _solution + ". " + _timeUsage;

		/// <summary>
		/// Reset result.
		/// </summary>
		static public void Reset()
		{
			_solution = null;
			_timeUsage = null;
		}
	}
}
