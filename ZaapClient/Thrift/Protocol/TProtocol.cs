using System;
using System.Text;
using Thrift.Transport;

namespace Thrift.Protocol
{
	// Token: 0x0200001C RID: 28
	public abstract class TProtocol : IDisposable
	{
		// Token: 0x060000FC RID: 252 RVA: 0x00004EB0 File Offset: 0x000030B0
		protected TProtocol(TTransport trans)
		{
			this.trans = trans;
			this.recursionLimit = 64;
			this.recursionDepth = 0;
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x060000FD RID: 253 RVA: 0x00004ED0 File Offset: 0x000030D0
		public TTransport Transport
		{
			get
			{
				return this.trans;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x060000FE RID: 254 RVA: 0x00004ED8 File Offset: 0x000030D8
		// (set) Token: 0x060000FF RID: 255 RVA: 0x00004EE0 File Offset: 0x000030E0
		public int RecursionLimit
		{
			get
			{
				return this.recursionLimit;
			}
			set
			{
				this.recursionLimit = value;
			}
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00004EEC File Offset: 0x000030EC
		public void IncrementRecursionDepth()
		{
			if (this.recursionDepth < this.recursionLimit)
			{
				this.recursionDepth++;
				return;
			}
			throw new TProtocolException(6, "Depth limit exceeded");
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00004F20 File Offset: 0x00003120
		public void DecrementRecursionDepth()
		{
			this.recursionDepth--;
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00004F30 File Offset: 0x00003130
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00004F3C File Offset: 0x0000313C
		protected virtual void Dispose(bool disposing)
		{
			if (!this._IsDisposed && disposing && this.trans != null)
			{
				((IDisposable)this.trans).Dispose();
			}
			this._IsDisposed = true;
		}

		// Token: 0x06000104 RID: 260
		public abstract void WriteMessageBegin(TMessage message);

		// Token: 0x06000105 RID: 261
		public abstract void WriteMessageEnd();

		// Token: 0x06000106 RID: 262
		public abstract void WriteStructBegin(TStruct struc);

		// Token: 0x06000107 RID: 263
		public abstract void WriteStructEnd();

		// Token: 0x06000108 RID: 264
		public abstract void WriteFieldBegin(TField field);

		// Token: 0x06000109 RID: 265
		public abstract void WriteFieldEnd();

		// Token: 0x0600010A RID: 266
		public abstract void WriteFieldStop();

		// Token: 0x0600010B RID: 267
		public abstract void WriteMapBegin(TMap map);

		// Token: 0x0600010C RID: 268
		public abstract void WriteMapEnd();

		// Token: 0x0600010D RID: 269
		public abstract void WriteListBegin(TList list);

		// Token: 0x0600010E RID: 270
		public abstract void WriteListEnd();

		// Token: 0x0600010F RID: 271
		public abstract void WriteSetBegin(TSet set);

		// Token: 0x06000110 RID: 272
		public abstract void WriteSetEnd();

		// Token: 0x06000111 RID: 273
		public abstract void WriteBool(bool b);

		// Token: 0x06000112 RID: 274
		public abstract void WriteByte(sbyte b);

		// Token: 0x06000113 RID: 275
		public abstract void WriteI16(short i16);

		// Token: 0x06000114 RID: 276
		public abstract void WriteI32(int i32);

		// Token: 0x06000115 RID: 277
		public abstract void WriteI64(long i64);

		// Token: 0x06000116 RID: 278
		public abstract void WriteDouble(double d);

		// Token: 0x06000117 RID: 279 RVA: 0x00004F6C File Offset: 0x0000316C
		public virtual void WriteString(string s)
		{
			this.WriteBinary(Encoding.UTF8.GetBytes(s));
		}

		// Token: 0x06000118 RID: 280
		public abstract void WriteBinary(byte[] b);

		// Token: 0x06000119 RID: 281
		public abstract TMessage ReadMessageBegin();

		// Token: 0x0600011A RID: 282
		public abstract void ReadMessageEnd();

		// Token: 0x0600011B RID: 283
		public abstract TStruct ReadStructBegin();

		// Token: 0x0600011C RID: 284
		public abstract void ReadStructEnd();

		// Token: 0x0600011D RID: 285
		public abstract TField ReadFieldBegin();

		// Token: 0x0600011E RID: 286
		public abstract void ReadFieldEnd();

		// Token: 0x0600011F RID: 287
		public abstract TMap ReadMapBegin();

		// Token: 0x06000120 RID: 288
		public abstract void ReadMapEnd();

		// Token: 0x06000121 RID: 289
		public abstract TList ReadListBegin();

		// Token: 0x06000122 RID: 290
		public abstract void ReadListEnd();

		// Token: 0x06000123 RID: 291
		public abstract TSet ReadSetBegin();

		// Token: 0x06000124 RID: 292
		public abstract void ReadSetEnd();

		// Token: 0x06000125 RID: 293
		public abstract bool ReadBool();

		// Token: 0x06000126 RID: 294
		public abstract sbyte ReadByte();

		// Token: 0x06000127 RID: 295
		public abstract short ReadI16();

		// Token: 0x06000128 RID: 296
		public abstract int ReadI32();

		// Token: 0x06000129 RID: 297
		public abstract long ReadI64();

		// Token: 0x0600012A RID: 298
		public abstract double ReadDouble();

		// Token: 0x0600012B RID: 299 RVA: 0x00004F80 File Offset: 0x00003180
		public virtual string ReadString()
		{
			byte[] array = this.ReadBinary();
			return Encoding.UTF8.GetString(array, 0, array.Length);
		}

		// Token: 0x0600012C RID: 300
		public abstract byte[] ReadBinary();

		// Token: 0x04000069 RID: 105
		private const int DEFAULT_RECURSION_DEPTH = 64;

		// Token: 0x0400006A RID: 106
		protected TTransport trans;

		// Token: 0x0400006B RID: 107
		protected int recursionLimit;

		// Token: 0x0400006C RID: 108
		protected int recursionDepth;

		// Token: 0x0400006D RID: 109
		private bool _IsDisposed;
	}
}
