using System;
using System.Text;
using Thrift.Protocol;

namespace com.ankama.zaap
{
	// Token: 0x02000052 RID: 82
	[Serializable]
	public class OverlayPosition : TBase, TAbstractBase
	{
		// Token: 0x0600026E RID: 622 RVA: 0x00009AC8 File Offset: 0x00007CC8
		public OverlayPosition()
		{
			this._posX = 25;
			this.__isset.posX = true;
			this._posY = 25;
			this.__isset.posY = true;
			this._width = 50;
			this.__isset.width = true;
			this._height = 50;
			this.__isset.height = true;
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x0600026F RID: 623 RVA: 0x00009B2C File Offset: 0x00007D2C
		// (set) Token: 0x06000270 RID: 624 RVA: 0x00009B34 File Offset: 0x00007D34
		public int PosX
		{
			get
			{
				return this._posX;
			}
			set
			{
				this.__isset.posX = true;
				this._posX = value;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000271 RID: 625 RVA: 0x00009B4C File Offset: 0x00007D4C
		// (set) Token: 0x06000272 RID: 626 RVA: 0x00009B54 File Offset: 0x00007D54
		public int PosY
		{
			get
			{
				return this._posY;
			}
			set
			{
				this.__isset.posY = true;
				this._posY = value;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000273 RID: 627 RVA: 0x00009B6C File Offset: 0x00007D6C
		// (set) Token: 0x06000274 RID: 628 RVA: 0x00009B74 File Offset: 0x00007D74
		public int Width
		{
			get
			{
				return this._width;
			}
			set
			{
				this.__isset.width = true;
				this._width = value;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000275 RID: 629 RVA: 0x00009B8C File Offset: 0x00007D8C
		// (set) Token: 0x06000276 RID: 630 RVA: 0x00009B94 File Offset: 0x00007D94
		public int Height
		{
			get
			{
				return this._height;
			}
			set
			{
				this.__isset.height = true;
				this._height = value;
			}
		}

		// Token: 0x06000277 RID: 631 RVA: 0x00009BAC File Offset: 0x00007DAC
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
						if (tfield.Type == TType.I32)
						{
							this.PosX = iprot.ReadI32();
						}
						else
						{
							TProtocolUtil.Skip(iprot, tfield.Type);
						}
						break;
					case 2:
						if (tfield.Type == TType.I32)
						{
							this.PosY = iprot.ReadI32();
						}
						else
						{
							TProtocolUtil.Skip(iprot, tfield.Type);
						}
						break;
					case 3:
						if (tfield.Type == TType.I32)
						{
							this.Width = iprot.ReadI32();
						}
						else
						{
							TProtocolUtil.Skip(iprot, tfield.Type);
						}
						break;
					case 4:
						if (tfield.Type == TType.I32)
						{
							this.Height = iprot.ReadI32();
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

		// Token: 0x06000278 RID: 632 RVA: 0x00009D10 File Offset: 0x00007F10
		public void Write(TProtocol oprot)
		{
			oprot.IncrementRecursionDepth();
			try
			{
				TStruct tstruct = new TStruct("OverlayPosition");
				oprot.WriteStructBegin(tstruct);
				TField tfield = default(TField);
				if (this.__isset.posX)
				{
					tfield.Name = "posX";
					tfield.Type = TType.I32;
					tfield.ID = 1;
					oprot.WriteFieldBegin(tfield);
					oprot.WriteI32(this.PosX);
					oprot.WriteFieldEnd();
				}
				if (this.__isset.posY)
				{
					tfield.Name = "posY";
					tfield.Type = TType.I32;
					tfield.ID = 2;
					oprot.WriteFieldBegin(tfield);
					oprot.WriteI32(this.PosY);
					oprot.WriteFieldEnd();
				}
				if (this.__isset.width)
				{
					tfield.Name = "width";
					tfield.Type = TType.I32;
					tfield.ID = 3;
					oprot.WriteFieldBegin(tfield);
					oprot.WriteI32(this.Width);
					oprot.WriteFieldEnd();
				}
				if (this.__isset.height)
				{
					tfield.Name = "height";
					tfield.Type = TType.I32;
					tfield.ID = 4;
					oprot.WriteFieldBegin(tfield);
					oprot.WriteI32(this.Height);
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

		// Token: 0x06000279 RID: 633 RVA: 0x00009E88 File Offset: 0x00008088
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder("OverlayPosition(");
			bool flag = true;
			if (this.__isset.posX)
			{
				if (!flag)
				{
					stringBuilder.Append(", ");
				}
				flag = false;
				stringBuilder.Append("PosX: ");
				stringBuilder.Append(this.PosX);
			}
			if (this.__isset.posY)
			{
				if (!flag)
				{
					stringBuilder.Append(", ");
				}
				flag = false;
				stringBuilder.Append("PosY: ");
				stringBuilder.Append(this.PosY);
			}
			if (this.__isset.width)
			{
				if (!flag)
				{
					stringBuilder.Append(", ");
				}
				flag = false;
				stringBuilder.Append("Width: ");
				stringBuilder.Append(this.Width);
			}
			if (this.__isset.height)
			{
				if (!flag)
				{
					stringBuilder.Append(", ");
				}
				stringBuilder.Append("Height: ");
				stringBuilder.Append(this.Height);
			}
			stringBuilder.Append(")");
			return stringBuilder.ToString();
		}

		// Token: 0x04000169 RID: 361
		private int _posX;

		// Token: 0x0400016A RID: 362
		private int _posY;

		// Token: 0x0400016B RID: 363
		private int _width;

		// Token: 0x0400016C RID: 364
		private int _height;

		// Token: 0x0400016D RID: 365
		public OverlayPosition.Isset __isset;

		// Token: 0x02000053 RID: 83
		[Serializable]
		public struct Isset
		{
			// Token: 0x0400016E RID: 366
			public bool posX;

			// Token: 0x0400016F RID: 367
			public bool posY;

			// Token: 0x04000170 RID: 368
			public bool width;

			// Token: 0x04000171 RID: 369
			public bool height;
		}
	}
}
