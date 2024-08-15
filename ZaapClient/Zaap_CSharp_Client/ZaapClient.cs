using System;
using System.IO;
using com.ankama.zaap;
using Thrift;
using Thrift.Protocol;
using Thrift.Transport;

namespace Zaap_CSharp_Client
{
	// Token: 0x0200004D RID: 77
	public class ZaapClient
	{
		// Token: 0x06000258 RID: 600 RVA: 0x00008D18 File Offset: 0x00006F18
		private ZaapClient(ZaapService.Client client, TTransport connection, string session)
		{
			this.m_client = client;
			this.m_connection = connection;
			this.Session = session;
		}

		// Token: 0x06000259 RID: 601 RVA: 0x00008D38 File Offset: 0x00006F38
		public static ZaapClient Connect(ZaapClient.ParametersSources source = ZaapClient.ParametersSources.FILE_FIRST)
		{
			ZaapClientParameters zaapClientParameters = null;
			switch (source)
			{
			case ZaapClient.ParametersSources.ENV_VARIABLES_FIRST:
				zaapClientParameters = ZaapClient.GetParametersFromEnv() ?? ZaapClient.GetParametersFromFile();
				break;
			case ZaapClient.ParametersSources.FILE_FIRST:
				zaapClientParameters = ZaapClient.GetParametersFromFile() ?? ZaapClient.GetParametersFromEnv();
				break;
			case ZaapClient.ParametersSources.ONLY_ENV_VARIABLES:
				zaapClientParameters = ZaapClient.GetParametersFromEnv();
				break;
			case ZaapClient.ParametersSources.ONLY_FILE:
				zaapClientParameters = ZaapClient.GetParametersFromFile();
				break;
			}
			return ZaapClient.Connect(zaapClientParameters);
		}

		// Token: 0x0600025A RID: 602 RVA: 0x00008DAC File Offset: 0x00006FAC
		public static ZaapClient Connect(ZaapClientParameters parameters)
		{
			if (parameters == null)
			{
				Console.WriteLine("[ZaapClient] Unable to connect to Zaap: No ZaapClientParameters");
				return null;
			}
			if (!parameters.Valid())
			{
				Console.WriteLine("[ZaapClient] Unable to connect to Zaap: Invalid ZaapClientParameters");
				return null;
			}
			Console.WriteLine(string.Concat(new object[]
			{
				"[ZaapClient] Trying to connect to port ", parameters.port, " with game ", parameters.name, "/", parameters.release, " (id: ", parameters.instanceId, ", hash: ", parameters.hash,
				")"
			}));
			return ZaapClient.Connect(parameters.port, parameters.name, parameters.release, parameters.instanceId, parameters.hash);
		}

		// Token: 0x0600025B RID: 603 RVA: 0x00008E80 File Offset: 0x00007080
		public static ZaapClient Connect(int port, string name, string release, int instanceId, string hash)
		{
			ZaapClient zaapClient = null;
			try
			{
				TTransport ttransport = new TSocket(ZaapClient.Host, port);
				ttransport.Open();
				TBinaryProtocol tbinaryProtocol = new TBinaryProtocol(ttransport);
				ZaapService.Client client = new ZaapService.Client(tbinaryProtocol);
				string text = client.connect(name, release, instanceId, hash);
				zaapClient = new ZaapClient(client, ttransport, text);
			}
			catch (Exception ex)
			{
				Console.Error.WriteLine("[ZaapClient] Exception while connecting to Zaap: " + ex);
			}
			return zaapClient;
		}

		// Token: 0x0600025C RID: 604 RVA: 0x00008EFC File Offset: 0x000070FC
		public ZaapService.Client GetClient()
		{
			if (this.m_client == null || this.Session == null)
			{
				throw new TException("Client is not connected");
			}
			return this.m_client;
		}

		// Token: 0x0600025D RID: 605 RVA: 0x00008F28 File Offset: 0x00007128
		public void Disconnect()
		{
			this.m_connection.Close();
			this.m_connection = null;
			this.m_client = null;
			this.Session = null;
		}

		// Token: 0x0600025E RID: 606 RVA: 0x00008F4C File Offset: 0x0000714C
		private static ZaapClientParameters GetParametersFromFile()
		{
			ZaapClientParameters zaapClientParameters = null;
			try
			{
				if (File.Exists("credentials.json"))
				{
					string text = File.ReadAllText("credentials.json");
					zaapClientParameters = text.FromJson<ZaapClientParameters>();
					if (!zaapClientParameters.Valid())
					{
						Console.Error.WriteLine("Configuration file credentials.json is not a valid Json parameters file.");
						zaapClientParameters = null;
					}
				}
			}
			catch (Exception ex)
			{
				Console.Error.WriteLine("Unable to read configuration file credentials.json: " + ex);
			}
			return zaapClientParameters;
		}

		// Token: 0x0600025F RID: 607 RVA: 0x00008FCC File Offset: 0x000071CC
		private static ZaapClientParameters GetParametersFromEnv()
		{
			int intFromEnvironmentVariable = ZaapClient.GetIntFromEnvironmentVariable("ZAAP_PORT", ZaapClient.DefaultPort);
			string environmentVariable = Environment.GetEnvironmentVariable("ZAAP_GAME");
			string environmentVariable2 = Environment.GetEnvironmentVariable("ZAAP_RELEASE");
			int intFromEnvironmentVariable2 = ZaapClient.GetIntFromEnvironmentVariable("ZAAP_INSTANCE_ID", 0);
			string environmentVariable3 = Environment.GetEnvironmentVariable("ZAAP_HASH");
			return new ZaapClientParameters
			{
				port = intFromEnvironmentVariable,
				name = environmentVariable,
				release = environmentVariable2,
				instanceId = intFromEnvironmentVariable2,
				hash = environmentVariable3
			};
		}

		// Token: 0x06000260 RID: 608 RVA: 0x0000904C File Offset: 0x0000724C
		private static int GetIntFromEnvironmentVariable(string name, int defaultValue)
		{
			string environmentVariable = Environment.GetEnvironmentVariable(name);
			if (string.IsNullOrEmpty(environmentVariable))
			{
				return defaultValue;
			}
			int num;
			int.TryParse(environmentVariable, out num);
			return num;
		}

		// Token: 0x04000128 RID: 296
		public const string PORT_VAR = "ZAAP_PORT";

		// Token: 0x04000129 RID: 297
		public const string GAME_NAME_VAR = "ZAAP_GAME";

		// Token: 0x0400012A RID: 298
		public const string GAME_RELEASE_VAR = "ZAAP_RELEASE";

		// Token: 0x0400012B RID: 299
		public const string INSTANCE_ID_VAR = "ZAAP_INSTANCE_ID";

		// Token: 0x0400012C RID: 300
		public const string HASH_VAR = "ZAAP_HASH";

		// Token: 0x0400012D RID: 301
		public const string CONFIGURATION_FILE = "credentials.json";

		// Token: 0x0400012E RID: 302
		public static string Host = "127.0.0.1";

		// Token: 0x0400012F RID: 303
		public static int DefaultPort = 26116;

		// Token: 0x04000130 RID: 304
		public string Session;

		// Token: 0x04000131 RID: 305
		private ZaapService.Client m_client;

		// Token: 0x04000132 RID: 306
		private TTransport m_connection;

		// Token: 0x0200004E RID: 78
		public enum ParametersSources
		{
			// Token: 0x04000134 RID: 308
			ENV_VARIABLES_FIRST,
			// Token: 0x04000135 RID: 309
			FILE_FIRST,
			// Token: 0x04000136 RID: 310
			ONLY_ENV_VARIABLES,
			// Token: 0x04000137 RID: 311
			ONLY_FILE
		}
	}
}
