using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using Thrift.Transport;

namespace Thrift.Protocol
{
	// Token: 0x0200000F RID: 15
	public class TJSONProtocol : TProtocol
	{
		// Token: 0x0600008C RID: 140 RVA: 0x00003740 File Offset: 0x00001940
		public TJSONProtocol(TTransport trans)
			: base(trans)
		{
			this.context = new TJSONProtocol.JSONBaseContext(this);
			this.reader = new TJSONProtocol.LookaheadReader(this);
		}

		// Token: 0x0600008D RID: 141 RVA: 0x000037E4 File Offset: 0x000019E4
		private static byte[] GetTypeNameForTypeID(TType typeID)
		{
			switch (typeID)
			{
			case TType.Bool:
				return TJSONProtocol.NAME_BOOL;
			case TType.Byte:
				return TJSONProtocol.NAME_BYTE;
			case TType.Double:
				return TJSONProtocol.NAME_DOUBLE;
			case TType.I16:
				return TJSONProtocol.NAME_I16;
			case TType.I32:
				return TJSONProtocol.NAME_I32;
			case TType.I64:
				return TJSONProtocol.NAME_I64;
			case TType.String:
				return TJSONProtocol.NAME_STRING;
			case TType.Struct:
				return TJSONProtocol.NAME_STRUCT;
			case TType.Map:
				return TJSONProtocol.NAME_MAP;
			case TType.Set:
				return TJSONProtocol.NAME_SET;
			case TType.List:
				return TJSONProtocol.NAME_LIST;
			}
			throw new TProtocolException(5, "Unrecognized type");
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00003884 File Offset: 0x00001A84
		private static TType GetTypeIDForTypeName(byte[] name)
		{
			TType ttype = TType.Stop;
			if (name.Length > 1)
			{
				byte b = name[0];
				switch (b)
				{
				case 105:
					switch (name[1])
					{
					case 49:
						ttype = TType.I16;
						break;
					case 51:
						ttype = TType.I32;
						break;
					case 54:
						ttype = TType.I64;
						break;
					case 56:
						ttype = TType.Byte;
						break;
					}
					break;
				default:
					switch (b)
					{
					case 114:
						ttype = TType.Struct;
						break;
					case 115:
						if (name[1] == 116)
						{
							ttype = TType.String;
						}
						else if (name[1] == 101)
						{
							ttype = TType.Set;
						}
						break;
					case 116:
						ttype = TType.Bool;
						break;
					default:
						if (b == 100)
						{
							ttype = TType.Double;
						}
						break;
					}
					break;
				case 108:
					ttype = TType.List;
					break;
				case 109:
					ttype = TType.Map;
					break;
				}
			}
			if (ttype == TType.Stop)
			{
				throw new TProtocolException(5, "Unrecognized type");
			}
			return ttype;
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00003990 File Offset: 0x00001B90
		protected void PushContext(TJSONProtocol.JSONBaseContext c)
		{
			this.contextStack.Push(this.context);
			this.context = c;
		}

		// Token: 0x06000090 RID: 144 RVA: 0x000039AC File Offset: 0x00001BAC
		protected void PopContext()
		{
			this.context = this.contextStack.Pop();
		}

		// Token: 0x06000091 RID: 145 RVA: 0x000039C0 File Offset: 0x00001BC0
		protected void ReadJSONSyntaxChar(byte[] b)
		{
			byte b2 = this.reader.Read();
			if (b2 != b[0])
			{
				throw new TProtocolException(1, "Unexpected character:" + (char)b2);
			}
		}

		// Token: 0x06000092 RID: 146 RVA: 0x000039FC File Offset: 0x00001BFC
		private static byte HexVal(byte ch)
		{
			if (ch >= 48 && ch <= 57)
			{
				return (byte)((ushort)ch - 48);
			}
			if (ch >= 97 && ch <= 102)
			{
				ch += 10;
				return (byte)((ushort)ch - 97);
			}
			throw new TProtocolException(1, "Expected hex character");
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00003A4C File Offset: 0x00001C4C
		private static byte HexChar(byte val)
		{
			val &= 15;
			if (val < 10)
			{
				return (byte)((ushort)val + 48);
			}
			val -= 10;
			return (byte)((ushort)val + 97);
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00003A74 File Offset: 0x00001C74
		private void WriteJSONString(byte[] b)
		{
			this.context.Write();
			this.trans.Write(TJSONProtocol.QUOTE);
			int num = b.Length;
			for (int i = 0; i < num; i++)
			{
				if ((b[i] & 255) >= 48)
				{
					if (b[i] == TJSONProtocol.BACKSLASH[0])
					{
						this.trans.Write(TJSONProtocol.BACKSLASH);
						this.trans.Write(TJSONProtocol.BACKSLASH);
					}
					else
					{
						this.trans.Write(b, i, 1);
					}
				}
				else
				{
					this.tempBuffer[0] = this.JSON_CHAR_TABLE[(int)b[i]];
					if (this.tempBuffer[0] == 1)
					{
						this.trans.Write(b, i, 1);
					}
					else if (this.tempBuffer[0] > 1)
					{
						this.trans.Write(TJSONProtocol.BACKSLASH);
						this.trans.Write(this.tempBuffer, 0, 1);
					}
					else
					{
						this.trans.Write(this.ESCSEQ);
						this.tempBuffer[0] = TJSONProtocol.HexChar((byte)(b[i] >> 4));
						this.tempBuffer[1] = TJSONProtocol.HexChar(b[i]);
						this.trans.Write(this.tempBuffer, 0, 2);
					}
				}
			}
			this.trans.Write(TJSONProtocol.QUOTE);
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00003BCC File Offset: 0x00001DCC
		private void WriteJSONInteger(long num)
		{
			this.context.Write();
			string text = num.ToString();
			bool flag = this.context.EscapeNumbers();
			if (flag)
			{
				this.trans.Write(TJSONProtocol.QUOTE);
			}
			this.trans.Write(this.utf8Encoding.GetBytes(text));
			if (flag)
			{
				this.trans.Write(TJSONProtocol.QUOTE);
			}
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00003C44 File Offset: 0x00001E44
		private void WriteJSONDouble(double num)
		{
			this.context.Write();
			string text = num.ToString("G17", CultureInfo.InvariantCulture);
			bool flag = false;
			char c = text[0];
			if (c != 'N' && c != 'I')
			{
				if (c == '-')
				{
					if (text[1] == 'I')
					{
						flag = true;
					}
				}
			}
			else
			{
				flag = true;
			}
			bool flag2 = flag || this.context.EscapeNumbers();
			if (flag2)
			{
				this.trans.Write(TJSONProtocol.QUOTE);
			}
			this.trans.Write(this.utf8Encoding.GetBytes(text));
			if (flag2)
			{
				this.trans.Write(TJSONProtocol.QUOTE);
			}
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00003D0C File Offset: 0x00001F0C
		private void WriteJSONBase64(byte[] b)
		{
			this.context.Write();
			this.trans.Write(TJSONProtocol.QUOTE);
			int i = b.Length;
			int num = 0;
			int num2 = ((i < 2) ? 0 : (i - 2));
			int num3 = i - 1;
			while (num3 >= num2 && b[num3] == 61)
			{
				i--;
				num3--;
			}
			while (i >= 3)
			{
				TBase64Utils.encode(b, num, 3, this.tempBuffer, 0);
				this.trans.Write(this.tempBuffer, 0, 4);
				num += 3;
				i -= 3;
			}
			if (i > 0)
			{
				TBase64Utils.encode(b, num, i, this.tempBuffer, 0);
				this.trans.Write(this.tempBuffer, 0, i + 1);
			}
			this.trans.Write(TJSONProtocol.QUOTE);
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00003DE0 File Offset: 0x00001FE0
		private void WriteJSONObjectStart()
		{
			this.context.Write();
			this.trans.Write(TJSONProtocol.LBRACE);
			this.PushContext(new TJSONProtocol.JSONPairContext(this));
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00003E0C File Offset: 0x0000200C
		private void WriteJSONObjectEnd()
		{
			this.PopContext();
			this.trans.Write(TJSONProtocol.RBRACE);
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00003E24 File Offset: 0x00002024
		private void WriteJSONArrayStart()
		{
			this.context.Write();
			this.trans.Write(TJSONProtocol.LBRACKET);
			this.PushContext(new TJSONProtocol.JSONListContext(this));
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00003E50 File Offset: 0x00002050
		private void WriteJSONArrayEnd()
		{
			this.PopContext();
			this.trans.Write(TJSONProtocol.RBRACKET);
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00003E68 File Offset: 0x00002068
		public override void WriteMessageBegin(TMessage message)
		{
			this.WriteJSONArrayStart();
			this.WriteJSONInteger(1L);
			byte[] bytes = this.utf8Encoding.GetBytes(message.Name);
			this.WriteJSONString(bytes);
			this.WriteJSONInteger((long)message.Type);
			this.WriteJSONInteger((long)message.SeqID);
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00003EBC File Offset: 0x000020BC
		public override void WriteMessageEnd()
		{
			this.WriteJSONArrayEnd();
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00003EC4 File Offset: 0x000020C4
		public override void WriteStructBegin(TStruct str)
		{
			this.WriteJSONObjectStart();
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00003ECC File Offset: 0x000020CC
		public override void WriteStructEnd()
		{
			this.WriteJSONObjectEnd();
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00003ED4 File Offset: 0x000020D4
		public override void WriteFieldBegin(TField field)
		{
			this.WriteJSONInteger((long)field.ID);
			this.WriteJSONObjectStart();
			this.WriteJSONString(TJSONProtocol.GetTypeNameForTypeID(field.Type));
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00003EFC File Offset: 0x000020FC
		public override void WriteFieldEnd()
		{
			this.WriteJSONObjectEnd();
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00003F04 File Offset: 0x00002104
		public override void WriteFieldStop()
		{
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00003F08 File Offset: 0x00002108
		public override void WriteMapBegin(TMap map)
		{
			this.WriteJSONArrayStart();
			this.WriteJSONString(TJSONProtocol.GetTypeNameForTypeID(map.KeyType));
			this.WriteJSONString(TJSONProtocol.GetTypeNameForTypeID(map.ValueType));
			this.WriteJSONInteger((long)map.Count);
			this.WriteJSONObjectStart();
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00003F48 File Offset: 0x00002148
		public override void WriteMapEnd()
		{
			this.WriteJSONObjectEnd();
			this.WriteJSONArrayEnd();
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00003F58 File Offset: 0x00002158
		public override void WriteListBegin(TList list)
		{
			this.WriteJSONArrayStart();
			this.WriteJSONString(TJSONProtocol.GetTypeNameForTypeID(list.ElementType));
			this.WriteJSONInteger((long)list.Count);
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00003F80 File Offset: 0x00002180
		public override void WriteListEnd()
		{
			this.WriteJSONArrayEnd();
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00003F88 File Offset: 0x00002188
		public override void WriteSetBegin(TSet set)
		{
			this.WriteJSONArrayStart();
			this.WriteJSONString(TJSONProtocol.GetTypeNameForTypeID(set.ElementType));
			this.WriteJSONInteger((long)set.Count);
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00003FB0 File Offset: 0x000021B0
		public override void WriteSetEnd()
		{
			this.WriteJSONArrayEnd();
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00003FB8 File Offset: 0x000021B8
		public override void WriteBool(bool b)
		{
			this.WriteJSONInteger((!b) ? 0L : 1L);
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00003FD0 File Offset: 0x000021D0
		public override void WriteByte(sbyte b)
		{
			this.WriteJSONInteger((long)b);
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00003FDC File Offset: 0x000021DC
		public override void WriteI16(short i16)
		{
			this.WriteJSONInteger((long)i16);
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00003FE8 File Offset: 0x000021E8
		public override void WriteI32(int i32)
		{
			this.WriteJSONInteger((long)i32);
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00003FF4 File Offset: 0x000021F4
		public override void WriteI64(long i64)
		{
			this.WriteJSONInteger(i64);
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00004000 File Offset: 0x00002200
		public override void WriteDouble(double dub)
		{
			this.WriteJSONDouble(dub);
		}

		// Token: 0x060000AF RID: 175 RVA: 0x0000400C File Offset: 0x0000220C
		public override void WriteString(string str)
		{
			byte[] bytes = this.utf8Encoding.GetBytes(str);
			this.WriteJSONString(bytes);
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00004030 File Offset: 0x00002230
		public override void WriteBinary(byte[] bin)
		{
			this.WriteJSONBase64(bin);
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x0000403C File Offset: 0x0000223C
		private byte[] ReadJSONString(bool skipContext)
		{
			MemoryStream memoryStream = new MemoryStream();
			List<char> list = new List<char>();
			if (!skipContext)
			{
				this.context.Read();
			}
			this.ReadJSONSyntaxChar(TJSONProtocol.QUOTE);
			for (;;)
			{
				byte b = this.reader.Read();
				if (b == TJSONProtocol.QUOTE[0])
				{
					break;
				}
				if (b != this.ESCSEQ[0])
				{
					memoryStream.Write(new byte[] { b }, 0, 1);
				}
				else
				{
					b = this.reader.Read();
					if (b != this.ESCSEQ[1])
					{
						int num = Array.IndexOf<char>(this.ESCAPE_CHARS, (char)b);
						if (num == -1)
						{
							goto Block_5;
						}
						b = this.ESCAPE_CHAR_VALS[num];
						memoryStream.Write(new byte[] { b }, 0, 1);
					}
					else
					{
						this.trans.ReadAll(this.tempBuffer, 0, 4);
						short num2 = (short)(((int)TJSONProtocol.HexVal(this.tempBuffer[0]) << 12) + ((int)TJSONProtocol.HexVal(this.tempBuffer[1]) << 8) + ((int)TJSONProtocol.HexVal(this.tempBuffer[2]) << 4) + (int)TJSONProtocol.HexVal(this.tempBuffer[3]));
						if (char.IsHighSurrogate((char)num2))
						{
							if (list.Count > 0)
							{
								goto Block_7;
							}
							list.Add((char)num2);
						}
						else if (char.IsLowSurrogate((char)num2))
						{
							if (list.Count == 0)
							{
								goto Block_9;
							}
							list.Add((char)num2);
							byte[] bytes = this.utf8Encoding.GetBytes(list.ToArray());
							memoryStream.Write(bytes, 0, bytes.Length);
							list.Clear();
						}
						else
						{
							byte[] bytes2 = this.utf8Encoding.GetBytes(new char[] { (char)num2 });
							memoryStream.Write(bytes2, 0, bytes2.Length);
						}
					}
				}
			}
			if (list.Count > 0)
			{
				throw new TProtocolException(1, "Expected low surrogate char");
			}
			return memoryStream.ToArray();
			Block_5:
			throw new TProtocolException(1, "Expected control char");
			Block_7:
			throw new TProtocolException(1, "Expected low surrogate char");
			Block_9:
			throw new TProtocolException(1, "Expected high surrogate char");
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00004238 File Offset: 0x00002438
		private bool IsJSONNumeric(byte b)
		{
			switch (b)
			{
			case 43:
			case 45:
			case 46:
			case 48:
			case 49:
			case 50:
			case 51:
			case 52:
			case 53:
			case 54:
			case 55:
			case 56:
			case 57:
			case 69:
				break;
			default:
				if (b != 101)
				{
					return false;
				}
				break;
			}
			return true;
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x000042CC File Offset: 0x000024CC
		private string ReadJSONNumericChars()
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (;;)
			{
				byte b = this.reader.Peek();
				if (!this.IsJSONNumeric(b))
				{
					break;
				}
				stringBuilder.Append((char)this.reader.Read());
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x0000431C File Offset: 0x0000251C
		private long ReadJSONInteger()
		{
			this.context.Read();
			if (this.context.EscapeNumbers())
			{
				this.ReadJSONSyntaxChar(TJSONProtocol.QUOTE);
			}
			string text = this.ReadJSONNumericChars();
			if (this.context.EscapeNumbers())
			{
				this.ReadJSONSyntaxChar(TJSONProtocol.QUOTE);
			}
			long num;
			try
			{
				num = long.Parse(text);
			}
			catch (FormatException)
			{
				throw new TProtocolException(1, "Bad data encounted in numeric data");
			}
			return num;
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x0000439C File Offset: 0x0000259C
		private double ReadJSONDouble()
		{
			this.context.Read();
			if (this.reader.Peek() != TJSONProtocol.QUOTE[0])
			{
				if (this.context.EscapeNumbers())
				{
					this.ReadJSONSyntaxChar(TJSONProtocol.QUOTE);
				}
				double num;
				try
				{
					num = double.Parse(this.ReadJSONNumericChars(), CultureInfo.InvariantCulture);
				}
				catch (FormatException)
				{
					throw new TProtocolException(1, "Bad data encounted in numeric data");
				}
				return num;
			}
			byte[] array = this.ReadJSONString(true);
			double num2 = double.Parse(this.utf8Encoding.GetString(array, 0, array.Length), CultureInfo.InvariantCulture);
			if (!this.context.EscapeNumbers() && !double.IsNaN(num2) && !double.IsInfinity(num2))
			{
				throw new TProtocolException(1, "Numeric data unexpectedly quoted");
			}
			return num2;
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00004474 File Offset: 0x00002674
		private byte[] ReadJSONBase64()
		{
			byte[] array = this.ReadJSONString(false);
			int i = array.Length;
			int num = 0;
			int num2 = 0;
			while (i > 0 && array[i - 1] == 61)
			{
				i--;
			}
			while (i > 4)
			{
				TBase64Utils.decode(array, num, 4, array, num2);
				num += 4;
				i -= 4;
				num2 += 3;
			}
			if (i > 1)
			{
				TBase64Utils.decode(array, num, i, array, num2);
				num2 += i - 1;
			}
			byte[] array2 = new byte[num2];
			Array.Copy(array, 0, array2, 0, num2);
			return array2;
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x000044FC File Offset: 0x000026FC
		private void ReadJSONObjectStart()
		{
			this.context.Read();
			this.ReadJSONSyntaxChar(TJSONProtocol.LBRACE);
			this.PushContext(new TJSONProtocol.JSONPairContext(this));
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00004520 File Offset: 0x00002720
		private void ReadJSONObjectEnd()
		{
			this.ReadJSONSyntaxChar(TJSONProtocol.RBRACE);
			this.PopContext();
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00004534 File Offset: 0x00002734
		private void ReadJSONArrayStart()
		{
			this.context.Read();
			this.ReadJSONSyntaxChar(TJSONProtocol.LBRACKET);
			this.PushContext(new TJSONProtocol.JSONListContext(this));
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00004558 File Offset: 0x00002758
		private void ReadJSONArrayEnd()
		{
			this.ReadJSONSyntaxChar(TJSONProtocol.RBRACKET);
			this.PopContext();
		}

		// Token: 0x060000BB RID: 187 RVA: 0x0000456C File Offset: 0x0000276C
		public override TMessage ReadMessageBegin()
		{
			TMessage tmessage = default(TMessage);
			this.ReadJSONArrayStart();
			if (this.ReadJSONInteger() != 1L)
			{
				throw new TProtocolException(4, "Message contained bad version.");
			}
			byte[] array = this.ReadJSONString(false);
			tmessage.Name = this.utf8Encoding.GetString(array, 0, array.Length);
			tmessage.Type = (TMessageType)this.ReadJSONInteger();
			tmessage.SeqID = (int)this.ReadJSONInteger();
			return tmessage;
		}

		// Token: 0x060000BC RID: 188 RVA: 0x000045DC File Offset: 0x000027DC
		public override void ReadMessageEnd()
		{
			this.ReadJSONArrayEnd();
		}

		// Token: 0x060000BD RID: 189 RVA: 0x000045E4 File Offset: 0x000027E4
		public override TStruct ReadStructBegin()
		{
			this.ReadJSONObjectStart();
			return default(TStruct);
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00004600 File Offset: 0x00002800
		public override void ReadStructEnd()
		{
			this.ReadJSONObjectEnd();
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00004608 File Offset: 0x00002808
		public override TField ReadFieldBegin()
		{
			TField tfield = default(TField);
			byte b = this.reader.Peek();
			if (b == TJSONProtocol.RBRACE[0])
			{
				tfield.Type = TType.Stop;
			}
			else
			{
				tfield.ID = (short)this.ReadJSONInteger();
				this.ReadJSONObjectStart();
				tfield.Type = TJSONProtocol.GetTypeIDForTypeName(this.ReadJSONString(false));
			}
			return tfield;
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x0000466C File Offset: 0x0000286C
		public override void ReadFieldEnd()
		{
			this.ReadJSONObjectEnd();
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00004674 File Offset: 0x00002874
		public override TMap ReadMapBegin()
		{
			TMap tmap = default(TMap);
			this.ReadJSONArrayStart();
			tmap.KeyType = TJSONProtocol.GetTypeIDForTypeName(this.ReadJSONString(false));
			tmap.ValueType = TJSONProtocol.GetTypeIDForTypeName(this.ReadJSONString(false));
			tmap.Count = (int)this.ReadJSONInteger();
			this.ReadJSONObjectStart();
			return tmap;
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x000046CC File Offset: 0x000028CC
		public override void ReadMapEnd()
		{
			this.ReadJSONObjectEnd();
			this.ReadJSONArrayEnd();
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x000046DC File Offset: 0x000028DC
		public override TList ReadListBegin()
		{
			TList tlist = default(TList);
			this.ReadJSONArrayStart();
			tlist.ElementType = TJSONProtocol.GetTypeIDForTypeName(this.ReadJSONString(false));
			tlist.Count = (int)this.ReadJSONInteger();
			return tlist;
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x0000471C File Offset: 0x0000291C
		public override void ReadListEnd()
		{
			this.ReadJSONArrayEnd();
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00004724 File Offset: 0x00002924
		public override TSet ReadSetBegin()
		{
			TSet tset = default(TSet);
			this.ReadJSONArrayStart();
			tset.ElementType = TJSONProtocol.GetTypeIDForTypeName(this.ReadJSONString(false));
			tset.Count = (int)this.ReadJSONInteger();
			return tset;
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00004764 File Offset: 0x00002964
		public override void ReadSetEnd()
		{
			this.ReadJSONArrayEnd();
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x0000476C File Offset: 0x0000296C
		public override bool ReadBool()
		{
			return this.ReadJSONInteger() != 0L;
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00004784 File Offset: 0x00002984
		public override sbyte ReadByte()
		{
			return (sbyte)this.ReadJSONInteger();
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00004790 File Offset: 0x00002990
		public override short ReadI16()
		{
			return (short)this.ReadJSONInteger();
		}

		// Token: 0x060000CA RID: 202 RVA: 0x0000479C File Offset: 0x0000299C
		public override int ReadI32()
		{
			return (int)this.ReadJSONInteger();
		}

		// Token: 0x060000CB RID: 203 RVA: 0x000047A8 File Offset: 0x000029A8
		public override long ReadI64()
		{
			return this.ReadJSONInteger();
		}

		// Token: 0x060000CC RID: 204 RVA: 0x000047B0 File Offset: 0x000029B0
		public override double ReadDouble()
		{
			return this.ReadJSONDouble();
		}

		// Token: 0x060000CD RID: 205 RVA: 0x000047B8 File Offset: 0x000029B8
		public override string ReadString()
		{
			byte[] array = this.ReadJSONString(false);
			return this.utf8Encoding.GetString(array, 0, array.Length);
		}

		// Token: 0x060000CE RID: 206 RVA: 0x000047E0 File Offset: 0x000029E0
		public override byte[] ReadBinary()
		{
			return this.ReadJSONBase64();
		}

		// Token: 0x04000033 RID: 51
		private static byte[] COMMA = new byte[] { 44 };

		// Token: 0x04000034 RID: 52
		private static byte[] COLON = new byte[] { 58 };

		// Token: 0x04000035 RID: 53
		private static byte[] LBRACE = new byte[] { 123 };

		// Token: 0x04000036 RID: 54
		private static byte[] RBRACE = new byte[] { 125 };

		// Token: 0x04000037 RID: 55
		private static byte[] LBRACKET = new byte[] { 91 };

		// Token: 0x04000038 RID: 56
		private static byte[] RBRACKET = new byte[] { 93 };

		// Token: 0x04000039 RID: 57
		private static byte[] QUOTE = new byte[] { 34 };

		// Token: 0x0400003A RID: 58
		private static byte[] BACKSLASH = new byte[] { 92 };

		// Token: 0x0400003B RID: 59
		private byte[] ESCSEQ = new byte[] { 92, 117, 48, 48 };

		// Token: 0x0400003C RID: 60
		private const long VERSION = 1L;

		// Token: 0x0400003D RID: 61
		private byte[] JSON_CHAR_TABLE = new byte[]
		{
			0, 0, 0, 0, 0, 0, 0, 0, 98, 116,
			110, 0, 102, 114, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 1, 1, 34, 1, 1, 1, 1, 1,
			1, 1, 1, 1, 1, 1, 1, 1
		};

		// Token: 0x0400003E RID: 62
		private char[] ESCAPE_CHARS = "\"\\/bfnrt".ToCharArray();

		// Token: 0x0400003F RID: 63
		private byte[] ESCAPE_CHAR_VALS = new byte[] { 34, 92, 47, 8, 12, 10, 13, 9 };

		// Token: 0x04000040 RID: 64
		private const int DEF_STRING_SIZE = 16;

		// Token: 0x04000041 RID: 65
		private static byte[] NAME_BOOL = new byte[] { 116, 102 };

		// Token: 0x04000042 RID: 66
		private static byte[] NAME_BYTE = new byte[] { 105, 56 };

		// Token: 0x04000043 RID: 67
		private static byte[] NAME_I16 = new byte[] { 105, 49, 54 };

		// Token: 0x04000044 RID: 68
		private static byte[] NAME_I32 = new byte[] { 105, 51, 50 };

		// Token: 0x04000045 RID: 69
		private static byte[] NAME_I64 = new byte[] { 105, 54, 52 };

		// Token: 0x04000046 RID: 70
		private static byte[] NAME_DOUBLE = new byte[] { 100, 98, 108 };

		// Token: 0x04000047 RID: 71
		private static byte[] NAME_STRUCT = new byte[] { 114, 101, 99 };

		// Token: 0x04000048 RID: 72
		private static byte[] NAME_STRING = new byte[] { 115, 116, 114 };

		// Token: 0x04000049 RID: 73
		private static byte[] NAME_MAP = new byte[] { 109, 97, 112 };

		// Token: 0x0400004A RID: 74
		private static byte[] NAME_LIST = new byte[] { 108, 115, 116 };

		// Token: 0x0400004B RID: 75
		private static byte[] NAME_SET = new byte[] { 115, 101, 116 };

		// Token: 0x0400004C RID: 76
		protected Encoding utf8Encoding = Encoding.UTF8;

		// Token: 0x0400004D RID: 77
		protected Stack<TJSONProtocol.JSONBaseContext> contextStack = new Stack<TJSONProtocol.JSONBaseContext>();

		// Token: 0x0400004E RID: 78
		protected TJSONProtocol.JSONBaseContext context;

		// Token: 0x0400004F RID: 79
		protected TJSONProtocol.LookaheadReader reader;

		// Token: 0x04000050 RID: 80
		private byte[] tempBuffer = new byte[4];

		// Token: 0x02000010 RID: 16
		public class Factory : TProtocolFactory
		{
			// Token: 0x060000D1 RID: 209 RVA: 0x00004970 File Offset: 0x00002B70
			public TProtocol GetProtocol(TTransport trans)
			{
				return new TJSONProtocol(trans);
			}
		}

		// Token: 0x02000011 RID: 17
		protected class JSONBaseContext
		{
			// Token: 0x060000D2 RID: 210 RVA: 0x00004978 File Offset: 0x00002B78
			public JSONBaseContext(TJSONProtocol proto)
			{
				this.proto = proto;
			}

			// Token: 0x060000D3 RID: 211 RVA: 0x00004988 File Offset: 0x00002B88
			public virtual void Write()
			{
			}

			// Token: 0x060000D4 RID: 212 RVA: 0x0000498C File Offset: 0x00002B8C
			public virtual void Read()
			{
			}

			// Token: 0x060000D5 RID: 213 RVA: 0x00004990 File Offset: 0x00002B90
			public virtual bool EscapeNumbers()
			{
				return false;
			}

			// Token: 0x04000051 RID: 81
			protected TJSONProtocol proto;
		}

		// Token: 0x02000012 RID: 18
		protected class JSONListContext : TJSONProtocol.JSONBaseContext
		{
			// Token: 0x060000D6 RID: 214 RVA: 0x00004994 File Offset: 0x00002B94
			public JSONListContext(TJSONProtocol protocol)
				: base(protocol)
			{
			}

			// Token: 0x060000D7 RID: 215 RVA: 0x000049A4 File Offset: 0x00002BA4
			public override void Write()
			{
				if (this.first)
				{
					this.first = false;
				}
				else
				{
					this.proto.trans.Write(TJSONProtocol.COMMA);
				}
			}

			// Token: 0x060000D8 RID: 216 RVA: 0x000049D4 File Offset: 0x00002BD4
			public override void Read()
			{
				if (this.first)
				{
					this.first = false;
				}
				else
				{
					this.proto.ReadJSONSyntaxChar(TJSONProtocol.COMMA);
				}
			}

			// Token: 0x04000052 RID: 82
			private bool first = true;
		}

		// Token: 0x02000013 RID: 19
		protected class JSONPairContext : TJSONProtocol.JSONBaseContext
		{
			// Token: 0x060000D9 RID: 217 RVA: 0x00004A00 File Offset: 0x00002C00
			public JSONPairContext(TJSONProtocol proto)
				: base(proto)
			{
			}

			// Token: 0x060000DA RID: 218 RVA: 0x00004A18 File Offset: 0x00002C18
			public override void Write()
			{
				if (this.first)
				{
					this.first = false;
					this.colon = true;
				}
				else
				{
					this.proto.trans.Write((!this.colon) ? TJSONProtocol.COMMA : TJSONProtocol.COLON);
					this.colon = !this.colon;
				}
			}

			// Token: 0x060000DB RID: 219 RVA: 0x00004A7C File Offset: 0x00002C7C
			public override void Read()
			{
				if (this.first)
				{
					this.first = false;
					this.colon = true;
				}
				else
				{
					this.proto.ReadJSONSyntaxChar((!this.colon) ? TJSONProtocol.COMMA : TJSONProtocol.COLON);
					this.colon = !this.colon;
				}
			}

			// Token: 0x060000DC RID: 220 RVA: 0x00004ADC File Offset: 0x00002CDC
			public override bool EscapeNumbers()
			{
				return this.colon;
			}

			// Token: 0x04000053 RID: 83
			private bool first = true;

			// Token: 0x04000054 RID: 84
			private bool colon = true;
		}

		// Token: 0x02000014 RID: 20
		protected class LookaheadReader
		{
			// Token: 0x060000DD RID: 221 RVA: 0x00004AE4 File Offset: 0x00002CE4
			public LookaheadReader(TJSONProtocol proto)
			{
				this.proto = proto;
			}

			// Token: 0x060000DE RID: 222 RVA: 0x00004B00 File Offset: 0x00002D00
			public byte Read()
			{
				if (this.hasData)
				{
					this.hasData = false;
				}
				else
				{
					this.proto.trans.ReadAll(this.data, 0, 1);
				}
				return this.data[0];
			}

			// Token: 0x060000DF RID: 223 RVA: 0x00004B3C File Offset: 0x00002D3C
			public byte Peek()
			{
				if (!this.hasData)
				{
					this.proto.trans.ReadAll(this.data, 0, 1);
				}
				this.hasData = true;
				return this.data[0];
			}

			// Token: 0x04000055 RID: 85
			protected TJSONProtocol proto;

			// Token: 0x04000056 RID: 86
			private bool hasData;

			// Token: 0x04000057 RID: 87
			private byte[] data = new byte[1];
		}
	}
}
