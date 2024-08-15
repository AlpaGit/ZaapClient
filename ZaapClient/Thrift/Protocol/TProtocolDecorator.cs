using System;

namespace Thrift.Protocol
{
	// Token: 0x0200001D RID: 29
	public abstract class TProtocolDecorator : TProtocol
	{
		// Token: 0x0600012D RID: 301 RVA: 0x00004FA4 File Offset: 0x000031A4
		public TProtocolDecorator(TProtocol protocol)
			: base(protocol.Transport)
		{
			this.WrappedProtocol = protocol;
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00004FBC File Offset: 0x000031BC
		public override void WriteMessageBegin(TMessage tMessage)
		{
			this.WrappedProtocol.WriteMessageBegin(tMessage);
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00004FCC File Offset: 0x000031CC
		public override void WriteMessageEnd()
		{
			this.WrappedProtocol.WriteMessageEnd();
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00004FDC File Offset: 0x000031DC
		public override void WriteStructBegin(TStruct tStruct)
		{
			this.WrappedProtocol.WriteStructBegin(tStruct);
		}

		// Token: 0x06000131 RID: 305 RVA: 0x00004FEC File Offset: 0x000031EC
		public override void WriteStructEnd()
		{
			this.WrappedProtocol.WriteStructEnd();
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00004FFC File Offset: 0x000031FC
		public override void WriteFieldBegin(TField tField)
		{
			this.WrappedProtocol.WriteFieldBegin(tField);
		}

		// Token: 0x06000133 RID: 307 RVA: 0x0000500C File Offset: 0x0000320C
		public override void WriteFieldEnd()
		{
			this.WrappedProtocol.WriteFieldEnd();
		}

		// Token: 0x06000134 RID: 308 RVA: 0x0000501C File Offset: 0x0000321C
		public override void WriteFieldStop()
		{
			this.WrappedProtocol.WriteFieldStop();
		}

		// Token: 0x06000135 RID: 309 RVA: 0x0000502C File Offset: 0x0000322C
		public override void WriteMapBegin(TMap tMap)
		{
			this.WrappedProtocol.WriteMapBegin(tMap);
		}

		// Token: 0x06000136 RID: 310 RVA: 0x0000503C File Offset: 0x0000323C
		public override void WriteMapEnd()
		{
			this.WrappedProtocol.WriteMapEnd();
		}

		// Token: 0x06000137 RID: 311 RVA: 0x0000504C File Offset: 0x0000324C
		public override void WriteListBegin(TList tList)
		{
			this.WrappedProtocol.WriteListBegin(tList);
		}

		// Token: 0x06000138 RID: 312 RVA: 0x0000505C File Offset: 0x0000325C
		public override void WriteListEnd()
		{
			this.WrappedProtocol.WriteListEnd();
		}

		// Token: 0x06000139 RID: 313 RVA: 0x0000506C File Offset: 0x0000326C
		public override void WriteSetBegin(TSet tSet)
		{
			this.WrappedProtocol.WriteSetBegin(tSet);
		}

		// Token: 0x0600013A RID: 314 RVA: 0x0000507C File Offset: 0x0000327C
		public override void WriteSetEnd()
		{
			this.WrappedProtocol.WriteSetEnd();
		}

		// Token: 0x0600013B RID: 315 RVA: 0x0000508C File Offset: 0x0000328C
		public override void WriteBool(bool b)
		{
			this.WrappedProtocol.WriteBool(b);
		}

		// Token: 0x0600013C RID: 316 RVA: 0x0000509C File Offset: 0x0000329C
		public override void WriteByte(sbyte b)
		{
			this.WrappedProtocol.WriteByte(b);
		}

		// Token: 0x0600013D RID: 317 RVA: 0x000050AC File Offset: 0x000032AC
		public override void WriteI16(short i)
		{
			this.WrappedProtocol.WriteI16(i);
		}

		// Token: 0x0600013E RID: 318 RVA: 0x000050BC File Offset: 0x000032BC
		public override void WriteI32(int i)
		{
			this.WrappedProtocol.WriteI32(i);
		}

		// Token: 0x0600013F RID: 319 RVA: 0x000050CC File Offset: 0x000032CC
		public override void WriteI64(long l)
		{
			this.WrappedProtocol.WriteI64(l);
		}

		// Token: 0x06000140 RID: 320 RVA: 0x000050DC File Offset: 0x000032DC
		public override void WriteDouble(double v)
		{
			this.WrappedProtocol.WriteDouble(v);
		}

		// Token: 0x06000141 RID: 321 RVA: 0x000050EC File Offset: 0x000032EC
		public override void WriteString(string s)
		{
			this.WrappedProtocol.WriteString(s);
		}

		// Token: 0x06000142 RID: 322 RVA: 0x000050FC File Offset: 0x000032FC
		public override void WriteBinary(byte[] bytes)
		{
			this.WrappedProtocol.WriteBinary(bytes);
		}

		// Token: 0x06000143 RID: 323 RVA: 0x0000510C File Offset: 0x0000330C
		public override TMessage ReadMessageBegin()
		{
			return this.WrappedProtocol.ReadMessageBegin();
		}

		// Token: 0x06000144 RID: 324 RVA: 0x0000511C File Offset: 0x0000331C
		public override void ReadMessageEnd()
		{
			this.WrappedProtocol.ReadMessageEnd();
		}

		// Token: 0x06000145 RID: 325 RVA: 0x0000512C File Offset: 0x0000332C
		public override TStruct ReadStructBegin()
		{
			return this.WrappedProtocol.ReadStructBegin();
		}

		// Token: 0x06000146 RID: 326 RVA: 0x0000513C File Offset: 0x0000333C
		public override void ReadStructEnd()
		{
			this.WrappedProtocol.ReadStructEnd();
		}

		// Token: 0x06000147 RID: 327 RVA: 0x0000514C File Offset: 0x0000334C
		public override TField ReadFieldBegin()
		{
			return this.WrappedProtocol.ReadFieldBegin();
		}

		// Token: 0x06000148 RID: 328 RVA: 0x0000515C File Offset: 0x0000335C
		public override void ReadFieldEnd()
		{
			this.WrappedProtocol.ReadFieldEnd();
		}

		// Token: 0x06000149 RID: 329 RVA: 0x0000516C File Offset: 0x0000336C
		public override TMap ReadMapBegin()
		{
			return this.WrappedProtocol.ReadMapBegin();
		}

		// Token: 0x0600014A RID: 330 RVA: 0x0000517C File Offset: 0x0000337C
		public override void ReadMapEnd()
		{
			this.WrappedProtocol.ReadMapEnd();
		}

		// Token: 0x0600014B RID: 331 RVA: 0x0000518C File Offset: 0x0000338C
		public override TList ReadListBegin()
		{
			return this.WrappedProtocol.ReadListBegin();
		}

		// Token: 0x0600014C RID: 332 RVA: 0x0000519C File Offset: 0x0000339C
		public override void ReadListEnd()
		{
			this.WrappedProtocol.ReadListEnd();
		}

		// Token: 0x0600014D RID: 333 RVA: 0x000051AC File Offset: 0x000033AC
		public override TSet ReadSetBegin()
		{
			return this.WrappedProtocol.ReadSetBegin();
		}

		// Token: 0x0600014E RID: 334 RVA: 0x000051BC File Offset: 0x000033BC
		public override void ReadSetEnd()
		{
			this.WrappedProtocol.ReadSetEnd();
		}

		// Token: 0x0600014F RID: 335 RVA: 0x000051CC File Offset: 0x000033CC
		public override bool ReadBool()
		{
			return this.WrappedProtocol.ReadBool();
		}

		// Token: 0x06000150 RID: 336 RVA: 0x000051DC File Offset: 0x000033DC
		public override sbyte ReadByte()
		{
			return this.WrappedProtocol.ReadByte();
		}

		// Token: 0x06000151 RID: 337 RVA: 0x000051EC File Offset: 0x000033EC
		public override short ReadI16()
		{
			return this.WrappedProtocol.ReadI16();
		}

		// Token: 0x06000152 RID: 338 RVA: 0x000051FC File Offset: 0x000033FC
		public override int ReadI32()
		{
			return this.WrappedProtocol.ReadI32();
		}

		// Token: 0x06000153 RID: 339 RVA: 0x0000520C File Offset: 0x0000340C
		public override long ReadI64()
		{
			return this.WrappedProtocol.ReadI64();
		}

		// Token: 0x06000154 RID: 340 RVA: 0x0000521C File Offset: 0x0000341C
		public override double ReadDouble()
		{
			return this.WrappedProtocol.ReadDouble();
		}

		// Token: 0x06000155 RID: 341 RVA: 0x0000522C File Offset: 0x0000342C
		public override string ReadString()
		{
			return this.WrappedProtocol.ReadString();
		}

		// Token: 0x06000156 RID: 342 RVA: 0x0000523C File Offset: 0x0000343C
		public override byte[] ReadBinary()
		{
			return this.WrappedProtocol.ReadBinary();
		}

		// Token: 0x0400006E RID: 110
		private TProtocol WrappedProtocol;
	}
}
