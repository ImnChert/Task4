using System.Net;
using System.Net.Sockets;
using System.Text;

namespace TCPIP.Models
{
	internal class Server
	{
		private static int s_port = 8005;
		private Socket _socket;
		private IPEndPoint _ip;

		public event EventHandler<byte[]> DataReceivingEvent = null!;

		public Server()
		{
			_socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			_ip = new IPEndPoint(IPAddress.Parse("127.0.0.1"), s_port);
		}

		/// <summary>
		/// Start server.
		/// </summary>
		public void StartServer()
		{
			_socket.Bind(_ip);
			_socket.Listen(10);

			while (true)
			{
				Socket handler = _socket.Accept();
				StringBuilder builder = new StringBuilder();
				byte[] data = new byte[256];

				do
				{
					DataReceivingEvent?.Invoke(this, data);
				}
				while (handler.Available > 0);

				handler.Send(data);

				CloseServer(handler);
			}
		}

		/// <summary>
		/// Colose server.
		/// </summary>
		/// <param name="handler">Socket.</param>
		private void CloseServer(Socket handler)
		{
			handler.Shutdown(SocketShutdown.Both);
			handler.Close();
		}
	}
}