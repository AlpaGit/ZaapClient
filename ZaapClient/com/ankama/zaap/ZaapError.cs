using System;
using System.Text;
using Thrift;
using Thrift.Protocol;

namespace com.ankama.zaap
{
	// Token: 0x0200005B RID: 91
	[Serializable]
	public class ZaapError : TException, TBase, TAbstractBase
	{
		// Token: 0x060002B4 RID: 692 RVA: 0x0000B984 File Offset: 0x00009B84
		public ZaapError()
		{
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x0000B98C File Offset: 0x00009B8C
		public ZaapError(ErrorCode code)
			: this()
		{
			this.Code = code;
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060002B6 RID: 694 RVA: 0x0000B99C File Offset: 0x00009B9C
		// (set) Token: 0x060002B7 RID: 695 RVA: 0x0000B9A4 File Offset: 0x00009BA4
		public ErrorCode Code { get; set; }

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060002B8 RID: 696 RVA: 0x0000B9B0 File Offset: 0x00009BB0
		// (set) Token: 0x060002B9 RID: 697 RVA: 0x0000B9B8 File Offset: 0x00009BB8
		public string Details
		{
			get
			{
				return this._details;
			}
			set
			{
				this.__isset.details = true;
				this._details = value;
			}
		}

		// Token: 0x060002BA RID: 698 RVA: 0x0000B9D0 File Offset: 0x00009BD0
		public void Read(TProtocol iprot)
		{
			iprot.IncrementRecursionDepth();
			try
			{
				bool flag = false;
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
						else if (tfield.Type == TType.String)
						{
							this.Details = iprot.ReadString();
						}
						else
						{
							TProtocolUtil.Skip(iprot, tfield.Type);
						}
					}
					else if (tfield.Type == TType.I32)
					{
						this.Code = (ErrorCode)iprot.ReadI32();
						flag = true;
					}
					else
					{
						TProtocolUtil.Skip(iprot, tfield.Type);
					}
					iprot.ReadFieldEnd();
				}
				iprot.ReadStructEnd();
				if (!flag)
				{
					throw new TProtocolException(1, "required field Code not set");
				}
			}
			finally
			{
				iprot.DecrementRecursionDepth();
			}
		}

		// Token: 0x060002BB RID: 699 RVA: 0x0000BAD4 File Offset: 0x00009CD4
		public void Write(TProtocol oprot)
		{
			oprot.IncrementRecursionDepth();
			try
			{
				TStruct tstruct = new TStruct("ZaapError");
				oprot.WriteStructBegin(tstruct);
				TField tfield = new TField
				{
					Name = "code",
					Type = TType.I32,
					ID = 1
				};
				oprot.WriteFieldBegin(tfield);
				oprot.WriteI32((int)this.Code);
				oprot.WriteFieldEnd();
				if (this.Details != null && this.__isset.details)
				{
					tfield.Name = "details";
					tfield.Type = TType.String;
					tfield.ID = 2;
					oprot.WriteFieldBegin(tfield);
					oprot.WriteString(this.Details);
					oprot.WriteFieldEnd();
				}
				oprot.WriteFieldStop();
				oprot.WriteStructEnd();
			}
			finally
			{
				oprot.DecrementRecursionDepth();
			}
		}

		// Token: 0x060002BC RID: 700 RVA: 0x0000BBB0 File Offset: 0x00009DB0
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder("ZaapError(");
			stringBuilder.Append(", Code: ");
			stringBuilder.Append(this.Code);
			if (this.Details != null && this.__isset.details)
			{
				stringBuilder.Append(", Details: ");
				stringBuilder.Append(this.Details);
			}
			stringBuilder.Append(")");
			return stringBuilder.ToString();
		}

		// Token: 0x040001A9 RID: 425
		private string _details;

		// Token: 0x040001AB RID: 427
		public ZaapError.Isset __isset;

		// Token: 0x0200005C RID: 92
		[Serializable]
		public struct Isset
		{
			// Token: 0x040001AC RID: 428
			public bool details;
		}
	}
}
