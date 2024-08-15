using System;
using System.IO;

namespace Thrift.Transport
{
	// Token: 0x02000040 RID: 64
	public class TStreamTransport : TTransport
	{
		// Token: 0x06000218 RID: 536 RVA: 0x000082D4 File Offset: 0x000064D4
		protected TStreamTransport()
		{
		}

		// Token: 0x06000219 RID: 537 RVA: 0x000082DC File Offset: 0x000064DC
		public TStreamTransport(Stream inputStream, Stream outputStream)
		{
			this.inputStream = inputStream;
			this.outputStream = outputStream;
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x0600021A RID: 538 RVA: 0x000082F4 File Offset: 0x000064F4
		public Stream OutputStream
		{
			get
			{
				return this.outputStream;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x0600021B RID: 539 RVA: 0x000082FC File Offset: 0x000064FC
		public Stream InputStream
		{
			get
			{
				return this.inputStream;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x0600021C RID: 540 RVA: 0x00008304 File Offset: 0x00006504
		public override bool IsOpen
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600021D RID: 541 RVA: 0x00008308 File Offset: 0x00006508
		public override void Open()
		{
		}

		// Token: 0x0600021E RID: 542 RVA: 0x0000830C File Offset: 0x0000650C
		public override void Close()
		{
			if (this.inputStream != null)
			{
				this.inputStream.Close();
				this.inputStream = null;
			}
			if (this.outputStream != null)
			{
				this.outputStream.Close();
				this.outputStream = null;
			}
		}

		// Token: 0x0600021F RID: 543 RVA: 0x00008348 File Offset: 0x00006548
		public override int Read(byte[] buf, int off, int len)
		{
			if (this.inputStream == null)
			{
				throw new TTransportException(TTransportException.ExceptionType.NotOpen, "Cannot read from null inputstream");
			}
			return this.inputStream.Read(buf, off, len);
		}

		// Token: 0x06000220 RID: 544 RVA: 0x00008370 File Offset: 0x00006570
		public override void Write(byte[] buf, int off, int len)
		{
			if (this.outputStream == null)
			{
				throw new TTransportException(TTransportException.ExceptionType.NotOpen, "Cannot write to null outputstream");
			}
			this.outputStream.Write(buf, off, len);
		}

		// Token: 0x06000221 RID: 545 RVA: 0x00008398 File Offset: 0x00006598
		public override void Flush()
		{
			if (this.outputStream == null)
			{
				throw new TTransportException(TTransportException.ExceptionType.NotOpen, "Cannot flush null outputstream");
			}
			this.outputStream.Flush();
		}

		// Token: 0x06000222 RID: 546 RVA: 0x000083BC File Offset: 0x000065BC
		protected override void Dispose(bool disposing)
		{
			if (!this._IsDisposed && disposing)
			{
				if (this.InputStream != null)
				{
					this.InputStream.Dispose();
				}
				if (this.OutputStream != null)
				{
					this.OutputStream.Dispose();
				}
			}
			this._IsDisposed = true;
		}

		// Token: 0x040000FA RID: 250
		protected Stream inputStream;

		// Token: 0x040000FB RID: 251
		protected Stream outputStream;

		// Token: 0x040000FC RID: 252
		private bool _IsDisposed;
	}
}
