using MethodGaus.Services;
using System.Text;
using TCPIP.Models;

namespace TCPIP.Services
{
	internal class ProcessData
	{
		private HTTPServer _http;

		public event EventHandler<string> DataReadyToCalculateEvent = null!;
		public Server Server { get; private set; }
		public Client Client { get; private set; }
		public GaussianHandler Gauss { get; }

		public ProcessData(GaussianHandler gaussian, HTTPServer http, Server server, Client client)
		{
			Server = server;
			Client = client;
			Gauss = gaussian;
			_http = http;
			server.DataReceivingEvent += Process;
		}

		/// <summary>
		/// Process data depends on which kind of data this is.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="data">Data.</param>
		public void Process(object? sender, byte[] data)
		{
			var dictionary = new Dictionary<bool, string>()
			{
				{true,  _http.ParseBody()},
				{false, Encoding.UTF8.GetString(data)}
			};

			bool correspondsToValidation = _http.ValidateHttpRequest(data);

			DataReadyToCalculateEvent?.Invoke(this, dictionary[correspondsToValidation]);
		}
	}
}
