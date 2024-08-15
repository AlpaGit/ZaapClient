using System;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;

namespace Thrift.Transport
{
	// Token: 0x02000042 RID: 66
	public class TTLSSocket : TStreamTransport
	{
		// Token: 0x06000229 RID: 553 RVA: 0x00008648 File Offset: 0x00006848
		public TTLSSocket(TcpClient client, X509Certificate certificate, bool isServer = false, RemoteCertificateValidationCallback certValidator = null, LocalCertificateSelectionCallback localCertificateSelectionCallback = null, SslProtocols sslProtocols = SslProtocols.Tls)
		{
			this.client = client;
			this.certificate = certificate;
			this.certValidator = certValidator;
			this.localCertificateSelectionCallback = localCertificateSelectionCallback;
			this.sslProtocols = sslProtocols;
			this.isServer = isServer;
			if (isServer && certificate == null)
			{
				throw new ArgumentException("TTLSSocket needs certificate to be used for server", "certificate");
			}
			if (this.IsOpen)
			{
				this.inputStream = client.GetStream();
				this.outputStream = client.GetStream();
			}
		}

		// Token: 0x0600022A RID: 554 RVA: 0x000086C8 File Offset: 0x000068C8
		public TTLSSocket(string host, int port, string certificatePath, RemoteCertificateValidationCallback certValidator = null, LocalCertificateSelectionCallback localCertificateSelectionCallback = null, SslProtocols sslProtocols = SslProtocols.Tls)
			: this(host, port, 0, X509Certificate.CreateFromCertFile(certificatePath), certValidator, localCertificateSelectionCallback, sslProtocols)
		{
		}

		// Token: 0x0600022B RID: 555 RVA: 0x000086E0 File Offset: 0x000068E0
		public TTLSSocket(string host, int port, X509Certificate certificate = null, RemoteCertificateValidationCallback certValidator = null, LocalCertificateSelectionCallback localCertificateSelectionCallback = null, SslProtocols sslProtocols = SslProtocols.Tls)
			: this(host, port, 0, certificate, certValidator, localCertificateSelectionCallback, sslProtocols)
		{
		}

		// Token: 0x0600022C RID: 556 RVA: 0x000086F4 File Offset: 0x000068F4
		public TTLSSocket(string host, int port, int timeout, X509Certificate certificate, RemoteCertificateValidationCallback certValidator = null, LocalCertificateSelectionCallback localCertificateSelectionCallback = null, SslProtocols sslProtocols = SslProtocols.Tls)
		{
			this.host = host;
			this.port = port;
			this.timeout = timeout;
			this.certificate = certificate;
			this.certValidator = certValidator;
			this.localCertificateSelectionCallback = localCertificateSelectionCallback;
			this.sslProtocols = sslProtocols;
			this.InitSocket();
		}

		// Token: 0x0600022D RID: 557 RVA: 0x00008744 File Offset: 0x00006944
		private void InitSocket()
		{
			this.client = new TcpClient();
			TcpClient tcpClient = this.client;
			int num = this.timeout;
			this.client.SendTimeout = num;
			tcpClient.ReceiveTimeout = num;
			this.client.Client.NoDelay = true;
		}

		// Token: 0x17000030 RID: 48
		// (set) Token: 0x0600022E RID: 558 RVA: 0x0000878C File Offset: 0x0000698C
		public int Timeout
		{
			set
			{
				TcpClient tcpClient = this.client;
				this.timeout = value;
				this.client.SendTimeout = value;
				tcpClient.ReceiveTimeout = value;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600022F RID: 559 RVA: 0x000087BC File Offset: 0x000069BC
		public TcpClient TcpClient
		{
			get
			{
				return this.client;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000230 RID: 560 RVA: 0x000087C4 File Offset: 0x000069C4
		public string Host
		{
			get
			{
				return this.host;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000231 RID: 561 RVA: 0x000087CC File Offset: 0x000069CC
		public int Port
		{
			get
			{
				return this.port;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000232 RID: 562 RVA: 0x000087D4 File Offset: 0x000069D4
		public override bool IsOpen
		{
			get
			{
				return this.client != null && this.client.Connected;
			}
		}

		// Token: 0x06000233 RID: 563 RVA: 0x000087F0 File Offset: 0x000069F0
		private bool DefaultCertificateValidator(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslValidationErrors)
		{
			return sslValidationErrors == SslPolicyErrors.None;
		}

		// Token: 0x06000234 RID: 564 RVA: 0x000087F8 File Offset: 0x000069F8
		public override void Open()
		{
			if (this.IsOpen)
			{
				throw new TTransportException(TTransportException.ExceptionType.AlreadyOpen, "Socket already connected");
			}
			if (string.IsNullOrEmpty(this.host))
			{
				throw new TTransportException(TTransportException.ExceptionType.NotOpen, "Cannot open null host");
			}
			if (this.port <= 0)
			{
				throw new TTransportException(TTransportException.ExceptionType.NotOpen, "Cannot open without port");
			}
			if (this.client == null)
			{
				this.InitSocket();
			}
			this.client.Connect(this.host, this.port);
			this.setupTLS();
		}

		// Token: 0x06000235 RID: 565 RVA: 0x00008880 File Offset: 0x00006A80
		public void setupTLS()
		{
			RemoteCertificateValidationCallback remoteCertificateValidationCallback = this.certValidator ?? new RemoteCertificateValidationCallback(this.DefaultCertificateValidator);
			if (this.localCertificateSelectionCallback != null)
			{
				this.secureStream = new SslStream(this.client.GetStream(), false, remoteCertificateValidationCallback, this.localCertificateSelectionCallback);
			}
			else
			{
				this.secureStream = new SslStream(this.client.GetStream(), false, remoteCertificateValidationCallback);
			}
			try
			{
				if (this.isServer)
				{
					this.secureStream.AuthenticateAsServer(this.certificate, this.certValidator != null, this.sslProtocols, true);
				}
				else
				{
					X509CertificateCollection x509CertificateCollection = ((this.certificate == null) ? new X509CertificateCollection() : new X509CertificateCollection { this.certificate });
					this.secureStream.AuthenticateAsClient(this.host, x509CertificateCollection, this.sslProtocols, true);
				}
			}
			catch (Exception)
			{
				this.Close();
				throw;
			}
			this.inputStream = this.secureStream;
			this.outputStream = this.secureStream;
		}

		// Token: 0x06000236 RID: 566 RVA: 0x0000899C File Offset: 0x00006B9C
		public override void Close()
		{
			base.Close();
			if (this.client != null)
			{
				this.client.Close();
				this.client = null;
			}
			if (this.secureStream != null)
			{
				this.secureStream.Close();
				this.secureStream = null;
			}
		}

		// Token: 0x04000105 RID: 261
		private TcpClient client;

		// Token: 0x04000106 RID: 262
		private string host;

		// Token: 0x04000107 RID: 263
		private int port;

		// Token: 0x04000108 RID: 264
		private int timeout;

		// Token: 0x04000109 RID: 265
		private SslStream secureStream;

		// Token: 0x0400010A RID: 266
		private bool isServer;

		// Token: 0x0400010B RID: 267
		private X509Certificate certificate;

		// Token: 0x0400010C RID: 268
		private RemoteCertificateValidationCallback certValidator;

		// Token: 0x0400010D RID: 269
		private LocalCertificateSelectionCallback localCertificateSelectionCallback;

		// Token: 0x0400010E RID: 270
		private readonly SslProtocols sslProtocols;
	}
}
