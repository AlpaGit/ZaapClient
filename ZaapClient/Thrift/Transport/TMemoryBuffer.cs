using System;
using System.IO;
using System.Reflection;
using Thrift.Protocol;

namespace Thrift.Transport
{
	// Token: 0x02000047 RID: 71
	public class TMemoryBuffer : TTransport
	{
		// Token: 0x0600024D RID: 589 RVA: 0x00008BBC File Offset: 0x00006DBC
		public TMemoryBuffer()
		{
			this.byteStream = new MemoryStream();
		}

		// Token: 0x0600024E RID: 590 RVA: 0x00008BD0 File Offset: 0x00006DD0
		public TMemoryBuffer(byte[] buf)
		{
			this.byteStream = new MemoryStream(buf);
		}

		// Token: 0x0600024F RID: 591 RVA: 0x00008BE4 File Offset: 0x00006DE4
		public override void Open()
		{
		}

		// Token: 0x06000250 RID: 592 RVA: 0x00008BE8 File Offset: 0x00006DE8
		public override void Close()
		{
		}

		// Token: 0x06000251 RID: 593 RVA: 0x00008BEC File Offset: 0x00006DEC
		public override int Read(byte[] buf, int off, int len)
		{
			return this.byteStream.Read(buf, off, len);
		}

		// Token: 0x06000252 RID: 594 RVA: 0x00008BFC File Offset: 0x00006DFC
		public override void Write(byte[] buf, int off, int len)
		{
			this.byteStream.Write(buf, off, len);
		}

		// Token: 0x06000253 RID: 595 RVA: 0x00008C0C File Offset: 0x00006E0C
		public byte[] GetBuffer()
		{
			return this.byteStream.ToArray();
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000254 RID: 596 RVA: 0x00008C1C File Offset: 0x00006E1C
		public override bool IsOpen
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000255 RID: 597 RVA: 0x00008C20 File Offset: 0x00006E20
		public static byte[] Serialize(TAbstractBase s)
		{
			TMemoryBuffer tmemoryBuffer = new TMemoryBuffer();
			TBinaryProtocol tbinaryProtocol = new TBinaryProtocol(tmemoryBuffer);
			s.Write(tbinaryProtocol);
			return tmemoryBuffer.GetBuffer();
		}

		// Token: 0x06000256 RID: 598 RVA: 0x00008C48 File Offset: 0x00006E48
		public static T DeSerialize<T>(byte[] buf) where T : TAbstractBase
		{
			TMemoryBuffer tmemoryBuffer = new TMemoryBuffer(buf);
			TBinaryProtocol tbinaryProtocol = new TBinaryProtocol(tmemoryBuffer);
			if (typeof(TBase).IsAssignableFrom(typeof(T)))
			{
				MethodInfo method = typeof(T).GetMethod("Read", BindingFlags.Instance | BindingFlags.Public);
				T t = Activator.CreateInstance<T>();
				method.Invoke(t, new object[] { tbinaryProtocol });
				return t;
			}
			MethodInfo method2 = typeof(T).GetMethod("Read", BindingFlags.Static | BindingFlags.Public);
			return (T)((object)method2.Invoke(null, new object[] { tbinaryProtocol }));
		}

		// Token: 0x06000257 RID: 599 RVA: 0x00008CE8 File Offset: 0x00006EE8
		protected override void Dispose(bool disposing)
		{
			if (!this._IsDisposed && disposing && this.byteStream != null)
			{
				this.byteStream.Dispose();
			}
			this._IsDisposed = true;
		}

		// Token: 0x04000119 RID: 281
		private readonly MemoryStream byteStream;

		// Token: 0x0400011A RID: 282
		private bool _IsDisposed;
	}
}
