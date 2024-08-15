using System;
using System.Net.Sockets;

namespace Thrift.Transport
{
	// Token: 0x0200003E RID: 62
	public class TSocket : TStreamTransport
	{
		// Token: 0x0600020A RID: 522 RVA: 0x00007EF0 File Offset: 0x000060F0
		public TSocket(TcpClient client)
		{
			this.client = client;
			if (this.IsOpen)
			{
				this.inputStream = client.GetStream();
				this.outputStream = client.GetStream();
			}
		}

		// Token: 0x0600020B RID: 523 RVA: 0x00007F24 File Offset: 0x00006124
		public TSocket(string host, int port)
			: this(host, port, 0)
		{
		}

		// Token: 0x0600020C RID: 524 RVA: 0x00007F30 File Offset: 0x00006130
		public TSocket(string host, int port, int timeout)
		{
			this.host = host;
			this.port = port;
			this.timeout = timeout;
			this.InitSocket();
		}

		// Token: 0x0600020D RID: 525 RVA: 0x00007F54 File Offset: 0x00006154
		private void InitSocket()
		{
			this.client = new TcpClient();
			TcpClient tcpClient = this.client;
			int num = this.timeout;
			this.client.SendTimeout = num;
			tcpClient.ReceiveTimeout = num;
			this.client.Client.NoDelay = true;
		}

		// Token: 0x17000028 RID: 40
		// (set) Token: 0x0600020E RID: 526 RVA: 0x00007F9C File Offset: 0x0000619C
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

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600020F RID: 527 RVA: 0x00007FCC File Offset: 0x000061CC
		public TcpClient TcpClient
		{
			get
			{
				return this.client;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000210 RID: 528 RVA: 0x00007FD4 File Offset: 0x000061D4
		public string Host
		{
			get
			{
				return this.host;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000211 RID: 529 RVA: 0x00007FDC File Offset: 0x000061DC
		public int Port
		{
			get
			{
				return this.port;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000212 RID: 530 RVA: 0x00007FE4 File Offset: 0x000061E4
		public override bool IsOpen
		{
			get
			{
				return this.client != null && this.client.Connected;
			}
		}

		// Token: 0x06000213 RID: 531 RVA: 0x00008000 File Offset: 0x00006200
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
			if (this.timeout == 0)
			{
				this.client.Connect(this.host, this.port);
			}
			else
			{
				TSocket.ConnectHelper connectHelper = new TSocket.ConnectHelper(this.client);
				IAsyncResult asyncResult = this.client.BeginConnect(this.host, this.port, new AsyncCallback(TSocket.ConnectCallback), connectHelper);
				if (!asyncResult.AsyncWaitHandle.WaitOne(this.timeout) || !this.client.Connected)
				{
					object mutex = connectHelper.Mutex;
					lock (mutex)
					{
						if (connectHelper.CallbackDone)
						{
							asyncResult.AsyncWaitHandle.Close();
							this.client.Close();
						}
						else
						{
							connectHelper.DoCleanup = true;
							this.client = null;
						}
					}
					throw new TTransportException(TTransportException.ExceptionType.TimedOut, "Connect timed out");
				}
			}
			this.inputStream = this.client.GetStream();
			this.outputStream = this.client.GetStream();
		}

		// Token: 0x06000214 RID: 532 RVA: 0x00008178 File Offset: 0x00006378
		private static void ConnectCallback(IAsyncResult asyncres)
		{
			TSocket.ConnectHelper connectHelper = asyncres.AsyncState as TSocket.ConnectHelper;
			object mutex = connectHelper.Mutex;
			lock (mutex)
			{
				connectHelper.CallbackDone = true;
				try
				{
					if (connectHelper.Client.Client != null)
					{
						connectHelper.Client.EndConnect(asyncres);
					}
				}
				catch (Exception)
				{
				}
				if (connectHelper.DoCleanup)
				{
					try
					{
						asyncres.AsyncWaitHandle.Close();
					}
					catch (Exception)
					{
					}
					try
					{
						if (connectHelper.Client != null)
						{
							((IDisposable)connectHelper.Client).Dispose();
						}
					}
					catch (Exception)
					{
					}
					connectHelper.Client = null;
				}
			}
		}

		// Token: 0x06000215 RID: 533 RVA: 0x00008258 File Offset: 0x00006458
		public override void Close()
		{
			base.Close();
			if (this.client != null)
			{
				this.client.Close();
				this.client = null;
			}
		}

		// Token: 0x06000216 RID: 534 RVA: 0x00008280 File Offset: 0x00006480
		protected override void Dispose(bool disposing)
		{
			if (!this._IsDisposed && disposing)
			{
				if (this.client != null)
				{
					((IDisposable)this.client).Dispose();
				}
				base.Dispose(disposing);
			}
			this._IsDisposed = true;
		}

		// Token: 0x040000F1 RID: 241
		private TcpClient client;

		// Token: 0x040000F2 RID: 242
		private string host;

		// Token: 0x040000F3 RID: 243
		private int port;

		// Token: 0x040000F4 RID: 244
		private int timeout;

		// Token: 0x040000F5 RID: 245
		private bool _IsDisposed;

		// Token: 0x0200003F RID: 63
		private class ConnectHelper
		{
			// Token: 0x06000217 RID: 535 RVA: 0x000082B8 File Offset: 0x000064B8
			public ConnectHelper(TcpClient client)
			{
				this.Client = client;
			}

			// Token: 0x040000F6 RID: 246
			public object Mutex = new object();

			// Token: 0x040000F7 RID: 247
			public bool DoCleanup;

			// Token: 0x040000F8 RID: 248
			public bool CallbackDone;

			// Token: 0x040000F9 RID: 249
			public TcpClient Client;
		}
	}
}
