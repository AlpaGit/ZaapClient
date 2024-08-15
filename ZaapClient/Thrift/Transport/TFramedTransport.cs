using System;
using System.IO;

namespace Thrift.Transport
{
	// Token: 0x02000031 RID: 49
	public class TFramedTransport : TTransport, IDisposable
	{
		// Token: 0x060001AC RID: 428 RVA: 0x00006A4C File Offset: 0x00004C4C
		public TFramedTransport(TTransport transport)
		{
			if (transport == null)
			{
				throw new ArgumentNullException("transport");
			}
			this.transport = transport;
			this.InitWriteBuffer();
		}

		// Token: 0x060001AD RID: 429 RVA: 0x00006AAC File Offset: 0x00004CAC
		public override void Open()
		{
			this.CheckNotDisposed();
			this.transport.Open();
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060001AE RID: 430 RVA: 0x00006AC0 File Offset: 0x00004CC0
		public override bool IsOpen
		{
			get
			{
				return !this._IsDisposed && this.transport.IsOpen;
			}
		}

		// Token: 0x060001AF RID: 431 RVA: 0x00006ADC File Offset: 0x00004CDC
		public override void Close()
		{
			this.CheckNotDisposed();
			this.transport.Close();
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x00006AF0 File Offset: 0x00004CF0
		public override int Read(byte[] buf, int off, int len)
		{
			this.CheckNotDisposed();
			TTransport.ValidateBufferArgs(buf, off, len);
			if (!this.IsOpen)
			{
				throw new TTransportException(TTransportException.ExceptionType.NotOpen);
			}
			int num = this.readBuffer.Read(buf, off, len);
			if (num > 0)
			{
				return num;
			}
			this.ReadFrame();
			return this.readBuffer.Read(buf, off, len);
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x00006B4C File Offset: 0x00004D4C
		private void ReadFrame()
		{
			this.transport.ReadAll(this.headerBuf, 0, 4);
			int num = TFramedTransport.DecodeFrameSize(this.headerBuf);
			this.readBuffer.SetLength((long)num);
			this.readBuffer.Seek(0L, SeekOrigin.Begin);
			byte[] buffer = this.readBuffer.GetBuffer();
			this.transport.ReadAll(buffer, 0, num);
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x00006BB0 File Offset: 0x00004DB0
		public override void Write(byte[] buf, int off, int len)
		{
			this.CheckNotDisposed();
			TTransport.ValidateBufferArgs(buf, off, len);
			if (!this.IsOpen)
			{
				throw new TTransportException(TTransportException.ExceptionType.NotOpen);
			}
			if (this.writeBuffer.Length + (long)len > 2147483647L)
			{
				this.Flush();
			}
			this.writeBuffer.Write(buf, off, len);
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x00006C0C File Offset: 0x00004E0C
		public override void Flush()
		{
			this.CheckNotDisposed();
			if (!this.IsOpen)
			{
				throw new TTransportException(TTransportException.ExceptionType.NotOpen);
			}
			byte[] buffer = this.writeBuffer.GetBuffer();
			int num = (int)this.writeBuffer.Length;
			int num2 = num - 4;
			if (num2 < 0)
			{
				throw new InvalidOperationException();
			}
			TFramedTransport.EncodeFrameSize(num2, buffer);
			this.transport.Write(buffer, 0, num);
			this.InitWriteBuffer();
			this.transport.Flush();
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x00006C84 File Offset: 0x00004E84
		private void InitWriteBuffer()
		{
			this.writeBuffer.SetLength(4L);
			this.writeBuffer.Seek(0L, SeekOrigin.End);
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x00006CA4 File Offset: 0x00004EA4
		private static void EncodeFrameSize(int frameSize, byte[] buf)
		{
			buf[0] = (byte)(255 & (frameSize >> 24));
			buf[1] = (byte)(255 & (frameSize >> 16));
			buf[2] = (byte)(255 & (frameSize >> 8));
			buf[3] = (byte)(255 & frameSize);
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x00006CDC File Offset: 0x00004EDC
		private static int DecodeFrameSize(byte[] buf)
		{
			return ((int)(buf[0] & byte.MaxValue) << 24) | ((int)(buf[1] & byte.MaxValue) << 16) | ((int)(buf[2] & byte.MaxValue) << 8) | (int)(buf[3] & byte.MaxValue);
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x00006D10 File Offset: 0x00004F10
		private void CheckNotDisposed()
		{
			if (this._IsDisposed)
			{
				throw new ObjectDisposedException("TFramedTransport");
			}
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x00006D28 File Offset: 0x00004F28
		protected override void Dispose(bool disposing)
		{
			if (!this._IsDisposed && disposing)
			{
				this.readBuffer.Dispose();
				this.writeBuffer.Dispose();
			}
			this._IsDisposed = true;
		}

		// Token: 0x040000BF RID: 191
		private readonly TTransport transport;

		// Token: 0x040000C0 RID: 192
		private readonly MemoryStream writeBuffer = new MemoryStream(1024);

		// Token: 0x040000C1 RID: 193
		private readonly MemoryStream readBuffer = new MemoryStream(1024);

		// Token: 0x040000C2 RID: 194
		private const int HeaderSize = 4;

		// Token: 0x040000C3 RID: 195
		private readonly byte[] headerBuf = new byte[4];

		// Token: 0x040000C4 RID: 196
		private bool _IsDisposed;

		// Token: 0x02000032 RID: 50
		public class Factory : TTransportFactory
		{
			// Token: 0x060001BA RID: 442 RVA: 0x00006D60 File Offset: 0x00004F60
			public override TTransport GetTransport(TTransport trans)
			{
				return new TFramedTransport(trans);
			}
		}
	}
}
