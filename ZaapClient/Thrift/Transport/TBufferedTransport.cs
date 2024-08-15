using System;
using System.IO;

namespace Thrift.Transport
{
	// Token: 0x02000030 RID: 48
	public class TBufferedTransport : TTransport, IDisposable
	{
		// Token: 0x060001A2 RID: 418 RVA: 0x000066DC File Offset: 0x000048DC
		public TBufferedTransport(TTransport transport, int bufSize = 1024)
		{
			if (transport == null)
			{
				throw new ArgumentNullException("transport");
			}
			if (bufSize <= 0)
			{
				throw new ArgumentException("bufSize", "Buffer size must be a positive number.");
			}
			this.transport = transport;
			this.bufSize = bufSize;
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x060001A3 RID: 419 RVA: 0x00006740 File Offset: 0x00004940
		public TTransport UnderlyingTransport
		{
			get
			{
				this.CheckNotDisposed();
				return this.transport;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x060001A4 RID: 420 RVA: 0x00006750 File Offset: 0x00004950
		public override bool IsOpen
		{
			get
			{
				return !this._IsDisposed && this.transport.IsOpen;
			}
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x0000676C File Offset: 0x0000496C
		public override void Open()
		{
			this.CheckNotDisposed();
			this.transport.Open();
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x00006780 File Offset: 0x00004980
		public override void Close()
		{
			this.CheckNotDisposed();
			this.transport.Close();
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x00006794 File Offset: 0x00004994
		public override int Read(byte[] buf, int off, int len)
		{
			this.CheckNotDisposed();
			TTransport.ValidateBufferArgs(buf, off, len);
			if (!this.IsOpen)
			{
				throw new TTransportException(TTransportException.ExceptionType.NotOpen);
			}
			if (this.inputBuffer.Capacity < this.bufSize)
			{
				this.inputBuffer.Capacity = this.bufSize;
			}
			int num = this.inputBuffer.Read(buf, off, len);
			if (num > 0)
			{
				return num;
			}
			this.inputBuffer.Seek(0L, SeekOrigin.Begin);
			this.inputBuffer.SetLength((long)this.inputBuffer.Capacity);
			int num2 = this.transport.Read(this.inputBuffer.GetBuffer(), 0, (int)this.inputBuffer.Length);
			this.inputBuffer.SetLength((long)num2);
			if (num2 == 0)
			{
				return 0;
			}
			return this.Read(buf, off, len);
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x00006868 File Offset: 0x00004A68
		public override void Write(byte[] buf, int off, int len)
		{
			this.CheckNotDisposed();
			TTransport.ValidateBufferArgs(buf, off, len);
			if (!this.IsOpen)
			{
				throw new TTransportException(TTransportException.ExceptionType.NotOpen);
			}
			int num = 0;
			if (this.outputBuffer.Length > 0L)
			{
				int num2 = (int)((long)this.outputBuffer.Capacity - this.outputBuffer.Length);
				int num3 = ((num2 > len) ? len : num2);
				this.outputBuffer.Write(buf, off, num3);
				num += num3;
				if (num3 == num2)
				{
					this.transport.Write(this.outputBuffer.GetBuffer(), 0, (int)this.outputBuffer.Length);
					this.outputBuffer.SetLength(0L);
				}
			}
			while (len - num >= this.bufSize)
			{
				this.transport.Write(buf, off + num, this.bufSize);
				num += this.bufSize;
			}
			int num4 = len - num;
			if (num4 > 0)
			{
				if (this.outputBuffer.Capacity < this.bufSize)
				{
					this.outputBuffer.Capacity = this.bufSize;
				}
				this.outputBuffer.Write(buf, off + num, num4);
			}
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x00006990 File Offset: 0x00004B90
		public override void Flush()
		{
			this.CheckNotDisposed();
			if (!this.IsOpen)
			{
				throw new TTransportException(TTransportException.ExceptionType.NotOpen);
			}
			if (this.outputBuffer.Length > 0L)
			{
				this.transport.Write(this.outputBuffer.GetBuffer(), 0, (int)this.outputBuffer.Length);
				this.outputBuffer.SetLength(0L);
			}
			this.transport.Flush();
		}

		// Token: 0x060001AA RID: 426 RVA: 0x00006A04 File Offset: 0x00004C04
		private void CheckNotDisposed()
		{
			if (this._IsDisposed)
			{
				throw new ObjectDisposedException("TBufferedTransport");
			}
		}

		// Token: 0x060001AB RID: 427 RVA: 0x00006A1C File Offset: 0x00004C1C
		protected override void Dispose(bool disposing)
		{
			if (!this._IsDisposed && disposing)
			{
				this.inputBuffer.Dispose();
				this.outputBuffer.Dispose();
			}
			this._IsDisposed = true;
		}

		// Token: 0x040000BA RID: 186
		private readonly int bufSize;

		// Token: 0x040000BB RID: 187
		private readonly MemoryStream inputBuffer = new MemoryStream(0);

		// Token: 0x040000BC RID: 188
		private readonly MemoryStream outputBuffer = new MemoryStream(0);

		// Token: 0x040000BD RID: 189
		private readonly TTransport transport;

		// Token: 0x040000BE RID: 190
		private bool _IsDisposed;
	}
}
