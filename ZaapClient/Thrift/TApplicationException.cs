using System;
using Thrift.Protocol;

namespace Thrift
{
	// Token: 0x0200002D RID: 45
	public class TApplicationException : TException
	{
		// Token: 0x0600019C RID: 412 RVA: 0x00006538 File Offset: 0x00004738
		public TApplicationException()
		{
		}

		// Token: 0x0600019D RID: 413 RVA: 0x00006540 File Offset: 0x00004740
		public TApplicationException(TApplicationException.ExceptionType type)
		{
			this.type = type;
		}

		// Token: 0x0600019E RID: 414 RVA: 0x00006550 File Offset: 0x00004750
		public TApplicationException(TApplicationException.ExceptionType type, string message)
			: base(message)
		{
			this.type = type;
		}

		// Token: 0x0600019F RID: 415 RVA: 0x00006560 File Offset: 0x00004760
		public static TApplicationException Read(TProtocol iprot)
		{
			string text = null;
			TApplicationException.ExceptionType exceptionType = TApplicationException.ExceptionType.Unknown;
			iprot.ReadStructBegin();
			for (;;)
			{
				TField tfield = iprot.ReadFieldBegin();
				if (tfield.Type == TType.Stop)
				{
					break;
				}
				short id = tfield.ID;
				if (id != 1)
				{
					if (id != 2)
					{
						TProtocolUtil.Skip(iprot, tfield.Type);
					}
					else if (tfield.Type == TType.I32)
					{
						exceptionType = (TApplicationException.ExceptionType)iprot.ReadI32();
					}
					else
					{
						TProtocolUtil.Skip(iprot, tfield.Type);
					}
				}
				else if (tfield.Type == TType.String)
				{
					text = iprot.ReadString();
				}
				else
				{
					TProtocolUtil.Skip(iprot, tfield.Type);
				}
				iprot.ReadFieldEnd();
			}
			iprot.ReadStructEnd();
			return new TApplicationException(exceptionType, text);
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x0000662C File Offset: 0x0000482C
		public void Write(TProtocol oprot)
		{
			TStruct tstruct = new TStruct("TApplicationException");
			TField tfield = default(TField);
			oprot.WriteStructBegin(tstruct);
			if (!string.IsNullOrEmpty(this.Message))
			{
				tfield.Name = "message";
				tfield.Type = TType.String;
				tfield.ID = 1;
				oprot.WriteFieldBegin(tfield);
				oprot.WriteString(this.Message);
				oprot.WriteFieldEnd();
			}
			tfield.Name = "type";
			tfield.Type = TType.I32;
			tfield.ID = 2;
			oprot.WriteFieldBegin(tfield);
			oprot.WriteI32((int)this.type);
			oprot.WriteFieldEnd();
			oprot.WriteFieldStop();
			oprot.WriteStructEnd();
		}

		// Token: 0x040000AD RID: 173
		protected TApplicationException.ExceptionType type;

		// Token: 0x0200002E RID: 46
		public enum ExceptionType
		{
			// Token: 0x040000AF RID: 175
			Unknown,
			// Token: 0x040000B0 RID: 176
			UnknownMethod,
			// Token: 0x040000B1 RID: 177
			InvalidMessageType,
			// Token: 0x040000B2 RID: 178
			WrongMethodName,
			// Token: 0x040000B3 RID: 179
			BadSequenceID,
			// Token: 0x040000B4 RID: 180
			MissingResult,
			// Token: 0x040000B5 RID: 181
			InternalError,
			// Token: 0x040000B6 RID: 182
			ProtocolError,
			// Token: 0x040000B7 RID: 183
			InvalidTransform,
			// Token: 0x040000B8 RID: 184
			InvalidProtocol,
			// Token: 0x040000B9 RID: 185
			UnsupportedClientType
		}
	}
}
