using MethodGaus.Interfaces;
using MethodGaus.Models;

namespace MethodGaus.Services
{
	public class GaussianHandler
	{
		public event EventHandler<Slae> DataCalculateEvent = null!;

		/// <summary>
		/// Invoke all subscribed processors of the Slae.
		/// </summary>
		/// <param name="matrix">Matrix.</param>
		public void GaussSolve(string matrix)
		{
			DataCalculateEvent?.Invoke(this, new Slae(matrix));
		}

		/// <summary>
		/// Connect processor.
		/// </summary>
		/// <param name="process"></param>
		public void ConnectProcess(IGaussianProcess process)
		{
			DataCalculateEvent += process.Process;
		}

		/// <summary>
		/// Disconnect processor.
		/// </summary>
		/// <param name="process"></param>
		public void DisconnectProcess(IGaussianProcess process)
		{
			DataCalculateEvent -= process.Process;
		}
	}
}
