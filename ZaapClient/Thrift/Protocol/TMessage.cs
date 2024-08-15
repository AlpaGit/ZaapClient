using System;

namespace Thrift.Protocol
{
	// Token: 0x02000017 RID: 23
	public struct TMessage
	{
		// Token: 0x060000EC RID: 236 RVA: 0x00004C10 File Offset: 0x00002E10
		public TMessage(string name, TMessageType type, int seqid)
		{
			this = default(TMessage);
			this.name = name;
			this.type = type;
			this.seqID = seqid;
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x060000ED RID: 237 RVA: 0x00004C30 File Offset: 0x00002E30
		// (set) Token: 0x060000EE RID: 238 RVA: 0x00004C38 File Offset: 0x00002E38
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x060000EF RID: 239 RVA: 0x00004C44 File Offset: 0x00002E44
		// (set) Token: 0x060000F0 RID: 240 RVA: 0x00004C4C File Offset: 0x00002E4C
		public TMessageType Type
		{
			get
			{
				return this.type;
			}
			set
			{
				this.type = value;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x060000F1 RID: 241 RVA: 0x00004C58 File Offset: 0x00002E58
		// (set) Token: 0x060000F2 RID: 242 RVA: 0x00004C60 File Offset: 0x00002E60
		public int SeqID
		{
			get
			{
				return this.seqID;
			}
			set
			{
				this.seqID = value;
			}
		}

		// Token: 0x0400005D RID: 93
		private string name;

		// Token: 0x0400005E RID: 94
		private TMessageType type;

		// Token: 0x0400005F RID: 95
		private int seqID;
	}
}
