using System.Net;
using System.Net.Sockets;
using System.Text;

namespace TCPIP.Models
{
	public class Client
	{
		private static int s_port = 8005;
		private static string s_address = "127.0.0.1";
		private IPEndPoint _ip;
		private Socket _socket;

		public IPEndPoint IP => _ip;
		public Socket Socket => _socket;

		public Client()
		{
			_ip = new IPEndPoint(IPAddress.Parse(s_address), s_port);
			_socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
		}

		/// <summary>
		/// Create request.
		/// </summary>
		public void CreateRequest()
		{
			_socket.Connect(_ip);

			string message = "";
			byte[] data = Encoding.Unicode.GetBytes(message);

			_socket.Send(data);

			GetAnswer(data);

			CloseServer();
		}

		/// <summary>
		/// Get answer.
		/// </summary>
		/// <param name="data">Data.</param>
		private void GetAnswer(byte[] data)
		{
			data = new byte[256];
			StringBuilder builder = new StringBuilder();

			do
			{
				int bytes = _socket.Receive(data, data.Length, 0);
				builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
			}
			while (_socket.Available > 0);

			Console.WriteLine("ответ сервера: " + builder.ToString());
		}

		/// <summary>
		/// Close server.
		/// </summary>
		private void CloseServer()
		{
			_socket.Shutdown(SocketShutdown.Both);
			_socket.Close();
		}
	}
}
