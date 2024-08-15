using System;
using System.IO.Pipes;

namespace Thrift.Transport
{
	// Token: 0x02000036 RID: 54
	public class TNamedPipeClientTransport : TTransport
	{
		// Token: 0x060001E3 RID: 483 RVA: 0x00007614 File Offset: 0x00005814
		public TNamedPipeClientTransport(string pipe)
		{
			this.ServerName = ".";
			this.PipeName = pipe;
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x00007630 File Offset: 0x00005830
		public TNamedPipeClientTransport(string server, string pipe)
		{
			this.ServerName = ((!(server != string.Empty)) ? "." : server);
			this.PipeName = pipe;
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060001E5 RID: 485 RVA: 0x00007660 File Offset: 0x00005860
		public override bool IsOpen
		{
			get
			{
				return this.client != null && this.client.IsConnected;
			}
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x0000767C File Offset: 0x0000587C
		public override void Open()
		{
			if (this.IsOpen)
			{
				throw new TTransportException(TTransportException.ExceptionType.AlreadyOpen);
			}
			this.client = new NamedPipeClientStream(this.ServerName, this.PipeName, PipeDirection.InOut, PipeOptions.None);
			this.client.Connect();
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x000076B4 File Offset: 0x000058B4
		public override void Close()
		{
			if (this.client != null)
			{
				this.client.Close();
				this.client = null;
			}
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x000076D4 File Offset: 0x000058D4
		public override int Read(byte[] buf, int off, int len)
		{
			if (this.client == null)
			{
				throw new TTransportException(TTransportException.ExceptionType.NotOpen);
			}
			return this.client.Read(buf, off, len);
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x000076F8 File Offset: 0x000058F8
		public override void Write(byte[] buf, int off, int len)
		{
			if (this.client == null)
			{
				throw new TTransportException(TTransportException.ExceptionType.NotOpen);
			}
			this.client.Write(buf, off, len);
		}

		// Token: 0x060001EA RID: 490 RVA: 0x0000771C File Offset: 0x0000591C
		protected override void Dispose(bool disposing)
		{
			this.client.Dispose();
		}

		// Token: 0x040000DB RID: 219
		private NamedPipeClientStream client;

		// Token: 0x040000DC RID: 220
		private string ServerName;

		// Token: 0x040000DD RID: 221
		private string PipeName;
	}
}
