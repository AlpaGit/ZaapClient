using System;
using System.Text;
using Thrift.Protocol;

namespace com.ankama.zaap
{
	// Token: 0x02000056 RID: 86
	[Serializable]
	public class NotificationResult : TBase, TAbstractBase
	{
		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000299 RID: 665 RVA: 0x0000AD6C File Offset: 0x00008F6C
		// (set) Token: 0x0600029A RID: 666 RVA: 0x0000AD74 File Offset: 0x00008F74
		public string Id
		{
			get
			{
				return this._id;
			}
			set
			{
				this.__isset.id = true;
				this._id = value;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x0600029B RID: 667 RVA: 0x0000AD8C File Offset: 0x00008F8C
		// (set) Token: 0x0600029C RID: 668 RVA: 0x0000AD94 File Offset: 0x00008F94
		public NotificationResultType Type
		{
			get
			{
				return this._type;
			}
			set
			{
				this.__isset.type = true;
				this._type = value;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x0600029D RID: 669 RVA: 0x0000ADAC File Offset: 0x00008FAC
		// (set) Token: 0x0600029E RID: 670 RVA: 0x0000ADB4 File Offset: 0x00008FB4
		public string Reply
		{
			get
			{
				return this._reply;
			}
			set
			{
				this.__isset.reply = true;
				this._reply = value;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x0600029F RID: 671 RVA: 0x0000ADCC File Offset: 0x00008FCC
		// (set) Token: 0x060002A0 RID: 672 RVA: 0x0000ADD4 File Offset: 0x00008FD4
		public string ActionIndex
		{
			get
			{
				return this._actionIndex;
			}
			set
			{
				this.__isset.actionIndex = true;
				this._actionIndex = value;
			}
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x0000ADEC File Offset: 0x00008FEC
		public void Read(TProtocol iprot)
		{
			iprot.IncrementRecursionDepth();
			try
			{
				iprot.ReadStructBegin();
				for (;;)
				{
					TField tfield = iprot.ReadFieldBegin();
					if (tfield.Type == TType.Stop)
					{
						break;
					}
					switch (tfield.ID)
					{
					case 1:
						if (tfield.Type == TType.String)
						{
							this.Id = iprot.ReadString();
						}
						else
						{
							TProtocolUtil.Skip(iprot, tfield.Type);
						}
						break;
					case 2:
						if (tfield.Type == TType.I32)
						{
							this.Type = (NotificationResultType)iprot.ReadI32();
						}
						else
						{
							TProtocolUtil.Skip(iprot, tfield.Type);
						}
						break;
					case 3:
						if (tfield.Type == TType.String)
						{
							this.Reply = iprot.ReadString();
						}
						else
						{
							TProtocolUtil.Skip(iprot, tfield.Type);
						}
						break;
					case 4:
						if (tfield.Type == TType.String)
						{
							this.ActionIndex = iprot.ReadString();
						}
						else
						{
							TProtocolUtil.Skip(iprot, tfield.Type);
						}
						break;
					default:
						TProtocolUtil.Skip(iprot, tfield.Type);
						break;
					}
					iprot.ReadFieldEnd();
				}
				iprot.ReadStructEnd();
			}
			finally
			{
				iprot.DecrementRecursionDepth();
			}
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x0000AF54 File Offset: 0x00009154
		public void Write(TProtocol oprot)
		{
			oprot.IncrementRecursionDepth();
			try
			{
				TStruct tstruct = new TStruct("NotificationResult");
				oprot.WriteStructBegin(tstruct);
				TField tfield = default(TField);
				if (this.Id != null && this.__isset.id)
				{
					tfield.Name = "id";
					tfield.Type = TType.String;
					tfield.ID = 1;
					oprot.WriteFieldBegin(tfield);
					oprot.WriteString(this.Id);
					oprot.WriteFieldEnd();
				}
				if (this.__isset.type)
				{
					tfield.Name = "type";
					tfield.Type = TType.I32;
					tfield.ID = 2;
					oprot.WriteFieldBegin(tfield);
					oprot.WriteI32((int)this.Type);
					oprot.WriteFieldEnd();
				}
				if (this.Reply != null && this.__isset.reply)
				{
					tfield.Name = "reply";
					tfield.Type = TType.String;
					tfield.ID = 3;
					oprot.WriteFieldBegin(tfield);
					oprot.WriteString(this.Reply);
					oprot.WriteFieldEnd();
				}
				if (this.ActionIndex != null && this.__isset.actionIndex)
				{
					tfield.Name = "actionIndex";
					tfield.Type = TType.String;
					tfield.ID = 4;
					oprot.WriteFieldBegin(tfield);
					oprot.WriteString(this.ActionIndex);
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

		// Token: 0x060002A3 RID: 675 RVA: 0x0000B0F0 File Offset: 0x000092F0
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder("NotificationResult(");
			bool flag = true;
			if (this.Id != null && this.__isset.id)
			{
				if (!flag)
				{
					stringBuilder.Append(", ");
				}
				flag = false;
				stringBuilder.Append("Id: ");
				stringBuilder.Append(this.Id);
			}
			if (this.__isset.type)
			{
				if (!flag)
				{
					stringBuilder.Append(", ");
				}
				flag = false;
				stringBuilder.Append("Type: ");
				stringBuilder.Append(this.Type);
			}
			if (this.Reply != null && this.__isset.reply)
			{
				if (!flag)
				{
					stringBuilder.Append(", ");
				}
				flag = false;
				stringBuilder.Append("Reply: ");
				stringBuilder.Append(this.Reply);
			}
			if (this.ActionIndex != null && this.__isset.actionIndex)
			{
				if (!flag)
				{
					stringBuilder.Append(", ");
				}
				stringBuilder.Append("ActionIndex: ");
				stringBuilder.Append(this.ActionIndex);
			}
			stringBuilder.Append(")");
			return stringBuilder.ToString();
		}

		// Token: 0x0400018D RID: 397
		private string _id;

		// Token: 0x0400018E RID: 398
		private NotificationResultType _type;

		// Token: 0x0400018F RID: 399
		private string _reply;

		// Token: 0x04000190 RID: 400
		private string _actionIndex;

		// Token: 0x04000191 RID: 401
		public NotificationResult.Isset __isset;

		// Token: 0x02000057 RID: 87
		[Serializable]
		public struct Isset
		{
			// Token: 0x04000192 RID: 402
			public bool id;

			// Token: 0x04000193 RID: 403
			public bool type;

			// Token: 0x04000194 RID: 404
			public bool reply;

			// Token: 0x04000195 RID: 405
			public bool actionIndex;
		}
	}
}
