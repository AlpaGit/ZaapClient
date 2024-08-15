using System;
using System.Collections.Generic;
using System.Text;
using Thrift.Transport;

namespace Thrift.Protocol
{
	// Token: 0x0200000B RID: 11
	public class TCompactProtocol : TProtocol
	{
		// Token: 0x06000044 RID: 68 RVA: 0x00002B90 File Offset: 0x00000D90
		public TCompactProtocol(TTransport trans)
			: base(trans)
		{
			TCompactProtocol.ttypeToCompactType[0] = 0;
			TCompactProtocol.ttypeToCompactType[2] = 1;
			TCompactProtocol.ttypeToCompactType[3] = 3;
			TCompactProtocol.ttypeToCompactType[6] = 4;
			TCompactProtocol.ttypeToCompactType[8] = 5;
			TCompactProtocol.ttypeToCompactType[10] = 6;
			TCompactProtocol.ttypeToCompactType[4] = 7;
			TCompactProtocol.ttypeToCompactType[11] = 8;
			TCompactProtocol.ttypeToCompactType[15] = 9;
			TCompactProtocol.ttypeToCompactType[14] = 10;
			TCompactProtocol.ttypeToCompactType[13] = 11;
			TCompactProtocol.ttypeToCompactType[12] = 12;
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002C4C File Offset: 0x00000E4C
		public void reset()
		{
			this.lastField_.Clear();
			this.lastFieldId_ = 0;
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002C60 File Offset: 0x00000E60
		private void WriteByteDirect(byte b)
		{
			this.byteDirectBuffer[0] = b;
			this.trans.Write(this.byteDirectBuffer);
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002C7C File Offset: 0x00000E7C
		private void WriteByteDirect(int n)
		{
			this.WriteByteDirect((byte)n);
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002C88 File Offset: 0x00000E88
		private void WriteVarint32(uint n)
		{
			int num = 0;
			while (((ulong)n & 18446744073709551488UL) != 0UL)
			{
				this.i32buf[num++] = (byte)((n & 127U) | 128U);
				n >>= 7;
			}
			this.i32buf[num++] = (byte)n;
			this.trans.Write(this.i32buf, 0, num);
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002CEC File Offset: 0x00000EEC
		public override void WriteMessageBegin(TMessage message)
		{
			this.WriteByteDirect(130);
			this.WriteByteDirect((byte)(TMessageType.Call | (((int)message.Type << 5) & (TMessageType)224)));
			this.WriteVarint32((uint)message.SeqID);
			this.WriteString(message.Name);
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002D2C File Offset: 0x00000F2C
		public override void WriteStructBegin(TStruct strct)
		{
			this.lastField_.Push(this.lastFieldId_);
			this.lastFieldId_ = 0;
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002D48 File Offset: 0x00000F48
		public override void WriteStructEnd()
		{
			this.lastFieldId_ = this.lastField_.Pop();
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002D5C File Offset: 0x00000F5C
		public override void WriteFieldBegin(TField field)
		{
			if (field.Type == TType.Bool)
			{
				this.booleanField_ = new TField?(field);
			}
			else
			{
				this.WriteFieldBeginInternal(field, byte.MaxValue);
			}
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002D88 File Offset: 0x00000F88
		private void WriteFieldBeginInternal(TField field, byte typeOverride)
		{
			byte b = ((typeOverride != byte.MaxValue) ? typeOverride : this.getCompactType(field.Type));
			if (field.ID > this.lastFieldId_ && field.ID - this.lastFieldId_ <= 15)
			{
				this.WriteByteDirect(((int)(field.ID - this.lastFieldId_) << 4) | (int)b);
			}
			else
			{
				this.WriteByteDirect(b);
				this.WriteI16(field.ID);
			}
			this.lastFieldId_ = field.ID;
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002E1C File Offset: 0x0000101C
		public override void WriteFieldStop()
		{
			this.WriteByteDirect(0);
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002E28 File Offset: 0x00001028
		public override void WriteMapBegin(TMap map)
		{
			if (map.Count == 0)
			{
				this.WriteByteDirect(0);
			}
			else
			{
				this.WriteVarint32((uint)map.Count);
				this.WriteByteDirect(((int)this.getCompactType(map.KeyType) << 4) | (int)this.getCompactType(map.ValueType));
			}
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002E80 File Offset: 0x00001080
		public override void WriteListBegin(TList list)
		{
			this.WriteCollectionBegin(list.ElementType, list.Count);
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002E98 File Offset: 0x00001098
		public override void WriteSetBegin(TSet set)
		{
			this.WriteCollectionBegin(set.ElementType, set.Count);
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002EB0 File Offset: 0x000010B0
		public override void WriteBool(bool b)
		{
			TField? tfield = this.booleanField_;
			if (tfield != null)
			{
				this.WriteFieldBeginInternal(this.booleanField_.Value, (!b) ? 2 : 1);
				this.booleanField_ = null;
			}
			else
			{
				this.WriteByteDirect((!b) ? 2 : 1);
			}
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002F18 File Offset: 0x00001118
		public override void WriteByte(sbyte b)
		{
			this.WriteByteDirect((byte)b);
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002F24 File Offset: 0x00001124
		public override void WriteI16(short i16)
		{
			this.WriteVarint32(this.intToZigZag((int)i16));
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002F34 File Offset: 0x00001134
		public override void WriteI32(int i32)
		{
			this.WriteVarint32(this.intToZigZag(i32));
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002F44 File Offset: 0x00001144
		public override void WriteI64(long i64)
		{
			this.WriteVarint64(this.longToZigzag(i64));
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002F54 File Offset: 0x00001154
		public override void WriteDouble(double dub)
		{
			byte[] array = new byte[8];
			this.fixedLongToBytes(BitConverter.DoubleToInt64Bits(dub), array, 0);
			this.trans.Write(array);
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00002F84 File Offset: 0x00001184
		public override void WriteString(string str)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(str);
			this.WriteBinary(bytes, 0, bytes.Length);
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002FA8 File Offset: 0x000011A8
		public override void WriteBinary(byte[] bin)
		{
			this.WriteBinary(bin, 0, bin.Length);
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00002FB8 File Offset: 0x000011B8
		private void WriteBinary(byte[] buf, int offset, int length)
		{
			this.WriteVarint32((uint)length);
			this.trans.Write(buf, offset, length);
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00002FD0 File Offset: 0x000011D0
		public override void WriteMessageEnd()
		{
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00002FD4 File Offset: 0x000011D4
		public override void WriteMapEnd()
		{
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00002FD8 File Offset: 0x000011D8
		public override void WriteListEnd()
		{
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00002FDC File Offset: 0x000011DC
		public override void WriteSetEnd()
		{
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00002FE0 File Offset: 0x000011E0
		public override void WriteFieldEnd()
		{
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00002FE4 File Offset: 0x000011E4
		protected void WriteCollectionBegin(TType elemType, int size)
		{
			if (size <= 14)
			{
				this.WriteByteDirect((size << 4) | (int)this.getCompactType(elemType));
			}
			else
			{
				this.WriteByteDirect((int)(240 | this.getCompactType(elemType)));
				this.WriteVarint32((uint)size);
			}
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00003020 File Offset: 0x00001220
		private void WriteVarint64(ulong n)
		{
			int num = 0;
			while ((n & 18446744073709551488UL) != 0UL)
			{
				this.varint64out[num++] = (byte)((n & 127UL) | 128UL);
				n >>= 7;
			}
			this.varint64out[num++] = (byte)n;
			this.trans.Write(this.varint64out, 0, num);
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00003084 File Offset: 0x00001284
		private ulong longToZigzag(long n)
		{
			return (ulong)((n << 1) ^ (n >> 63));
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00003090 File Offset: 0x00001290
		private uint intToZigZag(int n)
		{
			return (uint)((n << 1) ^ (n >> 31));
		}

		// Token: 0x06000064 RID: 100 RVA: 0x0000309C File Offset: 0x0000129C
		private void fixedLongToBytes(long n, byte[] buf, int off)
		{
			buf[off] = (byte)(n & 255L);
			buf[off + 1] = (byte)((n >> 8) & 255L);
			buf[off + 2] = (byte)((n >> 16) & 255L);
			buf[off + 3] = (byte)((n >> 24) & 255L);
			buf[off + 4] = (byte)((n >> 32) & 255L);
			buf[off + 5] = (byte)((n >> 40) & 255L);
			buf[off + 6] = (byte)((n >> 48) & 255L);
			buf[off + 7] = (byte)((n >> 56) & 255L);
		}

		// Token: 0x06000065 RID: 101 RVA: 0x0000312C File Offset: 0x0000132C
		public override TMessage ReadMessageBegin()
		{
			byte b = (byte)this.ReadByte();
			if (b != 130)
			{
				throw new TProtocolException("Expected protocol id " + 130.ToString("X") + " but got " + b.ToString("X"));
			}
			byte b2 = (byte)this.ReadByte();
			byte b3 = b2 & 31;
			if (b3 != 1)
			{
				throw new TProtocolException(string.Concat(new object[] { "Expected version ", 1, " but got ", b3 }));
			}
			byte b4 = (byte)((b2 >> 5) & 7);
			int num = (int)this.ReadVarint32();
			string text = this.ReadString();
			return new TMessage(text, (TMessageType)b4, num);
		}

		// Token: 0x06000066 RID: 102 RVA: 0x000031EC File Offset: 0x000013EC
		public override TStruct ReadStructBegin()
		{
			this.lastField_.Push(this.lastFieldId_);
			this.lastFieldId_ = 0;
			return TCompactProtocol.ANONYMOUS_STRUCT;
		}

		// Token: 0x06000067 RID: 103 RVA: 0x0000320C File Offset: 0x0000140C
		public override void ReadStructEnd()
		{
			this.lastFieldId_ = this.lastField_.Pop();
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00003220 File Offset: 0x00001420
		public override TField ReadFieldBegin()
		{
			byte b = (byte)this.ReadByte();
			if (b == 0)
			{
				return TCompactProtocol.TSTOP;
			}
			short num = (short)((b & 240) >> 4);
			short num2;
			if (num == 0)
			{
				num2 = this.ReadI16();
			}
			else
			{
				num2 = this.lastFieldId_ + num;
			}
			TField tfield = new TField(string.Empty, this.getTType(b & 15), num2);
			if (this.isBoolType(b))
			{
				this.boolValue_ = new bool?((b & 15) == 1);
			}
			this.lastFieldId_ = tfield.ID;
			return tfield;
		}

		// Token: 0x06000069 RID: 105 RVA: 0x000032B8 File Offset: 0x000014B8
		public override TMap ReadMapBegin()
		{
			int num = (int)this.ReadVarint32();
			byte b = ((num != 0) ? ((byte)this.ReadByte()) : 0);
			return new TMap(this.getTType((byte)(b >> 4)), this.getTType(b & 15), num);
		}

		// Token: 0x0600006A RID: 106 RVA: 0x000032FC File Offset: 0x000014FC
		public override TList ReadListBegin()
		{
			byte b = (byte)this.ReadByte();
			int num = (b >> 4) & 15;
			if (num == 15)
			{
				num = (int)this.ReadVarint32();
			}
			TType ttype = this.getTType(b);
			return new TList(ttype, num);
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00003338 File Offset: 0x00001538
		public override TSet ReadSetBegin()
		{
			return new TSet(this.ReadListBegin());
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00003348 File Offset: 0x00001548
		public override bool ReadBool()
		{
			bool? flag = this.boolValue_;
			if (flag != null)
			{
				bool value = this.boolValue_.Value;
				this.boolValue_ = null;
				return value;
			}
			return (int)this.ReadByte() == 1;
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00003390 File Offset: 0x00001590
		public override sbyte ReadByte()
		{
			this.trans.ReadAll(this.byteRawBuf, 0, 1);
			return (sbyte)this.byteRawBuf[0];
		}

		// Token: 0x0600006E RID: 110 RVA: 0x000033B0 File Offset: 0x000015B0
		public override short ReadI16()
		{
			return (short)this.zigzagToInt(this.ReadVarint32());
		}

		// Token: 0x0600006F RID: 111 RVA: 0x000033C0 File Offset: 0x000015C0
		public override int ReadI32()
		{
			return this.zigzagToInt(this.ReadVarint32());
		}

		// Token: 0x06000070 RID: 112 RVA: 0x000033D0 File Offset: 0x000015D0
		public override long ReadI64()
		{
			return this.zigzagToLong(this.ReadVarint64());
		}

		// Token: 0x06000071 RID: 113 RVA: 0x000033E0 File Offset: 0x000015E0
		public override double ReadDouble()
		{
			byte[] array = new byte[8];
			this.trans.ReadAll(array, 0, 8);
			return BitConverter.Int64BitsToDouble(this.bytesToLong(array));
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00003410 File Offset: 0x00001610
		public override string ReadString()
		{
			int num = (int)this.ReadVarint32();
			if (num == 0)
			{
				return string.Empty;
			}
			return Encoding.UTF8.GetString(this.ReadBinary(num));
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00003444 File Offset: 0x00001644
		public override byte[] ReadBinary()
		{
			int num = (int)this.ReadVarint32();
			if (num == 0)
			{
				return new byte[0];
			}
			byte[] array = new byte[num];
			this.trans.ReadAll(array, 0, num);
			return array;
		}

		// Token: 0x06000074 RID: 116 RVA: 0x0000347C File Offset: 0x0000167C
		private byte[] ReadBinary(int length)
		{
			if (length == 0)
			{
				return new byte[0];
			}
			byte[] array = new byte[length];
			this.trans.ReadAll(array, 0, length);
			return array;
		}

		// Token: 0x06000075 RID: 117 RVA: 0x000034B0 File Offset: 0x000016B0
		public override void ReadMessageEnd()
		{
		}

		// Token: 0x06000076 RID: 118 RVA: 0x000034B4 File Offset: 0x000016B4
		public override void ReadFieldEnd()
		{
		}

		// Token: 0x06000077 RID: 119 RVA: 0x000034B8 File Offset: 0x000016B8
		public override void ReadMapEnd()
		{
		}

		// Token: 0x06000078 RID: 120 RVA: 0x000034BC File Offset: 0x000016BC
		public override void ReadListEnd()
		{
		}

		// Token: 0x06000079 RID: 121 RVA: 0x000034C0 File Offset: 0x000016C0
		public override void ReadSetEnd()
		{
		}

		// Token: 0x0600007A RID: 122 RVA: 0x000034C4 File Offset: 0x000016C4
		private uint ReadVarint32()
		{
			uint num = 0U;
			int num2 = 0;
			for (;;)
			{
				byte b = (byte)this.ReadByte();
				num |= (uint)((uint)(b & 127) << num2);
				if ((b & 128) != 128)
				{
					break;
				}
				num2 += 7;
			}
			return num;
		}

		// Token: 0x0600007B RID: 123 RVA: 0x0000350C File Offset: 0x0000170C
		private ulong ReadVarint64()
		{
			int num = 0;
			ulong num2 = 0UL;
			for (;;)
			{
				byte b = (byte)this.ReadByte();
				num2 |= (ulong)((ulong)((long)(b & 127)) << num);
				if ((b & 128) != 128)
				{
					break;
				}
				num += 7;
			}
			return num2;
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00003554 File Offset: 0x00001754
		private int zigzagToInt(uint n)
		{
			return (int)((n >> 1) ^ -(int)(n & 1U));
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00003560 File Offset: 0x00001760
		private long zigzagToLong(ulong n)
		{
			return (long)((n >> 1) ^ -(long)(n & 1UL));
		}

		// Token: 0x0600007E RID: 126 RVA: 0x0000356C File Offset: 0x0000176C
		private long bytesToLong(byte[] bytes)
		{
			return (((long)bytes[7] & 255L) << 56) | (((long)bytes[6] & 255L) << 48) | (((long)bytes[5] & 255L) << 40) | (((long)bytes[4] & 255L) << 32) | (((long)bytes[3] & 255L) << 24) | (((long)bytes[2] & 255L) << 16) | (((long)bytes[1] & 255L) << 8) | ((long)bytes[0] & 255L);
		}

		// Token: 0x0600007F RID: 127 RVA: 0x000035EC File Offset: 0x000017EC
		private bool isBoolType(byte b)
		{
			int num = (int)(b & 15);
			return num == 1 || num == 2;
		}

		// Token: 0x06000080 RID: 128 RVA: 0x0000360C File Offset: 0x0000180C
		private TType getTType(byte type)
		{
			switch (type & 15)
			{
			case 0:
				return TType.Stop;
			case 1:
			case 2:
				return TType.Bool;
			case 3:
				return TType.Byte;
			case 4:
				return TType.I16;
			case 5:
				return TType.I32;
			case 6:
				return TType.I64;
			case 7:
				return TType.Double;
			case 8:
				return TType.String;
			case 9:
				return TType.List;
			case 10:
				return TType.Set;
			case 11:
				return TType.Map;
			case 12:
				return TType.Struct;
			default:
				throw new TProtocolException("don't know what type: " + (type & 15));
			}
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00003698 File Offset: 0x00001898
		private byte getCompactType(TType ttype)
		{
			return TCompactProtocol.ttypeToCompactType[(int)ttype];
		}

		// Token: 0x04000012 RID: 18
		private static TStruct ANONYMOUS_STRUCT = new TStruct(string.Empty);

		// Token: 0x04000013 RID: 19
		private static TField TSTOP = new TField(string.Empty, TType.Stop, 0);

		// Token: 0x04000014 RID: 20
		private static byte[] ttypeToCompactType = new byte[16];

		// Token: 0x04000015 RID: 21
		private const byte PROTOCOL_ID = 130;

		// Token: 0x04000016 RID: 22
		private const byte VERSION = 1;

		// Token: 0x04000017 RID: 23
		private const byte VERSION_MASK = 31;

		// Token: 0x04000018 RID: 24
		private const byte TYPE_MASK = 224;

		// Token: 0x04000019 RID: 25
		private const byte TYPE_BITS = 7;

		// Token: 0x0400001A RID: 26
		private const int TYPE_SHIFT_AMOUNT = 5;

		// Token: 0x0400001B RID: 27
		private Stack<short> lastField_ = new Stack<short>(15);

		// Token: 0x0400001C RID: 28
		private short lastFieldId_;

		// Token: 0x0400001D RID: 29
		private TField? booleanField_;

		// Token: 0x0400001E RID: 30
		private bool? boolValue_;

		// Token: 0x0400001F RID: 31
		private byte[] byteDirectBuffer = new byte[1];

		// Token: 0x04000020 RID: 32
		private byte[] i32buf = new byte[5];

		// Token: 0x04000021 RID: 33
		private byte[] varint64out = new byte[10];

		// Token: 0x04000022 RID: 34
		private byte[] byteRawBuf = new byte[1];

		// Token: 0x0200000C RID: 12
		private static class Types
		{
			// Token: 0x04000023 RID: 35
			public const byte STOP = 0;

			// Token: 0x04000024 RID: 36
			public const byte BOOLEAN_TRUE = 1;

			// Token: 0x04000025 RID: 37
			public const byte BOOLEAN_FALSE = 2;

			// Token: 0x04000026 RID: 38
			public const byte BYTE = 3;

			// Token: 0x04000027 RID: 39
			public const byte I16 = 4;

			// Token: 0x04000028 RID: 40
			public const byte I32 = 5;

			// Token: 0x04000029 RID: 41
			public const byte I64 = 6;

			// Token: 0x0400002A RID: 42
			public const byte DOUBLE = 7;

			// Token: 0x0400002B RID: 43
			public const byte BINARY = 8;

			// Token: 0x0400002C RID: 44
			public const byte LIST = 9;

			// Token: 0x0400002D RID: 45
			public const byte SET = 10;

			// Token: 0x0400002E RID: 46
			public const byte MAP = 11;

			// Token: 0x0400002F RID: 47
			public const byte STRUCT = 12;
		}

		// Token: 0x0200000D RID: 13
		public class Factory : TProtocolFactory
		{
			// Token: 0x06000084 RID: 132 RVA: 0x000036DC File Offset: 0x000018DC
			public TProtocol GetProtocol(TTransport trans)
			{
				return new TCompactProtocol(trans);
			}
		}
	}
}
