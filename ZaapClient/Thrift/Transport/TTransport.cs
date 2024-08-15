using System;
using System.IO;

namespace Thrift.Transport
{
	// Token: 0x02000043 RID: 67
	public abstract class TTransport : IDisposable
	{
		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000238 RID: 568
		public abstract bool IsOpen { get; }

		// Token: 0x06000239 RID: 569 RVA: 0x00008A00 File Offset: 0x00006C00
		public bool Peek()
		{
			if (this._hasPeekByte)
			{
				return true;
			}
			if (!this.IsOpen)
			{
				return false;
			}
			try
			{
				if (this.Read(this._peekBuffer, 0, 1) == 0)
				{
					return false;
				}
			}
			catch (IOException)
			{
				return false;
			}
			this._hasPeekByte = true;
			return true;
		}

		// Token: 0x0600023A RID: 570
		public abstract void Open();

		// Token: 0x0600023B RID: 571
		public abstract void Close();

		// Token: 0x0600023C RID: 572 RVA: 0x00008A6C File Offset: 0x00006C6C
		protected static void ValidateBufferArgs(byte[] buf, int off, int len)
		{
			if (buf == null)
			{
				throw new ArgumentNullException("buf");
			}
			if (off < 0)
			{
				throw new ArgumentOutOfRangeException("Buffer offset is smaller than zero.");
			}
			if (len < 0)
			{
				throw new ArgumentOutOfRangeException("Buffer length is smaller than zero.");
			}
			if (off + len > buf.Length)
			{
				throw new ArgumentOutOfRangeException("Not enough data.");
			}
		}

		// Token: 0x0600023D RID: 573
		public abstract int Read(byte[] buf, int off, int len);

		// Token: 0x0600023E RID: 574 RVA: 0x00008AC4 File Offset: 0x00006CC4
		public int ReadAll(byte[] buf, int off, int len)
		{
			TTransport.ValidateBufferArgs(buf, off, len);
			int i = 0;
			if (this._hasPeekByte)
			{
				buf[off + i++] = this._peekBuffer[0];
				this._hasPeekByte = false;
			}
			while (i < len)
			{
				int num = this.Read(buf, off + i, len - i);
				if (num <= 0)
				{
					throw new TTransportException(TTransportException.ExceptionType.EndOfFile, "Cannot read, Remote side has closed");
				}
				i += num;
			}
			return i;
		}

		// Token: 0x0600023F RID: 575 RVA: 0x00008B30 File Offset: 0x00006D30
		public virtual void Write(byte[] buf)
		{
			this.Write(buf, 0, buf.Length);
		}

		// Token: 0x06000240 RID: 576
		public abstract void Write(byte[] buf, int off, int len);

		// Token: 0x06000241 RID: 577 RVA: 0x00008B40 File Offset: 0x00006D40
		public virtual void Flush()
		{
		}

		// Token: 0x06000242 RID: 578 RVA: 0x00008B44 File Offset: 0x00006D44
		public virtual IAsyncResult BeginFlush(AsyncCallback callback, object state)
		{
			throw new TTransportException(TTransportException.ExceptionType.Unknown, "Asynchronous operations are not supported by this transport.");
		}

		// Token: 0x06000243 RID: 579 RVA: 0x00008B54 File Offset: 0x00006D54
		public virtual void EndFlush(IAsyncResult asyncResult)
		{
			throw new TTransportException(TTransportException.ExceptionType.Unknown, "Asynchronous operations are not supported by this transport.");
		}

		// Token: 0x06000244 RID: 580
		protected abstract void Dispose(bool disposing);

		// Token: 0x06000245 RID: 581 RVA: 0x00008B64 File Offset: 0x00006D64
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0400010F RID: 271
		private byte[] _peekBuffer = new byte[1];

		// Token: 0x04000110 RID: 272
		private bool _hasPeekByte;
	}
}
