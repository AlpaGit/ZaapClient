using System;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;

namespace Thrift.Transport
{
	// Token: 0x02000041 RID: 65
	public class TTLSServerSocket : TServerTransport
	{
		// Token: 0x06000223 RID: 547 RVA: 0x00008410 File Offset: 0x00006610
		public TTLSServerSocket(int port, X509Certificate2 certificate)
			: this(port, 0, certificate)
		{
		}

		// Token: 0x06000224 RID: 548 RVA: 0x0000841C File Offset: 0x0000661C
		public TTLSServerSocket(int port, int clientTimeout, X509Certificate2 certificate)
			: this(port, clientTimeout, false, certificate, null, null, SslProtocols.Tls)
		{
		}

		// Token: 0x06000225 RID: 549 RVA: 0x00008430 File Offset: 0x00006630
		public TTLSServerSocket(int port, int clientTimeout, bool useBufferedSockets, X509Certificate2 certificate, RemoteCertificateValidationCallback clientCertValidator = null, LocalCertificateSelectionCallback localCertificateSelectionCallback = null, SslProtocols sslProtocols = SslProtocols.Tls)
		{
			if (!certificate.HasPrivateKey)
			{
				throw new TTransportException(TTransportException.ExceptionType.Unknown, "Your server-certificate needs to have a private key");
			}
			this.port = port;
			this.clientTimeout = clientTimeout;
			this.serverCertificate = certificate;
			this.useBufferedSockets = useBufferedSockets;
			this.clientCertValidator = clientCertValidator;
			this.localCertificateSelectionCallback = localCertificateSelectionCallback;
			this.sslProtocols = sslProtocols;
			try
			{
				this.server = new TcpListener(IPAddress.Any, this.port);
				this.server.Server.NoDelay = true;
			}
			catch (Exception)
			{
				this.server = null;
				throw new TTransportException("Could not create ServerSocket on port " + port + ".");
			}
		}

		// Token: 0x06000226 RID: 550 RVA: 0x000084F0 File Offset: 0x000066F0
		public override void Listen()
		{
			if (this.server != null)
			{
				try
				{
					this.server.Start();
				}
				catch (SocketException ex)
				{
					throw new TTransportException("Could not accept on listening socket: " + ex.Message);
				}
			}
		}

		// Token: 0x06000227 RID: 551 RVA: 0x00008540 File Offset: 0x00006740
		protected override TTransport AcceptImpl()
		{
			if (this.server == null)
			{
				throw new TTransportException(TTransportException.ExceptionType.NotOpen, "No underlying server socket.");
			}
			TTransport ttransport;
			try
			{
				TcpClient tcpClient = this.server.AcceptTcpClient();
				TcpClient tcpClient2 = tcpClient;
				int num = this.clientTimeout;
				tcpClient.ReceiveTimeout = num;
				tcpClient2.SendTimeout = num;
				TTLSSocket ttlssocket = new TTLSSocket(tcpClient, this.serverCertificate, true, this.clientCertValidator, this.localCertificateSelectionCallback, this.sslProtocols);
				ttlssocket.setupTLS();
				if (this.useBufferedSockets)
				{
					TBufferedTransport tbufferedTransport = new TBufferedTransport(ttlssocket, 1024);
					ttransport = tbufferedTransport;
				}
				else
				{
					ttransport = ttlssocket;
				}
			}
			catch (Exception ex)
			{
				throw new TTransportException(ex.ToString());
			}
			return ttransport;
		}

		// Token: 0x06000228 RID: 552 RVA: 0x000085F4 File Offset: 0x000067F4
		public override void Close()
		{
			if (this.server != null)
			{
				try
				{
					this.server.Stop();
				}
				catch (Exception ex)
				{
					throw new TTransportException("WARNING: Could not close server socket: " + ex);
				}
				this.server = null;
			}
		}

		// Token: 0x040000FD RID: 253
		private TcpListener server;

		// Token: 0x040000FE RID: 254
		private int port;

		// Token: 0x040000FF RID: 255
		private readonly int clientTimeout;

		// Token: 0x04000100 RID: 256
		private bool useBufferedSockets;

		// Token: 0x04000101 RID: 257
		private X509Certificate serverCertificate;

		// Token: 0x04000102 RID: 258
		private RemoteCertificateValidationCallback clientCertValidator;

		// Token: 0x04000103 RID: 259
		private LocalCertificateSelectionCallback localCertificateSelectionCallback;

		// Token: 0x04000104 RID: 260
		private readonly SslProtocols sslProtocols;
	}
}
