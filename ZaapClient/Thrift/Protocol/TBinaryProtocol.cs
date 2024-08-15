using System;
using System.Text;
using Thrift.Transport;

namespace Thrift.Protocol
{
	// Token: 0x02000009 RID: 9
	public class TBinaryProtocol : TProtocol
	{
		// Token: 0x06000016 RID: 22 RVA: 0x00002410 File Offset: 0x00000610
		public TBinaryProtocol(TTransport trans)
			: this(trans, false, true)
		{
		}

		// Token: 0x06000017 RID: 23 RVA: 0x0000241C File Offset: 0x0000061C
		public TBinaryProtocol(TTransport trans, bool strictRead, bool strictWrite)
			: base(trans)
		{
			this.strictRead_ = strictRead;
			this.strictWrite_ = strictWrite;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000024A8 File Offset: 0x000006A8
		public override void WriteMessageBegin(TMessage message)
		{
			if (this.strictWrite_)
			{
				uint num = (uint)((TMessageType)(-2147418112) | message.Type);
				this.WriteI32((int)num);
				this.WriteString(message.Name);
				this.WriteI32(message.SeqID);
			}
			else
			{
				this.WriteString(message.Name);
				this.WriteByte((sbyte)message.Type);
				this.WriteI32(message.SeqID);
			}
		}

		// Token: 0x06000019 RID: 25 RVA: 0x0000251C File Offset: 0x0000071C
		public override void WriteMessageEnd()
		{
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002520 File Offset: 0x00000720
		public override void WriteStructBegin(TStruct struc)
		{
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002524 File Offset: 0x00000724
		public override void WriteStructEnd()
		{
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002528 File Offset: 0x00000728
		public override void WriteFieldBegin(TField field)
		{
			this.WriteByte((sbyte)field.Type);
			this.WriteI16(field.ID);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002548 File Offset: 0x00000748
		public override void WriteFieldEnd()
		{
		}

		// Token: 0x0600001E RID: 30 RVA: 0x0000254C File Offset: 0x0000074C
		public override void WriteFieldStop()
		{
			this.WriteByte(0);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002558 File Offset: 0x00000758
		public override void WriteMapBegin(TMap map)
		{
			this.WriteByte((sbyte)map.KeyType);
			this.WriteByte((sbyte)map.ValueType);
			this.WriteI32(map.Count);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002584 File Offset: 0x00000784
		public override void WriteMapEnd()
		{
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002588 File Offset: 0x00000788
		public override void WriteListBegin(TList list)
		{
			this.WriteByte((sbyte)list.ElementType);
			this.WriteI32(list.Count);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000025A8 File Offset: 0x000007A8
		public override void WriteListEnd()
		{
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000025AC File Offset: 0x000007AC
		public override void WriteSetBegin(TSet set)
		{
			this.WriteByte((sbyte)set.ElementType);
			this.WriteI32(set.Count);
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000025CC File Offset: 0x000007CC
		public override void WriteSetEnd()
		{
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000025D0 File Offset: 0x000007D0
		public override void WriteBool(bool b)
		{
			this.WriteByte((!b) ? 0 : 1);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000025E8 File Offset: 0x000007E8
		public override void WriteByte(sbyte b)
		{
			this.bout[0] = (byte)b;
			this.trans.Write(this.bout, 0, 1);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002608 File Offset: 0x00000808
		public override void WriteI16(short s)
		{
			this.i16out[0] = (byte)(255 & (s >> 8));
			this.i16out[1] = (byte)(255 & s);
			this.trans.Write(this.i16out, 0, 2);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002640 File Offset: 0x00000840
		public override void WriteI32(int i32)
		{
			this.i32out[0] = (byte)(255 & (i32 >> 24));
			this.i32out[1] = (byte)(255 & (i32 >> 16));
			this.i32out[2] = (byte)(255 & (i32 >> 8));
			this.i32out[3] = (byte)(255 & i32);
			this.trans.Write(this.i32out, 0, 4);
		}

		// Token: 0x06000029 RID: 41 RVA: 0x000026A8 File Offset: 0x000008A8
		public override void WriteI64(long i64)
		{
			this.i64out[0] = (byte)(255L & (i64 >> 56));
			this.i64out[1] = (byte)(255L & (i64 >> 48));
			this.i64out[2] = (byte)(255L & (i64 >> 40));
			this.i64out[3] = (byte)(255L & (i64 >> 32));
			this.i64out[4] = (byte)(255L & (i64 >> 24));
			this.i64out[5] = (byte)(255L & (i64 >> 16));
			this.i64out[6] = (byte)(255L & (i64 >> 8));
			this.i64out[7] = (byte)(255L & i64);
			this.trans.Write(this.i64out, 0, 8);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002764 File Offset: 0x00000964
		public override void WriteDouble(double d)
		{
			this.WriteI64(BitConverter.DoubleToInt64Bits(d));
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002774 File Offset: 0x00000974
		public override void WriteBinary(byte[] b)
		{
			this.WriteI32(b.Length);
			this.trans.Write(b, 0, b.Length);
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002790 File Offset: 0x00000990
		public override TMessage ReadMessageBegin()
		{
			TMessage tmessage = default(TMessage);
			int num = this.ReadI32();
			if (num < 0)
			{
				uint num2 = (uint)(num & -65536);
				if (num2 != 2147549184U)
				{
					throw new TProtocolException(4, "Bad version in ReadMessageBegin: " + num2);
				}
				tmessage.Type = (TMessageType)(num & 255);
				tmessage.Name = this.ReadString();
				tmessage.SeqID = this.ReadI32();
			}
			else
			{
				if (this.strictRead_)
				{
					throw new TProtocolException(4, "Missing version in readMessageBegin, old client?");
				}
				tmessage.Name = this.ReadStringBody(num);
				tmessage.Type = (TMessageType)this.ReadByte();
				tmessage.SeqID = this.ReadI32();
			}
			return tmessage;
		}

		// Token: 0x0600002D RID: 45 RVA: 0x0000284C File Offset: 0x00000A4C
		public override void ReadMessageEnd()
		{
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002850 File Offset: 0x00000A50
		public override TStruct ReadStructBegin()
		{
			return default(TStruct);
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002868 File Offset: 0x00000A68
		public override void ReadStructEnd()
		{
		}

		// Token: 0x06000030 RID: 48 RVA: 0x0000286C File Offset: 0x00000A6C
		public override TField ReadFieldBegin()
		{
			TField tfield = default(TField);
			tfield.Type = (TType)this.ReadByte();
			if (tfield.Type != TType.Stop)
			{
				tfield.ID = this.ReadI16();
			}
			return tfield;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x000028AC File Offset: 0x00000AAC
		public override void ReadFieldEnd()
		{
		}

		// Token: 0x06000032 RID: 50 RVA: 0x000028B0 File Offset: 0x00000AB0
		public override TMap ReadMapBegin()
		{
			return new TMap
			{
				KeyType = (TType)this.ReadByte(),
				ValueType = (TType)this.ReadByte(),
				Count = this.ReadI32()
			};
		}

		// Token: 0x06000033 RID: 51 RVA: 0x000028F0 File Offset: 0x00000AF0
		public override void ReadMapEnd()
		{
		}

		// Token: 0x06000034 RID: 52 RVA: 0x000028F4 File Offset: 0x00000AF4
		public override TList ReadListBegin()
		{
			return new TList
			{
				ElementType = (TType)this.ReadByte(),
				Count = this.ReadI32()
			};
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002928 File Offset: 0x00000B28
		public override void ReadListEnd()
		{
		}

		// Token: 0x06000036 RID: 54 RVA: 0x0000292C File Offset: 0x00000B2C
		public override TSet ReadSetBegin()
		{
			return new TSet
			{
				ElementType = (TType)this.ReadByte(),
				Count = this.ReadI32()
			};
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002960 File Offset: 0x00000B60
		public override void ReadSetEnd()
		{
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002964 File Offset: 0x00000B64
		public override bool ReadBool()
		{
			return (int)this.ReadByte() == 1;
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002970 File Offset: 0x00000B70
		public override sbyte ReadByte()
		{
			this.ReadAll(this.bin, 0, 1);
			return (sbyte)this.bin[0];
		}

		// Token: 0x0600003A RID: 58 RVA: 0x0000298C File Offset: 0x00000B8C
		public override short ReadI16()
		{
			this.ReadAll(this.i16in, 0, 2);
			return (short)(((int)(this.i16in[0] & byte.MaxValue) << 8) | (int)(this.i16in[1] & byte.MaxValue));
		}

		// Token: 0x0600003B RID: 59 RVA: 0x000029C0 File Offset: 0x00000BC0
		public override int ReadI32()
		{
			this.ReadAll(this.i32in, 0, 4);
			return ((int)(this.i32in[0] & byte.MaxValue) << 24) | ((int)(this.i32in[1] & byte.MaxValue) << 16) | ((int)(this.i32in[2] & byte.MaxValue) << 8) | (int)(this.i32in[3] & byte.MaxValue);
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002A20 File Offset: 0x00000C20
		public override long ReadI64()
		{
			this.ReadAll(this.i64in, 0, 8);
			return ((long)(this.i64in[0] & byte.MaxValue) << 56) | ((long)(this.i64in[1] & byte.MaxValue) << 48) | ((long)(this.i64in[2] & byte.MaxValue) << 40) | ((long)(this.i64in[3] & byte.MaxValue) << 32) | ((long)(this.i64in[4] & byte.MaxValue) << 24) | ((long)(this.i64in[5] & byte.MaxValue) << 16) | ((long)(this.i64in[6] & byte.MaxValue) << 8) | (long)(this.i64in[7] & byte.MaxValue);
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002AD0 File Offset: 0x00000CD0
		public override double ReadDouble()
		{
			return BitConverter.Int64BitsToDouble(this.ReadI64());
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002AE0 File Offset: 0x00000CE0
		public override byte[] ReadBinary()
		{
			int num = this.ReadI32();
			byte[] array = new byte[num];
			this.trans.ReadAll(array, 0, num);
			return array;
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002B0C File Offset: 0x00000D0C
		private string ReadStringBody(int size)
		{
			byte[] array = new byte[size];
			this.trans.ReadAll(array, 0, size);
			return Encoding.UTF8.GetString(array, 0, array.Length);
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002B40 File Offset: 0x00000D40
		private int ReadAll(byte[] buf, int off, int len)
		{
			return this.trans.ReadAll(buf, off, len);
		}

		// Token: 0x04000004 RID: 4
		protected const uint VERSION_MASK = 4294901760U;

		// Token: 0x04000005 RID: 5
		protected const uint VERSION_1 = 2147549184U;

		// Token: 0x04000006 RID: 6
		protected bool strictRead_;

		// Token: 0x04000007 RID: 7
		protected bool strictWrite_ = true;

		// Token: 0x04000008 RID: 8
		private byte[] bout = new byte[1];

		// Token: 0x04000009 RID: 9
		private byte[] i16out = new byte[2];

		// Token: 0x0400000A RID: 10
		private byte[] i32out = new byte[4];

		// Token: 0x0400000B RID: 11
		private byte[] i64out = new byte[8];

		// Token: 0x0400000C RID: 12
		private byte[] bin = new byte[1];

		// Token: 0x0400000D RID: 13
		private byte[] i16in = new byte[2];

		// Token: 0x0400000E RID: 14
		private byte[] i32in = new byte[4];

		// Token: 0x0400000F RID: 15
		private byte[] i64in = new byte[8];

		// Token: 0x0200000A RID: 10
		public class Factory : TProtocolFactory
		{
			// Token: 0x06000041 RID: 65 RVA: 0x00002B50 File Offset: 0x00000D50
			public Factory()
				: this(false, true)
			{
			}

			// Token: 0x06000042 RID: 66 RVA: 0x00002B5C File Offset: 0x00000D5C
			public Factory(bool strictRead, bool strictWrite)
			{
				this.strictRead_ = strictRead;
				this.strictWrite_ = strictWrite;
			}

			// Token: 0x06000043 RID: 67 RVA: 0x00002B7C File Offset: 0x00000D7C
			public TProtocol GetProtocol(TTransport trans)
			{
				return new TBinaryProtocol(trans, this.strictRead_, this.strictWrite_);
			}

			// Token: 0x04000010 RID: 16
			protected bool strictRead_;

			// Token: 0x04000011 RID: 17
			protected bool strictWrite_ = true;
		}
	}
}
