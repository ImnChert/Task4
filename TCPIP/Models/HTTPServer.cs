using System.Text;
using System.Text.RegularExpressions;

namespace TCPIP.Models
{
	internal class HTTPServer : Server
	{
		private string? _possibleHeader = null!;
		private string? _possibleBody = null!;
		private byte[] _data = null!;
		private int _currentCounter = 0;

		public HTTPServer()
			: base()
		{ }

		/// <summary>
		/// Validate http request
		/// </summary>
		/// <param name="data">Data.</param>
		/// <returns>Corresponds to validation.</returns>
		public bool ValidateHttpRequest(byte[] data)
		{
			_data = data;

			foreach (var piece in data)
			{
				_currentCounter++;
				byte[] buffer = new byte[1];
				buffer[0] = piece;
				_possibleHeader += Encoding.UTF8.GetString(buffer);

				if (_possibleHeader.Contains("\r\n\r\n"))
				{
					return true;
				}
			}

			ResetReceivedData();

			return false;
		}

		/// <summary>
		/// Parse body of the http request
		/// </summary>
		/// <returns>Possible body.</returns>
		public string ParseBody()
		{
			int contentLength = FindLenghOfTheContent();
			byte[] body = new byte[contentLength];
			Array.Copy(_data, _currentCounter, body, 0, contentLength);
			_currentCounter = 0;
			_possibleBody += Encoding.ASCII.GetString(body);

			return _possibleBody;
		}

		/// <summary>
		/// Find length of the content segment
		/// </summary>
		/// <returns>Content lenght.</returns>
		private int FindLenghOfTheContent()
		{
			Regex reg = new Regex("\\\r\nContent-Length: (.*?)\\\r\n");
			Match m = reg.Match(_possibleHeader);
			int contentLength = int.Parse(m.Groups[1].ToString());
			return contentLength;
		}

		/// <summary>
		/// Reset all data
		/// </summary>
		private void ResetReceivedData()
		{
			_possibleBody = null;
			_possibleHeader = null;
			_currentCounter = 0;
		}
	}
}
