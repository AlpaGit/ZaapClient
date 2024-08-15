using System;
using System.Collections.Generic;
using System.Text;
using Thrift.Protocol;

namespace com.ankama.zaap
{
	// Token: 0x02000058 RID: 88
	[Serializable]
	public class NotificationParams : TBase, TAbstractBase
	{
		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060002A5 RID: 677 RVA: 0x0000B240 File Offset: 0x00009440
		// (set) Token: 0x060002A6 RID: 678 RVA: 0x0000B248 File Offset: 0x00009448
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

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060002A7 RID: 679 RVA: 0x0000B260 File Offset: 0x00009460
		// (set) Token: 0x060002A8 RID: 680 RVA: 0x0000B268 File Offset: 0x00009468
		public string Body
		{
			get
			{
				return this._body;
			}
			set
			{
				this.__isset.body = true;
				this._body = value;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060002A9 RID: 681 RVA: 0x0000B280 File Offset: 0x00009480
		// (set) Token: 0x060002AA RID: 682 RVA: 0x0000B288 File Offset: 0x00009488
		public List<string> Image
		{
			get
			{
				return this._image;
			}
			set
			{
				this.__isset.image = true;
				this._image = value;
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060002AB RID: 683 RVA: 0x0000B2A0 File Offset: 0x000094A0
		// (set) Token: 0x060002AC RID: 684 RVA: 0x0000B2A8 File Offset: 0x000094A8
		public string SubTitle
		{
			get
			{
				return this._subTitle;
			}
			set
			{
				this.__isset.subTitle = true;
				this._subTitle = value;
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060002AD RID: 685 RVA: 0x0000B2C0 File Offset: 0x000094C0
		// (set) Token: 0x060002AE RID: 686 RVA: 0x0000B2C8 File Offset: 0x000094C8
		public string Background
		{
			get
			{
				return this._background;
			}
			set
			{
				this.__isset.background = true;
				this._background = value;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060002AF RID: 687 RVA: 0x0000B2E0 File Offset: 0x000094E0
		// (set) Token: 0x060002B0 RID: 688 RVA: 0x0000B2E8 File Offset: 0x000094E8
		public int Timeout
		{
			get
			{
				return this._timeout;
			}
			set
			{
				this.__isset.timeout = true;
				this._timeout = value;
			}
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x0000B300 File Offset: 0x00009500
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
						if (tfield.Type == TType.String)
						{
							this.Body = iprot.ReadString();
						}
						else
						{
							TProtocolUtil.Skip(iprot, tfield.Type);
						}
						break;
					case 3:
						if (tfield.Type == TType.List)
						{
							this.Image = new List<string>();
							TList tlist = iprot.ReadListBegin();
							for (int i = 0; i < tlist.Count; i++)
							{
								string text = iprot.ReadString();
								this.Image.Add(text);
							}
							iprot.ReadListEnd();
						}
						else
						{
							TProtocolUtil.Skip(iprot, tfield.Type);
						}
						break;
					case 4:
						if (tfield.Type == TType.String)
						{
							this.SubTitle = iprot.ReadString();
						}
						else
						{
							TProtocolUtil.Skip(iprot, tfield.Type);
						}
						break;
					case 5:
						if (tfield.Type == TType.String)
						{
							this.Background = iprot.ReadString();
						}
						else
						{
							TProtocolUtil.Skip(iprot, tfield.Type);
						}
						break;
					case 6:
						if (tfield.Type == TType.I32)
						{
							this.Timeout = iprot.ReadI32();
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

		// Token: 0x060002B2 RID: 690 RVA: 0x0000B508 File Offset: 0x00009708
		public void Write(TProtocol oprot)
		{
			oprot.IncrementRecursionDepth();
			try
			{
				TStruct tstruct = new TStruct("NotificationParams");
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
				if (this.Body != null && this.__isset.body)
				{
					tfield.Name = "body";
					tfield.Type = TType.String;
					tfield.ID = 2;
					oprot.WriteFieldBegin(tfield);
					oprot.WriteString(this.Body);
					oprot.WriteFieldEnd();
				}
				if (this.Image != null && this.__isset.image)
				{
					tfield.Name = "image";
					tfield.Type = TType.List;
					tfield.ID = 3;
					oprot.WriteFieldBegin(tfield);
					oprot.WriteListBegin(new TList(TType.String, this.Image.Count));
					foreach (string text in this.Image)
					{
						oprot.WriteString(text);
					}
					oprot.WriteListEnd();
					oprot.WriteFieldEnd();
				}
				if (this.SubTitle != null && this.__isset.subTitle)
				{
					tfield.Name = "subTitle";
					tfield.Type = TType.String;
					tfield.ID = 4;
					oprot.WriteFieldBegin(tfield);
					oprot.WriteString(this.SubTitle);
					oprot.WriteFieldEnd();
				}
				if (this.Background != null && this.__isset.background)
				{
					tfield.Name = "background";
					tfield.Type = TType.String;
					tfield.ID = 5;
					oprot.WriteFieldBegin(tfield);
					oprot.WriteString(this.Background);
					oprot.WriteFieldEnd();
				}
				if (this.__isset.timeout)
				{
					tfield.Name = "timeout";
					tfield.Type = TType.I32;
					tfield.ID = 6;
					oprot.WriteFieldBegin(tfield);
					oprot.WriteI32(this.Timeout);
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

		// Token: 0x060002B3 RID: 691 RVA: 0x0000B7B0 File Offset: 0x000099B0
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder("NotificationParams(");
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
			if (this.Body != null && this.__isset.body)
			{
				if (!flag)
				{
					stringBuilder.Append(", ");
				}
				flag = false;
				stringBuilder.Append("Body: ");
				stringBuilder.Append(this.Body);
			}
			if (this.Image != null && this.__isset.image)
			{
				if (!flag)
				{
					stringBuilder.Append(", ");
				}
				flag = false;
				stringBuilder.Append("Image: ");
				stringBuilder.Append(this.Image);
			}
			if (this.SubTitle != null && this.__isset.subTitle)
			{
				if (!flag)
				{
					stringBuilder.Append(", ");
				}
				flag = false;
				stringBuilder.Append("SubTitle: ");
				stringBuilder.Append(this.SubTitle);
			}
			if (this.Background != null && this.__isset.background)
			{
				if (!flag)
				{
					stringBuilder.Append(", ");
				}
				flag = false;
				stringBuilder.Append("Background: ");
				stringBuilder.Append(this.Background);
			}
			if (this.__isset.timeout)
			{
				if (!flag)
				{
					stringBuilder.Append(", ");
				}
				stringBuilder.Append("Timeout: ");
				stringBuilder.Append(this.Timeout);
			}
			stringBuilder.Append(")");
			return stringBuilder.ToString();
		}

		// Token: 0x04000196 RID: 406
		private string _id;

		// Token: 0x04000197 RID: 407
		private string _body;

		// Token: 0x04000198 RID: 408
		private List<string> _image;

		// Token: 0x04000199 RID: 409
		private string _subTitle;

		// Token: 0x0400019A RID: 410
		private string _background;

		// Token: 0x0400019B RID: 411
		private int _timeout;

		// Token: 0x0400019C RID: 412
		public NotificationParams.Isset __isset;

		// Token: 0x02000059 RID: 89
		[Serializable]
		public struct Isset
		{
			// Token: 0x0400019D RID: 413
			public bool id;

			// Token: 0x0400019E RID: 414
			public bool body;

			// Token: 0x0400019F RID: 415
			public bool image;

			// Token: 0x040001A0 RID: 416
			public bool subTitle;

			// Token: 0x040001A1 RID: 417
			public bool background;

			// Token: 0x040001A2 RID: 418
			public bool timeout;
		}
	}
}
