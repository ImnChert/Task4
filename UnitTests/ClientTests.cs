using Microsoft.VisualStudio.TestTools.UnitTesting;
using TCPIP.Models;

namespace UnitTests
{
	[TestClass]
	public class ClientTests
	{
		[TestMethod]
		public void CreateServer_SuccessfulCreation()
		{
			bool expected = true;
			bool actual = false;

			var client = new Client();

			if (client.IP != null & client.Socket != null)
				actual = true;

			Assert.AreEqual(expected, actual);
		}
	}
}