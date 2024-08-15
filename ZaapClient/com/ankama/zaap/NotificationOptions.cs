using System;
using System.Collections.Generic;
using System.Text;
using Thrift.Protocol;

namespace com.ankama.zaap
{
	// Token: 0x02000054 RID: 84
	[Serializable]
	public class NotificationOptions : TBase, TAbstractBase
	{
		// Token: 0x1700003C RID: 60
		// (get) Token: 0x0600027B RID: 635 RVA: 0x00009FB0 File Offset: 0x000081B0
		// (set) Token: 0x0600027C RID: 636 RVA: 0x00009FB8 File Offset: 0x000081B8
		public string Title
		{
			get
			{
				return this._title;
			}
			set
			{
				this.__isset.title = true;
				this._title = value;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x0600027D RID: 637 RVA: 0x00009FD0 File Offset: 0x000081D0
		// (set) Token: 0x0600027E RID: 638 RVA: 0x00009FD8 File Offset: 0x000081D8
		public string Subtitle
		{
			get
			{
				return this._subtitle;
			}
			set
			{
				this.__isset.subtitle = true;
				this._subtitle = value;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x0600027F RID: 639 RVA: 0x00009FF0 File Offset: 0x000081F0
		// (set) Token: 0x06000280 RID: 640 RVA: 0x00009FF8 File Offset: 0x000081F8
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

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000281 RID: 641 RVA: 0x0000A010 File Offset: 0x00008210
		// (set) Token: 0x06000282 RID: 642 RVA: 0x0000A018 File Offset: 0x00008218
		public bool Silent
		{
			get
			{
				return this._silent;
			}
			set
			{
				this.__isset.silent = true;
				this._silent = value;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000283 RID: 643 RVA: 0x0000A030 File Offset: 0x00008230
		// (set) Token: 0x06000284 RID: 644 RVA: 0x0000A038 File Offset: 0x00008238
		public string Icon
		{
			get
			{
				return this._icon;
			}
			set
			{
				this.__isset.icon = true;
				this._icon = value;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000285 RID: 645 RVA: 0x0000A050 File Offset: 0x00008250
		// (set) Token: 0x06000286 RID: 646 RVA: 0x0000A058 File Offset: 0x00008258
		public bool HasReply
		{
			get
			{
				return this._hasReply;
			}
			set
			{
				this.__isset.hasReply = true;
				this._hasReply = value;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000287 RID: 647 RVA: 0x0000A070 File Offset: 0x00008270
		// (set) Token: 0x06000288 RID: 648 RVA: 0x0000A078 File Offset: 0x00008278
		public string TimeoutType
		{
			get
			{
				return this._timeoutType;
			}
			set
			{
				this.__isset.timeoutType = true;
				this._timeoutType = value;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000289 RID: 649 RVA: 0x0000A090 File Offset: 0x00008290
		// (set) Token: 0x0600028A RID: 650 RVA: 0x0000A098 File Offset: 0x00008298
		public string ReplyPlaceholder
		{
			get
			{
				return this._replyPlaceholder;
			}
			set
			{
				this.__isset.replyPlaceholder = true;
				this._replyPlaceholder = value;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x0600028B RID: 651 RVA: 0x0000A0B0 File Offset: 0x000082B0
		// (set) Token: 0x0600028C RID: 652 RVA: 0x0000A0B8 File Offset: 0x000082B8
		public string Sound
		{
			get
			{
				return this._sound;
			}
			set
			{
				this.__isset.sound = true;
				this._sound = value;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x0600028D RID: 653 RVA: 0x0000A0D0 File Offset: 0x000082D0
		// (set) Token: 0x0600028E RID: 654 RVA: 0x0000A0D8 File Offset: 0x000082D8
		public string Urgency
		{
			get
			{
				return this._urgency;
			}
			set
			{
				this.__isset.urgency = true;
				this._urgency = value;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x0600028F RID: 655 RVA: 0x0000A0F0 File Offset: 0x000082F0
		// (set) Token: 0x06000290 RID: 656 RVA: 0x0000A0F8 File Offset: 0x000082F8
		public List<string> Actions
		{
			get
			{
				return this._actions;
			}
			set
			{
				this.__isset.actions = true;
				this._actions = value;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000291 RID: 657 RVA: 0x0000A110 File Offset: 0x00008310
		// (set) Token: 0x06000292 RID: 658 RVA: 0x0000A118 File Offset: 0x00008318
		public string CloseButtonText
		{
			get
			{
				return this._closeButtonText;
			}
			set
			{
				this.__isset.closeButtonText = true;
				this._closeButtonText = value;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000293 RID: 659 RVA: 0x0000A130 File Offset: 0x00008330
		// (set) Token: 0x06000294 RID: 660 RVA: 0x0000A138 File Offset: 0x00008338
		public string ToastXml
		{
			get
			{
				return this._toastXml;
			}
			set
			{
				this.__isset.toastXml = true;
				this._toastXml = value;
			}
		}

		// Token: 0x06000295 RID: 661 RVA: 0x0000A150 File Offset: 0x00008350
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
							this.Title = iprot.ReadString();
						}
						else
						{
							TProtocolUtil.Skip(iprot, tfield.Type);
						}
						break;
					case 2:
						if (tfield.Type == TType.String)
						{
							this.Subtitle = iprot.ReadString();
						}
						else
						{
							TProtocolUtil.Skip(iprot, tfield.Type);
						}
						break;
					case 3:
						if (tfield.Type == TType.String)
						{
							this.Body = iprot.ReadString();
						}
						else
						{
							TProtocolUtil.Skip(iprot, tfield.Type);
						}
						break;
					case 4:
						if (tfield.Type == TType.Bool)
						{
							this.Silent = iprot.ReadBool();
						}
						else
						{
							TProtocolUtil.Skip(iprot, tfield.Type);
						}
						break;
					case 5:
						if (tfield.Type == TType.String)
						{
							this.Icon = iprot.ReadString();
						}
						else
						{
							TProtocolUtil.Skip(iprot, tfield.Type);
						}
						break;
					case 6:
						if (tfield.Type == TType.Bool)
						{
							this.HasReply = iprot.ReadBool();
						}
						else
						{
							TProtocolUtil.Skip(iprot, tfield.Type);
						}
						break;
					case 7:
						if (tfield.Type == TType.String)
						{
							this.TimeoutType = iprot.ReadString();
						}
						else
						{
							TProtocolUtil.Skip(iprot, tfield.Type);
						}
						break;
					case 8:
						if (tfield.Type == TType.String)
						{
							this.ReplyPlaceholder = iprot.ReadString();
						}
						else
						{
							TProtocolUtil.Skip(iprot, tfield.Type);
						}
						break;
					case 9:
						if (tfield.Type == TType.String)
						{
							this.Sound = iprot.ReadString();
						}
						else
						{
							TProtocolUtil.Skip(iprot, tfield.Type);
						}
						break;
					case 10:
						if (tfield.Type == TType.String)
						{
							this.Urgency = iprot.ReadString();
						}
						else
						{
							TProtocolUtil.Skip(iprot, tfield.Type);
						}
						break;
					case 11:
						if (tfield.Type == TType.List)
						{
							this.Actions = new List<string>();
							TList tlist = iprot.ReadListBegin();
							for (int i = 0; i < tlist.Count; i++)
							{
								string text = iprot.ReadString();
								this.Actions.Add(text);
							}
							iprot.ReadListEnd();
						}
						else
						{
							TProtocolUtil.Skip(iprot, tfield.Type);
						}
						break;
					case 12:
						if (tfield.Type == TType.String)
						{
							this.CloseButtonText = iprot.ReadString();
						}
						else
						{
							TProtocolUtil.Skip(iprot, tfield.Type);
						}
						break;
					case 13:
						if (tfield.Type == TType.String)
						{
							this.ToastXml = iprot.ReadString();
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

		// Token: 0x06000296 RID: 662 RVA: 0x0000A4CC File Offset: 0x000086CC
		public void Write(TProtocol oprot)
		{
			oprot.IncrementRecursionDepth();
			try
			{
				TStruct tstruct = new TStruct("NotificationOptions");
				oprot.WriteStructBegin(tstruct);
				TField tfield = default(TField);
				if (this.Title != null && this.__isset.title)
				{
					tfield.Name = "title";
					tfield.Type = TType.String;
					tfield.ID = 1;
					oprot.WriteFieldBegin(tfield);
					oprot.WriteString(this.Title);
					oprot.WriteFieldEnd();
				}
				if (this.Subtitle != null && this.__isset.subtitle)
				{
					tfield.Name = "subtitle";
					tfield.Type = TType.String;
					tfield.ID = 2;
					oprot.WriteFieldBegin(tfield);
					oprot.WriteString(this.Subtitle);
					oprot.WriteFieldEnd();
				}
				if (this.Body != null && this.__isset.body)
				{
					tfield.Name = "body";
					tfield.Type = TType.String;
					tfield.ID = 3;
					oprot.WriteFieldBegin(tfield);
					oprot.WriteString(this.Body);
					oprot.WriteFieldEnd();
				}
				if (this.__isset.silent)
				{
					tfield.Name = "silent";
					tfield.Type = TType.Bool;
					tfield.ID = 4;
					oprot.WriteFieldBegin(tfield);
					oprot.WriteBool(this.Silent);
					oprot.WriteFieldEnd();
				}
				if (this.Icon != null && this.__isset.icon)
				{
					tfield.Name = "icon";
					tfield.Type = TType.String;
					tfield.ID = 5;
					oprot.WriteFieldBegin(tfield);
					oprot.WriteString(this.Icon);
					oprot.WriteFieldEnd();
				}
				if (this.__isset.hasReply)
				{
					tfield.Name = "hasReply";
					tfield.Type = TType.Bool;
					tfield.ID = 6;
					oprot.WriteFieldBegin(tfield);
					oprot.WriteBool(this.HasReply);
					oprot.WriteFieldEnd();
				}
				if (this.TimeoutType != null && this.__isset.timeoutType)
				{
					tfield.Name = "timeoutType";
					tfield.Type = TType.String;
					tfield.ID = 7;
					oprot.WriteFieldBegin(tfield);
					oprot.WriteString(this.TimeoutType);
					oprot.WriteFieldEnd();
				}
				if (this.ReplyPlaceholder != null && this.__isset.replyPlaceholder)
				{
					tfield.Name = "replyPlaceholder";
					tfield.Type = TType.String;
					tfield.ID = 8;
					oprot.WriteFieldBegin(tfield);
					oprot.WriteString(this.ReplyPlaceholder);
					oprot.WriteFieldEnd();
				}
				if (this.Sound != null && this.__isset.sound)
				{
					tfield.Name = "sound";
					tfield.Type = TType.String;
					tfield.ID = 9;
					oprot.WriteFieldBegin(tfield);
					oprot.WriteString(this.Sound);
					oprot.WriteFieldEnd();
				}
				if (this.Urgency != null && this.__isset.urgency)
				{
					tfield.Name = "urgency";
					tfield.Type = TType.String;
					tfield.ID = 10;
					oprot.WriteFieldBegin(tfield);
					oprot.WriteString(this.Urgency);
					oprot.WriteFieldEnd();
				}
				if (this.Actions != null && this.__isset.actions)
				{
					tfield.Name = "actions";
					tfield.Type = TType.List;
					tfield.ID = 11;
					oprot.WriteFieldBegin(tfield);
					oprot.WriteListBegin(new TList(TType.String, this.Actions.Count));
					foreach (string text in this.Actions)
					{
						oprot.WriteString(text);
					}
					oprot.WriteListEnd();
					oprot.WriteFieldEnd();
				}
				if (this.CloseButtonText != null && this.__isset.closeButtonText)
				{
					tfield.Name = "closeButtonText";
					tfield.Type = TType.String;
					tfield.ID = 12;
					oprot.WriteFieldBegin(tfield);
					oprot.WriteString(this.CloseButtonText);
					oprot.WriteFieldEnd();
				}
				if (this.ToastXml != null && this.__isset.toastXml)
				{
					tfield.Name = "toastXml";
					tfield.Type = TType.String;
					tfield.ID = 13;
					oprot.WriteFieldBegin(tfield);
					oprot.WriteString(this.ToastXml);
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

		// Token: 0x06000297 RID: 663 RVA: 0x0000A9A4 File Offset: 0x00008BA4
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder("NotificationOptions(");
			bool flag = true;
			if (this.Title != null && this.__isset.title)
			{
				if (!flag)
				{
					stringBuilder.Append(", ");
				}
				flag = false;
				stringBuilder.Append("Title: ");
				stringBuilder.Append(this.Title);
			}
			if (this.Subtitle != null && this.__isset.subtitle)
			{
				if (!flag)
				{
					stringBuilder.Append(", ");
				}
				flag = false;
				stringBuilder.Append("Subtitle: ");
				stringBuilder.Append(this.Subtitle);
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
			if (this.__isset.silent)
			{
				if (!flag)
				{
					stringBuilder.Append(", ");
				}
				flag = false;
				stringBuilder.Append("Silent: ");
				stringBuilder.Append(this.Silent);
			}
			if (this.Icon != null && this.__isset.icon)
			{
				if (!flag)
				{
					stringBuilder.Append(", ");
				}
				flag = false;
				stringBuilder.Append("Icon: ");
				stringBuilder.Append(this.Icon);
			}
			if (this.__isset.hasReply)
			{
				if (!flag)
				{
					stringBuilder.Append(", ");
				}
				flag = false;
				stringBuilder.Append("HasReply: ");
				stringBuilder.Append(this.HasReply);
			}
			if (this.TimeoutType != null && this.__isset.timeoutType)
			{
				if (!flag)
				{
					stringBuilder.Append(", ");
				}
				flag = false;
				stringBuilder.Append("TimeoutType: ");
				stringBuilder.Append(this.TimeoutType);
			}
			if (this.ReplyPlaceholder != null && this.__isset.replyPlaceholder)
			{
				if (!flag)
				{
					stringBuilder.Append(", ");
				}
				flag = false;
				stringBuilder.Append("ReplyPlaceholder: ");
				stringBuilder.Append(this.ReplyPlaceholder);
			}
			if (this.Sound != null && this.__isset.sound)
			{
				if (!flag)
				{
					stringBuilder.Append(", ");
				}
				flag = false;
				stringBuilder.Append("Sound: ");
				stringBuilder.Append(this.Sound);
			}
			if (this.Urgency != null && this.__isset.urgency)
			{
				if (!flag)
				{
					stringBuilder.Append(", ");
				}
				flag = false;
				stringBuilder.Append("Urgency: ");
				stringBuilder.Append(this.Urgency);
			}
			if (this.Actions != null && this.__isset.actions)
			{
				if (!flag)
				{
					stringBuilder.Append(", ");
				}
				flag = false;
				stringBuilder.Append("Actions: ");
				stringBuilder.Append(this.Actions);
			}
			if (this.CloseButtonText != null && this.__isset.closeButtonText)
			{
				if (!flag)
				{
					stringBuilder.Append(", ");
				}
				flag = false;
				stringBuilder.Append("CloseButtonText: ");
				stringBuilder.Append(this.CloseButtonText);
			}
			if (this.ToastXml != null && this.__isset.toastXml)
			{
				if (!flag)
				{
					stringBuilder.Append(", ");
				}
				stringBuilder.Append("ToastXml: ");
				stringBuilder.Append(this.ToastXml);
			}
			stringBuilder.Append(")");
			return stringBuilder.ToString();
		}

		// Token: 0x04000172 RID: 370
		private string _title;

		// Token: 0x04000173 RID: 371
		private string _subtitle;

		// Token: 0x04000174 RID: 372
		private string _body;

		// Token: 0x04000175 RID: 373
		private bool _silent;

		// Token: 0x04000176 RID: 374
		private string _icon;

		// Token: 0x04000177 RID: 375
		private bool _hasReply;

		// Token: 0x04000178 RID: 376
		private string _timeoutType;

		// Token: 0x04000179 RID: 377
		private string _replyPlaceholder;

		// Token: 0x0400017A RID: 378
		private string _sound;

		// Token: 0x0400017B RID: 379
		private string _urgency;

		// Token: 0x0400017C RID: 380
		private List<string> _actions;

		// Token: 0x0400017D RID: 381
		private string _closeButtonText;

		// Token: 0x0400017E RID: 382
		private string _toastXml;

		// Token: 0x0400017F RID: 383
		public NotificationOptions.Isset __isset;

		// Token: 0x02000055 RID: 85
		[Serializable]
		public struct Isset
		{
			// Token: 0x04000180 RID: 384
			public bool title;

			// Token: 0x04000181 RID: 385
			public bool subtitle;

			// Token: 0x04000182 RID: 386
			public bool body;

			// Token: 0x04000183 RID: 387
			public bool silent;

			// Token: 0x04000184 RID: 388
			public bool icon;

			// Token: 0x04000185 RID: 389
			public bool hasReply;

			// Token: 0x04000186 RID: 390
			public bool timeoutType;

			// Token: 0x04000187 RID: 391
			public bool replyPlaceholder;

			// Token: 0x04000188 RID: 392
			public bool sound;

			// Token: 0x04000189 RID: 393
			public bool urgency;

			// Token: 0x0400018A RID: 394
			public bool actions;

			// Token: 0x0400018B RID: 395
			public bool closeButtonText;

			// Token: 0x0400018C RID: 396
			public bool toastXml;
		}
	}
}
