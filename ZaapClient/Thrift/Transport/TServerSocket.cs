using System;
using System.Net;
using System.Net.Sockets;

namespace Thrift.Transport
{
	// Token: 0x0200003C RID: 60
	public class TServerSocket : TServerTransport
	{
		// Token: 0x060001FD RID: 509 RVA: 0x00007CA8 File Offset: 0x00005EA8
		public TServerSocket(TcpListener listener)
			: this(listener, 0)
		{
		}

		// Token: 0x060001FE RID: 510 RVA: 0x00007CB4 File Offset: 0x00005EB4
		public TServerSocket(TcpListener listener, int clientTimeout)
		{
			this.server = listener;
			this.clientTimeout = clientTimeout;
		}

		// Token: 0x060001FF RID: 511 RVA: 0x00007CCC File Offset: 0x00005ECC
		public TServerSocket(int port)
			: this(port, 0)
		{
		}

		// Token: 0x06000200 RID: 512 RVA: 0x00007CD8 File Offset: 0x00005ED8
		public TServerSocket(int port, int clientTimeout)
			: this(port, clientTimeout, false)
		{
		}

		// Token: 0x06000201 RID: 513 RVA: 0x00007CE4 File Offset: 0x00005EE4
		public TServerSocket(int port, int clientTimeout, bool useBufferedSockets)
		{
			this.port = port;
			this.clientTimeout = clientTimeout;
			this.useBufferedSockets = useBufferedSockets;
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

		// Token: 0x06000202 RID: 514 RVA: 0x00007D6C File Offset: 0x00005F6C
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

		// Token: 0x06000203 RID: 515 RVA: 0x00007DBC File Offset: 0x00005FBC
		protected override TTransport AcceptImpl()
		{
			if (this.server == null)
			{
				throw new TTransportException(TTransportException.ExceptionType.NotOpen, "No underlying server socket.");
			}
			TTransport ttransport;
			try
			{
				TSocket tsocket = null;
				TcpClient tcpClient = this.server.AcceptTcpClient();
				try
				{
					tsocket = new TSocket(tcpClient);
					tsocket.Timeout = this.clientTimeout;
					if (this.useBufferedSockets)
					{
						TBufferedTransport tbufferedTransport = new TBufferedTransport(tsocket, 1024);
						ttransport = tbufferedTransport;
					}
					else
					{
						ttransport = tsocket;
					}
				}
				catch (Exception)
				{
					if (tsocket != null)
					{
						tsocket.Dispose();
					}
					else
					{
						((IDisposable)tcpClient).Dispose();
					}
					throw;
				}
			}
			catch (Exception ex)
			{
				throw new TTransportException(ex.ToString());
			}
			return ttransport;
		}

		// Token: 0x06000204 RID: 516 RVA: 0x00007E6C File Offset: 0x0000606C
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

		// Token: 0x040000ED RID: 237
		private TcpListener server;

		// Token: 0x040000EE RID: 238
		private int port;

		// Token: 0x040000EF RID: 239
		private int clientTimeout;

		// Token: 0x040000F0 RID: 240
		private bool useBufferedSockets;
	}
}
